using Microsoft.EntityFrameworkCore.Migrations;

namespace Race.Repo.Migrations
{
    public partial class UpdatePilotId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Pilots_Id",
                table: "Result");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Result",
                table: "Result");

            migrationBuilder.RenameTable(
                name: "Result",
                newName: "Results");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pilots",
                newName: "PilotId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Results",
                newName: "ResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Pilots_ResultId",
                table: "Results",
                column: "ResultId",
                principalTable: "Pilots",
                principalColumn: "PilotId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Pilots_ResultId",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.RenameTable(
                name: "Results",
                newName: "Result");

            migrationBuilder.RenameColumn(
                name: "PilotId",
                table: "Pilots",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ResultId",
                table: "Result",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Result",
                table: "Result",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Pilots_Id",
                table: "Result",
                column: "Id",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
