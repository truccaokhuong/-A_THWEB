using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH_WEB.Migrations
{
    public partial class UpdateAttractionDecimalPrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(2055), new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(2055) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(2057), new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(2058) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(1974), new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(1980), new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(1981) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(1984), new DateTime(2025, 6, 3, 8, 7, 30, 890, DateTimeKind.Utc).AddTicks(1984) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9425), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9426) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9428), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9428) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9342), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9343) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9369), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9373), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9373) });
        }
    }
}
