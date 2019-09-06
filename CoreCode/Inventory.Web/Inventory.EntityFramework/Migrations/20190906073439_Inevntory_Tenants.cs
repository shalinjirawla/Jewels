using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Inevntory_Tenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantName = table.Column<string>(nullable: true),
                    BusinessRegisterNumber = table.Column<string>(nullable: true),
                    TagRegisterNumber = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    IsInTrialPeriod = table.Column<bool>(nullable: true),
                    SubscriptionEndDateUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
