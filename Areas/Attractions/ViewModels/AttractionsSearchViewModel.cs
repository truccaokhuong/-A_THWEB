using System.ComponentModel.DataAnnotations;
using TH_WEB.Areas.Attractions.Models;

namespace TH_WEB.Areas.Attractions.ViewModels
{
    public class AttractionsSearchViewModel
    {
        public string Keyword { get; set; }

        [EnumDataType(typeof(AttractionCategory))]
        public AttractionCategory? Category { get; set; }

        public string Location { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MinPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MaxPrice { get; set; }

        [EnumDataType(typeof(AttractionStatus))]
        public AttractionStatus? Status { get; set; }

        public string SortBy { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 12;
    }
} 