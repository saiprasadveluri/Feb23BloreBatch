using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.Design;

namespace FoodDeliveryApp
{
    
    public class DataAccessLayer
    {
        static SqlConnection Conn;
        string connectionString = "Data Source=.; Initial Catalog = FOODDELIVERY; Integrated Security = SSPI";
        public DataAccessLayer()
        {
            Conn = new SqlConnection(connectionString);
            Conn.Open();
        }
        SqlCommand cmd = new SqlCommand();
        
        public void CloseApp() 
        {
            if (Conn.State == System.Data.ConnectionState.Open)
            {
                Conn.Close();  
            }
        }


        public bool CheckEmail(string email)
        {
            cmd.Connection = Conn;
            cmd.CommandText = "SELECT COUNT(*) FROM USERS WHERE email = @email";
            cmd.Parameters.Add(new SqlParameter("@email", email));
            int res = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Parameters.Clear();
            if (res  == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddUser(Users user)
        {
            cmd.Connection = Conn;
            cmd.CommandText = "INSERT INTO USERS(username, email, password, role, location) VALUES(@username, @newemail, @password, @role, @location)";
            
            cmd.Parameters.Add(new SqlParameter("@username", user.username));
            cmd.Parameters.Add(new SqlParameter("@newemail", user.email));
            cmd.Parameters.Add(new SqlParameter("@password", user.password));
            cmd.Parameters.Add(new SqlParameter("@role", user.role));
            cmd.Parameters.Add(new SqlParameter("@location", user.location));
            int res = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (res > 0)
            {

                cmd.CommandText = "SELECT userid FROM USERS WHERE email = @email";
                cmd.Parameters.Add(new SqlParameter("@email", user.email));
                long userid = Convert.ToInt64(cmd.ExecuteScalar());
                user.userid = userid;
                cmd.Parameters.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }


        //Authenticate the user.
        public Users Login(string email, string password)
        {
            cmd.Connection = Conn;
            cmd.CommandText = "SELECT * FROM USERS WHERE email = @email AND password = @password";
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("password", password));
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            Users user = new Users();
            if (reader.HasRows) 
            {
                reader.Read();
                user.userid = reader.GetInt64(0);
                user.username = reader.GetString(1);
                user.email = reader.GetString(2);
                user.password = reader.GetString(3);
                user.role = reader.GetString(4);
                user.location = reader.GetString(5);
                reader.Close();

            }
            else
            {
                reader.Close();
                user = null;
            }

                return user;
        }


        //Add restaurant
        public bool AddRestaurant(Restaurants restaurant)
        {
            cmd.Connection = Conn;
            cmd.CommandText = "INSERT INTO RESTAURANTS(restaurantname, location, ownerid) VALUES(@restaurantname, @location, @ownerid)";
            cmd.Parameters.Add(new SqlParameter("@restaurantname", restaurant.restaurantname));
            cmd.Parameters.Add(new SqlParameter("@location", restaurant.location));
            cmd.Parameters.Add(new SqlParameter("@ownerid", restaurant.ownerid));
            int res = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (res > 0)
            {
                cmd.CommandText = "SELECT restaurantid FROM RESTAURANTS WHERE restaurantname = @name";
                cmd.Parameters.Add(new SqlParameter("@name", restaurant.restaurantname));
                long restaurantid = Convert.ToInt64(cmd.ExecuteScalar());
                restaurant.restaurantid = restaurantid;
                cmd.Parameters.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool RemoveRestaurant(long restaurantid)
        {
            cmd.Connection = Conn;
            cmd.CommandText = "DELETE FROM RESTAURANTS WHERE restaurantid = @restaurantid";
            cmd.Parameters.Add(new SqlParameter("@restaurantid", restaurantid));
            int res = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool AssignOwner(long restaurantid, long ownerid)
        {
            cmd.Connection = Conn;
            cmd.CommandText = "UPDATE RESTAURANTS SET ownerid = @ownerid WHERE restaurantid = @restaurantid";
            cmd.Parameters.Add(new SqlParameter("@ownerid", ownerid));
            cmd.Parameters.Add(new SqlParameter("@restaurantid", restaurantid));
            int res = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    

    public class Users
    {
        public long userid { get; set; } = 0;
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string location { get; set; }


    }

    public class Restaurants
    {
        public long restaurantid { get; set; }
        public string restaurantname { get; set; }
        public string location { get; set; }
        public long ownerid { get; set; }

    }

    public class MenuItems
    {
        public long menuitemid { get; set; }
        public string menuitemname { get; set; }
        public double price { get; set; }
        public string categor { get; set; }
        public long restaurantid { get; set; }

    }

    public class Orders
    {
        public long orderid { get; set; }
        public double payment { get; set; }
        public string status { get; set; }
        public string orderdetails { get; set; }
        public long userid { get; set; }
        public long restaurantid { get; set; }

    }


    public class OrderItems
    {
        public long orderitemid { get; set; }
        public long orderid { get; set; }
        public long itemid { get; set; }

    }



}
