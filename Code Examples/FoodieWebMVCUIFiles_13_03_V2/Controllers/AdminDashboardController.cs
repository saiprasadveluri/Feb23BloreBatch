using FoodDelApp;
using FoodDelApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MMVCDemoApp1.Infra;
using MMVCDemoApp1.Models;

namespace MMVCDemoApp1.Controllers
{
    public class AdminDashboardController : Controller
    {
        BusinessLayer bl;
        public AdminDashboardController(BusinessLayer businessLayer) 
        { 
            bl= businessLayer;
        }

        public IActionResult Index()
        {
            //Verify the User Role.
            string strloggedInUser = HttpContext.Session.GetString("LoggedInUser");
            UserDTO loggedInUser=ObjectJsonHelper.GetFromJson<UserDTO>(strloggedInUser);
            if(loggedInUser.RoleName!= "ADMIN")
            {
                return RedirectToAction("Login", "Account");
            }
            //BusinessLayer bl = new BusinessLayer();
            List<UserDTO> usersList=bl.GetAllusers();
            AdminDashboardViewModel model= new AdminDashboardViewModel();
            model.UserList = usersList;

            List<RestaurantDTO> RList = bl.GetAllRestaurants();
            model.Restaurants = RList;

            return View(model);            
        }

        public IActionResult DeleteRestaurant(long Id)
        {
            //BusinessLayer bl = new BusinessLayer();
            bl.DeleteRestaurant(Id);
            return RedirectToAction("Index");
        }
        
        public IActionResult AddRestaurant()
        {
            AddRestaurantInputModel model = new AddRestaurantInputModel();
           
            List<SelectListItem> ownerList = FetchRestData();
            model.owners=ownerList;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddRestaurant(AddRestaurantInputModel inp)
        {
            RestaurantDTO newRest = new RestaurantDTO()
            {
                Name=inp.rnm,
                Location=inp.loc,
                OwnerId=inp.rowner
            };
          
           // BusinessLayer bl=new BusinessLayer();
            bool Status=bl.AddNewRestaurant(newRest);
            if (Status)
            {
                return RedirectToAction("Index");
            }
            inp.owners=FetchRestData();
            return View(inp);
        }

        public ActionResult AddNewUser()
        {
            AddNewUserViewModel model = new();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddNewUser(AddNewUserViewModel inp)
        {
            UserDTO newUser = new UserDTO()
            {
                Name=inp.DisplayName,
                Email=inp.Email,
                Password=inp.Password,
                RoleName=inp.RoleName,
                Location=inp.Location
            };
            //BusinessLayer bl = new BusinessLayer();
            bool Status=bl.AddNewUser(newUser);
            if (Status==true)
            {
                return RedirectToAction("Index");
            }
            return View(inp);
        }


        public ActionResult EditUser(long Id)
        {
            //BusinessLayer bl = new BusinessLayer();
            UserDTO curUser=bl.GetUserById(Id);
            if(curUser==null)
            {
                return RedirectToAction("Index");
            }
            AddNewUserViewModel model = new AddNewUserViewModel()
            {
                UserId=curUser.UserId,
                DisplayName= curUser.Name,
                Email=curUser.Email,
                RoleName=curUser.RoleName,
                Location=curUser.Location
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(AddNewUserViewModel model)
        {
            //BusinessLayer bl = new BusinessLayer();
            UserDTO tempUser = new UserDTO()
            {
               UserId = model.UserId,
               Name=model.DisplayName,
               RoleName = model.RoleName,
               Location=model.Location
            };
            bool Result=bl.EditUser(tempUser);
            if (Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        private List<SelectListItem> FetchRestData()
        {
            //BusinessLayer bl = new BusinessLayer();
            List<UserDTO> ownersList = bl.GetRestaurantOwnersList();
            return ownersList
                .Select(obj => new SelectListItem() {Text=obj.Name,Value=obj.UserId.ToString()})
                .ToList();
            
        }
    }
}
