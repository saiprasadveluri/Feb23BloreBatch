using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    
        public class UserDTO
        {
            public long UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public long RoleId { get; set; }
            public string UserLoc { get; set; }
        }
        public class RestaurentsDTO
        {
            public long ResId { get; set; }
            public string ResName { get; set; }
            public long OwnerID { get; set; }
            public string ResLoc { get; set; }
        }
        public class OrdersDTO
        {
            public long  OrderId { get; set; }
            public long UserID { get; set; }
            public string Status { get; set; }
    }
        public class MenuDTO
        {
            public long DishID { get; set; }
            public string DishName { get; set; }
            public string Price { get; set; }
            public long RestaurantID { get; set; }
        }

        public class OrderItemsDTO
        {
            public long OrderID { get; set; }
            public long ResID { get; set; }
        }

}
