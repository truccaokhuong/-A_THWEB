// Models/Attractions.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class Attractions
    {
        [Key]
        public int AttractionId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(2000)]
        public string DetailedDescription { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Column(TypeName = "decimal(10,8)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(11,8)")]
        public decimal Longitude { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } // Museum, Theme Park, Historical Site, etc.

        [Column(TypeName = "decimal(10,2)")]
        public decimal Rating { get; set; }

        public int ReviewCount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PriceFrom { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [StringLength(2000)]
        public string ImageGallery { get; set; } // JSON array of image URLs

        public string OpeningHours { get; set; } // JSON format

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<AttractionBookings> AttractionBookings { get; set; }
        public virtual ICollection<AttractionReviews> AttractionReviews { get; set; }
        public virtual ICollection<AttractionFeatures> AttractionFeatures { get; set; }
    }

    public class AttractionBookings
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int AttractionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string BookingReference { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, Confirmed, Cancelled, Completed

        [StringLength(500)]
        public string SpecialRequests { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [StringLength(20)]
        public string CustomerPhone { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public DateTime? CancellationDate { get; set; }

        [StringLength(500)]
        public string CancellationReason { get; set; }

        // Navigation properties
        public virtual Attractions Attraction { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class AttractionReviews
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int AttractionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public bool IsVerified { get; set; } = false;

        public int HelpfulCount { get; set; } = 0;

        // Navigation properties
        public virtual Attractions Attraction { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class AttractionFeatures
    {
        [Key]
        public int FeatureId { get; set; }

        [Required]
        public int AttractionId { get; set; }

        [Required]
        [StringLength(100)]
        public string FeatureName { get; set; }

        [StringLength(200)]
        public string FeatureDescription { get; set; }

        [StringLength(50)]
        public string IconClass { get; set; }

        // Navigation properties
        public virtual Attractions Attraction { get; set; }
    }

    public class AttractionCategories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string IconClass { get; set; }

        public bool IsActive { get; set; } = true;
    }
}