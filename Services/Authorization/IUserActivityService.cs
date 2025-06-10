using TH_WEB.Models.Authorization;

namespace TH_WEB.Services.Authorization
{
    public interface IUserActivityService
    {
        // Activity logging
        Task LogActivityAsync(string userId, string action, string description, string? entityType = null, string? entityId = null, string? ipAddress = null, string? userAgent = null);
        Task LogLoginAsync(string userId, string? ipAddress = null, string? userAgent = null);
        Task LogLogoutAsync(string userId, string? ipAddress = null, string? userAgent = null);
        
        // Activity retrieval
        Task<IEnumerable<UserActivity>> GetUserActivitiesAsync(string userId, int page = 1, int pageSize = 50);
        Task<IEnumerable<UserActivity>> GetSystemActivitiesAsync(int page = 1, int pageSize = 50);
        Task<IEnumerable<UserActivity>> GetActivitiesByActionAsync(string action, int page = 1, int pageSize = 50);
        Task<IEnumerable<UserActivity>> GetActivitiesByEntityAsync(string entityType, string? entityId = null, int page = 1, int pageSize = 50);
        
        // Activity statistics
        Task<int> GetUserActivityCountAsync(string userId, DateTime? fromDate = null, DateTime? toDate = null);
        Task<Dictionary<string, int>> GetActivityStatsByActionAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<Dictionary<string, int>> GetActivityStatsByUserAsync(DateTime? fromDate = null, DateTime? toDate = null);
        
        // Cleanup
        Task CleanupOldActivitiesAsync(int daysToKeep = 90);
    }
}
