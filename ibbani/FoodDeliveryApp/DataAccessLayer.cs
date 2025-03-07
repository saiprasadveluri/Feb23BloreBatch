using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=FoodDeliveryApp;Integrated Security=SSPI";
        SqlConnection con = null;

        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void CloseApp()
        {
            if(con!=null)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public UserDTO Login(string email, string pswd)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM User where Email='{email}' AND Password='{pswd}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO();
                user.userid = reader.GetInt64(0);
                user.username = reader.GetString(1);
                user.rolename = reader.GetString(2);
                user.address= reader.GetString(3);
                user.email = reader.GetString(4);
                user.pswd = reader.GetString(5);
                reader.Close();
                return user;


            }
            reader.Close();
            return null;
                
        }

        public bool AddNewRestaurent(RestaurentDTO restaurent)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Restaurent ( RID,RestName,Location,OwnerId) VALUES('{restaurent.RID}','{restaurent.RestName}','{restaurent.location}','{restaurent.Ownerid}'";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool AddNewUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO User (userid,username,rolename,address,email,psw) VALUES('{user.userid}','{user.username}','{user.rolename}','{user.address}'," +
                $"'{user.email}','{user.pswd})";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;

        }

        public string GetUserRole(long userid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select rolename from User where userid={userid}";
            object res = cmd.ExecuteScalar();
            if (res != null)
            {
                return res.ToString();
            }
            else
            {
                return null;
            }
        }

    }
}
