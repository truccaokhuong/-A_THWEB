using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public int HotelId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountedPrice { get; set; }

        public int MaxOccupancy { get; set; }

        public int AdultCapacity { get; set; }
        
        public int ChildCapacity { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; }

        public bool IsFeatured { get; set; }

        // Thông tin phòng
        public int SquareMeters { get; set; }

        [StringLength(50)]
        public string BedType { get; set; } = string.Empty;

        public int BedCount { get; set; }

        [StringLength(50)]
        public string ViewType { get; set; } = string.Empty;

        [StringLength(50)]
        public string Floor { get; set; } = string.Empty;

        // Tiện nghi phòng
        public bool HasPrivateBathroom { get; set; }
        
        public bool HasAirConditioning { get; set; }
        
        public bool HasTV { get; set; }
        
        public bool HasWifi { get; set; }
        
        public bool HasMinibar { get; set; }
        
        public bool HasSafe { get; set; }
        
        public bool HasWorkDesk { get; set; }
        
        public bool HasHairDryer { get; set; }
        
        public bool HasBalcony { get; set; }
        
        public bool IsNonSmoking { get; set; }
        
        public bool HasRoomService { get; set; }
        
        public bool HasIron { get; set; }
        
        public bool HasCoffeemaker { get; set; }
        
        public bool HasBathtub { get; set; }
        
        public bool HasShower { get; set; }

        // Thông tin hình ảnh
        [StringLength(500)]
        public string MainImageUrl { get; set; } = string.Empty;

        // Thông tin chính sách
        [StringLength(500)]
        public string CancellationPolicy { get; set; } = string.Empty;

        [StringLength(500)]
        public string BreakfastPolicy { get; set; } = string.Empty;

        // Thông tin kiểm soát
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Mối quan hệ
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        
        public virtual ICollection<RoomImage> Images { get; set; } = new List<RoomImage>();
        
        public virtual ICollection<RoomAmenity> Amenities { get; set; } = new List<RoomAmenity>();

        // Aliases for backward compatibility
        public decimal PricePerNight => Price;
        public string Name => RoomType;
        public string ImageUrl => MainImageUrl;
        public int Capacity => MaxOccupancy;
        public bool HasFlatScreenTV => HasTV;
    }

    public class RoomImage
    {
        [Key]
        public int Id { get; set; }
        
        public int RoomId { get; set; }
        
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;
        
        public bool IsPrimary { get; set; } = false;
        
        public int SortOrder { get; set; } = 0;

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }

    public class RoomAmenity
    {
        [Key]
        public int Id { get; set; }
        
        public int RoomId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Icon { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}