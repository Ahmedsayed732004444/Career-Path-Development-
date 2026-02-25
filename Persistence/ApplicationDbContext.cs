using Intelligent_Career_Advisor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Career_Path.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)

{

    public DbSet<UserProfile> UserProfiles { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<ModelExtration> ModelExtrations { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Roadmap> Roadmaps { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobSubmission> JobSubmissions { get; set; }
    public DbSet<MembershipUpgrade> MembershipUpgrades { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);
    }

}