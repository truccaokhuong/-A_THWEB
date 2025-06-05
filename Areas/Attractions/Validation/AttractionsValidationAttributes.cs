using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using TH_WEB.Areas.Attractions.Configuration;
using TH_WEB.Areas.Attractions.Utilities;
using TH_WEB.Areas.Attractions.Models;

namespace TH_WEB.Areas.Attractions.Validation
{
    public class ValidPriceRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is decimal price)
            {
                return price >= 0 && price <= AttractionsConfig.MaxPrice;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Giá phải từ 0 đến {AttractionsHelper.FormatPrice(AttractionsConfig.MaxPrice)}";
        }
    }

    public class ValidOperatingHoursAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string operatingHours)
            {
                return string.IsNullOrEmpty(operatingHours) || AttractionsHelper.IsValidOperatingHours(operatingHours);
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Giờ hoạt động không đúng định dạng (VD: 08:00 - 18:00)";
        }
    }

    public class ValidCoordinateAttribute : ValidationAttribute
    {
        private readonly double _min;
        private readonly double _max;

        public ValidCoordinateAttribute(double min, double max)
        {
            _min = min;
            _max = max;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true; // Allow null for optional coordinates
            if (value is double coordinate)
            {
                return coordinate >= _min && coordinate <= _max;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} phải nằm trong khoảng {_min} đến {_max}";
        }
    }

    public class ValidLatitudeAttribute : ValidCoordinateAttribute
    {
        public ValidLatitudeAttribute() : base(-90, 90) { }
    }

    public class ValidLongitudeAttribute : ValidCoordinateAttribute
    {
        public ValidLongitudeAttribute() : base(-180, 180) { }
    }

    public class ValidUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return true; // Allow null/empty for optional URLs

            string url = value.ToString();
            
            // Check if URL starts with http:// or https://
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Try to create Uri object to validate format
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result) && 
                   (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} phải là một URL hợp lệ (bắt đầu bằng http:// hoặc https://)";
        }
    }

    public class ValidPhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return true; // Allow null/empty for optional phone numbers

            string phoneNumber = value.ToString().Trim();
            
            // Vietnamese phone number pattern
            // Supports: 0xxx-xxx-xxx, +84xxx-xxx-xxx, 84xxx-xxx-xxx
            string pattern = @"^(\+84|84|0)[1-9][0-9]{8,9}$";
            return Regex.IsMatch(phoneNumber.Replace(" ", "").Replace("-", ""), pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} không đúng định dạng số điện thoại Việt Nam";
        }
    }

    public class ValidEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return true; // Allow null/empty for optional emails

            string email = value.ToString().Trim();
            
            // Email validation pattern
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} không đúng định dạng email";
        }
    }

    public class ValidAttractionCategoryAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false; // Category is required

            if (value is TH_WEB.Areas.Attractions.Models.AttractionCategory category)
            {
                return Enum.IsDefined(typeof(TH_WEB.Areas.Attractions.Models.AttractionCategory), category);
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            var categories = string.Join(", ", Enum.GetNames(typeof(TH_WEB.Areas.Attractions.Models.AttractionCategory)));
            return $"{name} phải là một trong các loại hình: {categories}";
        }
    }

    public class ValidRatingAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true; // Allow null for optional ratings

            if (value is decimal rating)
            {
                return rating >= 0 && rating <= 5;
            }
            
            if (value is double doubleRating)
            {
                return doubleRating >= 0 && doubleRating <= 5;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} phải từ 0 đến 5 sao";
        }
    }

    public class ValidImageUrlAttribute : ValidationAttribute
    {
        private static readonly HashSet<string> ValidExtensions = new HashSet<string>
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp"
        };

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return true; // Allow null/empty for optional image URLs

            string url = value.ToString().ToLower();
            
            // Check if it's a valid URL first
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri result))
                return false;

            // Check if it has a valid image extension
            return ValidExtensions.Any(ext => url.EndsWith(ext));
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} phải là URL hình ảnh hợp lệ (.jpg, .jpeg, .png, .gif, .webp, .bmp)";
        }
    }

    public class ValidDateRangeAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly bool _isStartDate;

        public ValidDateRangeAttribute(string comparisonProperty, bool isStartDate = true)
        {
            _comparisonProperty = comparisonProperty;
            _isStartDate = isStartDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var currentValue = (DateTime)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            
            if (property == null)
                return new ValidationResult($"Không tìm thấy thuộc tính {_comparisonProperty}");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            
            if (comparisonValue == null) return ValidationResult.Success;

            var comparisonDate = (DateTime)comparisonValue;

            if (_isStartDate && currentValue >= comparisonDate)
            {
                return new ValidationResult("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
            }
            
            if (!_isStartDate && currentValue <= comparisonDate)
            {
                return new ValidationResult("Ngày kết thúc phải lớn hơn ngày bắt đầu");
            }

            return ValidationResult.Success;
        }
    }

    public class ValidBusinessHoursAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return true; // Allow null/empty

            string businessHours = value.ToString().Trim();
            
            // Pattern for business hours like "08:00-18:00" or "08:00 - 18:00"
            string pattern = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]\s*-\s*([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            
            if (!Regex.IsMatch(businessHours, pattern))
                return false;

            // Extract start and end times
            var parts = businessHours.Split('-');
            if (parts.Length != 2) return false;

            var startTime = TimeSpan.Parse(parts[0].Trim());
            var endTime = TimeSpan.Parse(parts[1].Trim());

            // End time should be after start time
            return endTime > startTime;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} phải có định dạng HH:mm-HH:mm (VD: 08:00-18:00)";
        }
    }

    public class ValidDiscountPercentageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true; // Allow null for optional discounts

            if (value is decimal percentage)
            {
                return percentage >= 0 && percentage <= 100;
            }

            if (value is int intPercentage)
            {
                return intPercentage >= 0 && intPercentage <= 100;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} phải từ 0% đến 100%";
        }
    }

    public class ValidTagsAttribute : ValidationAttribute
    {
        private readonly int _maxTags;
        private readonly int _maxTagLength;

        public ValidTagsAttribute(int maxTags = 10, int maxTagLength = 50)
        {
            _maxTags = maxTags;
            _maxTagLength = maxTagLength;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true; // Allow null for optional tags

            if (value is IEnumerable<string> tags)
            {
                var tagList = tags.ToList();
                
                // Check number of tags
                if (tagList.Count > _maxTags)
                    return false;

                // Check each tag length and format
                foreach (var tag in tagList)
                {
                    if (string.IsNullOrWhiteSpace(tag) || 
                        tag.Length > _maxTagLength ||
                        !Regex.IsMatch(tag, @"^[a-zA-Z0-9\s\u00C0-\u1EF9]+$")) // Allow Vietnamese characters
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} không được quá {_maxTags} thẻ, mỗi thẻ không quá {_maxTagLength} ký tự";
        }
    }
} 