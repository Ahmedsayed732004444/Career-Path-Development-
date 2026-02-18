namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class RoadmapPhaseConfiguration : IEntityTypeConfiguration<RoadmapPhase>
    {
        public void Configure(EntityTypeBuilder<RoadmapPhase> builder)
        {
            builder.HasMany(p => p.Skills)
                .WithOne(s => s.RoadmapPhase)
                .HasForeignKey(s => s.RoadmapPhaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Resources)
                .WithOne(r => r.RoadmapPhase)
                .HasForeignKey(r => r.RoadmapPhaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}