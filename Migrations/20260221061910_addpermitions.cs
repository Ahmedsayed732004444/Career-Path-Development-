using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class addpermitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "users:read", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 2, "permissions", "users:add", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 3, "permissions", "users:update", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 4, "permissions", "roles:read", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 5, "permissions", "roles:add", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 6, "permissions", "roles:update", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 7, "permissions", "profile:read", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 8, "permissions", "profile:update", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 9, "permissions", "jobs:read", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 10, "permissions", "jobs:add", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" },
                    { 11, "permissions", "jobs:update", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
