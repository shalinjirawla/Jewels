using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Inventory_Suppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerAddersses");

            migrationBuilder.DropTable(
                name: "customerContacts");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Designation = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    DefaultContact = table.Column<bool>(nullable: false),
                    CountryId = table.Column<long>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Office = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contacts_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    SupplierCode = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    DefaultCurrency = table.Column<long>(nullable: true),
                    DefaultPaymentTerms = table.Column<long>(nullable: true),
                    DefaultTaxCode = table.Column<long>(nullable: true),
                    Shipmenterms = table.Column<long>(nullable: true),
                    Shipmenmethod = table.Column<long>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Supplier_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_Currencies_DefaultCurrency",
                        column: x => x.DefaultCurrency,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_PaymentTerms_DefaultPaymentTerms",
                        column: x => x.DefaultPaymentTerms,
                        principalTable: "PaymentTerms",
                        principalColumn: "PaymentTermId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_TaxCode_DefaultTaxCode",
                        column: x => x.DefaultTaxCode,
                        principalTable: "TaxCode",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_ShipmentMethods_Shipmenmethod",
                        column: x => x.Shipmenmethod,
                        principalTable: "ShipmentMethods",
                        principalColumn: "ShipmentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_ShipmentTerms_Shipmenterms",
                        column: x => x.Shipmenterms,
                        principalTable: "ShipmentTerms",
                        principalColumn: "ShipmentTermId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addersses",
                columns: table => new
                {
                    AddressId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressType = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DefaultAddress = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    SupplierId = table.Column<long>(nullable: true),
                    CountryId = table.Column<long>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    TenantId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addersses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addersses_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addersses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addersses_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addersses_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addersses_CountryId",
                table: "Addersses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addersses_CustomerId",
                table: "Addersses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Addersses_SupplierId",
                table: "Addersses",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Addersses_TenantId",
                table: "Addersses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CountryId",
                table: "Contacts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerId",
                table: "Contacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CreatorUserId",
                table: "Supplier",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_DefaultCurrency",
                table: "Supplier",
                column: "DefaultCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_DefaultPaymentTerms",
                table: "Supplier",
                column: "DefaultPaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_DefaultTaxCode",
                table: "Supplier",
                column: "DefaultTaxCode");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_LastModifierUserId",
                table: "Supplier",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Shipmenmethod",
                table: "Supplier",
                column: "Shipmenmethod");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Shipmenterms",
                table: "Supplier",
                column: "Shipmenterms");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_TenantsId",
                table: "Supplier",
                column: "TenantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addersses");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.CreateTable(
                name: "customerAddersses",
                columns: table => new
                {
                    CustomerAddressId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    AddressType = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true),
                    DefaultAddress = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerAddersses", x => x.CustomerAddressId);
                    table.ForeignKey(
                        name: "FK_customerAddersses_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customerAddersses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customerContacts",
                columns: table => new
                {
                    CustomerContactId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    DefaultContact = table.Column<bool>(nullable: false),
                    Designation = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Office = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerContacts", x => x.CustomerContactId);
                    table.ForeignKey(
                        name: "FK_customerContacts_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customerAddersses_CountryId",
                table: "customerAddersses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_customerAddersses_CustomerId",
                table: "customerAddersses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_customerContacts_CountryId",
                table: "customerContacts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_customerContacts_CustomerId",
                table: "customerContacts",
                column: "CustomerId");
        }
    }
}
