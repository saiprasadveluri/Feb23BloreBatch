using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FoodDeliveryApp
{
    public class DataAccessLayer
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=fooddelivery;Integrated Security=SSPI";

        public List<User> GetUsers()
        {
            var users = new List<User>();
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM users", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            userid = (int)reader["userid"],
                            username = (string)reader["username"],
                            user_role = (string)reader["user_role"],
                            email = (string)reader["email"],
                            password = (string)reader["password"]
                        });
                    }
                }
            }
            return users;
        }

        public User Login(string email, string password)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM users WHERE email = @Email AND password = @Password", con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            userid = (int)reader["userid"],
                            username = (string)reader["username"],
                            user_role = (string)reader["user_role"],
                            email = (string)reader["email"],
                            password = (string)reader["password"]
                        };
                    }
                }
            }
            return null;
        }

        public bool UserExists(string email)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE email = @Email", con);
                cmd.Parameters.AddWithValue("@Email", email);
                var count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>();
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM restaurants", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        restaurants.Add(new Restaurant
                        {
                            resto_id = (int)reader["resto_id"],
                            resto_name = (string)reader["resto_name"],
                            resto_location = (string)reader["resto_location"]
                        });
                    }
                }
            }
            return restaurants;
        }

        public List<Menu> GetMenus()
        {
            var menus = new List<Menu>();
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM menu", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        menus.Add(new Menu
                        {
                            item_id = (int)reader["item_id"],
                            item_name = (string)reader["item_name"],
                            price = (int)reader["price"],
                            resto_id = (int)reader["resto_id"]
                        });
                    }
                }
            }
            return menus;
        }

        public List<Order> GetOrders()
        {
            var orders = new List<Order>();
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM orders", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            order_no = (int)reader["order_no"],
                            userid = (int)reader["userid"],
                            resto_id = (int)reader["resto_id"],
                            status = (string)reader["status"]
                        });
                    }
                }
            }
            return orders;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("INSERT INTO restaurants (resto_name, resto_location) VALUES (@name, @location)", con);
                cmd.Parameters.AddWithValue("@name", restaurant.resto_name);
                cmd.Parameters.AddWithValue("@location", restaurant.resto_location);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveRestaurant(int restoId)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("DELETE FROM restaurants WHERE resto_id = @restoId", con);
                cmd.Parameters.AddWithValue("@restoId", restoId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AssignOwner(int restoId, int ownerId)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("UPDATE restaurants SET owner_id = @ownerId WHERE resto_id = @restoId", con);
                cmd.Parameters.AddWithValue("@ownerId", ownerId);
                cmd.Parameters.AddWithValue("@restoId", restoId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddMenuItem(Menu menu)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if the restaurant exists
                var checkCmd = new SqlCommand("SELECT COUNT(*) FROM restaurants WHERE resto_id = @restoId", con);
                checkCmd.Parameters.AddWithValue("@restoId", menu.resto_id);
                var count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new Exception("Restaurant ID does not exist.");
                }

                var cmd = new SqlCommand("INSERT INTO menu (item_name, price, resto_id) VALUES (@itemName, @price, @restoId)", con);
                cmd.Parameters.AddWithValue("@itemName", menu.item_name);
                cmd.Parameters.AddWithValue("@price", menu.price);
                cmd.Parameters.AddWithValue("@restoId", menu.resto_id);
                cmd.ExecuteNonQuery();
            }
        }

        public void PlaceOrder(Order order)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if the user exists
                var checkCmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE userid = @userId", con);
                checkCmd.Parameters.AddWithValue("@userId", order.userid);
                var count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new Exception("User ID does not exist.");
                }

                var cmd = new SqlCommand("INSERT INTO orders (userid, resto_id, status) VALUES (@userId, @restoId, @status)", con);
                cmd.Parameters.AddWithValue("@userId", order.userid);
                cmd.Parameters.AddWithValue("@restoId", order.resto_id);
                cmd.Parameters.AddWithValue("@status", order.status);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateOrderStatus(int orderNo, string status)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("UPDATE orders SET status = @status WHERE order_no = @orderNo", con);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@orderNo", orderNo);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddUser(User user)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("INSERT INTO users (username, user_role, email, password) VALUES (@username, @user_role, @Email, @Password)", con);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@user_role", user.user_role);
                cmd.Parameters.AddWithValue("@Email", user.email);
                cmd.Parameters.AddWithValue("@Password", user.password);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

