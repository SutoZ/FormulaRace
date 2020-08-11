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
                    { new Guid("595a50ec-75a6-434c-98f7-dc28a5ad6c00"), "HAM", "Lewis Hamilton", "British", "44" },
                    { new Guid("8c4750cb-a810-4344-a2ce-ed82a02a6cc1"), "FIS", "Giancarlo Fisichella", "Italian", "31" },
                    { new Guid("c36e5c5a-355b-4f65-b6f3-814bba005eb9"), "VET", "Sebastian Vettel", "German", "30" },
                    { new Guid("5e76aee7-0b54-476b-9285-91eb98fe41a1"), "DAV", "Anthony Davidson", "British", "29" },
                    { new Guid("649c17e2-1ff7-4ad4-853f-103d7a24d937"), "BUT", "Jenson Button", "British", "28" },
                    { new Guid("76031f5a-3682-45ac-afac-8f31440cd863"), "ALO", "Alonso", "Spanish", "27" },
                    { new Guid("30e3c76b-106a-42af-880a-0e7a4b9d18e8"), "WEB", "Mark Webber", "Australian", "26" },
                    { new Guid("8e1b2be1-4a7c-4ec2-9072-a0ae97642605"), "SUT", "Adrian Sutil", "German", "25" },
                    { new Guid("265f95bd-189e-4286-9990-f6d212cb2bd5"), "BAR", "Rubens Barichello", "Brazilian", "32" },
                    { new Guid("3f179e30-f470-4253-a90c-fdc5cef4f76e"), "TRU", "Jarno Trulli", "Italian", "24" },
                    { new Guid("aeb5a563-9b9f-44db-a56e-d7404ade9be3"), "MAS", "Felipe Massa", "Brazilian", "22" },
                    { new Guid("a9e5ba71-62f9-49d6-977a-647bdd3b7037"), "SAT", "Tacuma Sato", "Japanese", "21" },
                    { new Guid("6b63d3a4-8a19-41e1-b895-60ea9d501c0a"), "GLO", "Timo Glock", "German", "20" },
                    { new Guid("f91f48d4-51ea-4f44-95ec-182b8e7b029c"), "KUB", "Robert Kubica", "Polish", "19" },
                    { new Guid("af592a6b-20f2-45eb-b942-b41b899dd887"), "RAI", "Kimi Raikonnen", "Finnish", "14" },
                    { new Guid("3dc612c0-5582-46ba-b2fa-6d72493e130d"), "ROS", "Nico Rosberg", "German", "6" },
                    { new Guid("c274f0f1-9aba-41ef-b271-e671074d8aa2"), "HEI", "Nick Heidfeld", "German", "50" },
                    { new Guid("918ad535-b482-40af-acb5-0219418b51fe"), "COU", "David Coulthard", "British", "23" },
                    { new Guid("3ab659d6-8df9-40c6-abac-d5e26b1e3f56"), "SCH", "Michael Schumacher", "German", "33" }
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
