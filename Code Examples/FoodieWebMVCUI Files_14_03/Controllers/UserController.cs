using Microsoft.AspNetCore.Mvc;

namespace MMVCDemoApp1.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
