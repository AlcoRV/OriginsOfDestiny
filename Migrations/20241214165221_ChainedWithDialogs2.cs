using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginsOfDestiny.Migrations
{
    /// <inheritdoc />
    public partial class ChainedWithDialogs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId1",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ActiveDialogId1",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ActiveDialogId1",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveDialogId",
                table: "Sessions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ActiveDialogId",
                table: "Sessions",
                column: "ActiveDialogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId",
                table: "Sessions",
                column: "ActiveDialogId",
                principalTable: "Dialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ActiveDialogId",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ActiveDialogId",
                table: "Sessions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ActiveDialogId1",
                table: "Sessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ActiveDialogId1",
                table: "Sessions",
                column: "ActiveDialogId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Dialogs_ActiveDialogId1",
                table: "Sessions",
                column: "ActiveDialogId1",
                principalTable: "Dialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
