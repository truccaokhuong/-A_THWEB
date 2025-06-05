using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace TH_WEB.Areas.Attractions
{
    public static class AttractionsAreaRegistration
    {
        public static void MapAttractionsRoutes(this IEndpointRouteBuilder endpoints)
        {
            // API Routes
            endpoints.MapControllerRoute(
                name: "AttractionsApi",
                pattern: "api/attractions/{action}/{id?}",
                defaults: new { controller = "AttractionsApi", action = "Index" },
                constraints: new { area = "Attractions" }
            );

            // Admin Routes
            endpoints.MapControllerRoute(
                name: "AttractionsAdmin",
                pattern: "admin/attractions/{action}/{id?}",
                defaults: new { controller = "AttractionsAdmin", action = "Index" },
                constraints: new { area = "Attractions" }
            );

            // Public Routes
            endpoints.MapControllerRoute(
                name: "Attractions_default",
                pattern: "attractions/{action}/{id?}",
                defaults: new { controller = "Attractions", action = "Index" },
                constraints: new { area = "Attractions" }
            );
        }
    }
} 