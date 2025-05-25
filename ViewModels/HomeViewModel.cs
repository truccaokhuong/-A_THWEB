// ViewModels/HomeViewModel.cs
using System;
using System.Collections.Generic;
using TH_WEB.Models;
using TH_WEB.Controllers;

namespace ViewModels
{
    public class HomeViewModel
    {
        public List<Hotel> FeaturedHotels { get; set; } = new List<Hotel>();
        public List<Booking> RecentBookings { get; set; } = new List<Booking>();
        public List<Hotel> PopularDestinations { get; set; } = new List<Hotel>();
        public List<Offer> Offers { get; set; } = new List<Offer>();
        public List<Hotel> TopDestinations { get; set; } = new List<Hotel>();
        public List<QuickDestination> QuickDestinations { get; set; } = new List<QuickDestination>();
        
        // Search parameters
        public string SearchDestination { get; set; }
        public DateTime CheckInDate { get; set; } = DateTime.Now.AddDays(1);
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(2);
        public int Guests { get; set; } = 2;
        
        // Statistics
        public int TotalHotels { get; set; }
        public int TotalBookings { get; set; }
        public int HappyCustomers { get; set; }
        public int CountriesServed { get; set; }
    }

    public class SearchViewModel
    {
        public string Destination { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Guests { get; set; } = 2;
        public int Rooms { get; set; } = 1;
        public string SearchType { get; set; } = "stays"; // stays, flights
    }
}