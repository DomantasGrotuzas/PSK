using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.Persistence.Migrations
{
    public partial class ChangedContraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Offices_OfficeId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEmployees_AspNetUsers_EmployeeId",
                table: "TripEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEmployees_Trips_TripId",
                table: "TripEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Offices_EndLocationId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AspNetUsers_OrganizerId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Offices_StartLocationId",
                table: "Trips");

            migrationBuilder.AlterColumn<Guid>(
                name: "StartLocationId",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizerId",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EndLocationId",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Trips",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TripId",
                table: "TripEmployees",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "TripEmployees",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "TripEmployees",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offices",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Addresses",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Addresses",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalSpaces",
                table: "Accommodations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficeId",
                table: "Accommodations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accommodations",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Offices_OfficeId",
                table: "Accommodations",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripEmployees_AspNetUsers_EmployeeId",
                table: "TripEmployees",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripEmployees_Trips_TripId",
                table: "TripEmployees",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Offices_EndLocationId",
                table: "Trips",
                column: "EndLocationId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_OrganizerId",
                table: "Trips",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Offices_StartLocationId",
                table: "Trips",
                column: "StartLocationId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Offices_OfficeId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEmployees_AspNetUsers_EmployeeId",
                table: "TripEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_TripEmployees_Trips_TripId",
                table: "TripEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Offices_EndLocationId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AspNetUsers_OrganizerId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Offices_StartLocationId",
                table: "Trips");

            migrationBuilder.AlterColumn<Guid>(
                name: "StartLocationId",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizerId",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "EndLocationId",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TripId",
                table: "TripEmployees",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "TripEmployees",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "TripEmployees",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offices",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<int>(
                name: "TotalSpaces",
                table: "Accommodations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "OfficeId",
                table: "Accommodations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accommodations",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Offices_OfficeId",
                table: "Accommodations",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripEmployees_AspNetUsers_EmployeeId",
                table: "TripEmployees",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripEmployees_Trips_TripId",
                table: "TripEmployees",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Offices_EndLocationId",
                table: "Trips",
                column: "EndLocationId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_OrganizerId",
                table: "Trips",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Offices_StartLocationId",
                table: "Trips",
                column: "StartLocationId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
