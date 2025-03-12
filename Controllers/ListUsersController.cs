using Microsoft.AspNetCore.Mvc;

namespace MMVCDemoApp1.Controllers
{
    public class ListUsersController : Controller
    {
        public ActionResult ListUsers()
        {
            // Add logic here to fetch and pass data to the view if needed
            return View();
        }
        public ActionResult GetList()
        {
            // You can add logic here to fetch and pass data to the view if needed
            return View();
        }
    }
}
