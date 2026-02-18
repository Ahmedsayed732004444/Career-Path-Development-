namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class ModelExtrationConfiguration: IEntityTypeConfiguration<ModelExtration>
    {
        public void Configure(EntityTypeBuilder<ModelExtration> builder)
        {
            builder.HasKey(x => x.Id);

            // Owned collections
            builder.OwnsMany(x => x.Education);
            builder.OwnsMany(x => x.Experience);
        }
    }
}
