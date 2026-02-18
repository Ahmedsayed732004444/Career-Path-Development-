namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class RoadmapConfiguration : IEntityTypeConfiguration<Roadmap>  // ✅ Roadmap
    {
        public void Configure(EntityTypeBuilder<Roadmap> builder)  // ✅ Roadmap
        {
            builder.HasOne(r => r.ApplicationUser)
                .WithMany()
                .HasForeignKey(r => r.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Phases)
                .WithOne(p => p.Roadmap)
                .HasForeignKey(p => p.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.ProjectImprovements)
                .WithOne(pi => pi.Roadmap)
                .HasForeignKey(pi => pi.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}