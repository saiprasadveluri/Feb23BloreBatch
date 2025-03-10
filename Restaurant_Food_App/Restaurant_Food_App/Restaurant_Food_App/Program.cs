using System;
using System.Collections.Generic;

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
            string password = Console.ReadLine();
            bool authenticated = bl.Authenticate(email, password);
            if (!authenticated)
            {
                Console.WriteLine("No Access to application");
                return;
            }
            while (true)
            {
                Console.WriteLine("Options: \n 0: Exit \n 1: Add New User \n 2: Add New Restaurant \n 3: Remove Restaurant \n 4: Assign Owner to Restaurant \n 5: Add Menu Item \n 6: Search Restaurants by Location \n 7: Place Order \n 8: Update Order Status");
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
                        PlaceOrder();
                        break;
                    case 8:
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

            Console.Write("Phone: ");
            long phone = long.Parse(Console.ReadLine());

            Console.Write("Owner Id: ");
            long ownerId = long.Parse(Console.ReadLine());
            RestaurantDTO newRestaurant = new RestaurantDTO()
            {
                res_name = name,
                city = location,
                phone = phone,
                OwnerId = ownerId
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
            Console.Write("Restaurant Id: ");
            long restaurantId = long.Parse(Console.ReadLine());

            Console.Write("Menu Item: ");
            string menuItem = Console.ReadLine();

            Console.Write("Unit Price: ");
            long unitPrice = long.Parse(Console.ReadLine());

            Console.Write("Food Type: ");
            string foodType = Console.ReadLine();

            MenuDTO menu = new MenuDTO()
            {
                restaurant_id = restaurantId,
                menu_item = menuItem,
                UnitPrice = unitPrice,
                Foodtype = foodType
            };

            bool status = bl.AddMenu(menu);
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

            List<RestaurantDTO> restaurants = bl.SearchRestaurantsBycity(location);
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine($"Restaurant Id: {restaurant.restaurant_id}, Name: {restaurant.res_name}, Location: {restaurant.city}, Owner Id: {restaurant.OwnerId}");
            }
        }

        private static void PlaceOrder()
        {
            Console.Write("User Id: ");
            long userId = long.Parse(Console.ReadLine());

            Console.Write("Restaurant Id: ");
            long restaurantId = long.Parse(Console.ReadLine());

            List<Order_itemDTO> orderItems = new List<Order_itemDTO>();
            while (true)
            {
                Console.Write("Menu Item Id: ");
                long menuItemId = long.Parse(Console.ReadLine());

                Console.Write("Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                orderItems.Add(new Order_itemDTO()
                {
                    menu_id = menuItemId,
                    quantity = quantity
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
                user_id = userId,
                restaurant_id = restaurantId,
                status = "Pending",
                order_items = orderItems
            };

            bool status = bl.PlaceOrder(order);
            if (!status)
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

            bool updateStatus = bl.UpdateOrderstatus(orderId, status);
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
