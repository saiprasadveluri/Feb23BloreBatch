using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Deliveryapp
{
    public class BusinessLayer
    {
        public DataAccessLayer dal;
        public UserDTO loggedInUser;
        public BusinessLayer()
        {
            dal = new DataAccessLayer();
        }

        public bool Authenticate(string email, string password)
        {
            loggedInUser = dal.Login(email, password);
            return loggedInUser != null;
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            if (IsAdmin())
            {
                return dal.AddNewRestaurant(restaurant, loggedInUser.Uid);
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
            return loggedInUser != null && loggedInUser.Role == "Admin";
        }
    }
}