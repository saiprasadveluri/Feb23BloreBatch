using System;
using System.Collections.Generic;

namespace FoodDelApp.Data
{
    public class User
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; } // Admin, Owner, User
        public string Location { get; set; }
    }

    public class Restaurant
    {
        public long RID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public long OwnerId { get; set; }
    }

    public class MenuItem
    {
        public long MID { get; set; }
        public string MenuName { get; set; }
        public long RID { get; set; }
        public double UnitPrice { get; set; }
        public string FoodType { get; set; } // VEG, NON-VEG
    }

    public class Order
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public long RID { get; set; }
        public string Status { get; set; } // e.g., 'DELIVERED'
    }

    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public long MID { get; set; }
        public int Quantity { get; set; }
    }
}
