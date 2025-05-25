// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using TH_WEB.Models;

namespace TH_WEB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Offer> Offers { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Avoid multiple cascade paths
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Hotel)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
            
            // Seed data for Hotels
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "EzBooking Luxury Resort",
                    Description = "Experience luxury and comfort in our 5-star resort with stunning views.",
                    Address = "123 Beach Road",
                    City = "Miami",
                    Country = "USA",
                    Rating = 4.8m,
                    ImageUrl = "/images/hotels/luxury-resort.jpg",
                    HasFreeWifi = true,
                    HasParking = true,
                    HasSwimmingPool = true,
                    HasGym = true,
                    HasRestaurant = true,
                    HasRoomService = true,
                    HasBar = true,
                    HasAirportShuttle = false,
                    HasFitnessCenter = true,
                    HasSpa = false,
                    HasBathTub = true,
                    IsPetFriendly = false,
                    HasBusinessFacilities = true,
                    HasChildrenActivities = true,
                    CheckInTime = "14:00",
                    CheckOutTime = "12:00",
                    YearBuilt = 2015,
                    YearRenovated = 2022,
                    HotelChain = "EzBooking",
                    HotelType = "Resort",
                    StarRating = 5,
                    Policies = "No smoking in rooms.",
                    CancellationPolicy = "Free cancellation within 24 hours.",
                    LanguagesSpoken = "English, Vietnamese",
                    TotalReviews = 1500,
                    TotalBookings = 6000,
                    PaymentOptions = "Credit Card, Cash",
                    AcceptsCreditCards = true,
                    CreatedAt = new DateTime(2015, 1, 1),
                    UpdatedAt = new DateTime(2024, 6, 1),
                    Email = "luxury@ezbooking.com",
                    Phone = "0123456789"
                },
                new Hotel
                {
                    Id = 2,
                    Name = "EzBooking City Center",
                    Description = "Located in the heart of downtown, perfect for business and leisure travelers.",
                    Address = "456 Main Street",
                    City = "New York",
                    Country = "USA",
                    Rating = 4.5m,
                    ImageUrl = "/images/hotels/city-center.jpg",
                    HasFreeWifi = true,
                    HasParking = false,
                    HasSwimmingPool = false,
                    HasGym = true,
                    HasRestaurant = true,
                    HasRoomService = true,
                    HasBar = true,
                    HasAirportShuttle = false,
                    HasFitnessCenter = true,
                    HasSpa = false,
                    HasBathTub = true,
                    IsPetFriendly = false,
                    HasBusinessFacilities = true,
                    HasChildrenActivities = false,
                    CheckInTime = "14:00",
                    CheckOutTime = "12:00",
                    YearBuilt = 2010,
                    YearRenovated = 2020,
                    HotelChain = "EzBooking",
                    HotelType = "Business Hotel",
                    StarRating = 4,
                    Policies = "No pets allowed.",
                    CancellationPolicy = "Non-refundable.",
                    LanguagesSpoken = "English",
                    TotalReviews = 900,
                    TotalBookings = 4000,
                    PaymentOptions = "Credit Card",
                    AcceptsCreditCards = true,
                    CreatedAt = new DateTime(2010, 1, 1),
                    UpdatedAt = new DateTime(2024, 6, 1),
                    Email = "citycenter@ezbooking.com",
                    Phone = "0987654321"
                },
                new Hotel
                {
                    Id = 3,
                    Name = "EzBooking Mountain Retreat",
                    Description = "Escape to the mountains and enjoy breathtaking views and fresh air.",
                    Address = "789 Mountain View Road",
                    City = "Denver",
                    Country = "USA",
                    Rating = 4.7m,
                    ImageUrl = "/images/hotels/mountain-retreat.jpg",
                    HasFreeWifi = true,
                    HasParking = true,
                    HasSwimmingPool = true,
                    HasGym = false,
                    HasRestaurant = true,
                    HasRoomService = false,
                    HasBar = false,
                    HasAirportShuttle = true,
                    HasFitnessCenter = false,
                    HasSpa = true,
                    HasBathTub = true,
                    IsPetFriendly = true,
                    HasBusinessFacilities = false,
                    HasChildrenActivities = true,
                    CheckInTime = "15:00",
                    CheckOutTime = "11:00",
                    YearBuilt = 2018,
                    YearRenovated = 2023,
                    HotelChain = "EzBooking",
                    HotelType = "Retreat",
                    StarRating = 5,
                    Policies = "Pets allowed.",
                    CancellationPolicy = "Free cancellation within 48 hours.",
                    LanguagesSpoken = "English, French",
                    TotalReviews = 700,
                    TotalBookings = 2500,
                    PaymentOptions = "Credit Card, Cash",
                    AcceptsCreditCards = true,
                    CreatedAt = new DateTime(2018, 1, 1),
                    UpdatedAt = new DateTime(2024, 6, 1),
                    Email = "mountain@ezbooking.com",
                    Phone = "0111222333"
                }
            );
            
            // Seed data for Rooms
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    HotelId = 1,
                    RoomNumber = "101",
                    RoomType = "Deluxe",
                    Description = "Spacious deluxe room with city view.",
                    Price = 199.99m,
                    DiscountedPrice = 179.99m,
                    MaxOccupancy = 2,
                    AdultCapacity = 2,
                    ChildCapacity = 1,
                    IsAvailable = true,
                    IsActive = true,
                    IsFeatured = false,
                    SquareMeters = 35,
                    BedType = "King",
                    BedCount = 1,
                    ViewType = "City",
                    Floor = "10",
                    HasPrivateBathroom = true,
                    HasAirConditioning = true,
                    HasTV = true,
                    HasWifi = true,
                    HasMinibar = true,
                    HasSafe = true,
                    HasWorkDesk = true,
                    HasHairDryer = true,
                    HasBalcony = false,
                    IsNonSmoking = true,
                    HasRoomService = true,
                    HasIron = false,
                    HasCoffeemaker = true,
                    HasBathtub = true,
                    HasShower = true,
                    MainImageUrl = "/images/rooms/deluxe-king.jpg",
                    CancellationPolicy = "Free cancellation within 24 hours.",
                    BreakfastPolicy = "Included",
                    CreatedAt = new DateTime(2024, 1, 1),
                    UpdatedAt = new DateTime(2024, 6, 1)
                },
                new Room
                {
                    Id = 2,
                    HotelId = 1,
                    RoomNumber = "201",
                    RoomType = "Suite",
                    Description = "Luxury suite with separate living area and city view.",
                    Price = 299.99m,
                    DiscountedPrice = 269.99m,
                    MaxOccupancy = 2,
                    AdultCapacity = 2,
                    ChildCapacity = 1,
                    IsAvailable = true,
                    IsActive = true,
                    IsFeatured = true,
                    SquareMeters = 50,
                    BedType = "Queen",
                    BedCount = 2,
                    ViewType = "City",
                    Floor = "20",
                    HasPrivateBathroom = true,
                    HasAirConditioning = true,
                    HasTV = true,
                    HasWifi = true,
                    HasMinibar = true,
                    HasSafe = true,
                    HasWorkDesk = true,
                    HasHairDryer = true,
                    HasBalcony = true,
                    IsNonSmoking = true,
                    HasRoomService = true,
                    HasIron = true,
                    HasCoffeemaker = true,
                    HasBathtub = true,
                    HasShower = true,
                    MainImageUrl = "/images/rooms/executive-suite.jpg",
                    CancellationPolicy = "Non-refundable.",
                    BreakfastPolicy = "Available for fee",
                    CreatedAt = new DateTime(2024, 2, 1),
                    UpdatedAt = new DateTime(2024, 6, 1)
                },
                new Room
                {
                    Id = 3,
                    HotelId = 2,
                    RoomNumber = "301",
                    RoomType = "Standard",
                    Description = "Comfortable room with two double beds.",
                    Price = 149.99m,
                    DiscountedPrice = 139.99m,
                    MaxOccupancy = 4,
                    AdultCapacity = 2,
                    ChildCapacity = 2,
                    IsAvailable = true,
                    IsActive = true,
                    IsFeatured = false,
                    SquareMeters = 30,
                    BedType = "Twin",
                    BedCount = 2,
                    ViewType = "City",
                    Floor = "5",
                    HasPrivateBathroom = true,
                    HasAirConditioning = true,
                    HasTV = true,
                    HasWifi = true,
                    HasMinibar = false,
                    HasSafe = false,
                    HasWorkDesk = true,
                    HasHairDryer = true,
                    HasBalcony = false,
                    IsNonSmoking = true,
                    HasRoomService = false,
                    HasIron = false,
                    HasCoffeemaker = false,
                    HasBathtub = false,
                    HasShower = true,
                    MainImageUrl = "/images/rooms/standard-double.jpg",
                    CancellationPolicy = "Free cancellation within 48 hours.",
                    BreakfastPolicy = "Included",
                    CreatedAt = new DateTime(2024, 3, 1),
                    UpdatedAt = new DateTime(2024, 6, 1)
                }
            );
            
            // Seed data for Offers
            modelBuilder.Entity<Offer>().HasData(
                new Offer
                {
                    Id = 1,
                    Title = "Summer Getaway Deal",
                    Description = "Save up to 20% on your summer vacation with our special offer.",
                    DiscountPercentage = 20,
                    ImageUrl = "/images/offers/summer-getaway.jpg",
                    StartDate = new DateTime(2025, 6, 1),
                    EndDate = new DateTime(2025, 8, 31),
                    PromoCode = "SUMMER20",
                    IsActive = true
                },
                new Offer
                {
                    Id = 2,
                    Title = "Weekend Escape",
                    Description = "Book a weekend stay and get a free breakfast for two.",
                    DiscountPercentage = 15,
                    ImageUrl = "/images/offers/weekend-escape.jpg",
                    StartDate = new DateTime(2025, 1, 1),
                    EndDate = new DateTime(2025, 12, 31),
                    PromoCode = "WEEKEND15",
                    IsActive = true
                }
            );
        }
    }
}