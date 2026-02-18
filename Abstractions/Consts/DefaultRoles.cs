namespace Career_Path.Abstractions.Consts;

public static class DefaultRoles
{
    public partial class Admin
    {
        public const string Name = nameof(Admin);
        public const string Id = "0191a4b6-c4fc-752e-9d95-40b5e4e68054";
        public const string ConcurrencyStamp = "0191a4b6-c4fc-752e-9d95-40b631d1866d";
    }
    public partial class Company
    {
        public const string Name = nameof(Company);
        public const string Id = "647f9fdc-4677-473b-a656-4deb7000478c";
        public const string ConcurrencyStamp = "fb32d7a4-c53f-421d-bbd3-2b00dd57fa1d";
    }
    public partial class Member
    {
        public const string Name = nameof(Member);
        public const string Id = "0191a4b6-c4fc-752e-9d95-40b7a5cb88f0";
        public const string ConcurrencyStamp = "0191a4b6-c4fc-752e-9d95-40b85cf3fd22";
    }

}