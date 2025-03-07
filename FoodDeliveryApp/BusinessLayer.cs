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

        public bool RemoveRestaurant(long restaurantid)
        {
            return dal.RemoveRestaurant(restaurantid);
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
