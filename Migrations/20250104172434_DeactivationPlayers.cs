using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginsOfDestiny.Migrations
{
    /// <inheritdoc />
    public partial class DeactivationPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "Player",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Player",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Player");
        }
    }
}
