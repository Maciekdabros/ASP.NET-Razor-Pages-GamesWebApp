using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesWebApp.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserLike");

            migrationBuilder.AddColumn<string>(
                name: "GiverId",
                table: "Like",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TakerId",
                table: "Like",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_GiverId",
                table: "Like",
                column: "GiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_TakerId",
                table: "Like",
                column: "TakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_GiverId",
                table: "Like",
                column: "GiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_TakerId",
                table: "Like",
                column: "TakerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_GiverId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_TakerId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_GiverId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_TakerId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "GiverId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "TakerId",
                table: "Like");

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
    }
}
