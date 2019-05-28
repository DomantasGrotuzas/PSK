using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.Persistence.Migrations
{
    public partial class FixesForModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TripEmployeeId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TripEmployeeId",
                table: "Accommodations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TripEmployeeId",
                table: "AspNetUsers",
                column: "TripEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_TripEmployeeId",
                table: "Accommodations",
                column: "TripEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_TripEmployees_TripEmployeeId",
                table: "Accommodations",
                column: "TripEmployeeId",
                principalTable: "TripEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TripEmployees_TripEmployeeId",
                table: "AspNetUsers",
                column: "TripEmployeeId",
                principalTable: "TripEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_TripEmployees_TripEmployeeId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TripEmployees_TripEmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TripEmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_TripEmployeeId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "TripEmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TripEmployeeId",
                table: "Accommodations");
        }
    }
}
