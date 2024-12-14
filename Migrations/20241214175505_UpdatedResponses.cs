using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginsOfDestiny.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedResponses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Dictionary<string, string>>(
                name: "Responses",
                table: "Dialogs",
                type: "hstore",
                nullable: true,
                oldClrType: typeof(Dictionary<string, string>),
                oldType: "hstore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Dictionary<string, string>>(
                name: "Responses",
                table: "Dialogs",
                type: "hstore",
                nullable: false,
                oldClrType: typeof(Dictionary<string, string>),
                oldType: "hstore",
                oldNullable: true);
        }
    }
}
