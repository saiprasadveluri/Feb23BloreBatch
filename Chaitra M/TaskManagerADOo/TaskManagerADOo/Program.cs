using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Data.SqlClient;

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

                //List<UserDTO> olist = lst.OrderBy(u => u.Name).ToString();

                List<UserDTO> olist = lst.OrderBy(u => u.Name).ToList();

                foreach (UserDTO user in lst)

                {

                    Console.WriteLine($"userid:{user.UserId}-name:{user.Name}- Department:{user.Dept}");

                }

                Console.WriteLine("Enter User Name:");

                string Name = Console.ReadLine();

                Console.WriteLine("Enter Dept:");

                string Dept = Console.ReadLine();

                Console.WriteLine("Enter Roleid:");

                int Roleid = Convert.ToInt32(Console.ReadLine());

                UserDTO user1 = new UserDTO();

                user1.Name = Name;

                user1.Dept = Dept;

                user1.Roleid = Roleid;

                bool res = dal.AddUser(user1);

                if (res)

                {

                    Console.WriteLine("User Added");

                }

                else

                {

                    Console.WriteLine("User not Added");

                }

                dal.CloseConnection();

            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.Message);

            }

            Console.WriteLine("Creation of Project");

            DataAccessLayer dal1 = new DataAccessLayer();

            dal1.OpenConnection();

            //dal1.CreateProject();

            dal1.CloseConnection();

            //string strcon = "Data Source=.;Initial Catalog=TaskManagerDemo;Integrated Security=SSPI";

            //con = new SqlConnection(strcon);

            //con.Open();

            //execute commands

            //1.mutiple Rows:Select*from users

            //2.Get Single or Scalar value:Select count(*) from users or select name from users where id=1

            //NoReturn:Insert Into....

            // SqlCommand cmd = new SqlCommand();

            // cmd.Connection = con;//pass open connection object

            // cmd.CommandText = "Select count(*) from Users";

            //object res= cmd.ExecuteScalar();

            // int UserCount = Convert.ToInt32(res);

            // Console.WriteLine($"Total Users:{UserCount}");


            //Console.WriteLine("Connection Open success");

            //AddUser();

            //GetUserCount();

            //Console.WriteLine("after adding new users");

            //GetUserCount();

            //AddProject();

            //ListUsers();

            ///ProjectByPm();

            // con.Close();

        }

        //public static void ListUsers()

        //{

        //    SqlCommand cmd = new SqlCommand();

        //    cmd.Connection = con; // pass open connection object

        //    cmd.CommandText = "Select * from Users";

        //    // multiple rows

        //    SqlDataReader dr = cmd.ExecuteReader();

        //    // sequential, from, Readonly access to result set

        //    while (dr.Read()) // progress one row at a time

        //    {

        //        long UserId = dr.GetInt64(0);

        //        string Name = dr.GetString(1);

        //        string Dept = dr.GetString(2);

        //        long Roleid = dr.GetInt64(3);

        //        Console.WriteLine($"userid:{UserId}-name:{Name}- Department:{Dept}- Roleid:{Roleid}");

        //        Console.WriteLine($"{dr["UserId"]}\t{dr["Name"]}\t{dr["Dept"]}\t{dr["Roleid"]}");

        //    }

        //    dr.Close();

        //}

        //public static void AddUser()

        //{

        //    //userInput

        //    Console.WriteLine("Enter User Name:");

        //    string Name = Console.ReadLine();

        //    Console.WriteLine("Enter Dept:");

        //    string Dept = Console.ReadLine();

        //    Console.WriteLine("Enter Roleid:");

        //    int Roleid = Convert.ToInt32(Console.ReadLine());

        //    //Command Object

        //    SqlCommand cmd = new SqlCommand();

        //    cmd.Connection = con;//pass open connection object

        //    cmd.CommandText = $"Insert into Users(Name,Dept,Roleid) values('{Name}','{Dept}',{Roleid})"; //query

        //    //executing command

        //    int RowsEffected = cmd.ExecuteNonQuery();

        //    Console.WriteLine($"user added");

        //}

        public static void GetUserCount()

        {

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;//pass open connection object

            cmd.CommandText = "Select count(*) from Users";

            object res = cmd.ExecuteScalar();

            int UserCount = Convert.ToInt32(res);

            Console.WriteLine($"Total Users:{UserCount}");


            Console.WriteLine("Connection Open success");


        }

        public static void AddProject()

        {

            Console.WriteLine("Add New Project");

            Console.WriteLine("Enter Project Name:");

            string Title = Console.ReadLine();

            Console.WriteLine("Enter PM id:");

            long Pm = long.Parse(Console.ReadLine());

            Console.WriteLine("project status:");

            string Status = Console.ReadLine();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;//pass open connection object

            cmd.CommandText = $"Insert into project(Title,Pm,Status) values('{Title}',{Pm},'{Status}')"; //query

            int RowsEffected = cmd.ExecuteNonQuery();

            Console.WriteLine($" new Project added");

        }

        public static void ProjectByPm()

        {

            Console.WriteLine("enter pm id");

            long pm = long.Parse(Console.ReadLine());

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;//pass open connection object

            cmd.CommandText = $"Select * from project where Pm=@PID";//query(parameters are used instead of concatenation) {+pm.TOSTRING()(leads to sql injection)}

            SqlParameter p = new SqlParameter("@PID", pm);

            cmd.Parameters.Add(p);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)

            {

                while (dr.Read())//progress one row at a time

                {

                    string Title = dr.GetString(1);

                    string Status = dr.GetString(3);

                    Console.WriteLine($"Title:{Title}- Status:{Status}");

                }

                dr.Close();

            }

            else

            {

                Console.WriteLine("No Projects assigned");

            }

            dr.Close();

        }

    }

}
 




