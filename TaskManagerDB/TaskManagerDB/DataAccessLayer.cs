using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDB
{
    public class DataAccessLayer
    {
        SqlConnection con;
        string strCon = "Data Source=.;Initial Catalog=STORAGEDEVICE;Integrated Security=SSPI";
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

        // User methods
        public List<UserDTO> ListUsers()
        {
            List<UserDTO> ListUsers = new List<UserDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM USERS";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                long UserId = dr.GetInt64(dr.GetOrdinal("UserID"));
                string Name = dr.GetString(dr.GetOrdinal("Name"));
                string Dept = dr.GetString(dr.GetOrdinal("Dept"));
                long RoleID = dr.GetInt64(dr.GetOrdinal("RoleID"));

                UserDTO user = new UserDTO();
                user.UserId = UserId;
                user.Name = Name;
                user.Dept = Dept;
                user.RoleID = RoleID;

                ListUsers.Add(user);
            }

            dr.Close();
            return ListUsers;
        }

        public bool AddUser(UserDTO user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO USERS (Name, Dept, RoleID) VALUES (@Name, @Dept, @RoleID)";
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Dept", user.Dept);
            cmd.Parameters.AddWithValue("@RoleID", user.RoleID);

            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }


        // Project methods
        public List<ProjectDTO> ListProjects()
        {
            List<ProjectDTO> ListProjects = new List<ProjectDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM PROJECTS";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                long ProjID = dr.GetInt64(dr.GetOrdinal("ProjID"));
                string Title = dr.GetString(dr.GetOrdinal("Title"));
                long PM = dr.GetInt64(dr.GetOrdinal("PM"));
                string Status = dr.GetString(dr.GetOrdinal("Status"));

                ProjectDTO project = new ProjectDTO();
                project.ProjID = ProjID;
                project.Title = Title;
                project.PM = PM;
                project.Status = Status;

                ListProjects.Add(project);
            }

            dr.Close();
            return ListProjects;
        }

        public bool AddProject(ProjectDTO project)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO PROJECTS (Title, PM, Status) VALUES (@Title, @PM, @Status)";
            cmd.Parameters.AddWithValue("@Title", project.Title);
            cmd.Parameters.AddWithValue("@PM", project.PM);
            cmd.Parameters.AddWithValue("@Status", project.Status);

            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        // Task methods
        public List<TaskDTO> ListTasks()
        {
            List<TaskDTO> ListTasks = new List<TaskDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM TASKS";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                long TaskID = dr.GetInt64(dr.GetOrdinal("TaskID"));
                string Title = dr.GetString(dr.GetOrdinal("Title"));
                long TaskType = dr.GetInt64(dr.GetOrdinal("TaskType"));
                long ProjID = dr.GetInt64(dr.GetOrdinal("ProjID"));
                long AssignTo = dr.GetInt64(dr.GetOrdinal("AssignTo"));

                TaskDTO task = new TaskDTO();
                task.TaskID = TaskID;
                task.Title = Title;
                task.TaskType = TaskType;
                task.ProjID = ProjID;
                task.AssignTo = AssignTo;

                ListTasks.Add(task);
            }

            dr.Close();
            return ListTasks;
        }

        public bool AddTask(TaskDTO task)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"SELECT COUNT(*) FROM PROJECTS WHERE ProjID = @ProjID";
            cmd.Parameters.AddWithValue("@ProjID", task.ProjID);
            int projectCount = (int)cmd.ExecuteScalar();

            if (projectCount == 0)
            {
                throw new Exception("The specified project does not exist.");
            }

            cmd.CommandText = $"INSERT INTO TASKS (Title, TaskType, ProjID, AssignTo) VALUES (@Title, @TaskType, @ProjID, @AssignTo)";
            cmd.Parameters.AddWithValue("@Title", task.Title);
            cmd.Parameters.AddWithValue("@TaskType", task.TaskType);
            cmd.Parameters.AddWithValue("@ProjID", task.ProjID);
            cmd.Parameters.AddWithValue("@AssignTo", task.AssignTo);

            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        // Comment methods
        public List<CommentDTO> ListComments()
        {
            List<CommentDTO> ListComments = new List<CommentDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM COMMENTS";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                long CommentID = dr.GetInt64(dr.GetOrdinal("CommentID"));
                string Title = dr.GetString(dr.GetOrdinal("Title"));
                string CommentText = dr.GetString(dr.GetOrdinal("CommentText"));
                long TaskID = dr.GetInt64(dr.GetOrdinal("TaskID"));
                long CommentedBy = dr.GetInt64(dr.GetOrdinal("CommentedBy"));

                CommentDTO comment = new CommentDTO();
                comment.CommentID = CommentID;
                comment.Title = Title;
                comment.CommentText = CommentText;
                comment.TaskID = TaskID;
                comment.CommentedBy = CommentedBy;

                ListComments.Add(comment);
            }

            dr.Close();
            return ListComments;
        }

        public bool AddComment(CommentDTO comment)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"SELECT COUNT(*) FROM TASKS WHERE TaskID = @TaskID";
            cmd.Parameters.AddWithValue("@TaskID", comment.TaskID);
            int taskCount = (int)cmd.ExecuteScalar();

            if (taskCount == 0)
            {
                throw new Exception("The specified task does not exist.");
            }

            cmd.CommandText = $"INSERT INTO COMMENTS (Title, CommentText, TaskID, CommentedBy) VALUES (@Title, @CommentText, @TaskID, @CommentedBy)";
            cmd.Parameters.AddWithValue("@Title", comment.Title);
            cmd.Parameters.AddWithValue("@CommentText", comment.CommentText);
            cmd.Parameters.AddWithValue("@TaskID", comment.TaskID);
            cmd.Parameters.AddWithValue("@CommentedBy", comment.CommentedBy);

            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        // Additional methods for Developer and QAAnalyst roles
        public bool AssignTaskToQA(TaskDTO task)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE TASKS SET AssignTo = @AssignTo WHERE TaskID = @TaskID";
            cmd.Parameters.AddWithValue("@AssignTo", task.AssignTo);
            cmd.Parameters.AddWithValue("@TaskID", task.TaskID);

            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }

        public bool ReopenOrCloseTask(TaskDTO task)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE TASKS SET Status = @Status WHERE TaskID = @TaskID";
            cmd.Parameters.AddWithValue("@Status", task.Status);
            cmd.Parameters.AddWithValue("@TaskID", task.TaskID);

            int RowsEffected = cmd.ExecuteNonQuery();
            return RowsEffected > 0;
        }
    }
}
