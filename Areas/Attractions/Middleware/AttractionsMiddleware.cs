using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.Validation;

namespace TH_WEB.Areas.Attractions.Middleware
{
    public class AttractionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AttractionsMiddleware> _logger;
        private readonly IMemoryCache _cache;

        public AttractionsMiddleware(RequestDelegate next, ILogger<AttractionsMiddleware> logger, IMemoryCache cache)
        {
            _next = next;
            _logger = logger;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var timestamp = DateTime.UtcNow;
            var userId = context.User?.FindFirst("sub")?.Value ?? "anonymous";
            var method = context.Request.Method;
            var url = context.Request.Path;
            var ip = context.Connection.RemoteIpAddress?.ToString();

            _logger.LogInformation(
                "[{Timestamp}] {Method} {Url} - User: {UserId} - IP: {Ip}",
                timestamp, method, url, userId, ip);

            await _next(context);
        }
    }

    public class RateLimitAttribute : ActionFilterAttribute
    {
        private readonly string _key;
        private readonly int _maxRequests;
        private readonly int _windowMinutes;

        public RateLimitAttribute(string key, int maxRequests, int windowMinutes)
        {
            _key = key;
            _maxRequests = maxRequests;
            _windowMinutes = windowMinutes;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            var cacheKey = $"{_key}:{ip}";

            if (cache.TryGetValue(cacheKey, out int requestCount))
            {
                if (requestCount >= _maxRequests)
                {
                    context.Result = new JsonResult(new
                    {
                        success = false,
                        message = $"Too many requests, please try again in {_windowMinutes} minutes"
                    })
                    {
                        StatusCode = StatusCodes.Status429TooManyRequests
                    };
                    return;
                }
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(_windowMinutes));

            cache.Set(cacheKey, requestCount + 1, cacheEntryOptions);
        }
    }

    public class ValidateAttractionDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("model", out var model))
            {
                if (model is Attraction attraction)
                {
                    var errors = new List<string>();

                    // Required fields validation
                    if (string.IsNullOrWhiteSpace(attraction.Name))
                    {
                        errors.Add("Tên điểm tham quan là bắt buộc");
                    }
                    else if (attraction.Name.Length > 200)
                    {
                        errors.Add("Tên điểm tham quan không được vượt quá 200 ký tự");
                    }

                    if (string.IsNullOrWhiteSpace(attraction.Description))
                    {
                        errors.Add("Mô tả là bắt buộc");
                    }
                    else if (attraction.Description.Length > 2000)
                    {
                        errors.Add("Mô tả không được vượt quá 2000 ký tự");
                    }

                    if (!Enum.IsDefined(typeof(AttractionCategory), attraction.Category))
                    {
                        errors.Add("Danh mục không hợp lệ");
                    }

                    if (string.IsNullOrWhiteSpace(attraction.Address))
                    {
                        errors.Add("Địa chỉ là bắt buộc");
                    }

                    if (string.IsNullOrWhiteSpace(attraction.City))
                    {
                        errors.Add("Thành phố là bắt buộc");
                    }

                    if (string.IsNullOrWhiteSpace(attraction.State))
                    {
                        errors.Add("Tỉnh/Thành phố là bắt buộc");
                    }

                    if (string.IsNullOrWhiteSpace(attraction.Country))
                    {
                        errors.Add("Quốc gia là bắt buộc");
                    }

                    if (!Enum.IsDefined(typeof(AttractionStatus), attraction.Status))
                    {
                        errors.Add("Trạng thái không hợp lệ");
                    }

                    // Email validation if provided
                    if (!string.IsNullOrWhiteSpace(attraction.Email))
                    {
                        var emailValidator = new ValidEmailAttribute();
                        if (!emailValidator.IsValid(attraction.Email))
                        {
                            errors.Add("Email không đúng định dạng");
                        }
                    }

                    if (errors.Any())
                    {
                        context.Result = new JsonResult(new
                        {
                            success = false,
                            message = "Lỗi xác thực",
                            errors
                        })
                        {
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                    }
                }
            }
        }
    }

    public class ValidatePaginationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var page = context.HttpContext.Request.Query["page"].ToString();
            var limit = context.HttpContext.Request.Query["limit"].ToString();

            if (!string.IsNullOrEmpty(page) && (!int.TryParse(page, out int pageNum) || pageNum < 1))
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Trang phải lớn hơn 0"
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return;
            }

            if (!string.IsNullOrEmpty(limit) && (!int.TryParse(limit, out int limitNum) || limitNum < 1 || limitNum > 100))
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Giới hạn phải từ 1 đến 100"
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return;
            }
        }
    }

    public class ValidateImagesAttribute : ActionFilterAttribute
    {
        private const int MaxFileSize = 5 * 1024 * 1024; // 5MB
        private static readonly string[] AllowedTypes = { "image/jpeg", "image/png", "image/webp" };

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var files = context.HttpContext.Request.Form.Files;

            if (files == null || !files.Any())
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Cần ít nhất một hình ảnh"
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return;
            }

            foreach (var file in files)
            {
                if (file.Length > MaxFileSize)
                {
                    context.Result = new JsonResult(new
                    {
                        success = false,
                        message = "Kích thước hình ảnh phải nhỏ hơn 5MB"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                    return;
                }

                if (!AllowedTypes.Contains(file.ContentType.ToLower()))
                {
                    context.Result = new JsonResult(new
                    {
                        success = false,
                        message = "Chỉ chấp nhận hình ảnh JPEG, PNG và WebP"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                    return;
                }
            }
        }
    }
} 