using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TH_WEB.Areas.Attractions.Models;

namespace TH_WEB.Areas.Attractions.Filters
{
    public class RequireAdminAttribute : AuthorizeAttribute
    {
        public RequireAdminAttribute()
        {
            Roles = "Admin";
        }
    }

    public class RequireOwnerOrAdminAttribute : TypeFilterAttribute
    {
        public RequireOwnerOrAdminAttribute() : base(typeof(RequireOwnerOrAdminFilter))
        {
        }
    }

    public class RequireOwnerOrAdminFilter : IAsyncAuthorizationFilter
    {
        private readonly ILogger<RequireOwnerOrAdminFilter> _logger;

        public RequireOwnerOrAdminFilter(ILogger<RequireOwnerOrAdminFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var attractionId = context.RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(attractionId))
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "ID điểm tham quan không hợp lệ"
                })
                {
                    StatusCode = 400
                };
                return;
            }

            var attraction = await GetAttractionAsync(attractionId);
            if (attraction == null)
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Không tìm thấy điểm tham quan"
                })
                {
                    StatusCode = 404
                };
                return;
            }

            var userId = context.HttpContext.User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Không có quyền truy cập"
                })
                {
                    StatusCode = 403
                };
                return;
            }

            var isAdmin = context.HttpContext.User.IsInRole("Admin");
            var isOwner = attraction.OwnerId == userId || attraction.CreatedById == userId;

            if (!isAdmin && !isOwner)
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Không có quyền truy cập"
                })
                {
                    StatusCode = 403
                };
                return;
            }

            // Store the attraction in HttpContext.Items for use in the controller
            context.HttpContext.Items["Attraction"] = attraction;
        }

        private async Task<Attraction> GetAttractionAsync(string id)
        {
            // TODO: Implement attraction retrieval from your repository
            throw new NotImplementedException();
        }
    }

    public class OptionalAuthenticationAttribute : TypeFilterAttribute
    {
        public OptionalAuthenticationAttribute() : base(typeof(OptionalAuthenticationFilter))
        {
        }
    }

    public class OptionalAuthenticationFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Optional authentication - just set the user if available
            var userId = context.HttpContext.User.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                context.HttpContext.Items["UserId"] = userId;
            }

            return Task.CompletedTask;
        }
    }
} 