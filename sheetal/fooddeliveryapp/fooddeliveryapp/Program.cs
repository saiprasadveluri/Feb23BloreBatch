using fooddeliveryapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static fooddeliveryapp.BusinessLayer;

namespace fooddeliveryapp
{
    internal class Program
    {
        //static DataAccessLayer dal = new DataAccessLayer();
        static BusinessLayer Business = new BusinessLayer();
        static void Main(string[] args)
        {
            //dal.OpenConnection();
            Console.Write("username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();//GetPassword();
            bool Authenticated = Business.Authenticate(username, password);
            if (!Authenticated)
            {
                Console.WriteLine("\nNot Authorized to access the application");
                return;
            }

            //Add New User
            while (true)
            {
                Console.WriteLine("Options: \n 0: Exit \n 1: Add New User \n 2: Add New Restaurant");
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 0:
                        {
                            Business.CloseApp();
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
            string rname = Console.ReadLine();

            Console.Write("Location: ");
            string location = Console.ReadLine();

            Console.WriteLine("Owner Id: ");
            long ownerid = long.Parse(Console.ReadLine());
            RestaurantDTO newrest = new RestaurantDTO()
            {
                rname = rname,
                location = location,
                ownerid = ownerid
            };

            bool Status = Business.AddNewRestaurant(newrest);
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
            Console.Write("Username: ");
            string username = Console.ReadLine();
            
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Role Name(admin, user, owner): ");
            string role = Console.ReadLine();

            Console.Write("address: ");
            string address = Console.ReadLine();

            Console.Write("preferences: ");
            string preferences = Console.ReadLine();

            UserDTO obj = new UserDTO()
            {
                username = username,
                password = password,
                role = role,
                address = address,
                preferences = preferences
            };

            bool Status = Business.AddNewUser(obj);
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