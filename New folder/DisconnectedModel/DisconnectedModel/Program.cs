using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQL;

namespace SQL2
{
    internal class Program
    {
        static DataAccessLayerDC da = new DataAccessLayerDC();
        static void Main(string[] args)
        {
            try
            {
                ListProject();
                AddProject();
                ListProject();



            }
            finally
            {

            }
        }
        public static void ListUser()
        {
            List<UserDTO> usr = da.ListUsers();
            foreach (UserDTO u in usr)
            {
                Console.WriteLine($"ID={u.UserId} - NAME={u.UName} - DEPT={u.Dept} - ROLEID={u.RoleId}");
            }
        }
        public static void AddUser()
        {
            UserDTO sur = new UserDTO();
            Console.WriteLine("Adding New Row");
            Console.WriteLine("Name: ");
            sur.UName = Console.ReadLine();
            Console.WriteLine("Dept: ");
            sur.Dept = Console.ReadLine();
            Console.WriteLine("RoleId: ");
            sur.RoleId = long.Parse(Console.ReadLine());
            da.AddUser(sur);
        }

        public static void UpdateUser()
        {
            Console.WriteLine("Updating Role id: ");
            Console.WriteLine("Enter userid: ");
            long useid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter RoleId To Update: ");
            long newrol = long.Parse(Console.ReadLine());
            da.UpdateUserRole(useid, newrol);
        }

        public static void DeleteUser()
        {
            Console.WriteLine("Deleting: ");
            Console.WriteLine("Enter userid to delete: ");
            long useid = long.Parse(Console.ReadLine());
            if (da.DeleteUser(useid))
            {
                Console.WriteLine("Deleted");
            }
            else
            {
                Console.WriteLine("Not Deleted");
            }
        }
        public static void ListProject()
        {
            List<ProjectDTO> prj = da.ListProjects();
            foreach (ProjectDTO p in prj)
            {
                Console.WriteLine($"ID={p.Projectid} - NAME={p.ProjectName} - MANAGERID={p.ProjectManagerid} - STATUS={p.Status}");
            }
        }
        public static void AddProject() {
            ProjectDTO pj = new ProjectDTO();
            Console.WriteLine("Adding New Row");
            Console.WriteLine("Project title: ");
            pj.ProjectName = Console.ReadLine();
            Console.WriteLine("Project Manager ID: ");
            pj.ProjectManagerid =long.Parse(Console.ReadLine());
            Console.WriteLine("Status: ");
            pj.Status = Console.ReadLine();
            da.AddProject(pj);
        }
    }
}