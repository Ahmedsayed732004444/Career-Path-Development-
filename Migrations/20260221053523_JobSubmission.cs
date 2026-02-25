using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class JobSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobSubmissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CVPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSubmissions_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSubmissions_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSubmissions_ApplicantId_JobId",
                table: "JobSubmissions",
                columns: new[] { "ApplicantId", "JobId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSubmissions_JobId",
                table: "JobSubmissions",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSubmissions");
        }
    }
}
