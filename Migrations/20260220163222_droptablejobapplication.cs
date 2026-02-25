using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class droptablejobapplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_ApplicationUserId",
                table: "JobApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "AttachmentUrl",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JobApplications");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                newName: "JobApplication");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_ApplicationUserId",
                table: "JobApplication",
                newName: "IX_JobApplication_ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "JobApplication",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "JobApplication",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobApplication",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "JobApplication",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationSource",
                table: "JobApplication",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDate",
                table: "JobApplication",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_ApplicationDate",
                table: "JobApplication",
                column: "ApplicationDate");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_Status",
                table: "JobApplication",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_AspNetUsers_ApplicationUserId",
                table: "JobApplication",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_AspNetUsers_ApplicationUserId",
                table: "JobApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_ApplicationDate",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_Status",
                table: "JobApplication");

            migrationBuilder.RenameTable(
                name: "JobApplication",
                newName: "JobApplications");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_ApplicationUserId",
                table: "JobApplications",
                newName: "IX_JobApplications_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "JobApplications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationSource",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDate",
                table: "JobApplications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentUrl",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
