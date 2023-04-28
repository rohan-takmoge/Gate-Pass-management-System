using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gate_Pass_management.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeOfVisitColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeOfVisit",
                table: "LocalODs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfVisit",
                table: "LocalODs");
        }
    }
}
