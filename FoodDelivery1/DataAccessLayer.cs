using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery1
{
    class DataAccessLayer
    {
        const string ConString = "Data Source.;Initial Catalog=Fooddelivery;Integrated Security=SSPI";
        SqlConnection con = null;
        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void CloseApp()
        {
            if(con != null)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public UserDTO Login(string Email,string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM users where Email'{Email} and password {Password}";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                UserDTO user = new UserDTO();
                user.UserId = reader.GetInt64(0);
                user.Name= reader.GetString(1);
                user.Email = reader.GetString(2);
                user.Password = reader.GetString(3);
                user.RoleName = reader.GetString(4);
                user.Location = reader.GetString(5);
                reader.Close();
                return user;

            }
            reader.Close();
            return null;
        }
        public bool AddNewRestaurant(Restaurant restaurant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Restaurents (RName,Location,OwnerId) VALUES('{restaurant.Name}','{restaurant.Location}',{restaurant.OwnerID}";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool AddNewUser(UserDTO users)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO UserInfo (Name,Email,Password,RoleName,Location) VALUES('{users.Name}','{users.Email}','{users.Password}','{users.RoleName}','{users.Location}')";
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


    }
}
