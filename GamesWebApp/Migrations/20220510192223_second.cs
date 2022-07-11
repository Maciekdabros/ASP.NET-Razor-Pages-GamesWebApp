using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesWebApp.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_ApplicationUserId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_UserID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Game_ApplicationUserId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ApplicationUserId",
                table: "Message",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_ApplicationUserId",
                table: "Message",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_ApplicationUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ApplicationUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Game",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserID",
                table: "Message",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_ApplicationUserId",
                table: "Game",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AspNetUsers_ApplicationUserId",
                table: "Game",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_UserID",
                table: "Message",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
