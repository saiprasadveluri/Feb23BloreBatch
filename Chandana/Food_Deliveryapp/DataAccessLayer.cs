using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Food_Deliveryapp
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=FOODDELIVERY;Integrated Security=SSPI";
        SqlConnection con = null;
        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void CloseApp()
        {
            if (con != null)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public UserDTO Login(string email, string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"select * from Users where Email = '{email}' and Password = '{password}'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                UserDTO user = new UserDTO
                {
                    Uid = dr.GetInt64(0),
                    Name = dr.GetString(1),
                    Email = dr.GetString(2),
                    Address = dr.GetString(3),
                    Role = dr.GetString(4)
                };
                dr.Close();
                return user;
            }
            dr.Close();
            return null;
        }

        public bool AddNewRestaurant(RestaurantDTO r)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Restaurant (rname, location) VALUES ('{r.rname}', '{r.location}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        public bool AddNewUser(UserDTO u)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Users (Name, Email, Password, Address, Role) VALUES ('{u.Name}', '{u.Email}', '{u.Password}', '{u.Address}', '{u.Role}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        public bool RemoveRestaurant(long rid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM Restaurant WHERE rid = {rid}";
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        public bool IsRestaurantOwner(long rid, long uid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT COUNT(*) FROM Restaurant WHERE rid = {rid} AND ownerId = {uid}";
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool AddMenu(MenuDTO menu)
        {
            if (!IsRestaurantOwner(menu.rid, long.Parse(menu.orderedby)))
            {
                throw new UnauthorizedAccessException("Only restaurant owners can add a new menu.");
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Menu (mname, rid, price, category, orderedby) VALUES ('{menu.mname}', '{menu.rid}', '{menu.price}', '{menu.category}', '{menu.orderedby}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        public List<RestaurantDTO> GetRestaurantsByLocation(string location)
        {
            List<RestaurantDTO> restaurants = new List<RestaurantDTO>();

            string query = $"SELECT rid, rname, location FROM Restaurant WHERE location = '{location}'";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RestaurantDTO restaurant = new RestaurantDTO
                {
                    rid = reader.GetInt64(0),
                    rname = reader.GetString(1),
                    location = reader.GetString(2)
                };
                restaurants.Add(restaurant);
            }
            reader.Close();
            return restaurants;
        }

        public List<MenuDTO> GetItemsByPreference(string preference)
        {
            List<MenuDTO> items = new List<MenuDTO>();
            SqlCommand cmd = new SqlCommand($"SELECT mid, mname, rid, price, category, orderedby FROM Menu WHERE mname LIKE '%{preference}%' OR category LIKE '%{preference}%'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new MenuDTO
                {
                    mid = reader.GetInt64(0),
                    mname = reader.GetString(1),
                    rid = reader.GetInt64(2),
                    price = reader.GetInt64(3),
                    category = reader.GetString(4),
                    orderedby = reader.GetString(5)
                });
            }
            reader.Close();
            return items;
        }

        public bool PlaceOrder(OrderDTO order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Orders (UserId, RestaurantId, MenuItemId, Quantity, OrderDate, Status) VALUES ({order.uid}, {order.rid}, {order.mid}, {order.total}, '{order.orderdate}', '{order.status}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        public bool UpdateOrderStatus(long orderId, string status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE Orders SET Status = '{status}' WHERE OrderId = {orderId}";
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        public List<MenuDTO> GetAllMenus()
        {
            List<MenuDTO> menus = new List<MenuDTO>();
            SqlCommand cmd = new SqlCommand("SELECT mid, mname, rid, price, category, orderedby FROM Menu", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                menus.Add(new MenuDTO
                {
                    mid = reader.GetInt64(0),
                    mname = reader.GetString(1),
                    rid = reader.GetInt64(2),
                    price = reader.GetInt64(3),
                    category = reader.GetString(4),
                    orderedby = reader.GetString(5)
                });
            }
            reader.Close();
            return menus;
        }

        public List<OrderDTO> GetAllOrders()
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            SqlCommand cmd = new SqlCommand("SELECT oid, mid, rid, uid, total, status, orderdate FROM Orders", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new OrderDTO
                {
                    oid = reader.GetInt64(0),
                    mid = reader.GetInt64(1),
                    rid = reader.GetInt64(2),
                    uid = reader.GetInt64(3),
                    total = reader.GetInt64(4),
                    status = reader.GetString(5),
                    orderdate = reader.GetString(6)
                });
            }
            reader.Close();
            return orders;
        }

        public List<OrderDTO> GetOrdersByUserId(long userId)
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Orders WHERE uid = {userId}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new OrderDTO
                {
                    oid = reader.GetInt64(0),
                    mid = reader.GetInt64(1),
                    rid = reader.GetInt64(2),
                    uid = reader.GetInt64(3),
                    total = reader.GetInt64(4),
                    status = reader.GetString(5),
                    orderdate = reader.GetString(6)
                });
            }
            reader.Close();
            return orders;
        }

        public bool AssignOwnerToRestaurant(long rid, long ownerId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Restaurant SET OwnerId = {ownerId} WHERE RestaurantId = {rid}", con);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
    }
}