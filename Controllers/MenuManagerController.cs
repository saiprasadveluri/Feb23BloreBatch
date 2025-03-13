using FoodDelApp;
using FoodDelApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using MMVCDemoApp1.Models;

namespace MMVCDemoApp1.Controllers
{
    public class MenuManagerController : Controller
    {
        BusinessLayer bl;
        public MenuManagerController(BusinessLayer businessLayer)
        {
            bl = businessLayer;
        }
        public IActionResult Index(int Id)
        {

            List<MenuItemDTO> menuItems = bl.GetRestaurentMenu(Id);
            MenuListViewModel mmodel = new MenuListViewModel();
            mmodel.Items = menuItems;
            return View(mmodel);
        }
    }
}