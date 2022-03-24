using Microsoft.EntityFrameworkCore.Migrations;

namespace Slot_Test.Migrations
{
    public partial class SessionProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Combination",
                table: "Session",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Multiplier",
                table: "Session",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combination",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "Multiplier",
                table: "Session");
        }
    }
}
