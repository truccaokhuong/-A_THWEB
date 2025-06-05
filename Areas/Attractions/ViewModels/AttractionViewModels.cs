using System.ComponentModel.DataAnnotations;
using TH_WEB.Areas.Attractions.Models;

namespace TH_WEB.Areas.Attractions.ViewModels
{
    public class CreateAttractionViewModel
    {
        [Required(ErrorMessage = "Tên điểm tham quan là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên điểm tham quan không được vượt quá 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Địa điểm là bắt buộc")]
        [StringLength(500, ErrorMessage = "Địa điểm không được vượt quá 500 ký tự")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        public AttractionCategory Category { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public AttractionStatus Status { get; set; }

        public OperatingHours OperatingHours { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool IsFeatured { get; set; }

        public string ImageUrl { get; set; }
    }

    public class UpdateAttractionViewModel : CreateAttractionViewModel
    {
        public int Id { get; set; }
    }

    public class BatchUpdateStatusViewModel
    {
        [Required]
        public List<int> Ids { get; set; }

        [Required]
        public AttractionStatus Status { get; set; }
    }

    public class BatchDeleteViewModel
    {
        [Required]
        public List<int> Ids { get; set; }
    }
} 