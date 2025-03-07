using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace TaskManagerADO
{
    public class DataAccessLayer
    {
        SqlConnection con;

        string strCon = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";
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

        public List<UserDTO> ListUsers()
        {
            List<UserDTO> usersList = new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM USERS";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long UserId = reader.GetInt64(0);
                string Name = reader.GetString(1);
                string Dept = reader.GetString(2);
                long RoleId = reader.GetInt64(3);
                string Email = reader.GetString(4);
                UserDTO usr = new UserDTO();
                usr.UserId = UserId;
                usr.Name = Name;
                usr.Dept = Dept;
                usr.RoleId = RoleId;
                usr.Email = Email;
                usersList.Add(usr);
            }
            reader.Close();
            return usersList;
        }

        public bool AddUser(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId,Email,Password) VALUES('{inp.Name}','{inp.Dept}',{inp.RoleId},'{inp.Email}','{inp.Password}')";

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

        public UserDTO LoginUser(string Email, string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM USERS where Email='{Email}' AND Password='{Password}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO();
                user.UserId = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                user.Dept = reader.GetString(2);
                user.RoleId = reader.GetInt64(3);
                user.Email = reader.GetString(4);
                reader.Close();
                return user;
            }
            reader.Close();
            return null;
        }
    }
}