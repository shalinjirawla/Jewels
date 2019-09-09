using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class CuurencyNCustomertypeNProBrandNProCategory_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AspNetUsers_UserId1",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AspNetUsers_UserIdId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_UserId1",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_UserIdId",
                table: "CustomerTypes");

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
                name: "IX_CustomerTypes_UserId1",
                table: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTypes_UserIdId",
                table: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UserId1",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UserIdId",
                table: "Currencies");

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
                table: "CustomerTypes");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "CustomerTypes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Currencies");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "ProductBrands",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "ProductBrands",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CreatorUserId",
                table: "ProductCategories",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_LastModifierUserId",
                table: "ProductCategories",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_CreatorUserId",
                table: "ProductBrands",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_LastModifierUserId",
                table: "ProductBrands",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_CreatorUserId",
                table: "CustomerTypes",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_LastModifierUserId",
                table: "CustomerTypes",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CreatorUserId",
                table: "Currencies",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_LastModifierUserId",
                table: "Currencies",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_AspNetUsers_CreatorUserId",
                table: "Currencies",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_AspNetUsers_LastModifierUserId",
                table: "Currencies",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_CreatorUserId",
                table: "CustomerTypes",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_LastModifierUserId",
                table: "CustomerTypes",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBrands_AspNetUsers_CreatorUserId",
                table: "ProductBrands",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBrands_AspNetUsers_LastModifierUserId",
                table: "ProductBrands",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatorUserId",
                table: "ProductCategories",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_LastModifierUserId",
                table: "ProductCategories",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AspNetUsers_CreatorUserId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AspNetUsers_LastModifierUserId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_CreatorUserId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_LastModifierUserId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBrands_AspNetUsers_CreatorUserId",
                table: "ProductBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBrands_AspNetUsers_LastModifierUserId",
                table: "ProductBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatorUserId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_LastModifierUserId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CreatorUserId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_LastModifierUserId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductBrands_CreatorUserId",
                table: "ProductBrands");

            migrationBuilder.DropIndex(
                name: "IX_ProductBrands_LastModifierUserId",
                table: "ProductBrands");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTypes_CreatorUserId",
                table: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTypes_LastModifierUserId",
                table: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_CreatorUserId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_LastModifierUserId",
                table: "Currencies");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(string),
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
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "ProductBrands",
                nullable: true,
                oldClrType: typeof(string),
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
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "CustomerTypes",
                nullable: true,
                oldClrType: typeof(string),
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
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Currencies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Currencies",
                nullable: true);

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
                name: "IX_CustomerTypes_UserId1",
                table: "CustomerTypes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_UserIdId",
                table: "CustomerTypes",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UserId1",
                table: "Currencies",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UserIdId",
                table: "Currencies",
                column: "UserIdId");

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
        }
    }
}
