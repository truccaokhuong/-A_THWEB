using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH_WEB.Models.Authorization
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
    
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string RoleId { get; set; } = string.Empty;
        
        [Required]
        public int PermissionId { get; set; }
        
        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
        
        public string? GrantedBy { get; set; }
        
        // Navigation properties
        [ForeignKey("RoleId")]
        public virtual Microsoft.AspNetCore.Identity.IdentityRole Role { get; set; } = null!;
        
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; } = null!;
    }
    
    public class UserPermission
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public int PermissionId { get; set; }
        
        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? ExpiresAt { get; set; }
        
        public string? GrantedBy { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
        
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; } = null!;
    }
    
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Department { get; set; }
        
        [StringLength(50)]
        public string? Position { get; set; }
        
        [StringLength(100)]
        public string? Organization { get; set; }
        
        public DateTime? HireDate { get; set; }
        
        public string? ProfilePicture { get; set; }
        
        public bool IsVerified { get; set; } = false;
        
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
        
        public int LoginCount { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
        
        public virtual ICollection<UserActivity> Activities { get; set; } = new List<UserActivity>();
    }
    
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string EntityType { get; set; } = string.Empty;
        
        public string? EntityId { get; set; }
        
        public string? IpAddress { get; set; }
        
        public string? UserAgent { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }
    
    public class ResourceOwnership
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string ResourceType { get; set; } = string.Empty; // Hotel, Attraction, CarRental, TravelPackage
        
        [Required]
        public int ResourceId { get; set; }
        
        [StringLength(50)]
        public string OwnershipType { get; set; } = "Owner"; // Owner, Manager, Staff
        
        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? ExpiresAt { get; set; }
        
        public string? GrantedBy { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
