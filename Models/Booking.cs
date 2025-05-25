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

        public int HotelId { get; set; }

        public int RoomId { get; set; }

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

        public int NumberOfGuests { get; set; }

        public int NumberOfRooms { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending";

        [StringLength(500)]
        public string SpecialRequests { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Thông tin thanh toán
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [StringLength(100)]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(100)]
        public string TransactionId { get; set; } = string.Empty;

        public bool IsFullyPaid { get; set; } = false;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountPaid { get; set; } = 0;

        // Thông tin quốc tịch và giấy tờ
        [StringLength(100)]
        public string Nationality { get; set; } = string.Empty;

        [StringLength(50)]
        public string IdentificationType { get; set; } = string.Empty;

        [StringLength(50)]
        public string IdentificationNumber { get; set; } = string.Empty;

        // Thông tin hủy đặt phòng
        public bool IsCancelled { get; set; } = false;

        public DateTime? CancellationDate { get; set; }

        [StringLength(500)]
        public string CancellationReason { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RefundAmount { get; set; }

        // Thông tin check-in, check-out thực tế
        public DateTime? ActualCheckInDate { get; set; }

        public DateTime? ActualCheckOutDate { get; set; }

        // Thông tin bổ sung
        [StringLength(200)]
        public string BookingSource { get; set; } = "Website";

        public bool HasReviewed { get; set; } = false;

        // Mối quan hệ
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; } = null!;

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        public virtual ICollection<BookingAddon> Addons { get; set; } = new List<BookingAddon>();

        // Aliases for backward compatibility
        public DateTime CheckIn => CheckInDate;
        public DateTime CheckOut => CheckOutDate;
        public DateTime BookingDate => CreatedAt;
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        CheckedIn,
        CheckedOut,
        Cancelled,
        NoShow
    }

    public enum PaymentStatus
    {
        Pending,
        Authorized,
        Paid,
        PartiallyPaid,
        Refunded,
        PartiallyRefunded,
        Failed
    }

    public class BookingAddon
    {
        [Key]
        public int Id { get; set; }

        public int BookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;
    }

    public class Offer
    {
        [Key]
        public int Id { get; set; }

        public int HotelId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(0, 100)]
        public int DiscountPercentage { get; set; } = 0;

        // Alias for backward compatibility
        public int DiscountPercent => DiscountPercentage;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DiscountAmount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? MinimumStay { get; set; } // Số đêm tối thiểu

        [StringLength(100)]
        public string PromoCode { get; set; } = string.Empty;

        [StringLength(500)]
        public string TermsAndConditions { get; set; } = string.Empty;

        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public bool IsExclusive { get; set; } = false;

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; } = null!;
    }
}