using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class changeProductdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PurchasePrice",
                table: "ProductDetail",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReorderQuantity",
                table: "ProductDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SellingPrice",
                table: "ProductDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "ProductDetail");

            migrationBuilder.DropColumn(
                name: "ReorderQuantity",
                table: "ProductDetail");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "ProductDetail");
        }
    }
}
