using Microsoft.AspNetCore.Mvc;

namespace MMVCDemoApp1.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
