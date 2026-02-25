using System.Text.Json;

namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            // Primary Key
            builder.HasKey(j => j.Id);

            // معلومات الوظيفة
            builder.Property(j => j.JobTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.JobDescription)
                .IsRequired()
                .HasMaxLength(2000);

            // معلومات إضافية
            builder.Property(j => j.Location)
                .HasMaxLength(200);

            builder.Property(j => j.JobType)
                .HasMaxLength(100);

            builder.Property(j => j.ExperienceLevel)
                .HasConversion<string>()
                .HasMaxLength(100);

            // Salary
            builder.Property(j => j.SalaryMin)
                .HasPrecision(18, 2);

            builder.Property(j => j.SalaryMax)
                .HasPrecision(18, 2);

            // التواريخ
            builder.Property(j => j.PostedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(j => j.DeadlineDate)
                .IsRequired(false);

            // Status
            builder.Property(j => j.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Lists (تخزين كـ JSON)
            builder.Property(j => j.JobRequirements)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
                )
                .HasColumnType("nvarchar(max)");

            // العلاقة مع ApplicationUser (Company)
            builder.HasOne(j => j.Company)
                .WithMany()
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index للبحث السريع
            builder.HasIndex(j => j.CompanyId);
            builder.HasIndex(j => j.IsActive);
            builder.HasIndex(j => j.PostedDate);
        }
    }
}