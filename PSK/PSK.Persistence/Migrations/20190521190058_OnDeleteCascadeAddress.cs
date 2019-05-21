using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.Persistence.Migrations
{
    public partial class OnDeleteCascadeAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Addresses_AddressId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_AddressId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_AddressId",
                table: "Accommodations");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Offices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Accommodations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_AddressId",
                table: "Offices",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AddressId",
                table: "Accommodations",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Addresses_AddressId",
                table: "Accommodations",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Addresses_AddressId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_AddressId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_AddressId",
                table: "Accommodations");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Offices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Accommodations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Offices_AddressId",
                table: "Offices",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AddressId",
                table: "Accommodations",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Addresses_AddressId",
                table: "Accommodations",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
