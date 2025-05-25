// Models/CarRental.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class CarRental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(500)]
        public string CompanyLogo { get; set; } = string.Empty;

        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }

        [Required]
        [StringLength(100)]
        public string CarModel { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string CarBrand { get; set; } = string.Empty;

        [StringLength(50)]
        public string CarYear { get; set; } = string.Empty;

        [StringLength(500)]
        public string CarImage { get; set; } = string.Empty;

        public CarCategory Category { get; set; } = CarCategory.Economy;

        [StringLength(20)]
        public string TransmissionType { get; set; } = string.Empty; // Manual, Automatic

        [StringLength(20)]
        public string FuelType { get; set; } = string.Empty; // Petrol, Diesel, Electric, Hybrid

        public int Seats { get; set; } = 5;
        public int Doors { get; set; } = 4;
        public int LargeBags { get; set; } = 2;
        public int SmallBags { get; set; } = 1;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerDay { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerWeek { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerMonth { get; set; }

        // Deposit và bảo hiểm
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SecurityDeposit { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal InsuranceFee { get; set; }

        // Thông tin xe
        public bool HasAirConditioning { get; set; }
        public bool HasGPS { get; set; }
        public bool HasBluetooth { get; set; }
        public bool HasWifi { get; set; }
        public bool HasChildSeat { get; set; }
        public bool HasUSBCharger { get; set; }

        [StringLength(20)]
        public string MileageLimit { get; set; } = string.Empty; // Unlimited, 200km/day

        public int MinimumAge { get; set; } = 21;
        public int MinimumDrivingExperience { get; set; } = 1; // years

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(500)]
        public string TermsAndConditions { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("PickupLocationId")]
        public virtual RentalLocation PickupLocation { get; set; } = null!;

        [ForeignKey("DropoffLocationId")]
        public virtual RentalLocation DropoffLocation { get; set; } = null!;

        public virtual ICollection<CarRentalBooking> Bookings { get; set; } = new List<CarRentalBooking>();
    }

    public class RentalLocation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [StringLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Vị trí địa lý
        [Column(TypeName = "decimal(10, 7)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(10, 7)")]
        public decimal Longitude { get; set; }

        // Giờ hoạt động
        [StringLength(100)]
        public string OpeningHours { get; set; } = string.Empty;

        public bool IsAirportLocation { get; set; } = false;
        public int? AirportId { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("AirportId")]
        public virtual Airport? Airport { get; set; }

        public virtual ICollection<CarRental> PickupRentals { get; set; } = new List<CarRental>();
        public virtual ICollection<CarRental> DropoffRentals { get; set; } = new List<CarRental>();
    }

    public class CarRentalBooking
    {
        [Key]
        public int Id { get; set; }

        public int CarRentalId { get; set; }

        [StringLength(100)]
        public string? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string DriverName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string DriverEmail { get; set; } = string.Empty;

        [StringLength(20)]
        public string DriverPhone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required]
        public DateTime LicenseExpiry { get; set; }

        [StringLength(100)]
        public string LicenseCountry { get; set; } = string.Empty;

        [Required]
        public DateTime PickupDateTime { get; set; }

        [Required]
        public DateTime DropoffDateTime { get; set; }

        public int RentalDays => (int)(DropoffDateTime - PickupDateTime).TotalDays + 1;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TaxesAndFees { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal InsuranceFee { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ExtrasFee { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SecurityDeposit { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [StringLength(100)]
        public string BookingReference { get; set; } = string.Empty;

        // Thông tin bổ sung
        [StringLength(500)]
        public string SpecialRequests { get; set; } = string.Empty;

        public bool HasGPS { get; set; } = false;
        public bool HasChildSeat { get; set; } = false;
        public bool HasAdditionalDriver { get; set; } = false;

        [StringLength(100)]
        public string AdditionalDriverName { get; set; } = string.Empty;

        [StringLength(50)]
        public string AdditionalDriverLicense { get; set; } = string.Empty;

        // Thông tin xe khi nhận và trả
        [StringLength(1000)]
        public string PickupCondition { get; set; } = string.Empty;

        [StringLength(1000)]
        public string DropoffCondition { get; set; } = string.Empty;

        public int? PickupMileage { get; set; }
        public int? DropoffMileage { get; set; }

        [StringLength(20)]
        public string FuelLevelPickup { get; set; } = string.Empty;

        [StringLength(20)]
        public string FuelLevelDropoff { get; set; } = string.Empty;

        public DateTime? ActualPickupTime { get; set; }
        public DateTime? ActualDropoffTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("CarRentalId")]
        public virtual CarRental CarRental { get; set; } = null!;

        public virtual ICollection<CarRentalExtra> Extras { get; set; } = new List<CarRentalExtra>();
    }

    public class CarRentalExtra
    {
        [Key]
        public int Id { get; set; }

        public int CarRentalBookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerDay { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; } = 1;

        [ForeignKey("CarRentalBookingId")]
        public virtual CarRentalBooking CarRentalBooking { get; set; } = null!;
    }

    // Enums
    public enum CarCategory
    {
        Economy,
        Compact,
        Midsize,
        Fullsize,
        Premium,
        Luxury,
        SUV,
        Van,
        Convertible,
        Truck
    }
}