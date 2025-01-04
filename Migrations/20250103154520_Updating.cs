using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginsOfDestiny.Migrations
{
    /// <inheritdoc />
    public partial class Updating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveDialogId",
                table: "Sessions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "MaxHealth",
                table: "Character",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId",
                table: "Sessions",
                column: "ActiveDialogId",
                principalTable: "Dialogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "MaxHealth",
                table: "Character");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveDialogId",
                table: "Sessions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId",
                table: "Sessions",
                column: "ActiveDialogId",
                principalTable: "Dialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
