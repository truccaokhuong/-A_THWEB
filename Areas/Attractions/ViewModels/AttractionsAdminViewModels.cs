using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TH_WEB.Areas.Attractions.Models;

namespace TH_WEB.Areas.Attractions.ViewModels
{
    public class AttractionsIndexViewModel
    {
        public IEnumerable<Attraction> Attractions { get; set; }
        public AttractionsSearchViewModel SearchModel { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class EditAttractionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên điểm tham quan là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên điểm tham quan không được vượt quá 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc")]
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

        public string OperatingHours { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool IsFeatured { get; set; }

        public string ImageUrl { get; set; }

        public string CurrentImageUrl { get; set; }
    }

    public class AttractionsStatisticsViewModel
    {
        public int TotalCount { get; set; }
        public decimal AveragePrice { get; set; }
        public IEnumerable<CategoryStat> CategoryStats { get; set; }
        public IEnumerable<StatusStat> StatusStats { get; set; }
    }

    public class CategoryStat
    {
        public string Category { get; set; }
        public int Count { get; set; }
    }

    public class StatusStat
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }
} 