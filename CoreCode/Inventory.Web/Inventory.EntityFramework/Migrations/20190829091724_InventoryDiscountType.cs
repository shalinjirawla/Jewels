﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class InventoryDiscountType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discountTypes",
                columns: table => new
                {
                    DsicounttTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiscountName = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discountTypes", x => x.DsicounttTypeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discountTypes");
        }
    }
}
