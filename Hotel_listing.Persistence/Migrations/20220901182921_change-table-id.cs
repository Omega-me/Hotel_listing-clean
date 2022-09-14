using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_listing.Infrastructure.Migrations
{
    public partial class changetableid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "Hotel",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Country",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hotel",
                newName: "HotelId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Country",
                newName: "CountryId");
        }
    }
}
