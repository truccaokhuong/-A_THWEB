using TH_WEB.Models.Authorization;

namespace TH_WEB.Services.Authorization
{
    public interface IPermissionService
    {
        // Permission management
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int id);
        Task<Permission?> GetPermissionByNameAsync(string name);
        Task<Permission> CreatePermissionAsync(Permission permission);
        Task<Permission> UpdatePermissionAsync(Permission permission);
        Task<bool> DeletePermissionAsync(int id);

        // User permissions
        Task<bool> HasPermissionAsync(string userId, string permissionName);
        Task<bool> HasAnyPermissionAsync(string userId, params string[] permissionNames);
        Task<bool> HasAllPermissionsAsync(string userId, params string[] permissionNames);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(string userId);
        Task<bool> GrantPermissionToUserAsync(string userId, string permissionName, string? grantedBy = null, DateTime? expiresAt = null);
        Task<bool> RevokePermissionFromUserAsync(string userId, string permissionName);

        // Role permissions
        Task<IEnumerable<Permission>> GetRolePermissionsAsync(string roleId);
        Task<bool> GrantPermissionToRoleAsync(string roleId, string permissionName, string? grantedBy = null);
        Task<bool> RevokePermissionFromRoleAsync(string roleId, string permissionName);

        // Resource ownership
        Task<bool> HasResourceOwnershipAsync(string userId, string resourceType, int resourceId);
        Task<bool> GrantResourceOwnershipAsync(string userId, string resourceType, int resourceId, string ownershipType = "Owner", string? grantedBy = null, DateTime? expiresAt = null);
        Task<bool> RevokeResourceOwnershipAsync(string userId, string resourceType, int resourceId);
        Task<IEnumerable<ResourceOwnership>> GetUserResourceOwnershipsAsync(string userId);
        Task<IEnumerable<ResourceOwnership>> GetResourceOwnersAsync(string resourceType, int resourceId);        // Combined permission check (role + user + ownership)
        Task<bool> CanAccessResourceAsync(string userId, string resourceType, int resourceId, string requiredPermission);
        Task<bool> CanPerformActionAsync(string userId, string action, string? resourceType = null, int? resourceId = null);
        
        // Extension method compatibility
        Task<bool> AssignResourceOwnershipAsync(string userId, string resourceType, int resourceId, string ownershipType = "Owner", string? grantedBy = null);
        Task<bool> HasResourceAccessAsync(string userId, string resourceType, int resourceId, string requiredPermission);
    }
}
