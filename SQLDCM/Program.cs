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


                while (true)
                {

                    Console.WriteLine("1. List Users");
                    Console.WriteLine("2. Add User");
                    Console.WriteLine("3. Update User");
                    Console.WriteLine("4. Delete User");
                    Console.WriteLine("5. List Projects");
                    Console.WriteLine("6. Add Project");
                    Console.WriteLine("7. Update Project");
                    Console.WriteLine("8. Delete Project");
                    Console.WriteLine("9. List Tasks");
                    Console.WriteLine("10. Add Task");
                    Console.WriteLine("11. Update Task");
                    Console.WriteLine("12. Delete Task");
                    Console.WriteLine("13. List Comments");
                    Console.WriteLine("14. Add Comment");
                    Console.WriteLine("15. Update Comment");
                    Console.WriteLine("16. Delete Comment");
                    Console.WriteLine("17. Exit");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            ListUser();
                            break;
                        case 2:
                            AddUser();
                            break;
                        case 3:
                            UpdateUser();
                            break;
                        case 4:
                            DeleteUser();
                            break;
                        case 5:
                            ListProject();
                            break;
                        case 6:
                            AddProject();
                            break;
                        case 7:
                            UpdateProject();
                            break;
                        case 8:
                            DeleteProject();
                            break;
                        case 9:
                            ListTask();
                            break;
                        case 10:
                            AddTask();
                            break;
                        case 11:
                           // UpdateTask();
                            break;
                        case 12:
                            DeleteTask();
                            break;
                        case 13:
                            ListComment();
                            break;
                        case 14:
                            AddComment();
                            break;
                        case 15:
                           // UpdateComment();
                            break;
                        case 16:
                            DeleteComment();
                            break;
                        case 17:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid Choice!");
                            break;
                    }

                }




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
             sur.UName= Console.ReadLine();

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
        public static void AddProject()
        {
            ProjectDTO project = new ProjectDTO();
            Console.WriteLine("Project Name: ");
            project.ProjectName = Console.ReadLine();

            Console.WriteLine("Project Manager Id: ");
            project.ProjectManagerId = Convert.ToInt64(Console.ReadLine());


            Console.WriteLine("Project Status ");
            project.PStatus = Console.ReadLine();

            bool add = da.AddProject(project);
            if (add)
            {
                Console.WriteLine("Project Added Successfully!");
            }
            else
            {
                Console.WriteLine("Project not added!");
            }

        }

        public static void ListProject()
        {
            List<ProjectDTO> ProjectList = da.ListProjects();
            foreach (ProjectDTO project in ProjectList)
            {
                Console.WriteLine($"Project Id: {project.ProjectId} - Project Name: {project.ProjectName} - Project Manager Id: {project.ProjectManagerId} - Project Status: {project.PStatus}");
            }
        }

        public static void UpdateProject()
        {
            Console.WriteLine("Updating Project Status: ");
            Console.WriteLine("Enter Project Id: ");
            long proid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Status To Update: ");
            string stat = Console.ReadLine();
            da.UpdateProjStatus(proid, stat);
        }


        public static void DeleteProject()
        {
            Console.WriteLine("Enter Project Id To Delete: ");
            long id = Convert.ToInt64(Console.ReadLine());
            bool delete = da.DeleteProject(id);
            if (delete)
            {
                Console.WriteLine("Project Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Project not deleted!");
            }
        }


        public static void AddTask()
        {
            TaskDTO task = new TaskDTO();
            Console.WriteLine("Task Title: ");
            task.Title = Console.ReadLine();
            Console.WriteLine("Task Type: ");
            task.TType = int.Parse(Console.ReadLine());
            Console.WriteLine("Project Id: ");
            task.ProjectId = Convert.ToInt64(Console.ReadLine());


            Console.WriteLine("Assigned To: ");
            task.AssignedTo = Convert.ToInt64(Console.ReadLine());


            bool add = da.AddTask(task);
            if (add)
            {
                Console.WriteLine("Task Added Successfully!");
            }
            else
            {
                Console.WriteLine("Task not added!");
            }

        }

        public static void ListTask()
        {
            List<TaskDTO> TaskList = da.ListTasks();
            foreach (TaskDTO task in TaskList)
            {
                Console.WriteLine($"Task Id: {task.TaskId} - Task Title: {task.Title} - Task Type: {task.TType} - Project Id: {task.ProjectId} - Assigned To: {task.AssignedTo}");
            }
        }

        public static void DeleteTask()
        {
            Console.WriteLine("Enter Task Id To Delete: ");
            long id = Convert.ToInt64(Console.ReadLine());
            bool delete = da.DeleteTask(id);
            if (delete)
            {
                Console.WriteLine("Task Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Task not deleted!");
            }
        }


        public static void AddComment()
        {
            CommentDTO comment = new CommentDTO();
            Console.WriteLine("Title: ");
            comment.Title = Console.ReadLine();
            Console.WriteLine("Comment Text: ");
            comment.CommentText = Console.ReadLine();
            Console.WriteLine("Task Id: ");
            comment.TaskId = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Commented By: ");
            comment.CommentedBy = Convert.ToInt64(Console.ReadLine());

            bool add = da.AddComment(comment);
            if (add)
            {
                Console.WriteLine("Comment Added Successfully!");
            }
            else
            {
                Console.WriteLine("Comment not added!");
            }
        }

        public static void ListComment()
        {
            List<CommentDTO> CommentList = da.ListComments();
            foreach (CommentDTO comment in CommentList)
            {
                Console.WriteLine($"Comment Id: {comment.CommentId} - Title: {comment.Title} - Comment Text: {comment.CommentText} - Task Id: {comment.TaskId} - Commented By: {comment.CommentedBy}");
            }
        }

        public static void DeleteComment()
        {
            Console.WriteLine("Enter Comment Id To Delete: ");
            long id = Convert.ToInt64(Console.ReadLine());
            bool delete = da.DeleteComment(id);
            if (delete)
            {
                Console.WriteLine("Comment Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Comment not deleted!");
            }
        }


    }
}
