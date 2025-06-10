using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TH_WEB.Attributes;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Controllers
{
    [Authorize]
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceProvider _serviceProvider;

        public SeedController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IServiceProvider serviceProvider)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _serviceProvider = serviceProvider;
        }

        [RequirePermission("system.management")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [RequirePermission("system.management")]
        public async Task<IActionResult> SeedTestAccounts()
        {
            try
            {
                await TestAccountsSeeder.SeedTestAccountsAsync(_userManager, _roleManager, _context);
                TempData["Success"] = "✅ Test accounts have been seeded successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error seeding test accounts: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [RequirePermission("system.management")]
        public async Task<IActionResult> SeedAll()
        {
            try
            {
                await DatabaseSeederRunner.SeedAllDataAsync(_serviceProvider);
                TempData["Success"] = "✅ All data has been seeded successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error seeding data: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [RequirePermission("system.management")]
        public async Task<IActionResult> TestAccountsList()
        {
            var testEmails = new[]
            {
                "admin@thweb.com",
                "hotelmanager@test.com",
                "attractionmanager@test.com",
                "carmanager@test.com",
                "travelmanager@test.com",
                "hotelstaff@test.com",
                "attractionstaff@test.com",
                "carstaff@test.com",
                "customerservice@test.com",
                "hotelowner@test.com",
                "attractionowner@test.com",
                "carowner@test.com",
                "customer@test.com",
                "premium@test.com",
                "moderator@test.com",
                "creator@test.com",
                "admin2@test.com"
            };

            var users = new List<object>();
            
            foreach (var email in testEmails)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    users.Add(new
                    {
                        Email = user.Email,
                        Name = $"{user.FirstName} {user.LastName}",
                        Roles = string.Join(", ", roles),
                        IsActive = user.IsActive,
                        EmailConfirmed = user.EmailConfirmed
                    });
                }
                else
                {
                    users.Add(new
                    {
                        Email = email,
                        Name = "Not Created",
                        Roles = "N/A",
                        IsActive = false,
                        EmailConfirmed = false
                    });
                }
            }

            ViewBag.TestAccounts = users;
            return View();
        }
    }
}
