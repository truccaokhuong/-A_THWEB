using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;
using TH_WEB.Models.Authorization;

namespace TH_WEB.Data
{
    public static class AuthorizationSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await SeedPermissionsAsync(context);
            await SeedRolesAsync(roleManager);
            await SeedRolePermissionsAsync(context, roleManager);
            await SeedDefaultAdminUserAsync(userManager, roleManager);
        }

        private static async Task SeedPermissionsAsync(ApplicationDbContext context)
        {
            var permissions = new List<Permission>
            {
                // System permissions
                new Permission { Name = Permissions.SystemManagement, Description = "Manage system settings and configuration", Category = "System" },
                new Permission { Name = Permissions.UserManagement, Description = "Manage users and their profiles", Category = "System" },
                new Permission { Name = Permissions.RoleManagement, Description = "Manage roles and permissions", Category = "System" },
                new Permission { Name = Permissions.SystemConfig, Description = "Configure system parameters", Category = "System" },

                // Hotel permissions
                new Permission { Name = Permissions.HotelView, Description = "View hotel information", Category = "Hotel" },
                new Permission { Name = Permissions.HotelCreate, Description = "Create new hotels", Category = "Hotel" },
                new Permission { Name = Permissions.HotelEdit, Description = "Edit hotel information", Category = "Hotel" },
                new Permission { Name = Permissions.HotelDelete, Description = "Delete hotels", Category = "Hotel" },
                new Permission { Name = Permissions.HotelManage, Description = "Full hotel management access", Category = "Hotel" },

                // Room permissions
                new Permission { Name = Permissions.RoomView, Description = "View room information", Category = "Room" },
                new Permission { Name = Permissions.RoomCreate, Description = "Create new rooms", Category = "Room" },
                new Permission { Name = Permissions.RoomEdit, Description = "Edit room information", Category = "Room" },
                new Permission { Name = Permissions.RoomDelete, Description = "Delete rooms", Category = "Room" },
                new Permission { Name = Permissions.RoomManage, Description = "Full room management access", Category = "Room" },

                // Attraction permissions
                new Permission { Name = Permissions.AttractionView, Description = "View attraction information", Category = "Attraction" },
                new Permission { Name = Permissions.AttractionCreate, Description = "Create new attractions", Category = "Attraction" },
                new Permission { Name = Permissions.AttractionEdit, Description = "Edit attraction information", Category = "Attraction" },
                new Permission { Name = Permissions.AttractionDelete, Description = "Delete attractions", Category = "Attraction" },
                new Permission { Name = Permissions.AttractionManage, Description = "Full attraction management access", Category = "Attraction" },

                // Car rental permissions
                new Permission { Name = Permissions.CarRentalView, Description = "View car rental information", Category = "CarRental" },
                new Permission { Name = Permissions.CarRentalCreate, Description = "Create new car rentals", Category = "CarRental" },
                new Permission { Name = Permissions.CarRentalEdit, Description = "Edit car rental information", Category = "CarRental" },
                new Permission { Name = Permissions.CarRentalDelete, Description = "Delete car rentals", Category = "CarRental" },
                new Permission { Name = Permissions.CarRentalManage, Description = "Full car rental management access", Category = "CarRental" },

                // Travel package permissions
                new Permission { Name = Permissions.TravelPackageView, Description = "View travel package information", Category = "TravelPackage" },
                new Permission { Name = Permissions.TravelPackageCreate, Description = "Create new travel packages", Category = "TravelPackage" },
                new Permission { Name = Permissions.TravelPackageEdit, Description = "Edit travel package information", Category = "TravelPackage" },
                new Permission { Name = Permissions.TravelPackageDelete, Description = "Delete travel packages", Category = "TravelPackage" },
                new Permission { Name = Permissions.TravelPackageManage, Description = "Full travel package management access", Category = "TravelPackage" },

                // Booking permissions
                new Permission { Name = Permissions.BookingView, Description = "View booking information", Category = "Booking" },
                new Permission { Name = Permissions.BookingCreate, Description = "Create new bookings", Category = "Booking" },
                new Permission { Name = Permissions.BookingEdit, Description = "Edit booking information", Category = "Booking" },
                new Permission { Name = Permissions.BookingCancel, Description = "Cancel bookings", Category = "Booking" },
                new Permission { Name = Permissions.BookingManage, Description = "Full booking management access", Category = "Booking" },

                // Review permissions
                new Permission { Name = Permissions.ReviewView, Description = "View reviews", Category = "Review" },
                new Permission { Name = Permissions.ReviewCreate, Description = "Create reviews", Category = "Review" },
                new Permission { Name = Permissions.ReviewEdit, Description = "Edit reviews", Category = "Review" },
                new Permission { Name = Permissions.ReviewDelete, Description = "Delete reviews", Category = "Review" },
                new Permission { Name = Permissions.ReviewModerate, Description = "Moderate reviews", Category = "Review" },

                // Report permissions
                new Permission { Name = Permissions.ReportView, Description = "View reports", Category = "Report" },
                new Permission { Name = Permissions.ReportGenerate, Description = "Generate reports", Category = "Report" },
                new Permission { Name = Permissions.ReportExport, Description = "Export reports", Category = "Report" },

                // Finance permissions
                new Permission { Name = Permissions.FinanceView, Description = "View financial information", Category = "Finance" },
                new Permission { Name = Permissions.FinanceManage, Description = "Manage financial operations", Category = "Finance" },
                new Permission { Name = Permissions.PaymentProcess, Description = "Process payments", Category = "Finance" },

                // Content permissions
                new Permission { Name = Permissions.ContentView, Description = "View content", Category = "Content" },
                new Permission { Name = Permissions.ContentCreate, Description = "Create content", Category = "Content" },
                new Permission { Name = Permissions.ContentEdit, Description = "Edit content", Category = "Content" },
                new Permission { Name = Permissions.ContentDelete, Description = "Delete content", Category = "Content" },
                new Permission { Name = Permissions.ContentPublish, Description = "Publish content", Category = "Content" }
            };

            foreach (var permission in permissions)
            {
                if (!context.Permissions.Any(p => p.Name == permission.Name))
                {
                    context.Permissions.Add(permission);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[]
            {
                UserRoles.SuperAdmin,
                UserRoles.Admin,
                UserRoles.HotelManager,
                UserRoles.AttractionManager,
                UserRoles.CarRentalManager,
                UserRoles.TravelPackageManager,
                UserRoles.HotelStaff,
                UserRoles.AttractionStaff,
                UserRoles.CarRentalStaff,
                UserRoles.CustomerService,
                UserRoles.HotelOwner,
                UserRoles.AttractionOwner,
                UserRoles.CarRentalOwner,
                UserRoles.Customer,
                UserRoles.PremiumCustomer,
                UserRoles.Moderator,
                UserRoles.ContentCreator
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedRolePermissionsAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            // SuperAdmin - all permissions
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.SuperAdmin, new[]
            {
                Permissions.SystemManagement, Permissions.UserManagement, Permissions.RoleManagement, Permissions.SystemConfig,
                Permissions.HotelManage, Permissions.RoomManage, Permissions.AttractionManage, Permissions.CarRentalManage,
                Permissions.TravelPackageManage, Permissions.BookingManage, Permissions.ReviewModerate,
                Permissions.ReportView, Permissions.ReportGenerate, Permissions.ReportExport,
                Permissions.FinanceView, Permissions.FinanceManage, Permissions.PaymentProcess,
                Permissions.ContentView, Permissions.ContentCreate, Permissions.ContentEdit, Permissions.ContentDelete, Permissions.ContentPublish
            });

            // Admin - most permissions except system management
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.Admin, new[]
            {
                Permissions.UserManagement, Permissions.HotelManage, Permissions.RoomManage, Permissions.AttractionManage,
                Permissions.CarRentalManage, Permissions.TravelPackageManage, Permissions.BookingManage, Permissions.ReviewModerate,
                Permissions.ReportView, Permissions.ReportGenerate, Permissions.FinanceView,
                Permissions.ContentView, Permissions.ContentCreate, Permissions.ContentEdit, Permissions.ContentDelete, Permissions.ContentPublish
            });

            // Hotel Manager
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.HotelManager, new[]
            {
                Permissions.HotelView, Permissions.HotelEdit, Permissions.RoomManage, Permissions.BookingView, Permissions.BookingEdit,
                Permissions.ReviewView, Permissions.ReportView, Permissions.ContentView, Permissions.ContentEdit
            });

            // Hotel Staff
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.HotelStaff, new[]
            {
                Permissions.HotelView, Permissions.RoomView, Permissions.RoomEdit, Permissions.BookingView, Permissions.BookingEdit,
                Permissions.ReviewView, Permissions.ContentView
            });

            // Attraction Manager
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.AttractionManager, new[]
            {
                Permissions.AttractionView, Permissions.AttractionEdit, Permissions.BookingView, Permissions.BookingEdit,
                Permissions.ReviewView, Permissions.ReportView, Permissions.ContentView, Permissions.ContentEdit
            });

            // Car Rental Manager
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.CarRentalManager, new[]
            {
                Permissions.CarRentalView, Permissions.CarRentalEdit, Permissions.BookingView, Permissions.BookingEdit,
                Permissions.ReviewView, Permissions.ReportView, Permissions.ContentView, Permissions.ContentEdit
            });

            // Customer Service
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.CustomerService, new[]
            {
                Permissions.BookingView, Permissions.BookingEdit, Permissions.BookingCancel,
                Permissions.HotelView, Permissions.RoomView, Permissions.AttractionView, Permissions.CarRentalView,
                Permissions.ReviewView, Permissions.ContentView
            });

            // Customer
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.Customer, new[]
            {
                Permissions.HotelView, Permissions.RoomView, Permissions.AttractionView, Permissions.CarRentalView, Permissions.TravelPackageView,
                Permissions.BookingCreate, Permissions.BookingView, Permissions.ReviewCreate, Permissions.ReviewEdit, Permissions.ContentView
            });

            // Premium Customer
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.PremiumCustomer, new[]
            {
                Permissions.HotelView, Permissions.RoomView, Permissions.AttractionView, Permissions.CarRentalView, Permissions.TravelPackageView,
                Permissions.BookingCreate, Permissions.BookingView, Permissions.BookingEdit,
                Permissions.ReviewCreate, Permissions.ReviewEdit, Permissions.ContentView, Permissions.ContentCreate
            });

            // Moderator
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.Moderator, new[]
            {
                Permissions.ReviewView, Permissions.ReviewModerate, Permissions.ContentView, Permissions.ContentEdit, Permissions.ContentDelete
            });

            // Content Creator
            await GrantRolePermissionsAsync(context, roleManager, UserRoles.ContentCreator, new[]
            {
                Permissions.ContentView, Permissions.ContentCreate, Permissions.ContentEdit
            });
        }

        private static async Task GrantRolePermissionsAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, string roleName, string[] permissionNames)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null) return;

            foreach (var permissionName in permissionNames)
            {
                var permission = await context.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName);
                if (permission == null) continue;

                var existingRolePermission = await context.RolePermissions
                    .FirstOrDefaultAsync(rp => rp.RoleId == role.Id && rp.PermissionId == permission.Id);

                if (existingRolePermission == null)
                {
                    context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permission.Id,
                        GrantedBy = "System",
                        GrantedAt = DateTime.UtcNow
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedDefaultAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string adminEmail = "admin@thweb.com";
            const string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator",
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRoles.SuperAdmin);
                }
            }
            else
            {
                // Ensure admin user has SuperAdmin role
                if (!await userManager.IsInRoleAsync(adminUser, UserRoles.SuperAdmin))
                {
                    await userManager.AddToRoleAsync(adminUser, UserRoles.SuperAdmin);
                }
            }
        }
    }
}
