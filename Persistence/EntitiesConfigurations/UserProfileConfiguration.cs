using Career_Path.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            // ---- Primary Key ----
            builder.HasKey(x => x.ID);

            // تحديد أن الـ ID من نوع string بحد أقصى 36 حرف
            //builder.Property(x => x.ID)
            //       .HasMaxLength(36)
            //       .IsRequired()
            //       .ValueGeneratedNever(); // ✅ لأنه يتم توليده في الكود وليس من قاعدة البيانات

            // ---- Properties ----
            // Gender (Optional)
            builder.Property(x => x.Gender)
                   .HasConversion<string>()
                   .HasMaxLength(10);

            // Basic Info (Optional)
            builder.Property(x => x.Country)
                   .HasMaxLength(100);

            builder.Property(x => x.City)
                   .HasMaxLength(100);

            // Career Info (Optional)
            builder.Property(x => x.JobTitle)
                   .HasMaxLength(200);

            builder.Property(x => x.YearsOfExperience);

            builder.Property(x => x.CurrentCompany)
                   .HasMaxLength(200);

            // Education Info (Optional)
            builder.Property(x => x.University)
                   .HasMaxLength(200);

            builder.Property(x => x.Degree)
                   .HasMaxLength(100);

            builder.Property(x => x.GraduationYear);

            // Profile Info
            builder.Property(x => x.Summary)
                   .HasMaxLength(500);

            builder.Property(x => x.ProfilePictureUrl)
                   .HasMaxLength(500);

            builder.Property(x => x.CvFileUrl)
                   .HasMaxLength(500);

            // ---- Relationships ----
            builder.HasOne(x => x.ApplicationUser)
                   .WithOne(u => u.UserProfile)
                   .HasForeignKey<UserProfile>(x => x.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Skills)
                   .WithOne(s => s.UserProfile)
                   .HasForeignKey(s => s.UserProfileId) // ✅ تأكد من إضافة اسم الـ Foreign Key
                   .OnDelete(DeleteBehavior.Cascade);

            // ---- Indexes ----
            builder.HasIndex(x => x.ApplicationUserId)
                   .IsUnique();

            // ---- Table Name ----
            builder.ToTable("UserProfiles");
        }
    }
}