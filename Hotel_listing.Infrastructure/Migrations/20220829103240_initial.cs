using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hotel_listing.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Adrsess = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotel_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Albania", "AL" },
                    { 2, "Germany", "DU" },
                    { 3, "Italy", "IT" },
                    { 4, "England", "EN" }
                });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "HotelId", "Adrsess", "CountryId", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "Hotel 1 street", 1, "Hotel 1", 4.7999999999999998 },
                    { 2, "Hotel 2 street", 2, "Hotel 2", 4.7999999999999998 },
                    { 3, "Hotel 3 street", 3, "Hotel 3", 4.7999999999999998 },
                    { 4, "Hotel 4 street", 4, "Hotel 4", 4.7999999999999998 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CountryId",
                table: "Hotel",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
