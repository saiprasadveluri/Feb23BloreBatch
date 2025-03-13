
using Microsoft.AspNetCore.Mvc;

namespace MMVCDemoApp1.Controllers
{
    public class ListUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Action Methods
        public IActionResult GetList()
        {
            return View();
        }
    }
}
