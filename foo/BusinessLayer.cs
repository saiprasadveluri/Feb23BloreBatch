using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelApp.Data
{
    public class BusinessLayer
    {
        private DataAccessLayer dal = new DataAccessLayer();
        private User loggedInUser;

        public bool Authenticate(string email, string password)
        {
            loggedInUser = dal.Login(email, password);
            return loggedInUser != null;
        }

        public void CloseApp()
        {
            dal.CloseApp();
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            if (loggedInUser.RoleName != "Admin")
                return false;

            return dal.AddNewRestaurant(restaurant);
        }

        public bool AddNewUser(UserDTO user)
        {
            if (loggedInUser.RoleName != "Admin")
                return false;

            return dal.AddNewUser(user);
        }

        public List<RestaurantDTO> ListMyRestaurants()
        {
            if (loggedInUser.RoleName != "Owner")
                return new List<RestaurantDTO>();

            return dal.ListRestaurantsByOwner(loggedInUser.UserId);
        }

        public List<RestaurantDTO> ListRestaurantsByLocation(string location)
        {
            return dal.ListRestaurantsByLocation(location);
        }

        public List<MenuItemDTO> GetRestaurentMenu(long RID)
        {
            return dal.GetRestaurentMenu(RID);
        }

        public bool PlaceOrder(long RID, List<OrderLineData> menuLst)
        {
            return dal.PlaceOrder(RID, loggedInUser.UserId, menuLst);
        }

        public bool AddMenuItem(MenuItemDTO itm)
        {
            if (loggedInUser.RoleName != "Owner")
                return false;

            return dal.AddMenuItem(itm);
        }
    }
}
