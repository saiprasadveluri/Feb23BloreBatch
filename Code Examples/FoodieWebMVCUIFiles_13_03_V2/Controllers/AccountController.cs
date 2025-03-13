using Microsoft.AspNetCore.Mvc;
using FoodDelApp;
using FoodDelApp.Data;
using MMVCDemoApp1.Infra;

namespace MMVCDemoApp1.Controllers
{
    //http:\\localhost:4235\Account\
    public class AccountController : Controller
    {

        //http:\\localhost:4235\Account\Login
        //Action Method
        public ActionResult Login()
        {
            

            return View();
        }

        //Accept the Form Values from Browser.
        //Modelbinding In Action.
        [HttpPost]
        public ActionResult Login(string uemail,string upwd)
        {
            //Consume Model class for Authentication.
            BusinessLayer bl = new BusinessLayer();
            bool Status= bl.Authenticate(uemail, upwd,out UserDTO loggedInUser);
            if(Status==true)
            {
                string strLoggedInUser= ObjectJsonHelper.ToJson(loggedInUser);
                HttpContext.Session.SetString("LoggedInUser", strLoggedInUser);
               if(loggedInUser.RoleName=="ADMIN")
                return RedirectToAction("Index", "AdminDashboard");
                else if (loggedInUser.RoleName == "OWNER")
                    return RedirectToAction("Index", "OwnerDashboard");
                else
                return RedirectToAction("Index", "UserDashboard");
            }
            ViewData["Err"]= "Error In Login";
            //ViewBag.ErrMsg = "Error In Login";            
            return View() ;
        }
    }
}
