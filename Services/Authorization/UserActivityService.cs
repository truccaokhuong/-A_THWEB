using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models.Authorization;

namespace TH_WEB.Services.Authorization
{
    public class UserActivityService : IUserActivityService
    {
        private readonly ApplicationDbContext _context;

        public UserActivityService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Activity Logging

        public async Task LogActivityAsync(string userId, string action, string description, string? entityType = null, string? entityId = null, string? ipAddress = null, string? userAgent = null)
        {
            var activity = new UserActivity
            {
                UserId = userId,
                Action = action,
                Description = description,
                EntityType = entityType ?? string.Empty,
                EntityId = entityId,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserActivities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task LogLoginAsync(string userId, string? ipAddress = null, string? userAgent = null)
        {
            await LogActivityAsync(userId, "Login", "User logged in", "Authentication", null, ipAddress, userAgent);
            
            // Update user profile login statistics
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);
            if (userProfile != null)
            {
                userProfile.LastLoginAt = DateTime.UtcNow;
                userProfile.LoginCount++;
                userProfile.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task LogLogoutAsync(string userId, string? ipAddress = null, string? userAgent = null)
        {
            await LogActivityAsync(userId, "Logout", "User logged out", "Authentication", null, ipAddress, userAgent);
        }

        #endregion

        #region Activity Retrieval

        public async Task<IEnumerable<UserActivity>> GetUserActivitiesAsync(string userId, int page = 1, int pageSize = 50)
        {
            return await _context.UserActivities
                .Where(ua => ua.UserId == userId)
                .OrderByDescending(ua => ua.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserActivity>> GetSystemActivitiesAsync(int page = 1, int pageSize = 50)
        {
            return await _context.UserActivities
                .Include(ua => ua.User)
                .OrderByDescending(ua => ua.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserActivity>> GetActivitiesByActionAsync(string action, int page = 1, int pageSize = 50)
        {
            return await _context.UserActivities
                .Include(ua => ua.User)
                .Where(ua => ua.Action == action)
                .OrderByDescending(ua => ua.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserActivity>> GetActivitiesByEntityAsync(string entityType, string? entityId = null, int page = 1, int pageSize = 50)
        {
            var query = _context.UserActivities
                .Include(ua => ua.User)
                .Where(ua => ua.EntityType == entityType);

            if (!string.IsNullOrEmpty(entityId))
            {
                query = query.Where(ua => ua.EntityId == entityId);
            }

            return await query
                .OrderByDescending(ua => ua.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        #endregion

        #region Activity Statistics

        public async Task<int> GetUserActivityCountAsync(string userId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _context.UserActivities.Where(ua => ua.UserId == userId);

            if (fromDate.HasValue)
                query = query.Where(ua => ua.CreatedAt >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(ua => ua.CreatedAt <= toDate.Value);

            return await query.CountAsync();
        }

        public async Task<Dictionary<string, int>> GetActivityStatsByActionAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _context.UserActivities.AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(ua => ua.CreatedAt >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(ua => ua.CreatedAt <= toDate.Value);

            return await query
                .GroupBy(ua => ua.Action)
                .Select(g => new { Action = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Action, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetActivityStatsByUserAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _context.UserActivities
                .Include(ua => ua.User)
                .AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(ua => ua.CreatedAt >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(ua => ua.CreatedAt <= toDate.Value);

            return await query
                .GroupBy(ua => ua.User.UserName)
                .Select(g => new { UserName = g.Key ?? "Unknown", Count = g.Count() })
                .ToDictionaryAsync(x => x.UserName, x => x.Count);
        }

        #endregion

        #region Cleanup

        public async Task CleanupOldActivitiesAsync(int daysToKeep = 90)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);
            
            var oldActivities = _context.UserActivities
                .Where(ua => ua.CreatedAt < cutoffDate);

            _context.UserActivities.RemoveRange(oldActivities);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
