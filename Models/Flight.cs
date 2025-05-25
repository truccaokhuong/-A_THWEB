// Models/Flight.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string AirlineName { get; set; } = string.Empty;

        [StringLength(10)]
        public string AirlineCode { get; set; } = string.Empty;

        [StringLength(500)]
        public string AirlineLogo { get; set; } = string.Empty;

        // Airport thông tin
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }

        // Thời gian bay
        public TimeSpan Duration => ArrivalDateTime - DepartureDateTime;

        // Giá vé theo hạng
        [Column(TypeName = "decimal(18, 2)")]
        public decimal EconomyPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PremiumEconomyPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BusinessPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FirstClassPrice { get; set; }

        // Số ghế có sẵn
        public int EconomySeatsAvailable { get; set; }
        public int PremiumEconomySeatsAvailable { get; set; }
        public int BusinessSeatsAvailable { get; set; }
        public int FirstClassSeatsAvailable { get; set; }

        // Tổng số ghế
        public int TotalEconomySeats { get; set; }
        public int TotalPremiumEconomySeats { get; set; }
        public int TotalBusinessSeats { get; set; }
        public int TotalFirstClassSeats { get; set; }

        // Thông tin máy bay
        [StringLength(50)]
        public string AircraftType { get; set; } = string.Empty;

        [StringLength(20)]
        public string AircraftRegistration { get; set; } = string.Empty;

        // Trạng thái chuyến bay
        public FlightStatus Status { get; set; } = FlightStatus.Scheduled;

        [StringLength(100)]
        public string Gate { get; set; } = string.Empty;

        [StringLength(100)]
        public string Terminal { get; set; } = string.Empty;

        // Thông tin bổ sung
        public bool HasWifi { get; set; }
        public bool HasMeals { get; set; }
        public bool HasEntertainment { get; set; }
        public bool HasPowerOutlets { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        // Thông tin chuyến bay khứ hồi
        public int? ReturnFlightId { get; set; }
        public bool IsRoundTrip { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        [ForeignKey("DepartureAirportId")]
        public virtual Airport DepartureAirport { get; set; } = null!;

        [ForeignKey("ArrivalAirportId")]
        public virtual Airport ArrivalAirport { get; set; } = null!;

        [ForeignKey("ReturnFlightId")]
        public virtual Flight? ReturnFlight { get; set; }

        public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();
        public virtual ICollection<FlightSeat> Seats { get; set; } = new List<FlightSeat>();
    }

    public class Airport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string IATACode { get; set; } = string.Empty; // VD: JFK, LAX

        [Required]
        [StringLength(10)]
        public string ICAOCode { get; set; } = string.Empty; // VD: KJFK, KLAX

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [StringLength(100)]
        public string Region { get; set; } = string.Empty;

        [StringLength(50)]
        public string TimeZone { get; set; } = string.Empty;

        // Vị trí địa lý
        [Column(TypeName = "decimal(10, 7)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(10, 7)")]
        public decimal Longitude { get; set; }

        public int Elevation { get; set; } // Độ cao (feet)

        // Thông tin sân bay
        public int NumberOfTerminals { get; set; }
        public int NumberOfRunways { get; set; }

        [StringLength(500)]
        public string Website { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        // Tiện nghi sân bay
        public bool HasCarRental { get; set; }
        public bool HasHotelShuttle { get; set; }
        public bool HasPublicTransport { get; set; }
        public bool HasWifi { get; set; }
        public bool HasDutyFree { get; set; }
        public bool HasRestaurants { get; set; }
        public bool HasLounges { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsInternational { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();
        public virtual ICollection<Flight> ArrivalFlights { get; set; } = new List<Flight>();
        public virtual ICollection<CarRental> CarRentals { get; set; } = new List<CarRental>();
    }

    public class FlightBooking
    {
        [Key]
        public int Id { get; set; }

        public int FlightId { get; set; }

        [StringLength(100)]
        public string? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string PassengerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string PassengerEmail { get; set; } = string.Empty;

        [StringLength(20)]
        public string PassengerPhone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(100)]
        public string Nationality { get; set; } = string.Empty;

        [StringLength(50)]
        public string PassportNumber { get; set; } = string.Empty;

        public DateTime? PassportExpiry { get; set; }

        [Required]
        public FlightClass FlightClass { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TicketPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TaxesAndFees { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [StringLength(10)]
        public string SeatNumber { get; set; } = string.Empty;

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [StringLength(100)]
        public string BookingReference { get; set; } = string.Empty;

        [StringLength(100)]
        public string ETicketNumber { get; set; } = string.Empty;

        // Check-in thông tin
        public bool IsCheckedIn { get; set; } = false;
        public DateTime? CheckInTime { get; set; }

        // Hành lý
        public bool HasCheckedBaggage { get; set; } = false;
        public int CheckedBaggageWeight { get; set; } = 0;
        public int HandBaggageCount { get; set; } = 1;

        // Dịch vụ bổ sung
        public bool HasMealPreference { get; set; } = false;
        [StringLength(100)]
        public string MealPreference { get; set; } = string.Empty;

        public bool HasSpecialAssistance { get; set; } = false;
        [StringLength(500)]
        public string SpecialAssistanceDetails { get; set; } = string.Empty;

        [StringLength(500)]
        public string SpecialRequests { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; } = null!;

        public virtual ICollection<FlightPassenger> Passengers { get; set; } = new List<FlightPassenger>();
    }

    public class FlightPassenger
    {
        [Key]
        public int Id { get; set; }

        public int FlightBookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(100)]
        public string Nationality { get; set; } = string.Empty;

        [StringLength(50)]
        public string PassportNumber { get; set; } = string.Empty;

        public DateTime? PassportExpiry { get; set; }

        [StringLength(10)]
        public string SeatNumber { get; set; } = string.Empty;

        public PassengerType Type { get; set; } = PassengerType.Adult;

        [StringLength(100)]
        public string FrequentFlyerNumber { get; set; } = string.Empty;

        [ForeignKey("FlightBookingId")]
        public virtual FlightBooking FlightBooking { get; set; } = null!;
    }

    public class FlightSeat
    {
        [Key]
        public int Id { get; set; }

        public int FlightId { get; set; }

        [Required]
        [StringLength(10)]
        public string SeatNumber { get; set; } = string.Empty;

        public FlightClass SeatClass { get; set; }

        public SeatType Type { get; set; } = SeatType.Standard;

        public bool IsAvailable { get; set; } = true;

        public bool IsWindowSeat { get; set; } = false;

        public bool IsAisleSeat { get; set; } = false;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ExtraFee { get; set; } = 0;

        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; } = null!;
    }

    // Enums
    public enum FlightStatus
    {
        Scheduled,
        Delayed,
        Boarding,
        Departed,
        InFlight,
        Arrived,
        Cancelled,
        Diverted
    }

    public enum FlightClass
    {
        Economy,
        PremiumEconomy,
        Business,
        FirstClass
    }

    public enum PassengerType
    {
        Adult,
        Child,
        Infant
    }

    public enum SeatType
    {
        Standard,
        ExtraLegroom,
        Emergency,
        Premium,
        Blocked
    }
}