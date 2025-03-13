using FoodDelApp;
using FoodDelApp.Data;
using Microsoft.AspNetCore.Mvc;
using MMVCDemoApp1.Infra;
using MMVCDemoApp1.Models;
using System.Security.Cryptography;

namespace MMVCDemoApp1.Controllers
{
    public class OrdersController : Controller
    {
        BusinessLayer bl;
        public OrdersController(BusinessLayer businessLayer)
        {
            bl = businessLayer;
        }
        public IActionResult Index(long Id)
        {
            ObjectJsonHelper.RemoveSessionItem(HttpContext, "OrderData");
            List<MenuItemDTO> menuItems = bl.GetRestaurentMenu(Id);
            OrderViewModel omodel = new OrderViewModel();
            omodel.MenuItems = menuItems;
            omodel.RID = Id;
            return View(omodel);
        }

        [HttpPost]
        public IActionResult OrderSummary(long RID, string[] mids)
        {
            List<MenuItemDTO> menuItems = bl.GetRestaurentMenu(RID);
            OrderSummaryViewModel model = new();
            List<OrderLineData> OrderItems = new List<OrderLineData>();
            model.RID = RID;
            double OrderTotal = 0.0;
            foreach (string mid in mids)
            {
                string QtyId = $"qty_{mid}";
                string qty = HttpContext.Request.Form[QtyId][0];
                int nQty = int.Parse(qty);
                OrderLineData old = new OrderLineData();
                if (nQty > 0)
                {

                    long menuId = long.Parse(mid);
                    string Menuname = menuItems.Where(m => m.MenuID == menuId).Select(m => m.MenuName).FirstOrDefault();
                    double Price = menuItems.Where(m => m.MenuID == menuId).Select(m => m.UnitPrice).FirstOrDefault();
                    old.MenuId = menuId;
                    old.MenuName = Menuname;
                    old.QTY = nQty;
                    old.UnitTotal = nQty * Price;
                    OrderTotal += old.UnitTotal;
                    OrderItems.Add(old);
                }
            }
            if (OrderItems.Count > 0)
            {
                model.OrderdLineItems = OrderItems;
                model.OrderTotal = OrderTotal;
                ObjectJsonHelper.AddToSession(HttpContext, "OrderData", model);
                return View(model);
            }
            return RedirectToAction("Index", new { Id = RID });
        }

        public IActionResult ConfirmOrder()
        {
            OrderSummaryViewModel model = ObjectJsonHelper.GetFromSession<OrderSummaryViewModel>(HttpContext, "OrderData");
            UserDTO loggedUser = ObjectJsonHelper.GetFromSession<UserDTO>(HttpContext, "LoggedInUser");
            if (model != null && loggedUser != null)
            {
                bool Status = bl.PlaceOrder(loggedUser, model.RID, model.OrderdLineItems);
                return RedirectToAction("Index", "UserDashboard");
            }
            return RedirectToAction("Index", "UserDashboard");
        }
    }
}