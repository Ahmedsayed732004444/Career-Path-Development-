namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class PrompetRoadMapConfiguration : IEntityTypeConfiguration<PrompetRoadMap>
    {
        public void Configure(EntityTypeBuilder<PrompetRoadMap> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            // UserId هو الـ FK على AspNetUsers
            builder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.PrompetRoadMaps)
                .HasForeignKey(x => x.UserId)   // ← UserId هو الـ FK
                .IsRequired(false)              // ← optional عشان الـ Python service
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}