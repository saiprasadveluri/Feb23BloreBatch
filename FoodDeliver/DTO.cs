using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliver
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RoleName{get; set; }

        public string Location { get; set; }

    }
    public class RestaurantDTO
    {
        public long RID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public long OwnerId { get; set; }


    }
    public class Orders
    {
        public long OrderId { get; set; }
        public long RID { get; set; }
        public long OrderBy { get; set; }
        public string Status { get; set; }
        public string OrderDate { get; set; }


    }
    public enum UserTypeEnum
    {
        ADMIN = 1,
        OWNER = 2,
        USER = 3
    }
}
