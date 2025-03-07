using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Deliveryapp
{
    public class BuisnessLayer
    {
        DataAccessLayer dal;
        UserDTO loggedInUser;
        public BuisnessLayer()
        {
            dal = new DataAccessLayer();
        }
        public enum UserRole
        {
            Admin,
            Customer,
            RestaurantOwner
        }
        public bool Authenticate(string email, string password)
        {
            loggedInUser = dal.Login(email, password);
            if (loggedInUser != null)
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
            if (IsAdmin())
            {
                return dal.AddNewRestaurant(restaurant);
            }
            else
            {
                throw new UnauthorizedAccessException("Only admins can add a new restaurant.");
            }
        }
        public List<RestaurantDTO> SearchRestaurantsByLocation(string location)
        {
            return dal.GetRestaurantsByLocation(location);
        }

        public bool AddNewUser(UserDTO user)
        {
            return dal.AddNewUser(user);
        }
        private bool IsAdmin()
        {
            return loggedInUser != null && loggedInUser.Role == UserRole.Admin.ToString();
        }
    }
}