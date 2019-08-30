using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    CountryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "customerContacts",
                columns: table => new
                {
                    CustomerContactId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Designation = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Office = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerContacts", x => x.CustomerContactId);
                    table.ForeignKey(
                        name: "FK_customerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customerAddersses",
                columns: table => new
                {
                    CustomerAddressId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressType = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DefaultAddress = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    CountryId = table.Column<long>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerAddersses", x => x.CustomerAddressId);
                    table.ForeignKey(
                        name: "FK_customerAddersses_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customerAddersses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customerAddersses_CountryId",
                table: "customerAddersses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_customerAddersses_CustomerId",
                table: "customerAddersses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_customerContacts_CustomerId",
                table: "customerContacts",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerAddersses");

            migrationBuilder.DropTable(
                name: "customerContacts");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
