
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FoodDelApp
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=MTMDb1;Integrated Security=SSPI";
        SqlConnection con = null;

        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void CloseApp()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public UserDTO Login(string email, string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Users WHERE Email=@Email AND Password=@Password";
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO
                {
                    UserId = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    RoleName = reader.GetString(4)
                };
                reader.Close();
                return user;
            }
            reader.Close();
            return null;
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Restaurants (Name, Location, UserId) VALUES(@Name, @Location, @UserId)";
            cmd.Parameters.AddWithValue("@Name", restaurant.Name);
            cmd.Parameters.AddWithValue("@Location", restaurant.Location);
            cmd.Parameters.AddWithValue("@UserId", restaurant.UserId);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool RemoveRestaurant(long restaurantId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM Restaurants WHERE RId=@RId";
            cmd.Parameters.AddWithValue("@RId", restaurantId);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool AssignOwnerToRestaurant(long restaurantId, long ownerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Restaurants SET UserId=@UserId WHERE RId=@RId";
            cmd.Parameters.AddWithValue("@UserId", ownerId);
            cmd.Parameters.AddWithValue("@RId", restaurantId);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool AddMenuItem(MenuItemDTO menuItem)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO MenuItems (ResId, Mname , Price, Category ) VALUES(@ResId, @Mname, @Price, @Category)";
            cmd.Parameters.AddWithValue("@ResId", menuItem.RestaurantId);
            cmd.Parameters.AddWithValue("@Mname ", menuItem.MenuName);
            cmd.Parameters.AddWithValue("@Price", menuItem.Price);
            cmd.Parameters.AddWithValue("@Category", menuItem.Category);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public List<RestaurantDTO> SearchRestaurantsByLocation(string location)
        {
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Restaurants WHERE Location LIKE @Location";
            cmd.Parameters.AddWithValue("@Location", "%" + location + "%");
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO
                {
                    RId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Location = reader.GetString(2),
                    UserId = reader.GetInt64(3)
                };
                restaurants.Add(restaurant);
            }
            reader.Close();
            return restaurants;
        }

        public List<MenuItemDTO> FilterMenuItems(string preference)
        {
            List<MenuItemDTO> menuItems = new List<MenuItemDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM MenuItems WHERE Name LIKE @Preference OR Description LIKE @Preference";
            cmd.Parameters.AddWithValue("@Preference", "%" + preference + "%");
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MenuItemDTO menuItem = new MenuItemDTO
                {
                    
                    RestaurantId = reader.GetInt32(1),
                    MenuName = reader.GetString(2),
                    Price = reader.GetInt32(3),
                    Category = reader.GetString(4)
                };
                menuItems.Add(menuItem);
            }
            reader.Close();
            return menuItems;
        }

        public bool PlaceOrder(OrderDTO order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Orders (UserId, RestaurantId, OrderDate, Status) VALUES(@UserId, @RestaurantId, @OrderDate, @Status)";
            cmd.Parameters.AddWithValue("@UserId", order.UserId);
            cmd.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            cmd.Parameters.AddWithValue("@Status", order.Status);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                long orderId = (long)cmd.ExecuteScalar();
                foreach (var item in order.OrderItems)
                {
                    SqlCommand itemCmd = new SqlCommand();
                    itemCmd.Connection = con;
                    itemCmd.CommandText = $"INSERT INTO OrderItems (OrderId, MenuItemId, Quantity) VALUES(@OrderId, @MenuItemId, @Quantity)";
                    itemCmd.Parameters.AddWithValue("@OrderId", orderId);
                    itemCmd.Parameters.AddWithValue("@MenuItemId", item.MenuItemId);
                    itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    itemCmd.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public bool UpdateOrderStatus(long orderId, string status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Orders SET Status=@Status WHERE OrderId=@OrderId";
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool AddNewUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Users (Name, Email, Password, Role) VALUES(@Name, @Email, @Password, @Role)";
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.RoleName);
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
            
        }

        public string GetUserRole(long userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT Role FROM Users WHERE UserId=@UserId";
            cmd.Parameters.AddWithValue("@UserId", userId);
            object res = cmd.ExecuteScalar();
            return res?.ToString();
        }

      
    }
}
