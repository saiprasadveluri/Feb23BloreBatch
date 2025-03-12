using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MMVCDemoApp1.Models
{
    public class AddNewUserModel : PageModel
    {
        [BindProperty]
        public UserInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add logic to save the new user
            // For example: _userService.AddUser(Input);

            return RedirectToPage("/AdminDashboard/Index");
        }
    }

    public class UserInputModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Location { get; set; }
    }
}

