using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TH_WEB.Areas.Attractions.Validation;

namespace TH_WEB.Areas.Attractions.Models
{
    public class Attraction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên điểm tham quan là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên điểm tham quan không được vượt quá 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        [ValidAttractionCategory]
        public AttractionCategory Category { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Thành phố là bắt buộc")]
        [StringLength(100, ErrorMessage = "Thành phố không được vượt quá 100 ký tự")]
        public string City { get; set; }

        [Required(ErrorMessage = "Tỉnh/Thành phố là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tỉnh/Thành phố không được vượt quá 100 ký tự")]
        public string State { get; set; }

        [Required(ErrorMessage = "Quốc gia là bắt buộc")]
        [StringLength(100, ErrorMessage = "Quốc gia không được vượt quá 100 ký tự")]
        public string Country { get; set; }

        [ValidLatitude]
        public double? Latitude { get; set; }

        [ValidLongitude]
        public double? Longitude { get; set; }

        [StringLength(20, ErrorMessage = "Mã bưu điện không được vượt quá 20 ký tự")]
        public string ZipCode { get; set; }

        [ValidPhoneNumber]
        public string Phone { get; set; }

        [ValidEmail]
        public string Email { get; set; }

        [ValidUrl]
        public string Website { get; set; }

        public OperatingHours OperatingHours { get; set; }

        public Pricing Pricing { get; set; }

        public ICollection<AttractionImage> Images { get; set; }

        public ICollection<AttractionFeature> Features { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        [ValidRating]
        public decimal Rating { get; set; }

        public int ReviewCount { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<AttractionTag> Tags { get; set; }

        [Required]
        public AttractionStatus Status { get; set; }

        public bool IsVerified { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public string CreatedById { get; set; }

        public int ViewCount { get; set; }

        public int FavoriteCount { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsFeatured { get; set; }
    }

    public class OperatingHours
    {
        [Key]
        public int Id { get; set; }

        public int AttractionId { get; set; }

        [ForeignKey("AttractionId")]
        public Attraction Attraction { get; set; }

        [ValidBusinessHours]
        public string Monday { get; set; }

        [ValidBusinessHours]
        public string Tuesday { get; set; }

        [ValidBusinessHours]
        public string Wednesday { get; set; }

        [ValidBusinessHours]
        public string Thursday { get; set; }

        [ValidBusinessHours]
        public string Friday { get; set; }

        [ValidBusinessHours]
        public string Saturday { get; set; }

        [ValidBusinessHours]
        public string Sunday { get; set; }

        public static OperatingHours Parse(string operatingHours)
        {
            var hours = new OperatingHours();
            var days = operatingHours.Split(';');
            
            foreach (var day in days)
            {
                var parts = day.Split(':');
                if (parts.Length == 2)
                {
                    var dayName = parts[0].Trim();
                    var hoursStr = parts[1].Trim();

                    switch (dayName.ToLower())
                    {
                        case "monday":
                            hours.Monday = hoursStr;
                            break;
                        case "tuesday":
                            hours.Tuesday = hoursStr;
                            break;
                        case "wednesday":
                            hours.Wednesday = hoursStr;
                            break;
                        case "thursday":
                            hours.Thursday = hoursStr;
                            break;
                        case "friday":
                            hours.Friday = hoursStr;
                            break;
                        case "saturday":
                            hours.Saturday = hoursStr;
                            break;
                        case "sunday":
                            hours.Sunday = hoursStr;
                            break;
                    }
                }
            }

            return hours;
        }

        public override string ToString()
        {
            var days = new[]
            {
                $"Monday:{Monday}",
                $"Tuesday:{Tuesday}",
                $"Wednesday:{Wednesday}",
                $"Thursday:{Thursday}",
                $"Friday:{Friday}",
                $"Saturday:{Saturday}",
                $"Sunday:{Sunday}"
            };

            return string.Join(";", days);
        }
    }

    public class Pricing
    {
        [Key]
        public int Id { get; set; }

        public int AttractionId { get; set; }

        [ForeignKey("AttractionId")]
        public Attraction Attraction { get; set; }

        public bool IsFree { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AdultPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ChildPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SeniorPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? StudentPrice { get; set; }

        [StringLength(3)]
        public string Currency { get; set; } = "VND";
    }

    public class AttractionImage
    {
        [Key]
        public int Id { get; set; }

        public int AttractionId { get; set; }

        [ForeignKey("AttractionId")]
        public Attraction Attraction { get; set; }

        [Required]
        [ValidImageUrl]
        public string Url { get; set; }

        [StringLength(200)]
        public string Caption { get; set; }

        public bool IsPrimary { get; set; }
    }

    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int AttractionId { get; set; }

        [ForeignKey("AttractionId")]
        public Attraction Attraction { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

        public int HelpfulCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    public class AttractionFeature
    {
        [Key]
        public int Id { get; set; }

        public int AttractionId { get; set; }

        [ForeignKey("AttractionId")]
        public Attraction Attraction { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string IconClass { get; set; }
    }

    public class AttractionTag
    {
        [Key]
        public int Id { get; set; }

        public int AttractionId { get; set; }

        [ForeignKey("AttractionId")]
        public Attraction Attraction { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
} 