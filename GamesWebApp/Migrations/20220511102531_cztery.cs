using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesWebApp.Migrations
{
    public partial class cztery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number_of_likes",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number_of_likes",
                table: "AspNetUsers");
        }
    }
}
