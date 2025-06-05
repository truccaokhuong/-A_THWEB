using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TH_WEB.Models;
using TH_WEB.Areas.Identity.ViewModels;
using System.Threading.Tasks;

namespace TH_WEB.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Identity/Account/LoginRegister
        [HttpGet]
        public IActionResult LoginRegister(string? tab = "signin")
        {
            ViewBag.ActiveTab = tab;
            return View();
        }

        // GET: /Identity/Account/Register
        // [HttpGet]
        // public IActionResult Register()
        // {
        //     return View();
        // }

        // POST: /Identity/Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // If registration is successful, return Ok with a success indicator.
                    return Ok(new { success = true, message = "Registration successful!" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, return BadRequest with ModelState errors.
            // This allows client-side JS to read specific errors.
            return BadRequest(ModelState);
        }

        // GET: /Identity/Account/Login
        // [HttpGet]
        // public IActionResult Login()
        // {
        //     return View();
        // }

        // POST: /Identity/Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "" }); // Redirect to home or a success page
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, something failed, return BadRequest with ModelState errors.
            // This allows client-side JS to read specific errors.
            return BadRequest(ModelState);
        }

        // POST: /Identity/Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" }); // Redirect to home page
        }
    }
} 