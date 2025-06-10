using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TH_WEB.Data;
using TH_WEB.Models;
using TH_WEB.Models.Authorization;

namespace TH_WEB.Services.Authorization
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Permission Management

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions
                .Where(p => p.IsActive)
                .OrderBy(p => p.Category)
                .ThenBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<Permission?> GetPermissionByNameAsync(string name)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Name == name && p.IsActive);
        }

        public async Task<Permission> CreatePermissionAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission> UpdatePermissionAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var permission = await GetPermissionByIdAsync(id);
            if (permission == null) return false;

            permission.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

        #region User Permissions

        public async Task<bool> HasPermissionAsync(string userId, string permissionName)
        {
            // Check direct user permission
            var hasDirectPermission = await _context.UserPermissions
                .Include(up => up.Permission)
                .AnyAsync(up => up.UserId == userId && 
                               up.Permission.Name == permissionName && 
                               up.IsActive && 
                               up.Permission.IsActive &&
                               (up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow));

            if (hasDirectPermission) return true;

            // Check role-based permission
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var userRoles = await _userManager.GetRolesAsync(user);
            
            var hasRolePermission = await _context.RolePermissions
                .Include(rp => rp.Permission)
                .Include(rp => rp.Role)
                .AnyAsync(rp => userRoles.Contains(rp.Role.Name) && 
                               rp.Permission.Name == permissionName && 
                               rp.Permission.IsActive);

            return hasRolePermission;
        }

        public async Task<bool> HasAnyPermissionAsync(string userId, params string[] permissionNames)
        {
            foreach (var permission in permissionNames)
            {
                if (await HasPermissionAsync(userId, permission))
                    return true;
            }
            return false;
        }

        public async Task<bool> HasAllPermissionsAsync(string userId, params string[] permissionNames)
        {
            foreach (var permission in permissionNames)
            {
                if (!await HasPermissionAsync(userId, permission))
                    return false;
            }
            return true;
        }

        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<Permission>();

            var userRoles = await _userManager.GetRolesAsync(user);

            // Get direct user permissions
            var directPermissions = await _context.UserPermissions
                .Include(up => up.Permission)
                .Where(up => up.UserId == userId && 
                            up.IsActive && 
                            up.Permission.IsActive &&
                            (up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow))
                .Select(up => up.Permission)
                .ToListAsync();

            // Get role-based permissions
            var rolePermissions = await _context.RolePermissions
                .Include(rp => rp.Permission)
                .Include(rp => rp.Role)
                .Where(rp => userRoles.Contains(rp.Role.Name) && rp.Permission.IsActive)
                .Select(rp => rp.Permission)
                .ToListAsync();

            // Combine and remove duplicates
            return directPermissions.Union(rolePermissions).Distinct().ToList();
        }

        public async Task<bool> GrantPermissionToUserAsync(string userId, string permissionName, string? grantedBy = null, DateTime? expiresAt = null)
        {
            var permission = await GetPermissionByNameAsync(permissionName);
            if (permission == null) return false;

            var existingPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permission.Id);

            if (existingPermission != null)
            {
                existingPermission.IsActive = true;
                existingPermission.ExpiresAt = expiresAt;
                existingPermission.GrantedBy = grantedBy;
                existingPermission.GrantedAt = DateTime.UtcNow;
            }
            else
            {
                var userPermission = new UserPermission
                {
                    UserId = userId,
                    PermissionId = permission.Id,
                    GrantedBy = grantedBy,
                    ExpiresAt = expiresAt,
                    IsActive = true
                };
                _context.UserPermissions.Add(userPermission);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RevokePermissionFromUserAsync(string userId, string permissionName)
        {
            var permission = await GetPermissionByNameAsync(permissionName);
            if (permission == null) return false;

            var userPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permission.Id);

            if (userPermission != null)
            {
                userPermission.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        #endregion

        #region Role Permissions

        public async Task<IEnumerable<Permission>> GetRolePermissionsAsync(string roleId)
        {
            return await _context.RolePermissions
                .Include(rp => rp.Permission)
                .Where(rp => rp.RoleId == roleId && rp.Permission.IsActive)
                .Select(rp => rp.Permission)
                .ToListAsync();
        }

        public async Task<bool> GrantPermissionToRoleAsync(string roleId, string permissionName, string? grantedBy = null)
        {
            var permission = await GetPermissionByNameAsync(permissionName);
            if (permission == null) return false;

            var existingPermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission.Id);

            if (existingPermission == null)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permission.Id,
                    GrantedBy = grantedBy
                };
                _context.RolePermissions.Add(rolePermission);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RevokePermissionFromRoleAsync(string roleId, string permissionName)
        {
            var permission = await GetPermissionByNameAsync(permissionName);
            if (permission == null) return false;

            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission.Id);

            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        #endregion

        #region Resource Ownership

        public async Task<bool> HasResourceOwnershipAsync(string userId, string resourceType, int resourceId)
        {
            return await _context.ResourceOwnerships
                .AnyAsync(ro => ro.UserId == userId && 
                               ro.ResourceType == resourceType && 
                               ro.ResourceId == resourceId && 
                               ro.IsActive &&
                               (ro.ExpiresAt == null || ro.ExpiresAt > DateTime.UtcNow));
        }

        public async Task<bool> GrantResourceOwnershipAsync(string userId, string resourceType, int resourceId, string ownershipType = "Owner", string? grantedBy = null, DateTime? expiresAt = null)
        {
            var existingOwnership = await _context.ResourceOwnerships
                .FirstOrDefaultAsync(ro => ro.UserId == userId && 
                                          ro.ResourceType == resourceType && 
                                          ro.ResourceId == resourceId);

            if (existingOwnership != null)
            {
                existingOwnership.OwnershipType = ownershipType;
                existingOwnership.IsActive = true;
                existingOwnership.ExpiresAt = expiresAt;
                existingOwnership.GrantedBy = grantedBy;
                existingOwnership.GrantedAt = DateTime.UtcNow;
            }
            else
            {
                var ownership = new ResourceOwnership
                {
                    UserId = userId,
                    ResourceType = resourceType,
                    ResourceId = resourceId,
                    OwnershipType = ownershipType,
                    GrantedBy = grantedBy,
                    ExpiresAt = expiresAt,
                    IsActive = true
                };
                _context.ResourceOwnerships.Add(ownership);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RevokeResourceOwnershipAsync(string userId, string resourceType, int resourceId)
        {
            var ownership = await _context.ResourceOwnerships
                .FirstOrDefaultAsync(ro => ro.UserId == userId && 
                                          ro.ResourceType == resourceType && 
                                          ro.ResourceId == resourceId);

            if (ownership != null)
            {
                ownership.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ResourceOwnership>> GetUserResourceOwnershipsAsync(string userId)
        {
            return await _context.ResourceOwnerships
                .Where(ro => ro.UserId == userId && 
                            ro.IsActive &&
                            (ro.ExpiresAt == null || ro.ExpiresAt > DateTime.UtcNow))
                .ToListAsync();
        }

        public async Task<IEnumerable<ResourceOwnership>> GetResourceOwnersAsync(string resourceType, int resourceId)
        {
            return await _context.ResourceOwnerships
                .Include(ro => ro.User)
                .Where(ro => ro.ResourceType == resourceType && 
                            ro.ResourceId == resourceId && 
                            ro.IsActive &&
                            (ro.ExpiresAt == null || ro.ExpiresAt > DateTime.UtcNow))
                .ToListAsync();
        }

        #endregion

        #region Combined Permission Checks

        public async Task<bool> CanAccessResourceAsync(string userId, string resourceType, int resourceId, string requiredPermission)
        {
            // Check if user has general permission
            if (await HasPermissionAsync(userId, requiredPermission))
                return true;

            // Check if user owns the resource
            if (await HasResourceOwnershipAsync(userId, resourceType, resourceId))
                return true;

            // Check specific resource permissions based on ownership type
            var ownerships = await _context.ResourceOwnerships
                .Where(ro => ro.UserId == userId && 
                            ro.ResourceType == resourceType && 
                            ro.ResourceId == resourceId && 
                            ro.IsActive &&
                            (ro.ExpiresAt == null || ro.ExpiresAt > DateTime.UtcNow))
                .ToListAsync();

            foreach (var ownership in ownerships)
            {
                string resourceSpecificPermission = $"{resourceType.ToLower()}.{requiredPermission.Split('.').LastOrDefault()}";
                if (await HasPermissionAsync(userId, resourceSpecificPermission))
                    return true;
            }

            return false;
        }

        public async Task<bool> CanPerformActionAsync(string userId, string action, string? resourceType = null, int? resourceId = null)
        {
            // Check general action permission
            if (await HasPermissionAsync(userId, action))
                return true;

            // If resource specific, check resource access
            if (resourceType != null && resourceId.HasValue)
            {
                return await CanAccessResourceAsync(userId, resourceType, resourceId.Value, action);
            }

            return false;
        }

        #endregion

        #region Extension Method Compatibility

        public async Task<bool> AssignResourceOwnershipAsync(string userId, string resourceType, int resourceId, string ownershipType = "Owner", string? grantedBy = null)
        {
            return await GrantResourceOwnershipAsync(userId, resourceType, resourceId, ownershipType, grantedBy);
        }

        public async Task<bool> HasResourceAccessAsync(string userId, string resourceType, int resourceId, string requiredPermission)
        {
            return await CanAccessResourceAsync(userId, resourceType, resourceId, requiredPermission);
        }

        #endregion
    }
}
