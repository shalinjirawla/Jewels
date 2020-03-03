using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class add_product_service_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductService_TaxCode_TaxCodeTaxId",
                table: "ProductService");

            migrationBuilder.DropIndex(
                name: "IX_ProductService_TaxCodeTaxId",
                table: "ProductService");

            migrationBuilder.DropColumn(
                name: "TaxCodeTaxId",
                table: "ProductService");

            migrationBuilder.AlterColumn<long>(
                name: "TaxId",
                table: "ProductService",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductService_TaxId",
                table: "ProductService",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_TaxCode_TaxId",
                table: "ProductService",
                column: "TaxId",
                principalTable: "TaxCode",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductService_TaxCode_TaxId",
                table: "ProductService");

            migrationBuilder.DropIndex(
                name: "IX_ProductService_TaxId",
                table: "ProductService");

            migrationBuilder.AlterColumn<string>(
                name: "TaxId",
                table: "ProductService",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "TaxCodeTaxId",
                table: "ProductService",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductService_TaxCodeTaxId",
                table: "ProductService",
                column: "TaxCodeTaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_TaxCode_TaxCodeTaxId",
                table: "ProductService",
                column: "TaxCodeTaxId",
                principalTable: "TaxCode",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
