using Intelligent_Career_Advisor.Models;

namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            // Primary Key
            builder.HasKey(j => j.Id);

            // Properties
            builder.Property(j => j.JobTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.ApplicationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(j => j.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(j => j.ApplicationSource)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(j => j.Notes)
                .HasMaxLength(1000);


            // Foreign Key
            builder.Property(j => j.ApplicationUserId)
                .IsRequired();

            // Relationship
            builder.HasOne(j => j.ApplicationUser)
                .WithMany()
                .HasForeignKey(j => j.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(j => j.ApplicationUserId);
            builder.HasIndex(j => j.Status);
            builder.HasIndex(j => j.ApplicationDate);
        }
    }
}
