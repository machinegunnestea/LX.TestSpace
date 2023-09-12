using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LX.TestSpace.Server.Migrations
{
    /// <inheritdoc />
    public partial class Addcodesnippet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeSnippet",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeSnippet",
                table: "Questions");
        }
    }
}
