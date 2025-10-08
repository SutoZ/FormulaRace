using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagementService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRowversionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Rowversion",
                table: "Teams",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Rowversion",
                table: "Pilots",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rowversion",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Rowversion",
                table: "Pilots");
        }
    }
}
