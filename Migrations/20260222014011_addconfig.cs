using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class addconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrompetRoadMap_AspNetUsers_ApplicationUserId",
                table: "PrompetRoadMap");

            migrationBuilder.DropIndex(
                name: "IX_PrompetRoadMap_ApplicationUserId",
                table: "PrompetRoadMap");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PrompetRoadMap");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PrompetRoadMap",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PrompetRoadMap_UserId",
                table: "PrompetRoadMap",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrompetRoadMap_AspNetUsers_UserId",
                table: "PrompetRoadMap",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrompetRoadMap_AspNetUsers_UserId",
                table: "PrompetRoadMap");

            migrationBuilder.DropIndex(
                name: "IX_PrompetRoadMap_UserId",
                table: "PrompetRoadMap");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PrompetRoadMap",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PrompetRoadMap",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PrompetRoadMap_ApplicationUserId",
                table: "PrompetRoadMap",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrompetRoadMap_AspNetUsers_ApplicationUserId",
                table: "PrompetRoadMap",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
