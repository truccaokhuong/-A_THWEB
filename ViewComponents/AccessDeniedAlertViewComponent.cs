using Microsoft.AspNetCore.Mvc;

namespace TH_WEB.ViewComponents
{
    public class AccessDeniedAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Check if this is an access denied page
            var isAccessDenied = ViewContext.RouteData.Values["action"]?.ToString() == "AccessDenied" ||
                                ViewContext.ViewData["Title"]?.ToString() == "Access Denied";

            if (isAccessDenied)
            {
                return View();
            }

            return Content(string.Empty);
        }
    }
}
