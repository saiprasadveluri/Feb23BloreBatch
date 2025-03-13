using FoodDelApp;
using FoodDelApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using MMVCDemoApp1.Infra;

namespace MMVCDemoApp1.Controllers
{
    public class OwnerDashboardController : Controller
    {
        public IActionResult Index()
        {
            string strUserInfo = HttpContext.Session.GetString("LoggedInUser");
            if (strUserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            UserDTO luser = ObjectJsonHelper.GetFromJson<UserDTO>(strUserInfo);
            if (luser.RoleName != "OWNER")
            {
                return RedirectToAction("Login", "Account");
            }

            BusinessLayer bl = new BusinessLayer();
            List<RestaurantDTO> RList = bl.GetAllRestaurants();

            bl.GetAllRestaurants();
            return View();
        }
    }
}
