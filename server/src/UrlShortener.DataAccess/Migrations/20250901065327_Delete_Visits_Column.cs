using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Visits_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitCounter",
                table: "UrlVisits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitCounter",
                table: "UrlVisits",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
