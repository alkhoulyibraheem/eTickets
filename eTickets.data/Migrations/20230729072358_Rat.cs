using Microsoft.EntityFrameworkCore.Migrations;

namespace eTickets.data.Migrations
{
    public partial class Rat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfStars",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfStars",
                table: "Movies");
        }
    }
}
