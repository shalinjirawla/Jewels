using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Changes_TaxCodeANDWarehouse_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TaxCode");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "TaxCode");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxCode_CreatorUserId",
                table: "TaxCode",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCode_LastModifierUserId",
                table: "TaxCode",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxCode_AspNetUsers_CreatorUserId",
                table: "TaxCode",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxCode_AspNetUsers_LastModifierUserId",
                table: "TaxCode",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxCode_AspNetUsers_CreatorUserId",
                table: "TaxCode");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxCode_AspNetUsers_LastModifierUserId",
                table: "TaxCode");

            migrationBuilder.DropIndex(
                name: "IX_TaxCode_CreatorUserId",
                table: "TaxCode");

            migrationBuilder.DropIndex(
                name: "IX_TaxCode_LastModifierUserId",
                table: "TaxCode");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "TaxCode",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TaxCode",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "TaxCode",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxCode_UserId1",
                table: "TaxCode",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCode_UserIdId",
                table: "TaxCode",
                column: "UserIdId");

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
    }
}
