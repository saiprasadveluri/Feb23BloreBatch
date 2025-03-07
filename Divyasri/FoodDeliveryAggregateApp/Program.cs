using System;
using System.Collections.Generic;

namespace FoodDeliveryAggregateApp
{
    class Program
    {
        static BusinessAccessLayer bal = new BusinessAccessLayer();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Food Delivery App");
            while (true)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter your choice:");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Register();
                        break;
                    case 3:
                        bal.CloseApp();
                        return;
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

            if (bal.Authentication(email, password))
            {
                Console.WriteLine("Login Success");
                if (bal.loggedInUser.Rolename.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    AdminMenu();
                }
                else if (bal.loggedInUser.Rolename.Equals("User", StringComparison.OrdinalIgnoreCase))
                {
                    UserMenu();
                }
                else if (bal.loggedInUser.Rolename.Equals("Owner", StringComparison.OrdinalIgnoreCase))
                {
                    OwnerMenu();
                }
            }
            else
            {
                Console.WriteLine("Login Failed. Please check your credentials.");
            }
        }

        static void Register()
        {
            Console.WriteLine("Enter Username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Role (Admin/User/Owner):");
            string role = Console.ReadLine();
            Console.WriteLine("Enter Location:");
            string location = Console.ReadLine();

            UserDTO newUser = new UserDTO
            {
                Username = username,
                Password = password,
                Email = email,
                Rolename = role,
                Location = location
            };

            if (bal.AddUser(newUser))
            {
                Console.WriteLine("Registration Successful");
            }
            else
            {
                Console.WriteLine("Registration Failed");
            }
        }

        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Restaurant");
                Console.WriteLine("2. Assign Owner");
                Console.WriteLine("3. View Orders");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");
                int adminChoice = int.Parse(Console.ReadLine());

                switch (adminChoice)
                {
                    case 1:
                        AddRestaurant();
                        break;
                    case 2:
                        AssignOwner();
                        break;
                    case 3:
                        ViewOrders();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("1. View Restaurants");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3. View Orders");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");
                int userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        ViewRestaurants();
                        break;
                    case 2:
                        PlaceOrder();
                        break;
                    case 3:
                        ViewUserOrders();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void OwnerMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Menu");
                Console.WriteLine("2. Exit");
                Console.WriteLine("Enter your choice:");
                int ownerChoice = int.Parse(Console.ReadLine());

                switch (ownerChoice)
                {
                    case 1:
                        AddMenuByOwner();
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddRestaurant()
        {
            Console.WriteLine("Enter Restaurant Name:");
            string restaurantName = Console.ReadLine();
            Console.WriteLine("Enter Location:");
            string location = Console.ReadLine();
            Console.WriteLine("Enter Owner Id:");
            long ownerId = long.Parse(Console.ReadLine());

            RestuarantDTO restuarant = new RestuarantDTO
            {
                Name = restaurantName,
                Location = location,
                OwnerId = ownerId
            };

            if (bal.AddRestaurantByAdmin(restuarant))
            {
                Console.WriteLine("Restaurant Added Successfully");
            }
            else
            {
                Console.WriteLine("Restaurant Not Added");
            }
        }

        static void AssignOwner()
        {
            Console.WriteLine("Enter Restaurant Id:");
            long restaurantId = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Owner Id:");
            long ownerId = long.Parse(Console.ReadLine());

            if (bal.AssignOwnerByAdmin(restaurantId, ownerId))
            {
                Console.WriteLine("Owner Assigned Successfully");
            }
            else
            {
                Console.WriteLine("Owner Not Assigned");
            }
        }

        static void AddMenuByOwner()
        {
            Console.WriteLine("Enter Menu Name:");
            string menuName = Console.ReadLine();
            Console.WriteLine("Enter Restaurant Id:");
            long restaurantId = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Unit Price:");
            decimal unitPrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Food Type:");
            string foodType = Console.ReadLine();

            MenuDTO menu = new MenuDTO
            {
                MenuName = menuName,
                RID = restaurantId,
                UnitPrice = unitPrice,
                FoodType = foodType
            };

            if (bal.AddMenuByOwner(menu))
            {
                Console.WriteLine("Menu Added Successfully");
            }
            else
            {
                Console.WriteLine("Menu Not Added");
            }
        }

        static void ViewOrders()
        {
            List<OrderDTO> orders = bal.GetOrders();
            foreach (OrderDTO order in orders)
            {
                Console.WriteLine("Order Id: " + order.OrderId);
                Console.WriteLine("Restaurant Id: " + order.RID);
                Console.WriteLine("Ordered By: " + order.OrderBy);
                Console.WriteLine("Status: " + order.Status);
                Console.WriteLine("Order Date: " + order.OrderDate);
                Console.WriteLine();
            }
        }

        static void ViewRestaurants()
        {
            Console.WriteLine("Enter Location:");
            string location = Console.ReadLine();
            List<RestuarantDTO> restaurants = bal.GetRestuarantsByLocation(location);
            foreach (RestuarantDTO restaurant in restaurants)
            {
                Console.WriteLine("Restaurant Id: " + restaurant.RID);
                Console.WriteLine("Name: " + restaurant.Name);
                Console.WriteLine("Location: " + restaurant.Location);
                Console.WriteLine("Owner Id: " + restaurant.OwnerId);
                Console.WriteLine();
            }
        }

        static void PlaceOrder()
        {
            Console.WriteLine("Enter Restaurant Id:");
            long restaurantId = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Menu Id:");
            long menuId = long.Parse(Console.ReadLine());

            if (bal.PlaceOrderByUser(restaurantId, menuId))
            {
                Console.WriteLine("Order Placed Successfully");
            }
            else
            {
                Console.WriteLine("Order Not Placed");
            }
        }

        static void ViewUserOrders()
        {
            Console.WriteLine("Enter Order Id:");
            long orderId = long.Parse(Console.ReadLine());
            OrderDTO order = bal.ViewOrderByUser(orderId);
            if (order != null)
            {
                Console.WriteLine("Order Id: " + order.OrderId);
                Console.WriteLine("Restaurant Id: " + order.RID);
                Console.WriteLine("Ordered By: " + order.OrderBy);
                Console.WriteLine("Status: " + order.Status);
                Console.WriteLine("Order Date: " + order.OrderDate);
            }
            else
            {
                Console.WriteLine("Order Not Found");
            }
        }
    }
}
