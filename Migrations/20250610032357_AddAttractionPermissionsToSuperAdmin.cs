using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH_WEB.Migrations
{
    public partial class AddAttractionPermissionsToSuperAdmin : Migration
    {        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing TravelPackages data to prevent duplicate key errors
            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "Id",
                keyValue: 2);            // Add attraction permissions to SuperAdmin role
            migrationBuilder.Sql(@"
                -- Check if SuperAdmin role exists, if not create it
                DECLARE @SuperAdminRoleId NVARCHAR(450)
                SELECT @SuperAdminRoleId = Id FROM AspNetRoles WHERE Name = 'SuperAdmin'
                
                IF @SuperAdminRoleId IS NULL
                BEGIN
                    SET @SuperAdminRoleId = NEWID()
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
                    VALUES (@SuperAdminRoleId, 'SuperAdmin', 'SUPERADMIN', NEWID())
                END
                
                -- Insert attraction permissions if they don't exist
                IF NOT EXISTS (SELECT 1 FROM AspNetRoleClaims WHERE RoleId = @SuperAdminRoleId AND ClaimType = 'permission' AND ClaimValue = 'attraction.create')
                BEGIN
                    INSERT INTO AspNetRoleClaims (RoleId, ClaimType, ClaimValue)
                    VALUES (@SuperAdminRoleId, 'permission', 'attraction.create')
                END
                
                IF NOT EXISTS (SELECT 1 FROM AspNetRoleClaims WHERE RoleId = @SuperAdminRoleId AND ClaimType = 'permission' AND ClaimValue = 'attraction.edit')
                BEGIN
                    INSERT INTO AspNetRoleClaims (RoleId, ClaimType, ClaimValue)
                    VALUES (@SuperAdminRoleId, 'permission', 'attraction.edit')
                END
                
                IF NOT EXISTS (SELECT 1 FROM AspNetRoleClaims WHERE RoleId = @SuperAdminRoleId AND ClaimType = 'permission' AND ClaimValue = 'attraction.delete')
                BEGIN
                    INSERT INTO AspNetRoleClaims (RoleId, ClaimType, ClaimValue)
                    VALUES (@SuperAdminRoleId, 'permission', 'attraction.delete')
                END
                
                IF NOT EXISTS (SELECT 1 FROM AspNetRoleClaims WHERE RoleId = @SuperAdminRoleId AND ClaimType = 'permission' AND ClaimValue = 'attraction.manage')
                BEGIN
                    INSERT INTO AspNetRoleClaims (RoleId, ClaimType, ClaimValue)
                    VALUES (@SuperAdminRoleId, 'permission', 'attraction.manage')
                END
            ");

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5693), new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5693) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5696), new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5696) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5636), new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5636) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5642), new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5642) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5645), new DateTime(2025, 6, 10, 3, 23, 56, 115, DateTimeKind.Utc).AddTicks(5645) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
