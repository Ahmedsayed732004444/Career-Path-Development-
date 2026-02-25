using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class updatepermition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 13, "permissions", "membershipUpgradeRequests:read", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 14, "permissions", "membershipUpgradeRequests:approve", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 15, "permissions", "membershipUpgradeRequests:reject", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
