using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TaskManagerADO
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

                UserDTO usr = new UserDTO();
                usr.UserId = UserId;
                usr.Name = Name;
                usr.Department = Dept;
                usr.RoleId = RoleId;

                usersList.Add(usr);
            }
            reader.Close();
            return usersList;
        }

        public bool AddUser(UserDTO inp)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId) VALUES('{inp.Name}','{inp.Department}',{inp.RoleId})";
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

        public bool AddProject(ProjectDTO proj)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO TASKS(title,PM,Status) VALUES('{proj.Title}',{proj.ProjManager},{proj.Status})";
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