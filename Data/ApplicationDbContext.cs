// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Models;
using TH_WEB.Models.Authorization;
using TH_WEB.Areas.Attractions.Models;
using System;

namespace TH_WEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<TH_WEB.Models.Review> Reviews { get; set; } = null!;
        public DbSet<Offer> Offers { get; set; } = null!;
        public DbSet<HotelImage> HotelImages { get; set; } = null!;
        public DbSet<TravelPackage> TravelPackages { get; set; } = null!;
        public DbSet<CarRental> CarRentals { get; set; } = null!;
        public DbSet<CarRentalBooking> CarRentalBookings { get; set; } = null!;
        public DbSet<PackageBooking> PackageBookings { get; set; } = null!;
        public DbSet<PackageItinerary> PackageItineraries { get; set; } = null!;
        public DbSet<PackageExtra> PackageExtras { get; set; } = null!;
        public DbSet<PackageFAQ> PackageFAQs { get; set; } = null!;
        public DbSet<CarType> CarTypes { get; set; } = null!;        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<RentalLocation> RentalLocations { get; set; } = null!;
        public DbSet<CarRentalExtra> CarRentalExtras { get; set; } = null!;
        public DbSet<Attraction> Attractions { get; set; } = null!;
        
        // Authorization tables
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<UserPermission> UserPermissions { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<UserActivity> UserActivities { get; set; } = null!;
        public DbSet<ResourceOwnership> ResourceOwnerships { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
              // Configure decimal precision for Attraction model
            modelBuilder.Entity<Attraction>()
                .Property(a => a.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Attraction>()
                .Property(a => a.Rating)
                .HasPrecision(3, 2);
            
            // Configure decimal precision for other models
            modelBuilder.Entity<Location>()
                .Property(l => l.Latitude)
                .HasPrecision(10, 8);
            
            modelBuilder.Entity<Location>()
                .Property(l => l.Longitude)
                .HasPrecision(11, 8);
            
            modelBuilder.Entity<PackageExtra>()
                .Property(pe => pe.Price)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<TravelPackage>()
                .Property(tp => tp.DiscountPercentage)
                .HasPrecision(5, 2);
            
            // Configure decimal precision for Review ratings
            modelBuilder.Entity<TH_WEB.Models.Review>()
                .Property(r => r.CleanlinessRating)
                .HasPrecision(3, 2);
            
            modelBuilder.Entity<TH_WEB.Models.Review>()
                .Property(r => r.ComfortRating)
                .HasPrecision(3, 2);
            
            modelBuilder.Entity<TH_WEB.Models.Review>()
                .Property(r => r.LocationRating)
                .HasPrecision(3, 2);
            
            modelBuilder.Entity<TH_WEB.Models.Review>()
                .Property(r => r.ServiceRating)
                .HasPrecision(3, 2);
            
            modelBuilder.Entity<TH_WEB.Models.Review>()
                .Property(r => r.ValueForMoneyRating)
                .HasPrecision(3, 2);
            
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
            
            // Configure relationships and constraints
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Images)
                .WithOne(hi => hi.Hotel)
                .HasForeignKey(hi => hi.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Reviews)
                .WithOne(hr => hr.Hotel)
                .HasForeignKey(hr => hr.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelPackage>()
                .HasOne(p => p.Hotel)
                .WithMany()
                .HasForeignKey(p => p.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelPackage>()
                .HasOne(p => p.CarRental)
                .WithMany()
                .HasForeignKey(p => p.CarRentalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarRental>()
                .HasOne(c => c.Location)
                .WithMany(l => l.CarRentals)
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarRental>()
                .HasOne(c => c.PickupLocation)
                .WithMany(l => l.PickupLocations)
                .HasForeignKey(c => c.PickupLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarRental>()
                .HasOne(c => c.DropoffLocation)
                .WithMany(l => l.DropoffLocations)
                .HasForeignKey(c => c.DropoffLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarRentalExtra>()
                .HasOne(e => e.CarRentalBooking)
                .WithMany(b => b.Extras)
                .HasForeignKey(e => e.CarRentalBookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarRentalBooking>()
                .HasOne(b => b.CarRental)
                .WithMany(c => c.CarRentalBookings)
                .HasForeignKey(b => b.CarRentalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PackageBooking>()
                .HasOne(b => b.TravelPackage)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelPackage>()
                .HasMany(p => p.Itinerary)
                .WithOne(i => i.Package)
                .HasForeignKey(i => i.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelPackage>()
                .HasMany(p => p.Extras)
                .WithOne(e => e.Package)
                .HasForeignKey(e => e.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelPackage>()
                .HasMany(p => p.FAQs)
                .WithOne(f => f.Package)
                .HasForeignKey(f => f.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TH_WEB.Models.Review>()
                .HasOne(r => r.TravelPackage)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.TravelPackageId)
                .OnDelete(DeleteBehavior.Cascade);
            
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
                    Policies = "No smoking in rooms.",
                    CancellationPolicy = "Free cancellation within 24 hours.",
                    LanguagesSpoken = "English, Vietnamese",
                    TotalReviews = 1200,
                    TotalBookings = 5000,
                    PaymentOptions = "Credit Card, Cash",
                    AcceptsCreditCards = true,
                    CreatedAt = new DateTime(2018, 1, 1),
                    UpdatedAt = new DateTime(2024, 6, 1),
                    Email = "mountain@ezbooking.com",
                    Phone = "0112233445"
                }
            );
            
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    HotelId = 1,
                    RoomNumber = "101",
                    RoomType = "Standard",
                    Price = 150.00m,
                    Description = "A comfortable standard room with city view.",
                    MaxOccupancy = 2,
                    AdultCapacity = 2,
                    ChildCapacity = 0,
                    IsAvailable = true,
                    IsActive = true,
                    IsFeatured = false,
                    SquareMeters = 30,
                    BedType = "King",
                    BedCount = 1,
                    ViewType = "City",
                    Floor = "1",
                    HasPrivateBathroom = true,
                    HasAirConditioning = true,
                    HasTV = true,
                    HasWifi = true,
                    HasMinibar = false,
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
                    MainImageUrl = "/images/rooms/standard-city.jpg",
                    CancellationPolicy = "Free cancellation within 24 hours.",
                    BreakfastPolicy = "Included",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    Id = 2,
                    HotelId = 1,
                    RoomNumber = "102",
                    RoomType = "Deluxe",
                    Price = 200.00m,
                    Description = "Spacious deluxe room with ocean view.",
                    MaxOccupancy = 3,
                    AdultCapacity = 2,
                    ChildCapacity = 1,
                    IsAvailable = true,
                    IsActive = true,
                    IsFeatured = false,
                    SquareMeters = 35,
                    BedType = "Queen",
                    BedCount = 2,
                    ViewType = "Ocean",
                    Floor = "10",
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
                    MainImageUrl = "/images/rooms/deluxe-ocean.jpg",
                    CancellationPolicy = "Free cancellation within 24 hours.",
                    BreakfastPolicy = "Included",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    Id = 3,
                    HotelId = 2,
                    RoomNumber = "501",
                    RoomType = "Suite",
                    Price = 350.00m,
                    Description = "Luxury suite with separate living area.",
                    MaxOccupancy = 4,
                    AdultCapacity = 2,
                    ChildCapacity = 2,
                    IsAvailable = true,
                    IsActive = true,
                    IsFeatured = true,
                    SquareMeters = 50,
                    BedType = "King",
                    BedCount = 1,
                    ViewType = "City",
                    Floor = "5",
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
                    HasIron = true,
                    HasCoffeemaker = true,
                    HasBathtub = true,
                    HasShower = true,
                    MainImageUrl = "/images/rooms/suite-city.jpg",
                    CancellationPolicy = "Free cancellation within 48 hours.",
                    BreakfastPolicy = "Available for fee",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Offer>().HasData(
                new Offer
                {
                    Id = 1,
                    HotelId = 1,
                    Title = "Summer Discount",
                    Description = "Get 15% off on all room types for stays in June and July.",
                    DiscountPercentage = 15,
                    StartDate = new DateTime(2024, 6, 1),
                    EndDate = new DateTime(2024, 7, 31),
                    IsActive = true,
                }
            );

            modelBuilder.Entity<CarType>().HasData(
                new CarType { Id = 1, Name = "Sedan", Category = CarCategory.Economy, Description = "Comfortable sedan", Seats = 4, Doors = 4, HasAirConditioning = true, HasAutomaticTransmission = true, HasGPS = true, HasBluetooth = true, HasUSBPort = true, HasChildSeat = false, HasLuggageSpace = true },
                new CarType { Id = 2, Name = "SUV", Category = CarCategory.SUV, Description = "Spacious SUV", Seats = 5, Doors = 4, HasAirConditioning = true, HasAutomaticTransmission = true, HasGPS = true, HasBluetooth = true, HasUSBPort = true, HasChildSeat = true, HasLuggageSpace = true }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Miami International Airport", Address = "2100 NW 42nd Ave", City = "Miami", Country = "USA", PostalCode = "33142", Latitude = 25.7959m, Longitude = -80.2902m },
                new Location { Id = 2, Name = "New York City", Address = "Times Square", City = "New York", Country = "USA", PostalCode = "10036", Latitude = 40.7580m, Longitude = -73.9855m }
            );             modelBuilder.Entity<CarRental>().HasData(
                new CarRental
                {
                    Id = 1,
                    Name = "Economy Sedan",
                    Description = "Fuel-efficient economy car",
                    CarTypeId = 1, // Sedan
                    LocationId = 1, // Miami Airport
                    PickupLocationId = 1, // Miami Airport
                    DropoffLocationId = 1, // Miami Airport
                    DailyRate = 35.00m,
                    IsAvailable = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new CarRental
                {
                    Id = 2,
                    Name = "Standard SUV",
                    Description = "Spacious SUV for families",
                    CarTypeId = 2, // SUV
                    LocationId = 2, // New York City
                    PickupLocationId = 2, // New York City
                    DropoffLocationId = 2, // New York City
                    DailyRate = 60.00m,
                    IsAvailable = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }            );            
        }
    }
}