using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_listing.Infrastructure.Migrations
{
    public partial class boolColAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEuropeCountry",
                table: "Country",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEuropeCountry",
                table: "Country");
        }
    }
}
