using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooddeliveryusingADO
{
    class Program
    {

        public static void Main(string[] args)
        {
            Businesslayer service = new Businesslayer();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Food Delivery App");
                Console.WriteLine("==================");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Add Restaurant");
                Console.WriteLine("3. Add Menu Item");
                Console.WriteLine("4. Search Restaurants by Location");
                Console.WriteLine("5. Filter Items by Food Preference");
                Console.WriteLine("6. Place Order");
                Console.WriteLine("7. Update Order Status");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AdminLogin(service);
                        break;
                    case 2:
                        AddRestaurant(service);
                        break;
                    case 3:
                        AddMenuItem(service);
                        break;
                    case 4:
                        SearchRestaurantsByLocation(service);
                        break;
                    case 5:
                        FilterItemsByPreference(service);
                        break;
                    case 6:
                        PlaceOrder(service);
                        break;
                    case 7:
                        UpdateOrderStatus(service);
                        break;
                    case 8:
                        exit = true;
                        service.CloseApp();
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
                
            

            public static void AdminLogin(Businesslayer service)
            {
                Console.Write("Enter Admin Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter Admin Password: ");
                string password = Console.ReadLine();
                var admin = service.AuthenticateAdmin(email, password);
                if (admin != null)
                {
                    Console.WriteLine($"Welcome, {admin.Name}!");
                }
                else
                {
                    Console.WriteLine("Invalid email or password.");
                }
            }

            public static void AddRestaurant(Businesslayer service)
            {
                Console.Write("Enter Restaurant Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Restaurant Location: ");
                string location = Console.ReadLine();
                Console.Write("Enter Admin ID: ");
                int adminId = Convert.ToInt32(Console.ReadLine());

                Restaurant restaurant = new Restaurant { Name = name, Location = location, AdminID = adminId };
                service.AddRestaurant(restaurant);
            }

            public static void AddMenuItem(Businesslayer service)
            {
                Console.Write("Enter Restaurant ID: ");
                int restaurantId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Item Name: ");
                string itemName = Console.ReadLine();
                Console.Write("Enter Description: ");
                string description = Console.ReadLine();
                Console.Write("Enter Price: ");
                int price = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Food Preference (Vegetarian, Vegan, Non-Vegetarian, etc.): ");
                string foodPreference = Console.ReadLine();

                Menu menu = new Menu { RestaurantID = restaurantId, ItemName = itemName, Description = description, Price = price, FoodPreference = foodPreference };
                service.AddMenuItem(menu);
            }

            public static void SearchRestaurantsByLocation(Businesslayer service)
            {
                Console.Write("Enter Location: ");
                string location = Console.ReadLine();
                var restaurants = service.SearchRestaurantsByLocation(location);
                DisplayRestaurants(restaurants);
            }

            static void FilterItemsByPreference(Businesslayer service)
            {
                Console.Write("Enter Food Preference (Vegetarian, Vegan, Non-Vegetarian, etc.): ");
                string preference = Console.ReadLine();
                var menuItems = service.FilterItemsByPreference(preference);
                DisplayMenuItems(menuItems);
            }

            public static void PlaceOrder(Businesslayer service)
            {
                Console.Write("Enter User ID: ");
                int userId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Restaurant ID: ");
                int restaurantId = Convert.ToInt32(Console.ReadLine());

                Order order = new Order { UserID = userId, RestaurentID = restaurantId, OrderDate = DateTime.Now, Status = "Pending" };
                List<OrderItem> orderItems = new List<OrderItem>();

                while (true)
                {
                    Console.Write("Enter Menu ID: ");
                    int menuId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Quantity: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    orderItems.Add(new OrderItem { MenuID = menuId, Quantity = quantity });

                    Console.Write("Add more items? (yes/no): ");
                    string addMore = Console.ReadLine();
                    if (addMore.ToLower() != "yes")
                    {
                        break;
                    }
                }

                service.PlaceOrder(order, orderItems);
            }

            public static void UpdateOrderStatus(Businesslayer service)
            {
                Console.Write("Enter Order ID: ");
                int orderId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter New Status: ");
                string status = Console.ReadLine();
                service.UpdateOrderStatus(orderId, status);
            }

            public static void DisplayRestaurants(List<Restaurant> restaurants)
            {
                foreach (var restaurant in restaurants)
                {
                    Console.WriteLine($"{restaurant.Name} located at {restaurant.Location}");
                }
            }

            public static void DisplayMenuItems(List<Menu> menus)
            {
                foreach (var menu in menus)
                {
                    Console.WriteLine($"{menu.ItemName}: {menu.Description} - {menu.Price} ({menu.FoodPreference})");
                }
            }
        }
    }


    

