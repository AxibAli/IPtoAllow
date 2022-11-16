using Microsoft.AspNetCore.Mvc;

namespace IpToAllow.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
