using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    public class DataAccess
    {
        SqlConnection con;
        string strcon = "Data Source=.;Initial Catalog=tmdb;Integrated Security=SSPI";
        public DataAccess()
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
        public List<UserDTO> Listusers()
        {
            List<UserDTO> usersList = new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from users";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())//progress one row at a time
            {
                long Userid = reader.GetInt64(0);
                string Name = reader.GetString(1);
                string Dept = reader.GetString(2);
                long Roleid = reader.GetInt64(3);
                string Email = reader.GetString(4);
                UserDTO usr = new UserDTO();
                usr.UserId = Userid;
                usr.Name = Name;
                usr.Department = Dept;
                usr.RoleId = Roleid;
                usr.Email = Email;
                usersList.Add(usr);
            }
            reader.Close();
            return usersList;
        }
        public bool AddUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into users(Name,Dept,Roleid) values('{user.Name}','{user.Department}',{user.RoleId},'{user.Email}','{user.Password}')";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                return true;
            else
                return false;
        }
        public List<ProjectDTO> Listprojects()
        {
            List<ProjectDTO> projectsList = new List<ProjectDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from projects";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())//progress one row at a time
            {
                long Projectid = reader.GetInt64(0);
                string Projecttitle = reader.GetString(1);
                long PMid = reader.GetInt64(2);
                string status = reader.GetString(3);
                ProjectDTO prj = new ProjectDTO();
                prj.Projectid = Projectid;
                prj.Projecttitle = Projecttitle;
                prj.PMid = PMid;
                prj.status = status;
                projectsList.Add(prj);
            }
            reader.Close();
            return projectsList;
        }
        public bool AddProject(ProjectDTO project)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into projects(Title,PM,status) values('{project.Projecttitle}',{project.PMid},'{project.status}')";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                return true;
            else
                return false;
        }
        public List<TaskDTO> ListTasks()
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Tasks";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long Taskid = reader.GetInt64(0);
                string title = reader.GetString(1);
                int Tasktype = reader.GetInt32(2);
                long Projectid = reader.GetInt64(3);
                long assignedto = reader.GetInt64(4);
                TaskDTO tsk = new TaskDTO();
                tsk.Taskid = Taskid;
                tsk.title = title;
                tsk.Tasktype = Tasktype;
                tsk.Projectid = Projectid;
                tsk.assignedto = assignedto;
                tasksList.Add(tsk);
            }
            reader.Close();
            return tasksList;



        }
        public bool AddTask(TaskDTO task)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into tasks(Title,Tasktype,Projid,Assignedto) values('{task.title}',{task.Tasktype},{task.Projectid},{task.assignedto})";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                return true;
            else
                return false;
        }
        public bool UpdateUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE users SET Name = '{user.Name}', Dept = '{user.Department}', Roleid = {user.RoleId} WHERE UserId = {user.UserId}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0) return true;
            else return false;
        }

        public bool DeleteUser(long userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM users WHERE UserId = {userId}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0) return true;
            else return false;
        }
        public bool UpdateProject(ProjectDTO project)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE projects SET Title = '{project.Projecttitle}', PM = {project.PMid}, status = '{project.status}' WHERE Projectid = {project.Projectid}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0) return true;
            else return false;
        }

        public bool DeleteProject(long projectId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM projects WHERE Projectid = {projectId}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0) return true;
            else return false;
        }

        public bool UpdateTask(TaskDTO task)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE tasks SET Title = '{task.title}', Tasktype = {task.Tasktype}, Projid = {task.Projectid}, Assignedto = {task.assignedto} WHERE Taskid = {task.Taskid}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0) return true;
            else return false;
        }

        public bool DeleteTask(long taskId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM tasks WHERE Taskid = {taskId}";
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0) return true;
            else return false;
        }
        public bool AddComment(CommentsDTO comment)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into comments(Title, Commenttext, Taskid, Commentedby) values('{comment.Title}', '{comment.Commenttext}', {comment.Taskid}, {comment.Commentedby})";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool UpdateComment(CommentsDTO comment)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE comments SET Title = '{comment.Title}', Commenttext = '{comment.Commenttext}', Taskid = {comment.Taskid}, Commentedby = {comment.Commentedby} WHERE Commentid = {comment.Commentid}";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool DeleteComment(long commentId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM comments WHERE Commentid = {commentId}";
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        public List<CommentsDTO> DisplayComments()
        {
            List<CommentsDTO> commentsList = new List<CommentsDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from comments";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long Commentid = reader.GetInt64(0);
                string Title = reader.GetString(1);
                string Commenttext = reader.GetString(2);
                long Taskid = reader.GetInt64(3);
                long Commentedby = reader.GetInt64(4);
                CommentsDTO comment = new CommentsDTO();
                comment.Commentid = Commentid;
                comment.Title = Title;
                comment.Commenttext = Commenttext;
                comment.Taskid = Taskid;
                comment.Commentedby = Commentedby;
                commentsList.Add(comment);
            }
            reader.Close();
            return commentsList;
        }
        public UserDTO Login(string Email, string Password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"select * from users where Email = '{Email}' and Password = '{Password}'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDTO user = new UserDTO();
                user.UserId = reader.GetInt64(0);
                user.Name = reader.GetString(1);
                user.Department = reader.GetString(2);
                user.RoleId = reader.GetInt64(3);
                user.Email = reader.GetString(4);
                user.Password = reader.GetString(5);
                return user;
            }
            else
            {
                reader.Close();
                return null;
            }
        }
    }
}
