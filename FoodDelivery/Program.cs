using System;
using FoodDelivery.Business;
using FoodDelivery.DataAccess;

namespace FoodDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            // Open database connection
            DataAccess.OpenConnection();

            // Add restaurant (for admin)
            Console.WriteLine("Adding new restaurant...");
            RestaurantManager.AddRestaurant("Pasta Palace", "123 Italian St", 1);

            // List restaurants (for user)
            Console.WriteLine("\nListing all restaurants...");
            RestaurantManager.ListRestaurants();

            // Add a menu item for a specific restaurant (for restaurant owner)
            Console.WriteLine("\nAdding menu item for Pasta Palace...");
            RestaurantManager.AddMenuItem(1, "Spaghetti Bolognese", "Traditional Italian pasta", 12.99m, "Italian", "Main Course");

            // Place an order (for user)
            Console.WriteLine("\nPlacing order...");
            OrderManager.PlaceOrder(1, 1, "1", 2);  // User with ID 1 places an order for menu item with ID 1 (quantity = 2)

            // Update order status to 'Delivered' (for delivery person)
            Console.WriteLine("\nMarking order as delivered...");
            OrderManager.MarkOrderAsDelivered(1);  // Mark order with ID 1 as delivered

            // Close database connection
            DataAccess.CloseConnection();
        }
    }
}
