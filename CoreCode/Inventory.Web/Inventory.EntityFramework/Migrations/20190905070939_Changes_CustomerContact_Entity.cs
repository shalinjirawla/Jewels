using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Changes_CustomerContact_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "customerContacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customerContacts_CountryId",
                table: "customerContacts",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_customerContacts_country_CountryId",
                table: "customerContacts",
                column: "CountryId",
                principalTable: "country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customerContacts_country_CountryId",
                table: "customerContacts");

            migrationBuilder.DropIndex(
                name: "IX_customerContacts_CountryId",
                table: "customerContacts");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "customerContacts");
        }
    }
}
