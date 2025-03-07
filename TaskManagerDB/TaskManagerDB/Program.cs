using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TaskManagerDB;

namespace TaskManagerADO
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.OpenConnection();

            Console.WriteLine("Enter your role (SiteAdmin, ProjectManager, Developer, QAAnalyst): ");
            string role = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Choose an option:");
                if (role == "SiteAdmin")
                {
                    Console.WriteLine("1. List all users");
                    Console.WriteLine("2. Add a new user");
                    Console.WriteLine("3. List all projects");
                    Console.WriteLine("4. Add a new project");
                }
                else if (role == "ProjectManager")
                {
                    Console.WriteLine("1. List all projects");
                    Console.WriteLine("2. Add a new project");
                    Console.WriteLine("3. List all tasks");
                    Console.WriteLine("4. Add a new task");
                }
                else if (role == "Developer")
                {
                    Console.WriteLine("1. List all tasks");
                    Console.WriteLine("2. Add a new comment");
                    Console.WriteLine("3. Assign task to QA");
                }
                else if (role == "QAAnalyst")
                {
                    Console.WriteLine("1. List all tasks");
                    Console.WriteLine("2. Add a new comment");
                    Console.WriteLine("3. Reopen or close task");
                }
                Console.WriteLine("9. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (role == "SiteAdmin")
                            ListUsers(dal);
                        else if (role == "ProjectManager" || role == "Developer" || role == "QAAnalyst")
                            ListTasks(dal);
                        else if (role == "ProjectManager" || role == "SiteAdmin")
                            ListProjects(dal);
                        break;
                    case "2":
                        if (role == "SiteAdmin")
                            AddUser(dal);
                        else if (role == "ProjectManager" || role == "SiteAdmin")
                            AddProject(dal);
                        else if (role == "Developer" || role == "QAAnalyst")
                            AddComment(dal);
                        break;
                    case "3":
                        if (role == "ProjectManager")
                            ListTasks(dal);
                        else if (role == "Developer")
                            AssignTaskToQA(dal);
                        else if (role == "QAAnalyst")
                            ReopenOrCloseTask(dal);
                        break;
                    case "4":
                        if (role == "ProjectManager")
                            AddTask(dal);
                        break;
                    case "9":
                        dal.CloseConnection();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ListUsers(DataAccessLayer dal)
        {
            List<UserDTO> users = dal.ListUsers();
            foreach (UserDTO user in users)
            {
                Console.WriteLine($"User ID: {user.UserId}, Name: {user.Name}, Dept: {user.Dept}, RoleID: {user.RoleID}");
            }
        }

        static void AddUser(DataAccessLayer dal)
        {
            Console.WriteLine("Enter User Details");
            Console.WriteLine("Name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Dept: ");
            string userDept = Console.ReadLine();
            Console.WriteLine("RoleID: ");
            long userRoleID = long.Parse(Console.ReadLine());

            UserDTO newUser = new UserDTO();
            newUser.Name = userName;
            newUser.Dept = userDept;
            newUser.RoleID = userRoleID;

            bool success = dal.AddUser(newUser);
            if (success)
            {
                Console.WriteLine("User Added Successfully");
            }
            else
            {
                Console.WriteLine("User Not Added");
            }
        }

        static void ListTasks(DataAccessLayer dal)
        {
            List<TaskDTO> tasks = dal.ListTasks();
            foreach (TaskDTO task in tasks)
            {
                Console.WriteLine($"Task ID: {task.TaskID}, Title: {task.Title}, TaskType: {task.TaskType}, ProjID: {task.ProjID}, AssignTo: {task.AssignTo}");
            }
        }

        static void AddTask(DataAccessLayer dal)
        {
            Console.WriteLine("Enter Task Details");
            Console.WriteLine("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("TaskType: ");
            long taskType = long.Parse(Console.ReadLine());
            Console.WriteLine("ProjID: ");
            long projID = long.Parse(Console.ReadLine());
            Console.WriteLine("AssignTo: ");
            long assignTo = long.Parse(Console.ReadLine());

            TaskDTO newTask = new TaskDTO();
            newTask.Title = title;
            newTask.TaskType = taskType;
            newTask.ProjID = projID;
            newTask.AssignTo = assignTo;

            bool success = dal.AddTask(newTask);
            if (success)
            {
                Console.WriteLine("Task Added Successfully");
            }
            else
            {
                Console.WriteLine("Task Not Added");
            }
        }

        static void ListProjects(DataAccessLayer dal)
        {
            List<ProjectDTO> projects = dal.ListProjects();
            foreach (ProjectDTO project in projects)
            {
                Console.WriteLine($"Project ID: {project.ProjID}, Title: {project.Title}, PM: {project.PM}, Status: {project.Status}");
            }
        }

        static void AddProject(DataAccessLayer dal)
        {
            Console.WriteLine("Enter Project Details");
            Console.WriteLine("Title: ");
            string projectTitle = Console.ReadLine();
            Console.WriteLine("PM: ");
            long pm = long.Parse(Console.ReadLine());
            Console.WriteLine("Status: ");
            string projectStatus = Console.ReadLine();

            ProjectDTO newProject = new ProjectDTO();
            newProject.Title = projectTitle;
            newProject.PM = pm;
            newProject.Status = projectStatus;

            bool success = dal.AddProject(newProject);
            if (success)
            {
                Console.WriteLine("Project Added Successfully");
            }
            else
            {
                Console.WriteLine("Project Not Added");
            }
        }

        static void ListComments(DataAccessLayer dal)
        {
            List<CommentDTO> comments = dal.ListComments();
            foreach (CommentDTO comment in comments)
            {
                Console.WriteLine($"Comment ID: {comment.CommentID}, Title: {comment.Title}, CommentText: {comment.CommentText}, TaskID: {comment.TaskID}, CommentedBy: {comment.CommentedBy}");
            }
        }

        static void AddComment(DataAccessLayer dal)
        {
            Console.WriteLine("Enter Comment Details");
            Console.WriteLine("Title: ");
            string commentTitle = Console.ReadLine();
            Console.WriteLine("CommentText: ");
            string commentText = Console.ReadLine();
            Console.WriteLine("TaskID: ");
            long taskID = long.Parse(Console.ReadLine());
            Console.WriteLine("CommentedBy: ");
            long commentedBy = long.Parse(Console.ReadLine());

            CommentDTO newComment = new CommentDTO();
            newComment.Title = commentTitle;
            newComment.CommentText = commentText;
            newComment.TaskID = taskID;
            newComment.CommentedBy = commentedBy;

            bool success = dal.AddComment(newComment);
            if (success)
            {
                Console.WriteLine("Comment Added Successfully");
            }
            else
            {
                Console.WriteLine("Comment Not Added");
            }
        }

        static void AssignTaskToQA(DataAccessLayer dal)
        {
            Console.WriteLine("Enter Task ID to assign to QA: ");
            long taskID = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter QA User ID: ");
            long qaUserID = long.Parse(Console.ReadLine());

            TaskDTO task = new TaskDTO();
            task.TaskID = taskID;
            task.AssignTo = qaUserID;

            bool success = dal.AssignTaskToQA(task);
            if (success)
            {
                Console.WriteLine("Task Assigned to QA Successfully");
            }
            else
            {
                Console.WriteLine("Task Not Assigned to QA");
            }
        }

        static void ReopenOrCloseTask(DataAccessLayer dal)
        {
            Console.WriteLine("Enter Task ID to reopen or close: ");
            long taskID = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter new status (Reopen/Close): ");
            string status = Console.ReadLine();

            TaskDTO task = new TaskDTO();
            task.TaskID = taskID;
            task.Status = status;

            bool success = dal.ReopenOrCloseTask(task);
            if (success)
            {
                Console.WriteLine("Task status updated successfully");
            }
            else
            {
                Console.WriteLine("Task status not updated");
            }
        }
    }
}
