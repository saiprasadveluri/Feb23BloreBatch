using FoodDelApp;
using FoodDelApp.Data;
using Microsoft.AspNetCore.Mvc;
using MMVCDemoApp1.Infra;
using MMVCDemoApp1.Models;

namespace MMVCDemoApp1.Controllers
{
    public class OwnerDashboardController : Controller
    {
        public IActionResult Index()
        {
            string strUserInfo=HttpContext.Session.GetString("LoggedInUser");
            if(strUserInfo== null )
            {
                return RedirectToAction("Login", "Account");
            }
            UserDTO luser=ObjectJsonHelper.GetFromJson<UserDTO>(strUserInfo);
            if(luser.RoleName!="OWNER")
            {
                return RedirectToAction("Login", "Account");
            }
            BusinessLayer bl = new();
            List<RestaurantDTO> MyRes= bl.ListMyRestaurants(luser);
            OwnerDashboardViewModel model= new OwnerDashboardViewModel();
            model.Restaurants = MyRes;
            return View(model);
        }
    }
}
