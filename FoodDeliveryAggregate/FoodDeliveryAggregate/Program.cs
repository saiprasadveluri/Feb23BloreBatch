using System;
using System.Collections.Generic;

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
            string password = Console.ReadLine();
            bool authenticated = bl.Authenticate(email, password);
            if (!authenticated)
            {
                Console.WriteLine("No Access to application");
                return;
            }
            while (true)
            {
                Console.WriteLine("Options: \n 0: Exit \n 1: Add New User \n 2: Add New Restaurant \n 3: Remove Restaurant \n 4: Assign Owner to Restaurant \n 5: Add Menu Item \n 6: Search Restaurants by Location \n 7: Filter Menu Items \n 8: Place Order \n 9: Update Order Status");
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 0:
                        {
                            bl.CloseApp();
                            return;
                        }
                    case 1:
                        {
                            AddNewUser();
                        }
                        break;
                    case 2:
                        AddNewRestaurant();
                        break;
                    case 3:
                        RemoveRestaurant();
                        break;
                    case 4:
                        AssignOwnerToRestaurant();
                        break;
                    case 5:
                        AddMenuItem();
                        break;
                    case 6:
                        SearchRestaurantsByLocation();
                        break;
                    case 7:
                        FilterMenuItems();
                        break;
                    case 8:
                        PlaceOrder();
                        break;
                    case 9:
                        UpdateOrderStatus();
                        break;
                    default:
                        Console.WriteLine("Wrong Option...");
                        break;
                }
            }
        }

        private static void AddNewRestaurant()
        {
            Console.WriteLine("New Restaurant: ");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Location: ");
            string location = Console.ReadLine();

            Console.Write("Owner Id: ");
            long ownerId = long.Parse(Console.ReadLine());
            RestaurantDTO newRestaurant = new RestaurantDTO()
            {
                Name = name,
                Location = location,
                UserId = ownerId
            };

            bool status = bl.AddNewRestaurant(newRestaurant);
            if (!status)
            {
                Console.WriteLine("Error In Adding New Restaurant");
            }
            else
            {
                Console.WriteLine("Success in Adding New Restaurant");
            }
        }

        public static void AddNewUser()
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
            UserDTO obj = new UserDTO()
            {
                Name = name,
                Email = email,
                Password = password,
                RoleName = roleName
            };

            bool status = bl.AddNewUser(obj);
            if (!status)
            {
                Console.WriteLine("Error In Adding New User");
            }
            else
            {
                Console.WriteLine("Success in Adding New User");
            }
        }

        private static void RemoveRestaurant()
        {
            Console.Write("Restaurant Id: ");
            long restaurantId = long.Parse(Console.ReadLine());

            bool status = bl.RemoveRestaurant(restaurantId);
            if (!status)
            {
                Console.WriteLine("Error In Removing Restaurant");
            }
            else
            {
                Console.WriteLine("Success in Removing Restaurant");
            }
        }

        private static void AssignOwnerToRestaurant()
        {
            Console.Write("Restaurant Id: ");
            long restaurantId = long.Parse(Console.ReadLine());

            Console.Write("Owner Id: ");
            long ownerId = long.Parse(Console.ReadLine());

            bool status = bl.AssignOwnerToRestaurant(restaurantId, ownerId);
            if (!status)
            {
                Console.WriteLine("Error In Assigning Owner to Restaurant");
            }
            else
            {
                Console.WriteLine("Success in Assigning Owner to Restaurant");
            }
        }

        private static void AddMenuItem()
        {
            Console.Write("RestaurantID: ");
            int resid = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            string menuname = Console.ReadLine();

            Console.Write("Price: ");
            int menuprice = int.Parse(Console.ReadLine());

            Console.Write("Category : ");
            string category = Console.ReadLine();

            MenuItemDTO menuItem = new MenuItemDTO()
            {
                ResID = resid,
                MenuName = menuname,
                Price = menuprice,
                Category = category
            };

            bool status = bl.AddMenuItem(menuItem);
            if (!status)
            {
                Console.WriteLine("Error In Adding Menu Item");
            }
            else
            {
                Console.WriteLine("Success in Adding Menu Item");
            }
        }

        private static void SearchRestaurantsByLocation()
        {
            Console.Write("Location: ");
            string location = Console.ReadLine();

            List<RestaurantDTO> restaurants = bl.SearchRestaurantsByLocation(location);
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine($"Restaurant Id: {restaurant.RId}, Name: {restaurant.Name}, Location: {restaurant.Location}");
            }
        }

        private static void FilterMenuItems()
        {
            Console.Write("Preference: ");
            string preference = Console.ReadLine();

            List<MenuItemDTO> menuItems = bl.FilterMenuItems(preference);
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine($" Menu Name: {menuItem.MenuName}, RestaurantID: {menuItem.ResID},Price: {menuItem.Price},Category: {menuItem.Category}");
            }
        }

        private static void PlaceOrder()
        {
            Console.Write("Oder Id: ");
            long orderId = long.Parse(Console.ReadLine());

            Console.Write("User Id: ");
            long userId = long.Parse(Console.ReadLine());

            Console.Write("Restaurant Id: ");
            long restaurantId = long.Parse(Console.ReadLine());

            Console.Write("Total Amount  ");
            int amount = int.Parse(Console.ReadLine());

            Console.Write("Status ");
            string status = Console.ReadLine();

            List<OrderItemDTO> orderItems = new List<OrderItemDTO>();
            while (true)
            {
                Console.Write("Menu Item Id: ");
                int menuItemId = int.Parse(Console.ReadLine());

                Console.Write("Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                orderItems.Add(new OrderItemDTO()
                {
                    MenuItemId = menuItemId,
                    Quantity = quantity
                });

                Console.Write("Add more items? (y/n): ");
                string addMore = Console.ReadLine();
                if (addMore.ToLower() != "y")
                {
                    break;
                }
            }

            OrderDTO order = new OrderDTO()
            {
                OrderId = orderId,
                UserId = userId,
                RestaurantId = restaurantId,
                Total = amount,
                Status = "Pending",
                OrderItems = orderItems
            };

            bool Status = bl.PlaceOrder(order);
            if (!Status)
            {
                Console.WriteLine("Error In Placing Order");
            }
            else
            {
                Console.WriteLine("Success in Placing Order");
            }
        }

        private static void UpdateOrderStatus()
        {
            Console.Write("Order Id: ");
            long orderId = long.Parse(Console.ReadLine());

            Console.Write("Status: ");
            string status = Console.ReadLine();

            bool updateStatus = bl.UpdateOrderStatus(orderId, status);
            if (!updateStatus)
            {
                Console.WriteLine("Error In Updating Order Status");
            }
            else
            {
                Console.WriteLine("Success in Updating Order Status");
            }
        }
    }
}
