using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class AddPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrdersId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrderNumber = table.Column<string>(nullable: true),
                    DateOrdered = table.Column<DateTime>(nullable: true),
                    EstimatedDeliveryDate = table.Column<DateTime>(nullable: true),
                    SupplierId = table.Column<long>(nullable: false),
                    ReferenceNumber = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    CreditTermId = table.Column<long>(nullable: true),
                    ShipmentMethodId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: true),
                    PaymentTermId = table.Column<long>(nullable: false),
                    ExchangeRate = table.Column<double>(nullable: true),
                    PaymentStatus = table.Column<int>(nullable: false),
                    ReceiveStatus = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrdersId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_CreditTerms_CreditTermId",
                        column: x => x.CreditTermId,
                        principalTable: "CreditTerms",
                        principalColumn: "CreditTermId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_PaymentTerms_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerms",
                        principalColumn: "PaymentTermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_ShipmentMethods_ShipmentMethodId",
                        column: x => x.ShipmentMethodId,
                        principalTable: "ShipmentMethods",
                        principalColumn: "ShipmentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderAdditionalChargeForAll",
                columns: table => new
                {
                    AdditionalChargeForAllId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrdersId = table.Column<long>(nullable: true),
                    AdditionalChargeId = table.Column<long>(nullable: true),
                    TaxId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderAdditionalChargeForAll", x => x.AdditionalChargeForAllId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAdditionalChargeForAll_AdditionalCharge_AdditionalChargeId",
                        column: x => x.AdditionalChargeId,
                        principalTable: "AdditionalCharge",
                        principalColumn: "AdditionalChargeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAdditionalChargeForAll_PurchaseOrders_PurchaseOrdersId",
                        column: x => x.PurchaseOrdersId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrdersId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAdditionalChargeForAll_TaxCode_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderAdditionalChargeForProduct",
                columns: table => new
                {
                    AdditionalChargeForProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrdersId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: true),
                    IsTaxble = table.Column<bool>(nullable: true),
                    AdditionalChargeId = table.Column<long>(nullable: true),
                    TaxId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderAdditionalChargeForProduct", x => x.AdditionalChargeForProductId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAdditionalChargeForProduct_AdditionalCharge_AdditionalChargeId",
                        column: x => x.AdditionalChargeId,
                        principalTable: "AdditionalCharge",
                        principalColumn: "AdditionalChargeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAdditionalChargeForProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAdditionalChargeForProduct_PurchaseOrders_PurchaseOrdersId",
                        column: x => x.PurchaseOrdersId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrdersId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderAdditionalChargeForProduct_TaxCode_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                columns: table => new
                {
                    PurchaseOrderDetailsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrdersId = table.Column<long>(nullable: true),
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
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.PurchaseOrderDetailsId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrdersId",
                        column: x => x.PurchaseOrdersId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrdersId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItems",
                columns: table => new
                {
                    OrderItemsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrdersId = table.Column<long>(nullable: true),
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
                    table.PrimaryKey("PK_PurchaseOrderItems", x => x.OrderItemsId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrdersId",
                        column: x => x.PurchaseOrdersId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrdersId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_TaxCode_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAdditionalChargeForAll_AdditionalChargeId",
                table: "PurchaseOrderAdditionalChargeForAll",
                column: "AdditionalChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAdditionalChargeForAll_PurchaseOrdersId",
                table: "PurchaseOrderAdditionalChargeForAll",
                column: "PurchaseOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAdditionalChargeForAll_TaxId",
                table: "PurchaseOrderAdditionalChargeForAll",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAdditionalChargeForProduct_AdditionalChargeId",
                table: "PurchaseOrderAdditionalChargeForProduct",
                column: "AdditionalChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAdditionalChargeForProduct_ProductId",
                table: "PurchaseOrderAdditionalChargeForProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAdditionalChargeForProduct_PurchaseOrdersId",
                table: "PurchaseOrderAdditionalChargeForProduct",
                column: "PurchaseOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderAdditionalChargeForProduct_TaxId",
                table: "PurchaseOrderAdditionalChargeForProduct",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrdersId",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_ProductId",
                table: "PurchaseOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrdersId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_TaxId",
                table: "PurchaseOrderItems",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CreatorUserId",
                table: "PurchaseOrders",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CreditTermId",
                table: "PurchaseOrders",
                column: "CreditTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_LastModifierUserId",
                table: "PurchaseOrders",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PaymentTermId",
                table: "PurchaseOrders",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ShipmentMethodId",
                table: "PurchaseOrders",
                column: "ShipmentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_TenantsId",
                table: "PurchaseOrders",
                column: "TenantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderAdditionalChargeForAll");

            migrationBuilder.DropTable(
                name: "PurchaseOrderAdditionalChargeForProduct");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");
        }
    }
}
