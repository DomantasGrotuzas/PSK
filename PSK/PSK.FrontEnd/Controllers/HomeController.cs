using Microsoft.AspNetCore.Mvc;

namespace PSK.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return Redirect("/Identity/Account/Login");
            return View();
        }
    }
}