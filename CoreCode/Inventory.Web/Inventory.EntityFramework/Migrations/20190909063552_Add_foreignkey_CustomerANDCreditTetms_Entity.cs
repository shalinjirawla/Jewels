using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Add_foreignkey_CustomerANDCreditTetms_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserId1",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserIdId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId1",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserIdId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId1",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserIdId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_UserId1",
                table: "CreditTerms");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_UserIdId",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "CreditTerms");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatorUserId",
                table: "Customers",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LastModifierUserId",
                table: "Customers",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_CreatorUserId",
                table: "CreditTerms",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_LastModifierUserId",
                table: "CreditTerms",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_AspNetUsers_CreatorUserId",
                table: "CreditTerms",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_AspNetUsers_LastModifierUserId",
                table: "CreditTerms",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_CreatorUserId",
                table: "Customers",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_LastModifierUserId",
                table: "Customers",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_CreatorUserId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_LastModifierUserId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_CreatorUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_LastModifierUserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreatorUserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LastModifierUserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_CreatorUserId",
                table: "CreditTerms");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_LastModifierUserId",
                table: "CreditTerms");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "CreditTerms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CreditTerms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "CreditTerms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId1",
                table: "Customers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserIdId",
                table: "Customers",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_UserId1",
                table: "CreditTerms",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_UserIdId",
                table: "CreditTerms",
                column: "UserIdId");

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
        }
    }
}
