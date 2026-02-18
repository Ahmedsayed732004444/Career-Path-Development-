using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Career_Path.Migrations
{
    /// <inheritdoc />
    public partial class addRoadmapmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roadmaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentDomain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    TransitionDifficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsValidTransition = table.Column<bool>(type: "bit", nullable: false),
                    ValidationMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MermaidDiagram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roadmaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roadmaps_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImprovement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoadmapId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImprovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImprovement_Roadmaps_RoadmapId",
                        column: x => x.RoadmapId,
                        principalTable: "Roadmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoadmapPhase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoadmapId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    FocusArea = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadmapPhase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoadmapPhase_Roadmaps_RoadmapId",
                        column: x => x.RoadmapId,
                        principalTable: "Roadmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhaseResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoadmapPhaseId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhaseResource_RoadmapPhase_RoadmapPhaseId",
                        column: x => x.RoadmapPhaseId,
                        principalTable: "RoadmapPhase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhaseSkill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoadmapPhaseId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhaseSkill_RoadmapPhase_RoadmapPhaseId",
                        column: x => x.RoadmapPhaseId,
                        principalTable: "RoadmapPhase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhaseResource_RoadmapPhaseId",
                table: "PhaseResource",
                column: "RoadmapPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseSkill_RoadmapPhaseId",
                table: "PhaseSkill",
                column: "RoadmapPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImprovement_RoadmapId",
                table: "ProjectImprovement",
                column: "RoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadmapPhase_RoadmapId",
                table: "RoadmapPhase",
                column: "RoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_Roadmaps_ApplicationUserId",
                table: "Roadmaps",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhaseResource");

            migrationBuilder.DropTable(
                name: "PhaseSkill");

            migrationBuilder.DropTable(
                name: "ProjectImprovement");

            migrationBuilder.DropTable(
                name: "RoadmapPhase");

            migrationBuilder.DropTable(
                name: "Roadmaps");
        }
    }
}
