namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class MembershipUpgradeConfiguratrion : IEntityTypeConfiguration<MembershipUpgrade>
    {
        public void Configure(EntityTypeBuilder<MembershipUpgrade> builder)
        {

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.RequestedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.Status)
                    .HasConversion<string>()
                   .HasDefaultValue(RequestStatus.Pending);

            builder.Property(x => x.Note)
                   .HasMaxLength(1000);
        }
    }
}
