using FooddeliveryApp;
using FoodDeliveryMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryMVC.Controllers
{
    public class AdminDashboardController : Controller
    {
        public ActionResult Index()
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            List<usersDTO> users = bl.ListUsers();
            
            AdminDashboardBiewModel model = new AdminDashboardBiewModel();
            model.users = users;
            List<restaurantDTO> rest = bl.ListRest();
            model.rest = rest;

            return View(model);


        }
        public IActionResult DeleteUser(long Id)
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            bl.DelUser(Id);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteRestaurant(long Id)
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            bl.DeleteRest(Id);
            return RedirectToAction("Index");
        }
        

        public IActionResult AddRestaurant()
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            List<usersDTO> owners = new List<usersDTO>();
            owners = bl.getowners();
            AdminDashboardBiewModel model = new AdminDashboardBiewModel();
            restaurantDTO res = new restaurantDTO();
            model.users = owners;
            
            return View(model);
        }
        [HttpPost]
        public IActionResult AddRestaurant(restaurantDTO rest)
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            bl.Addrestaurant(rest);


            return RedirectToAction("Index");
        }

        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(usersDTO users)
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            bl.AddUser(users);
            return RedirectToAction("Index");
        }
        public IActionResult EditRestaurant(long Id)
        {
            ViewBag.Id = Id;
            BusinessLayerfd bl = new BusinessLayerfd();
            List<usersDTO> owners = new List<usersDTO>();
            owners = bl.getowners();
            AdminDashboardBiewModel model = new AdminDashboardBiewModel();
            
            List<restaurantDTO> rest = bl.ListRest();
            model.rest = rest;
            model.users = owners;

            return View(model);
            
        }
        [HttpPost]
        public IActionResult EditRestaurant(restaurantDTO rest)
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            bl.EditRest(rest);
            return RedirectToAction("Index");
        }

        public IActionResult EditUser(long Id)
        {
            ViewBag.Id = Id;
            BusinessLayerfd bl = new BusinessLayerfd();
            List<usersDTO> users = new List<usersDTO>();
            users = bl.ListUsers();
            AdminDashboardBiewModel model = new AdminDashboardBiewModel();
            model.users = users;
            return View(model);
        }

        [HttpPost]
        public IActionResult EditUser(usersDTO users)
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            bl.EditUser(users);
            return RedirectToAction("Index");
        }
    }
}
