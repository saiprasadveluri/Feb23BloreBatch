using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class BusinessLayer
    {
        DataAccessLayer dal = new DataAccessLayer();

        public Users LoggedInUser = null;

        public bool Authenticate(string email, string password)
        {
            LoggedInUser = dal.Login(email, password);
            if(LoggedInUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddRestaurant(string restaurantname, string location, long ownerid)
        {
            Restaurants restaurant = new Restaurants();
            restaurant.restaurantname = restaurantname;
            restaurant.location = location;
            restaurant.ownerid = ownerid;
            return dal.AddRestaurant(restaurant);
        }

        public bool AddOrder(string status, string payment, string orderdetails, long userid, long restaurantid)
        {
            // Create a new instance of the Order class
            Order order = new Order
            {
                status = status,
                payment = payment,
                orderdetails = orderdetails,
                userid = userid,
                restaurantid = restaurantid
            };

            // Call the Data Access Layer (DAL) method to add the order
            return dal.AddOrder(order);
        }


        public bool RemoveRestaurant(long restaurantid)
        {
            return dal.RemoveRestaurant(restaurantid);
        }

        public bool RemoveOrder(long orderid)
        {
            return dal.RemoveOrder(orderid);
        }



        public bool AddMenuItems(string itemname, string category, double price, long restaurantid)
        {
            MenuItems menuItems = new MenuItems();
            menuItems.menuitemname = menuitemname;
            menuItems.price = price;
            menuItems.category = category;
            menuItems.restaurantid = restaurantid;
            return dal.AddMenuItems(menuItems);
        }





    }
}
