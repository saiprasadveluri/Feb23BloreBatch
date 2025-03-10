﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Food_delivery
{
    public class DataAccessLayer
    {
        private string connectionString = "Data Source=.; Initial Catalog=FoodDeliveryDB;Integrated Security=SSPI";
        private SqlConnection con;

        public DataAccessLayer()
        {
            con = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        // Admin functionalities
        public bool AddRestaurant(RestaurantDTO restaurant)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("INSERT INTO Restaurants (name, owner_id, location) VALUES (@Name, @OwnerId, @Location)", con);
            cmd.Parameters.AddWithValue("@Name", restaurant.Name);
            cmd.Parameters.AddWithValue("@OwnerId", restaurant.OwnerId);
            cmd.Parameters.AddWithValue("@Location", restaurant.Location);
            int rows = cmd.ExecuteNonQuery();
            CloseConnection();
            return rows > 0;
        }

        public bool RemoveRestaurant(int restaurantId)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM Restaurants WHERE restaurant_id = @RestaurantId", con);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            int rows = cmd.ExecuteNonQuery();
            CloseConnection();
            return rows > 0;
        }

        public bool AssignOwner(int restaurantId, int ownerId)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Restaurants SET owner_id = @OwnerId WHERE restaurant_id = @RestaurantId", con);
            cmd.Parameters.AddWithValue("@OwnerId", ownerId);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            int rows = cmd.ExecuteNonQuery();
            CloseConnection();
            return rows > 0;
        }

        // Owner functionalities
        public bool AddMenuItem(MenuDTO menuItem)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("INSERT INTO Menu (restaurant_id, name, price, category, description) VALUES (@RestaurantId, @Name, @Price, @Category, @Description)", con);
            cmd.Parameters.AddWithValue("@RestaurantId", menuItem.RestaurantId);
            cmd.Parameters.AddWithValue("@Name", menuItem.Name);
            cmd.Parameters.AddWithValue("@Price", menuItem.Price);
            cmd.Parameters.AddWithValue("@Category", menuItem.Category);
            cmd.Parameters.AddWithValue("@Description", menuItem.Description);
            int rows = cmd.ExecuteNonQuery();
            CloseConnection();
            return rows > 0;
        }

        // User functionalities
        public List<RestaurantDTO> GetRestaurantsByLocation(string location)
        {
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
            OpenConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Restaurants WHERE location = @Location", con);
            cmd.Parameters.AddWithValue("@Location", location);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                restaurants.Add(new RestaurantDTO
                {
                    RestaurantId = (int)reader["restaurant_id"],
                    Name = reader["name"].ToString(),
                    OwnerId = (int)reader["owner_id"],
                    Location = reader["location"].ToString()
                });
            }
            reader.Close();
            CloseConnection();
            return restaurants;
        }

        public List<MenuDTO> GetMenuByPreferences(int restaurantId, string category)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Menu WHERE restaurant_id = @RestaurantId AND category = @Category", con);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            cmd.Parameters.AddWithValue("@Category", category);
            SqlDataReader reader = cmd.ExecuteReader();
            List<MenuDTO> menuItems = new List<MenuDTO>();
            while (reader.Read())
            {
                menuItems.Add(new MenuDTO
                {
                    MenuId = Convert.ToInt32(reader["menu_id"]),
                    RestaurantId = Convert.ToInt32(reader["restaurant_id"]),
                    Name = reader["name"].ToString(),
                    Price = Convert.ToDecimal(reader["price"]),
                    Category = reader["category"].ToString(),
                    Description = reader["description"].ToString()
                });
            }
            reader.Close();
            CloseConnection();
            return menuItems;
        }

        public bool PlaceOrder(OrderDTO order, List<OrderItemDTO> orderItems)
        {
            OpenConnection();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Orders (user_id, restaurant_id, total_price, status) OUTPUT INSERTED.order_id VALUES (@UserId, @RestaurantId, @TotalPrice, @Status)", con, transaction);
                cmd.Parameters.AddWithValue("@UserId", order.UserId);
                cmd.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);
                cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                int orderId = (int)cmd.ExecuteScalar();

                foreach (var item in orderItems)
                {
                    SqlCommand itemCmd = new SqlCommand("INSERT INTO Order_Items (order_id, menu_id, quantity, price) VALUES (@OrderId, @MenuId, @Quantity, @Price)", con, transaction);
                    itemCmd.Parameters.AddWithValue("@OrderId", orderId);
                    itemCmd.Parameters.AddWithValue("@MenuId", item.MenuId);
                    itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    itemCmd.Parameters.AddWithValue("@Price", item.Price);
                    itemCmd.ExecuteNonQuery();
                }

                transaction.Commit();
                CloseConnection();
                return true;
            }
            catch
            {
                transaction.Rollback();
                CloseConnection();
                return false;
            }
        }

        public bool UpdateOrderStatus(int orderId, string status)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Orders SET status = @Status WHERE order_id = @OrderId", con);
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            cmd.Parameters.AddWithValue("@Status", status);
            int rows = cmd.ExecuteNonQuery();
            CloseConnection();
            return rows > 0;
        }

        // User registration and authentication
        public bool RegisterUser(UserDTO user)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("INSERT INTO Users (name, email, phone, role, password, location) VALUES (@Name, @Email, @Phone, @Role, @Password, @Location)", con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Location", user.Location);
            int rows = cmd.ExecuteNonQuery();
            CloseConnection();
            return rows > 0;
        }

        public UserDTO AuthenticateUser(string email, string password)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE email = @Email AND password = @Password", con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                UserDTO user = new UserDTO
                {
                    UserId = (int)reader["user_id"],
                    Name = reader["name"].ToString(),
                    Email = reader["email"].ToString(),
                    Phone = reader["phone"].ToString(),
                    Role = reader["role"].ToString(),
                    Location = reader["location"].ToString()
                };
                reader.Close();
                CloseConnection();
                return user;
            }
            reader.Close();
            CloseConnection();
            return null;
        }

        public List<MenuDTO> GetMenuByRestaurant(int restaurantId)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Menu WHERE restaurant_id = @RestaurantId", con);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            SqlDataReader reader = cmd.ExecuteReader();
            List<MenuDTO> menuItems = new List<MenuDTO>();
            while (reader.Read())
            {
                menuItems.Add(new MenuDTO
                {
                    MenuId = Convert.ToInt32(reader["menu_id"]),
                    RestaurantId = Convert.ToInt32(reader["restaurant_id"]),
                    Name = reader["name"].ToString(),
                    Price = Convert.ToDecimal(reader["price"]),
                    Category = reader["category"].ToString(),
                    Description = reader["description"].ToString()
                });
            }
            reader.Close();
            CloseConnection();
            return menuItems;
        }
    }
}