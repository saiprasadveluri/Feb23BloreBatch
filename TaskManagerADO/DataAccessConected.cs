//using Microsoft.Win32;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TaskMangerADO;

//namespace TaskManger
//{
//    class DataAccessLayer
//    {
//        SqlConnection con;
//        string strCon = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";
//        public DataAccessLayer()
//        {
//            con = new SqlConnection(strCon);

//        }
//        public void OpenConnection()
//        {
//            con.Open();
//        }
//        public void CloseConnection()
//        {
//            con.Close();
//        }
//        public List<UserDTO> ListUsers()
//        {
//            List<UserDTO> usersList = new List<UserDTO>();
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;
//            cmd.CommandText = "SELECT * FROM USERS";
//            SqlDataReader reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                long UserId = reader.GetInt64(0);
//                string Name = reader.GetString(1);
//                string Dept = reader.GetString(2);
//                long RoleId = reader.GetInt64(3);
//                UserDTO usr = new UserDTO();
//                usr.UserId = UserId;
//                usr.Name = Name;
//                usr.Department = Dept;
//                usr.RoleId = RoleId;
//                usersList.Add(usr);


//            }
//            reader.Close();
//            return usersList;
//        }
//        public bool AddUser(UserDTO inp)
//        {
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;
//            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId) VALUES ('{inp.Name}','{inp.Department}',{inp.RoleId})";
//            int RowsEffected = cmd.ExecuteNonQuery();
//            Console.WriteLine("Users Added");
//            //SqlCommand cmd = new SqlCommand();
//            ////            cmd.Connection = con;
//            ////            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId) VALUES ('{name}','{dept}',{roleid})";
//            ////           int RowsEffected= cmd.ExecuteNonQuery();
//            ////            Console.WriteLine("Users Added");
//            if (RowsEffected > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        public bool UpdateUser(UserDTO inp)
//        {
//            SqlCommand cmd = new SqlCommand($"UPDATE USERS SET Name='{inp.Name}', Department='{inp.Department}', RoleId={inp.RoleId} WHERE UserId={inp.UserId}", con);
//            return cmd.ExecuteNonQuery() > 0;
//        }
//        public bool DeleteUser(long userId)
//        {
//            SqlCommand cmd = new SqlCommand($"DELETE FROM USERS WHERE UserId={userId}", con);
//            return cmd.ExecuteNonQuery() > 0;
//        }


//        public List<ProjectDTO> ListProjects()
//        {
//            List<ProjectDTO> projectList = new List<ProjectDTO>();
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;
//            cmd.CommandText = "SELECT * FROM PROJECT";
//            SqlDataReader reader = cmd.ExecuteReader();
//            //if (reader.HasRows)
//            //{
//            while (reader.Read())
//            {
//                string ProjTitle = reader.GetString(1);
//                long PMID = reader.GetInt64(2);
//                string ProjStatus = reader.GetString(3);

//                ProjectDTO pro = new ProjectDTO();
//                pro.title = ProjTitle;
//                pro.PMID = PMID;
//                pro.ProjStatus = ProjStatus;
//                projectList.Add(pro);

//            }
//            reader.Close();
//            return projectList;


//        }
//        public bool AddProject(ProjectDTO inp)
//        {
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;
//            cmd.CommandText = $"INSERT INTO PROJECT(Title,PMID,ProjStatus) VALUES ('{inp.title}','{inp.PMID}',{inp.ProjStatus})";
//            int RowsEffected = cmd.ExecuteNonQuery();
//            Console.WriteLine("Project Added");
//            //SqlCommand cmd = new SqlCommand();
//            ////            cmd.Connection = con;
//            ////            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId) VALUES ('{name}','{dept}',{roleid})";
//            ////           int RowsEffected= cmd.ExecuteNonQuery();
//            ////            Console.WriteLine("Users Added");
//            if (RowsEffected > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        public bool UpdateProject(ProjectDTO inp)
//        {
//            SqlCommand cmd = new SqlCommand($"UPDATE PROJECT SET Title='{inp.title}', PMID={inp.PMID}, ProjStatus='{inp.ProjStatus}' WHERE PMID={inp.PMID}", con);
//            return cmd.ExecuteNonQuery() > 0;
//        }
//        public bool DeleteProject(long pmid)
//        {
//            SqlCommand cmd = new SqlCommand($"DELETE FROM PROJECT WHERE PMID={pmid}", con);
//            return cmd.ExecuteNonQuery() > 0;
//        }
//        public bool AddTask(TaskDTO task)
//        {
//            string query = $"INSERT INTO TASKS(TaskName, AssignedTo, ProjectId, Status) VALUES ('{task.TaskName}', '{task.AssignedTo}', {task.ProjectId}, '{task.Status}')";
//            SqlCommand cmd = new SqlCommand(query, con);
//            return cmd.ExecuteNonQuery() > 0;
//        }

//        public List<TaskDTO> ListTasks()
//        {
//            List<TaskDTO> tasks = new List<TaskDTO>();
//            SqlCommand cmd = new SqlCommand("SELECT * FROM TASKS", con);
//            SqlDataReader reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                tasks.Add(new TaskDTO
//                {
//                    TaskId = reader.GetInt64(0),
//                    TaskName = reader.GetString(1),
//                    AssignedTo = reader.GetString(2),
//                    ProjectId = reader.GetInt64(3),
//                    Status = reader.GetString(4)
//                });
//            }
//            reader.Close();
//            return tasks;
//        }
//        public bool UpdateTask(TaskDTO task)
//        {
//            string query = "UPDATE TASKS SET Status = 'Completed' WHERE TaskId = 1";
//            SqlCommand cmd = new SqlCommand(query, con);
//            return cmd.ExecuteNonQuery() > 0;
//        }
//        public bool DeleteTask(TaskDTO task)
//        {
//            string query = "DELETE FROM TASKS WHERE TaskId = 1";
//            SqlCommand cmd = new SqlCommand(query, con);
//            return cmd.ExecuteNonQuery() > 0;
//        }
//        public bool AddComment(CommentDTO comment)
//        {
//            string query = $"INSERT INTO COMMENTS(CommentText, TaskId, UserId) VALUES ('{comment.CommentText}', {comment.TaskId}, {comment.UserId})";
//            SqlCommand cmd = new SqlCommand(query, con);
//            return cmd.ExecuteNonQuery() > 0;
//        }
//        public bool UpdateComment(CommentDTO comment)
//        {
//            string query = "UPDATE COMMENTS SET CommentText = 'Updated comment text' WHERE CommentId = 1";
//            SqlCommand cmd = new SqlCommand(query, con);
//            return cmd.ExecuteNonQuery() > 0;
//        }
//        public bool DeleteComment(CommentDTO comment)
//        {
//            string query = "DELETE FROM COMMENTS WHERE CommentId = 1";
//            SqlCommand cmd = new SqlCommand(query, con);
//            return cmd.ExecuteNonQuery() > 0;
//        }

//        public UserDTO LoginUser(string Email,string Password)
//        {
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;
//            cmd.CommandText = $"SELECT * FROM USERS where Email='{Email}' AND Password='{Password}'";
//            SqlDataReader reader = cmd.ExecuteReader();
//            if(reader.HasRows)
//            {
//                reader.Read();
//                UserDTO user = new UserDTO();
//                user.UserId = reader.GetInt64(0);
//                user.Name= reader.GetString(1);
//                user.Department = reader.GetString(2);
//                user.RoleId = reader.GetInt64(3);
//                user.Email = reader.GetString(4);
//                return user;

//            }
//            reader.Close();
//            return null;
//        }
//    }

//}
