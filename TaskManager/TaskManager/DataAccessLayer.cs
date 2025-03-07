using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class DataAccessLayer
    {
        SqlConnection con;
        string strCon = "Data Source=.;Initial Catalog=MTMDb;Integrated Security=SSPI";

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

        public bool AddUser(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId,email,password) VALUES('{inp.Name}','{inp.Department}',{inp.RoleId},'{inp.Email}',{inp.Password}')";
            int RowsEffected = cmd.ExecuteNonQuery();//Executing command
            if (RowsEffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }

        public bool AddProject(ProjectDTO proj)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO PROJECTS(title,PM,Status) VALUES('{proj.Title}',{proj.ProjManager},{proj.Status})";
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

        public bool AssignProj(ProjAssToDTO proj)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO PROJASSTO(projid,userid) VALUES({proj.ProjId},{proj.Userid})";
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

        public bool AddTask(TaskDTO tsk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO TASKS(title,TskType,ProjectId,AssignTo) VALUES('{tsk.Title}',{tsk.TaskType},{tsk.ProjId},{tsk.AssignedTo})";
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

        public List<TaskDTO> ListTasks()
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM TASKS";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long TaskId = reader.GetInt64(0);
                string Title = reader.GetString(1);
                long TaskType = reader.GetInt64(2);
                long ProjId = reader.GetInt64(3);
                long AssignedTo = reader.GetInt64(3);
                string Status = reader.GetString(4);

                TaskDTO tsk = new TaskDTO();
                tsk.TaskId = TaskId;
                tsk.Title = Title;
                tsk.TaskType = TaskType;
                tsk.ProjId = ProjId;
                tsk.AssignedTo = AssignedTo;
                tsk.Status = Status;

                tasksList.Add(tsk);
            }
            reader.Close();
            return tasksList;

        }

        public List<TaskDTO> DevTasks(long usrid)
        {
            List<TaskDTO> devTasks = new List<TaskDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM TASKS WHERE ASSIGNEDTO={usrid}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long TaskId = reader.GetInt64(0);
                string Title = reader.GetString(1);
                long TaskType = reader.GetInt64(2);
                long ProjId = reader.GetInt64(3);
                long AssignedTo = reader.GetInt64(3);
                string Status = reader.GetString(4);

                TaskDTO tsk = new TaskDTO();
                tsk.TaskId = TaskId;
                tsk.Title = Title;
                tsk.TaskType = TaskType;
                tsk.ProjId = ProjId;
                tsk.AssignedTo = AssignedTo;
                tsk.Status = Status;

                devTasks.Add(tsk);
            }
            reader.Close();
            return devTasks;

        }

        public bool AddComment(CommentDTO comm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO COMMENTS(title,commtext,taskId,commBy) VALUES('{comm.Title}',{comm.CommText},{comm.TaskId},{comm.CommentedBy})";
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

        public bool PMUpdateTask(TaskDTO tsk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"UPDATE TASKS SET STATUS={tsk.Status} WHERE TASKID={tsk.TaskId}";
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

        public bool DevUpdateTask(TaskDTO tsk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            string stat = "QA";
            cmd.CommandText = $"UPDATE TASKS SET STATUS={stat}, ASSIGNEDTO={tsk.AssignedTo} WHERE TASKID={tsk.TaskId}";
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

        public bool QAUpdateTask(TaskDTO tsk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            string stat = "Close";
            cmd.CommandText = $"UPDATE TASKS SET STATUS={stat} WHERE TASKID={tsk.TaskId}";
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

    }
}
