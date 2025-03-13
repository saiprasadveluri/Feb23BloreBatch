
using FoodDelApp.Data;
using FoodDelApp;
using Microsoft.AspNetCore.Mvc;
using MMVCDemoApp1.Infra;
using MMVCDemoApp1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Models;


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
            //Verify the User Role.
            string strloggedInUser = HttpContext.Session.GetString("LoggedInUser");
            UserDTO loggedInUser = ObjectJsonHelper.GetFromJson<UserDTO>(strloggedInUser);
            if (loggedInUser.RoleName != "User")
            {
                return RedirectToAction("Login", "Account");
            }
            BusinessLayer bl = new BusinessLayer();
            List<UserDTO> usersList = bl.GetAllusers();
            AdminDashboardViewModel model = new AdminDashboardViewModel();
            model.UserList = usersList;

            List<RestaurantDTO> RList = bl.GetAllRestaurants();
            model.Restaurants = RList;

            return View(model);
        }

        public IActionResult DeleteUser(long Id)
        {
            //BusinessLayer bl = new BusinessLayer();
            bl.DeleteUser(Id);
            return RedirectToAction("Index");
        }

            public IActionResult ViewOrder(long orderId)
            {
                string strloggedInUser = HttpContext.Session.GetString("LoggedInUser");
                UserDTO loggedInUser = ObjectJsonHelper.GetFromJson<UserDTO>(strloggedInUser);
                if (loggedInUser == null || loggedInUser.RoleName != "USER")
                {
                    return RedirectToAction("Login", "Account");
                }

                OrderDTO order = bl.GetOrdersByUser(loggedInUser.UserId).Find(o => o.OrderId == orderId);
                if (order == null)
                {
                    return RedirectToAction("Index");
                }

                return View(order);
            }

            public IActionResult EditProfile()
            {
                string strloggedInUser = HttpContext.Session.GetString("LoggedInUser");
                UserDTO loggedInUser = ObjectJsonHelper.GetFromJson<UserDTO>(strloggedInUser);
                if (loggedInUser == null || loggedInUser.RoleName != "USER")
                {
                    return RedirectToAction("Login", "Account");
                }

            AddNewUserViewModel model = new AddNewUserViewModel
            {
                    UserId = loggedInUser.UserId,
                    DisplayName = loggedInUser.Name,
                    Email = loggedInUser.Email,
                    Location = loggedInUser.Location
                };

                return View(model);
            }

            [HttpPost]
            public IActionResult EditProfile(EditProfileViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                UserDTO updatedUser = new UserDTO
                {
                    UserId = model.UserId,
                    Name = model.DisplayName,
                    Email = model.Email,
                    Location = model.Location
                };

                bool result = bl.EditUser(updatedUser);
                if (result)
                {
                    HttpContext.Session.SetString("LoggedInUser", ObjectJsonHelper.ToJson(updatedUser));
                    return RedirectToAction("Index");
                }

                return View(model);
            }



      

    public IActionResult AddUser()
        {
              AddNewUserViewModel model = new AddNewUserViewModel();

         
            return View(model);
        }
        public ActionResult AddNewUser(AddNewUserViewModel inp)
        {
            UserDTO newUser = new UserDTO()
            {
                Name = inp.DisplayName,
                Email = inp.Email,
                Password = inp.Password,
                RoleName = inp.RoleName,
                Location = inp.Location
            };
            //BusinessLayer bl = new BusinessLayer();
            bool Status = bl.AddNewUser(newUser);
            if (Status == true)
            {
                return RedirectToAction("Index");
            }
            return View(inp);
        }

        public ActionResult EditUser(long Id)
        {
            //BusinessLayer bl = new BusinessLayer();
            UserDTO curUser = bl.GetUserById(Id);
            if (curUser == null)
            {
                return RedirectToAction("Index");
            }
            AddNewUserViewModel model = new AddNewUserViewModel()
            {
                UserId = curUser.UserId,
                DisplayName = curUser.Name,
                Email = curUser.Email,
                RoleName = curUser.RoleName,
                Location = curUser.Location
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
                Name = model.DisplayName,
                RoleName = model.RoleName,
                Location = model.Location
            };
            bool Result = bl.EditUser(tempUser);
            if (Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
   
        


            

          

            

           

            public IActionResult ViewRestaurants()
            {
                string strloggedInUser = HttpContext.Session.GetString("LoggedInUser");
                UserDTO loggedInUser = ObjectJsonHelper.GetFromJson<UserDTO>(strloggedInUser);
                if (loggedInUser == null || loggedInUser.RoleName != "USER")
                {
                    return RedirectToAction("Login", "Account");
                }

                List<RestaurantDTO> restaurants = bl.ListRestaurantsByLocation(loggedInUser.Location);
                return View(restaurants);
            }

            public IActionResult ViewMenu(long restaurantId)
            {
                List<MenuItemDTO> menuItems = bl.GetRestaurentMenu(restaurantId);
                return View(menuItems);
            }

            [HttpPost]
            public IActionResult PlaceOrder(long restaurantId, List<OrderLineData> orderLines)
            {
                string strloggedInUser = HttpContext.Session.GetString("LoggedInUser");
                UserDTO loggedInUser = ObjectJsonHelper.GetFromJson<UserDTO>(strloggedInUser);
                if (loggedInUser == null || loggedInUser.RoleName != "USER")
                {
                    return RedirectToAction("Login", "Account");
                }

                bool result = bl.PlaceOrder(loggedInUser, restaurantId, orderLines);
                if (result)
                {
                    return RedirectToAction("Index");
                }

                return View("ViewMenu",bl.GetRestaurentMenu(restaurantId));
            }
        }
    }


