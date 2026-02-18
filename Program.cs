using Career_Path;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// 1️⃣ HTTPS Redirection
app.UseHttpsRedirection();

// 2️⃣ Static Files
app.UseStaticFiles();

// 3️⃣ CORS (قبل Authentication)
app.UseCors();

// 4️⃣ Authentication (قبل Authorization)
app.UseAuthentication();

// 5️⃣ Authorization
app.UseAuthorization();

// 6️⃣ Swagger (Development only - optional)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "careerPath V1");
    });
}

// 7️⃣ Controllers
app.MapControllers();

app.Run();