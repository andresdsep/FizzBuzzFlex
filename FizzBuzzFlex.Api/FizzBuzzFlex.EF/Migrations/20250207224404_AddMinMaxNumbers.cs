using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzBuzzFlex.EF.Migrations
{
    public partial class AddMinMaxNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumNumber",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumNumber",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumNumber",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MinimumNumber",
                table: "Matches");
        }
    }
}
