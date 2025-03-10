using System;
using System.Collections.Generic;

namespace Food_delivery
{
    public class BusinessLayer
    {
        private DataAccessLayer dal = new DataAccessLayer();
        private UserDTO loggedInUser = null;

        public bool RegisterUser(UserDTO user)
        {
            return dal.RegisterUser(user);
        }

        public UserDTO AuthenticateUser(string email, string password)
        {
            loggedInUser = dal.AuthenticateUser(email, password);
            return loggedInUser;
        }

        public UserDTO LoggedInUser
        {
            get { return loggedInUser; }
        }

        // Admin functionalities
        public bool AddRestaurant(RestaurantDTO restaurant)
        {
            if (loggedInUser != null && loggedInUser.Role == "Admin")
            {
                return dal.AddRestaurant(restaurant);
            }
            return false;
        }

        public bool RemoveRestaurant(int restaurantId)
        {
            if (loggedInUser != null && loggedInUser.Role == "Admin")
            {
                return dal.RemoveRestaurant(restaurantId);
            }
            return false;
        }

        public bool AssignOwner(int restaurantId, int ownerId)
        {
            if (loggedInUser != null && loggedInUser.Role == "Admin")
            {
                return dal.AssignOwner(restaurantId, ownerId);
            }
            return false;
        }

        // Owner functionalities
        public bool AddMenuItem(MenuDTO menuItem)
        {
            if (loggedInUser != null && loggedInUser.Role == "Owner")
            {
                return dal.AddMenuItem(menuItem);
            }
            return false;
        }

        // User functionalities
        public List<RestaurantDTO> GetRestaurantsByLocation(string location)
        {
            return dal.GetRestaurantsByLocation(location);
        }

        public List<MenuDTO> GetMenuByPreferences(int restaurantId, string category)
        {
            return dal.GetMenuByPreferences(restaurantId, category);
        }

        public bool PlaceOrder(OrderDTO order, List<OrderItemDTO> orderItems)
        {
            if (loggedInUser != null)
            {
                order.UserId = loggedInUser.UserId;
                order.Status = "PLACED";
                return dal.PlaceOrder(order, orderItems);
            }
            return false;
        }

        public bool UpdateOrderStatus(int orderId, string status)
        {
            if (loggedInUser != null && loggedInUser.Role == "User")
            {
                return dal.UpdateOrderStatus(orderId, status);
            }
            return false;
        }
        //create get menu by restaurant
        public List<MenuDTO> GetMenuByRestaurant(int restaurantId)
        {
            return dal.GetMenuByRestaurant(restaurantId);
        }
    }s
}