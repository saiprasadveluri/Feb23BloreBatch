using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    public class DataAccessLayer
    {
        SqlConnection conn;

        string connectionString = "server=WIN2019;Initial Catalog=testdb;Integrated Security=true;";
        public DataAccessLayer()
        {
            conn = new SqlConnection(connectionString);
            
        }

        public void OpenConnection()
        {
            conn.Open();
            Console.WriteLine("Connection successful!");
        }
        public void CloseConnection()
        {
            conn.Close();
        }

        public List<UserDTO> ListUsers()
        {
            List<UserDTO> usersList = new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Users";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                long UserId = reader.GetInt64(0);
                string UName = reader.GetString(1);
                string Dept = reader.GetString(2);
                long RoleId = reader.GetInt64(3);
                string Email = reader.GetString(4);
                string Password = reader.GetString(5);


                UserDTO user = new UserDTO();

                user.UserId = UserId;
                user.UName = UName;
                user.Dept = Dept;
                user.RoleId = RoleId;
                user.Email = Email;
                user.Password = Password;
                usersList.Add(user);
            }
            reader.Close();
            return usersList;

        }

        public bool AddUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Users(UName,UDept,RoleId,Email,Password) values(@uname, @dept, @roleid,@Email,@Password)";
            cmd.Parameters.AddWithValue("@uname", user.UName);
            cmd.Parameters.AddWithValue("@dept", user.Dept);
            cmd.Parameters.AddWithValue("@roleid", user.RoleId);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ProjectAssignedToDTO> ListProjAss()
        {
            List<ProjectAssignedToDTO> projAssList = new List<ProjectAssignedToDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from ProjectAssignedTo";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long id = reader.GetInt64(0);
                long projid = reader.GetInt64(1);
                long userid = reader.GetInt64(2);
                ProjectAssignedToDTO projAss = new ProjectAssignedToDTO();
                projAss.id = id;
                projAss.projid = projid;
                projAss.userid = userid;
                projAssList.Add(projAss);
            }
            reader.Close();
            return projAssList;
        }

        public bool AddProjAss(ProjectAssignedToDTO projAss)
        {
            if (projAss.userid == 2 || projAss.userid == 1)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into ProjectAssignedTo(projid, userid) values(@projid, @userid)";
            cmd.Parameters.AddWithValue("@projid", projAss.projid);
            cmd.Parameters.AddWithValue("@userid", projAss.userid);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateProjAss(ProjectAssignedToDTO projAss)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"update ProjectAssignedTo set projid = {projAss.projid}, userid = {projAss.userid} where id = {projAss.id}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProjAss(long id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"delete from ProjectAssignedTo where id = {id}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ProjectDTO> ListProjects()
        {
            List<ProjectDTO> projectsList = new List<ProjectDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Projects";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long ProjectId = reader.GetInt64(0);
                string ProjectName = reader.GetString(1);
                long ProjectManagerId = reader.GetInt64(2);
                string PStatus = reader.GetString(3);
                ProjectDTO project = new ProjectDTO();
                project.ProjectId = ProjectId;
                project.ProjectName = ProjectName;
                project.ProjectManagerId = ProjectManagerId;
                project.PStatus = PStatus;
                projectsList.Add(project);
            }
            reader.Close();
            return projectsList;
        }

        public bool AddProject(ProjectDTO project)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Projects(ProjectName, ProjectManagerId, PStatus) values(@pname, @pmid, @pstatus)";
            cmd.Parameters.AddWithValue("@pname", project.ProjectName);
            cmd.Parameters.AddWithValue("@pmid", project.ProjectManagerId);
            cmd.Parameters.AddWithValue("@pstatus", project.PStatus);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TaskDTO> ListTasks()
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Tasks";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long TaskId = reader.GetInt64(0);
                string Title = reader.GetString(1);
                int TType = reader.GetInt32(2);
                long ProjectId = reader.GetInt64(3);
                long AssignedTo = reader.GetInt64(4);
                
                TaskDTO task = new TaskDTO();
                task.TaskId = TaskId;
                task.Title = Title;
                task.TType = TType;
                task.ProjectId = ProjectId;
                task.AssignedTo = AssignedTo;
                tasksList.Add(task);
            }
            reader.Close();
            return tasksList;
        }

        public bool AddTask(TaskDTO task)
        {
            if (task.AssignedTo == 3)
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Tasks(Title, TType, ProjectId, AssignedTo) values(@title, @ttype, @pid, @atid, @tstat)";
                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@ttype", task.TType);
                cmd.Parameters.AddWithValue("@pid", task.ProjectId);
                cmd.Parameters.AddWithValue("@atid", task.AssignedTo);
                cmd.Parameters.AddWithValue("@tstat", task.TStatus);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<CommentDTO> ListComments()
        {
            List<CommentDTO> commentsList = new List<CommentDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Comments";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long CommentId = reader.GetInt64(0);
                string Title = reader.GetString(1);
                string CommentText = reader.GetString(2);
                long TaskId = reader.GetInt64(3);
                long CommentedBy = reader.GetInt64(4);
                CommentDTO comment = new CommentDTO();
                comment.CommentId = CommentId;
                comment.Title = Title;
                comment.CommentText = CommentText;
                comment.TaskId = TaskId;
                comment.CommentedBy = CommentedBy;
                commentsList.Add(comment);
            }
            reader.Close();
            return commentsList;
        }

        public bool AddComment(CommentDTO comment)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Comments(Title, CommentText, TaskId, CommentedBy) values(@title, @ctext, @tid, @cbid)";
            cmd.Parameters.AddWithValue("@title", comment.Title);
            cmd.Parameters.AddWithValue("@ctext", comment.CommentText);
            cmd.Parameters.AddWithValue("@tid", comment.TaskId);
            cmd.Parameters.AddWithValue("@cbid", comment.CommentedBy);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Users set UName = @uname, UDept = @udept, RoleId = @roleid where UserId = @uid";
            cmd.Parameters.AddWithValue("@uname", user.UName);
            cmd.Parameters.AddWithValue("@udept", user.Dept);
            cmd.Parameters.AddWithValue("@roleid", user.RoleId);
            cmd.Parameters.AddWithValue("@uid", user.UserId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUser(long UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from Users where UserId = @uid";
            cmd.Parameters.AddWithValue("@uid", UserId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool userhastasks(long UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"select * from Tasks where AssignedTo = {UserId}";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }

        public bool UpdateProject(ProjectDTO project)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Projects set ProjectName = @pname, ProjectManagerId = @pmid, PStatus = @pstatus where ProjectId = @pid";
            cmd.Parameters.AddWithValue("@pname", project.ProjectName);
            cmd.Parameters.AddWithValue("@pmid", project.ProjectManagerId);
            cmd.Parameters.AddWithValue("@pstatus", project.PStatus);
            cmd.Parameters.AddWithValue("@pid", project.ProjectId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProject(long ProjectId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from Projects where ProjectId = @pid";
            cmd.Parameters.AddWithValue("@pid", ProjectId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTask(TaskDTO task)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Tasks set Title = @title, TType = @ttype, ProjectId = @pid, AssignedTo = @atid where TaskId = @tid";
            cmd.Parameters.AddWithValue("@title", task.Title);
            cmd.Parameters.AddWithValue("@ttype", task.TType);
            cmd.Parameters.AddWithValue("@pid", task.ProjectId);
            cmd.Parameters.AddWithValue("@atid", task.AssignedTo);
            cmd.Parameters.AddWithValue("@tid", task.TaskId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTaskStatus(TaskDTO task)
        {
            if (task.AssignedTo ==4)
            {
                if (task.TStatus != "Close")
                {
                    return false;
                }
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Tasks set TStatus = @tstat where TaskId = @tid";
            cmd.Parameters.AddWithValue("@tstat", task.TStatus);
            cmd.Parameters.AddWithValue("@tid", task.TaskId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ProjectAssignedToDTO> ListProjectsAssigned(long userid)
        {
            List<ProjectAssignedToDTO> projAssList = new List<ProjectAssignedToDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"select * from ProjectAssignedTo where userid = {userid}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long id = reader.GetInt64(0);
                long projid = reader.GetInt64(1);
                long userid1 = reader.GetInt64(2);
                ProjectAssignedToDTO projAss = new ProjectAssignedToDTO();
                projAss.id = id;
                projAss.projid = projid;
                projAss.userid = userid1;
                projAssList.Add(projAss);
            }
            reader.Close();
            return projAssList;
        }

        public List<TaskDTO> ListTasksAssignedTo(long UserId)
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"select * from Tasks where AssignedTo = {UserId}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long TaskId = reader.GetInt64(0);
                string Title = reader.GetString(1);
                int TType = reader.GetInt32(2);
                long ProjectId = reader.GetInt64(3);
                long AssignedTo = reader.GetInt64(4);
                string TStatus = reader.GetString(5);
                TaskDTO task = new TaskDTO();
                task.TaskId = TaskId;
                task.Title = Title;
                task.TType = TType;
                task.ProjectId = ProjectId;
                task.AssignedTo = AssignedTo;
                task.TStatus = TStatus;
                tasksList.Add(task);
            }
            reader.Close();
            return tasksList;
        }

        public bool UpdateTaskAssignedTo(TaskDTO task)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Tasks set AssignedTo = @atid where TaskId = @tid";
            cmd.Parameters.AddWithValue("@atid", task.AssignedTo);
            cmd.Parameters.AddWithValue("@tid", task.TaskId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTask(long TaskId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from Tasks where TaskId = @tid";
            cmd.Parameters.AddWithValue("@tid", TaskId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateComment(CommentDTO comment)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"update Comments set Title = '{comment.Title}', CommentText = '{comment.CommentText}', TaskId = {comment.TaskId}, CommentedBy = {comment.CommentedBy} where CommentId = {comment.CommentId}";
            
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteComment(long CommentId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from Comments where CommentId = @cid";
            cmd.Parameters.AddWithValue("@cid", CommentId);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserDTO LoginUser(string email, string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"Select * from Users where Email = '{email}' and Password='{Password}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                long UserId = reader.GetInt64(0);
                string UName = reader.GetString(1);
                string Dept = reader.GetString(2);
                long RoleId = reader.GetInt64(3);
                string Email = reader.GetString(4);
                


                UserDTO user = new UserDTO();

                user.UserId = UserId;
                user.UName = UName;
                user.Dept = Dept;
                user.RoleId = RoleId;
                user.Email = Email;
                user.Password = Password;
                reader.Close();
                return user;


            }
            reader.Close();
            return null;
        }




    }
}
