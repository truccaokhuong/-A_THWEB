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
                    Name = "Grand Hotel",
                    Description = "Luxury hotel in the heart of the city",
                    Address = "123 Main St",
                    City = "New York",
                    Country = "USA",
                    Rating = 4.5m,
                    ImageUrl = "/images/hotels/grand-hotel.jpg",
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
                    YearBuilt = 2000,
                    YearRenovated = 2018,
                    HotelChain = "GrandChain",
                    HotelType = "Business Hotel",
                    StarRating = 5,
                    Policies = "No smoking in rooms.",
                    CancellationPolicy = "Free cancellation within 24 hours.",
                    LanguagesSpoken = "English, Spanish",
                    TotalReviews = 1200,
                    TotalBookings = 5000,
                    PaymentOptions = "Credit Card, Cash",
                    AcceptsCreditCards = true,
                    CreatedAt = DateTime.Now.AddYears(-10),
                    UpdatedAt = DateTime.Now,
                    Email = "info@grandhotel.com",
                    Phone = "+1-555-0123"
                },
                new Hotel
                {
                    Name = "Seaside Resort",
                    Description = "Beautiful beachfront resort",
                    Address = "456 Ocean Dr",
                    City = "Miami",
                    Country = "USA",
                    Rating = 4.8m,
                    ImageUrl = "/images/hotels/seaside-resort.jpg",
                    HasFreeWifi = true,
                    HasParking = true,
                    HasSwimmingPool = true,
                    HasGym = false,
                    HasRestaurant = true,
                    HasRoomService = true,
                    HasBar = true,
                    HasAirportShuttle = true,
                    HasFitnessCenter = false,
                    HasSpa = true,
                    HasBathTub = true,
                    IsPetFriendly = true,
                    HasBusinessFacilities = true,
                    HasChildrenActivities = true,
                    CheckInTime = "15:00",
                    CheckOutTime = "11:00",
                    YearBuilt = 2010,
                    YearRenovated = 2020,
                    HotelChain = "SeasideChain",
                    HotelType = "Resort",
                    StarRating = 5,
                    Policies = "Pets allowed.",
                    CancellationPolicy = "Non-refundable.",
                    LanguagesSpoken = "English, French",
                    TotalReviews = 800,
                    TotalBookings = 3000,
                    PaymentOptions = "Credit Card",
                    AcceptsCreditCards = true,
                    CreatedAt = DateTime.Now.AddYears(-5),
                    UpdatedAt = DateTime.Now,
                    Email = "info@seasideresort.com",
                    Phone = "+1-555-0124"
                }
            };

            context.Hotels.AddRange(hotels);

            // Add sample reviews
            var reviews = new Review[]
            {
                new Review
                {
                    HotelId = 1,
                    Rating = 5,
                    Comment = "Excellent service and beautiful rooms!",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new Review
                {
                    HotelId = 1,
                    Rating = 4,
                    Comment = "Great location and comfortable stay.",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new Review
                {
                    HotelId = 2,
                    Rating = 5,
                    Comment = "Amazing ocean views and friendly staff!",
                    CreatedAt = DateTime.Now.AddDays(-3)
                }
            };

            context.Reviews.AddRange(reviews);

            // Add sample bookings
            var bookings = new Booking[]
            {
                new Booking
                {
                    RoomId = 1,
                    GuestName = "John Doe",
                    GuestEmail = "john@example.com",
                    GuestPhone = "123-456-7890",
                    CheckInDate = DateTime.Now.AddDays(5),
                    CheckOutDate = DateTime.Now.AddDays(7),
                    TotalPrice = 400,
                    Status = "Confirmed",
                    SpecialRequests = "Early check-in if possible"
                },
                new Booking
                {
                    RoomId = 3,
                    GuestName = "Jane Smith",
                    GuestEmail = "jane@example.com",
                    GuestPhone = "987-654-3210",
                    CheckInDate = DateTime.Now.AddDays(10),
                    CheckOutDate = DateTime.Now.AddDays(15),
                    TotalPrice = 1250,
                    Status = "Confirmed"
                }
            };

            context.Bookings.AddRange(bookings);

            // Add sample rooms
            var rooms = new Room[]
            {
                new Room
                {
                    HotelId = 1,
                    RoomNumber = "101",
                    RoomType = "Deluxe",
                    Description = "Spacious deluxe room with city view.",
                    Price = 250m,
                    DiscountedPrice = 200m,
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
                    MainImageUrl = "/images/rooms/deluxe-101.jpg",
                    CancellationPolicy = "Free cancellation within 24 hours.",
                    BreakfastPolicy = "Included",
                    CreatedAt = DateTime.Now.AddMonths(-6),
                    UpdatedAt = DateTime.Now
                },
                new Room
                {
                    HotelId = 2,
                    RoomNumber = "201",
                    RoomType = "Suite",
                    Description = "Luxury suite with ocean view.",
                    Price = 400m,
                    DiscountedPrice = 350m,
                    MaxOccupancy = 4,
                    AdultCapacity = 2,
                    ChildCapacity = 2,
                    IsAvailable = true,
                    IsActive = true,
                    IsFeatured = true,
                    SquareMeters = 60,
                    BedType = "Queen",
                    BedCount = 2,
                    ViewType = "Ocean",
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
                    MainImageUrl = "/images/rooms/suite-201.jpg",
                    CancellationPolicy = "Non-refundable.",
                    BreakfastPolicy = "Available for fee",
                    CreatedAt = DateTime.Now.AddMonths(-3),
                    UpdatedAt = DateTime.Now
                }
            };

            context.Rooms.AddRange(rooms);

            // Add sample offers
            var offers = new Offer[]
            {
                new Offer
                {
                    Title = "Summer Special",
                    Description = "Get 20% off on all bookings",
                    DiscountPercentage = 20,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    IsActive = true,
                    ImageUrl = "/images/offers/summer-special.jpg",
                    PromoCode = "SUMMER20"
                }
            };

            context.Offers.AddRange(offers);

            context.SaveChanges();
        }
    }
} 