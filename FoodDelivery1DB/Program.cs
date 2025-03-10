using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register New User");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        RegisterNewUser();
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void Login()
        {
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            User user = DatabaseHelper.LoginUser(email, password);
            if (user != null)
            {
                Console.WriteLine($"Welcome, {user.Name}!");
                ShowRoleBasedMenu(user);
            }
            else
            {
                Console.WriteLine("Invalid email or password.");
            }
        }

        static void RegisterNewUser()
        {
            Console.WriteLine("Enter User Name:");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter User Email:");
            string userEmail = Console.ReadLine();
            Console.WriteLine("Enter User Phone:");
            string userPhone = Console.ReadLine();
            Console.WriteLine("Enter User Location:");
            string userLocation = Console.ReadLine();
            Console.WriteLine("Enter User Password:");
            string userPassword = Console.ReadLine();

            User newUser = new User { Name = userName, Email = userEmail, Phone = userPhone, Location = userLocation, Password = userPassword };
            bool userAdded = DatabaseHelper.AddUser(newUser);
            Console.WriteLine($"User added: {userAdded}");
        }

        static void ShowRoleBasedMenu(User user)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Role-Based Menu:");
                if (user.Role == "Admin")
                {
                    Console.WriteLine("1. Add Restaurant");
                    Console.WriteLine("2. Remove Restaurant");
                    Console.WriteLine("3. Assign Owner to Restaurant");
                }
                if (user.Role == "Owner")
                {
                    Console.WriteLine("4. Add Menu Item");
                }
                Console.WriteLine("5. Search Restaurants by Location");
                Console.WriteLine("6. Filter Items by Preferences");
                Console.WriteLine("7. Place Order");
                Console.WriteLine("8. List Orders");
                Console.WriteLine("9. Update Order Status to Delivered");
                Console.WriteLine("10. Logout");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        if (user.Role == "Admin") AddRestaurant();
                        break;
                    case 2:
                        if (user.Role == "Admin") RemoveRestaurant();
                        break;
                    case 3:
                        if (user.Role == "Admin") AssignOwnerToRestaurant();
                        break;
                    case 4:
                        if (user.Role == "Owner") AddMenuItem();
                        break;
                    case 5:
                        SearchRestaurantsByLocation();
                        break;
                    case 6:
                        FilterItemsByPreferences();
                        break;
                    case 7:
                        PlaceOrder();
                        break;
                    case 8:
                        ListOrders();
                        break;
                    case 9:
                        UpdateOrderStatusToDelivered();
                        break;
                    case 10:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddRestaurant()
        {
            Console.WriteLine("Enter Restaurant Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Restaurant Location:");
            string location = Console.ReadLine();

            Restaurant newRestaurant = new Restaurant { Name = name, Location = location };
            bool restaurantAdded = DatabaseHelper.AddRestaurant(newRestaurant);
            Console.WriteLine($"Restaurant added: {restaurantAdded}");
        }

        static void RemoveRestaurant()
        {
            Console.WriteLine("Enter Restaurant ID:");
            int restaurantId = int.Parse(Console.ReadLine());

            bool restaurantRemoved = DatabaseHelper.RemoveRestaurant(restaurantId);
            Console.WriteLine($"Restaurant removed: {restaurantRemoved}");
        }

        static void AssignOwnerToRestaurant()
        {
            Console.WriteLine("Enter Restaurant ID:");
            int restaurantId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Owner User ID:");
            int ownerId = int.Parse(Console.ReadLine());

            bool ownerAssigned = DatabaseHelper.AssignOwnerToRestaurant(restaurantId, ownerId);
            Console.WriteLine($"Owner assigned: {ownerAssigned}");
        }

        static void AddMenuItem()
        {
            Console.WriteLine("Enter Restaurant ID:");
            int restaurantId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Menu Item Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Menu Item Description:");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Menu Item Price:");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Menu Item Category:");
            string category = Console.ReadLine();

            MenuItem newMenuItem = new MenuItem { RestaurantId = restaurantId, Name = name, Description = description, Price = price, Category = category };
            bool menuItemAdded = DatabaseHelper.AddMenuItem(newMenuItem);
            Console.WriteLine($"Menu item added: {menuItemAdded}");
        }

        static void SearchRestaurantsByLocation()
        {
            Console.WriteLine("Enter Location:");
            string location = Console.ReadLine();

            var restaurants = DatabaseHelper.SearchRestaurantsByLocation(location);
            Console.WriteLine("Restaurants:");
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine($"ID: {restaurant.RestaurantId}, Name: {restaurant.Name}, Location: {restaurant.Location}");
            }
        }

        static void FilterItemsByPreferences()
        {
            Console.WriteLine("Enter Food Preference:");
            string preference = Console.ReadLine();

            var menuItems = DatabaseHelper.FilterItemsByPreferences(preference);
            Console.WriteLine("Menu Items:");
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine($"ID: {menuItem.ItemId}, Name: {menuItem.Name}, Description: {menuItem.Description}, Price: {menuItem.Price}, Category: {menuItem.Category}");
            }
        }

        static void PlaceOrder()
        {
            Console.WriteLine("Enter User ID:");
            int userId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Restaurant ID:");
            int restaurantId = int.Parse(Console.ReadLine());

            Order newOrder = new Order { UserId = userId, RestaurantId = restaurantId, OrderStatus = "PLACED" };
            bool orderPlaced = DatabaseHelper.PlaceOrder(newOrder);
            Console.WriteLine($"Order placed: {orderPlaced}");
        }

        static void ListOrders()
        {
            var orders = DatabaseHelper.ListOrders();
            Console.WriteLine("Orders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.OrderId}, User ID: {order.UserId}, Restaurant ID: {order.RestaurantId}, Status: {order.OrderStatus}, Date: {order.OrderDate}");
            }
        }

        static void UpdateOrderStatusToDelivered()
        {
            Console.WriteLine("Enter Order ID:");
            int orderId = int.Parse(Console.ReadLine());

            bool statusUpdated = DatabaseHelper.UpdateOrderStatusToDelivered(orderId);
            Console.WriteLine($"Order status updated: {statusUpdated}");
        }
    }
}
