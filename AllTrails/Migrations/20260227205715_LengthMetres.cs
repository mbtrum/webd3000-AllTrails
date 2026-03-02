using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllTrails.Migrations
{
    /// <inheritdoc />
    public partial class LengthMetres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LengthMetres",
                table: "Trail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthMetres",
                table: "Trail");
        }
    }
}
