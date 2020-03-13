using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.EntityFrameworkCore.Migrations
{
    public partial class changereceiveNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReceiveNotesItems_ReceiveNoteId",
                table: "ReceiveNotesItems",
                column: "ReceiveNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveNotesItems_ReceiveNotes_ReceiveNoteId",
                table: "ReceiveNotesItems",
                column: "ReceiveNoteId",
                principalTable: "ReceiveNotes",
                principalColumn: "ReceiveNoteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveNotesItems_ReceiveNotes_ReceiveNoteId",
                table: "ReceiveNotesItems");

            migrationBuilder.DropIndex(
                name: "IX_ReceiveNotesItems_ReceiveNoteId",
                table: "ReceiveNotesItems");
        }
    }
}
