using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TH_WEB.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string City { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string PostalCode { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Region { get; set; } = string.Empty;
        
        [Range(0, 5)]
        [Column(TypeName = "decimal(3, 1)")]
        public decimal Rating { get; set; }
        
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        // Thông tin liên hệ
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;
        
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string Website { get; set; } = string.Empty;
        
        // Giá và đặt phòng
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerNight { get; set; }
        
        public int TotalRooms { get; set; }
        
        public int AvailableRooms { get; set; }
        
        // Thông tin vị trí địa lý
        [Column(TypeName = "decimal(10, 7)")]
        public decimal? Latitude { get; set; }
        
        [Column(TypeName = "decimal(10, 7)")]
        public decimal? Longitude { get; set; }
        
        // Tiện nghi cơ bản
        public bool HasFreeWifi { get; set; }
        public bool HasParking { get; set; }
        public bool HasSwimmingPool { get; set; }
        public bool HasGym { get; set; }
        public bool HasRestaurant { get; set; }
        public bool HasRoomService { get; set; }
        public bool HasBar { get; set; }
        public bool HasAirportShuttle { get; set; }
        public bool HasFitnessCenter { get; set; }
        public bool HasSpa { get; set; }
        public bool HasBathTub { get; set; }
        public bool IsPetFriendly { get; set; }
        public bool HasBusinessFacilities { get; set; }
        public bool HasChildrenActivities { get; set; }
        public bool HasBreakfast { get; set; }
        
        // Thông tin check-in/check-out
        [StringLength(20)]
        public string CheckInTime { get; set; } = "14:00";
        
        [StringLength(20)]
        public string CheckOutTime { get; set; } = "12:00";
        
        // Thông tin bổ sung
        public int? YearBuilt { get; set; }
        public int? YearRenovated { get; set; }
        [StringLength(50)]
        public string HotelChain { get; set; } = string.Empty;
        [StringLength(50)]
        public string HotelType { get; set; } = string.Empty; // Resort, Business Hotel, Boutique, etc.
        public int? StarRating { get; set; } // Số sao chính thức (1-5)
        [StringLength(1000)]
        public string Policies { get; set; } = string.Empty;
        [StringLength(1000)]
        public string CancellationPolicy { get; set; } = string.Empty;
        
        // Thông tin ngôn ngữ
        [StringLength(200)]
        public string LanguagesSpoken { get; set; } = string.Empty;
        
        // Số liệu thống kê
        public int TotalReviews { get; set; }
        public int TotalBookings { get; set; }
        
        // Thông tin tài chính
        [StringLength(100)]
        public string PaymentOptions { get; set; } = string.Empty;
        public bool AcceptsCreditCards { get; set; }
        
        // Thời gian
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        
        // Mối quan hệ
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public virtual ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();
        public virtual ICollection<HotelFacility> Facilities { get; set; } = new List<HotelFacility>();

        // Computed property for lowest price
        public decimal LowestPrice => Rooms?.Min(r => r.Price) ?? 0;
    }

    public class HotelImage
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; } = null!;
    }

    public class HotelFacility
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [StringLength(100)]
        public string Category { get; set; } = string.Empty; // General, Dining, Wellness, etc.
        [StringLength(50)]
        public string Icon { get; set; } = string.Empty;

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; } = null!;
    }
}