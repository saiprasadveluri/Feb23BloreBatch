using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Restaurant_Food_App
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=RES_FOOD;Integrated Security=SSPI";
        SqlConnection con = null;

        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
        }

        public void CloseApp()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public UserDTO Login(string Email, string Password)
        {
            UserDTO user = null;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Email = @Email AND Password = @Password", con);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserDTO
                    {
                        User_id = (long)reader["User_id"],
                        Name = (string)reader["Name"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["Password"],
                        RoleName = (string)reader["RoleName"]
                    };
                }
                reader.Close();
            }
            finally
            {
                con.Close();
            }
            return user;
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Restaurants (res_name, city, phone, OwnerId) VALUES (@res_name, @city, @phone, @OwnerId)", con);
                cmd.Parameters.AddWithValue("@res_name", restaurant.res_name);
                cmd.Parameters.AddWithValue("@city", restaurant.city);
                cmd.Parameters.AddWithValue("@phone", restaurant.phone);
                cmd.Parameters.AddWithValue("@OwnerId", restaurant.OwnerId);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            finally
            {
                con.Close();
            }
        }

        public bool RemoveRestaurant(long restaurant_id)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Restaurants WHERE restaurant_id = @restaurant_id", con);
                cmd.Parameters.AddWithValue("@restaurant_id", restaurant_id);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AssignOwnerToRestaurant(long restaurant_id, long ownerId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Restaurants SET OwnerId = @OwnerId WHERE restaurant_id = @restaurant_id", con);
                cmd.Parameters.AddWithValue("@restaurant_id", restaurant_id);
                cmd.Parameters.AddWithValue("@OwnerId", ownerId);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AddMen(MenuDTO menu)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Menu (restaurant_id, menu_item, UnitPrice, Foodtype) VALUES (@restaurant_id, @menu_item, @UnitPrice, @Foodtype)", con);
                cmd.Parameters.AddWithValue("@restaurant_id", menu.restaurant_id);
                cmd.Parameters.AddWithValue("@menu_item", menu.menu_item);
                cmd.Parameters.AddWithValue("@UnitPrice", menu.UnitPrice);
                cmd.Parameters.AddWithValue("@Foodtype", menu.Foodtype);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AddNewUser(UserDTO user)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (Name, Email, Password, RoleName) VALUES (@Name, @Email, @Password, @RoleName)", con);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@RoleName", user.RoleName);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            finally
            {
                con.Close();
            }
        }

        public List<RestaurantDTO> SearchRestaurantsBycity(string city)
        {
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Restaurants WHERE city = @city", con);
                cmd.Parameters.AddWithValue("@city", city);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RestaurantDTO restaurant = new RestaurantDTO
                    {
                        restaurant_id = (long)reader["restaurant_id"],
                        res_name = (string)reader["res_name"],
                        city = (string)reader["city"],
                        phone = (long)reader["phone"],
                        OwnerId = (long)reader["OwnerId"]
                    };
                    restaurants.Add(restaurant);
                }
                reader.Close();
            }
            finally
            {
                con.Close();
            }
            return restaurants;
        }

        public bool PlaceOrder(OrderDTO order)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Orders (user_id, restaurant_id, status) VALUES (@user_id, @restaurant_id, @status); SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@user_id", order.user_id);
                cmd.Parameters.AddWithValue("@restaurant_id", order.restaurant_id);
                cmd.Parameters.AddWithValue("@status", order.status);
                long orderId = Convert.ToInt64(cmd.ExecuteScalar());

                foreach (var item in order.order_items)
                {
                    SqlCommand itemCmd = new SqlCommand("INSERT INTO OrderItems (order_id, menu_id, quantity) VALUES (@order_id, @menu_id, @quantity)", con);
                    itemCmd.Parameters.AddWithValue("@order_id", orderId);
                    itemCmd.Parameters.AddWithValue("@menu_id", item.menu_id);
                    itemCmd.Parameters.AddWithValue("@quantity", item.quantity);
                    itemCmd.ExecuteNonQuery();
                }
                return true;
            }
            finally
            {
                con.Close();
            }
        }

        public bool UpdateOrderstatus(long order_id, string status)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Orders SET status = @status WHERE order_id = @order_id", con);
                cmd.Parameters.AddWithValue("@order_id", order_id);
                cmd.Parameters.AddWithValue("@status", status);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            finally
            {
                con.Close();
            }
        }

        public string GetUserRole(long User_id)
        {
            string role = null;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT RoleName FROM Users WHERE User_id = @User_id", con);
                cmd.Parameters.AddWithValue("@User_id", User_id);
                role = (string)cmd.ExecuteScalar();
            }
            finally
            {
                con.Close();
            }
            return role;
        }
    }
}
