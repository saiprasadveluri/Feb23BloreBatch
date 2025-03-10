using System;
using FoodDelApp;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    internal class Program
    {
        static BusinessLayer bl = new BusinessLayer();

        static void Main(string[] args)
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            bool authenticated = bl.Authenticate(email, password);
            if (!authenticated)
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
            long RIDInp = long.Parse(Console.ReadLine());
            List<ResturantDTO> restaurantDTOs = bl.ListMyRestaurants();
            ResturantDTO curRst = restaurantDTOs.Find(r => r.Rid == RIDInp);
            if (curRst == null)
            {
                Console.WriteLine("You are Not owner of the selected Restaurant....");
                return;
            }
            Console.WriteLine("Menu Name: ");
            string nm = Console.ReadLine();
            Console.WriteLine("Price: ");
            double price = double.Parse(Console.ReadLine());
            Console.WriteLine("Food Type: [VEG/NON-VEG]: ");
            string ftype = Console.ReadLine();
            MenuDTO mitm = new MenuDTO()
            {
                Menuname = nm,
                Rid = RIDInp,
                UnitPrice = (decimal)price,
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
            List<OrderlistDTO> orderMenuList = new List<OrderlistDTO>();
            List<MenuDTO> list = bl.GetRestaurentMenu(RID);
            if (list.Count > 0)
            {
                while (true)
                {
                    foreach (MenuDTO item in list)
                    {                                                                                                                                                                                                                                                                                
                        Console.WriteLine($"{item.MenuId} - {item.Menuname}");
                    }
                    Console.WriteLine("Select Menu ID. 0 for End");
                    long MnuId = long.Parse(Console.ReadLine());
                    if (MnuId == 0)
                    {
                        break;
                    }
                    Console.WriteLine("Select Quantity");
                    int qty = int.Parse(Console.ReadLine());

                    OrderlistDTO cur = new OrderlistDTO()
                    {
                        Menuid = MnuId,
                        Qty = qty,
                    };
                    orderMenuList.Add(cur);
                }
                bool status = bl.PlaceOrder(RID, orderMenuList);
                if (status)
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
            ResturantDTO newRestaurant = new ResturantDTO()
            {
                Name = name,
                Location = location,
                Ownerid = ownerId
            };

            bool status = bl.AddNewRestaurant(newRestaurant);
            if (status == false)
            {
                Console.WriteLine("Error In Adding New Restaurant");
            }
            else
            {
                Console.WriteLine("Success in Adding New Restaurant");
            }
        }

        private  static void AddNewUser()
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

            bool status = bl.AddNewUser(obj);
            if (status == false)
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
