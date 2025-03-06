using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    

   

    enum Role { SiteAdmin, ProjectManager, Developer, QA }
    enum TaskStatus { Open, Development, QA, Closed }
    enum TaskType { Bug, Feature }

    class User
    {
        public string Name { get; set; }
        public Role UserRole { get; set; }
        public List<Task> AssignedTasks { get; set; } = new List<Task>();
    }

    class Task
    {
        public string Title { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Open;
        public User AssignedTo { get; set; }
        public List<string> Comments { get; set; } = new List<string>();
    }

    class Project
    {
        public string Name { get; set; }
        public User Manager { get; set; }
        public List<User> Team { get; set; } = new List<User>();
        public List<Task> Tasks { get; set; } = new List<Task>();
    }

    class TaskManager
    {
        private List<User> users = new List<User>();
        private List<Project> projects = new List<Project>();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nTask Management System");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Project");
                Console.WriteLine("3. Assign Project Manager");
                Console.WriteLine("4. Assign Team Members");
                Console.WriteLine("5. Create Task");
                Console.WriteLine("6. Assign Task");
                Console.WriteLine("7. Update Task Status");
                Console.WriteLine("8. Add Comment to Task");
                Console.WriteLine("9. List All Tasks");
                Console.WriteLine("10. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": CreateUser();
                        break;
                    case "2": CreateProject();
                        break;
                    case "3": AssignProjectManager();
                        break;
                    case "4": AssignTeamMembers();
                        break;
                    case "5": CreateTask();
                        break;
                    case "6": AssignTask();
                        break;
                    case "7": UpdateTaskStatus();
                        break;
                    case "8": AddComment();
                        break;
                    case "9": ListTasks();
                        break;
                    case "10": return;
                    default: Console.WriteLine("Invalid choice, try again."); 
                        break;
                }
            }
        }

        private void CreateUser()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Role (SiteAdmin/ProjectManager/Developer/QA): ");
            Role role = (Role)Enum.Parse(typeof(Role), Console.ReadLine(), true);
            users.Add(new User { Name = name, UserRole = role });
            Console.WriteLine("User created successfully.");
        }

        private void CreateProject()
        {
            Console.Write("Enter Project Name: ");
            string projectName = Console.ReadLine();
            projects.Add(new Project { Name = projectName });
            Console.WriteLine("Project created.");
        }

        private void AssignProjectManager()
        {
            Console.Write("Enter Project Name: ");
            string projectName = Console.ReadLine();
            Project project = projects.Find(p => p.Name == projectName);
            if (project == null) { Console.WriteLine("Project not found."); return; }
            Console.Write("Enter Manager Name: ");
            string managerName = Console.ReadLine();
            User manager = users.Find(u => u.Name == managerName && u.UserRole == Role.ProjectManager);
            if (manager == null) { Console.WriteLine("Manager not found."); return; }
            project.Manager = manager;
            Console.WriteLine("Manager assigned.");
        }

        private void AssignTeamMembers()
        {
            Console.Write("Enter Project Name: ");
            string projectName = Console.ReadLine();
            Project project = projects.Find(p => p.Name == projectName);
            if (project == null) { Console.WriteLine("Project not found."); return; }
            Console.Write("Enter Team Member Name: ");
            string memberName = Console.ReadLine();
            User member = users.Find(u => u.Name == memberName);
            if (member == null) { Console.WriteLine("User not found."); return; }
            project.Team.Add(member);
            Console.WriteLine("User assigned to project.");
        }

        private void CreateTask()
        {
            Console.Write("Enter Project Name: ");
            string projectName = Console.ReadLine();
            Project project = projects.Find(p => p.Name == projectName);
            if (project == null) { Console.WriteLine("Project not found."); return; }
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Task Type (Bug/Feature): ");
            TaskType type = (TaskType)Enum.Parse(typeof(TaskType), Console.ReadLine(), true);
            project.Tasks.Add(new Task { Title = title, Type = type });
            Console.WriteLine("Task created.");
        }

        private void AssignTask()
        {
            Console.Write("Enter Developer Name: ");
            string devName = Console.ReadLine();
            User developer = users.Find(u => u.Name == devName && u.UserRole == Role.Developer);
            if (developer == null) { Console.WriteLine("Developer not found."); return; }
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            Task task = projects.SelectMany(p => p.Tasks).FirstOrDefault(t => t.Title == title);
            if (task == null) { Console.WriteLine("Task not found."); return; }
            task.AssignedTo = developer;
            developer.AssignedTasks.Add(task);
            Console.WriteLine("Task assigned.");
        }

        private void UpdateTaskStatus()
        {
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            Task task = projects.SelectMany(p => p.Tasks).FirstOrDefault(t => t.Title == title);
            if (task == null) { Console.WriteLine("Task not found."); return; }
            Console.Write("Enter New Status (Open/Development/QA/Closed): ");
            task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), Console.ReadLine(), true);
            Console.WriteLine("Task status updated.");
        }

        private void AddComment()
        {
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            Task task = projects.SelectMany(p => p.Tasks).FirstOrDefault(t => t.Title == title);
            if (task == null) { Console.WriteLine("Task not found."); return; }
            Console.Write("Enter Comment: ");
            task.Comments.Add(Console.ReadLine());
            Console.WriteLine("Comment added.");
        }

        private void ListTasks()
        {
            foreach (var project in projects)
                foreach (var task in project.Tasks)
                    Console.WriteLine($"{task.Title} ({task.Type}) - Status: {task.Status}, Assigned To: {(task.AssignedTo?.Name ?? "None")}");
        }
    }

}