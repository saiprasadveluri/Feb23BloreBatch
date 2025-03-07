using FoodDelivery1;
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
            string Email = Console.ReadLine();
            Console.Write("Password: ");
            string Password = Console.ReadLine();
            bool Authenticated = bl.Authenticate(Email, Password);
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
            Console.WriteLine("RId");
            long RIDInp = long.Parse(Console.ReadLine());
            List<Restaurant> restaurant = bl.ListMyRestaurants();
            Restaurant curRst = restaurant.Find(r => r.RID == RIDInp);
            if (curRst == null)
            {
                Console.WriteLine("You are Not owner of the selected Restaurant....");
                return;
            }
            Console.WriteLine("Name: ");
            string nm = Console.ReadLine();
            Console.WriteLine("UnitPrice: ");
            double price= double.Parse(Console.ReadLine());
            Console.WriteLine("Food Type: [VEG/NON-VEG]: ");
            string ftype = Console.ReadLine();
            MenuItem mitm = new MenuItem()
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
            long RID = long.Parse(Console.ReadLine());
            List<OrderLineData> orderMenuList = new List<OrderLineData>();
            List<MenuItem> list = bl.GetRestaurentMenu(RID);
            if (list.Count > 0)
            {
                while (true)
                {
                    foreach (MenuItem item in list)
                    {
                        Console.WriteLine($"{item.MID} - {item.MenuName}");
                    }
                    Console.WriteLine("Select Menu ID. 0 for End");
                    long MnuId = long.Parse(Console.ReadLine());
                    if (MnuId == 0)
                    {
                        break;
                    }
                    Console.WriteLine("Select Quantity");
                    int qty = int.Parse(Console.ReadLine());

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

            Console.WriteLine("Owner Id: ");
            long ownerId = long.Parse(Console.ReadLine());
            Restaurant newRestaurant = new Restaurant()
            {
                Name = name,
                Location = location,
                OwnerID = ownerId
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
