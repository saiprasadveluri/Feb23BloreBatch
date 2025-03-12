using FoodDelApp;
using FoodDelApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MMVCDemoApp1.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string uemail, string upwd)
        {
            // Consume Model class for Authentication
            BusinessLayer bl = new BusinessLayer();
            bool status = bl.Authenticate(uemail, upwd);
            if (status)
            {
                return RedirectToAction("GetList", "ListUsers");
            }
            ViewBag.ErrMsg = "Invalid User";
            return View();
        }
    }
}
