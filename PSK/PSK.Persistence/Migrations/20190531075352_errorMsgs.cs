using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.Persistence.Migrations
{
    public partial class errorMsgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Offices_Name",
                table: "Offices",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offices_Name",
                table: "Offices");
        }
    }
}
