using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerADO;

namespace TaskManagerADO
{ 
class Program
{
    static SqlConnection con;
    static void Main(string[] args)
    {
        try
        {
            DataAccessLayer dal = new DataAccessLayer();
                dal.OpenConnection();
                List<UserDTO> lst = dal.ListUsers();
            List<UserDTO> olist = lst.OrderBy(u => u.Name).ToList();
            
            foreach (UserDTO user in lst)
            {
                Console.WriteLine(user.Name);
            }
             Console.WriteLine("Enter user details");
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Dept: ");
            string dept = Console.ReadLine();
            Console.WriteLine("RoleId: ");
            int roleid = int.Parse(Console.ReadLine());
            UserDTO newUser = new UserDTO();
            newUser.Name = name;
            newUser.Department = dept;
            newUser.RoleId = roleid;
            bool Success = dal.AddUser(newUser);
            if (Success)
            {
                Console.WriteLine("User added");
            }
            else
            {
                Console.WriteLine("Failed to add user");
            }

                Console.WriteLine("Add Tasks");
            Console.WriteLine("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Task type: ");
            int tsktype = int.Parse(Console.ReadLine());
            Console.WriteLine("Project Id: ");
            int projid = int.Parse(Console.ReadLine());
            Console.WriteLine("Assigned to: ");
            int assignedTo = int.Parse(Console.ReadLine());
            TaskDTO newTask = new TaskDTO();
            newTask.Title = title;
            newTask.TaskType = tsktype;
            newTask.ProjId = projid;
            newTask.AssignedTo = assignedTo;
            bool Succes = dal.AddTask(newTask);
            if (Succes)
            {
                Console.WriteLine("Task added");
            }
            else
            {
                Console.WriteLine("Failed to add task");
            }

                Console.WriteLine("Enter project details");
            Console.WriteLine("Title: ");
            string ttle = Console.ReadLine();
            Console.WriteLine("Project Manager: ");
            int pm = int.Parse(Console.ReadLine());
            Console.WriteLine("Status: ");
            string status = Console.ReadLine();
            ProjectDTO newProj = new ProjectDTO();
            newProj.Title = ttle;
            newProj.ProjManager = pm;
            newProj.Status = status;
            bool Succe = dal.AddProject(newProj);
            if (Succe)
            {
                Console.WriteLine("Project added");
            }
            else
            {
                Console.WriteLine("Failed to add project");
            }
            dal.CloseConnection();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        //string strCon = "Data Source=.;Initial Catalog=MTMDb;Integrated Security=SSPI";
        //con = new SqlConnection(strCon);
        //con.Open();
        //Execute commands
        //get rows: Select * from Users
        //Scalar value:Select Name from Users where userid=1/Select Count(*) from Users
        //No return: Insert Into

        //SqlCommand cmd = new SqlCommand();
        //cmd.Connection = con;//Parse open connection obj
        //cmd.CommandText = "Select  Count(*) from Users";
        //object res=cmd.ExecuteScalar();
        //int UserCount = Convert.ToInt32(res);
        //Console.WriteLine($"User Count:{UserCount}");
        //GetUserCount();
        //AddUser();
        //GetUserCount();
        //ListUsers();
        //AddProject();
        //ProjectsByPM();
        //con.Close();
    }

    /*public static void ListUsers()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM USERS";
        SqlDataReader reader=cmd.ExecuteReader();
        //Seq,For,Readonly access to result set
        while(reader.Read())
        {
            long UserId = reader.GetInt64(0);
            string Name = reader.GetString(1);
            string Dept = reader.GetString(2);
            long RoleId = reader.GetInt64(3);
            Console.WriteLine($"User Id:{UserId}-Name:{Name}-Department:{Dept}-RoleId:{RoleId}");
        }
        reader.Close();
    }
     public static void AddUser()
    {
        Console.WriteLine("Name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Dept: ");
        string dept = Console.ReadLine();
        Console.WriteLine("RoleId: ");
        int roleid = int.Parse(Console.ReadLine());

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO USERS(Name,Dept,RoleId) VALUES('{name}','{dept}',{roleid})";
        int RowsEffected=cmd.ExecuteNonQuery();//Executing command
        Console.WriteLine("User added");
    }

    public static void GetUserCount()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;//Parse open connection obj
        cmd.CommandText = "Select  Count(*) from Users";
        object res = cmd.ExecuteScalar();
        int UserCount = Convert.ToInt32(res);
        Console.WriteLine($"User Count:{UserCount}");
    }

    public static void AddProject()
    {
        Console.WriteLine("Add new project");
        Console.WriteLine("Project title:");
        string title = Console.ReadLine();
        Console.WriteLine("Enter PM id:");
        long PMID = long.Parse(Console.ReadLine());
        Console.WriteLine("Project status:");
        string ProjStatus = Console.ReadLine();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO PROJECTS(Title,PM,Status) VALUES('{title}',{PMID},'{ProjStatus}')";
        int RowsEffected = cmd.ExecuteNonQuery();
        Console.WriteLine("New project added");
    }

    public static void ProjectsByPM()
    {
        Console.WriteLine("Enter PM id:");
        long PmId = long.Parse(Console.ReadLine());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM PROJECTS WHERE PM=@PId";// + PmId.ToString();
        SqlParameter p1 = new SqlParameter("@PId", PmId);
        cmd.Parameters.Add(p1);
        SqlDataReader reader=cmd.ExecuteReader();
        if(reader.HasRows)
        {
            while(reader.Read())
            {
                string ProjTitle = reader.GetString(1);
                string ProjStatus = reader.GetString(3);
                Console.WriteLine($"Project title:{ProjTitle}, Status:{ProjStatus}");
            }
        }
        else
        {
            Console.WriteLine("No projects assigned");
        }
    }*/
}
}