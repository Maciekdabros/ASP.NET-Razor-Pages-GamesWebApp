using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesWebApp.Migrations
{
    public partial class piec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number_of_likes",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    LikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.LikeID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserLike",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikesLikeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLike", x => new { x.ApplicationUserId, x.LikesLikeID });
                    table.ForeignKey(
                        name: "FK_ApplicationUserLike_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserLike_Like_LikesLikeID",
                        column: x => x.LikesLikeID,
                        principalTable: "Like",
                        principalColumn: "LikeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLike_LikesLikeID",
                table: "ApplicationUserLike",
                column: "LikesLikeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserLike");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.AddColumn<int>(
                name: "Number_of_likes",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
