using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Changes_DiscountTpye_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_discountTypes_AspNetUsers_UserId1",
                table: "discountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_discountTypes_AspNetUsers_UserIdId",
                table: "discountTypes");

            migrationBuilder.DropIndex(
                name: "IX_discountTypes_UserId1",
                table: "discountTypes");

            migrationBuilder.DropIndex(
                name: "IX_discountTypes_UserIdId",
                table: "discountTypes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "discountTypes");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "discountTypes");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_discountTypes_CreatorUserId",
                table: "discountTypes",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_discountTypes_LastModifierUserId",
                table: "discountTypes",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_discountTypes_AspNetUsers_CreatorUserId",
                table: "discountTypes",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_discountTypes_AspNetUsers_LastModifierUserId",
                table: "discountTypes",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_discountTypes_AspNetUsers_CreatorUserId",
                table: "discountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_discountTypes_AspNetUsers_LastModifierUserId",
                table: "discountTypes");

            migrationBuilder.DropIndex(
                name: "IX_discountTypes_CreatorUserId",
                table: "discountTypes");

            migrationBuilder.DropIndex(
                name: "IX_discountTypes_LastModifierUserId",
                table: "discountTypes");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "discountTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "discountTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "discountTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_discountTypes_UserId1",
                table: "discountTypes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_discountTypes_UserIdId",
                table: "discountTypes",
                column: "UserIdId");

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
        }
    }
}
