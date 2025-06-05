using System;
using System.Linq;
using System.Collections.Generic;
using TH_WEB.Models;
using Microsoft.EntityFrameworkCore;

namespace TH_WEB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if we already have data
            if (context.Hotels.Any())
            {
                return; // Database has been seeded
            }

            // Add sample hotels
            var hotels = new Hotel[]
            {
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
            };

            context.Hotels.AddRange(hotels);

            // Add sample reviews
            var reviews = new Review[]
            {
                new Review
                {
                    Id = 1,
                    HotelId = 1,
                    Rating = 5,
                    Comment = "Excellent service and beautiful rooms!",
                    ReviewerName = "John Doe",
                    ReviewerEmail = "john.doe@example.com",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new Review
                {
                    Id = 2,
                    HotelId = 1,
                    Rating = 4,
                    Comment = "Great location and comfortable stay.",
                    ReviewerName = "Jane Smith",
                    ReviewerEmail = "jane.smith@example.com",
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = DateTime.Now.AddDays(-10)
                },
                new Review
                {
                    Id = 3,
                    HotelId = 2,
                    Rating = 5,
                    Comment = "Amazing ocean views and friendly staff!",
                    ReviewerName = "Peter Jones",
                    ReviewerEmail = "peter.jones@example.com",
                    CreatedAt = DateTime.Now.AddDays(-3),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                }
            };

            context.Reviews.AddRange(reviews);

            // Add sample bookings
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    UserId = "user1@example.com", // Replace with actual user ID
                    RoomId = 1,
                    HotelId = 1,
                    CheckInDate = DateTime.Now.AddDays(1),
                    CheckOutDate = DateTime.Now.AddDays(3),
                    NumberOfAdults = 2,
                    NumberOfChildren = 0,
                    NumberOfInfants = 0,
                    TotalPrice = 300.00m, // Example total price
                    Status = BookingStatus.Confirmed,
                    PaymentStatus = PaymentStatus.Paid,
                    BookingReference = "B123456",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Booking
                {
                    Id = 2,
                    UserId = "user2@example.com", // Replace with actual user ID
                    RoomId = 2,
                    HotelId = 1,
                    CheckInDate = DateTime.Now.AddDays(2),
                    CheckOutDate = DateTime.Now.AddDays(4),
                     NumberOfAdults = 1,
                    NumberOfChildren = 1,
                    NumberOfInfants = 0,
                    TotalPrice = 400.00m, // Example total price
                    Status = BookingStatus.Pending,
                    PaymentStatus = PaymentStatus.Pending,
                    BookingReference = "B789012",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Bookings.AddRange(bookings);

            // Add sample rooms
            var rooms = new Room[]
            {
                new Room
                {
                    Id = 1,
                    HotelId = 1,
                    RoomNumber = "101",
                    RoomType = "Standard",
                    Description = "A comfortable standard room with city view.",
                    Price = 150.00m,
                    DiscountedPrice = 150.00m,
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
                    Description = "Spacious deluxe room with ocean view.",
                    Price = 200.00m,
                    DiscountedPrice = 200.00m,
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
                    Description = "Luxury suite with separate living area.",
                    Price = 350.00m,
                    DiscountedPrice = 350.00m,
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
            };

            context.Rooms.AddRange(rooms);

            // Add sample offers
            var offers = new Offer[]
            {
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
            };

            context.Offers.AddRange(offers);

            // Add sample car types
            var carTypes = new CarType[]
            {
                 new CarType { Id = 1, Name = "Sedan", Category = CarCategory.Economy, Description = "Comfortable sedan", Seats = 4, Doors = 4, HasAirConditioning = true, HasAutomaticTransmission = true, HasGPS = true, HasBluetooth = true, HasUSBPort = true, HasChildSeat = false, HasLuggageSpace = true },
                 new CarType { Id = 2, Name = "SUV", Category = CarCategory.SUV, Description = "Spacious SUV", Seats = 5, Doors = 4, HasAirConditioning = true, HasAutomaticTransmission = true, HasGPS = true, HasBluetooth = true, HasUSBPort = true, HasChildSeat = true, HasLuggageSpace = true }
            };

            context.CarTypes.AddRange(carTypes);

            // Add sample locations
            var locations = new Location[]
            {
                 new Location { Id = 1, Name = "Miami International Airport", Address = "2100 NW 42nd Ave", City = "Miami", Country = "USA", PostalCode = "33142", Latitude = 25.7959m, Longitude = -80.2902m },
                 new Location { Id = 2, Name = "New York City", Address = "Times Square", City = "New York", Country = "USA", PostalCode = "10036", Latitude = 40.7580m, Longitude = -73.9855m }
            };

            context.Locations.AddRange(locations);

             // Add sample car rentals
             var carRentals = new CarRental[]
             {
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
                }
            };

            context.CarRentals.AddRange(carRentals);


            context.SaveChanges();
        }
    }
} 