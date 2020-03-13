using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class addReceivenotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiveNotes",
                columns: table => new
                {
                    ReceiveNoteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceiveNoteNumber = table.Column<string>(nullable: true),
                    SupplierId = table.Column<long>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveNotes", x => x.ReceiveNoteId);
                    table.ForeignKey(
                        name: "FK_ReceiveNotes_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveNotes_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveNotes_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiveNotes_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveNotesItems",
                columns: table => new
                {
                    ReceiveNoteItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceiveNoteId = table.Column<long>(nullable: true),
                    PurchaseOrdersId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    WarehouseId = table.Column<long>(nullable: false),
                    ProductQTY = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveNotesItems", x => x.ReceiveNoteItemId);
                    table.ForeignKey(
                        name: "FK_ReceiveNotesItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiveNotesItems_PurchaseOrders_PurchaseOrdersId",
                        column: x => x.PurchaseOrdersId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrdersId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveNotesItems_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotes_CreatorUserId",
                table: "ReceiveNotes",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotes_LastModifierUserId",
                table: "ReceiveNotes",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotes_SupplierId",
                table: "ReceiveNotes",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotes_TenantsId",
                table: "ReceiveNotes",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotesItems_ProductId",
                table: "ReceiveNotesItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotesItems_PurchaseOrdersId",
                table: "ReceiveNotesItems",
                column: "PurchaseOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotesItems_WarehouseId",
                table: "ReceiveNotesItems",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiveNotes");

            migrationBuilder.DropTable(
                name: "ReceiveNotesItems");
        }
    }
}
