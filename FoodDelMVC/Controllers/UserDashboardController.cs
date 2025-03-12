using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryMVC.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
