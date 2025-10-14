using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagementService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRewversionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rowversion",
                table: "Teams");

            migrationBuilder.AddColumn<byte[]>(
                name: "Rowversion",
                table: "Teams",
                type: "rowversion",
                rowVersion: true,
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rowversion",
                table: "Teams");

            migrationBuilder.AddColumn<byte[]>(
                name: "Rowversion",
                table: "Teams",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
