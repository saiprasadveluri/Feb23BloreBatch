//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TaskMangerADO;

//namespace TodaysProg
//{
//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                DataAccessLayer daLobj = new DataAccessLayer();
//                List<UserDTO> users = daLobj.ListUsers();
//                Console.WriteLine("Users List: ");
//                foreach (UserDTO user in users)
//                {
//                    Console.WriteLine(user.Name);
//                }

//                Console.WriteLine("1-List Users 2- Add Users");
//                int opts = int.Parse(Console.ReadLine());
//                switch(opts)
//                {
//                    case 1:
//                        List<UserDTO> users = daLobj.ListUsers();
//                        Console.WriteLine
//                }

//                string Name = Console.ReadLine();
//                string dept = Console.ReadLine();
//                long RID = long.Parse(Console.ReadLine());
//                string eml = Console.ReadLine();
//                string pwd = Console.ReadLine();

                
//                UserDTO usrObj = new UserDTO();
//                usrObj.Name = Name;
//                usrObj.Department = dept;
//                usrObj.RoleId = RID;
//                usrObj.Email = eml;
//                usrObj.Password = pwd;
//                daLobj.AddUser(usrObj);
//            }
//            finally
//            {

//            }

//        }


//    }
//}
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Data.SqlClient;
//using System.CodeDom.Compiler;
//using System.Xml.Linq;
//using System.IO;
//using System.Data;

//namespace TaskManager
//{
//    internal class Program
//    {
//        //static SqlConnection con;
//        static void Main(string[] args)
//        {
//            try
//            {
//                DataAcessLayerDC dalObj = new DataAcessLayerDC();
//                List<UserDTO> userList = dalObj.ListUsers();
//                Console.WriteLine("Users List:");
//                foreach (UserDTO user in userList)
//                {
//                    Console.WriteLine(user.Name);
//                }
//            }
//            finally
//            {

//            }
//        }
//   }
//}
//string ConString = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";
//string strCommand1 = "SELECT * FROM USERS";
//DataSet ds = new DataSet();
//SqlDataAdapter adptr = new SqlDataAdapter(strCommand1, ConString);
//adptr.Fill(ds, "Users");
//DataTable dtUsers = ds.Tables["Users"];
//if (dtUsers != null)
//{
//    Console.WriteLine($"User Count:{dtUsers.Rows.Count}");
//    foreach (DataColumn col in dtUsers.Columns)
//    {
//        Console.WriteLine(col.ColumnName);
//    }
//    foreach (DataRow dr in dtUsers.Rows)
//    {
//        Console.WriteLine(dr[0]);
//        Console.WriteLine(dr[1]);
//    }

//    //adding of new row
//    Console.WriteLine("Adding New Row:");
//    Console.WriteLine("Name: ");
//    string name = Console.ReadLine();

//    Console.WriteLine("Dept: ");
//    string dept = Console.ReadLine();

//    Console.WriteLine("RoleId: ");
//    int roleid = int.Parse(Console.ReadLine());

//    DataRow drNewRow = dtUsers.NewRow();
//    drNewRow[1] = name;
//    drNewRow[2] = dept;
//    drNewRow[3] = roleid;
//    dtUsers.Rows.Add(drNewRow);

//    //SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adptr);
//    //adptr.Update(ds,"Users");


//    //upadte row

//    long ReqUserdId = long.Parse(Console.ReadLine());
//    for(int n=0;n<dtUsers.Rows.Count;n++)
//    {
//        object UserIdObjVal=dtUsers.Rows[n][0];
//        long UserId = Convert.ToInt64(UserIdObjVal);
//        if(UserId==ReqUserdId)
//        {
//            dtUsers.Rows[n][1] = "Modified Name";
//            break;
//        }

//    }
//    //SqlCommandBuilder sqlCommandBuilder2 = new SqlCommandBuilder(adptr);
//    //adptr.Update(ds, "Users");

//    //delete row
//    long DelUserId = long.Parse(Console.ReadLine());

//    for (int n = 0; n < dtUsers.Rows.Count; n++)
//    {
//        object UserIdObjVal = dtUsers.Rows[n][0];
//        long UserId = Convert.ToInt64(UserIdObjVal);
//        if (UserId == ReqUserdId)
//        {
//            dtUsers.Rows[n].Delete();
//            break;
//        }

//    }
//    SqlCommandBuilder sqlCommandBuilder2= new SqlCommandBuilder(adptr);
//    adptr.Update(ds, "Users");
//}

//            }
//            finally
//            {

//            }
//        }
//    }
//}

//string strCon = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";//Connection string.
//con = new SqlConnection(strCon);
//con.Open();
//Console.WriteLine("Connection Success...");
//Execute commands...
//1.Get Rows:select * from Users
//2.Scalar Value:select Name from Users where UserId=1/select count(*)from users;
//3.No Return :Insert Into..

//GetUserCount();
//ListProjects();
//AddUser();
//    Console.WriteLine("After adding New User");
//    //GetUserCount();
//    //ListUsers();
//    AddProject();
//    ProjectsByPM();
//    AddTask();
//    con.Close();

//}

//public static void ListUsers()
//{
//    SqlCommand cmd = new SqlCommand();
//    cmd.Connection = con;//Pass Open Connection Object
//    cmd.CommandText = "Select * from Users";

