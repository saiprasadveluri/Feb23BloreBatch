using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Food_Deliveryapp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccessLayer dal = new DataAccessLayer();
            UserDTO loggedInUser = null;
            try
            {
                while (true)
                {
                    Console.WriteLine("Welcome to the Food Delivery App");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Register");
                    Console.WriteLine("3. Add New Restaurant (Admin)");
                    Console.WriteLine("4. Remove Restaurant (Admin)");
                    Console.WriteLine("5. Assign Owner to Restaurant (Admin)");
                    Console.WriteLine("6. Search Restaurants by Location");
                    Console.WriteLine("7. Search Menu Items by Preference");
                    Console.WriteLine("8. Place Order");
                    Console.WriteLine("9. Update Order Status to Delivered");
                    Console.WriteLine("10. Enter Menu (Restaurant Owner)");
                    Console.WriteLine("11. View Orders");
                    Console.WriteLine("12. Show Menu List");
                    Console.WriteLine("13. Show Order List");
                    Console.WriteLine("14. Exit");
                    Console.Write("Select an option: ");
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            string password = Console.ReadLine();
                            loggedInUser = dal.Login(email, password);
                            if (loggedInUser != null)
                            {
                                Console.WriteLine("Login successful!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid email or password.");
                            }
                            break;

                        case 2:
                            UserDTO newUser = new UserDTO();
                            Console.Write("Enter Name: ");
                            newUser.Name = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            newUser.Email = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            newUser.Password = Console.ReadLine();
                            Console.Write("Enter Address: ");
                            newUser.Address = Console.ReadLine();
                            Console.Write("Enter Role (Admin, Customer, RestaurantOwner): ");
                            newUser.Role = Console.ReadLine();
                            if (dal.AddNewUser(newUser))
                            {
                                Console.WriteLine("User registered successfully!");
                            }
                            else
                            {
                                Console.WriteLine("User registration failed.");
                            }
                            break;

                        case 3:
                            if (loggedInUser == null || loggedInUser.Role != "Admin")
                            {
                                Console.WriteLine("Only admins can add a new restaurant.");
                                break;
                            }
                            RestaurantDTO newRestaurant = new RestaurantDTO();
                            Console.Write("Enter Restaurant Name: ");
                            newRestaurant.rname = Console.ReadLine();
                            Console.Write("Enter Location: ");
                            newRestaurant.location = Console.ReadLine();
                            Console.Write("Enter Owner ID: ");
                            newRestaurant.ownerid = long.Parse(Console.ReadLine());
                            if (dal.AddNewRestaurant(newRestaurant))
                            {
                                Console.WriteLine("Restaurant added successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add restaurant.");
                            }
                            break;

                        case 4:
                            if (loggedInUser == null || loggedInUser.Role != "Admin")
                            {
                                Console.WriteLine("Only admins can remove a restaurant.");
                                break;
                            }
                            Console.Write("Enter Restaurant ID to remove: ");
                            long restaurantId = long.Parse(Console.ReadLine());
                            if (dal.RemoveRestaurant(restaurantId))
                            {
                                Console.WriteLine("Restaurant removed successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to remove restaurant.");
                            }
                            break;

                        case 5:
                            if (loggedInUser == null || loggedInUser.Role != "Admin")
                            {
                                Console.WriteLine("Only admins can assign an owner to a restaurant.");
                                break;
                            }
                            Console.Write("Enter Restaurant ID: ");
                            long rid = long.Parse(Console.ReadLine());
                            Console.Write("Enter Owner ID: ");
                            long ownerId = long.Parse(Console.ReadLine());
                            if (dal.AssignOwnerToRestaurant(rid, ownerId))
                            {
                                Console.WriteLine("Owner assigned to restaurant successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to assign owner to restaurant.");
                            }
                            break;

                        case 6:
                            Console.Write("Enter Location: ");
                            string location = Console.ReadLine();
                            List<RestaurantDTO> restaurants = dal.GetRestaurantsByLocation(location);
                            foreach (var restaurant in restaurants)
                            {
                                Console.WriteLine($"ID: {restaurant.rid}, Name: {restaurant.rname}, Location: {restaurant.location}");
                            }
                            break;

                        case 7:
                            Console.Write("Enter Food Preference: ");
                            string preference = Console.ReadLine();
                            List<MenuDTO> items = dal.GetItemsByPreference(preference);
                            foreach (var item in items)
                            {
                                Console.WriteLine($"ID: {item.mid}, Name: {item.mname}, Price: {item.price}, Category: {item.category}");
                            }
                            break;

                        case 8:
                            if (loggedInUser == null)
                            {
                                Console.WriteLine("Please login first.");
                                break;
                            }
                            OrderDTO newOrder = new OrderDTO();
                            newOrder.uid = loggedInUser.Uid;
                            Console.Write("Enter Restaurant ID: ");
                            newOrder.rid = long.Parse(Console.ReadLine());
                            Console.Write("Enter Menu Item ID: ");
                            newOrder.mid = long.Parse(Console.ReadLine());
                            Console.Write("Enter Quantity: ");
                            newOrder.total = long.Parse(Console.ReadLine());
                            newOrder.orderdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            newOrder.status = "Pending";
                            if (dal.PlaceOrder(newOrder))
                            {
                                Console.WriteLine("Order placed successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Order placement failed.");
                            }
                            break;

                        case 9:
                            Console.Write("Enter Order ID: ");
                            long orderId = long.Parse(Console.ReadLine());
                            if (dal.UpdateOrderStatus(orderId, "Delivered"))
                            {
                                Console.WriteLine("Order status updated to Delivered.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update order status.");
                            }
                            break;

                        case 10:
                            if (loggedInUser == null || loggedInUser.Role != "RestaurantOwner")
                            {
                                Console.WriteLine("Only restaurant owners can add a new menu.");
                                break;
                            }
                            MenuDTO newMenu = new MenuDTO();
                            Console.Write("Enter Menu Name: ");
                            newMenu.mname = Console.ReadLine();
                            Console.Write("Enter Restaurant ID: ");
                            newMenu.rid = long.Parse(Console.ReadLine());
                            Console.Write("Enter Price: ");
                            newMenu.price = long.Parse(Console.ReadLine());
                            Console.Write("Enter Category: ");
                            newMenu.category = Console.ReadLine();
                            newMenu.orderedby = loggedInUser.Uid.ToString();
                            if (dal.AddMenu(newMenu))
                            {
                                Console.WriteLine("Menu added successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add menu.");
                            }
                            break;

                        case 11:
                            if (loggedInUser == null)
                            {
                                Console.WriteLine("Please login first.");
                                break;
                            }
                            List<OrderDTO> orders = dal.GetOrdersByUserId(loggedInUser.Uid);
                            foreach (var order in orders)
                            {
                                Console.WriteLine($"Order ID: {order.oid}, Menu Item ID: {order.mid}, Restaurant ID: {order.rid}, Quantity: {order.total}, Status: {order.status}, Order Date: {order.orderdate}");
                            }
                            break;

                        case 12:
                            List<MenuDTO> menus = dal.GetAllMenus();
                            foreach (var menu in menus)
                            {
                                Console.WriteLine($"Menu ID: {menu.mid}, Name: {menu.mname}, Restaurant ID: {menu.rid}, Price: {menu.price}, Category: {menu.category}, Ordered By: {menu.orderedby}");
                            }
                            break;

                        case 13:
                            List<OrderDTO> allOrders = dal.GetAllOrders();
                            foreach (var order in allOrders)
                            {
                                Console.WriteLine($"Order ID: {order.oid}, Menu Item ID: {order.mid}, Restaurant ID: {order.rid}, User ID: {order.uid}, Quantity: {order.total}, Status: {order.status}, Order Date: {order.orderdate}");
                            }
                            break;

                        case 14:
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
            finally
            {
                dal.CloseApp();
            }
        }
    }
}