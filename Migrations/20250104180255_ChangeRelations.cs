using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginsOfDestiny.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Player_TelegramId",
                table: "Player");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TelegramId",
                table: "Player",
                column: "TelegramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Player_TelegramId",
                table: "Player");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TelegramId",
                table: "Player",
                column: "TelegramId",
                unique: true);
        }
    }
}
