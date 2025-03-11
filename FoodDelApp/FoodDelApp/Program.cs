using System;
using System.Collections.Generic;
using FoodDelApp.DTOs;

namespace FoodDelApp
{
    internal class Program
    {
        static DataAccessLayer dal = new DataAccessLayer();
        static BusinessLayer bl = new BusinessLayer();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Options: \n 0: Exit \n 1: Register \n 2: Login");
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 0:
                        return;
                    case 1:
                        Register();
                        break;
                    case 2:
                        Login();
                        break;
                    default:
                        Console.WriteLine("Wrong Option...");
                        break;
                }
            }
        }

        private static void Register()
        {
            Console.WriteLine("New User Registration: ");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Role Name (ADMIN/OWNER/USER): ");
            string roleName = Console.ReadLine();
            Console.Write("Location: ");
            string location = Console.ReadLine();
            UserDTO obj = new UserDTO()
            {
                Name = name,
                Email = email,
                Password = password,
                RoleName = roleName,
                Location = location,
            };

            bool Status = bl.AddNewUser(obj);
            if (Status == false)
            {
                Console.WriteLine("Error In Registering New User");
            }
            else
            {
                Console.WriteLine("Success in Registering New User");
            }
        }

        private static void Login()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            bool Authenticated = bl.Authenticate(email, password);
            if (!Authenticated)
            {
                Console.WriteLine("No Access to application");
                return;
            }
            while (true)
            {
                Console.WriteLine("Options: \n 0: Exit \n 1: Add New User \n 2: Add New Restaurant \n 3: Add Menu Item \n 4: Place Order");
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 0:
                        bl.CloseApp();
                        return;
                    case 1:
                        AddNewUser();
                        break;
                    case 2:
                        AddNewRestaurant();
                        break;
                    case 3:
                        AddMenuItem();
                        break;
                    case 4:
                        PlaceOrder();
                        break;
                    default:
                        Console.WriteLine("Wrong Option...");
                        break;
                }
            }
        }

        private static void AddMenuItem()
        {
            Console.WriteLine("Res Id");
            int RIDInp;
            while (!int.TryParse(Console.ReadLine(), out RIDInp))
            {
                Console.WriteLine("Invalid Res Id. Please enter a valid number.");
                Console.Write("Res Id: ");
            }

            List<RestaurantDTO> restaurantDTOs = bl.ListMyRestaurants();
            RestaurantDTO curRst = restaurantDTOs.Find(r => r.RID == RIDInp);
            if (curRst == null)
            {
                Console.WriteLine("You are Not owner of the selected Restaurant....");
                return;
            }
            Console.WriteLine("Menu Name: ");
            string nm = Console.ReadLine();
            Console.WriteLine("Price: ");
            int price;
            while (!int.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid Price. Please enter a valid number.");
                Console.Write("Price: ");
            }
            Console.WriteLine("Food Type: [VEG/NON-VEG]: ");
            string ftype = Console.ReadLine();
            MenuItemDTO mitm = new MenuItemDTO()
            {
                MenuName = nm,
                RID = RIDInp,
                UnitPrice = price,
                FoodType = ftype
            };
            bool status = bl.AddMenuItem(mitm);
            if (status)
            {
                Console.WriteLine("Success in Adding Menu Item");
            }
            else
            {
                Console.WriteLine("Error In adding menu Item");
            }
        }

        private static void PlaceOrder()
        {
            Console.WriteLine("Res Id");
            int RID;
            while (!int.TryParse(Console.ReadLine(), out RID))
            {
                Console.WriteLine("Invalid Res Id. Please enter a valid number.");
                Console.Write("Res Id: ");
            }

            List<OrderLineData> orderMenuList = new List<OrderLineData>();
            List<MenuItemDTO> list = bl.GetRestaurentMenu(RID);
            if (list.Count > 0)
            {
                while (true)
                {
                    foreach (MenuItemDTO item in list)
                    {
                        Console.WriteLine($"{item.MID} - {item.MenuName}");
                    }
                    Console.WriteLine("Select Menu ID. 0 for End");
                    int MnuId;
                    while (!int.TryParse(Console.ReadLine(), out MnuId))
                    {
                        Console.WriteLine("Invalid Menu ID. Please enter a valid number.");
                        Console.Write("Menu ID: ");
                    }
                    if (MnuId == 0)
                    {
                        break;
                    }
                    Console.WriteLine("Select Quantity");
                    int qty;
                    while (!int.TryParse(Console.ReadLine(), out qty))
                    {
                        Console.WriteLine("Invalid Quantity. Please enter a valid number.");
                        Console.Write("Quantity: ");
                    }

                    OrderLineData cur = new OrderLineData()
                    {
                        MenuId = MnuId,
                        Qty = qty,
                    };
                    orderMenuList.Add(cur);
                }
                bool Status = bl.PlaceOrder(RID, orderMenuList);
                if (Status)
                {
                    Console.WriteLine("Success in placing Order...");
                }
                else
                {
                    Console.WriteLine("Failed to place order");
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

            Console.Write("Owner Id: ");
            int ownerId;
            while (!int.TryParse(Console.ReadLine(), out ownerId))
            {
                Console.WriteLine("Invalid Owner Id. Please enter a valid number.");
                Console.Write("Owner Id: ");
            }

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
                Name = name,
                Email = email,
                Password = password,
                RoleName = roleName,
                Location = location,
            };

            bool Status = bl.AddNewUser(obj);
            if (Status == false)
            {
                Console.WriteLine("Error In Adding New User");
            }
            else
            {
                Console.WriteLine("Success in Adding New User");
            }
        }
    }
}
