using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class addone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 12, "permissions", "jobApplicants:read", "0191a4b6-c4fc-752e-9d95-40b5e4e68054" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
