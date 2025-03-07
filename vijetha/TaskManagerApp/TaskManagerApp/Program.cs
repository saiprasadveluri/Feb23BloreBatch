using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Businesslayer bl = new Businesslayer();
            bool running = true;

            Console.WriteLine("=== Task Manager Console App ===");

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var user = bl.AuthenticateUser(email, password);

            if (user == null)
            {
                Console.WriteLine("Invalid credentials. Exiting.");
                return;
            }

            Console.WriteLine($"Welcome {user.Name}!");

            while (running)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add User (Site Admin)");
                Console.WriteLine("2. Create Project (Site Admin)");
                Console.WriteLine("3. Create Task (Project Manager)");
                Console.WriteLine("4. Add Comment to Task");
                Console.WriteLine("5. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        if (bl.GetLoggedInUserRole() == 1) // Site Admin
                        {
                            UserDTO newUser = new UserDTO();
                            Console.Write("Enter Name: ");
                            newUser.Name = Console.ReadLine();

                            Console.Write("Enter Department: ");
                            newUser.Department = Console.ReadLine();

                            Console.Write("Enter Role (1-Site Admin, 2-Manager, 3-Developer, 4-QA): ");
                            newUser.RoleId = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Email: ");
                            newUser.Email = Console.ReadLine();

                            Console.Write("Enter Password: ");
                            newUser.Password = Console.ReadLine();

                            bool success = bl.AddUser(newUser);
                            Console.WriteLine(success ? "User added successfully!" : "Failed to add user.");
                        }
                        else
                        {
                            Console.WriteLine("Access Denied! Only Site Admin can add users.");
                        }
                        break;

                    case 2:
                        if (bl.GetLoggedInUserRole() == 1) // Site Admin
                        {
                            ProjectDTO project = new ProjectDTO();
                            Console.Write("Enter Project Title: ");
                            project.Title = Console.ReadLine();

                            Console.Write("Enter Project Status: ");
                            project.Status = Console.ReadLine();

                            Console.Write("Assign Manager ID: ");
                            project.ManagerId = Convert.ToInt64(Console.ReadLine());

                            bool projAdded = bl.CreateProject(project);
                            Console.WriteLine(projAdded ? "Project Created!" : "Failed to create project.");
                        }
                        else
                        {
                            Console.WriteLine("Access Denied! Only Site Admin can create projects.");
                        }
                        break;

                    case 3:
                        if (bl.GetLoggedInUserRole() == 2) // Project Manager
                        {
                            TaskDTO task = new TaskDTO();
                            Console.Write("Enter Task Title: ");
                            task.Title = Console.ReadLine();

                            Console.Write("Enter Task Type (Bug/Feature): ");
                            task.Type = Console.ReadLine();

                            Console.Write("Assign to Developer ID: ");
                            task.AssignedTo = Convert.ToInt64(Console.ReadLine());

                            Console.Write("Enter Project ID: ");
                            task.ProjID = Convert.ToInt64(Console.ReadLine());

                            task.Status = "Open";
                            task.StartDate = DateTime.Now;

                            Console.Write("Estimated Hours: ");
                            task.HoursLogged = float.Parse(Console.ReadLine());

                            bool taskAdded = bl.CreateTask(task);
                            Console.WriteLine(taskAdded ? "Task Created!" : "Failed to create task.");
                        }
                        else
                        {
                            Console.WriteLine("Access Denied! Only Project Manager can create tasks.");
                        }
                        break;

                    case 4:
                        CommentDTO comment = new CommentDTO();
                        Console.Write("Enter Task ID: ");
                        comment.TaskID = Convert.ToInt64(Console.ReadLine());

                        comment.CommentedBy = bl.GetLoggedInUserId();

                        Console.Write("Enter Comment: ");
                        comment.CommentText = Console.ReadLine();

                        bool commentAdded = bl.AddComment(comment);
                        Console.WriteLine(commentAdded ? "Comment Added!" : "Failed to add comment.");
                        break;

                    case 5:
                        bl.CloseApp();
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }

}
