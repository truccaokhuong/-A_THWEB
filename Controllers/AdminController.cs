using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Models;
using TH_WEB.Models.Authorization;
using TH_WEB.Services.Authorization;
using TH_WEB.Attributes;
using TH_WEB.Data;
using TH_WEB.ViewModels;

namespace TH_WEB.Controllers
{
    [RequirePermission(Permissions.UserManagement, Permissions.SystemManagement)]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionService _permissionService;
        private readonly IUserActivityService _activityService;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPermissionService permissionService,
            IUserActivityService activityService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionService = permissionService;
            _activityService = activityService;
            _context = context;
        }

        #region User Management

        public async Task<IActionResult> Users(int page = 1, int pageSize = 20, string? search = null)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Email!.Contains(search) || 
                                        u.FirstName!.Contains(search) || 
                                        u.LastName!.Contains(search));
            }

            var totalUsers = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Email)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userViewModels = new List<UserManagementViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var profile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == user.Id);
                
                userViewModels.Add(new UserManagementViewModel
                {
                    User = user,
                    Roles = roles.ToList(),
                    Profile = profile,
                    LastActivity = await _activityService.GetUserActivitiesAsync(user.Id, 1, 1)
                        .ContinueWith(t => t.Result.FirstOrDefault())
                });
            }

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalUsers / pageSize);
            ViewBag.Search = search;

            return View(userViewModels);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == id);
            var permissions = await _permissionService.GetUserPermissionsAsync(id);
            var activities = await _activityService.GetUserActivitiesAsync(id, 1, 10);
            var ownerships = await _permissionService.GetUserResourceOwnershipsAsync(id);

            var viewModel = new UserDetailsViewModel
            {
                User = user,
                Roles = roles.ToList(),
                Profile = profile,
                Permissions = permissions.ToList(),
                RecentActivities = activities.ToList(),
                ResourceOwnerships = ownerships.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                if (!await _userManager.IsInRoleAsync(user, roleName))
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                    
                    await _activityService.LogActivityAsync(
                        User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "System",
                        "AssignRole",
                        $"Assigned role {roleName} to user {user.Email}",
                        "UserManagement",
                        userId
                    );
                }
            }

            return RedirectToAction(nameof(UserDetails), new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
                
                await _activityService.LogActivityAsync(
                    User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "System",
                    "RemoveRole",
                    $"Removed role {roleName} from user {user.Email}",
                    "UserManagement",
                    userId
                );
            }

            return RedirectToAction(nameof(UserDetails), new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> GrantPermission(string userId, string permissionName, DateTime? expiresAt = null)
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _permissionService.GrantPermissionToUserAsync(userId, permissionName, currentUserId, expiresAt);
            
            if (result)
            {
                await _activityService.LogActivityAsync(
                    currentUserId ?? "System",
                    "GrantPermission",
                    $"Granted permission {permissionName} to user",
                    "UserManagement",
                    userId
                );
            }

            return RedirectToAction(nameof(UserDetails), new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> RevokePermission(string userId, string permissionName)
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _permissionService.RevokePermissionFromUserAsync(userId, permissionName);
            
            if (result)
            {
                await _activityService.LogActivityAsync(
                    currentUserId ?? "System",
                    "RevokePermission",
                    $"Revoked permission {permissionName} from user",
                    "UserManagement",
                    userId
                );
            }

            return RedirectToAction(nameof(UserDetails), new { id = userId });
        }

        #endregion

        #region Role Management        [RequirePermission(Permissions.RoleManagement)]
        public async Task<IActionResult> Roles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleViewModels = new List<RoleManagementViewModel>();

            foreach (var role in roles)
            {
                var userCount = await _userManager.GetUsersInRoleAsync(role.Name!);
                var permissions = await _permissionService.GetRolePermissionsAsync(role.Id);
                
                roleViewModels.Add(new RoleManagementViewModel
                {
                    Role = role,
                    UserCount = userCount.Count,
                    Permissions = permissions.ToList()
                });
            }

            return View(roleViewModels);
        }[RequirePermission(Permissions.RoleManagement)]
        public async Task<IActionResult> RoleDetails(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var permissions = await _permissionService.GetRolePermissionsAsync(id);
            var users = await _userManager.GetUsersInRoleAsync(role.Name!);
            var allPermissions = await _permissionService.GetAllPermissionsAsync();

            var viewModel = new RoleDetailsViewModel
            {
                Role = role,
                Permissions = permissions.ToList(),
                UsersInRole = users.ToList(),
                AvailablePermissions = allPermissions.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [RequirePermission(Permissions.RoleManagement)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest(new { message = "Role name is required" });
            }

            var existingRole = await _roleManager.FindByNameAsync(request.Name);
            if (existingRole != null)
            {
                return BadRequest(new { message = "Role already exists" });
            }

            var role = new IdentityRole(request.Name);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                await _activityService.LogActivityAsync(
                    currentUserId ?? "System",
                    "CreateRole",
                    $"Created role: {request.Name}",
                    "RoleManagement",
                    role.Id
                );

                return Ok(new { success = true, message = "Role created successfully" });
            }

            return BadRequest(new { message = "Failed to create role" });
        }

        [HttpPost]
        [RequirePermission(Permissions.RoleManagement)]
        public async Task<IActionResult> EditRole([FromBody] EditRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return NotFound(new { message = "Role not found" });
            }

            // Prevent editing system roles
            if (role.Name == "SuperAdmin" || role.Name == "Admin")
            {
                return BadRequest(new { message = "Cannot edit system roles" });
            }

            var oldName = role.Name;
            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                await _activityService.LogActivityAsync(
                    currentUserId ?? "System",
                    "EditRole",
                    $"Renamed role from '{oldName}' to '{request.Name}'",
                    "RoleManagement",
                    role.Id
                );

                return Ok(new { success = true, message = "Role updated successfully" });
            }

            return BadRequest(new { message = "Failed to update role" });
        }

        [HttpPost]
        [RequirePermission(Permissions.RoleManagement)]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return NotFound(new { message = "Role not found" });
            }

            // Prevent deleting system roles
            if (role.Name == "SuperAdmin" || role.Name == "Admin")
            {
                return BadRequest(new { message = "Cannot delete system roles" });
            }

            // Check if role has users
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            if (usersInRole.Any())
            {
                return BadRequest(new { message = $"Cannot delete role. It has {usersInRole.Count} assigned users." });
            }

            var roleName = role.Name;
            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                await _activityService.LogActivityAsync(
                    currentUserId ?? "System",
                    "DeleteRole",
                    $"Deleted role: {roleName}",
                    "RoleManagement",
                    request.Id
                );

                return Ok(new { success = true, message = "Role deleted successfully" });
            }

            return BadRequest(new { message = "Failed to delete role" });
        }

        [HttpPost]
        [RequirePermission(Permissions.RoleManagement)]
        public async Task<IActionResult> GrantPermissionToRole([FromBody] RolePermissionRequest request)
        {
            var result = await _permissionService.GrantPermissionToRoleAsync(request.RoleId, request.PermissionName);
            
            if (result)
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                await _activityService.LogActivityAsync(
                    currentUserId ?? "System",
                    "GrantRolePermission",
                    $"Granted permission {request.PermissionName} to role",
                    "RoleManagement",
                    request.RoleId
                );

                return Ok(new { success = true, message = "Permission granted successfully" });
            }

            return BadRequest(new { message = "Failed to grant permission" });
        }

        [HttpPost]
        [RequirePermission(Permissions.RoleManagement)]
        public async Task<IActionResult> RevokePermissionFromRole([FromBody] RolePermissionRequest request)
        {
            var result = await _permissionService.RevokePermissionFromRoleAsync(request.RoleId, request.PermissionName);
            
            if (result)
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                await _activityService.LogActivityAsync(
                    currentUserId ?? "System",
                    "RevokeRolePermission",
                    $"Revoked permission {request.PermissionName} from role",
                    "RoleManagement",
                    request.RoleId
                );

                return Ok(new { success = true, message = "Permission revoked successfully" });
            }

            return BadRequest(new { message = "Failed to revoke permission" });
        }

        [RequirePermission(Permissions.UserManagement, Permissions.SystemManagement)]
        public async Task<IActionResult> ActivityLog(string? entityType = null, int page = 1, int pageSize = 50)
        {
            IEnumerable<UserActivity> activities;
            
            if (!string.IsNullOrEmpty(entityType))
            {
                activities = await _activityService.GetActivitiesByEntityAsync(entityType, null, page, pageSize);
            }
            else
            {
                activities = await _activityService.GetSystemActivitiesAsync(page, pageSize);
            }

            ViewBag.EntityType = entityType;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(activities.ToList());
        }
        #endregion

        #region System Analytics

        public async Task<IActionResult> Dashboard()
        {
            var totalUsers = await _userManager.Users.CountAsync();
            var activeUsers = await _userManager.Users.CountAsync(u => u.IsActive);
            var totalRoles = await _roleManager.Roles.CountAsync();
            var totalPermissions = await _context.Permissions.CountAsync(p => p.IsActive);

            var recentActivities = await _activityService.GetSystemActivitiesAsync(1, 20);
            var activityStats = await _activityService.GetActivityStatsByActionAsync(DateTime.UtcNow.AddDays(-7));

            var viewModel = new AdminDashboardViewModel
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                TotalRoles = totalRoles,
                TotalPermissions = totalPermissions,
                RecentActivities = recentActivities.ToList(),
                WeeklyActivityStats = activityStats            };

            return View(viewModel);
        }

        #endregion
    }

    // Request models for role management
    public class CreateRoleRequest
    {
        public string Name { get; set; } = "";
    }

    public class EditRoleRequest
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }

    public class DeleteRoleRequest
    {
        public string Id { get; set; } = "";
    }

    public class RolePermissionRequest
    {
        public string RoleId { get; set; } = "";
        public string PermissionName { get; set; } = "";
    }
}
