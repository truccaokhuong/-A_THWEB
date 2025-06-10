using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH_WEB.Migrations
{
    public partial class AddTravelPackageSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "TravelPackages",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValueForMoneyRating",
                table: "Reviews",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ServiceRating",
                table: "Reviews",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationRating",
                table: "Reviews",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ComfortRating",
                table: "Reviews",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CleanlinessRating",
                table: "Reviews",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Locations",
                type: "decimal(11,8)",
                precision: 11,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Locations",
                type: "decimal(10,8)",
                precision: 10,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1768), new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1768) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1771), new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1686), new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1686) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1692), new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1692) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1696), new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1696) });

            migrationBuilder.InsertData(
                table: "TravelPackages",
                columns: new[] { "Id", "AdultPrice", "CarRentalId", "ChildPrice", "City", "Country", "CreatedAt", "Description", "DestinationCode", "DiscountPercentage", "EndDate", "HotelId", "ImageUrl", "IncludesBreakfast", "IncludesHotel", "IncludesInsurance", "IncludesMeals", "IncludesTours", "IncludesTransfers", "InfantPrice", "IsActive", "IsFeatured", "MaxAdults", "MaxChildren", "MaxInfants", "Name", "Priority", "Region", "StartDate", "Status", "TotalPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 800.00m, 1, 400.00m, "Miami", "USA", new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1788), "Enjoy 5 days and 4 nights at our luxury resort in Miami with all-inclusive amenities.", "MIA001", 10.0m, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "/images/packages/miami-beach.jpg", true, true, true, true, true, true, 0.00m, true, true, 4, 2, 1, "Miami Beach Vacation", 1, "Florida", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1200.00m, new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1788) },
                    { 2, 600.00m, 2, 300.00m, "New York", "USA", new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1794), "Discover the best of NYC with 3 days and 2 nights in the heart of Manhattan.", "NYC001", 5.0m, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "/images/packages/nyc-explorer.jpg", false, true, true, false, true, false, 0.00m, true, false, 6, 3, 2, "New York City Explorer", 2, "New York State", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 800.00m, new DateTime(2025, 6, 10, 2, 53, 13, 626, DateTimeKind.Utc).AddTicks(1794) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "TravelPackages",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValueForMoneyRating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ServiceRating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationRating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ComfortRating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CleanlinessRating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Locations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,8)",
                oldPrecision: 11,
                oldScale: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Locations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)",
                oldPrecision: 10,
                oldScale: 8);

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7362), new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7363) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7365), new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7365) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7305), new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7306) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7311), new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7312) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7316), new DateTime(2025, 6, 9, 14, 57, 16, 329, DateTimeKind.Utc).AddTicks(7316) });
        }
    }
}
