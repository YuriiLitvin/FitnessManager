using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_HomeWork_4_CORE.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 255, nullable: false),
                    TypeOfTraining = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    TrainingPeolpeCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfTraining = table.Column<int>(nullable: false),
                    CoachId = table.Column<int>(nullable: false),
                    GymId = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workouts_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "Email", "FullName", "MobileNumber", "TypeOfTraining" },
                values: new object[,]
                {
                    { 1, "petrovich@gmail.com", "Petrovich", "09923", 1 },
                    { 2, "samson@gmail.com", "Samson", "097325", 2 },
                    { 3, "sashkapower@gmail.com", "Oleksandr I", "09544234", 3 },
                    { 4, "g_anna@gmail.com", "Anna G.", "0930954", 0 }
                });

            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "Id", "Title", "TrainingPeolpeCount" },
                values: new object[,]
                {
                    { 1, "Sparta", 100 },
                    { 2, "FitnessLife", 150 }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "CoachId", "FinishTime", "GymId", "StartTime", "TypeOfTraining" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2020, 8, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 8, 27, 9, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, 3, new DateTime(2020, 8, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 8, 27, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, 2, new DateTime(2020, 8, 27, 11, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 8, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, 1, new DateTime(2020, 8, 27, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 8, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_CoachId",
                table: "Workouts",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_GymId",
                table: "Workouts",
                column: "GymId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Gyms");
        }
    }
}
