using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Race.Repo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    PilotId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.PilotId);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(nullable: false),
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
                        principalColumn: "PilotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pilots",
                columns: new[] { "PilotId", "Code", "Name", "Nationality", "Number" },
                values: new object[,]
                {
                    { new Guid("595a50ec-75a6-434c-98f7-dc28a5ad6c00"), "HAM", "Hamilton", "British", "44" },
                    { new Guid("c274f0f1-9aba-41ef-b271-e671074d8aa2"), "HEI", "Heidfeld", "German", "50" },
                    { new Guid("3dc612c0-5582-46ba-b2fa-6d72493e130d"), "ROS", "Rosberg", "German", "6" },
                    { new Guid("76031f5a-3682-45ac-afac-8f31440cd863"), "ALO", "Alonso", "Spanish", "14" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Pilots");
        }
    }
}
