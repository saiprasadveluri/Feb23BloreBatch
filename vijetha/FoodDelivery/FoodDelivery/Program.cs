using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            var dal = new DataAccessLayer();

            while (true)
            {
                Console.WriteLine("Welcome to Food Delivery Aggregator App!");
                Console.WriteLine("Select Role: 1. Admin 2. Owner 3. User 4. Exit");
                var roleChoice = Console.ReadLine();

                switch (roleChoice)
                {
                    case "1":
                        AdminFlow(dal);
                        break;
                    case "2":
                        OwnerFlow(dal);
                        break;
                    case "3":
                        UserFlow(dal);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid Choice. Try Again.");
                        break;
                }
            }
        }

        static void AdminFlow(DataAccessLayer dal)
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Menu");
                Console.WriteLine("1. Add Restuarnt");
                Console.WriteLine("2. Remove Restuarnt");
                Console.WriteLine("3. Assign Owner");
                Console.WriteLine("4. Back");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var restuarnt = new RestuarntDTO();
                        Console.Write("Enter Restuarnt Name: ");
                        restuarnt.Name = Console.ReadLine();
                        Console.Write("Enter Location: ");
                        restuarnt.Location = Console.ReadLine();
                        restuarnt.OwnerId = null; 
                        dal.AddRestuarnt(restuarnt);
                        Console.WriteLine("Restuarnt Added.");
                        break;

                    case "2":
                        Console.Write("Enter Restuarnt ID to Remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        dal.RemoveRestuarnt(removeId);
                        Console.WriteLine("Restuarnt Removed.");
                        break;

                    case "3":
                        Console.Write("Enter Restuarnt ID: ");
                        int restuarntId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Owner ID: ");
                        int ownerId = int.Parse(Console.ReadLine());
                        dal.AssignOwner(restuarntId, ownerId);
                        Console.WriteLine("Owner Assigned.");
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }

        static void OwnerFlow(DataAccessLayer dal)
        {
            Console.Write("Enter Your Restuarnt ID: ");
            int restuarntId = int.Parse(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("\nOwner Menu");
                Console.WriteLine("1. Add Menu Item");
                Console.WriteLine("2. Back");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var menuItem = new MenuItemDTO();
                        menuItem.RestuarntId = restuarntId;
                        Console.Write("Enter Dish Name: ");
                        menuItem.Name = Console.ReadLine();
                        Console.Write("Enter Price: ");
                        menuItem.Price = decimal.Parse(Console.ReadLine());
                        dal.AddMenuItem(menuItem);
                        Console.WriteLine("Menu Item Added.");
                        break;

                    case "2":
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }

        static void UserFlow(DataAccessLayer dal)
        {
            Console.Write("Enter Your User ID: ");
            int userId = int.Parse(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("\nUser Menu");
                Console.WriteLine("1. Search Restuarnt by Location");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3. Mark Order Delivered");
                Console.WriteLine("4. Back");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Location to Search: ");
                        string location = Console.ReadLine();
                        var restuarnts = dal.SearchRestuarnts(location);

                        Console.WriteLine("Available Restuarnts:");
                        foreach (var restuarnt in restuarnts)
                        {
                            Console.WriteLine($"{restuarnt.RestuarntId}. {restuarnt.Name} - {restuarnt.Location}");
                        }
                        break;

                    case "2":
                        Console.Write("Enter Restuarnt ID to Order From: ");
                        int restuarntId = int.Parse(Console.ReadLine());

                        Console.Write("Enter Dish Name to Filter Menu (or leave empty to show all): ");
                        string filterDish = Console.ReadLine();
                        var menuItems = dal.FilterMenuItems(restuarntId, filterDish);

                        Console.WriteLine("Menu:");
                        foreach (var item in menuItems)
                        {
                            Console.WriteLine($"{item.MenuItemId}. {item.Name} - ₹{item.Price}");
                        }

                        var orderItems = new List<(int, int)>();
                        while (true)
                        {
                            Console.Write("Enter Menu Item ID to Add to Order (0 to Finish): ");
                            int menuItemId = int.Parse(Console.ReadLine());
                            if (menuItemId == 0) break;

                            Console.Write("Enter Quantity: ");
                            int quantity = int.Parse(Console.ReadLine());
                            orderItems.Add((menuItemId, quantity));
                        }

                        int orderId = dal.PlaceOrder(userId, restuarntId, orderItems);
                        Console.WriteLine($"Order Placed! Order ID: {orderId}");
                        break;

                    case "3":
                        Console.Write("Enter Order ID to Mark as Delivered: ");
                        int deliveredOrderId = int.Parse(Console.ReadLine());
                        dal.MarkOrderDelivered(deliveredOrderId);
                        Console.WriteLine("Order Marked as Delivered.");
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
    }

}