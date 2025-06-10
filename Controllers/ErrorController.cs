using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TH_WEB.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
            if (exceptionHandlerPathFeature?.Error != null)
            {
                // Log the error here if needed
                ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
                ViewBag.RequestPath = exceptionHandlerPathFeature.Path;
            }

            return View();
        }

        [Route("Error/AccessDenied")]
        public IActionResult AccessDenied()
        {
            // Get the original request path
            var returnUrl = Request.Query["ReturnUrl"].FirstOrDefault();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                HttpContext.Items["AttemptedAction"] = returnUrl;
            }

            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        [Route("Error/404")]
        public IActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        [Route("Error/403")]
        public IActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
