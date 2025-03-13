using FoodDelApp;
using FoodDelApp.Data;
using Microsoft.AspNetCore.Mvc;
using MMVCDemoApp1.Infra;
using MMVCDemoApp1.Models;

namespace MMVCDemoApp1.Controllers
{
    public class UserDashboardController : Controller
    {
        BusinessLayer bl;
        public UserDashboardController(BusinessLayer businessLayer)
        {
            bl = businessLayer;
        }
        public IActionResult Index()
        {
            UserDTO loggedUser = ObjectJsonHelper.GetFromSession<UserDTO>(HttpContext, "LoggedInUser");
            if (loggedUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<RestaurantDTO> Rests= bl.ListRestaurantsNearMe(loggedUser);
            UserDashboardViewModel model = new();
            model.Restaurants = Rests;

            List<OrderDTO> MyOrders = bl.GetOrdersByUser(loggedUser.UserId);
            model.MyOrders = MyOrders;
            return View(model);
        }
    }
}
