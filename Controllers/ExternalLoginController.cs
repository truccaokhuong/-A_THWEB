using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TH_WEB.Models;

namespace TH_WEB.Controllers
{
    public class ExternalLoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ExternalLoginController> _logger;

        public ExternalLoginController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            _logger.LogInformation("Starting external login for provider: {Provider}", provider);
            
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "ExternalLogin", new { returnUrl });
            _logger.LogInformation("Redirect URL: {RedirectUrl}", redirectUrl);
            
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            
            // Add additional properties to help with OAuth state
            properties.SetParameter("prompt", "select_account");
            
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
              if (remoteError != null)
            {
                _logger.LogError("Error from external provider: {RemoteError}", remoteError);
                TempData["ErrorMessage"] = $"Error from external provider: {remoteError}";
                return RedirectToAction("LoginRegister", "Account", new { area = "Identity" });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                _logger.LogError("Error loading external login information.");
                TempData["ErrorMessage"] = "Error loading external login information.";
                return RedirectToAction("LoginRegister", "Account", new { area = "Identity" });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
              if (result.IsLockedOut)
            {
                return RedirectToAction("Lockout", "Account", new { area = "Identity" });
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);
                var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "";
                var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? "";

                if (email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = email,
                            Email = email,
                            FirstName = firstName,
                            LastName = lastName,
                            EmailConfirmed = true,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                        var createResult = await _userManager.CreateAsync(user);
                        if (createResult.Succeeded)
                        {
                            // Assign default role
                            await _userManager.AddToRoleAsync(user, "Customer");
                            
                            var addLoginResult = await _userManager.AddLoginAsync(user, info);
                            if (addLoginResult.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                                return LocalRedirect(returnUrl);
                            }
                        }
                        foreach (var error in createResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        // User exists, add the external login
                        var addLoginResult = await _userManager.AddLoginAsync(user, info);
                        if (addLoginResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                }                TempData["ErrorMessage"] = "Unable to load user information from external provider.";
                return RedirectToAction("LoginRegister", "Account", new { area = "Identity" });
            }
        }
    }
}
