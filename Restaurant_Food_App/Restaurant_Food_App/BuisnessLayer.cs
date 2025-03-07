using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
// Remove the following line as it is causing the error
// using FoodDelApp;
using Restaurant_Food_App;

namespace Restaurant_Food_App
{
    public class BusinessLayer
    {
        DataAccessLayer dal;
        UserDTO loggedInUser;
        public BusinessLayer()
        {
            dal = new DataAccessLayer();
        }

        public void CloseApp()
        {
            dal.CloseApp();
            loggedInUser = null;
        }
        public bool Authenticate(string Email, string Password)
        {
            loggedInUser = dal.Login(Email, Password);
            return loggedInUser != null;
            
        }
        public UserDTO GetLoggedInUser()
        {
            return loggedInUser;
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            if (IsInRole("ADMIN"))
            {
                return dal.AddNewRestaurant(restaurant);
            }
            return false;
        }
        public bool AddNewUser(UserDTO user)
        {
            if (IsInRole("ADMIN"))
            {
                return dal.AddNewUser(user);
            }
            return false;
        }

        private bool IsInRole(string reqRole)
        {
            if (loggedInUser != null)
            {
                return loggedInUser.RoleName.ToUpper() == reqRole.ToUpper();
            }
            return false;
        }

       

    }
}
