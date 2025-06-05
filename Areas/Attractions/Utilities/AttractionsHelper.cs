using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.Configuration;

namespace TH_WEB.Areas.Attractions.Utilities
{
    public static class AttractionsHelper
    {
        public static string FormatPrice(decimal price)
        {
            return price.ToString("N0") + " VNĐ";
        }

        public static string GetRatingStars(double rating)
        {
            var fullStars = (int)rating;
            var halfStar = (rating - fullStars) >= 0.5;
            var emptyStars = 5 - fullStars - (halfStar ? 1 : 0);

            var stars = string.Concat(Enumerable.Repeat("★", fullStars));
            if (halfStar) stars += "☆";
            stars += string.Concat(Enumerable.Repeat("☆", emptyStars));

            return stars;
        }

        public static string GetStatusBadge(AttractionStatus status)
        {
            return status switch
            {
                AttractionStatus.Active => "<span class='badge bg-success'>Hoạt động</span>",
                AttractionStatus.Inactive => "<span class='badge bg-secondary'>Tạm dừng</span>",
                AttractionStatus.Maintenance => "<span class='badge bg-warning'>Bảo trì</span>",
                AttractionStatus.Closed => "<span class='badge bg-danger'>Đóng cửa</span>",
                _ => "<span class='badge bg-light'>Không xác định</span>"
            };
        }

        public static async Task<string> ProcessImageUpload(IFormFile file, string uploadPath)
        {
            if (file == null || file.Length == 0)
                return null;

            // Validate file type
            var fileExtension = Path.GetExtension(file.FileName)?.ToLower().TrimStart('.');
            if (!AttractionsConfig.AllowedImageTypes.Contains(fileExtension))
                throw new ArgumentException("Định dạng file không được hỗ trợ");

            // Validate file size
            if (file.Length > AttractionsConfig.MaxImageSize)
                throw new ArgumentException("Kích thước file quá lớn");

            // Generate unique filename
            var fileName = Guid.NewGuid().ToString() + "." + fileExtension;
            var fullPath = Path.Combine(uploadPath, fileName);

            // Create directory if not exists
            Directory.CreateDirectory(uploadPath);

            // Save file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public static List<AttractionCategory> GetCategories()
        {
            return Enum.GetValues(typeof(AttractionCategory))
                       .Cast<AttractionCategory>()
                       .Select(c => c)
                       .ToList();
        }

        public static string GetCategoryName(AttractionCategory category)
        {
            return category switch
            {
                AttractionCategory.Nature => "Thiên nhiên",
                AttractionCategory.Culture => "Văn hóa",
                AttractionCategory.Adventure => "Phiêu lưu",
                AttractionCategory.Entertainment => "Giải trí",
                AttractionCategory.History => "Lịch sử",
                AttractionCategory.Sports => "Thể thao",
                AttractionCategory.Food => "Ẩm thực",
                AttractionCategory.Shopping => "Mua sắm",
                AttractionCategory.Nightlife => "Cuộc sống về đêm",
                _ => "Khác"
            };
        }

        public static bool IsValidOperatingHours(string operatingHours)
        {
            if (string.IsNullOrWhiteSpace(operatingHours))
                return false;

            // Basic validation for format like "08:00 - 18:00"
            var parts = operatingHours.Split('-');
            if (parts.Length != 2)
                return false;

            return parts.All(part => TimeSpan.TryParse(part.Trim(), out _));
        }

        public static string GetDistanceText(double distance)
        {
            if (distance < 1)
                return $"{(distance * 1000):F0}m";
            else
                return $"{distance:F1}km";
        }

        public static string TruncateDescription(string description, int maxLength = 150)
        {
            if (string.IsNullOrEmpty(description) || description.Length <= maxLength)
                return description;

            return description.Substring(0, maxLength) + "...";
        }
    }
} 