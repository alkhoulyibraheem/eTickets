using Microsoft.EntityFrameworkCore.Migrations;

namespace eTickets.data.Migrations
{
    public partial class Ra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberRater",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberRater",
                table: "Movies");
        }
    }
}
