using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    internal class BusinessLayer
    {
        DataAccessLayer dal = new DataAccessLayer();
        UserDTO loggedinUser = null;
        public BusinessLayer()
        {
            dal.OpenConnection();
        }
        public UserDTO AuthenticationUser(string email, string password)
        {
            loggedinUser = dal.LoginUser(email, password);
            return loggedinUser;
        }
        public bool AddUser(UserDTO inp)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.AddUser(inp);
            }
            else
            {
                return false;
            }
        }
        public bool AddRestaurant(RestaurentsDTO inp)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2)
            {
                return dal.AddRestaurant(inp);
            }
            else
            {
                return false;
            }
        }
        public bool CheckOwnerId(UserDTO inp)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2)
            {
                return dal.CheckOwnerId(inp);
            }
            else
            {
                return false;
            }
        }
        public bool DeleteRestaurant(RestaurentsDTO inp)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.DeleteRestaurant(inp);
            }
            else
            {
                return false;
            }
        }
        public void CloseApp()
        {
            dal.CloseConnection();
        }
    }
}
