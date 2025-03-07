using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FooddeliveryADO
{
    public class DataAccess
    {
        SqlConnection con;
        string strcon = "Data Source=.;Initial Catalog=FOODDEL;Integrated Security=SSPI";
        public DataAccess()
        {
            con = new SqlConnection(strcon);
        }

        public void OpenConnection()
        {
            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
        }

        public List<usersDTO> Listusers()
        {
            List<usersDTO> users = new List<usersDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from users";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                usersDTO user = new usersDTO();
                user.userid = reader.GetInt64(0);
                user.uname = reader.GetString(1);
                user.email = reader.GetString(2);
                user.password = reader.GetString(3);
                user.roledal = (role)reader.GetInt64(4);
                user.ulocation = reader.GetString(5);
                users.Add(user);
            }
            reader.Close();
            return users;
        }

        public bool Addusers(usersDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into users(userid,uname,email,password,role,ulocation) values({user.userid},'{user.uname}','{user.email}','{user.password}',{(long)user.roledal},'{user.ulocation}')";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public usersDTO AuthenticateUser(string email, string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM users WHERE email = @Email AND password = @Password";
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                usersDTO user = new usersDTO();
                user.userid = reader.GetInt64(0);
                user.uname = reader.GetString(1);
                user.email = reader.GetString(2);
                user.password = reader.GetString(3);
                user.roledal = (role)reader.GetInt64(4);
                user.ulocation = reader.GetString(5);
                reader.Close();
                return user;
            }
            else
            {
                reader.Close();
                return null;
            }
        }
        public List<restaurantsDTO> ListRestaurants()
        {
            List<restaurantsDTO> restaurants = new List<restaurantsDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from restaurants";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                restaurantsDTO restaurant = new restaurantsDTO();
                restaurant.rid = reader.GetInt64(0);
                restaurant.rname = reader.GetString(1);
                restaurant.rlocation = reader.GetString(2);
                restaurant.ownerid = reader.GetInt64(3);
                restaurants.Add(restaurant);
            }
            reader.Close();
            return restaurants;
        }
        public bool AddRestaurants(restaurantsDTO restaurant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into restaurants(rid,rname,rlocation,ownerid) values({restaurant.rid},'{restaurant.rname}','{restaurant.rlocation}',{restaurant.ownerid})";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public List<menusDTO> ListMenus()
        {
            List<menusDTO> menus = new List<menusDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from menus";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                menusDTO menu = new menusDTO();
                menu.menuid = reader.GetInt64(0);
                menu.rid = reader.GetInt64(1);
                menu.itemname = reader.GetString(2);
                menu.price = reader.GetDouble(3);
                menus.Add(menu);
            }
            reader.Close();
            return menus;
        }
       public bool deletereataurant(long rid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"delete from restaurants where rid = {rid}";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public bool AddMenus(menusDTO menu)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into menus(menuid,rid,itemname,price) values({menu.menuid},{menu.rid},'{menu.itemname}',{menu.price})";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public List<ordersDTO> ListOrders()
        {
            List<ordersDTO> orders = new List<ordersDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from orders";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ordersDTO order = new ordersDTO();
                order.orderid = reader.GetInt64(0);
                order.userid = reader.GetInt64(1);
                order.rid = reader.GetInt64(2);
                order.orderstatus = reader.GetString(3);
                orders.Add(order);
            }
            reader.Close();
            return orders;
        }
        public bool AddOrders(ordersDTO order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into orders(orderid,userid,rid,orderstatus) values({order.orderid},{order.userid},{order.rid},'{order.orderstatus}')";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public List<orderitemDTO> ListOrderItems()
        {
            List<orderitemDTO> orderitems = new List<orderitemDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from orderitems";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                orderitemDTO orderitem = new orderitemDTO();
                orderitem.orderitemid = reader.GetInt64(0);
                orderitem.orderid = reader.GetInt64(1);
                orderitem.menuid = reader.GetInt64(2);
                orderitem.quantity = reader.GetInt32(3);
                orderitems.Add(orderitem);
            }
            reader.Close();
            return orderitems;
        }
        public bool AddOrderItems(orderitemDTO orderitem)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into orderitems(orderitemid,orderid,menuid,quantity) values({orderitem.orderitemid},{orderitem.orderid},{orderitem.menuid},{orderitem.quantity})";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public bool AssignOwnerToRestaurant(long restaurantId, long ownerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"update restaurants set ownerid = {ownerId} where rid = {restaurantId}";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public List<restaurantsDTO> SearchRestaurantsByLocation(string location)
        {
            List<restaurantsDTO> restaurants = new List<restaurantsDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from restaurants where rlocation = @Location";
            cmd.Parameters.AddWithValue("@Location", location);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                restaurantsDTO restaurant = new restaurantsDTO();
                restaurant.rid = reader.GetInt64(0);
                restaurant.rname = reader.GetString(1);
                restaurant.rlocation = reader.GetString(2);
                restaurant.ownerid = reader.GetInt64(3);
                restaurants.Add(restaurant);
            }
            reader.Close();
            return restaurants;
        }
        public bool UpdateOrderStatus(long orderId, string status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"update orders set orderstatus = '{status}' where orderid = {orderId}";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool AssignOwnerToRestaurants(long restaurantId, long ownerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"update restaurants set ownerid = {ownerId} where rid = {restaurantId}";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public List<menusDTO> GetMenuByRestaurant(long restaurantId)
        {
            List<menusDTO> menus = new List<menusDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from menus where rid = @RestaurantId";
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                menusDTO menu = new menusDTO();
                menu.menuid = reader.GetInt64(0);
                menu.rid = reader.GetInt64(1);
                menu.itemname = reader.GetString(2);
                menu.price = reader.GetDouble(3);
                menus.Add(menu);
            }
            reader.Close();
            return menus;
        }


    }
}
