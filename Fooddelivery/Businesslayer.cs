using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_delivery
{

    public class BusinessLayer
    {
        private DataAccessLayer dal = new DataAccessLayer();
        private UserDTO loggedInUser = null;

        public BusinessLayer()
        {
            dal.OpenConnection();
        }
        public bool RegisterUser(UserDTO user)
        {
            return dal.RegisterUser(user);
        }
        public UserDTO LoggedInUser
        {
            get { return loggedInUser; }
        }
        public UserDTO AuthenticateUser(string email, string password)
        {
            loggedInUser = dal.AuthenticateUser(email, password);
            return loggedInUser;
        }
        public List<MenuDTO> GetMenuByRestaurant(int restaurantId)
        {
            return dal.GetMenuByRestaurant(restaurantId);
        }
        //user functions
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
        //owner functions
        public bool AddMenuItem(MenuDTO menuItem)
        {
            if (loggedInUser != null && loggedInUser.Role == "Owner")
            {
                return dal.AddMenuItem(menuItem);
            }
            return false;
        }

        public bool UpdateMenuItem(MenuDTO menuItem)
        {
            if (loggedInUser != null && loggedInUser.Role == "Owner")
            {
                return dal.UpdateMenuItem(menuItem);
            }
            return false;
        }
        //admin but add user to think about
        public bool AddUser(UserDTO user)
        {
            if (loggedInUser != null && loggedInUser.Role == "Admin")
            {
                return dal.AddUser(user);
            }
            return false;
        }

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
        //preference
        public List<MenuDTO> GetMenuByPreferences(int restaurantId, string category)
        {
            return dal.GetMenuByPreferences(restaurantId, category);
        }
        public List<RestaurantDTO> GetAllRestaurants()
        {
            if (loggedInUser != null && loggedInUser.Role == "Admin")
            {
                return dal.GetAllRestaurants();
            }
            return new List<RestaurantDTO>();
        }
        //get orders
        public List<OrderDTO> GetOrdersByUser(int userId)
        {
            if (loggedInUser != null && loggedInUser.UserId == userId)
            {
                return dal.GetOrdersByUser(userId);
            }
            return new List<OrderDTO>();
        }

        public List<OrderDTO> GetOrdersByRestaurant(int restaurantId)
        {
            if (loggedInUser != null && loggedInUser.Role == "Owner")
            {
                return dal.GetOrdersByRestaurant(restaurantId);
            }
            return new List<OrderDTO>();
        }

        public List<UserDTO> GetAllUsers()
        {
            if (loggedInUser != null && loggedInUser.Role == "Admin")
            {
                return dal.GetAllUsers();
            }
            return new List<UserDTO>();
        }

        public List<OrderDTO> GetAllOrders()
        {
            if (loggedInUser != null && loggedInUser.Role == "Admin")
            {
                return dal.GetAllOrders();
            }
            return new List<OrderDTO>();
        }
        public List<RestaurantDTO> GetRestaurantsByLocation(string location)
        {
            return dal.GetRestaurantsByLocation(location);
        }

        public void CloseApp()
        {
            dal.CloseConnection();
        }
    }
}

