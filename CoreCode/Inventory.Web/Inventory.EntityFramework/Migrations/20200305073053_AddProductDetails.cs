using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class AddProductDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    ProductDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    MinmOrderQuantity = table.Column<long>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    DefaultSupplierId = table.Column<long>(nullable: true),
                    SupplierId = table.Column<long>(nullable: true),
                    DefaultTaxId = table.Column<int>(nullable: true),
                    DefaultWarehouseId = table.Column<long>(nullable: true),
                    TaxCodeTaxId = table.Column<long>(nullable: true),
                    WarehouseId = table.Column<long>(nullable: true),
                    UnitsOfMeasurement = table.Column<string>(nullable: true),
                    InitialStockHand = table.Column<double>(nullable: true),
                    InitialStockPrice = table.Column<double>(nullable: true),
                    InitialHandCost = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.ProductDetailId);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductDetail_TaxCode_TaxCodeTaxId",
                        column: x => x.TaxCodeTaxId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_ProductId",
                table: "ProductDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_SupplierId",
                table: "ProductDetail",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_TaxCodeTaxId",
                table: "ProductDetail",
                column: "TaxCodeTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_WarehouseId",
                table: "ProductDetail",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetail");
        }
    }
}
