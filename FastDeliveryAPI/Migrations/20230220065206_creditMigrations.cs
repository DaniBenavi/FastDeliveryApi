using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastDeliveryAPI.Migrations
{
    /// <inheritdoc />
    public partial class creditMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "decimal",
                maxLength: 8,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedOnUtc", "Email", "ModifiedOnUtc", "Name", "PhoneNumberCustomer", "Status" },
                values: new object[,]
                {
                    { 1, "San miguel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "suleima@univo.edu.sv", null, "Suleima lopez", "2200-4400", true },
                    { 2, "San salvador", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kevin@univo.edu.sv", null, "Kevin Vasquez", "8800-4400", true }
                });
        }
    }
}
