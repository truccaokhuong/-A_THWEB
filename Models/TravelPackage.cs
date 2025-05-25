// Models/TravelPackage.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class TravelPackage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public PackageType Type { get; set; } = PackageType.FlightHotel;

        [StringLength(100)]
        public string Destination { get; set; } = string.Empty;

        [StringLength(100)]
        public string DepartureCity { get; set; } = string.Empty;

        public int Duration { get; set; } // số ngày

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountPercentage { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FinalPrice { get; set; }

        // Thông tin chuyến bay
        public int? OutboundFlightId { get; set; }
        public int? ReturnFlightId { get; set; }

        // Thông tin khách sạn
        public int? HotelId { get; set; }
        public int NumberOfNights { get; set; }

        // Thông tin xe thuê (tùy chọn)
        public int? CarRentalId { get; set; }
        public bool IncludesCarRental { get; set; } = false;

        // Bao gồm trong gói
        public bool IncludesFlights { get; set; } = true;
        public bool IncludesHotel { get; set; } = true;
        public bool IncludesBreakfast { get; set; } = false;
        public bool IncludesMeals { get; set; } = false;
        public bool IncludesTransfers { get; set; } = false;
        public bool IncludesTours { get; set; } = false;
        public bool IncludesInsurance { get; set; } = false;

        [StringLength(1000)]
        public string Inclusions { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Exclusions { get; set; } = string.Empty;

        // Ngày có hiệu lực
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        // Điều kiện đặt chỗ
        public int MinimumStay { get; set; } = 1;
        public int MaximumStay { get; set; } = 365;
        public int AdvanceBookingDays { get; set; } = 1;

        [StringLength(500)]
        public string CancellationPolicy { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;

        public int MaxGuests { get; set; } = 4;
        public int MinGuests { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("OutboundFlightId")]
        public virtual Flight? OutboundFlight { get; set; }

        [ForeignKey("ReturnFlightId")]
        public virtual Flight? ReturnFlight { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel? Hotel { get; set; }

        [ForeignKey("CarRentalId")]
        public virtual CarRental? CarRental { get; set; }

        public virtual ICollection<PackageBooking> Bookings { get; set; } = new List<PackageBooking>();
        public virtual ICollection<PackageImage> Images { get; set; } = new List<PackageImage>();
        public virtual ICollection<PackageItinerary> Itinerary { get; set; } = new List<PackageItinerary>();
    }

    public class PackageBooking
    {
        [Key]
        public int Id { get; set; }

        public int TravelPackageId { get; set; }

        [StringLength(100)]
        public string? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string LeadPassengerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string LeadPassengerEmail { get; set; } = string.Empty;

        [StringLength(20)]
        public string LeadPassengerPhone { get; set; } = string.Empty;

        [Required]
        public DateTime TravelStartDate { get; set; }

        [Required]
        public DateTime TravelEndDate { get; set; }

        public int NumberOfAdults { get; set; } = 1;
        public int NumberOfChildren { get; set; } = 0;
        public int NumberOfInfants { get; set; } = 0;

        public int TotalPassengers => NumberOfAdults + NumberOfChildren + NumberOfInfants;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PackagePrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ExtrasPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TaxesAndFees { get; set;