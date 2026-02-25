using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class droptablej : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_AspNetUsers_ApplicationUserId",
                table: "JobApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication");

            migrationBuilder.RenameTable(
                name: "JobApplication",
                newName: "JobApplications");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_Status",
                table: "JobApplications",
                newName: "IX_JobApplications_Status");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_ApplicationUserId",
                table: "JobApplications",
                newName: "IX_JobApplications_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_ApplicationDate",
                table: "JobApplications",
                newName: "IX_JobApplications_ApplicationDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_ApplicationUserId",
                table: "JobApplications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_ApplicationUserId",
                table: "JobApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                newName: "JobApplication");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_Status",
                table: "JobApplication",
                newName: "IX_JobApplication_Status");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_ApplicationUserId",
                table: "JobApplication",
                newName: "IX_JobApplication_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_ApplicationDate",
                table: "JobApplication",
                newName: "IX_JobApplication_ApplicationDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_AspNetUsers_ApplicationUserId",
                table: "JobApplication",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
