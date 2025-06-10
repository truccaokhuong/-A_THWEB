using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TH_WEB.Models;
using TH_WEB.Areas.Identity.ViewModels;
using TH_WEB.Services.Authorization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TH_WEB.Areas.Identity.Controllers
{
    [Area("Identity")]    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserActivityService _activityService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserActivityService activityService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _activityService = activityService;
            _logger = logger;
        }        // GET: /Identity/Account/LoginRegister
        [HttpGet]
        public async Task<IActionResult> LoginRegister(string? tab = "signin")
        {
            ViewBag.ActiveTab = tab;
            
            try
            {
                // Get external authentication providers
                var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                ViewBag.ExternalLogins = externalLogins;
                
                _logger.LogInformation("LoginRegister action called successfully. Tab: {Tab}, External logins count: {Count}", tab, externalLogins.Count);
                
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LoginRegister action: {Message}", ex.Message);
                
                // Fallback: return simple view without external logins
                ViewBag.ExternalLogins = new List<Microsoft.AspNetCore.Authentication.AuthenticationScheme>();
                return View();
            }
        }

        // Simple test action to verify routing
        [HttpGet]
        public IActionResult Test()
        {
            return Json(new { 
                Message = "AccountController is working!", 
                Area = "Identity",
                Controller = "Account",
                Action = "Test",
                Time = DateTime.Now
            });
        }

        // Simple login page for testing
        [HttpGet]
        public async Task<IActionResult> TestLogin()
        {
            ViewBag.ActiveTab = "signin";
            
            try
            {
                var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                ViewBag.ExternalLogins = externalLogins;
                
                _logger.LogInformation("TestLogin action called. External logins count: {Count}", externalLogins.Count);
                
                return View("LoginRegister_Simple");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TestLogin action: {Message}", ex.Message);
                ViewBag.ExternalLogins = new List<Microsoft.AspNetCore.Authentication.AuthenticationScheme>();
                return View("LoginRegister_Simple");
            }
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
        }        // POST: /Identity/Account/Logout (preferred method)  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("=== LOGOUT POST METHOD CALLED ===");
            _logger.LogInformation("User Identity Name: {UserName}", User.Identity?.Name ?? "null");
            _logger.LogInformation("User IsAuthenticated: {IsAuth}", User.Identity?.IsAuthenticated ?? false);
            _logger.LogInformation("Request Method: {Method}", Request.Method);
            _logger.LogInformation("Request Path: {Path}", Request.Path);
            
            try
            {
                // Log the logout activity if user was signed in
                if (User.Identity?.IsAuthenticated == true)
                {
                    var userId = _userManager.GetUserId(User);
                    _logger.LogInformation("Getting user ID: {UserId}", userId ?? "null");
                    
                    if (!string.IsNullOrEmpty(userId))
                    {
                        _logger.LogInformation("Logging activity for user: {UserId}", userId);
                        await _activityService.LogActivityAsync(
                            userId,
                            "Logout",
                            "User logged out via POST",
                            "Authentication",
                            null
                        );
                        _logger.LogInformation("Activity logged successfully");
                    }
                }

                _logger.LogInformation("Calling SignOutAsync...");
                await _signInManager.SignOutAsync();
                _logger.LogInformation("SignOutAsync completed successfully");
                
                _logger.LogInformation("Redirecting to Home/Index");
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout process: {Message}", ex.Message);
                // Still try to redirect even if there was an error
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }// JSON API endpoint for Login (for AJAX requests)
        [HttpPost]
        [Route("Identity/Account/LoginJson")]
        public async Task<IActionResult> LoginJson([FromBody] LoginJsonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState });
            }

            try
            {
                var result = await _signInManager.PasswordSignInAsync(
                    request.Email, 
                    request.Password, 
                    request.RememberMe, 
                    lockoutOnFailure: false);                if (result.Succeeded)
                {
                    // Log successful login
                    var user = await _userManager.FindByEmailAsync(request.Email);
                    if (user != null)
                    {
                        await _activityService.LogLoginAsync(
                            user.Id, 
                            HttpContext.Connection.RemoteIpAddress?.ToString(),
                            HttpContext.Request.Headers.UserAgent.ToString()
                        );
                    }

                    return Ok(new { success = true, redirectUrl = "/" });
                }

                if (result.IsLockedOut)
                {
                    return BadRequest(new { message = "User account locked out." });
                }

                if (result.IsNotAllowed)
                {
                    return BadRequest(new { message = "User is not allowed to sign in." });
                }

                return BadRequest(new { message = "Invalid login attempt." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred during login. Please try again." });
            }
        }        // GET: /Identity/Account/Logout (for direct URL access)
        [HttpGet]
        [ActionName("Logout")]
        public async Task<IActionResult> LogoutGet()
        {
            _logger.LogInformation("=== LOGOUT GET METHOD CALLED ===");
            _logger.LogInformation("User Identity Name: {UserName}", User.Identity?.Name ?? "null");
            _logger.LogInformation("User IsAuthenticated: {IsAuth}", User.Identity?.IsAuthenticated ?? false);
            _logger.LogInformation("Request Method: {Method}", Request.Method);
            _logger.LogInformation("Request Path: {Path}", Request.Path);
            
            try
            {
                // For security, we'll sign out the user even on GET request
                // but ideally logout should be POST only
                _logger.LogInformation("Calling SignOutAsync...");
                await _signInManager.SignOutAsync();
                _logger.LogInformation("SignOutAsync completed successfully");
                
                // Log the logout activity if user was signed in
                if (User.Identity?.IsAuthenticated == true)
                {
                    var userId = _userManager.GetUserId(User);
                    _logger.LogInformation("Getting user ID: {UserId}", userId ?? "null");
                    
                    if (!string.IsNullOrEmpty(userId))
                    {
                        _logger.LogInformation("Logging activity for user: {UserId}", userId);
                        await _activityService.LogActivityAsync(
                            userId,
                            "Logout",
                            "User logged out via GET",
                            "Authentication",
                            null
                        );
                        _logger.LogInformation("Activity logged successfully");
                    }
                }
                
                _logger.LogInformation("Redirecting to Home/Index");
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout GET process: {Message}", ex.Message);
                // Still try to redirect even if there was an error
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        // JSON API endpoint for Register (for AJAX requests)
        [HttpPost]
        [Route("Identity/Account/RegisterJson")]
        public async Task<IActionResult> RegisterJson([FromBody] RegisterJsonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState });
            }

            try
            {
                var user = new ApplicationUser 
                { 
                    UserName = request.Email, 
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailConfirmed = true, // You might want to require email confirmation
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                
                var result = await _userManager.CreateAsync(user, request.Password);                if (result.Succeeded)
                {
                    // Assign default Customer role
                    await _userManager.AddToRoleAsync(user, "Customer");
                    
                    // Log successful registration
                    await _activityService.LogActivityAsync(
                        user.Id,
                        "Register",
                        "User registered via JSON API",
                        "Authentication",
                        null
                    );

                    // Optionally sign in the user immediately
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Ok(new { success = true, message = "Registration successful!", redirectUrl = "/" });
                }

                // If registration failed, return validation errors
                var errors = new Dictionary<string, List<string>>();
                foreach (var error in result.Errors)
                {
                    var key = error.Code.Contains("Password") ? "password" : 
                             error.Code.Contains("Email") ? "email" : "general";
                    
                    if (!errors.ContainsKey(key))
                        errors[key] = new List<string>();
                    
                    errors[key].Add(error.Description);
                }

                return BadRequest(new { errors = errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during JSON registration: {Message}", ex.Message);
                return BadRequest(new { message = "An error occurred during registration. Please try again." });
            }
        }
    }

    // Request models for JSON API
    public class LoginJsonRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool RememberMe { get; set; } = false;
    }    public class RegisterJsonRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = "";
        
        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
        public string FirstName { get; set; } = "";
        
        [Required(ErrorMessage = "Last name is required")]  
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
        public string LastName { get; set; } = "";
        
        public bool AgreeTerms { get; set; } = false;
        public bool Newsletter { get; set; } = false;
    }
}