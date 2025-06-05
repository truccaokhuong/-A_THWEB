// Models/Booking.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        public int? HotelId { get; set; }

        [StringLength(100)]
        public string? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string GuestEmail { get; set; } = string.Empty;

        [StringLength(20)]
        public string GuestPhone { get; set; } = string.Empty;

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfInfants { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [StringLength(100)]
        public string BookingReference { get; set; } = string.Empty;

        [StringLength(1000)]
        public string SpecialRequests { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        [ForeignKey("HotelId")]
        public virtual Hotel? Hotel { get; set; }

        public virtual ICollection<BookingAmenity> Amenities { get; set; } = new List<BookingAmenity>();

        public virtual ICollection<BookingService> Services { get; set; } = new List<BookingService>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Aliases for backward compatibility
        public DateTime CheckIn => CheckInDate;
        public DateTime CheckOut => CheckOutDate;
        public DateTime BookingDate => CreatedAt;
    }

    public class BookingAmenity
    {
        [Key]
        public int Id { get; set; }

        public int BookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;
    }

    public class BookingService
    {
        [Key]
        public int Id { get; set; }

        public int BookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;
    }
}