using System.Security.Claims;
using TH_WEB.Services.Authorization;

namespace TH_WEB.Extensions
{
    public static class ResourceOwnershipExtensions
    {        public static async Task AssignOwnershipAsync(this IPermissionService permissionService, 
            ClaimsPrincipal user, string resourceType, int resourceId)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                await permissionService.AssignResourceOwnershipAsync(userId, resourceType, resourceId);
            }
        }

        public static async Task<bool> CanAccessResourceAsync(this IPermissionService permissionService,
            ClaimsPrincipal user, string permission, string resourceType, int resourceId)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return false;

            // Check if user has the permission
            if (!await permissionService.HasPermissionAsync(userId, permission))
                return false;

            // Check if user owns the resource or has admin permissions
            return await permissionService.HasResourceAccessAsync(userId, resourceType, resourceId, permission);
        }
    }
}
