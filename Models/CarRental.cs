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
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int CarTypeId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public int PickupLocationId { get; set; }

        [Required]
        public int DropoffLocationId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DailyRate { get; set; }

        public bool IsAvailable { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("CarTypeId")]
        public virtual CarType CarType { get; set; } = null!;

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; } = null!;

        [ForeignKey("PickupLocationId")]
        public virtual Location PickupLocation { get; set; } = null!;

        [ForeignKey("DropoffLocationId")]
        public virtual Location DropoffLocation { get; set; } = null!;

        public virtual ICollection<CarRentalBooking> CarRentalBookings { get; set; } = new List<CarRentalBooking>();
        public virtual ICollection<CarRentalExtra> Extras { get; set; } = new List<CarRentalExtra>();
    }

    public class CarType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public CarCategory Category { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool HasAutomaticTransmission { get; set; }
        public bool HasGPS { get; set; }
        public bool HasBluetooth { get; set; }
        public bool HasUSBPort { get; set; }
        public bool HasChildSeat { get; set; }
        public bool HasLuggageSpace { get; set; }

        public virtual ICollection<CarRental> CarRentals { get; set; } = new List<CarRental>();
    }

    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [StringLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public virtual ICollection<CarRental> CarRentals { get; set; } = new List<CarRental>();
        public virtual ICollection<CarRental> PickupLocations { get; set; } = new List<CarRental>();
        public virtual ICollection<CarRental> DropoffLocations { get; set; } = new List<CarRental>();
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

        public bool IsActive { get; set; } = true;

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
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int RentalDays => (int)(EndDate - StartDate).TotalDays + 1;

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

        [StringLength(500)]
        public string SpecialRequests { get; set; } = string.Empty;

        public bool HasGPS { get; set; } = false;
        public bool HasChildSeat { get; set; } = false;
        public bool HasAdditionalDriver { get; set; } = false;

        [StringLength(100)]
        public string AdditionalDriverName { get; set; } = string.Empty;

        [StringLength(50)]
        public string AdditionalDriverLicense { get; set; } = string.Empty;

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