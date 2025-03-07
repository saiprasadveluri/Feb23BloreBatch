using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Food_App
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
                Console.WriteLine("Options: \n 0: Exit \n 1: Add New User \n 2: Add New Restaurant");
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

            Console.WriteLine("phone ");
            long Phone = long.Parse(Console.ReadLine());
            Console.WriteLine("OwnerId ");
            long ownerid = long.Parse(Console.ReadLine());
            RestaurantDTO newRestaurant = new RestaurantDTO()
            {
                res_name = name,
                city = location,
                phone = Phone,
                owner_id = ownerid // Fixed property name
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
            UserDTO obj = new UserDTO();
            Console.WriteLine("New User Data: ");
            Console.Write("Name: ");
            obj.Name = Console.ReadLine();
            Console.Write("Email: ");
            obj.Email = Console.ReadLine();
            Console.Write("Password: ");
            obj.Password = Console.ReadLine();
            Console.Write("Role Name: ");
            obj.RoleName = Console.ReadLine();

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

