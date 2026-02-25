
namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class JobSubmissionConfiguration : IEntityTypeConfiguration<JobSubmission>
    {
        public void Configure(EntityTypeBuilder<JobSubmission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.CVPath)
                   .IsRequired();

            builder.Property(x => x.Notes)
                   .HasMaxLength(1000);

            builder.HasOne(x => x.ApplicationUser)
                   .WithMany(u => u.JobSubmissions)
                   .HasForeignKey(x => x.ApplicantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Job)
                   .WithMany(j => j.JobSubmissions)
                   .HasForeignKey(x => x.JobId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.ApplicantId, x.JobId })
                   .IsUnique(); // يمنع التقديم مرتين

        }
    }
}
