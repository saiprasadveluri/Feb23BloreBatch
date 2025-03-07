using System;
using System.Collections.Generic;

namespace TaskManagerADO
{
    internal class Program
    {
        //static DataAccessLayer dal = new DataAccessLayer();
        static Businesslayer business = new Businesslayer();
        static void Main(string[] args)
        {
            try
            {
                
                //dal.OpenConnection();
                Console.Write("Email: ");
                string email=Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();//GetPassword();
                UserDTO usr = business.AuthenticateUser(email, password.Trim());
                if (usr != null)
                {
                    Console.WriteLine($"\nWelcome! {usr.Name}");
                }
                else
                {
                    Console.WriteLine("\nNot Authorized to access the application");
                }
                //Add New User

                Console.WriteLine("1 - List Users 2- Add Users");
                int opts=int.Parse(Console.ReadLine());
                switch(opts)
                {
                    case 1:
                        //List<UserDTO> users=dal.ListUsers();
                        //Console.WriteLine($"User Count: {users.Count}");
                      break;
                        case 2:
                        string Name = Console.ReadLine();
                        string dept = Console.ReadLine();
                        long RId = long.Parse(Console.ReadLine());
                        string eml = Console.ReadLine();
                        string pwd = Console.ReadLine();

                        UserDTO usrObj = new UserDTO();
                        usrObj.Name = Name;
                        usrObj.Department = dept;
                        usrObj.RoleId = RId;
                        usrObj.Email = eml;
                        usrObj.Password = pwd;
                        bool status=business.AddUser(usrObj);
                        if(status)
                        {
                            Console.WriteLine("Success in Adding user");
                        }
                        else
                        {
                            Console.WriteLine("Error In operation");
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong option");
                        break;
                }
                

                //DataAccessLayerDC dalObj= new DataAccessLayerDC();

                //List<UserDTO> userList=dalObj.ListUsers();

                //Console.WriteLine("Users List: ");
                //foreach (UserDTO user in userList)
                //{
                //    Console.WriteLine(user.Name);
                //}
                //

                // dalObj.UpdateUserRole(5, 3);
                //string usrXml=dalObj.GetUserJson();
                //Console.WriteLine(usrXml);
            }
            finally
            {
                business.CloseApp();
            }
            //    DataAccessLayer dal = new DataAccessLayer();
            //    dal.OpenConnection();
            //    List<UserDTO> lst = dal.ListUsers();
            //    //List<UserDTO> olist=lst.OrderBy(u=>u.Name).ToList();//LINQ query
            //    foreach (UserDTO user in lst)
            //    {
            //        Console.WriteLine(user.Name);
            //    }
            //    //User Input
            //    Console.WriteLine("Name: ");
            //    string name = Console.ReadLine();

                //    Console.WriteLine("Dept: ");
                //    string dept = Console.ReadLine();

                //    Console.WriteLine("RoleId: ");
                //    int roleid = int.Parse(Console.ReadLine());

                //    UserDTO newUser = new UserDTO();
                //    newUser.Name = name;
                //    newUser.Department = dept;
                //    newUser.RoleId = roleid;

                //    bool Success = dal.AddUser(newUser);
                //    if (Success)
                //    {
                //        Console.WriteLine("User added...");
                //    }
                //    else
                //    {
                //        Console.WriteLine("Failed to add user");
                //    }

                //    dal.CloseConnection();
                //}
                //catch(Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                //string strCon = "Data Source=.;Initial Catalog=MTMDb;Integrated Security=SSPI";//Connection string.
                //con=new SqlConnection(strCon);
                //con.Open();
                //Console.WriteLine("Connection Open SUCCESS...");
                ////Execute Commands....
                ////1. Multiple Rows: Select * from Users

                ////3. No Retun: Insert Into ....
                ///*GetUserCount();
                //AddUser();
                //Console.WriteLine("After Adding New User: ");
                //GetUserCount();*/
                //ListUsers();
                ////AddProject();
                //ProjectsByPM();
                //con.Close();
        }

        /*public static void ListUsers()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM USERS";

            //MULTIPLE NUMBER OF ROWS
            SqlDataReader reader=cmd.ExecuteReader();
            
            //Seq, Forw, Readonly access to result set
            while (reader.Read())//Progress One row at a time.
            {
                long UserId = reader.GetInt64(0);
                string Name= reader.GetString(1);
                string Dept= reader.GetString(2);
                long RoleId= reader.GetInt64(3);
                Console.WriteLine($"User Id: {UserId} - Name: {Name} - Department: {Dept} - RoleId: {RoleId}");                
            }
            reader.Close();
        }
        public static void AddUser()
        {
            //User Input
            Console.WriteLine("Name: ");
            string name=Console.ReadLine();

            Console.WriteLine("Dept: ");
            string dept = Console.ReadLine();

            Console.WriteLine("RoleId: ");
            int roleid = int.Parse(Console.ReadLine());

            //Command Object
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId) VALUES('{name}','{dept}',{roleid})";

            //Executing Command
            int RowsEffected=cmd.ExecuteNonQuery();
            Console.WriteLine("User Added...");

        }
        public static void GetUserCount()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;//Pass Open Connection Object
            cmd.CommandText = "Select Count(*) from Users";
            //2. Scalar Value :
            object res = cmd.ExecuteScalar();

            int UserCount = Convert.ToInt32(res);
            Console.WriteLine($"User Count: {UserCount}");
        }

        //Add New Project
        public static void AddProject()
        {
            Console.WriteLine("Add New Project");
            Console.WriteLine("Project Title:");
            string title=Console.ReadLine();
            Console.WriteLine("Enter PM Id");
            long PMID=long.Parse(Console.ReadLine());
            Console.WriteLine("Project Status:");
            string ProjStatus=Console.ReadLine();
            
            SqlCommand cmd=new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO PROJECTS(Title,PM,Status) VALUES('{title}',{PMID},'{ProjStatus}')";
            int RowsEfected=cmd.ExecuteNonQuery();
            Console.WriteLine("New Project Added");
        }

        public static void ProjectsByPM()
        {
            Console.WriteLine("Enter PM ID:");
            long PmId=long.Parse(Console.ReadLine());

            SqlCommand cmd=new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM PROJECTS WHERE PM=@PId";//Prepared Statements.
            SqlParameter p1 = new SqlParameter("@PId", PmId);            
            cmd.Parameters.Add(p1);

            SqlDataReader reader= cmd.ExecuteReader();
            
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    string ProjTitle = reader.GetString(1);
                    string ProjStatus = reader.GetString(3);
                    Console.WriteLine($"Project Title: {ProjTitle} - Status: {ProjStatus}");
                }
            }
            else
            {
                Console.WriteLine("No Projects assigned...");
            }
            reader.Close();
        }*/
        
        public static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass.Substring(0, pass.Length-1);
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
    }
}
