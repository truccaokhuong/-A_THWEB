using Microsoft.Extensions.Configuration;

namespace TH_WEB.Areas.Attractions.Configuration
{
    public static class AttractionsConfig
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string DefaultImagePath => _configuration["Attractions:DefaultImagePath"] ?? "/wwwroot/images/attractions/default.jpg";
        public static int PageSize => int.Parse(_configuration["Attractions:PageSize"] ?? "12");
        public static int MaxImageSize => int.Parse(_configuration["Attractions:MaxImageSize"] ?? "5242880"); // 5MB
        public static string[] AllowedImageTypes => (_configuration["Attractions:AllowedImageTypes"] ?? "jpg,jpeg,png,gif").Split(',');
        public static decimal MaxPrice => decimal.Parse(_configuration["Attractions:MaxPrice"] ?? "10000000");
        public static int MaxCapacity => int.Parse(_configuration["Attractions:MaxCapacity"] ?? "1000");
    }
} 