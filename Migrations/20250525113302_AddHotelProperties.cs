using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH_WEB.Migrations
{
    public partial class AddHotelProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Hotels_HotelId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "BookingAddon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage");

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ActualCheckInDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ActualCheckOutDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingSource",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HasReviewed",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IdentificationType",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsFullyPaid",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RefundAmount",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "HotelImage",
                newName: "HotelImages");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Bookings",
                newName: "BookingReference");

            migrationBuilder.RenameColumn(
                name: "NumberOfRooms",
                table: "Bookings",
                newName: "NumberOfInfants");

            migrationBuilder.RenameColumn(
                name: "NumberOfGuests",
                table: "Bookings",
                newName: "NumberOfChildren");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImage_HotelId",
                table: "HotelImages",
                newName: "IX_HotelImages_HotelId");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TravelPackageId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Offers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Hotels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Hotels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Hotels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParkingPolicy",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetPolicy",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "Hotels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Hotels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Hotels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SmokingPolicy",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecialInstructions",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Hotels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "Hotels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequests",
                table: "Bookings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingAmenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingAmenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingAmenity_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingService_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    Doors = table.Column<int>(type: "int", nullable: false),
                    HasAirConditioning = table.Column<bool>(type: "bit", nullable: false),
                    HasAutomaticTransmission = table.Column<bool>(type: "bit", nullable: false),
                    HasGPS = table.Column<bool>(type: "bit", nullable: false),
                    HasBluetooth = table.Column<bool>(type: "bit", nullable: false),
                    HasUSBPort = table.Column<bool>(type: "bit", nullable: false),
                    HasChildSeat = table.Column<bool>(type: "bit", nullable: false),
                    HasLuggageSpace = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    OpeningHours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarRentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    PickupLocationId = table.Column<int>(type: "int", nullable: false),
                    DropoffLocationId = table.Column<int>(type: "int", nullable: false),
                    DailyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentalLocationId = table.Column<int>(type: "int", nullable: true),
                    RentalLocationId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentals_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRentals_Locations_DropoffLocationId",
                        column: x => x.DropoffLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarRentals_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarRentals_Locations_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarRentals_RentalLocations_RentalLocationId",
                        column: x => x.RentalLocationId,
                        principalTable: "RentalLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarRentals_RentalLocations_RentalLocationId1",
                        column: x => x.RentalLocationId1,
                        principalTable: "RentalLocations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarRentalBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarRentalId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DriverEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DriverPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LicenseCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxesAndFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtrasFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SecurityDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    BookingReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecialRequests = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HasGPS = table.Column<bool>(type: "bit", nullable: false),
                    HasChildSeat = table.Column<bool>(type: "bit", nullable: false),
                    HasAdditionalDriver = table.Column<bool>(type: "bit", nullable: false),
                    AdditionalDriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdditionalDriverLicense = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PickupCondition = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DropoffCondition = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PickupMileage = table.Column<int>(type: "int", nullable: true),
                    DropoffMileage = table.Column<int>(type: "int", nullable: true),
                    FuelLevelPickup = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FuelLevelDropoff = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActualPickupTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualDropoffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentalBookings_CarRentals_CarRentalId",
                        column: x => x.CarRentalId,
                        principalTable: "CarRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DestinationCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdultPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChildPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InfantPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxAdults = table.Column<int>(type: "int", nullable: false),
                    MaxChildren = table.Column<int>(type: "int", nullable: false),
                    MaxInfants = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    CarRentalId = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncludesHotel = table.Column<bool>(type: "bit", nullable: false),
                    IncludesBreakfast = table.Column<bool>(type: "bit", nullable: false),
                    IncludesMeals = table.Column<bool>(type: "bit", nullable: false),
                    IncludesTransfers = table.Column<bool>(type: "bit", nullable: false),
                    IncludesTours = table.Column<bool>(type: "bit", nullable: false),
                    IncludesInsurance = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPackages_CarRentals_CarRentalId",
                        column: x => x.CarRentalId,
                        principalTable: "CarRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TravelPackages_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarRentalExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarRentalBookingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CarRentalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentalExtras_CarRentalBookings_CarRentalBookingId",
                        column: x => x.CarRentalBookingId,
                        principalTable: "CarRentalBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRentalExtras_CarRentals_CarRentalId",
                        column: x => x.CarRentalId,
                        principalTable: "CarRentals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackageBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfAdults = table.Column<int>(type: "int", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: false),
                    NumberOfInfants = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageBookings_TravelPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "TravelPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageExtras_TravelPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "TravelPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageFAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFAQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageFAQs_TravelPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "TravelPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageItineraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageItineraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageItineraries_TravelPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "TravelPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "Id", "Category", "Description", "Doors", "HasAirConditioning", "HasAutomaticTransmission", "HasBluetooth", "HasChildSeat", "HasGPS", "HasLuggageSpace", "HasUSBPort", "Name", "Seats" },
                values: new object[,]
                {
                    { 1, 0, "Comfortable sedan", 4, true, true, true, false, true, true, true, "Sedan", 4 },
                    { 2, 6, "Spacious SUV", 4, true, true, true, true, true, true, true, "SUV", 5 }
                });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Currency", "Location", "ParkingPolicy", "PetPolicy", "SeoDescription", "SeoKeywords", "SeoTitle", "Slug", "SmokingPolicy", "SpecialInstructions", "State", "TimeZone" },
                values: new object[] { "", "", "", "", "", "", "", "", "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Currency", "Location", "ParkingPolicy", "PetPolicy", "SeoDescription", "SeoKeywords", "SeoTitle", "Slug", "SmokingPolicy", "SpecialInstructions", "State", "TimeZone" },
                values: new object[] { "", "", "", "", "", "", "", "", "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CancellationPolicy", "Currency", "LanguagesSpoken", "Location", "ParkingPolicy", "PetPolicy", "Phone", "Policies", "SeoDescription", "SeoKeywords", "SeoTitle", "Slug", "SmokingPolicy", "SpecialInstructions", "State", "TimeZone", "TotalBookings", "TotalReviews" },
                values: new object[] { "Free cancellation within 24 hours.", "", "English, Vietnamese", "", "", "", "0112233445", "No smoking in rooms.", "", "", "", "", "", "", "", "", 5000, 1200 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "Country", "Latitude", "Longitude", "Name", "PostalCode" },
                values: new object[,]
                {
                    { 1, "2100 NW 42nd Ave", "Miami", "USA", 25.7959m, -80.2902m, "Miami International Airport", "33142" },
                    { 2, "Times Square", "New York", "USA", 40.7580m, -73.9855m, "New York City", "10036" }
                });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DiscountPercentage", "EndDate", "ImageUrl", "PromoCode", "StartDate", "Title" },
                values: new object[] { "Get 15% off on all room types for stays in June and July.", 15, new DateTime(2024, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Summer Discount" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChildCapacity", "CreatedAt", "Description", "DiscountedPrice", "Floor", "HasMinibar", "MainImageUrl", "Price", "RoomType", "SquareMeters", "UpdatedAt" },
                values: new object[] { 0, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8638), "A comfortable standard room with city view.", 0m, "1", false, "/images/rooms/standard-city.jpg", 150.00m, "Standard", 30, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8638) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BreakfastPolicy", "CancellationPolicy", "CreatedAt", "Description", "DiscountedPrice", "Floor", "IsFeatured", "MainImageUrl", "MaxOccupancy", "Price", "RoomNumber", "RoomType", "SquareMeters", "UpdatedAt", "ViewType" },
                values: new object[] { "Included", "Free cancellation within 24 hours.", new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8644), "Spacious deluxe room with ocean view.", 0m, "10", false, "/images/rooms/deluxe-ocean.jpg", 3, 200.00m, "102", "Deluxe", 35, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8644), "Ocean" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BedCount", "BedType", "BreakfastPolicy", "CreatedAt", "Description", "DiscountedPrice", "HasBathtub", "HasCoffeemaker", "HasIron", "HasMinibar", "HasRoomService", "HasSafe", "IsFeatured", "MainImageUrl", "Price", "RoomNumber", "RoomType", "SquareMeters", "UpdatedAt" },
                values: new object[] { 1, "King", "Available for fee", new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8647), "Luxury suite with separate living area.", 0m, true, true, true, true, true, true, true, "/images/rooms/suite-city.jpg", 350.00m, "501", "Suite", 50, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8647) });

            migrationBuilder.InsertData(
                table: "CarRentals",
                columns: new[] { "Id", "CarTypeId", "CreatedAt", "DailyRate", "Description", "DropoffLocationId", "IsActive", "IsAvailable", "LocationId", "Name", "PickupLocationId", "RentalLocationId", "RentalLocationId1", "UpdatedAt" },
                values: new object[] { 1, 1, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8738), 35.00m, "Fuel-efficient economy car", 1, true, true, 1, "Economy Sedan", 1, null, null, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.InsertData(
                table: "CarRentals",
                columns: new[] { "Id", "CarTypeId", "CreatedAt", "DailyRate", "Description", "DropoffLocationId", "IsActive", "IsAvailable", "LocationId", "Name", "PickupLocationId", "RentalLocationId", "RentalLocationId1", "UpdatedAt" },
                values: new object[] { 2, 2, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8741), 60.00m, "Spacious SUV for families", 2, true, true, 2, "Standard SUV", 2, null, null, new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8741) });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TravelPackageId",
                table: "Reviews",
                column: "TravelPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookingAmenity_BookingId",
                table: "BookingAmenity",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_BookingId",
                table: "BookingService",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalBookings_CarRentalId",
                table: "CarRentalBookings",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalExtras_CarRentalBookingId",
                table: "CarRentalExtras",
                column: "CarRentalBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalExtras_CarRentalId",
                table: "CarRentalExtras",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_CarTypeId",
                table: "CarRentals",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_DropoffLocationId",
                table: "CarRentals",
                column: "DropoffLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_LocationId",
                table: "CarRentals",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_PickupLocationId",
                table: "CarRentals",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_RentalLocationId",
                table: "CarRentals",
                column: "RentalLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_RentalLocationId1",
                table: "CarRentals",
                column: "RentalLocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_PackageBookings_PackageId",
                table: "PackageBookings",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageExtras_PackageId",
                table: "PackageExtras",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFAQs_PackageId",
                table: "PackageFAQs",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageItineraries_PackageId",
                table: "PackageItineraries",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackages_CarRentalId",
                table: "TravelPackages",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackages_HotelId",
                table: "TravelPackages",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Hotels_HotelId",
                table: "HotelImages",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Hotels_HotelId",
                table: "Offers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_TravelPackages_TravelPackageId",
                table: "Reviews",
                column: "TravelPackageId",
                principalTable: "TravelPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Hotels_HotelId",
                table: "HotelImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Hotels_HotelId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_TravelPackages_TravelPackageId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookingAmenity");

            migrationBuilder.DropTable(
                name: "BookingService");

            migrationBuilder.DropTable(
                name: "CarRentalExtras");

            migrationBuilder.DropTable(
                name: "PackageBookings");

            migrationBuilder.DropTable(
                name: "PackageExtras");

            migrationBuilder.DropTable(
                name: "PackageFAQs");

            migrationBuilder.DropTable(
                name: "PackageItineraries");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CarRentalBookings");

            migrationBuilder.DropTable(
                name: "TravelPackages");

            migrationBuilder.DropTable(
                name: "CarRentals");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "RentalLocations");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TravelPackageId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages");

            migrationBuilder.DropColumn(
                name: "TravelPackageId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ParkingPolicy",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PetPolicy",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "SmokingPolicy",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "SpecialInstructions",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "HotelImages",
                newName: "HotelImage");

            migrationBuilder.RenameColumn(
                name: "NumberOfInfants",
                table: "Bookings",
                newName: "NumberOfRooms");

            migrationBuilder.RenameColumn(
                name: "NumberOfChildren",
                table: "Bookings",
                newName: "NumberOfGuests");

            migrationBuilder.RenameColumn(
                name: "BookingReference",
                table: "Bookings",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImage",
                newName: "IX_HotelImage_HotelId");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hotels",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequests",
                table: "Bookings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualCheckInDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualCheckOutDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BookingSource",
                table: "Bookings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Bookings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasReviewed",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationType",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullyPaid",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Bookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Bookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RefundAmount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BookingAddon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingAddon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingAddon_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CancellationPolicy", "LanguagesSpoken", "Phone", "Policies", "TotalBookings", "TotalReviews" },
                values: new object[] { "Free cancellation within 48 hours.", "English, French", "0111222333", "Pets allowed.", 2500, 700 });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DiscountPercentage", "EndDate", "ImageUrl", "PromoCode", "StartDate", "Title" },
                values: new object[] { "Save up to 20% on your summer vacation with our special offer.", 20, new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/offers/summer-getaway.jpg", "SUMMER20", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Summer Getaway Deal" });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Description", "DiscountAmount", "DiscountPercentage", "EndDate", "HotelId", "ImageUrl", "IsActive", "IsExclusive", "MinimumStay", "PromoCode", "StartDate", "TermsAndConditions", "Title" },
                values: new object[] { 2, "Book a weekend stay and get a free breakfast for two.", null, 15, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "/images/offers/weekend-escape.jpg", true, false, null, "WEEKEND15", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Weekend Escape" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChildCapacity", "CreatedAt", "Description", "DiscountedPrice", "Floor", "HasMinibar", "MainImageUrl", "Price", "RoomType", "SquareMeters", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spacious deluxe room with city view.", 179.99m, "10", true, "/images/rooms/deluxe-king.jpg", 199.99m, "Deluxe", 35, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BreakfastPolicy", "CancellationPolicy", "CreatedAt", "Description", "DiscountedPrice", "Floor", "IsFeatured", "MainImageUrl", "MaxOccupancy", "Price", "RoomNumber", "RoomType", "SquareMeters", "UpdatedAt", "ViewType" },
                values: new object[] { "Available for fee", "Non-refundable.", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxury suite with separate living area and city view.", 269.99m, "20", true, "/images/rooms/executive-suite.jpg", 2, 299.99m, "201", "Suite", 50, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "City" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BedCount", "BedType", "BreakfastPolicy", "CreatedAt", "Description", "DiscountedPrice", "HasBathtub", "HasCoffeemaker", "HasIron", "HasMinibar", "HasRoomService", "HasSafe", "IsFeatured", "MainImageUrl", "Price", "RoomNumber", "RoomType", "SquareMeters", "UpdatedAt" },
                values: new object[] { 2, "Twin", "Included", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable room with two double beds.", 139.99m, false, false, false, false, false, false, false, "/images/rooms/standard-double.jpg", 149.99m, "301", "Standard", 30, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_BookingAddon_BookingId",
                table: "BookingAddon",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Hotels_HotelId",
                table: "Offers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
