using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TH_WEB.Areas.Attractions.Middleware
{
    public class AttractionsErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AttractionsErrorHandlingMiddleware> _logger;

        public AttractionsErrorHandlingMiddleware(RequestDelegate next, ILogger<AttractionsErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                success = false,
                message = GetUserFriendlyMessage(exception),
                error = exception.Message
            };

            context.Response.StatusCode = GetStatusCode(exception);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

        private static string GetUserFriendlyMessage(Exception exception)
        {
            return exception switch
            {
                UnauthorizedAccessException => "Bạn không có quyền thực hiện thao tác này",
                ArgumentException => "Dữ liệu không hợp lệ",
                InvalidOperationException => "Không thể thực hiện thao tác này",
                _ => "Đã xảy ra lỗi, vui lòng thử lại sau"
            };
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }
    }
} 