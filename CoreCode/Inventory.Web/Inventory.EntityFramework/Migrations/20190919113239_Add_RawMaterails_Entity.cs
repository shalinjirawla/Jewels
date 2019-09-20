using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Add_RawMaterails_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DefaultImage",
                table: "UploadImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RawMaterails",
                columns: table => new
                {
                    RMId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RMName = table.Column<string>(nullable: true),
                    AlternativeRMName = table.Column<string>(nullable: true),
                    Itemcode = table.Column<string>(nullable: true),
                    Purchase_Price = table.Column<decimal>(nullable: true),
                    Selling_Price = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StockItem = table.Column<bool>(nullable: false),
                    Taxable = table.Column<bool>(nullable: false),
                    IStockOnHand = table.Column<decimal>(nullable: true),
                    ICostPrice = table.Column<decimal>(nullable: true),
                    ILandedCost = table.Column<decimal>(nullable: true),
                    Reorder_Quantity = table.Column<long>(nullable: true),
                    Minimumu_Order_Quantity = table.Column<long>(nullable: true),
                    UOMId = table.Column<long>(nullable: true),
                    Outer_Weight = table.Column<decimal>(nullable: true),
                    Outer_Weight_metric_Units = table.Column<long>(nullable: true),
                    Inner_Weight = table.Column<decimal>(nullable: true),
                    Inner_Weight_metric_Units = table.Column<long>(nullable: true),
                    OD_Width = table.Column<decimal>(nullable: true),
                    OD_Height = table.Column<decimal>(nullable: true),
                    OD_length = table.Column<decimal>(nullable: true),
                    OD_metric_Units = table.Column<long>(nullable: true),
                    OD_CBM = table.Column<decimal>(nullable: true),
                    ID_Width = table.Column<decimal>(nullable: true),
                    ID_Height = table.Column<decimal>(nullable: true),
                    ID_length = table.Column<decimal>(nullable: true),
                    ID_metric_Units = table.Column<long>(nullable: true),
                    ID_CBM = table.Column<decimal>(nullable: true),
                    ProductCategorieId = table.Column<long>(nullable: true),
                    BrandId = table.Column<long>(nullable: true),
                    WarehouseId = table.Column<long>(nullable: true),
                    TaxCodeId = table.Column<long>(nullable: true),
                    SupplierId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterails", x => x.RMId);
                    table.ForeignKey(
                        name: "FK_RawMaterails_ProductBrands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_Metric_Units_ID_metric_Units",
                        column: x => x.ID_metric_Units,
                        principalTable: "Metric_Units",
                        principalColumn: "Metric_UnitsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_Metric_Units_Inner_Weight_metric_Units",
                        column: x => x.Inner_Weight_metric_Units,
                        principalTable: "Metric_Units",
                        principalColumn: "Metric_UnitsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_Metric_Units_OD_metric_Units",
                        column: x => x.OD_metric_Units,
                        principalTable: "Metric_Units",
                        principalColumn: "Metric_UnitsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_Metric_Units_Outer_Weight_metric_Units",
                        column: x => x.Outer_Weight_metric_Units,
                        principalTable: "Metric_Units",
                        principalColumn: "Metric_UnitsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_ProductCategories_ProductCategorieId",
                        column: x => x.ProductCategorieId,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoriesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_TaxCode_TaxCodeId",
                        column: x => x.TaxCodeId,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_UOMs_UOMId",
                        column: x => x.UOMId,
                        principalTable: "UOMs",
                        principalColumn: "UOMId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawMaterails_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadImages_RawMaterailId",
                table: "UploadImages",
                column: "RawMaterailId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_BrandId",
                table: "RawMaterails",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_CreatorUserId",
                table: "RawMaterails",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_ID_metric_Units",
                table: "RawMaterails",
                column: "ID_metric_Units");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_Inner_Weight_metric_Units",
                table: "RawMaterails",
                column: "Inner_Weight_metric_Units");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_LastModifierUserId",
                table: "RawMaterails",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_OD_metric_Units",
                table: "RawMaterails",
                column: "OD_metric_Units");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_Outer_Weight_metric_Units",
                table: "RawMaterails",
                column: "Outer_Weight_metric_Units");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_ProductCategorieId",
                table: "RawMaterails",
                column: "ProductCategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_SupplierId",
                table: "RawMaterails",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_TaxCodeId",
                table: "RawMaterails",
                column: "TaxCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_TenantsId",
                table: "RawMaterails",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_UOMId",
                table: "RawMaterails",
                column: "UOMId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterails_WarehouseId",
                table: "RawMaterails",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadImages_RawMaterails_RawMaterailId",
                table: "UploadImages",
                column: "RawMaterailId",
                principalTable: "RawMaterails",
                principalColumn: "RMId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadImages_RawMaterails_RawMaterailId",
                table: "UploadImages");

            migrationBuilder.DropTable(
                name: "RawMaterails");

            migrationBuilder.DropIndex(
                name: "IX_UploadImages_RawMaterailId",
                table: "UploadImages");

            migrationBuilder.DropColumn(
                name: "DefaultImage",
                table: "UploadImages");
        }
    }
}
