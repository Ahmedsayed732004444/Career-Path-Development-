using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class addjobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    JobRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ExperienceLevel = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SalaryMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalaryMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_AspNetUsers_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ApplicationUserId",
                table: "Jobs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_IsActive",
                table: "Jobs",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PostedDate",
                table: "Jobs",
                column: "PostedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_ApplicationUserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_CompanyId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Job");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_PostedDate",
                table: "Job",
                newName: "IX_Job_PostedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_IsActive",
                table: "Job",
                newName: "IX_Job_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_CompanyId",
                table: "Job",
                newName: "IX_Job_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_ApplicationUserId",
                table: "Job",
                newName: "IX_Job_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Job",
                table: "Job",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_ApplicationUserId",
                table: "Job",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_CompanyId",
                table: "Job",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
