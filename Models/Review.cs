// Models/Review.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int HotelId { get; set; }

        public int? BookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string ReviewerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ReviewerEmail { get; set; } = string.Empty;

        [Range(0, 5)]
        [Column(TypeName = "decimal(3, 1)")]
        public decimal Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; } = string.Empty;

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Alias for backward compatibility
        public DateTime Date => ReviewDate;

        public bool IsVerified { get; set; }

        public bool IsPublished { get; set; }

        // Ratings chi tiết
        [Range(0, 5)]
        public decimal? CleanlinessRating { get; set; }

        [Range(0, 5)]
        public decimal? ComfortRating { get; set; }

        [Range(0, 5)]
        public decimal? LocationRating { get; set; }

        [Range(0, 5)]
        public decimal? ServiceRating { get; set; }

        [Range(0, 5)]
        public decimal? ValueForMoneyRating { get; set; }

        // Thông tin chuyến đi
        [StringLength(50)]
        public string TripType { get; set; } = string.Empty;

        [StringLength(50)]
        public string TravelerType { get; set; } = string.Empty;

        // Phản hồi từ khách sạn
        [StringLength(1000)]
        public string HotelResponse { get; set; } = string.Empty;

        public DateTime? ResponseDate { get; set; }

        // Mối quan hệ
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; } = null!;

        [ForeignKey("BookingId")]
        public virtual Booking? Booking { get; set; }

        public virtual ICollection<ReviewImage> Images { get; set; } = new List<ReviewImage>();
    }

    public class ReviewImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string Caption { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; } = null!;
    }
}