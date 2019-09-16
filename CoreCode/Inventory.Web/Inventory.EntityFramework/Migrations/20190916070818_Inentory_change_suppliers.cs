using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Inentory_change_suppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addersses_Supplier_SupplierId",
                table: "Addersses");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_AspNetUsers_CreatorUserId",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Currencies_DefaultCurrency",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_PaymentTerms_DefaultPaymentTerms",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_TaxCode_DefaultTaxCode",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_AspNetUsers_LastModifierUserId",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_ShipmentMethods_Shipmenmethod",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_ShipmentTerms_Shipmenterms",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Tenants_TenantsId",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_TenantsId",
                table: "Suppliers",
                newName: "IX_Suppliers_TenantsId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_Shipmenterms",
                table: "Suppliers",
                newName: "IX_Suppliers_Shipmenterms");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_Shipmenmethod",
                table: "Suppliers",
                newName: "IX_Suppliers_Shipmenmethod");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_LastModifierUserId",
                table: "Suppliers",
                newName: "IX_Suppliers_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_DefaultTaxCode",
                table: "Suppliers",
                newName: "IX_Suppliers_DefaultTaxCode");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_DefaultPaymentTerms",
                table: "Suppliers",
                newName: "IX_Suppliers_DefaultPaymentTerms");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_DefaultCurrency",
                table: "Suppliers",
                newName: "IX_Suppliers_DefaultCurrency");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_CreatorUserId",
                table: "Suppliers",
                newName: "IX_Suppliers_CreatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addersses_Suppliers_SupplierId",
                table: "Addersses",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_CreatorUserId",
                table: "Suppliers",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Currencies_DefaultCurrency",
                table: "Suppliers",
                column: "DefaultCurrency",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_PaymentTerms_DefaultPaymentTerms",
                table: "Suppliers",
                column: "DefaultPaymentTerms",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_TaxCode_DefaultTaxCode",
                table: "Suppliers",
                column: "DefaultTaxCode",
                principalTable: "TaxCode",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_LastModifierUserId",
                table: "Suppliers",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_ShipmentMethods_Shipmenmethod",
                table: "Suppliers",
                column: "Shipmenmethod",
                principalTable: "ShipmentMethods",
                principalColumn: "ShipmentMethodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_ShipmentTerms_Shipmenterms",
                table: "Suppliers",
                column: "Shipmenterms",
                principalTable: "ShipmentTerms",
                principalColumn: "ShipmentTermId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Tenants_TenantsId",
                table: "Suppliers",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addersses_Suppliers_SupplierId",
                table: "Addersses");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_CreatorUserId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Currencies_DefaultCurrency",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_PaymentTerms_DefaultPaymentTerms",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_TaxCode_DefaultTaxCode",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_LastModifierUserId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_ShipmentMethods_Shipmenmethod",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_ShipmentTerms_Shipmenterms",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Tenants_TenantsId",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_TenantsId",
                table: "Supplier",
                newName: "IX_Supplier_TenantsId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_Shipmenterms",
                table: "Supplier",
                newName: "IX_Supplier_Shipmenterms");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_Shipmenmethod",
                table: "Supplier",
                newName: "IX_Supplier_Shipmenmethod");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_LastModifierUserId",
                table: "Supplier",
                newName: "IX_Supplier_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_DefaultTaxCode",
                table: "Supplier",
                newName: "IX_Supplier_DefaultTaxCode");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_DefaultPaymentTerms",
                table: "Supplier",
                newName: "IX_Supplier_DefaultPaymentTerms");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_DefaultCurrency",
                table: "Supplier",
                newName: "IX_Supplier_DefaultCurrency");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_CreatorUserId",
                table: "Supplier",
                newName: "IX_Supplier_CreatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addersses_Supplier_SupplierId",
                table: "Addersses",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_AspNetUsers_CreatorUserId",
                table: "Supplier",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Currencies_DefaultCurrency",
                table: "Supplier",
                column: "DefaultCurrency",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_PaymentTerms_DefaultPaymentTerms",
                table: "Supplier",
                column: "DefaultPaymentTerms",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_TaxCode_DefaultTaxCode",
                table: "Supplier",
                column: "DefaultTaxCode",
                principalTable: "TaxCode",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_AspNetUsers_LastModifierUserId",
                table: "Supplier",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_ShipmentMethods_Shipmenmethod",
                table: "Supplier",
                column: "Shipmenmethod",
                principalTable: "ShipmentMethods",
                principalColumn: "ShipmentMethodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_ShipmentTerms_Shipmenterms",
                table: "Supplier",
                column: "Shipmenterms",
                principalTable: "ShipmentTerms",
                principalColumn: "ShipmentTermId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Tenants_TenantsId",
                table: "Supplier",
                column: "TenantsId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
