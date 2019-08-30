using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Update_Customer_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultShipmentMethod",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DefaultShipmentTerms",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PriceList",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultShipmentMethod",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultShipmentTerms",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceList",
                table: "Customers",
                nullable: true);
        }
    }
}
