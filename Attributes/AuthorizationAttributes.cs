using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TH_WEB.Services.Authorization;
using System.Security.Claims;

namespace TH_WEB.Attributes
{
    /// <summary>
    /// Custom authorization attribute for permission-based access control
    /// </summary>
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(string permission) : base(typeof(RequirePermissionFilter))
        {
            Arguments = new object[] { permission };
        }

        public RequirePermissionAttribute(params string[] permissions) : base(typeof(RequirePermissionFilter))
        {
            Arguments = new object[] { permissions };
        }
    }    public class RequirePermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string[] _permissions;
        private readonly IPermissionService _permissionService;

        public RequirePermissionFilter(string[] permissions, IPermissionService permissionService)
        {
            _permissions = permissions;
            _permissionService = permissionService;
        }

        // Constructor for single permission
        public RequirePermissionFilter(string permission, IPermissionService permissionService)
        {
            _permissions = new[] { permission };
            _permissionService = permissionService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Check if user is authenticated
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new ChallengeResult();
                return;
            }

            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ForbidResult();
                return;
            }            // Check if user has any of the required permissions
            bool hasPermission = await _permissionService.HasAnyPermissionAsync(userId, _permissions);

            if (!hasPermission)
            {
                // Store the attempted permission for the access denied page
                context.HttpContext.Items["AttemptedPermission"] = string.Join(", ", _permissions);
                context.HttpContext.Items["AttemptedAction"] = $"{context.ActionDescriptor.RouteValues["controller"]}/{context.ActionDescriptor.RouteValues["action"]}";
                
                context.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/AccessDenied.cshtml"
                };
                return;
            }
        }
    }

    /// <summary>
    /// Requires all specified permissions
    /// </summary>
    public class RequireAllPermissionsAttribute : TypeFilterAttribute
    {
        public RequireAllPermissionsAttribute(params string[] permissions) : base(typeof(RequireAllPermissionsFilter))
        {
            Arguments = new object[] { permissions };
        }
    }    public class RequireAllPermissionsFilter : IAsyncAuthorizationFilter
    {
        private readonly string[] _permissions;
        private readonly IPermissionService _permissionService;

        public RequireAllPermissionsFilter(string[] permissions, IPermissionService permissionService)
        {
            _permissions = permissions;
            _permissionService = permissionService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new ChallengeResult();
                return;
            }

            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ForbidResult();
                return;
            }            // Check if user has all required permissions
            bool hasAllPermissions = await _permissionService.HasAllPermissionsAsync(userId, _permissions);

            if (!hasAllPermissions)
            {
                // Store the attempted permissions for the access denied page
                context.HttpContext.Items["AttemptedPermission"] = string.Join(", ", _permissions);
                context.HttpContext.Items["AttemptedAction"] = $"{context.ActionDescriptor.RouteValues["controller"]}/{context.ActionDescriptor.RouteValues["action"]}";
                
                context.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/AccessDenied.cshtml"
                };
                return;
            }
        }
    }

    /// <summary>
    /// Requires resource ownership or specific permission
    /// </summary>
    public class RequireResourceOwnershipAttribute : TypeFilterAttribute
    {
        public RequireResourceOwnershipAttribute(string resourceType, string? fallbackPermission = null) 
            : base(typeof(RequireResourceOwnershipFilter))
        {
            Arguments = new object[] { resourceType, fallbackPermission ?? string.Empty };
        }
    }    public class RequireResourceOwnershipFilter : IAsyncAuthorizationFilter
    {
        private readonly string _resourceType;
        private readonly string _fallbackPermission;
        private readonly IPermissionService _permissionService;

        public RequireResourceOwnershipFilter(string resourceType, string fallbackPermission, IPermissionService permissionService)
        {
            _resourceType = resourceType;
            _fallbackPermission = fallbackPermission;
            _permissionService = permissionService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new ChallengeResult();
                return;
            }

            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            // Try to get resource ID from route
            var resourceIdStr = context.RouteData.Values["id"]?.ToString();
            if (!int.TryParse(resourceIdStr, out int resourceId))
            {
                context.Result = new BadRequestResult();
                return;
            }

            // Check resource ownership first
            bool hasOwnership = await _permissionService.HasResourceOwnershipAsync(userId, _resourceType, resourceId);
            
            // If no ownership, check fallback permission
            if (!hasOwnership && !string.IsNullOrEmpty(_fallbackPermission))
            {
                hasOwnership = await _permissionService.HasPermissionAsync(userId, _fallbackPermission);
            }            if (!hasOwnership)
            {
                // Store resource ownership info for the access denied page
                context.HttpContext.Items["AttemptedResource"] = _resourceType;
                context.HttpContext.Items["AttemptedResourceId"] = resourceId;
                context.HttpContext.Items["AttemptedAction"] = $"{context.ActionDescriptor.RouteValues["controller"]}/{context.ActionDescriptor.RouteValues["action"]}";
                
                context.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/AccessDenied.cshtml"
                };
                return;
            }

            // Store resource info for use in controller
            context.HttpContext.Items["ResourceType"] = _resourceType;
            context.HttpContext.Items["ResourceId"] = resourceId;
        }
    }
}
