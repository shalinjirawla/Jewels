using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class AddSalesOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    SalesOrdersId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrderNumber = table.Column<string>(nullable: true),
                    DateOrdered = table.Column<DateTime>(nullable: true),
                    EstimatedDeliveryDate = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerPurchesOrderNumber = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SalesOrderTypeId = table.Column<long>(nullable: true),
                    CreditTermId = table.Column<long>(nullable: true),
                    ShipmentMethodId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: true),
                    PaymentStatus = table.Column<int>(nullable: false),
                    ShipmentStatus = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.SalesOrdersId);
                    table.ForeignKey(
                        name: "FK_SalesOrders_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_CreditTerms_CreditTermId",
                        column: x => x.CreditTermId,
                        principalTable: "CreditTerms",
                        principalColumn: "CreditTermId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrders_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_SalesOrderType_SalesOrderTypeId",
                        column: x => x.SalesOrderTypeId,
                        principalTable: "SalesOrderType",
                        principalColumn: "SalesOrderTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_ShipmentMethods_ShipmentMethodId",
                        column: x => x.ShipmentMethodId,
                        principalTable: "ShipmentMethods",
                        principalColumn: "ShipmentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderAdditionalChargeForAll",
                columns: table => new
                {
                    AdditionalChargeForProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrdersId = table.Column<long>(nullable: true),
                    AdditionalChargeId = table.Column<long>(nullable: true),
                    TaxId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderAdditionalChargeForAll", x => x.AdditionalChargeForProductId);
                    table.ForeignKey(
                        name: "FK_SalesOrderAdditionalChargeForAll_AdditionalCharge_AdditionalChargeId",
                        column: x => x.AdditionalChargeId,
                        principalTable: "AdditionalCharge",
                        principalColumn: "AdditionalChargeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderAdditionalChargeForAll_SalesOrders_SalesOrdersId",
                        column: x => x.SalesOrdersId,
                        principalTable: "SalesOrders",
                        principalColumn: "SalesOrdersId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderAdditionalChargeForAll_TaxCode_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderAdditionalChargeForProduct",
                columns: table => new
                {
                    AdditionalChargeForProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrdersId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: true),
                    AdditionalChargeId = table.Column<long>(nullable: false),
                    TaxId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderAdditionalChargeForProduct", x => x.AdditionalChargeForProductId);
                    table.ForeignKey(
                        name: "FK_SalesOrderAdditionalChargeForProduct_AdditionalCharge_AdditionalChargeId",
                        column: x => x.AdditionalChargeId,
                        principalTable: "AdditionalCharge",
                        principalColumn: "AdditionalChargeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderAdditionalChargeForProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderAdditionalChargeForProduct_SalesOrders_SalesOrdersId",
                        column: x => x.SalesOrdersId,
                        principalTable: "SalesOrders",
                        principalColumn: "SalesOrdersId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderAdditionalChargeForProduct_TaxCode_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDetails",
                columns: table => new
                {
                    SalesOrderDetailsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrdersId = table.Column<long>(nullable: false),
                    AdditionalChargeType = table.Column<string>(nullable: true),
                    TotalQTY = table.Column<long>(nullable: true),
                    Total = table.Column<double>(nullable: true),
                    FinalTotal = table.Column<double>(nullable: true),
                    TaxInclude = table.Column<bool>(nullable: true),
                    FinalTaxTotal = table.Column<double>(nullable: true),
                    AdditionalChargeAmount = table.Column<double>(nullable: true),
                    IsAdditionalChargeApply = table.Column<bool>(nullable: true),
                    IsAdditionalChargeApplyType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDetails", x => x.SalesOrderDetailsId);
                    table.ForeignKey(
                        name: "FK_SalesOrderDetails_SalesOrders_SalesOrdersId",
                        column: x => x.SalesOrdersId,
                        principalTable: "SalesOrders",
                        principalColumn: "SalesOrdersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderItems",
                columns: table => new
                {
                    OrderItemsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrdersId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: true),
                    Unit = table.Column<int>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: true),
                    QTY = table.Column<int>(nullable: true),
                    DiscountType = table.Column<int>(nullable: true),
                    Discount = table.Column<double>(nullable: true),
                    TaxId = table.Column<long>(nullable: true),
                    IsTaxble = table.Column<bool>(nullable: true),
                    TaxTotal = table.Column<double>(nullable: true),
                    Total = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderItems", x => x.OrderItemsId);
                    table.ForeignKey(
                        name: "FK_SalesOrderItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderItems_SalesOrders_SalesOrdersId",
                        column: x => x.SalesOrdersId,
                        principalTable: "SalesOrders",
                        principalColumn: "SalesOrdersId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderItems_TaxCode_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAdditionalChargeForAll_AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForAll",
                column: "AdditionalChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAdditionalChargeForAll_SalesOrdersId",
                table: "SalesOrderAdditionalChargeForAll",
                column: "SalesOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAdditionalChargeForAll_TaxId",
                table: "SalesOrderAdditionalChargeForAll",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAdditionalChargeForProduct_AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForProduct",
                column: "AdditionalChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAdditionalChargeForProduct_ProductId",
                table: "SalesOrderAdditionalChargeForProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAdditionalChargeForProduct_SalesOrdersId",
                table: "SalesOrderAdditionalChargeForProduct",
                column: "SalesOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAdditionalChargeForProduct_TaxId",
                table: "SalesOrderAdditionalChargeForProduct",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDetails_SalesOrdersId",
                table: "SalesOrderDetails",
                column: "SalesOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItems_ProductId",
                table: "SalesOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItems_SalesOrdersId",
                table: "SalesOrderItems",
                column: "SalesOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItems_TaxId",
                table: "SalesOrderItems",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CreatorUserId",
                table: "SalesOrders",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CreditTermId",
                table: "SalesOrders",
                column: "CreditTermId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CurrencyId",
                table: "SalesOrders",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CustomerId",
                table: "SalesOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_LastModifierUserId",
                table: "SalesOrders",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_SalesOrderTypeId",
                table: "SalesOrders",
                column: "SalesOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_ShipmentMethodId",
                table: "SalesOrders",
                column: "ShipmentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_TenantsId",
                table: "SalesOrders",
                column: "TenantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesOrderAdditionalChargeForAll");

            migrationBuilder.DropTable(
                name: "SalesOrderAdditionalChargeForProduct");

            migrationBuilder.DropTable(
                name: "SalesOrderDetails");

            migrationBuilder.DropTable(
                name: "SalesOrderItems");

            migrationBuilder.DropTable(
                name: "SalesOrders");
        }
    }
}
