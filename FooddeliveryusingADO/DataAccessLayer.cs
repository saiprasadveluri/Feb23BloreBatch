using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooddeliveryusingADO
{
    public class DataAccessLayer
    {
            private SqlConnection connection;

            public DataAccessLayer()
            {
                connection = new SqlConnection("Data Source=.;Initial Catalog=FoodDelivery;Integrated Security=SSPI");
            }

            public void OpenConnection()
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Console.WriteLine("Database connection opened.");
                }
            }

            public void CloseConnection()
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }

            public Admin LoginAdmin(string email, string password)
            {
                Admin admin = null;
                string query = "SELECT * FROM Admin WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        admin = new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                    }
                }
                return admin;
            }

            public void AddRestaurant(Restaurant restaurant)
            {
                string query = "INSERT INTO Restaurent (Name, Location, AdminID) VALUES (@Name, @Location, @AdminID)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                cmd.Parameters.AddWithValue("@Location", restaurant.Location);
                cmd.Parameters.AddWithValue("@AdminID", restaurant.AdminID);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Added restaurant: {restaurant.Name}");
            }

            public void AddMenuItem(Menu menu)
            {
                string query = "INSERT INTO Menu (RestaurantID, ItemName, Description, Price, FoodPreference) VALUES (@RestaurantID, @ItemName, @Description, @Price, @FoodPreference)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RestaurantID", menu.RestaurantID);
                cmd.Parameters.AddWithValue("@ItemName", menu.ItemName);
                cmd.Parameters.AddWithValue("@Description", menu.Description);
                cmd.Parameters.AddWithValue("@Price", menu.Price);
                cmd.Parameters.AddWithValue("@FoodPreference", menu.FoodPreference);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Added menu item: {menu.ItemName} to restaurant ID: {menu.RestaurantID}");
            }

            public List<Restaurant> SearchRestaurantsByLocation(string location)
            {
                List<Restaurant> restaurants = new List<Restaurant>();
                string query = "SELECT * FROM Restaurent WHERE Location = @Location";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Location", location);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        restaurants.Add(new Restaurant
                        {
                            RestaurantID = (int)reader["RestaurentID"],
                            Name = reader["Name"].ToString(),
                            Location = reader["Location"].ToString(),
                            AdminID = (int)reader["AdminID"]
                        });
                    }
                }
                return restaurants;
            }

            public List<Menu> FilterItemsByPreference(string preference)
            {
                List<Menu> menus = new List<Menu>();
                string query = "SELECT * FROM Menu WHERE FoodPreference = @FoodPreference";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FoodPreference", preference);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        menus.Add(new Menu
                        {
                            MenuID = (int)reader["MenuID"],
                            RestaurantID = (int)reader["RestaurantID"],
                            ItemName = reader["ItemName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (int)reader["Price"],
                            FoodPreference = reader["FoodPreference"].ToString()
                        });
                    }
                }
                return menus;
            }

        


         public void PlaceOrder(Order order, List<OrderItem> orderItems)
         {
             string orderQuery = "INSERT INTO Orders (UserID, RestaurentID, OrderDate, Status) VALUES (@UserID, @RestaurentID, @OrderDate, @Status); SELECT SCOPE_IDENTITY();";
             SqlCommand orderCmd = new SqlCommand(orderQuery, connection);
             orderCmd.Parameters.AddWithValue("@UserID", order.UserID);
             orderCmd.Parameters.AddWithValue("@RestaurentID", order.RestaurentID);
             orderCmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
             orderCmd.Parameters.AddWithValue("@Status", order.Status);

             int orderId = Convert.ToInt32(orderCmd.ExecuteScalar());

             foreach (var orderItem in orderItems)
             {
                 string orderItemQuery = "INSERT INTO OrderItem (OrderID, MenuID, Quantity) VALUES (@OrderID, @MenuID, @Quantity)";
                 SqlCommand orderItemCmd = new SqlCommand(orderItemQuery, connection);
                 orderItemCmd.Parameters.AddWithValue("@OrderID", orderId);
                 orderItemCmd.Parameters.AddWithValue("@MenuID", orderItem.MenuID);
                 orderItemCmd.Parameters.AddWithValue("@Quantity", orderItem.Quantity);

                 orderItemCmd.ExecuteNonQuery();
             }

             Console.WriteLine($"Order placed with ID: {orderId} for User ID: {order.UserID}");
         }

        public void UpdateOrderStatus(int orderId, string status)
            {
                string query = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Order ID: {orderId} status updated to: {status}");
            }
        }
    }





