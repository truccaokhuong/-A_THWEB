using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TH_WEB.Areas.Attractions.Middleware
{
    public class AttractionsLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AttractionsLoggingMiddleware> _logger;

        public AttractionsLoggingMiddleware(RequestDelegate next, ILogger<AttractionsLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();
                var elapsed = sw.ElapsedMilliseconds;

                var userId = context.User?.FindFirst("sub")?.Value ?? "anonymous";
                var method = context.Request.Method;
                var path = context.Request.Path;
                var statusCode = context.Response.StatusCode;
                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                _logger.LogInformation(
                    "Request completed in {ElapsedMilliseconds}ms | User: {UserId} | {Method} {Path} | Status: {StatusCode} | IP: {IpAddress}",
                    elapsed,
                    userId,
                    method,
                    path,
                    statusCode,
                    ipAddress
                );

                // Log additional details for errors
                if (statusCode >= 400)
                {
                    _logger.LogWarning(
                        "Error response details | User: {UserId} | {Method} {Path} | Status: {StatusCode} | IP: {IpAddress}",
                        userId,
                        method,
                        path,
                        statusCode,
                        ipAddress
                    );
                }
            }
        }
    }
} 