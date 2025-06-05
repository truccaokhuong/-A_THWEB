using System.ComponentModel.DataAnnotations;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.Validation;

namespace TH_WEB.Areas.Attractions.ViewModels
{
    public class AttractionCreateViewModel
    {
        [Required(ErrorMessage = "Tên điểm tham quan là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên điểm tham quan không được vượt quá 200 ký tự")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Vị trí là bắt buộc")]
        [StringLength(500, ErrorMessage = "Vị trí không được vượt quá 500 ký tự")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        [ValidAttractionCategory]
        public AttractionCategory Category { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public AttractionStatus Status { get; set; }

        public OperatingHours OperatingHours { get; set; } = null!;

        [ValidImageUrl]
        public string ImageUrl { get; set; } = null!;

        public bool IsFeatured { get; set; }

        [ValidLatitude]
        public double? Latitude { get; set; }

        [ValidLongitude]
        public double? Longitude { get; set; }
    }
} 