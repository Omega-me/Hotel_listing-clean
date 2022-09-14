using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_listing.Infrastructure.Migrations
{
    public partial class fixAddressCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adrsess",
                table: "Hotel",
                newName: "Addrsess");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Addrsess",
                table: "Hotel",
                newName: "Adrsess");
        }
    }
}
