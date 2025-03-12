using FoodDelApp;
using FoodDelApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using MMVCDemoApp1.Models;
//using MMVCDemoApp1.Data;

namespace MMVCDemoApp1.Controllers
{
    public class AdminDashboardController : Controller
    {





        public IActionResult Index()
        {
            BusinessLayer bl = new BusinessLayer();

            List<UserDTO> usersList = bl.GetAllUsers();
            AdminDashboardViewModel model = new AdminDashboardViewModel();
            model.UserList = usersList;

            List<RestaurantDTO> RList = bl.GetAllRestaurants();
            model.RList = RList;
            return View(model);
        }

        [HttpGet]
        public IActionResult AddNewUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewUser(UserDTO user)
        {
            BusinessLayer bl = new BusinessLayer();
            bl.AddNewUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddNewRestaurant()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRestaurant(RestaurantDTO restaurant)
        {
            BusinessLayer bl = new BusinessLayer();
            bl.AddNewRestaurant(restaurant);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            BusinessLayer bl = new BusinessLayer();
            UserDTO user = bl.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(UserDTO user)
        {
            BusinessLayer bl = new BusinessLayer();
            bl.UpdateUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditRestaurant(int id)
        {
            BusinessLayer bl = new BusinessLayer();
            RestaurantDTO restaurant = bl.GetRestaurantById(id);
            return View(restaurant);
        }

        [HttpPost]
        public IActionResult EditRestaurant(RestaurantDTO restaurant)
        {
            BusinessLayer bl = new BusinessLayer();
            bool isUpdated = bl.UpdateRestaurant(restaurant);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the error case, e.g., show an error message
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View(restaurant);
            }
        }


        
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            BusinessLayer bl = new BusinessLayer();
            bl.DeleteUser(userId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteRestaurant(int Id)
        {
            BusinessLayer bl = new BusinessLayer();
            bl.DeleteRestaurant(Id);
            return RedirectToAction("Index");
        }
    }
}

