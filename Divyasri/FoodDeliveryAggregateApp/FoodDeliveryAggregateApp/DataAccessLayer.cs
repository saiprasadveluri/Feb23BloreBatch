


using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FoodDeliveryAggregateApp
{
    class DataAccessLayer
    {
        const string ConString = "Data Source=.; Initial Catalog=fooddeliveryapp;Integrated Security=SSPI";
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

        public UserDTO Login(string Email, string password)
        {
            UserDTO user = null;
            string query = "select * from Userinfo where Email=@Email and Password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                user = new UserDTO
                {
                    UserId = dr.GetInt64(dr.GetOrdinal("UserId")),
                    Username = dr.GetString(dr.GetOrdinal("Username")),
                    Password = dr.GetString(dr.GetOrdinal("Password")),
                    Email = dr.GetString(dr.GetOrdinal("Email")),
                    Rolename = dr.GetString(dr.GetOrdinal("Rolename")),
                    Location = dr.GetString(dr.GetOrdinal("Location"))
                };
            }
            dr.Close();
            return user;
        }

        public List<RestaurantDTO> GetRestaurants()
        {
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
            string query = "select * from Restaurant";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO
                {
                    RID = dr.GetInt64(dr.GetOrdinal("RID")),
                    Name = dr.GetString(dr.GetOrdinal("Name")),
                    OwnerId = dr.GetInt64(dr.GetOrdinal("OwnerId")),
                    Location = dr.GetString(dr.GetOrdinal("Location"))
                };
                restaurants.Add(restaurant);
            }
            dr.Close();
            return restaurants;
        }

        public List<RestaurantDTO> GetRestaurantsByLocation(string location)
        {
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();
            string query = "select * from Restaurant where Location=@Location";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Location", location);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO
                {
                    RID = dr.GetInt64(dr.GetOrdinal("RID")),
                    Name = dr.GetString(dr.GetOrdinal("Name")),
                    OwnerId = dr.GetInt64(dr.GetOrdinal("OwnerId")),
                    Location = dr.GetString(dr.GetOrdinal("Location"))
                };
                restaurants.Add(restaurant);
            }
            dr.Close();
            return restaurants;
        }

        public List<MenuItemDTO> GetMenu(long RID)
        {
            List<MenuItemDTO> menus = new List<MenuItemDTO>();
            string query = "select * from MenuItem where RID=@RID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@RID", RID);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MenuItemDTO menu = new MenuItemDTO
                {
                    MenuId = dr.GetInt64(dr.GetOrdinal("MenuId")),
                    MenuName = dr.GetString(dr.GetOrdinal("MenuName")),
                    RID = dr.GetInt64(dr.GetOrdinal("RID")),
                    UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice")),
                    FoodType = dr.GetString(dr.GetOrdinal("FoodType"))
                };
                menus.Add(menu);
            }
            dr.Close();
            return menus;
        }

        public List<MenuItemDTO> GetMenuByFoodType(string foodType)
        {
            List<MenuItemDTO> menus = new List<MenuItemDTO>();
            string query = "select * from MenuItem where FoodType=@FoodType";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FoodType", foodType);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MenuItemDTO menu = new MenuItemDTO
                {
                    MenuId = dr.GetInt64(dr.GetOrdinal("MenuId")),
                    MenuName = dr.GetString(dr.GetOrdinal("MenuName")),
                    RID = dr.GetInt64(dr.GetOrdinal("RID")),
                    UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice")),
                    FoodType = dr.GetString(dr.GetOrdinal("FoodType"))
                };
                menus.Add(menu);
            }
            dr.Close();
            return menus;
        }

        public bool AddUser(UserDTO user)
        {
            string query = "insert into Userinfo (Username, Password, Email, Rolename, Location) values (@Username, @Password, @Email, @Rolename, @Location)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Rolename", user.Rolename);
            cmd.Parameters.AddWithValue("@Location", user.Location);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool AddRestaurant(RestaurantDTO restaurant)
        {
            string query = "insert into Restaurant (Name, OwnerId, Location) values (@Name, @OwnerId, @Location)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", restaurant.Name);
            cmd.Parameters.AddWithValue("@OwnerId", restaurant.OwnerId);
            cmd.Parameters.AddWithValue("@Location", restaurant.Location);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool AssignOwner(long restaurantId, long ownerId)
        {
            string query = "update Restaurant set OwnerId=@OwnerId where RID=@RID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OwnerId", ownerId);
            cmd.Parameters.AddWithValue("@RID", restaurantId);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool AddMenu(MenuItemDTO menu)
        {
            string query = "insert into MenuItem (MenuName, RID, UnitPrice, FoodType) values (@MenuName, @RID, @UnitPrice, @FoodType)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MenuName", menu.MenuName);
            cmd.Parameters.AddWithValue("@RID", menu.RID);
            cmd.Parameters.AddWithValue("@UnitPrice", menu.UnitPrice);
            cmd.Parameters.AddWithValue("@FoodType", menu.FoodType);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool PlaceOrder(OrderDTO order)
        {
            string query = "insert into Orders (RID, OrderBy, Status, OrderDate) values (@RID, @OrderBy, @Status, @OrderDate)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@RID", order.RID);
            cmd.Parameters.AddWithValue("@OrderBy", order.OrderBy);
            cmd.Parameters.AddWithValue("@Status", order.Status);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool UpdateOrderStatus(long orderId, string status)
        {
            string query = "update Orders set Status=@Status where OrderId=@OrderId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public List<OrderDTO> GetOrders()
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            string query = "select * from Orders";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                OrderDTO order = new OrderDTO
                {
                    OrderId = dr.GetInt64(dr.GetOrdinal("OrderId")),
                    RID = dr.GetInt64(dr.GetOrdinal("RID")),
                    OrderBy = dr.GetInt64(dr.GetOrdinal("OrderBy")),
                    Status = dr.GetString(dr.GetOrdinal("Status")),
                    OrderDate = dr.GetDateTime(dr.GetOrdinal("OrderDate"))
                };
                orders.Add(order);
            }
            dr.Close();
            return orders;
        }

        public OrderDTO GetOrderById(long orderId)
        {
            OrderDTO order = null;
            string query = "select * from Orders where OrderId=@OrderId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                order = new OrderDTO
                {
                    OrderId = dr.GetInt64(dr.GetOrdinal("OrderId")),
                    RID = dr.GetInt64(dr.GetOrdinal("RID")),
                    OrderBy = dr.GetInt64(dr.GetOrdinal("OrderBy")),
                    Status = dr.GetString(dr.GetOrdinal("Status")),
                    OrderDate = dr.GetDateTime(dr.GetOrdinal("OrderDate"))
                };
            }
            dr.Close();
            return order;
        }
    }
}


