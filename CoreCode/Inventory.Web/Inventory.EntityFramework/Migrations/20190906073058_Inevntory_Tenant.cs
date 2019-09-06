using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Inevntory_Tenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUsers");
        }
    }
}
