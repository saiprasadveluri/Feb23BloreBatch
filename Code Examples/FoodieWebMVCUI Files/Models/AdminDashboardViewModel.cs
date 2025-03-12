using FoodDelApp.Data;

namespace MMVCDemoApp1.Models
{
    public class AdminDashboardViewModel
    {
        public List<UserDTO> UserList { get; set; }
        public List<RestaurantDTO> Restaurants { get; set; }
    }
}
