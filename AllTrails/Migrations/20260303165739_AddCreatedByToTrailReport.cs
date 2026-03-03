using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllTrails.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByToTrailReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TripReport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TripReport");
        }
    }
}
