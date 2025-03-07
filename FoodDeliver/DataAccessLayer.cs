using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliver
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=foody;Integrated Security=SSPI";
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
                    con.Close();
            }
        }

        public UserDTO Login(string Email, string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM UserInfo where Email='{Email}' AND Password='{Password}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO();
                user.UserId = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                user.RoleName = reader.GetString(4);
                user.Location = reader.GetString(5);
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
            cmd.CommandText = $"INSERT INTO Restaurant (RName,RLocation,OwerId) VALUES('{restaurant.Name}','{restaurant.Location}',{restaurant.OwnerId})";
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
            cmd.CommandText = $"INSERT INTO UserInfo (DisplayName,Email,Password,RoleName,Loaction) VALUES('{user.Name}','{user.Email}','{user.Password}','{user.RoleName}','{user.Location}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }

        public string GetUserRole(long UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select RoleName from UserInfo where UserId={UserId}";
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
        public bool DeleteRestaurant(long Rid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"delete from Restaurant where RId ={Rid}";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool RestaurantByUserLocation(UserDTO usr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"select RName from Restaurant r, UserInfo u where u.Loaction = r.RLocation and UserId = {usr.UserId} ";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }
        public List<RestaurantDTO> ListRestaurant()
        {
            List<RestaurantDTO> RestaurantList = new List<RestaurantDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * From Restaurant";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long RId = reader.GetInt64(0);
                string RName = reader.GetString(1);
                string RLocation = reader.GetString(2);
                long OwnerId = reader.GetInt64(3);
                

                RestaurantDTO rst = new RestaurantDTO();
                rst.RID = RId;
                rst.Name = RName;
                rst.Location = RLocation;
                rst.OwnerId = OwnerId;


                RestaurantList.Add(rst);
            }
            reader.Close();
            return RestaurantList;
        }
    }
}

