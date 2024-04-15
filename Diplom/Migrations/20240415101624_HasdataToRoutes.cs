using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diplom.Migrations
{
    /// <inheritdoc />
    public partial class HasdataToRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "Created", "EndPointId", "Price", "StartPointId" },
                values: new object[] { new Guid("7cd6414a-c856-47eb-ab41-d9003655e959"), new DateTime(2024, 4, 15, 10, 16, 24, 645, DateTimeKind.Utc).AddTicks(289), new Guid("92abfca3-7e4a-42d7-bc24-e3079575057a"), 120000, new Guid("8dd63283-1bbd-4ffd-9a15-eb806c41614f") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("7cd6414a-c856-47eb-ab41-d9003655e959"));
        }
    }
}
