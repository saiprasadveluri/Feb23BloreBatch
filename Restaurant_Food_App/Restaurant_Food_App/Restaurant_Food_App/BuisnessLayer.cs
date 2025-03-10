using System.Collections.Generic;

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
            return dal.AddNewRestaurant(restaurant);
        }

        public bool AddNewUser(UserDTO user)
        {
            return dal.AddNewUser(user);
        }

        public bool PlaceOrder(OrderDTO order)
        {
            return dal.PlaceOrder(order);
        }

        public bool UpdateOrderstatus(long order_id, string status)
        {
            return dal.UpdateOrderstatus(order_id, status);
        }

        public List<RestaurantDTO> SearchRestaurantsBycity(string city)
        {
            return dal.SearchRestaurantsBycity(city);
        }

        public bool RemoveRestaurant(long restaurant_id)
        {
            return dal.RemoveRestaurant(restaurant_id);
        }

        public bool AssignOwnerToRestaurant(long restaurant_id, long ownerId)
        {
            return dal.AssignOwnerToRestaurant(restaurant_id, ownerId);
        }

        public bool AddMenu(MenuDTO menu)
        {
            return dal.AddMen(menu);
        }

        private bool IsInRole(string reqRole)
        {
            return loggedInUser != null && loggedInUser.RoleName == reqRole;
        }
    }
}
