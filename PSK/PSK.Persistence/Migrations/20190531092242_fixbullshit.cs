using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.Persistence.Migrations
{
    public partial class fixbullshit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_TripEmployees_TripEmployeeId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_TripEmployees_TripEmployeeId",
                table: "Files",
                column: "TripEmployeeId",
                principalTable: "TripEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_TripEmployees_TripEmployeeId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_TripEmployees_TripEmployeeId",
                table: "Files",
                column: "TripEmployeeId",
                principalTable: "TripEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
