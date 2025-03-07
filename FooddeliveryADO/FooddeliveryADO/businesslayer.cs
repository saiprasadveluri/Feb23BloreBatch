using System;
using System.Collections.Generic;

namespace FooddeliveryADO
{
    public class BusinessLayer
    {
        static DataAccess dal = new DataAccess();

        public usersDTO AuthenticateUser(string email, string password)
        {
            dal.OpenConnection();
            usersDTO user = dal.AuthenticateUser(email, password);
            dal.CloseConnection();
            return user;
        }

        public List<usersDTO> GetAllUsers()
        {
            dal.OpenConnection();
            List<usersDTO> users = dal.Listusers();
            dal.CloseConnection();
            return users;
        }

        public bool AddUser(usersDTO user)
        {
            dal.OpenConnection();
            bool result = dal.Addusers(user);
            dal.CloseConnection();
            return result;
        }
        public List<restaurantsDTO> GetAllRestaurants()
        {
            dal.OpenConnection();
            List<restaurantsDTO> restaurants = dal.ListRestaurants();
            dal.CloseConnection();
            return restaurants;
        }
        public bool AddRestaurant(restaurantsDTO restaurant)
        {
            dal.OpenConnection();
            bool result = dal.AddRestaurants(restaurant);
            dal.CloseConnection();
            return result;
        }
        public List<menusDTO> GetAllMenus()
        {
            dal.OpenConnection();
            List<menusDTO> menus = dal.ListMenus();
            dal.CloseConnection();
            return menus;
        }   
        public bool AddMenu(menusDTO menu)
        {
            dal.OpenConnection();
            bool result = dal.AddMenus(menu);
            dal.CloseConnection();
            return result;
        }
        public List<ordersDTO> GetAllOrders()
        {
            dal.OpenConnection();
            List<ordersDTO> orders = dal.ListOrders();
            dal.CloseConnection();
            return orders;
        }
        public bool AddOrder(ordersDTO order)
        {
            dal.OpenConnection();
            bool result = dal.AddOrders(order);
            dal.CloseConnection();
            return result;
        }
        public List<orderitemDTO> GetAllOrderItems()
        {
            dal.OpenConnection();
            List<orderitemDTO> orderitems = dal.ListOrderItems();
            dal.CloseConnection();
            return orderitems;
        }
        public bool AddOrderItem(orderitemDTO orderitem)
        {
            dal.OpenConnection();
            bool result = dal.AddOrderItems(orderitem);
            dal.CloseConnection();
            return result;
        }
        public List<restaurantsDTO> GetRestaurantsByOwner(string rlocation)
        {
            dal.OpenConnection();
            List<restaurantsDTO> restaurants = dal.SearchRestaurantsByLocation(rlocation);
            dal.CloseConnection();
            return restaurants;
        }
        public bool UpdateOrderStatus(long orderid, string orderstatus)
        {
            dal.OpenConnection();
            bool result = dal.UpdateOrderStatus(orderid, orderstatus);
            dal.CloseConnection();
            return result;
        }
        public bool AssignOwnerToRestaurant(long restaurantId, long ownerId)
        {
            dal.OpenConnection();
            bool result = dal.AssignOwnerToRestaurants(restaurantId, ownerId);
            dal.CloseConnection();
            return result;
        }
        public bool DeleteResturant(long userid)
        {
            dal.OpenConnection();
            bool result = dal.deletereataurant(userid);
            dal.CloseConnection();
            return result;
        }
        public List<menusDTO> GetMenuByRestaurant(long restaurantId)
        {
            dal.OpenConnection();
            List<menusDTO> menus = dal.GetMenuByRestaurant(restaurantId);
            dal.CloseConnection();
            return menus;
        }

    }
}
