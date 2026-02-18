using Asp.Versioning.ApiExplorer;
using Career_Path.Contracts.Matching;
using Career_Path.Persistence;
using Career_Path.Settings;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Career_Path
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    );
                });

            services.AddOpenApi();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                //.AllowCredentials()
                // .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                )
            );

            services.AddAuthConfig(configuration);

            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services
                .AddMapsterConfig()
                .AddFluentValidationConfig();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<IExtractionService, ExtractionService>();
            services.AddScoped<IRoadmapService, RoadmapService>();
            services.AddScoped<IJobSearchService, JobSearchService>();
            services.AddScoped<IRemoteOkScraperService, RemoteOkScraperService>();
            services.AddScoped<IGitHubAuthService, GitHubAuthService>();  // ✅ أضفت
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IUserService, UserService>();

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddOptions<MailSettings>()
                .BindConfiguration(nameof(MailSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }

        // ==================== Mapster ====================
        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;
        }

        // ==================== FluentValidation ====================
        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        // ==================== AUTH CONFIG ====================
        private static IServiceCollection AddAuthConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IJwtProvider, JwtProvider>();

            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var jwtSettings = configuration
                .GetSection(JwtOptions.SectionName)
                .Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                // JWT هو الافتراضي
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // ⭐ Cookie ضروري لـ External Login
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            // JWT
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings!.Key)
                    ),
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience
                };
            })
            // GitHub OAuth
            .AddGitHub(options =>
            {
                options.ClientId = configuration["Auth:GitHub:ClientId"]!;
                options.ClientSecret = configuration["Auth:GitHub:ClientSecret"]!;
                options.Scope.Add("user:email");
            })
            // ✅ Google OAuth - استخدم configuration بدل builder.Configuration
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
                options.Scope.Add("profile");
                options.Scope.Add("email");
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }
    }
}