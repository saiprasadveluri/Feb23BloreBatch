using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisconnetedModel
{
    internal class Program
    {
        //static SqlConnection con;
        static void Main(string[] args)
        {
            try
            {
                DAC dac = new DAC();
                List<Project> projects = dac.ListProjects();
                //List<User> users = dac.GetUsers();
                //foreach (User user in users)
                //{
                //    //Console.WriteLine(user.UserId);
                //    Console.WriteLine(user.Name);
                //    //Console.WriteLine(user.Dept);
                //    //Console.WriteLine(user.RoleId);
                //}

                foreach (Project project in projects)
                {
                    Console.WriteLine(project.ProjectName);
                }

                string name = Console.ReadLine();
                long ProjectmanagerId= long.Parse(Console.ReadLine());
                string status = Console.ReadLine();

                Project project1 = new Project();
                project1.ProjectName = name;
                project1.ProjectManagerId = ProjectmanagerId;
                project1.PStatus = status;
                dac.AddProjects(project1);
                Console.WriteLine("project added");
                /*
                //Add new user
                string name = Console.ReadLine();
                string dept = Console.ReadLine();
                long Rid = long.Parse(Console.ReadLine());

                User user1 = new User();
                user1.Name= name;
                user1.Dept= dept;
                user1.RoleId= Rid;
                dac.Addusers(user1);
                Console.WriteLine("user added");
                */
                /*
                //update user
                bool res = dac.UpdateUserRole(1, 10);
                if (res)
                {
                    Console.WriteLine("updted successfully");
                }
                else
                {
                    Console.WriteLine("failed");
                }
                */

                //delete user
                /*bool res = dac.DeleteUser(4);
                if (res)
                {
                    Console.WriteLine("deleted successfully");
                }
                else
                {
                    Console.WriteLine("failed");
                }*/


                //    string conString = @"Data Source=.;Initial Catalog=TaskManager;Integrated Security=SSPI";
                //    string strCommand1 = "select * from Users";
                //    DataSet ds = new DataSet();
                //    SqlDataAdapter da = new SqlDataAdapter(strCommand1, conString);
                //    da.Fill(ds, "Users");
                //    DataTable dt = ds.Tables["Users"];
                //    if (dt != null)
                //    {
                //        Console.WriteLine($"User count: {dt.Rows.Count}");
                //        foreach (DataColumn col in dt.Columns)
                //        {
                //            Console.WriteLine(col.ColumnName);
                //        }
                //        foreach (DataRow row in dt.Rows)
                //        {
                //            Console.WriteLine(row[0]);
                //            Console.WriteLine(row[1]);
                //        }
                /*
                Console.WriteLine("adding new row: ");
                Console.WriteLine("name: ");
                string name = Console.ReadLine();
                Console.WriteLine("dept: ");
                string dept = Console.ReadLine();
                Console.WriteLine("RoleId: ");
                int roleId = Convert.ToInt32(Console.ReadLine());

                DataRow newRow = dt.NewRow();
                newRow[1] = name;
                newRow[2] = dept;
                newRow[3] = roleId;
                dt.Rows.Add(newRow);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.Update(ds, "Users");
                */
                //update rows
                //        Console.WriteLine("Enter the user id to update: ");
                //        long reguserid = long.Parse(Console.ReadLine());
                //        for (int n=0;n<dt.Rows.Count; n++)
                //        {
                //            object UserId = dt.Rows[n][0];
                //            long id = Convert.ToInt64(UserId);
                //            if(id == reguserid)
                //            {
                //                dt.Rows[n][1] = "john";
                //                break;
                //            }
                //        }
                //        SqlCommandBuilder builder1 = new SqlCommandBuilder(da);
                //        da.Update(ds, "Users");
                //    }
                //}
                /*long DeleteUserId = long.Parse(Console.ReadLine());

                    var temp = dt.AsEnumerable();

                    //LINQ to DataSet

                    DataRow res = (from obj in temp where obj.Field<long>(0) == DeleteUserId select obj).ToList().FirstOrDefault();

                    if (res != null)
                    {
                        res.Delete();
                    }
                    //for (int n = 0; n < dt.Rows.Count; n++)
                    //{
                    //    object UserIdObjVal = dt.Rows[n][0];
                    //    long UserId = Convert.ToInt64(UserIdObjVal);

                    //    if (UserId == DeleteUserId)
                    //    {
                    //        dt.Rows[n].Delete();
                    //        Console.WriteLine("Removed");
                    //        break;
                    //    }
                    //}
                    SqlCommandBuilder builder2 = new SqlCommandBuilder(da);
                    da.Update(ds, "Users");*/

            }



            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            finally
            {
                //if (con != null)
                //{
                //    con.Close();
                //}
            }
        }
    }
}
    

