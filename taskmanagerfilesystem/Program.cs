using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    using System;
    using System.Collections.Generic;
    class Program

    {

        static List<Project> projects = new List<Project>();

        static DataStorage<Project> projectStorage = new DataStorage<Project>("projects");

        static void Main()

        {

            while (true)

            {

                Console.WriteLine("\nTask Manager - Role Selection");

                Console.WriteLine("1. Admin");

                Console.WriteLine("2. Project Manager");

                Console.WriteLine("3. Developer");

                Console.WriteLine("4. QA Analyst");

                Console.WriteLine("5. Exit");

                Console.Write("Select your role (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)

                {

                    case "1":

                        AdminMenu();

                        break;

                    case "2":

                        ProjectManagerMenu();

                        break;

                    case "3":

                        DeveloperMenu();

                        break;

                    case "4":

                        QAMenu();

                        break;

                    case "5":

                        Console.WriteLine("Exiting Task Manager. Goodbye!");

                        return;

                    default:

                        Console.WriteLine("Invalid choice! Please try again.");

                        break;

                }

            }

        }

        static void AdminMenu()

        {

            Console.WriteLine("\nAdmin Menu:");

            Console.WriteLine("1. Create a new project");

            Console.WriteLine("2. View all projects");

            Console.WriteLine("3. Back to main menu");

            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)

            {

                case "1":

                    Console.Write("Enter project name: ");

                    string projectName = Console.ReadLine();

                    projects.Add(new Project(projectName));

                    projectStorage.SaveData(projects);

                    Console.WriteLine($"Project '{projectName}' created successfully.");

                    break;

                case "2":

                    List<string> storedProjects = projectStorage.LoadData();

                    Console.WriteLine("\nStored Projects:");

                    foreach (string proj in storedProjects)

                    {

                        Console.WriteLine(proj);

                    }

                    break;

                case "3":

                    return;

                default:

                    Console.WriteLine("Invalid choice.");

                    break;

            }

        }

        static void ProjectManagerMenu()

        {

            Console.WriteLine("\nProject Manager Menu:");

            Console.WriteLine("1. Create a new task");

            Console.WriteLine("2. View tasks");

            Console.WriteLine("3. Back to main menu");

            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)

            {

                case "1":

                    Console.Write("Enter task title: ");

                    string taskTitle = Console.ReadLine();

                    Console.Write("Enter task type (Bug/Feature): ");

                    string taskType = Console.ReadLine();

                    Task task = new Task(taskTitle, taskType);

                    Console.WriteLine($"Task '{taskTitle}' created successfully.");

                    break;

                case "2":

                    Console.WriteLine("Viewing Assigned Tasks feature coming soon...");

                    break;

                case "3":

                    return;

                default:

                    Console.WriteLine("Invalid choice.");

                    break;

            }

        }

        static void DeveloperMenu()

        {

            Console.WriteLine("\nDeveloper Menu:");

            Console.WriteLine("1. View assigned tasks");

            Console.WriteLine("2. Update task status");

            Console.WriteLine("3. Add comments to a task");

            Console.WriteLine("4. Back to main menu");

            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)

            {

                case "1":

                    Console.WriteLine("Viewing Assigned Tasks feature coming soon...");

                    break;

                case "2":

                    Console.WriteLine("Updating Task Status feature coming soon...");

                    break;

                case "3":

                    Console.WriteLine("Adding Comments feature coming soon...");

                    break;

                case "4":

                    return;

                default:

                    Console.WriteLine("Invalid choice.");

                    break;

            }

        }

        static void QAMenu()

        {

            Console.WriteLine("\nQA Analyst Menu:");

            Console.WriteLine("1. View assigned tasks");

            Console.WriteLine("2. Test a task (update status)");

            Console.WriteLine("3. Add comments or reopen a task");

            Console.WriteLine("4. Back to main menu");

            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)

            {

                case "1":

                    Console.WriteLine("Viewing Assigned Tasks feature coming soon...");

                    break;

                case "2":

                    Console.WriteLine("Testing Tasks feature coming soon...");

                    break;

                case "3":

                    Console.WriteLine("Adding/Reopening Task Comments feature coming soon...");

                    break;

                case "4":

                    return;

                default:

                    Console.WriteLine("Invalid choice.");

                    break;

            }

        }

    }


}
