using FoodDelApp;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MMVCDemoApp1.Models
{
    public class AddRestaurantInputModel
    {
        public string rnm { get; set; }
        public string loc { get; set; }
        public long rowner { get; set; }
        //List Items
        public List<SelectListItem> owners { get; set; } = new();
    }
}