using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Add_UploadImage_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadImages",
                columns: table => new
                {
                    UploadImageId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UploadImageString = table.Column<string>(nullable: true),
                    RawMaterailId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadImages", x => x.UploadImageId);
                    table.ForeignKey(
                        name: "FK_UploadImages_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadImages_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadImages_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadImages_CreatorUserId",
                table: "UploadImages",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadImages_LastModifierUserId",
                table: "UploadImages",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadImages_TenantsId",
                table: "UploadImages",
                column: "TenantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadImages");
        }
    }
}
