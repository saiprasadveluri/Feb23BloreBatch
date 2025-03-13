using Microsoft.AspNetCore.Mvc.Rendering;

namespace MMVCDemoApp1.Models
{
    public class AddNewUserViewModel
    {
        public long UserId { get; set; }
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }

        public string Location { get; set; }
        //Role List.
        public List<SelectListItem> RolesList { get; set; } = new List<SelectListItem>() { 
                                                            new SelectListItem("Admin","ADMIN"),
                                                            new SelectListItem("Owner","OWNER"),
                                                             new SelectListItem("User","USER"),
                                                                        };
    }
}
