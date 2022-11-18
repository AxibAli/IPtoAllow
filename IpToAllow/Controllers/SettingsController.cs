using Microsoft.AspNetCore.Mvc;

namespace IpToAllow.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
