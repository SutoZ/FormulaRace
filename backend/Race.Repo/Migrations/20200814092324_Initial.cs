﻿using System;
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
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                    PilotId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Pilots_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "ChampionShipPoints", "DateOfFoundation", "Name", "OwnerName" },
                values: new object[] { 1, 20, new DateTime(1950, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ferrari", "Teszt Random" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "ChampionShipPoints", "DateOfFoundation", "Name", "OwnerName" },
                values: new object[] { 2, 30, new DateTime(1970, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mercedes", "Teszt Random 23" });

            migrationBuilder.InsertData(
                table: "Pilots",
                columns: new[] { "Id", "Code", "Name", "Nationality", "Number", "TeamId" },
                values: new object[,]
                {
                    { 1, "HAM", "Lewis Hamilton", "British", "44", 1 },
                    { 2, "HEI", "Nick Heidfeld", "German", "50", 1 },
                    { 3, "ROS", "Nico Rosberg", "German", "6", 1 },
                    { 4, "RAI", "Kimi Raikonnen", "Finnish", "14", 1 },
                    { 5, "KUB", "Robert Kubica", "Polish", "19", 1 },
                    { 6, "GLO", "Timo Glock", "German", "20", 2 },
                    { 7, "SAT", "Tacuma Sato", "Japanese", "21", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_TeamId",
                table: "Pilots",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}