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
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string DestinationCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [StringLength(100)]
        public string Region { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AdultPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ChildPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal InfantPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int MaxInfants { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; }
        public int Priority { get; set; }
        public PackageStatus Status { get; set; } = PackageStatus.Active;

        [StringLength(200)]
        public string ImageUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public int? HotelId { get; set; }
        public int? CarRentalId { get; set; }

        // Navigation properties
        [ForeignKey("HotelId")]
        public virtual Hotel? Hotel { get; set; }

        [ForeignKey("CarRentalId")]
        public virtual CarRental? CarRental { get; set; }

        public virtual ICollection<PackageBooking> Bookings { get; set; } = new List<PackageBooking>();

        // Additional properties for views
        public string Title => Name;
        public string Destination => $"{City}, {Country}";
        public string City { get; set; } = string.Empty;
        public int Duration => (EndDate - StartDate).Days;
        public decimal BasePrice => TotalPrice;
        public decimal DiscountPercentage { get; set; }
        public decimal FinalPrice => TotalPrice * (1 - DiscountPercentage / 100);
        public int NumberOfNights => Duration;

        // Package features
        public bool IncludesHotel { get; set; }
        public bool IncludesBreakfast { get; set; }
        public bool IncludesMeals { get; set; }
        public bool IncludesTransfers { get; set; }
        public bool IncludesTours { get; set; }
        public bool IncludesInsurance { get; set; }

        // Additional collections
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<PackageItinerary> Itinerary { get; set; } = new List<PackageItinerary>();
        public virtual ICollection<PackageExtra> Extras { get; set; } = new List<PackageExtra>();
        public virtual ICollection<PackageFAQ> FAQs { get; set; } = new List<PackageFAQ>();
    }

    public class PackageBooking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfInfants { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PackageId")]
        public virtual TravelPackage TravelPackage { get; set; } = null!;
    }

    public class PackageItinerary
    {
        [Key]
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int DayNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        [ForeignKey("PackageId")]
        public virtual TravelPackage Package { get; set; } = null!;
    }

    public class PackageExtra
    {
        [Key]
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [ForeignKey("PackageId")]
        public virtual TravelPackage Package { get; set; } = null!;
    }

    public class PackageFAQ
    {
        [Key]
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;

        [ForeignKey("PackageId")]
        public virtual TravelPackage Package { get; set; } = null!;
    }
} 