using FoodDelApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class BusinessLayer
    {
        DataAccessLayer dal;
        UserDTO loggedInUser;
        public BusinessLayer()
        {
            dal=new DataAccessLayer();
        }

        public void CloseApp()
        {
            dal.CloseApp();
            loggedInUser = null;
        }
        public bool Authenticate(string Email, string Password)
        {
            loggedInUser= dal.Login(Email, Password);
            if (loggedInUser!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            if (IsInRole(UserTypeEnum.ADMIN))
            {
                if (IsInRole(restaurant.OwnerId, UserTypeEnum.OWNER))
                    return dal.AddNewRestaurant(restaurant);
                else
                    return false;
            }
            else
                return false;
        }
        public bool AddNewUser(UserDTO user)
        {
            if (IsInRole(UserTypeEnum.ADMIN))
                return dal.AddNewUser(user);
            else
                return false;
        }

        private bool IsInRole(UserTypeEnum reqRole)
        {
            if (loggedInUser != null)
            {
                return loggedInUser.RoleName == reqRole.ToString();
            }
            else
            {
                return false;
            }
        }

        private bool IsInRole(long UserId, UserTypeEnum reqRole)
        {
            string ExistingRole = dal.GetUserRole(UserId);
            if (ExistingRole != null)
            {
                if (ExistingRole== reqRole.ToString())
                    return true;
            }
            return false;
        }
        public bool DeleteRestaurant(long rid)
        {
            if (IsInRole(UserTypeEnum.ADMIN))
                return dal.DeleteRestaurant(rid);
            else
                return false;
        }
    }
}