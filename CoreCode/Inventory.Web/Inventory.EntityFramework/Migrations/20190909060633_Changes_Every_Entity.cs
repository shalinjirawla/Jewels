using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Changes_Every_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TaxCode",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "TaxCode",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "ProductBrands",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "ProductBrands",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ProductBrands",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "ProductBrands",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "discountTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "discountTypes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CustomerTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "CustomerTypes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Currencies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Currencies",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CreditTerms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "CreditTerms",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "country",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "country",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "country",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "country",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "country",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxCode_UserId1",
                table: "TaxCode",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCode_UserIdId",
                table: "TaxCode",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_UserId1",
                table: "ProductCategories",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_UserIdId",
                table: "ProductCategories",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_UserId1",
                table: "ProductBrands",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_UserIdId",
                table: "ProductBrands",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_discountTypes_UserId1",
                table: "discountTypes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_discountTypes_UserIdId",
                table: "discountTypes",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_UserId1",
                table: "CustomerTypes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_UserIdId",
                table: "CustomerTypes",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId1",
                table: "Customers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserIdId",
                table: "Customers",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UserId1",
                table: "Currencies",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UserIdId",
                table: "Currencies",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_UserId1",
                table: "CreditTerms",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_UserIdId",
                table: "CreditTerms",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_country_CreatorUserId",
                table: "country",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_country_LastModifierUserId",
                table: "country",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_country_AspNetUsers_CreatorUserId",
                table: "country",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_country_AspNetUsers_LastModifierUserId",
                table: "country",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserId1",
                table: "CreditTerms",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserIdId",
                table: "CreditTerms",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_AspNetUsers_UserId1",
                table: "Currencies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_AspNetUsers_UserIdId",
                table: "Currencies",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId1",
                table: "Customers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserIdId",
                table: "Customers",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_UserId1",
                table: "CustomerTypes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_UserIdId",
                table: "CustomerTypes",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_discountTypes_AspNetUsers_UserId1",
                table: "discountTypes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_discountTypes_AspNetUsers_UserIdId",
                table: "discountTypes",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBrands_AspNetUsers_UserId1",
                table: "ProductBrands",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBrands_AspNetUsers_UserIdId",
                table: "ProductBrands",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_UserId1",
                table: "ProductCategories",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_UserIdId",
                table: "ProductCategories",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxCode_AspNetUsers_UserId1",
                table: "TaxCode",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxCode_AspNetUsers_UserIdId",
                table: "TaxCode",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_country_AspNetUsers_CreatorUserId",
                table: "country");

            migrationBuilder.DropForeignKey(
                name: "FK_country_AspNetUsers_LastModifierUserId",
                table: "country");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserId1",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserIdId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AspNetUsers_UserId1",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AspNetUsers_UserIdId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId1",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserIdId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_UserId1",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_UserIdId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_discountTypes_AspNetUsers_UserId1",
                table: "discountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_discountTypes_AspNetUsers_UserIdId",
                table: "discountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBrands_AspNetUsers_UserId1",
                table: "ProductBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBrands_AspNetUsers_UserIdId",
                table: "ProductBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_UserId1",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_UserIdId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxCode_AspNetUsers_UserId1",
                table: "TaxCode");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxCode_AspNetUsers_UserIdId",
                table: "TaxCode");

            migrationBuilder.DropIndex(
                name: "IX_TaxCode_UserId1",
                table: "TaxCode");

            migrationBuilder.DropIndex(
                name: "IX_TaxCode_UserIdId",
                table: "TaxCode");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_UserId1",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_UserIdId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductBrands_UserId1",
                table: "ProductBrands");

            migrationBuilder.DropIndex(
                name: "IX_ProductBrands_UserIdId",
                table: "ProductBrands");

            migrationBuilder.DropIndex(
                name: "IX_discountTypes_UserId1",
                table: "discountTypes");

            migrationBuilder.DropIndex(
                name: "IX_discountTypes_UserIdId",
                table: "discountTypes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTypes_UserId1",
                table: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTypes_UserIdId",
                table: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId1",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserIdId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UserId1",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UserIdId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_UserId1",
                table: "CreditTerms");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_UserIdId",
                table: "CreditTerms");

            migrationBuilder.DropIndex(
                name: "IX_country_CreatorUserId",
                table: "country");

            migrationBuilder.DropIndex(
                name: "IX_country_LastModifierUserId",
                table: "country");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TaxCode");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "TaxCode");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ProductBrands");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "ProductBrands");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "discountTypes");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "discountTypes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CustomerTypes");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "CustomerTypes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "country");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "country");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "country");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "country");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "country");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "ProductBrands",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "ProductBrands",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifierUserId",
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorUserId",
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
