using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Change_Customer_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Currencies_DefaultCurrency",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Customers");

            migrationBuilder.AlterColumn<long>(
                name: "DiscountOption",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "DefaultCurrency",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DiscountOption",
                table: "Customers",
                column: "DiscountOption");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Currencies_DefaultCurrency",
                table: "Customers",
                column: "DefaultCurrency",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_discountTypes_DiscountOption",
                table: "Customers",
                column: "DiscountOption",
                principalTable: "discountTypes",
                principalColumn: "DsicounttTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Currencies_DefaultCurrency",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_discountTypes_DiscountOption",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DiscountOption",
                table: "Customers");

            migrationBuilder.AlterColumn<long>(
                name: "DiscountOption",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DefaultCurrency",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercentage",
                table: "Customers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Currencies_DefaultCurrency",
                table: "Customers",
                column: "DefaultCurrency",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
