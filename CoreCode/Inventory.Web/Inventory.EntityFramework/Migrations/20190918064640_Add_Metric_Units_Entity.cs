using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class Add_Metric_Units_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metric_Units",
                columns: table => new
                {
                    Metric_UnitsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Metric_UnitsName = table.Column<string>(nullable: true),
                    Metric_UnitsType = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metric_Units", x => x.Metric_UnitsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metric_Units");
        }
    }
}
