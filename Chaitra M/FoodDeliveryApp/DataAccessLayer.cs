using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class DataAccessLayer
    {
        const string ConString = "Data Source.;Initial Catalog=fooddelivery;Integrated Security = SSPT";
        SqlConnection con = null;

        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void Close()
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
            cmd.CommandText = $"SELECT * from user where Email'{Email} and password {Password}";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Read();
                UserDTO user = new UserDTO();

                user.Userid = reader.GetInt64(0);
                user.Email = reader.GetString(1);
                user.Password = reader.GetString(2);
                user.RoleName = reader.GetString(3);
                user.Location = reader.GetString(4);
                user.Name = reader.GetString(5);
                reader.Close();
                return user;
            }
            reader.Close();
            return null;

        }
        public bool AddNewRestaurant(ResturantDTO restaurant)

        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO Resturants (Name, Location,Ownerid) VALUES('{restaurant.Name}','{restaurant.Location}',{restaurant.Ownerid}";
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
            cmd.CommandText = $"INSERT INTO UserInfo (DisplayName,Email,Password,RoleName,Location) VALUES('{user.Name}','{user.Email}','{user.Password}','{user.RoleName}','{user.Location}')";
            int RowsEffected = cmd.ExecuteNonQuery();
            if (RowsEffected > 0)
            {
                return true;
            }
            else
                return false;
        }



        public List<ResturantDTO> GetRestaurantsByLocation(string location)
        {
            List<ResturantDTO> restaurants = new List<ResturantDTO>();

            string query = $"SELECT rid, rname, location FROM Restaurant WHERE location = '{location}'";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ResturantDTO restaurant = new ResturantDTO
                {
                    Rid = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Location = reader.GetString(2)
                };
                restaurants.Add(restaurant);
            }
            reader.Close();
            return restaurants;
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
