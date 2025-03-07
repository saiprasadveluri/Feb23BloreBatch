using System.Collections.Generic;
using System.Linq;
using System;

namespace FoodDeliveryApp
{
    public class User
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string user_role { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public User() { }

        public User(int userId)
        {
            this.userid = userId;
        }

        public void SearchRestaurantByLocation(string location)
        {
            var result = Program.dal.GetRestaurants().Where(r => r.resto_location == location).ToList();
            Console.WriteLine($"Restaurants in {location}:");
            foreach (var resto in result)
            {
                Console.WriteLine($"- {resto.resto_name}");
            }
        }

        public void FilterItemsByPreference(string preference)
        {
            var result = Program.dal.GetMenus().Where(m => m.item_name.Contains(preference)).ToList();
            Console.WriteLine($"Items matching {preference}:");
            foreach (var item in result)
            {
                Console.WriteLine($"- {item.item_name} (${item.price})");
            }
        }

        public void PlaceOrder(int restoId, List<int> itemIds)
        {
            var order = new Order
            {
                userid = this.userid,
                resto_id = restoId,
                status = "PLACED"
            };
            Program.dal.PlaceOrder(order);
            Console.WriteLine($"Order placed successfully. Order No: {order.order_no}");
        }
    }

    public class Restaurant
    {
        public int resto_id { get; set; }
        public string resto_name { get; set; }
        public string resto_location { get; set; }
        public int owner_id { get; set; }
    }

    public class Menu
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public int price { get; set; }
        public int resto_id { get; set; }
    }

    public class Order
    {
        public int order_no { get; set; }
        public int userid { get; set; }
        public int resto_id { get; set; }
        public string status { get; set; }
    }
}

