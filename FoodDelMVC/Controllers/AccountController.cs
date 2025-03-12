using Microsoft.AspNetCore.Mvc;
using FooddeliveryApp;
namespace FoodDeliveryMVC.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string uemail,string upwd)
        {
            BusinessLayerfd bl = new BusinessLayerfd();
            usersDTO Status = bl.AuthenticateUser(uemail, upwd);
            if (Status != null)
            {
                if (Status.roledal == usersDTO.role.admin) 
                { 
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "UserDashboard");
                }
         
            }
            return View();
        }
    }
}
