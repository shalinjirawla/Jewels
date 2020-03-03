using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class add_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CategorieId = table.Column<long>(nullable: true),
                    BrandId = table.Column<long>(nullable: true),
                    BatchItem = table.Column<bool>(nullable: false),
                    Stockitem = table.Column<bool>(nullable: false),
                    Taxable = table.Column<bool>(nullable: false),
                    SerialNumber = table.Column<bool>(nullable: false),
                    IsRawMaterail = table.Column<bool>(nullable: false),
                    RawMaterial_points = table.Column<string>(nullable: true),
                    IsProductVariants = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariant",
                columns: table => new
                {
                    ProductVariantId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(nullable: true),
                    VariantOptionsType = table.Column<string>(nullable: true),
                    VariantslabelId = table.Column<int>(nullable: true),
                    Variantslabel = table.Column<string>(nullable: true),
                    Sku = table.Column<string>(nullable: true),
                    ReorderQuantity = table.Column<long>(nullable: true),
                    PurchasePrice = table.Column<double>(nullable: true),
                    SellingPrice = table.Column<double>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    VariMinmOrderQuantity = table.Column<long>(nullable: true),
                    VariantDesc = table.Column<string>(nullable: true),
                    DefaultSupplierId = table.Column<long>(nullable: true),
                    SupplierId = table.Column<long>(nullable: true),
                    DefaultTaxId = table.Column<int>(nullable: true),
                    DefaultWarehouseId = table.Column<long>(nullable: true),
                    WarehouseId = table.Column<long>(nullable: true),
                    UnitsOfMeasurement = table.Column<double>(nullable: true),
                    InitialStockHand = table.Column<double>(nullable: true),
                    InitialStockPrice = table.Column<double>(nullable: true),
                    InitialHandCost = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant", x => x.ProductVariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatorUserId",
                table: "Product",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_LastModifierUserId",
                table: "Product",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TenantsId",
                table: "Product",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_SupplierId",
                table: "ProductVariant",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_WarehouseId",
                table: "ProductVariant",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVariant");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
