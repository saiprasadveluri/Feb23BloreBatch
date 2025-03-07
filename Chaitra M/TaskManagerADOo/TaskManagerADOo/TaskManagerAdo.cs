using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            con.Open();
        }
        public void CloseConnection()
        {
            con.Close();
        }
        public List<UserDTO> ListUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con; // pass open connection object
            cmd.CommandText = "Select * from Users";
            // multiple rows
            SqlDataReader dr = cmd.ExecuteReader();
            // sequential, from, Readonly access to result set
            while (dr.Read()) // progress one row at a time
            {
                long UserId = dr.GetInt64(0);
                string Name = dr.GetString(1);
                string Dept = dr.GetString(2);
                long Roleid = dr.GetInt64(3);
                UserDTO user = new UserDTO();
                user.UserId = UserId;
                user.Name = Name;
                user.Dept = Dept;
                users.Add(user);

            }
            dr.Close();
            return users;

        }
        public List<projectDTO> projectbypm()
        {

            List<projectDTO> projects = new List<projectDTO>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Projects where PmId=@PmId";
            cmd.Parameters.AddWithValue("@PmId", 1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Title = dr.GetString(1);
                string Status = dr.GetString(3);
                Console.WriteLine($"Title:{Title}- Status:{Status}");
            }
            dr.Close();
            return projects;

        }
        public bool AddUser(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Insert into Users values(@Name,@Dept,@Roleid)";
            cmd.Parameters.AddWithValue("@Name", inp.Name);
            cmd.Parameters.AddWithValue("@Dept", inp.Dept);
            cmd.Parameters.AddWithValue("@Roleid", inp.Roleid);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                return true;
            else
                return false;
        }
        //public void CreateProject()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "Insert into Projects values(@ProjectName,@StartDate,@EndDate,@PmId)";
        //    cmd.Parameters.AddWithValue("@ProjectName", "Project1");
        //    cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
        //    cmd.Parameters.AddWithValue("@EndDate", DateTime.Now.AddDays(10));
        //    cmd.Parameters.AddWithValue("@PmId", 1);
        //    int rows = cmd.ExecuteNonQuery();
        //    if (rows > 0)
        //        Console.WriteLine("Project Added");
        //    else
        //        Console.WriteLine("Project not Added");
        //}
        public bool CreateTask()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Insert into Tasks values(@TaskName,@StartDate,@EndDate,@PmId,@UserId)";
            cmd.Parameters.AddWithValue("@TaskName", "Task1");
            cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@EndDate", DateTime.Now.AddDays(10));
            cmd.Parameters.AddWithValue("@PmId", 1);
            cmd.Parameters.AddWithValue("@UserId", 1);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                return true;
            else
                return false;
        }
        public int GetUserCount()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;//pass open connection object
            cmd.CommandText = "Select count(*) from Users";
            object res = cmd.ExecuteScalar();
            int UserCount = Convert.ToInt32(res);
            return UserCount;
        }
        public bool Addproject()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Insert into Projects values(@ProjectName,@StartDate,@EndDate,@PmId)";
            cmd.Parameters.AddWithValue("@ProjectName", "Project1");
            cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@EndDate", DateTime.Now.AddDays(10));
            cmd.Parameters.AddWithValue("@PmId", 1);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                return true;
            else
                return false;
        }
        //public List<projectDTO> projectbypm()
        //{    
        //    List<projectDTO> projects = new List<projectDTO>();

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "Select * from Projects where PmId=@PmId";
        //    cmd.Parameters.AddWithValue("@PmId", 1);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            string Title = dr.GetString(1);
        //            string Status = dr.GetString(3);
        //            Console.WriteLine($"Title:{Title}- Status:{Status}");
        //        }
        //        dr.Close();
        //    }
        //    else
        //    {
        //        Console.WriteLine("No Projects assigned");
        //    }

        //}
        //public void CreateUser()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "Insert into Users values(@Name,@Dept,@Roleid)";
        //    cmd.Parameters.AddWithValue("@Name", "User1");
        //    cmd.Parameters.AddWithValue("@Dept", "IT");
        //    cmd.Parameters.AddWithValue("@Roleid", 1);
        //    int rows = cmd.ExecuteNonQuery();
        //    if (rows > 0)
        //        Console.WriteLine("User Added");
        //    else
        //        Console.WriteLine("User not Added");
        //}
        public bool CreateComment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Insert into Comments values(@Comment,@TaskId,@UserId)";
            cmd.Parameters.AddWithValue("@Comment", "Comment1");
            cmd.Parameters.AddWithValue("@TaskId", 1);
            cmd.Parameters.AddWithValue("@UserId", 1);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                return true;
            else
                return false;

        }

    }
}