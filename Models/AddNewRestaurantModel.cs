using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MMVCDemoApp1.Models
{
    public class AddNewRestaurantModel : PageModel
    {
        [BindProperty]
        public RestaurantInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add logic to save the new restaurant
            // For example: _restaurantService.AddRestaurant(Input);

            return RedirectToPage("/AdminDashboard/Index");
        }
    }

    public class RestaurantInputModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int OwnerId { get; set; }
    }
}
