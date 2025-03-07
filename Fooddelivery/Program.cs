using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_delivery
{
    class Program
    {
        static void Main()
        {
            BusinessLayer bl = new BusinessLayer();
            Console.WriteLine("Welcome to the Food Delivery System");
            while (true)
            {
                Console.WriteLine("1. Login\n2. Register\n3. Exit");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = Console.ReadLine();
                    UserDTO user = bl.AuthenticateUser(email, password);

                    if (user != null)
                    {
                        Console.WriteLine($"Welcome {user.Name}, Role: {user.Role}");
                        if (user.Role == "Admin")
                        {
                            AdminMenu(bl);
                        }
                        else if (user.Role == "Owner")
                        {
                            OwnerMenu(bl);
                        }
                        else if (user.Role == "User")
                        {
                            UserMenu(bl);
                        }
                        else
                        {
                            Console.WriteLine("Invalid entry");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentials.");
                    }
                }
                else if (option == 2)
                {
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Phone: ");
                    string phone = Console.ReadLine();
                    Console.Write("Enter Role (Admin/Owner/User): ");
                    string role = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = Console.ReadLine();
                    Console.Write("Enter Location: ");
                    string location = Console.ReadLine();
                    bool success = bl.RegisterUser(new UserDTO { Name = name, Email = email, Phone = phone, Role = role, Password = password, Location = location });
                    if (success)
                    {
                        Console.WriteLine("User registered successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Registration failed.");
                    }
                }
                else if (option == 3)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }

        static void AdminMenu(BusinessLayer bl)
        {
            while (true)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Add Restaurant\n2. Remove Restaurant\n3. Assign Owner\n4. Add User\n5. Display Restaurants\n6. Display Users\n7. Display Orders\n8. Logout");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    Console.Write("Enter Restaurant Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Owner ID: ");
                    int ownerId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Location: ");
                    string location = Console.ReadLine();
                    bool success = bl.AddRestaurant(new RestaurantDTO { Name = name, OwnerId = ownerId, Location = location });
                    if (success)
                    {
                        Console.WriteLine("Restaurant added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add restaurant.");
                    }
                }
                else if (option == 2)
                {
                    Console.Write("Enter Restaurant ID to remove: ");
                    int restaurantId = Convert.ToInt32(Console.ReadLine());
                    bool success = bl.RemoveRestaurant(restaurantId);
                    if (success)
                    {
                        Console.WriteLine("Restaurant removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to remove restaurant.");
                    }
                }
                else if (option == 3)
                {
                    Console.Write("Enter Restaurant ID: ");
                    int restaurantId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Owner ID to assign: ");
                    int ownerId = Convert.ToInt32(Console.ReadLine());
                    bool success = bl.AssignOwner(restaurantId, ownerId);
                    if (success)
                    {
                        Console.WriteLine("Owner assigned successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to assign owner.");
                    }
                }
                else if (option == 4)
                {
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Phone: ");
                    string phone = Console.ReadLine();
                    Console.Write("Enter Role (Admin/Owner/User): ");
                    string role = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = Console.ReadLine();
                    Console.Write("Enter Location: ");
                    string location = Console.ReadLine();
                    bool success = bl.AddUser(new UserDTO { Name = name, Email = email, Phone = phone, Role = role, Password = password, Location = location });
                    if (success)
                    {
                        Console.WriteLine("User added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add user.");
                    }
                }
                else if (option == 5)
                {
                    List<RestaurantDTO> restaurants = bl.GetAllRestaurants();
                    if (restaurants.Any())
                    {
                        Console.WriteLine("Restaurants:");
                        foreach (var restaurant in restaurants)
                        {
                            Console.WriteLine($"ID: {restaurant.RestaurantId}, Name: {restaurant.Name}, Location: {restaurant.Location}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No restaurants found.");
                    }
                }
                else if (option == 6)
                {
                    List<UserDTO> users = bl.GetAllUsers();
                    if (users.Any())
                    {
                        Console.WriteLine("Users:");
                        foreach (var user in users)
                        {
                            Console.WriteLine($"ID: {user.UserId}, Name: {user.Name}, Email: {user.Email}, Phone: {user.Phone}, Role: {user.Role}, Location: {user.Location}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No users found.");
                    }
                }
                else if (option == 7)
                {
                    List<OrderDTO> orders = bl.GetAllOrders();
                    if (orders.Any())
                    {
                        Console.WriteLine("Orders:");
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"ID: {order.OrderId}, User ID: {order.UserId}, Restaurant ID: {order.RestaurantId}, Total Price: {order.TotalPrice}, Status: {order.Status}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No orders found.");
                    }
                }
                else if (option == 8)
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }

        static void OwnerMenu(BusinessLayer bl)
        {
            while (true)
            {
                Console.WriteLine("Owner Menu:");
                Console.WriteLine("1. Add Menu Item\n2. Update Menu Item\n3. View Orders for Your Restaurant\n4. Logout");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    Console.Write("Enter Restaurant ID: ");
                    int restaurantId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Menu Item Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    decimal price = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter Category: ");
                    string category = Console.ReadLine();
                    Console.Write("Enter Description: ");
                    string description = Console.ReadLine();
                    bool success = bl.AddMenuItem(new MenuDTO { RestaurantId = restaurantId, Name = name, Price = price, Category = category, Description = description });
                    if (success)
                    {
                        Console.WriteLine("Menu item added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add menu item.");
                    }
                }
                else if (option == 2)
                {
                    Console.Write("Enter Menu Item ID: ");
                    int menuItemId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Menu Item Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    decimal price = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter Category: ");
                    string category = Console.ReadLine();
                    Console.Write("Enter Description: ");
                    string description = Console.ReadLine();
                    bool success = bl.UpdateMenuItem(new MenuDTO { MenuId = menuItemId, Name = name, Price = price, Category = category, Description = description });
                    if (success)
                    {
                        Console.WriteLine("Menu item updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update menu item.");
                    }
                }
                else if (option == 3)
                {
                    Console.Write("Enter Restaurant ID: ");
                    int restaurantId = Convert.ToInt32(Console.ReadLine());
                    List<OrderDTO> orders = bl.GetOrdersByRestaurant(restaurantId);
                    if (orders.Any())
                    {
                        Console.WriteLine("Orders for Your Restaurant:");
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"Order ID: {order.OrderId}, User ID: {order.UserId}, Total Price: {order.TotalPrice}, Status: {order.Status}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No orders found for your restaurant.");
                    }
                }
                else if (option == 4)
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }
        static void UserMenu(BusinessLayer bl)
        {
            while (true)
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. View Restaurants in Your Location\n2. Filter Menu Items by Preferences\n3. Place Order\n4. View Order Status\n5. Logout");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    if (bl.LoggedInUser != null)
                    {
                        string location = bl.LoggedInUser.Location;
                        List<RestaurantDTO> restaurants = bl.GetRestaurantsByLocation(location);

                        if (restaurants.Any())
                        {
                            Console.WriteLine("Restaurants found:");
                            foreach (var restaurant in restaurants)
                            {
                                Console.WriteLine($"ID: {restaurant.RestaurantId}, Name: {restaurant.Name}, Location: {restaurant.Location}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No restaurants found in your location.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not logged in.");
                    }
                }
                else if (option == 2)
                {
                    Console.Write("Enter Restaurant ID: ");
                    int restaurantId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Category (e.g., Veg, Non-Veg, Vegan): ");
                    string category = Console.ReadLine();
                    List<MenuDTO> menuItems = bl.GetMenuByPreferences(restaurantId, category);

                    if (menuItems.Any())
                    {
                        Console.WriteLine("Menu items found:");
                        foreach (var item in menuItems)
                        {
                            Console.WriteLine($"ID: {item.MenuId}, Name: {item.Name}, Price: {item.Price}, Category: {item.Category}, Description: {item.Description}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No menu items found for this category.");
                    }
                }
                else if (option == 3)
                {
                    Console.Write("Enter Restaurant ID: ");
                    int restaurantId = Convert.ToInt32(Console.ReadLine());
                    List<MenuDTO> menuItems = bl.GetMenuByRestaurant(restaurantId);

                    if (menuItems.Any())
                    {
                        Console.WriteLine("Menu items found:");
                        foreach (var item in menuItems)
                        {
                            Console.WriteLine($"ID: {item.MenuId}, Name: {item.Name}, Price: {item.Price}, Category: {item.Category}, Description: {item.Description}");
                        }

                        List<OrderItemDTO> orderItems = new List<OrderItemDTO>();
                        while (true)
                        {
                            Console.Write("Enter Menu Item ID to add to order (or 0 to finish): ");
                            int menuItemId = Convert.ToInt32(Console.ReadLine());
                            if (menuItemId == 0)
                            {
                                break;
                            }
                            Console.Write("Enter Quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            MenuDTO menuItem = menuItems.FirstOrDefault(m => m.MenuId == menuItemId);
                            if (menuItem != null)
                            {
                                orderItems.Add(new OrderItemDTO
                                {
                                    MenuId = menuItemId,
                                    Quantity = quantity,
                                    Price = menuItem.Price * quantity
                                });
                            }
                            else
                            {
                                Console.WriteLine("Invalid Menu Item ID.");
                            }
                        }

                        decimal totalPrice = orderItems.Sum(item => item.Price);
                        bool success = bl.PlaceOrder(new OrderDTO { RestaurantId = restaurantId, TotalPrice = totalPrice }, orderItems);
                        if (success)
                        {
                            Console.WriteLine("Order placed successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to place order.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No menu items found for this restaurant.");
                    }
                }
                else if (option == 4)
                {
                    if (bl.LoggedInUser != null)
                    {
                        List<OrderDTO> orders = bl.GetOrdersByUser(bl.LoggedInUser.UserId);
                        if (orders.Any())
                        {
                            Console.WriteLine("Your Orders:");
                            foreach (var order in orders)
                            {
                                Console.WriteLine($"Order ID: {order.OrderId}, Restaurant ID: {order.RestaurantId}, Total Price: {order.TotalPrice}, Status: {order.Status}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No orders found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not logged in.");
                    }
                }
                else if (option == 5)
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }
    }
}