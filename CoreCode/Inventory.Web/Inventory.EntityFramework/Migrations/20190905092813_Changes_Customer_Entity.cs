using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Changes_Customer_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCreditTerms",
                table: "Customers");

            migrationBuilder.AddColumn<long>(
                name: "CreditTermId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreditTermId",
                table: "Customers",
                column: "CreditTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CreditTerms_CreditTermId",
                table: "Customers",
                column: "CreditTermId",
                principalTable: "CreditTerms",
                principalColumn: "CreditTermId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CreditTerms_CreditTermId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreditTermId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreditTermId",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "DefaultCreditTerms",
                table: "Customers",
                nullable: true);
        }
    }
}
