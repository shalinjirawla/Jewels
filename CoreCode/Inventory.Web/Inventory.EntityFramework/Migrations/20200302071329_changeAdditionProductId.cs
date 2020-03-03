using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class changeAdditionProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderAdditionalChargeForProduct_AdditionalCharge_AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForProduct");

            migrationBuilder.RenameColumn(
                name: "AdditionalChargeForProductId",
                table: "SalesOrderAdditionalChargeForAll",
                newName: "AdditionalChargeForAllId");

            migrationBuilder.AlterColumn<long>(
                name: "AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForProduct",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderAdditionalChargeForProduct_AdditionalCharge_AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForProduct",
                column: "AdditionalChargeId",
                principalTable: "AdditionalCharge",
                principalColumn: "AdditionalChargeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderAdditionalChargeForProduct_AdditionalCharge_AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForProduct");

            migrationBuilder.RenameColumn(
                name: "AdditionalChargeForAllId",
                table: "SalesOrderAdditionalChargeForAll",
                newName: "AdditionalChargeForProductId");

            migrationBuilder.AlterColumn<long>(
                name: "AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForProduct",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderAdditionalChargeForProduct_AdditionalCharge_AdditionalChargeId",
                table: "SalesOrderAdditionalChargeForProduct",
                column: "AdditionalChargeId",
                principalTable: "AdditionalCharge",
                principalColumn: "AdditionalChargeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
