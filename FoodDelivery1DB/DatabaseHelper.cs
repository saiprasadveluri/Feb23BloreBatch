using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FoodDeliveryDB
{
    public static class DatabaseHelper
    {
        private static string connectionString = "Server=localhost;Database=FoodDeliveryDB;Trusted_Connection=True;";

        public static User LoginUser(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users WHERE email = @Email AND password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = (int)reader["user_id"],
                                Name = reader["name"].ToString(),
                                Email = reader["email"].ToString(),
                                Phone = reader["phone"].ToString(),
                                Location = reader["location"].ToString(),
                                Password = reader["password"].ToString(),
                                Role = reader["role"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static bool AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO users (name, email, phone, location, password, role) VALUES (@Name, @Email, @Phone, @Location, @Password, 'User')";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@Location", user.Location);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool AddRestaurant(Restaurant restaurant)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO restaurants (name, location) VALUES (@Name, @Location)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", restaurant.Name);
                    command.Parameters.AddWithValue("@Location", restaurant.Location);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool RemoveRestaurant(int restaurantId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM restaurants WHERE restaurant_id = @RestaurantId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool AssignOwnerToRestaurant(int restaurantId, int ownerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE restaurants SET owner_id = @OwnerId WHERE restaurant_id = @RestaurantId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", ownerId);
                    command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool AddMenuItem(MenuItem menuItem)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO menu_items (restaurant_id, name, description, price, category) VALUES (@RestaurantId, @Name, @Description, @Price, @Category)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RestaurantId", menuItem.RestaurantId);
                    command.Parameters.AddWithValue("@Name", menuItem.Name);
                    command.Parameters.AddWithValue("@Description", menuItem.Description);
                    command.Parameters.AddWithValue("@Price", menuItem.Price);
                    command.Parameters.AddWithValue("@Category", menuItem.Category);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<Restaurant> SearchRestaurantsByLocation(string location)
        {
            var restaurants = new List<Restaurant>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM restaurants WHERE location = @Location";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Location", location);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            restaurants.Add(new Restaurant
                            {
                                RestaurantId = (int)reader["restaurant_id"],
                                Name = reader["name"].ToString(),
                                Location = reader["location"].ToString()
                            });
                        }
                    }
                }
            }
            return restaurants;
        }

        public static List<MenuItem> FilterItemsByPreferences(string preference)
        {
            var menuItems = new List<MenuItem>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM menu_items WHERE category = @Preference";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Preference", preference);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            menuItems.Add(new MenuItem
                            {
                                ItemId = (int)reader["item_id"],
                                RestaurantId = (int)reader["restaurant_id"],
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                Price = (decimal)reader["price"],
                                Category = reader["category"].ToString()
                            });
                        }
                    }
                }
            }
            return menuItems;
        }

        public static bool PlaceOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO orders (user_id, restaurant_id, order_status) VALUES (@UserId, @RestaurantId, @OrderStatus)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", order.UserId);
                    command.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);
                    command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<Order> ListOrders()
        {
            var orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM orders";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = (int)reader["order_id"],
                                UserId = (int)reader["user_id"],
                                RestaurantId = (int)reader["restaurant_id"],
                                OrderStatus = reader["order_status"].ToString(),
                                OrderDate = (DateTime)reader["order_date"]
                            });
                        }
                    }
                }
            }
            return orders;
        }

        public static bool UpdateOrderStatusToDelivered(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE orders SET order_status = 'DELIVERED' WHERE order_id = @OrderId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
