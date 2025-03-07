using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    public class DataAccessLayer
    {
        SqlConnection con;
        string strCon = "Data Source=.;Initial Catalog=TaskManager;Integrated Security=SSPI";

        public DataAccessLayer() { con = new SqlConnection(strCon); }

        public void OpenConnection() { con.Open(); }
        public void CloseConnection() { con.Close(); }

        public UserDTO LoginUser(string email, string password)
        {
            string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                UserDTO user = new UserDTO
                {
                    UserId = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Department = reader.GetString(2),
                    RoleId = reader.GetInt32(3),
                    Email = reader.GetString(4),
                    Password = reader.GetString(5)
                };
                reader.Close();
                return user;
            }
            reader.Close();
            return null;
        }

        public bool AddUser(UserDTO user)
        {
            string sql = @"INSERT INTO Users(Name,Department,RoleId,Email,Password) 
                       VALUES(@Name,@Department,@RoleId,@Email,@Password)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Department", user.Department);
            cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool AddProject(ProjectDTO project)
        {
            string sql = "INSERT INTO Projects (Title, Status, ManagerId) VALUES (@Title, @Status, @ManagerId)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Title", project.Title);
            cmd.Parameters.AddWithValue("@Status", project.Status);
            cmd.Parameters.AddWithValue("@ManagerId", project.ManagerId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool AddTask(TaskDTO task)
        {
            string sql = @"INSERT INTO Tasks (Title, Type, AssignedTo, ProjID, Status, StartDate, EndDate, HoursLogged) 
                       VALUES (@Title, @Type, @AssignedTo, @ProjID, @Status, @StartDate, @EndDate, @HoursLogged)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Title", task.Title);
            cmd.Parameters.AddWithValue("@Type", task.Type);
            cmd.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
            cmd.Parameters.AddWithValue("@ProjID", task.ProjID);
            cmd.Parameters.AddWithValue("@Status", task.Status);
            cmd.Parameters.AddWithValue("@StartDate", task.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", task.EndDate);
            cmd.Parameters.AddWithValue("@HoursLogged", task.HoursLogged);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool AddComment(CommentDTO comment)
        {
            string sql = "INSERT INTO Comments (TaskID, CommentedBy, CommentText) VALUES (@TaskID, @CommentedBy, @CommentText)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@TaskID", comment.TaskID);
            cmd.Parameters.AddWithValue("@CommentedBy", comment.CommentedBy);
            cmd.Parameters.AddWithValue("@CommentText", comment.CommentText);
            return cmd.ExecuteNonQuery() > 0;
        }
    }

}
