namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.UserProfile)
                .WithMany(u => u.Skills)
                .OnDelete(DeleteBehavior.Cascade);

            // Table Name
            builder.ToTable("Skills");


        }
    }
}