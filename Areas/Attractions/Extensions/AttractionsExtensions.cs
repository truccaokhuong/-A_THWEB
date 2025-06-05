using System;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.Utilities;

namespace TH_WEB.Areas.Attractions.Extensions
{
    public static class AttractionsExtensions
    {
        public static SelectList AttractionsDropDownList(this IHtmlHelper helper, AttractionCategory? selectedCategory = null)
        {
            var categories = Enum.GetValues(typeof(AttractionCategory))
                .Cast<AttractionCategory>()
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString(),
                    Selected = selectedCategory.HasValue && c == selectedCategory.Value
                });

            return new SelectList(categories, "Value", "Text", selectedCategory?.ToString() ?? string.Empty);
        }

        public static SelectList StatusDropDownList(this IHtmlHelper helper, AttractionStatus? selectedStatus = null)
        {
            var statuses = Enum.GetValues(typeof(AttractionStatus))
                .Cast<AttractionStatus>()
                .Select(s => new SelectListItem
                {
                    Value = s.ToString(),
                    Text = GetStatusText(s),
                    Selected = selectedStatus.HasValue && s == selectedStatus.Value
                });

            return new SelectList(statuses, "Value", "Text", selectedStatus?.ToString() ?? string.Empty);
        }

        public static string GetStatusText(AttractionStatus status)
        {
            return status switch
            {
                AttractionStatus.Active => "Hoạt động",
                AttractionStatus.Inactive => "Không hoạt động",
                AttractionStatus.Maintenance => "Đang bảo trì",
                AttractionStatus.Closed => "Đã đóng cửa",
                _ => status.ToString()
            };
        }

        public static IHtmlContent FormatPrice(this IHtmlHelper htmlHelper, decimal price)
        {
            return new HtmlString(AttractionsHelper.FormatPrice(price));
        }

        public static IHtmlContent RatingStars(this IHtmlHelper htmlHelper, double rating)
        {
            return new HtmlString(AttractionsHelper.GetRatingStars(rating));
        }

        public static IHtmlContent StatusBadge(this IHtmlHelper htmlHelper, AttractionStatus status)
        {
            return new HtmlString(AttractionsHelper.GetStatusBadge(status));
        }
    }
} 