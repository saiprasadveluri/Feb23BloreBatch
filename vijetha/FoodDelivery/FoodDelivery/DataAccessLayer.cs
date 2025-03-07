using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FoodDelivery
{
    
    public class DataAccessLayer
    {
        private string connectionString = "Data Source=.;Initial Catalog=FOODDELIVERY;Trusted_Connection=True;"; 

        // Admin Functions
        public void AddRestuarnt(RestuarntDTO restuarnt)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Restuarnts (Name, Location, OwnerId) VALUES (@Name, @Location, @OwnerId)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", restuarnt.Name);
                    cmd.Parameters.AddWithValue("@Location", restuarnt.Location);
                    cmd.Parameters.AddWithValue("@OwnerId", (object)restuarnt.OwnerId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveRestuarnt(int restuarntId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Restuarnts WHERE RestuarntId = @RestuarntId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestuarntId", restuarntId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AssignOwner(int restuarntId, int ownerId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Restuarnts SET OwnerId = @OwnerId WHERE RestuarntId = @RestuarntId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestuarntId", restuarntId);
                    cmd.Parameters.AddWithValue("@OwnerId", ownerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Owner Functions
        public void AddMenuItem(MenuItemDTO menuItem)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO MenuItems (RestuarntId, Name, Price) VALUES (@RestuarntId, @Name, @Price)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestuarntId", menuItem.RestuarntId);
                    cmd.Parameters.AddWithValue("@Name", menuItem.Name);
                    cmd.Parameters.AddWithValue("@Price", menuItem.Price);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // User Functions
        public List<RestuarntDTO> SearchRestuarnts(string location)
        {
            var restuarnts = new List<RestuarntDTO>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Restuarnts WHERE Location LIKE @Location";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Location", "%" + location + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            restuarnts.Add(new RestuarntDTO
                            {
                                RestuarntId = (int)reader["RestuarntId"],
                                Name = reader["Name"].ToString(),
                                Location = reader["Location"].ToString(),
                                OwnerId = reader["OwnerId"] as int?
                            });
                        }
                    }
                }
            }
            return restuarnts;
        }

        public List<MenuItemDTO> FilterMenuItems(int restuarntId, string dishName)
        {
            var items = new List<MenuItemDTO>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM MenuItems WHERE RestuarntId = @RestuarntId AND Name LIKE @Name";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestuarntId", restuarntId);
                    cmd.Parameters.AddWithValue("@Name", "%" + dishName + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new MenuItemDTO
                            {
                                MenuItemId = (int)reader["MenuItemId"],
                                RestuarntId = (int)reader["RestuarntId"],
                                Name = reader["Name"].ToString(),
                                Price = (decimal)reader["Price"]
                            });
                        }
                    }
                }
            }
            return items;
        }

        public int PlaceOrder(int userId, int restuarntId, List<(int menuItemId, int quantity)> items)
        {
            int orderId;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var orderQuery = "INSERT INTO [Orders] (UserId, RestuarntId, Status) OUTPUT INSERTED.OrderId VALUES (@UserId, @RestuarntId, 'PLACED')";
                        using (var cmd = new SqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@RestuarntId", restuarntId);
                            orderId = (int)cmd.ExecuteScalar();
                        }

                        var orderItemQuery = "INSERT INTO OrderItems (OrderId, MenuItemId, Quantity) VALUES (@OrderId, @MenuItemId, @Quantity)";
                        foreach (var item in items)
                        {
                            using (var cmd = new SqlCommand(orderItemQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@OrderId", orderId);
                                cmd.Parameters.AddWithValue("@MenuItemId", item.menuItemId);
                                cmd.Parameters.AddWithValue("@Quantity", item.quantity);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return orderId;
        }

        public void MarkOrderDelivered(int orderId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE [Orders] SET Status = 'DELIVERED' WHERE OrderId = @OrderId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }





}
