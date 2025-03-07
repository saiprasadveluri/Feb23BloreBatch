using System;
using System.Collections.Generic;

namespace FoodDelApp
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

        public bool Authenticate(string email, string password)
        {
            loggedInUser = dal.Login(email, password);
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

        public bool RemoveRestaurant(long restaurantId)
        {
            if (IsInRole("ADMIN"))
            {
                return dal.RemoveRestaurant(restaurantId);
            }
            return false;
        }

        public bool AssignOwnerToRestaurant(long restaurantId, long ownerId)
        {
            if (IsInRole("ADMIN"))
            {
                return dal.AssignOwnerToRestaurant(restaurantId, ownerId);
            }
            return false;
        }

        public bool AddMenuItem(MenuItemDTO menuItem)
        {
            if (IsInRole("OWNER"))
            {
                return dal.AddMenuItem(menuItem);
            }
            return false;
        }

        public List<RestaurantDTO> SearchRestaurantsByLocation(string location)
        {
            return dal.SearchRestaurantsByLocation(location);
        }

        public List<MenuItemDTO> FilterMenuItems(string preference)
        {
            return dal.FilterMenuItems(preference);
        }

        public bool PlaceOrder(OrderDTO order)
        {
            return dal.PlaceOrder(order);
        }

        public bool UpdateOrderStatus(long orderId, string status)
        {
            return dal.UpdateOrderStatus(orderId, status);
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
