using TH_WEB.Models;
using TH_WEB.Models.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TH_WEB.ViewModels
{
    public class UserManagementViewModel
    {
        public ApplicationUser User { get; set; } = null!;
        public List<string> Roles { get; set; } = new();
        public UserProfile? Profile { get; set; }
        public UserActivity? LastActivity { get; set; }
    }

    public class UserDetailsViewModel
    {
        public ApplicationUser User { get; set; } = null!;
        public List<string> Roles { get; set; } = new();
        public UserProfile? Profile { get; set; }
        public List<Permission> Permissions { get; set; } = new();
        public List<UserActivity> RecentActivities { get; set; } = new();        public List<ResourceOwnership> ResourceOwnerships { get; set; } = new();
    }

    public class UserActivityViewModel
    {
        public string Activity { get; set; } = "";
        public string Action { get; set; } = "";
        public string Description { get; set; } = "";
        public string? EntityType { get; set; }
        public string? EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? IpAddress { get; set; }        public string Details { get; set; } = "";
    }    public class RoleManagementViewModel
    {
        public IdentityRole Role { get; set; } = null!;
        public int UserCount { get; set; }
        public List<Permission> Permissions { get; set; } = new();
    }    public class RoleDetailsViewModel
    {
        public IdentityRole Role { get; set; } = null!;
        public List<Permission> Permissions { get; set; } = new();
        public List<Permission> AvailablePermissions { get; set; } = new();
        public List<ApplicationUser> UsersInRole { get; set; } = new();
    }

    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalRoles { get; set; }
        public int TotalPermissions { get; set; }
        public List<UserActivity> RecentActivities { get; set; } = new();
        public Dictionary<string, int> UsersByRole { get; set; } = new();
        public Dictionary<string, int> ActivityByDay { get; set; } = new();
        public Dictionary<string, int> WeeklyActivityStats { get; set; } = new();
    }

    public class AssignRoleViewModel
    {
        public string UserId { get; set; } = "";
        public string RoleId { get; set; } = "";
        public List<string> AvailableRoles { get; set; } = new();
    }
}
