using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangerADO
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                DataAccessLayer daLobj = new DataAccessLayer();
                Console.WriteLine("Enter User Name:");
                string userName = Console.ReadLine();

                Console.WriteLine("Enter User Department:");
                string userDept = Console.ReadLine();

                Console.WriteLine("Enter User Role ID:");
                long userRoleId = long.Parse(Console.ReadLine());

                UserDTO user = new UserDTO();
                user.Name = userName;
                user.Department = userDept;
                user.RoleId = userRoleId;

                bool isUserAdded = daLobj.AddUser(user);
                if (isUserAdded)
                {
                    Console.WriteLine("User added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add user.");
                }
                List<UserDTO> users = daLobj.ListUsers();
                Console.WriteLine("User List:");
                foreach (UserDTO user in users)
                {
                    Console.WriteLine($"Username: {user.Name}, Status: {user.Department}, PM: {user.RoleId}");
                }


                Console.WriteLine("Enter User ID to update role:");
                long userIdToUpdate = long.Parse(Console.ReadLine());

                Console.WriteLine("Enter new Role ID:");
                long newRoleId = long.Parse(Console.ReadLine());

                bool isUserRoleUpdated = daLobj.UpdateUserRole(userIdToUpdate, newRoleId);
                if (isUserRoleUpdated)
                {
                    Console.WriteLine("User role updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update user role.");
                }


                Console.WriteLine("Enter User ID to delete:");
                long userIdToDelete = long.Parse(Console.ReadLine());

                bool isUserDeleted = daLobj.DelUserRole(userIdToDelete);
                if (isUserDeleted)
                {
                    Console.WriteLine("User deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete user.");
                }

                Console.WriteLine("Enter Project Title:");
                string Title = Console.ReadLine();

                Console.WriteLine("Enter Project Status:");
                string Status = Console.ReadLine();

                ProjectDTO pro = new ProjectDTO();
                pro.title = Title;
                pro.status = Status;

                bool isAdded = daLobj.AddProject(pro);
                if (isAdded)
                {
                    Console.WriteLine("Project added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add project.");
                }


                List<ProjectDTO> projects = daLobj.ListProjects();
                Console.WriteLine("Projects List:");
                foreach (ProjectDTO project in projects)
                {
                    Console.WriteLine($"Title: {project.title}, Status: {project.status}, PM: {project.PM}");
                }
            }
            finally
            {
            }
        }
    }
}

            //Console.WriteLine("Users List: ");
                //foreach (UserDTO user in users)
                //{
                //    Console.WriteLine(user.Name);
                //}

                //string Name = Console.ReadLine();
                //string dept = Console.ReadLine();
                //long RID = long.Parse(Console.ReadLine());
                //UserDTO usr = new UserDTO();
                //usr.Name = Name;
                //usr.Department = dept;
                //usr.RoleId = RID;
                //daLobj.AddUser(usr);
                //// daLobj.UpdateUserRole(1, 2);
                //daLobj.GetUserXml();
            //    string Title = Console.ReadLine();
            //    string Status = Console.ReadLine();
            //    ProjectDTO pro = new ProjectDTO();
            //    pro.title = Title;
            //    pro.status = Status;

            //    bool isAdded = daLobj.AddProject(pro);
            //    if (isAdded)
            //    {
            //        Console.WriteLine("Project added successfully.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Failed to add project.");
            //    }
            //}
            //string ConString = "Data Source=.;Initial Catalog=MTMDB;Integrated Security=SSPI";
            //string strCommand1 = "SELECT * FROM USERS";
            //DataSet ds = new DataSet();
            //SqlDataAdapter adptr = new SqlDataAdapter(strCommand1, ConString);
            //adptr.Fill(ds, "Users");
            //DataTable dtUsers = ds.Tables["Users"];
            //if (dtUsers != null)
            //{
            //    Console.WriteLine($"Usercount: {dtUsers.Rows.Count}");
            //    foreach (DataColumn col in dtUsers.Columns)
            //    {
            //        Console.WriteLine(col.ColumnName);
            //    }
            //}
            //foreach (DataRow dr in dtUsers.Rows)
            //{
            //    Console.WriteLine(dr[0]);
            //    Console.WriteLine(dr[1]);
            //}

            //Console.WriteLine("Adding New Row: ");
            //Console.WriteLine("Name: ");
            //string Name = Console.ReadLine();
            //Console.WriteLine("Dept:");
            //string dept = Console.ReadLine();
            //Console.WriteLine("Roleid");
            //int roleid = int.Parse(Console.ReadLine());
            //DataRow drNewRow = dtUsers.NewRow();
            //drNewRow[1] = Name;
            //drNewRow[2] = dept;
            //drNewRow[3] = roleid;
            //dtUsers.Rows.Add(drNewRow);
            //SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adptr);
            //adptr.Update(ds, "Users");
            //update rows
            //long ReqUserId = long.Parse(Console.ReadLine());
            //for (int n = 0; n < dtUsers.Rows.Count; n++)
            //{
            //    object UserIdObjVal = dtUsers.Rows[n][0];
            //    long UserId = Convert.ToInt64(UserIdObjVal);
            //    if (UserId == ReqUserId)
            //    {
            //        dtUsers.Rows[n][1] = "Modified Name";
            //        break;
            //    }
            //}
            //SqlCommandBuilder sqlCommandBuilder2 = new SqlCommandBuilder(adptr);
            //adptr.Update(ds, "Users");

            //long DelUserId = long.Parse(Console.ReadLine());
            //var temp = dtUsers.AsEnumerable();
            //var res = (from obj in temp where obj.Field < long>(0) == DelUserId select obj).ToList().FirstOrDefault();
            //if(res!=null)
            //{
            //    res.Delete();

            //}
            //for (int n = 0; n < dtUsers.Rows.Count; n++)
            //{
            //    object UserIdObjVal = dtUsers.Rows[n][0];
            //    long UserId = Convert.ToInt64(UserIdObjVal);
            //    if (UserId == DelUserId)
            //    {
            //        dtUsers.Rows[n].Delete();
            //        break;
            //    }
            //}

            //    SqlCommandBuilder sqlCommandBuilder2 = new SqlCommandBuilder(adptr);
            //    adptr.Update(ds, "Users");
            //}




//            finally
//            {

//            }
//        }
//    }
//}
