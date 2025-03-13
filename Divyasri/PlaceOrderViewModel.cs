using FoodDelApp.Data;

namespace MMVCDemoApp1.Models
{
    public class OrderViewModel
    {
        public List<MenuItemDTO> MenuItems { get; set; }
        public long RID { get; set; }
    }
}