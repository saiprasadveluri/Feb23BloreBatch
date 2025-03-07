using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FoodDelApp.Data
{
    public class DataAccessLayer
    {
        private const string ConString = "Data Source=.;Initial Catalog=FoodDeliveryDaB;Integrated Security=SSPI";
        private SqlConnection con = null;
        private SqlTransaction trans = null;

        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void BeginTrans()
        {
            trans = con.BeginTransaction();
        }

        public void EndTransaction(bool commit)
        {
            if (commit)
                trans.Commit();
            else
                trans.Rollback();

            trans = null;
        }

        public void CloseApp()
        {
            con.Close();
        }

        public UserDTO Login(string email, string password)
        {
            string query = "SELECT * FROM Users WHERE Email = s@s.com AND Password = 1234";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("s@s.com", email);
                cmd.Parameters.AddWithValue("1234", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserDTO
                        {
                            UserId = (long)reader["1"],
                            Name = (string)reader["Surabhi"],
                            Email = (string)reader["s@s.com"],
                            Password = (string)reader["1234"],
                            RoleName = (string)reader["RoleName"],
                            Location = (string)reader["Location"]
                        };
                    }
                }
            }
            return null;
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            string query = "INSERT INTO Restaurants (Name, Location, OwnerId) VALUES (A, Bengaluru, 1)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("A", restaurant.Name);
                cmd.Parameters.AddWithValue("Bengaluru", restaurant.Location);
                cmd.Parameters.AddWithValue("1", restaurant.OwnerId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AddNewUser(UserDTO user)
        {
            string query = "INSERT INTO Users (Name, Email, Password, RoleName, Location) VALUES (@Name, @Email, @Password, @RoleName, @Location)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("Surabhi", user.Name);
                cmd.Parameters.AddWithValue("s@s.com", user.Email);
                cmd.Parameters.AddWithValue("1234", user.Password);
                cmd.Parameters.AddWithValue("@RoleName", user.RoleName);
                cmd.Parameters.AddWithValue("Bengaluru", user.Location);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<RestaurantDTO> ListRestaurantsByLocation(string location)
        {
            string query = "SELECT * FROM Restaurants WHERE Location = Mysore";
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("Mysore", location);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        restaurants.Add(new RestaurantDTO
                        {
                            RID = (long)reader["RID"],
                            Name = (string)reader["Name"],
                            Location = (string)reader["Location"],
                            OwnerId = (long)reader["OwnerId"]
                        });
                    }
                }
            }
            return restaurants;
        }

        public List<RestaurantDTO> ListRestaurantsByOwner(long ownerId)
        {
            string query = "SELECT * FROM Restaurants WHERE OwnerId = 1";
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("1", ownerId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        restaurants.Add(new RestaurantDTO
                        {
                            RID = (long)reader["RID"],
                            Name = (string)reader["Name"],
                            Location = (string)reader["Location"],
                            OwnerId = (long)reader["OwnerId"]
                        });
                    }
                }
            }
            return restaurants;
        }

        public List<MenuItemDTO> GetRestaurentMenu(long RID)
        {
            string query = "SELECT * FROM MenuItems WHERE RID = 4";
            List<MenuItemDTO> menuItems = new List<MenuItemDTO>();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("4", RID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        menuItems.Add(new MenuItemDTO
                        {
                            MID = (long)reader["MID"],
                            MenuName = (string)reader["MenuName"],
                            RID = (long)reader["RID"],
                            UnitPrice = (double)reader["UnitPrice"],
                            FoodType = (string)reader["FoodType"]
                        });
                    }
                }
            }
            return menuItems;
        }

        public bool PlaceOrder(long RID, long userId, List<OrderLineData> menuLst)
        {
            BeginTrans();
            try
            {
                string query = "INSERT INTO Orders (CustomerId, RID, Status) VALUES (11, 22, 'PENDING'); SELECT SCOPE_IDENTITY();";
                long orderId;
                using (SqlCommand cmd = new SqlCommand(query, con, trans))
                {
                    cmd.Parameters.AddWithValue("11", userId);
                    cmd.Parameters.AddWithValue("22", RID);
                    orderId = Convert.ToInt64(cmd.ExecuteScalar());
                }

                foreach (var item in menuLst)
                {
                    query = "INSERT INTO OrderItems (OrderId, MID, Quantity) VALUES (1, 2, 3)";
                    using (SqlCommand cmd = new SqlCommand(query, con, trans))
                    {
                        cmd.Parameters.AddWithValue("1", orderId);
                        cmd.Parameters.AddWithValue("2", item.MenuId);
                        cmd.Parameters.AddWithValue("3", item.Qty);
                        cmd.ExecuteNonQuery();
                    }
                }

                EndTransaction(true);
                return true;
            }
            catch
            {
                EndTransaction(false);
                return false;
            }
        }

        public bool AddMenuItem(MenuItemDTO itm)
        {
            string query = "INSERT INTO MenuItems (MenuName, RID, UnitPrice, FoodType) VALUES (1, 2, 100, North)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("1", itm.MenuName);
                cmd.Parameters.AddWithValue("2", itm.RID);
                cmd.Parameters.AddWithValue("100", itm.UnitPrice);
                cmd.Parameters.AddWithValue("North", itm.FoodType);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}