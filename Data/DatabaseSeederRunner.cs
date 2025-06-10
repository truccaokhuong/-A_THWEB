using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Data
{
    public static class DatabaseSeederRunner
    {
        public static async Task SeedAllDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Console.WriteLine("🚀 Starting database seeding...");

            try
            {                // 1. Seed roles and permissions first
                Console.WriteLine("📋 Seeding roles and permissions...");
                await AuthorizationSeeder.SeedAsync(context, roleManager, userManager);
                  // 2. Seed test accounts
                Console.WriteLine("👥 Seeding test accounts...");
                await TestAccountsSeeder.SeedTestAccountsAsync(userManager, roleManager, context);
                
                Console.WriteLine("✅ Database seeding completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error during seeding: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
