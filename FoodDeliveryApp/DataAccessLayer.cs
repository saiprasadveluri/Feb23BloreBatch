using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    class DataAccessLayer
    {
        SqlConnection con;
        string strCon = "Data Source=.; Initial Catalog=FD;Integrated Security=SSPI";//connection string
        public DataAccessLayer()
        {
            con = new SqlConnection(strCon);

        }
        public void OpenConnection()
        {
            con.Open();
        }
        public void CloseConnection()
        {
            con.Close();
        }

       

        public bool AddUser(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Insert Into Users(RoleId,Email,Password,UserLoc,UserName) Values({inp.RoleId},'{inp.Email}','{inp.Password}','{inp.UserLoc}','{inp.UserName}')";

            //Executing Command
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddRestaurant(RestaurentsDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Insert Into Restaurants(ResName,ResLoc,OwnerID)Values('{inp.ResName}','{inp.ResLoc}',{inp.OwnerID})";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckOwnerId(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select UserName from Users where UserId={inp.UserID}";
            object res = cmd.ExecuteScalar();
            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteRestaurant(RestaurentsDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM RESTAURANTS WHERE ResID = {inp.ResId} ";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Password verification
        public UserDTO LoginUser(string Email, string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"select * from Users where Email='{Email}' AND Password ='{Password}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO();
                user.UserID = reader.GetInt64(1);
                user.UserName = reader.GetString(5);
                user.Password = reader.GetString(3);
                user.RoleId = reader.GetInt64(0);
                user.Email = reader.GetString(2);
                user.UserLoc = reader.GetString(4);
                reader.Close();
                return user;
            }
            reader.Close();
            return null;
        }


        


        
    }
}
