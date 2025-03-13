using FoodDelApp.Data;
using MMVCDemoApp1.Infra;

namespace WebApplication3.Models
{
    public class UserDashboardViewModel
    {
        public UserDTO User { get; set; }
        public List<OrderDTO> Orders { get; set; }
        public List<RestaurantDTO> NearbyRestaurants { get; set; }
    }

    public class EditProfileViewModel
    {
        public long UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
