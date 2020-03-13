using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class changePurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_PaymentTerms_PaymentTermId",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentTermId",
                table: "PurchaseOrders",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_PaymentTerms_PaymentTermId",
                table: "PurchaseOrders",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_PaymentTerms_PaymentTermId",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentTermId",
                table: "PurchaseOrders",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_PaymentTerms_PaymentTermId",
                table: "PurchaseOrders",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
