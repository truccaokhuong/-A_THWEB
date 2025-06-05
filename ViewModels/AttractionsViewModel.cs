// ViewModels/AttractionsViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace TH_WEB.ViewModels
{
    public class AttractionSearchViewModel
    {
        public string Destination { get; set; }
        public DateTime? VisitDate { get; set; }
        public int AdultCount { get; set; } = 2;
        public int ChildCount { get; set; } = 0;
        public string Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinRating { get; set; }
        public string SortBy { get; set; } = "popular"; // popular, rating, price_low, price_high, distance
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }

    public class AttractionListViewModel
    {
        public List<AttractionCardViewModel> Attractions { get; set; }
        public AttractionSearchViewModel SearchCriteria { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<string> Categories { get; set; }
        public List<string> PopularDestinations { get; set; }
    }

    public class AttractionCardViewModel
    {
        public int AttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public decimal PriceFrom { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public List<string> Features { get; set; }
        public string Distance { get; set; }
    }

    public class AttractionDetailViewModel
    {
        public int AttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DetailedDescription { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Category { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public decimal PriceFrom { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageGallery { get; set; }
        public Dictionary<string, string> OpeningHours { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public List<AttractionFeatureViewModel> Features { get; set; }
        public List<AttractionReviewViewModel> Reviews { get; set; }
        public List<AttractionCardViewModel> SimilarAttractions { get; set; }
        public bool CanBook { get; set; }
    }

    public class AttractionFeatureViewModel
    {
        public string FeatureName { get; set; }
        public string FeatureDescription { get; set; }
        public string IconClass { get; set; }
    }

    public class AttractionReviewViewModel
    {
        public int ReviewId { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool IsVerified { get; set; }
        public int HelpfulCount { get; set; }
    }

    public class AttractionBookingViewModel
    {
        public int AttractionId { get; set; }
        public string AttractionName { get; set; }
        public string AttractionImage { get; set; }
        public string AttractionAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Số lượng người lớn phải từ 1 đến 20")]
        public int AdultCount { get; set; } = 2;

        [Range(0, 10, ErrorMessage = "Số lượng trẻ em phải từ 0 đến 10")]
        public int ChildCount { get; set; } = 0;

        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal TotalPrice { get; set; }

        [StringLength(500)]
        public string SpecialRequests { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [StringLength(20)]
        public string CustomerPhone { get; set; }

        public bool AgreeToTerms { get; set; }
    }

    public class AttractionBookingConfirmationViewModel
    {
        public string BookingReference { get; set; }
        public string AttractionName { get; set; }
        public string AttractionAddress { get; set; }
        public DateTime VisitDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Status { get; set; }
        public DateTime BookingDate { get; set; }
    }

    public class MyAttractionBookingsViewModel
    {
        public List<AttractionBookingHistoryViewModel> UpcomingBookings { get; set; }
        public List<AttractionBookingHistoryViewModel> PastBookings { get; set; }
        public List<AttractionBookingHistoryViewModel> CancelledBookings { get; set; }
    }

    public class AttractionBookingHistoryViewModel
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; }
        public string AttractionName { get; set; }
        public string AttractionImage { get; set; }
        public string AttractionAddress { get; set; }
        public DateTime VisitDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime BookingDate { get; set; }
        public bool CanCancel { get; set; }
        public bool CanReview { get; set; }
    }

    public class AttractionReviewCreateViewModel
    {
        public int AttractionId { get; set; }
        public int BookingId { get; set; }
        public string AttractionName { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Vui lòng chọn số sao từ 1 đến 5")]
        public int Rating { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }
    }

    public class AttractionDashboardViewModel
    {
        public int TotalAttractions { get; set; }
        public int TotalBookings { get; set; }
        public int TodayBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<AttractionCardViewModel> FeaturedAttractions { get; set; }
        public List<AttractionCardViewModel> PopularAttractions { get; set; }
        public Dictionary<string, int> BookingsByMonth { get; set; }
        public Dictionary<string, int> BookingsByCategory { get; set; }
    }
}