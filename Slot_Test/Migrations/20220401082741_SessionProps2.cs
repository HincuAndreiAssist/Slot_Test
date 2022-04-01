using Microsoft.EntityFrameworkCore.Migrations;

namespace Slot_Test.Migrations
{
    public partial class SessionProps2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Combination",
                table: "Session",
                newName: "WinningCombination");

            migrationBuilder.AddColumn<string>(
                name: "LowerCombination",
                table: "Session",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpperCombination",
                table: "Session",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowerCombination",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "UpperCombination",
                table: "Session");

            migrationBuilder.RenameColumn(
                name: "WinningCombination",
                table: "Session",
                newName: "Combination");
        }
    }
}
