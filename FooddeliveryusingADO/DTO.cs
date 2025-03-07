using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooddeliveryusingADO
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Restaurant
    {
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int AdminID { get; set; }
    }

    public class Menu
    {
        public int MenuID { get; set; }
        public int RestaurantID { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string FoodPreference { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int RestaurentID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int MenuID { get; set; }
        public int Quantity { get; set; }
    }

}
