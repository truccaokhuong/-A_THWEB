using System.Security.Claims;
using TH_WEB.Services.Authorization;

namespace TH_WEB.Middleware
{
    public class UserActivityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<UserActivityMiddleware> _logger;

        public UserActivityMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, ILogger<UserActivityMiddleware> logger)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }        public async Task InvokeAsync(HttpContext context)
        {
            // Capture request start time
            var startTime = DateTime.UtcNow;
            context.Items["RequestStartTime"] = startTime;

            // Execute the next middleware
            await _next(context);

            // Log activity after the request is processed (if user is authenticated)
            if (context.User.Identity?.IsAuthenticated == true)
            {
                // Capture all needed data before starting background task
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userId))
                {
                    var method = context.Request.Method;
                    var path = context.Request.Path.ToString();
                    var statusCode = context.Response.StatusCode;
                    var userAgent = context.Request.Headers.UserAgent.ToString();
                    var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                    var duration = DateTime.UtcNow.Subtract(startTime).TotalMilliseconds;

                    // Execute logging in background without accessing HttpContext
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            using var scope = _serviceScopeFactory.CreateScope();
                            var userActivityService = scope.ServiceProvider.GetRequiredService<IUserActivityService>();
                            
                            var activity = $"{method} {path}";
                            var details = $"StatusCode: {statusCode}, " +
                                        $"UserAgent: {userAgent}, " +
                                        $"IPAddress: {ipAddress}, " +
                                        $"Duration: {duration:F2}ms";

                            await userActivityService.LogActivityAsync(userId, activity, details);
                        }
                        catch (Exception ex)
                        {
                            // Log the error but don't throw to avoid affecting the main request
                            _logger.LogError(ex, "Error logging user activity");
                        }
                    });
                }
            }
        }
    }

    public static class UserActivityMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserActivityLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserActivityMiddleware>();
        }
    }
}
