using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH_WEB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRooms = table.Column<int>(type: "int", nullable: false),
                    AvailableRooms = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: true),
                    HasFreeWifi = table.Column<bool>(type: "bit", nullable: false),
                    HasParking = table.Column<bool>(type: "bit", nullable: false),
                    HasSwimmingPool = table.Column<bool>(type: "bit", nullable: false),
                    HasGym = table.Column<bool>(type: "bit", nullable: false),
                    HasRestaurant = table.Column<bool>(type: "bit", nullable: false),
                    HasRoomService = table.Column<bool>(type: "bit", nullable: false),
                    HasBar = table.Column<bool>(type: "bit", nullable: false),
                    HasAirportShuttle = table.Column<bool>(type: "bit", nullable: false),
                    HasFitnessCenter = table.Column<bool>(type: "bit", nullable: false),
                    HasSpa = table.Column<bool>(type: "bit", nullable: false),
                    HasBathTub = table.Column<bool>(type: "bit", nullable: false),
                    IsPetFriendly = table.Column<bool>(type: "bit", nullable: false),
                    HasBusinessFacilities = table.Column<bool>(type: "bit", nullable: false),
                    HasChildrenActivities = table.Column<bool>(type: "bit", nullable: false),
                    HasBreakfast = table.Column<bool>(type: "bit", nullable: false),
                    CheckInTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CheckOutTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    YearBuilt = table.Column<int>(type: "int", nullable: true),
                    YearRenovated = table.Column<int>(type: "int", nullable: true),
                    HotelChain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HotelType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StarRating = table.Column<int>(type: "int", nullable: true),
                    Policies = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LanguagesSpoken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TotalReviews = table.Column<int>(type: "int", nullable: false),
                    TotalBookings = table.Column<int>(type: "int", nullable: false),
                    PaymentOptions = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AcceptsCreditCards = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelFacility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelFacility_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelImage_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinimumStay = table.Column<int>(type: "int", nullable: true),
                    PromoCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsExclusive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: false),
                    AdultCapacity = table.Column<int>(type: "int", nullable: false),
                    ChildCapacity = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    SquareMeters = table.Column<int>(type: "int", nullable: false),
                    BedType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    ViewType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HasPrivateBathroom = table.Column<bool>(type: "bit", nullable: false),
                    HasAirConditioning = table.Column<bool>(type: "bit", nullable: false),
                    HasTV = table.Column<bool>(type: "bit", nullable: false),
                    HasWifi = table.Column<bool>(type: "bit", nullable: false),
                    HasMinibar = table.Column<bool>(type: "bit", nullable: false),
                    HasSafe = table.Column<bool>(type: "bit", nullable: false),
                    HasWorkDesk = table.Column<bool>(type: "bit", nullable: false),
                    HasHairDryer = table.Column<bool>(type: "bit", nullable: false),
                    HasBalcony = table.Column<bool>(type: "bit", nullable: false),
                    IsNonSmoking = table.Column<bool>(type: "bit", nullable: false),
                    HasRoomService = table.Column<bool>(type: "bit", nullable: false),
                    HasIron = table.Column<bool>(type: "bit", nullable: false),
                    HasCoffeemaker = table.Column<bool>(type: "bit", nullable: false),
                    HasBathtub = table.Column<bool>(type: "bit", nullable: false),
                    HasShower = table.Column<bool>(type: "bit", nullable: false),
                    MainImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BreakfastPolicy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GuestName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GuestEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GuestPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialRequests = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsFullyPaid = table.Column<bool>(type: "bit", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdentificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActualCheckInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualCheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookingSource = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HasReviewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoomAmenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomAmenity_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomImage_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingAddon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReviewerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CleanlinessRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ComfortRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LocationRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServiceRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValueForMoneyRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TripType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TravelerType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HotelResponse = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewImage_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "AcceptsCreditCards", "Address", "AvailableRooms", "CancellationPolicy", "CheckInTime", "CheckOutTime", "City", "Country", "CreatedAt", "Description", "Email", "HasAirportShuttle", "HasBar", "HasBathTub", "HasBreakfast", "HasBusinessFacilities", "HasChildrenActivities", "HasFitnessCenter", "HasFreeWifi", "HasGym", "HasParking", "HasRestaurant", "HasRoomService", "HasSpa", "HasSwimmingPool", "HotelChain", "HotelType", "ImageUrl", "IsActive", "IsFeatured", "IsPetFriendly", "LanguagesSpoken", "Latitude", "Longitude", "Name", "PaymentOptions", "Phone", "Policies", "PostalCode", "PricePerNight", "Rating", "Region", "StarRating", "TotalBookings", "TotalReviews", "TotalRooms", "UpdatedAt", "Website", "YearBuilt", "YearRenovated" },
                values: new object[,]
                {
                    { 1, true, "123 Beach Road", 0, "Free cancellation within 24 hours.", "14:00", "12:00", "Miami", "USA", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Experience luxury and comfort in our 5-star resort with stunning views.", "luxury@ezbooking.com", false, true, true, false, true, true, true, true, true, true, true, true, false, true, "EzBooking", "Resort", "/images/hotels/luxury-resort.jpg", true, false, false, "English, Vietnamese", null, null, "EzBooking Luxury Resort", "Credit Card, Cash", "0123456789", "No smoking in rooms.", "", 0m, 4.8m, "", 5, 6000, 1500, 0, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 2015, 2022 },
                    { 2, true, "456 Main Street", 0, "Non-refundable.", "14:00", "12:00", "New York", "USA", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Located in the heart of downtown, perfect for business and leisure travelers.", "citycenter@ezbooking.com", false, true, true, false, true, false, true, true, true, false, true, true, false, false, "EzBooking", "Business Hotel", "/images/hotels/city-center.jpg", true, false, false, "English", null, null, "EzBooking City Center", "Credit Card", "0987654321", "No pets allowed.", "", 0m, 4.5m, "", 4, 4000, 900, 0, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 2010, 2020 },
                    { 3, true, "789 Mountain View Road", 0, "Free cancellation within 48 hours.", "15:00", "11:00", "Denver", "USA", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Escape to the mountains and enjoy breathtaking views and fresh air.", "mountain@ezbooking.com", true, false, true, false, false, true, false, true, false, true, true, false, true, true, "EzBooking", "Retreat", "/images/hotels/mountain-retreat.jpg", true, false, true, "English, French", null, null, "EzBooking Mountain Retreat", "Credit Card, Cash", "0111222333", "Pets allowed.", "", 0m, 4.7m, "", 5, 2500, 700, 0, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 2018, 2023 }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Description", "DiscountAmount", "DiscountPercentage", "EndDate", "HotelId", "ImageUrl", "IsActive", "IsExclusive", "MinimumStay", "PromoCode", "StartDate", "TermsAndConditions", "Title" },
                values: new object[] { 1, "Save up to 20% on your summer vacation with our special offer.", null, 20, new DateTime(2025, 8, 31), 1, "/images/offers/summer-getaway.jpg", true, false, null, "SUMMER20", new DateTime(2025, 6, 1), "", "Summer Getaway Deal" });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Description", "DiscountAmount", "DiscountPercentage", "EndDate", "HotelId", "ImageUrl", "IsActive", "IsExclusive", "MinimumStay", "PromoCode", "StartDate", "TermsAndConditions", "Title" },
                values: new object[] { 2, "Book a weekend stay and get a free breakfast for two.", null, 15, new DateTime(2025, 12, 31), 1, "/images/offers/weekend-escape.jpg", true, false, null, "WEEKEND15", new DateTime(2025, 1, 1), "", "Weekend Escape" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultCapacity", "BedCount", "BedType", "BreakfastPolicy", "CancellationPolicy", "ChildCapacity", "CreatedAt", "Description", "DiscountedPrice", "Floor", "HasAirConditioning", "HasBalcony", "HasBathtub", "HasCoffeemaker", "HasHairDryer", "HasIron", "HasMinibar", "HasPrivateBathroom", "HasRoomService", "HasSafe", "HasShower", "HasTV", "HasWifi", "HasWorkDesk", "HotelId", "IsActive", "IsAvailable", "IsFeatured", "IsNonSmoking", "MainImageUrl", "MaxOccupancy", "Price", "RoomNumber", "RoomType", "SquareMeters", "UpdatedAt", "ViewType" },
                values: new object[] { 1, 2, 1, "King", "Included", "Free cancellation within 24 hours.", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spacious deluxe room with city view.", 179.99m, "10", true, false, true, true, true, false, true, true, true, true, true, true, true, true, 1, true, true, false, true, "/images/rooms/deluxe-king.jpg", 2, 199.99m, "101", "Deluxe", 35, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "City" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultCapacity", "BedCount", "BedType", "BreakfastPolicy", "CancellationPolicy", "ChildCapacity", "CreatedAt", "Description", "DiscountedPrice", "Floor", "HasAirConditioning", "HasBalcony", "HasBathtub", "HasCoffeemaker", "HasHairDryer", "HasIron", "HasMinibar", "HasPrivateBathroom", "HasRoomService", "HasSafe", "HasShower", "HasTV", "HasWifi", "HasWorkDesk", "HotelId", "IsActive", "IsAvailable", "IsFeatured", "IsNonSmoking", "MainImageUrl", "MaxOccupancy", "Price", "RoomNumber", "RoomType", "SquareMeters", "UpdatedAt", "ViewType" },
                values: new object[] { 2, 2, 2, "Queen", "Available for fee", "Non-refundable.", 1, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxury suite with separate living area and city view.", 269.99m, "20", true, true, true, true, true, true, true, true, true, true, true, true, true, true, 1, true, true, true, true, "/images/rooms/executive-suite.jpg", 2, 299.99m, "201", "Suite", 50, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "City" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultCapacity", "BedCount", "BedType", "BreakfastPolicy", "CancellationPolicy", "ChildCapacity", "CreatedAt", "Description", "DiscountedPrice", "Floor", "HasAirConditioning", "HasBalcony", "HasBathtub", "HasCoffeemaker", "HasHairDryer", "HasIron", "HasMinibar", "HasPrivateBathroom", "HasRoomService", "HasSafe", "HasShower", "HasTV", "HasWifi", "HasWorkDesk", "HotelId", "IsActive", "IsAvailable", "IsFeatured", "IsNonSmoking", "MainImageUrl", "MaxOccupancy", "Price", "RoomNumber", "RoomType", "SquareMeters", "UpdatedAt", "ViewType" },
                values: new object[] { 3, 2, 2, "Twin", "Included", "Free cancellation within 48 hours.", 2, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable room with two double beds.", 139.99m, "5", true, false, false, false, true, false, false, true, false, false, true, true, true, true, 2, true, true, false, true, "/images/rooms/standard-double.jpg", 4, 149.99m, "301", "Standard", 30, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "City" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingAddon_BookingId",
                table: "BookingAddon",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacility_HotelId",
                table: "HotelFacility",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImage_HotelId",
                table: "HotelImage",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_HotelId",
                table: "Offers",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImage_ReviewId",
                table: "ReviewImage",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenity_RoomId",
                table: "RoomAmenity",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImage_RoomId",
                table: "RoomImage",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingAddon");

            migrationBuilder.DropTable(
                name: "HotelFacility");

            migrationBuilder.DropTable(
                name: "HotelImage");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "ReviewImage");

            migrationBuilder.DropTable(
                name: "RoomAmenity");

            migrationBuilder.DropTable(
                name: "RoomImage");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
