using Microsoft.AspNetCore.Identity;
using TH_WEB.Models;
using TH_WEB.Models.Authorization;

namespace TH_WEB.Data
{
    public static class TestAccountsSeeder
    {
        public static async Task SeedTestAccountsAsync(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            var testAccounts = new[]
            {
                // Admin Account (already exists)
                new { Email = "admin@thweb.com", Password = "Admin@123", Role = UserRoles.SuperAdmin, FirstName = "Super", LastName = "Admin" },
                
                // Management Roles
                new { Email = "hotelmanager@test.com", Password = "Test@123", Role = UserRoles.HotelManager, FirstName = "Hotel", LastName = "Manager" },
                new { Email = "attractionmanager@test.com", Password = "Test@123", Role = UserRoles.AttractionManager, FirstName = "Attraction", LastName = "Manager" },
                new { Email = "carmanager@test.com", Password = "Test@123", Role = UserRoles.CarRentalManager, FirstName = "Car Rental", LastName = "Manager" },
                new { Email = "travelmanager@test.com", Password = "Test@123", Role = UserRoles.TravelPackageManager, FirstName = "Travel", LastName = "Manager" },
                
                // Staff Roles
                new { Email = "hotelstaff@test.com", Password = "Test@123", Role = UserRoles.HotelStaff, FirstName = "Hotel", LastName = "Staff" },
                new { Email = "attractionstaff@test.com", Password = "Test@123", Role = UserRoles.AttractionStaff, FirstName = "Attraction", LastName = "Staff" },
                new { Email = "carstaff@test.com", Password = "Test@123", Role = UserRoles.CarRentalStaff, FirstName = "Car Rental", LastName = "Staff" },
                new { Email = "customerservice@test.com", Password = "Test@123", Role = UserRoles.CustomerService, FirstName = "Customer", LastName = "Service" },
                
                // Owner Roles
                new { Email = "hotelowner@test.com", Password = "Test@123", Role = UserRoles.HotelOwner, FirstName = "Hotel", LastName = "Owner" },
                new { Email = "attractionowner@test.com", Password = "Test@123", Role = UserRoles.AttractionOwner, FirstName = "Attraction", LastName = "Owner" },
                new { Email = "carowner@test.com", Password = "Test@123", Role = UserRoles.CarRentalOwner, FirstName = "Car Rental", LastName = "Owner" },
                
                // Customer Roles
                new { Email = "customer@test.com", Password = "Test@123", Role = UserRoles.Customer, FirstName = "Regular", LastName = "Customer" },
                new { Email = "premium@test.com", Password = "Test@123", Role = UserRoles.PremiumCustomer, FirstName = "Premium", LastName = "Customer" },
                
                // Special Roles
                new { Email = "moderator@test.com", Password = "Test@123", Role = UserRoles.Moderator, FirstName = "Content", LastName = "Moderator" },
                new { Email = "creator@test.com", Password = "Test@123", Role = UserRoles.ContentCreator, FirstName = "Content", LastName = "Creator" },
                
                // Additional Admin
                new { Email = "admin2@test.com", Password = "Test@123", Role = UserRoles.Admin, FirstName = "System", LastName = "Admin" }
            };

            foreach (var account in testAccounts)
            {
                // Skip if user already exists
                var existingUser = await userManager.FindByEmailAsync(account.Email);
                if (existingUser != null)
                {
                    Console.WriteLine($"User {account.Email} already exists. Skipping...");
                    continue;
                }

                // Ensure role exists
                if (!await roleManager.RoleExistsAsync(account.Role))
                {
                    Console.WriteLine($"Role {account.Role} does not exist. Creating...");
                    await roleManager.CreateAsync(new IdentityRole(account.Role));
                }

                // Create user
                var user = new ApplicationUser
                {
                    UserName = account.Email,
                    Email = account.Email,
                    EmailConfirmed = true,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user, account.Password);
                
                if (result.Succeeded)
                {
                    // Assign role
                    await userManager.AddToRoleAsync(user, account.Role);
                      // Create user profile
                    var profile = new UserProfile
                    {
                        UserId = user.Id,
                        Department = account.Role.Contains("Manager") ? "Management" : 
                                   account.Role.Contains("Staff") ? "Operations" : 
                                   account.Role.Contains("Customer") ? "Customer Services" : "General",
                        Position = account.Role,
                        Organization = "TH WEB Tourism",
                        HireDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(30, 365)),
                        IsVerified = true,
                        LastLoginAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 30)),
                        LoginCount = Random.Shared.Next(1, 50),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    
                    context.UserProfiles.Add(profile);
                    
                    Console.WriteLine($"✅ Created test account: {account.Email} with role {account.Role}");
                }
                else
                {
                    Console.WriteLine($"❌ Failed to create user {account.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            await context.SaveChangesAsync();
            Console.WriteLine("🎉 Test accounts seeding completed!");
        }
    }
}
