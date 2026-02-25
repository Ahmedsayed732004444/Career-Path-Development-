using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class dropjob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. امسح الـ Foreign Keys أولاً
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_ApplicationUserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_CompanyId",
                table: "Jobs");

            // 2. امسح الـ Primary Key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            // 3. امسح الـ Id column القديم
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Jobs");

            // 4. أضف الـ Id column الجديد
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // 5. أعد إنشاء الـ Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");

            // 6. أعد إنشاء الـ Foreign Keys
            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_ApplicationUserId",
                table: "Jobs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_ApplicationUserId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_CompanyId",
                table: "Job");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Job",
                table: "Job");

            migrationBuilder.RenameTable(
                name: "Job",
                newName: "Jobs");

            migrationBuilder.RenameIndex(
                name: "IX_Job_PostedDate",
                table: "Jobs",
                newName: "IX_Jobs_PostedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Job_IsActive",
                table: "Jobs",
                newName: "IX_Jobs_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Job_CompanyId",
                table: "Jobs",
                newName: "IX_Jobs_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Job_ApplicationUserId",
                table: "Jobs",
                newName: "IX_Jobs_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Jobs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_ApplicationUserId",
                table: "Jobs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
