using Microsoft.AspNetCore.Mvc;

namespace Fiery.Api.Identity.UI
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
