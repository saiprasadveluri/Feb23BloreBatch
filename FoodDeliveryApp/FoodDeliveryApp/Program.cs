using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryApp
{
    internal class Program
    {
        internal static DataAccessLayer dal = new DataAccessLayer();
        internal static User loggedInUser = null;

        public static void Main(string[] args)
        {
            while (true)
            {
                if (loggedInUser == null)
                {
                    Login();
                }
                else
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add User");
                    Console.WriteLine("2. Add Restaurant");
                    Console.WriteLine("3. Remove Restaurant");
                    Console.WriteLine("4. Assign Owner to Restaurant");
                    Console.WriteLine("5. Add Menu Item");
                    Console.WriteLine("6. Search Restaurant by Location");
                    Console.WriteLine("7. Filter Items by Preference");
                    Console.WriteLine("8. Place Order");
                    Console.WriteLine("9. Update Order Status");
                    Console.WriteLine("10. Logout");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddUser();
                            break;
                        case "2":
                            AddRestaurant();
                            break;
                        case "3":
                            RemoveRestaurant();
                            break;
                        case "4":
                            AssignOwner();
                            break;
                        case "5":
                            AddMenuItem();
                            break;
                        case "6":
                            SearchRestaurantByLocation();
                            break;
                        case "7":
                            FilterItemsByPreference();
                            break;
                        case "8":
                            PlaceOrder();
                            break;
                        case "9":
                            UpdateOrderStatus();
                            break;
                        case "10":
                            loggedInUser = null;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }

        private static void Login()
        {
            Console.Write("Enter email: ");
            var email = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            loggedInUser = dal.Login(email, password);

            if (loggedInUser == null)
            {
                Console.WriteLine("Invalid email or password. Would you like to register? (yes/no)");
                var register = Console.ReadLine();
                if (register.ToLower() == "yes")
                {
                    RegisterUser(email, password);
                }
            }
            else
            {
                Console.WriteLine($"Welcome, {loggedInUser.username}!");
            }
        }

        private static void RegisterUser(string email, string password)
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter user role (admin/owner/user): ");
            var userRole = Console.ReadLine();

            var user = new User
            {
                username = username,
                user_role = userRole,
                email = email,
                password = password
            };
            Program.dal.AddUser(user);
            Console.WriteLine($"User {username} registered successfully.");
            loggedInUser = user;
        }

        private static void AddRestaurant()
        {
            if (loggedInUser.user_role != "admin" && loggedInUser.user_role != "owner")
            {
                Console.WriteLine("Only admins and owners can add restaurants.");
                return;
            }

            Console.Write("Enter restaurant name: ");
            var name = Console.ReadLine();
            Console.Write("Enter restaurant location: ");
            var location = Console.ReadLine();

            var admin = new Admin(loggedInUser.userid);
            admin.AddRestaurant(name, location);
        }

        private static void RemoveRestaurant()
        {
            Console.Write("Enter restaurant ID to remove: ");
            var restoId = int.Parse(Console.ReadLine());

            var admin = new Admin(loggedInUser.userid);
            admin.RemoveRestaurant(restoId);
        }

        private static void AssignOwner()
        {
            Console.Write("Enter restaurant ID: ");
            var restoId = int.Parse(Console.ReadLine());
            Console.Write("Enter owner ID: ");
            var ownerId = int.Parse(Console.ReadLine());

            var admin = new Admin(loggedInUser.userid);
            admin.AssignOwner(restoId, ownerId);
        }

        private static void AddMenuItem()
        {
            try
            {
                Console.Write("Enter restaurant ID: ");
                var restoId = int.Parse(Console.ReadLine());
                Console.Write("Enter item name: ");
                var itemName = Console.ReadLine();
                Console.Write("Enter item price: ");
                var price = int.Parse(Console.ReadLine());

                var owner = new Owner(loggedInUser.userid);
                owner.AddMenuItem(restoId, itemName, price);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void SearchRestaurantByLocation()
        {
            Console.Write("Enter location: ");
            var location = Console.ReadLine();

            var user = new User(loggedInUser.userid);
            user.SearchRestaurantByLocation(location);
        }

        private static void FilterItemsByPreference()
        {
            Console.Write("Enter food preference: ");
            var preference = Console.ReadLine();

            var user = new User(loggedInUser.userid);
            user.FilterItemsByPreference(preference);
        }

        private static void PlaceOrder()
        {
            Console.Write("Enter restaurant ID: ");
            var restoId = int.Parse(Console.ReadLine());
            Console.Write("Enter item IDs (comma separated): ");
            var itemIds = Console.ReadLine().Split(',').Select(int.Parse).ToList();

            var user = new User(loggedInUser.userid);
            user.PlaceOrder(restoId, itemIds);
        }

        private static void UpdateOrderStatus()
        {
            Console.Write("Enter order number: ");
            var orderNo = int.Parse(Console.ReadLine());
            Console.Write("Enter new status: ");
            var status = Console.ReadLine();

            var user = new User(loggedInUser.userid);
            Program.dal.UpdateOrderStatus(orderNo, status);
            Console.WriteLine($"Order status updated to {status}.");
        }

        private static void AddUser()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter user role (admin/owner/user): ");
            var userRole = Console.ReadLine();
            Console.Write("Enter email: ");
            var email = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var user = new User
            {
                username = username,
                user_role = userRole,
                email = email,
                password = password
            };
            Program.dal.AddUser(user);
            Console.WriteLine($"User {username} added successfully.");
        }
    }

    public class Admin : User
    {
        public Admin(int userId) : base(userId) { }

        public void AddRestaurant(string name, string location)
        {
            var restaurant = new Restaurant
            {
                resto_name = name,
                resto_location = location
            };
            Program.dal.AddRestaurant(restaurant);
            Console.WriteLine($"Restaurant {name} added successfully.");
        }

        public void RemoveRestaurant(int restoId)
        {
            Program.dal.RemoveRestaurant(restoId);
            Console.WriteLine($"Restaurant removed successfully.");
        }

        public void AssignOwner(int restoId, int ownerId)
        {
            Program.dal.AssignOwner(restoId, ownerId);
            Console.WriteLine($"Owner assigned to restaurant successfully.");
        }
    }

    public class Owner : User
    {
        public Owner(int userId) : base(userId) { }

        public void AddMenuItem(int restoId, string itemName, int price)
        {
            var menu = new Menu
            {
                item_name = itemName,
                price = price,
                resto_id = restoId
            };
            Program.dal.AddMenuItem(menu);
            Console.WriteLine($"Menu item {itemName} added successfully.");
        }
    }
}

