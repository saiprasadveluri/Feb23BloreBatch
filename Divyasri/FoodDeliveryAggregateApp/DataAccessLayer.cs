


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
            string query = "select * from Users where Email=@Email and Password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                user = new UserDTO
                {
                    UserId = dr.GetInt64(0),
                    Username = dr.GetString(1),
                    Password = dr.GetString(2),
                    Email = dr.GetString(3),
                    Rolename = dr.GetString(4),
                    Location = dr.GetString(5)
                };
            }
            dr.Close();
            return user;
        }

        public List<RestuarantDTO> GetRestuarants()
        {
            List<RestuarantDTO> restuarants = new List<RestuarantDTO>();
            string query = "select * from Restuarants";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RestuarantDTO restuarant = new RestuarantDTO
                {
                    RID = dr.GetInt64(0),
                    Name = dr.GetString(1),
                    OwnerId = dr.GetInt64(2),
                    Location = dr.GetString(3)
                };
                restuarants.Add(restuarant);
            }
            dr.Close();
            return restuarants;
        }

        public List<RestuarantDTO> GetRestuarantsByLocation(string location)
        {
            List<RestuarantDTO> restuarants = new List<RestuarantDTO>();
            string query = "select * from Restuarants where Location=@Location";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Location", location);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RestuarantDTO restuarant = new RestuarantDTO
                {
                    RID = dr.GetInt64(0),
                    Name = dr.GetString(1),
                    OwnerId = dr.GetInt64(2),
                    Location = dr.GetString(3)
                };
                restuarants.Add(restuarant);
            }
            dr.Close();
            return restuarants;
        }

        public List<MenuDTO> GetMenu(long RID)
        {
            List<MenuDTO> menus = new List<MenuDTO>();
            string query = "select * from Menu where RID=@RID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@RID", RID);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MenuDTO menu = new MenuDTO
                {
                    MenuId = dr.GetInt64(0),
                    MenuName = dr.GetString(1),
                    RID = dr.GetInt64(2),
                    UnitPrice = dr.GetDecimal(3),
                    FoodType = dr.GetString(4)
                };
                menus.Add(menu);
            }
            dr.Close();
            return menus;
        }

        public List<MenuDTO> GetMenuByFoodType(string foodType)
        {
            List<MenuDTO> menus = new List<MenuDTO>();
            string query = "select * from Menu where FoodType=@FoodType";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FoodType", foodType);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MenuDTO menu = new MenuDTO
                {
                    MenuId = dr.GetInt64(0),
                    MenuName = dr.GetString(1),
                    RID = dr.GetInt64(2),
                    UnitPrice = dr.GetDecimal(3),
                    FoodType = dr.GetString(4)
                };
                menus.Add(menu);
            }
            dr.Close();
            return menus;
        }

        public bool AddUser(UserDTO user)
        {
            string query = "insert into Users (Username, Password, Email, Rolename, Location) values (@Username, @Password, @Email, @Rolename, @Location)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Rolename", user.Rolename);
            cmd.Parameters.AddWithValue("@Location", user.Location);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool AddRestuarant(RestuarantDTO restuarant)
        {
            string query = "insert into Restuarants (Name, OwnerId, Location) values (@Name, @OwnerId, @Location)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", restuarant.Name);
            cmd.Parameters.AddWithValue("@OwnerId", restuarant.OwnerId);
            cmd.Parameters.AddWithValue("@Location", restuarant.Location);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool AssignOwner(long restuarantId, long ownerId)
        {
            string query = "update Restuarants set OwnerId=@OwnerId where RID=@RID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OwnerId", ownerId);
            cmd.Parameters.AddWithValue("@RID", restuarantId);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool AddMenu(MenuDTO menu)
        {
            string query = "insert into Menu (MenuName, RID, UnitPrice, FoodType) values (@MenuName, @RID, @UnitPrice, @FoodType)";
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
                    OrderId = dr.GetInt64(0),
                    RID = dr.GetInt64(1),
                    OrderBy = dr.GetInt64(2),
                    Status = dr.GetString(3),
                    OrderDate = dr.GetDateTime(4)
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
                    OrderId = dr.GetInt64(0),
                    RID = dr.GetInt64(1),
                    OrderBy = dr.GetInt64(2),
                    Status = dr.GetString(3),
                    OrderDate = dr.GetDateTime(4)
                };
            }
            dr.Close();
            return order;
        }
    }
}
