



using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TaskManagerADO
{
    class DataAccessLayer
    {
        SqlConnection con;
        string strcon = "Data Source=.;Initial Catalog=TaskManagerDemo;Integrated Security=SSPI";

        public DataAccessLayer()
        {
            con = new SqlConnection(strcon);
        }

        public void OpenConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public List<UserDTO> ListUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Users";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserDTO user = new UserDTO
                {
                    UserId = dr.GetInt64(0),
                    Name = dr.GetString(1),
                    Dept = dr.GetString(2),
                    Roleid = dr.GetInt64(3),
                    Email = dr.GetString(4),
                    Password = dr.GetString(5)
                };
                users.Add(user);
            }
            dr.Close();
            return users;
        }

        public bool AddUser(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Insert into Users(Name,Dept,Roleid,Email,Password) values(@Name,@Dept,@Roleid,@Email,@Password)";
            cmd.Parameters.AddWithValue("@Name", inp.Name);
            cmd.Parameters.AddWithValue("@Dept", inp.Dept);
            cmd.Parameters.AddWithValue("@Roleid", inp.Roleid);
            cmd.Parameters.AddWithValue("@Email", inp.Email);
            cmd.Parameters.AddWithValue("@Password", inp.Password);
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public UserDTO LoginUser(string Email, string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Users where Email=@Email and Password=@Password";
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                UserDTO user = new UserDTO
                {
                    UserId = dr.GetInt64(0),
                    Name = dr.GetString(1),
                    Dept = dr.GetString(2),
                    Roleid = dr.GetInt64(3),
                    Email = dr.GetString(4),
                    Password = dr.GetString(5)
                };
                dr.Close();
                return user;
            }
            dr.Close();
            return null;
        }
    }
}

