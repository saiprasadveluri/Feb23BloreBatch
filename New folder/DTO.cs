using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_delivery
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Location { get; set; } // New property
    }

    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Location { get; set; }
    }

    public class MenuDTO
    {
        public int MenuId { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } // Veg, Non-Veg, Vegan
        public string Description { get; set; }
    }

    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // PLACED, IN_PROGRESS, DELIVERED, CANCELLED
    }

    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class DeliveryDTO
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public string DeliveryStatus { get; set; } // OUT_FOR_DELIVERY, DELIVERED
    }
}
