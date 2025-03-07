using FoodDelApp;
using FoodDeliveryApp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelApp
{
    internal class Program
    {
        static BusinessLayer bl = new BusinessLayer();
        static void Main(string[] args)
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string Password = Console.ReadLine();
            bool Authenticated = bl.Authenticate(email, Password);
            if (!Authenticated)
            {
                Console.WriteLine("No Access to application");
                return;
            }
            while (true)
            {
                Console.WriteLine("Options: \n 0: Exit \n 1: Add New User \n 2: Add New Restaurant \n 3: Delete Restaurant \n 4: List Restaurant");
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 0:
                        {
                            bl.CloseApp();
                            return;
                        }
                        break;
                    case 1:
                        {
                            AddNewUser();
                        }
                        break;
                    case 2:
                        AddNewRestaurant();
                        break;
                    case 3:
                        DeleteRestaurant();
                        break;
                    case 4:
                        List<RestaurantDTO> rstList = bl.ListRestaurant();
                        Console.WriteLine("Restaurant List:");
                        foreach (RestaurantDTO rst in rstList)
                        {
                            Console.WriteLine(rst.Name);
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong Option...");
                        break;
                }
            }

        }

        private static void AddNewRestaurant()
        {
            Console.WriteLine("New User Restaurant: ");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Location: ");
            string location = Console.ReadLine();

            Console.WriteLine("Owner Id: ");
            long ownerId = long.Parse(Console.ReadLine());
            RestaurantDTO newRestaurant = new RestaurantDTO()
            {
                Name = name,
                Location = location,
                OwnerId = ownerId
            };

            bool Status = bl.AddNewRestaurant(newRestaurant);
            if (Status == false)
            {
                Console.WriteLine("Error In Adding New Restaurant");
            }
            else
            {
                Console.WriteLine("Success in Adding New Restaurant");
            }
        }
        private static void AddNewUser()
        {
            Console.WriteLine("New User Data: ");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Role Name: ");
            string roleName = Console.ReadLine();
            Console.Write("Location: ");
            string location = Console.ReadLine();
            UserDTO obj = new UserDTO()
            {
                Name= name,
                Email= email,
                Password=password,
                RoleName= roleName,
                Location = location,
            };

            bool Status = bl.AddNewUser(obj);
            if (Status==false)
            {
                Console.WriteLine("Error In Adding New User");
            }
            else
            {
                Console.WriteLine("Success in Adding New User");
            }
        }

        public static void DeleteRestaurant()
        {
            Console.WriteLine("Delete Restaurant");
            Console.Write("Restaurant ID: ");
            long RID = long.Parse(Console.ReadLine());


            bool Status = bl.DeleteRestaurant(RID);
            if (Status == false)
            {
                Console.WriteLine("Error In Deleting Restaurant");
            }
            else
            {
                Console.WriteLine("Success in Deleting Restaurant");
            }
        }

    }
}
    }
}