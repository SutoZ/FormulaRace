using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Race.Repo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    DateOfFoundation = table.Column<DateTime>(nullable: false),
                    OwnerName = table.Column<string>(nullable: false),
                    ChampionShipPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pilots_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    ResultId = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                    PilotId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Result_Pilots_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "ChampionShipPoints", "DateOfFoundation", "Name", "OwnerName" },
                values: new object[,]
                {
                    { 1, 20, new DateTime(1950, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ferrari", "Mattia Binotto" },
                    { 2, 30, new DateTime(1970, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mercedes", "Toto Wolff" },
                    { 3, 80, new DateTime(1960, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "McLaren", "Andreas Seidl" },
                    { 4, 20, new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reanult", "Cyril Abiteboul" },
                    { 5, 5, new DateTime(1990, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Williams Racing", "Frank Williams" },
                    { 6, 3, new DateTime(1990, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alpha Tauri", "Franz Tost" }
                });

            migrationBuilder.InsertData(
                table: "Pilots",
                columns: new[] { "Id", "Code", "Name", "Nationality", "Number", "TeamId" },
                values: new object[,]
                {
                    { 3, "ROS", "Nico Rosberg", "German", "6", 1 },
                    { 12, "VET", "Sebastian Vettel", "German", "30", 1 },
                    { 13, "SCH", "Michael Schumacher", "German", "33", 1 },
                    { 1, "HAM", "Lewis Hamilton", "British", "44", 2 },
                    { 4, "RAI", "Kimi Raikonnen", "Finnish", "14", 2 },
                    { 24, "BOT", "Walteri Bottas", "Finnish", "70", 2 },
                    { 7, "NOR", "Lando Norris", "Italian", "24", 3 },
                    { 8, "SAI", "Carlos Sainz", "Spanish", "25", 3 },
                    { 2, "OCO", "Esteban Occon", "France", "50", 4 },
                    { 9, "RIC", "Daniel Ricciardo", "Australian", "32", 4 },
                    { 5, "RUS", "Gerogre Russel", "English", "19", 5 },
                    { 6, "LAT", "Nicholas Latifi", "Canadian", "20", 5 },
                    { 10, "GAS", "Pierre Gasly", "French", "29", 6 },
                    { 11, "KVY", "Daniil Kvyat", "Russian", "31", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_TeamId",
                table: "Pilots",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
