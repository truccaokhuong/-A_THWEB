using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TH_WEB.Areas.Attractions.Middleware
{
    public static class AttractionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseAttractionsErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AttractionsErrorHandlingMiddleware>();
        }

        public static IApplicationBuilder UseAttractionsLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AttractionsLoggingMiddleware>();
        }

        public static IServiceCollection AddAttractionsMiddleware(this IServiceCollection services)
        {
            // Add any required services for the middleware
            services.AddMemoryCache(); // For rate limiting
            return services;
        }
    }
} 