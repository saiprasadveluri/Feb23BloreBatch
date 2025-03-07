using Restaurant_Food_App;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaurant_Food_App
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source=.;Initial Catalog=RES_FOOD;Integrated Security=SSPI";
        //const string ConString = "Data Source=.;Initial Catalog=RES_FOOD;Integrated Security=SSPI";
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
            cmd.CommandText = $"SELECT * FROM Users where Email=@Email AND Password=@Password";
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO()
                {
                    User_id = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    RoleName = reader.GetString(4)
                };
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
            cmd.CommandText = $"INSERT INTO Restaurants (res_name,city,phone,OwnerId) VALUES('{restaurant.res_name}, '{restaurant.city}', '{restaurant.phone},'{restaurant.owner_id}')";
            cmd.Parameters.AddWithValue("@res_name", restaurant.res_name);
            cmd.Parameters.AddWithValue("@city", restaurant.city);
            cmd.Parameters.AddWithValue("@phone", restaurant.phone);
            cmd.Parameters.AddWithValue("@OwnerId", restaurant.owner_id);
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }


        public bool AddNewUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Users (Name,Email,Password,RoleName) VALUES('{user.Name}','{user.Email}','{user.Password}','{user.RoleName}')";
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RoleName", user.RoleName);
            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
            
        }

        public string GetUserRole(long User_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"Select RoleName from Users where User_id={User_id}";
            cmd.Parameters.AddWithValue("@User_id", User_id);
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
