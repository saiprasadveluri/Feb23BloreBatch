using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooddeliveryApp
{
    public class DataAcessLayer
    {
        string constring = "Data Source=.;Initial Catalog=FOODDEL;Integrated Security=SSPI";
        SqlConnection con;
        public DataAcessLayer()
        {
            con = new SqlConnection(constring);

        }
        public void OpenConnection()
        {
            con.Open();
        }
        public void CloseConnection()
        {
            con.Close();
        }
        public bool Addorderitems(orderitemDTO orderitems)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Insert into Orderitems(orderid,menuid,quantity) values(,{orderitems.orderid},{orderitems.menuid},{orderitems.quantity})";
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
                return true;
            else
                return false;
        }
        public bool Addmenu(menusDTO menus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Insert into Menus(rid,itemname,price) values({menus.rid},'{menus.itemname}',{menus.price})";
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
                return true;
            else
                return false;
        }
       
        public bool Adduser(usersDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Insert into Users(uname,email,password,role,ulocation) values('{user.uname}','{user.email}','{user.password}',{(long)user.roledal},'{user.ulocation}')";
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
                return true;
            else
                return false;
        }
        public bool Addorders(ordersDTO ord)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Insert into Orders(userid,rid,orderstatus) values({ord.userid},{ord.rid},'{ord.orderstatus}')";
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
                return true;
            else
                return false;
        }
        public bool Addrestaurant(restaurantDTO res)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Insert into Restaurants(rname,rlocation,ownerid) values('{res.rname}','{res.rlocation}',{res.ownerid})";
            int ress = cmd.ExecuteNonQuery();
            if (ress > 0)
                return true;
            else
                return false;
        }

        public usersDTO LoginUser(string email, string password)
        {
            List<usersDTO> users = new List<usersDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"select * from users where Email='{email}' AND Password='{password}'";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                usersDTO user = new usersDTO();
                user.userid = dr.GetInt64(0);
                user.uname = dr.GetString(1);
                user.email = dr.GetString(2);
                user.password = dr.GetString(3);
                user.roledal = (usersDTO.role)dr.GetInt64(4);
                user.ulocation = dr.GetString(5);
                dr.Close();
                return user;
            }
            else
            {
                dr.Close();
                return null;

            }
        }
        public List<usersDTO> Listusers()
        {
            List<usersDTO> users = new List<usersDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Users";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                usersDTO user = new usersDTO();
                user.userid = dr.GetInt64(0);
                user.uname = dr.GetString(1);
                user.email = dr.GetString(2);
                user.password = dr.GetString(3);
                user.roledal = (usersDTO.role)dr.GetInt64(4);
                user.ulocation = dr.GetString(5);
                users.Add(user);
            }
            dr.Close();
            return users;
        }
        public List<ordersDTO> Listorders()
        {
            List<ordersDTO> orders = new List<ordersDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Orders";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ordersDTO order = new ordersDTO();
                order.orderid = dr.GetInt64(0);
                order.userid = dr.GetInt64(1);
                order.rid = dr.GetInt64(2);
                order.orderstatus = dr.GetString(3);
                orders.Add(order);
            }
            dr.Close();
            return orders;
        }
        public List<orderitemDTO> Listorderitems()
        {
            List<orderitemDTO> orderitems = new List<orderitemDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Orderitems";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                orderitemDTO orderitem = new orderitemDTO();
                orderitem.orderitemid = dr.GetInt64(0);
                orderitem.orderid = dr.GetInt64(1);
                orderitem.menuid = dr.GetInt64(2);
                orderitem.quantity = dr.GetInt64(3);
                orderitems.Add(orderitem);
            }
            dr.Close();
            return orderitems;
        }
        public List<restaurantDTO> Listrestaurant()
        {
            List<restaurantDTO> res = new List<restaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Restaurants";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                restaurantDTO ress = new restaurantDTO();
                ress.resid = dr.GetInt64(0);
                ress.rname = dr.GetString(1);
                ress.rlocation = dr.GetString(2);
                ress.ownerid = dr.GetInt64(3);
                res.Add(ress);
            }
            dr.Close();
            return res;
        }
        public bool deleterestaurant(long resid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Delete from Restaurants where resid={resid}";
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
                return true;
            else
                return false;
        }
        public List<restaurantDTO> Searchrestaurant(string location)
        {
            List<restaurantDTO> res = new List<restaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select * from Restaurants where rlocation='{location}'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                restaurantDTO ress = new restaurantDTO();
                ress.resid = dr.GetInt64(0);
                ress.rname = dr.GetString(1);
                ress.rlocation = dr.GetString(2);
                ress.ownerid = dr.GetInt64(3);
                res.Add(ress);
            }
            dr.Close();
            return res;
        }
    }
}