//    //Multiple Number of Rows
//    SqlDataReader reader = cmd.ExecuteReader();
//    //SEQ,Forw,ReadOnly access to result set
//    while (reader.Read())//Progress one row at a time
//    {
//        long UserId = reader.GetInt64(0);
//        string Name = reader.GetString(1);
//        string Dept = reader.GetString(2);
//        long RoleId = reader.GetInt64(3);
//        Console.WriteLine($"User Id:{UserId}-----Name:{Name}-----Dept:{Dept}-----Role Id:{RoleId}");
//    }
//    reader.Close();

//}
//public static void AddUser()
//{
//    Console.WriteLine("Name: ");
//    string name = Console.ReadLine();
//    Console.WriteLine("Dept: ");
//    string dept = Console.ReadLine();
//    //Console.WriteLine("RoleId: ");
//    //int roleid = int.Parse(Console.ReadLine());
//    SqlCommand cmd = new SqlCommand();
//    cmd.Connection = con;
//    cmd.CommandText = $"INSERT INTO USERS(Name,Dept) VALUES ('{name}','{dept}')";
//    int RowsEffected = cmd.ExecuteNonQuery();
//    Console.WriteLine("Users Added");

//}
//public static void GetUserCount()
//{
//    SqlCommand cmd = new SqlCommand();
//    cmd.Connection = con;//Pass Open Connection Object
//    cmd.CommandText = "Select Count(*) from Users";
//    object res = cmd.ExecuteScalar();
//    int UserCount = Convert.ToInt32(res);
//    Console.WriteLine($"UserCount: {UserCount}");


//}

//public static void AddProject()
//{
//    Console.WriteLine("Add new project:");

//    string PM = Console.ReadLine();
//    //Console.WriteLine("Enter PROJID");
//   // int ProjectId = int.Parse(Console.ReadLine());
//    Console.WriteLine("Enter TITLE:");
//    string TITLE = (Console.ReadLine());
//    Console.WriteLine("PM:");
//    int PM = int.Parse(Console.ReadLine());
//    Console.WriteLine("PSTATUS:");
//    string PSTATUS = Console.ReadLine();
//    SqlCommand cmd = new SqlCommand();
//    cmd.Connection = con;
//    cmd.CommandText = $"INSERT INTO PROJECT(PMID,PTITLE,PSTATUS) VALUES ({PM},'{Title}','{PSTATUS}')";
//    int RowsEffected = cmd.ExecuteNonQuery();
//    Console.WriteLine("Project Added");
//}

//public static void ListProjects()
//{
//    SqlCommand cmd = new SqlCommand();
//    cmd.Connection = con;//Pass Open Connection Object
//    cmd.CommandText = "Select * from project";

//    //Multiple Number of Rows
//    SqlDataReader reader = cmd.ExecuteReader();
//    //SEQ,Forw,ReadOnly access to result set
//    while (reader.Read())//Progress one row at a time
//    {
//        long ProjectID = reader.GetInt64(0);
//        long PMID = reader.GetInt64(1);
//        string PTITLE = reader.GetString(2);
//        string PSTATUS = reader.GetString(3);
//        Console.WriteLine($"Project Id:{ProjectID}-----PMID:{PMID}-----Title:{PTITLE}-----Pro Status :{PSTATUS}");
//    }
//    reader.Close();

//}





//public static void ProjectsByPM()
//{
//    Console.WriteLine("Enter PM ID:");
//    long PmId = long.Parse(Console.ReadLine());
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;//Pass Open Connection Object
//            cmd.CommandText = "Select * from Project where PM=@PId"; //+ PmId.ToString(); 
//            SqlParameter p1 = new SqlParameter("@PId", PmId);
//            cmd.Parameters.Add(p1);

//            SqlDataReader reader = cmd.ExecuteReader();
//            if (reader.HasRows)
//            {
//                while (reader.Read())
//                {
//                    string ProjTitle = reader.GetString(1);
//                    string ProjStatus = reader.GetString(2);
//                    Console.WriteLine($"Project Title:{ProjTitle}----Project Status:{ProjStatus}");


//                }
//            }
//            else
//            {
//                Console.WriteLine("No Projects added");
//            }
//        }

//        public static void AddTask()
//        {
//            Console.WriteLine("Add new Task:");
//            //Console.WriteLine("TASKID:");
//            //int TASKID = int.Parse( Console.ReadLine());
//            Console.WriteLine("Enter TASKTITLE:");
//            string TASKTITLE = (Console.ReadLine());
//            Console.WriteLine("TASKTYPE:");
//            int TASKTYPE = int.Parse(Console.ReadLine());
//            Console.WriteLine("PROJID");
//            int PROJID = int.Parse(Console.ReadLine());
//            Console.WriteLine("ASSIGNEDTO");
//            int ASSIGNEDTO = int.Parse(Console.ReadLine());

//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;
//            cmd.CommandText = $"INSERT INTO TASK(TASKTITLE,TASKTYPE,PROJID,ASSIGNEDTO) VALUES ('{TASKTITLE}','{TASKTYPE}',{PROJID},{ASSIGNEDTO})";
//            int RowsEffected = cmd.ExecuteNonQuery();
//            Console.WriteLine("Task Added");
//        }




//    }
//}