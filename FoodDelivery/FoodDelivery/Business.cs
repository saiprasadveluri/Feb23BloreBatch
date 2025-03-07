using System;
using System.Data.SqlClient;

namespace FoodDelivery.Business
{
    public class RestaurantManager
    {
        // Add a new restaurant
        public static void AddRestaurant(string name, string address, int ownerId)
        {
            string query = $"INSERT INTO Restaurant (Name, Address, OwnerId) VALUES ('{name}', '{address}', {ownerId})";
            DataAccess.DataAccess.ExecuteNonQuery(query);
            Console.WriteLine("Restaurant added successfully.");
        }

        // Add a menu item for a specific restaurant
        public static void AddMenuItem(int restaurantId, string name, string description, decimal price, string cuisine, string dishType)
        {
            string query = $"INSERT INTO MenuItem (RestaurantId, Name, Description, Price, Cuisine, DishType) VALUES ({restaurantId}, '{name}', '{description}', {price}, '{cuisine}', '{dishType}')";
            DataAccess.DataAccess.ExecuteNonQuery(query);
            Console.WriteLine("Menu item added successfully.");
        }

        // List all restaurants
        public static void ListRestaurants()
        {
            string query = "SELECT * FROM Restaurant";
            SqlDataReader reader = DataAccess.DataAccess.ExecuteReader(query);
            while (reader.Read())
            {
                Console.WriteLine($"Restaurant ID: {reader.GetInt32(0)} - Name: {reader.GetString(1)} - Address: {reader.GetString(2)}");
            }
            reader.Close();
        }
    }

    public class OrderManager
    {
        // Place an order
        public static void PlaceOrder(int userId, int restaurantId, string menuItemIds, int quantity)
        {
            string query = $"INSERT INTO [Order] (UserId, RestaurantId, Status) VALUES ({userId}, {restaurantId}, 'Pending')";
            int orderId = (int)DataAccess.DataAccess.ExecuteScalar(query);  // Assuming ExecuteScalar returns the last inserted ID.

            foreach (var menuItemId in menuItemIds.Split(','))
            {
                string orderItemQuery = $"INSERT INTO OrderItem (OrderId, MenuItemId, Quantity) VALUES ({orderId}, {menuItemId}, {quantity})";
                DataAccess.DataAccess.ExecuteNonQuery(orderItemQuery);
            }

            Console.WriteLine($"Order placed successfully with Order ID: {orderId}");
        }

        // Update order status to 'Delivered'
        public static void MarkOrderAsDelivered(int orderId)
        {
            string query = $"UPDATE [Order] SET Status = 'Delivered' WHERE OrderId = {orderId}";
            DataAccess.DataAccess.ExecuteNonQuery(query);
            Console.WriteLine("Order marked as delivered.");
        }
    }
}
