using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Add_TenantIdInEveryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "Warehouses",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "TaxCode",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "ProductBrands",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "discountTypes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "CustomerTypes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "Currencies",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "CreditTerms",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantsId",
                table: "country",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_TenantsId",
                table: "Warehouses",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCode_TenantsId",
                table: "TaxCode",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_TenantsId",
                table: "ProductCategories",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_TenantsId",
                table: "ProductBrands",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_discountTypes_TenantsId",
                table: "discountTypes",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_TenantsId",
                table: "CustomerTypes",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TenantsId",
                table: "Customers",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_TenantsId",
                table: "Currencies",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_TenantsId",
                table: "CreditTerms",
                column: "TenantsId");

            migrationBuilder.CreateIndex(
                name: "IX_country_TenantsId",
                table: "country",
                column: "TenantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_country_Tenants_TenantsId",
                table: "country",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_Tenants_TenantsId",
                table: "CreditTerms",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Tenants_TenantsId",
                table: "Currencies",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Tenants_TenantsId",
                table: "Customers",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_Tenants_TenantsId",
                table: "CustomerTypes",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_discountTypes_Tenants_TenantsId",
                table: "discountTypes",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBrands_Tenants_TenantsId",
                table: "ProductBrands",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Tenants_TenantsId",
                table: "ProductCategories",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxCode_Tenants_TenantsId",
                table: "TaxCode",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Tenants_TenantsId",
                table: "Warehouses",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_country_Tenants_TenantsId",
                table: "country");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_Tenants_TenantsId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Tenants_TenantsId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Tenants_TenantsId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_Tenants_TenantsId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_discountTypes_Tenants_TenantsId",
                table: "discountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBrands_Tenants_TenantsId",
                table: "ProductBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Tenants_TenantsId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxCode_Tenants_TenantsId",
                table: "TaxCode");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Tenants_TenantsId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_TenantsId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_TaxCode_TenantsId",
                table: "TaxCode");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_TenantsId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductBrands_TenantsId",
                table: "ProductBrands");

            migrationBuilder.DropIndex(
                name: "IX_discountTypes_TenantsId",
                table: "discountTypes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTypes_TenantsId",
                table: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Customers_TenantsId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_TenantsId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_TenantsId",
                table: "CreditTerms");

            migrationBuilder.DropIndex(
                name: "IX_country_TenantsId",
                table: "country");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "TaxCode");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "ProductBrands");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "discountTypes");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "CustomerTypes");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "country");
        }
    }
}
