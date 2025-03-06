
using System.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using SQL;


class Program
{
    static DataAccessLayer dal = new DataAccessLayer();

    static BusinessLayer bl = new BusinessLayer();
    static void Main(string[] args)
    {

        //dal.OpenConnection();
        
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        UserDTO usr = bl.Authentication(email, password.Trim());
        if (usr != null)
        {
            Console.WriteLine($"Welcome! {usr.UName}");
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
                        UpdateTask();
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
                        UpdateComment();
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
        else
        {
            Console.WriteLine("\n Not Authorized");
        }
            bl.CloseApp();


    }
    public static void ListProject()
    {
        List<ProjectDTO> ProjectList = bl.ListProjects();
        foreach (ProjectDTO project in ProjectList)
        {
            Console.WriteLine($"Project Id: {project.ProjectId} - Project Name: {project.ProjectName} - Project Manager Id: {project.ProjectManagerId} - Project Status: {project.PStatus}");
        }
    }
    public static void ListUser()
    {
        List<UserDTO> lst = bl.ListUsers();
        foreach (UserDTO user in lst)
        {
            Console.WriteLine($"User Id: {user.UserId} - Name: {user.UName} - Dept: {user.Dept} - Role Id: {user.RoleId} - Email: {user.Email}");
        }
    }
    public static void AddUser()
    {
        UserDTO user = new UserDTO();
        Console.WriteLine("Name: ");
        user.UName = Console.ReadLine();

        Console.WriteLine("Dept: ");
        user.Dept = Console.ReadLine();

        Console.WriteLine("RoleId: ");
        user.RoleId = Convert.ToInt64(Console.ReadLine());

        Console.WriteLine("Email: ");
        user.Email = Console.ReadLine();

        Console.WriteLine("Password: ");
        user.Password = Console.ReadLine();

        bool add = bl.AddUser(user);
        if (add)
        {
            Console.WriteLine("User Added Successfully!");
        }
        else
        {
            Console.WriteLine("User not added!/Unauthorized");
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

        bool add = dal.AddProject(project);
        if (add)
        {
            Console.WriteLine("Project Added Successfully!");
        }
        else
        {
            Console.WriteLine("Project not added!");
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


        bool add = dal.AddTask(task);
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
        List<TaskDTO> TaskList = bl.ListTasks();
        foreach (TaskDTO task in TaskList)
        {
            Console.WriteLine($"Task Id: {task.TaskId} - Task Title: {task.Title} - Task Type: {task.TType} - Project Id: {task.ProjectId} - Assigned To: {task.AssignedTo}");
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

        bool add = dal.AddComment(comment);
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
        List<CommentDTO> CommentList = bl.ListComments();
        foreach (CommentDTO comment in CommentList)
        {
            Console.WriteLine($"Comment Id: {comment.CommentId} - Title: {comment.Title} - Comment Text: {comment.CommentText} - Task Id: {comment.TaskId} - Commented By: {comment.CommentedBy}");
        }
    }

    public static void UpdateUser()
    {
        Console.WriteLine("Enter User Id to Update: ");
        long uid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Dept: ");
        string dept = Console.ReadLine();
        Console.WriteLine("RoleId: ");
        int roleId = Convert.ToInt32(Console.ReadLine());
        UserDTO user = new UserDTO();
        user.UserId = uid;
        user.UName = name;
        user.Dept = dept;
        user.RoleId = roleId;
        bool add = bl.UpdateUser(user);
        if (add)
        {
            Console.WriteLine("User Updated Successfully!");
        }
        else
        {
            Console.WriteLine("User not updated!/UnAuthorized");
        }
    }

    public static void DeleteUser()
    {
        Console.WriteLine("Enter UserId To Delete: ");
        long id = Convert.ToInt64(Console.ReadLine());
        bool delete = bl.DeleteUser(id);
        if (delete)
        {
            Console.WriteLine("User Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("User not deleted! / UnAuthorized");
        }
    }

    public static void UpdateProject()
    {
        Console.WriteLine("Enter Project Id to Update: ");
        long pid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Project Name: ");
        string pname = Console.ReadLine();
        Console.WriteLine("Project Manager Id: ");
        long pmid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Project Status: ");
        string pstatus = Console.ReadLine();
        ProjectDTO project = new ProjectDTO();
        project.ProjectId = pid;
        project.ProjectName = pname;
        project.ProjectManagerId = pmid;
        project.PStatus = pstatus;
        bool add = dal.UpdateProject(project);
        if (add)
        {
            Console.WriteLine("Project Updated Successfully!");
        }
        else
        {
            Console.WriteLine("Project not updated!");
        }
    }

    public static void DeleteProject()
    {
        Console.WriteLine("Enter Project Id To Delete: ");
        long id = Convert.ToInt64(Console.ReadLine());
        bool delete = dal.DeleteProject(id);
        if (delete)
        {
            Console.WriteLine("Project Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Project not deleted!");
        }
    }

    public static void UpdateTask()
    {
        Console.WriteLine("Enter Task Id to Update: ");
        long tid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Task Title: ");
        string ttitle = Console.ReadLine();
        Console.WriteLine("Task Type: ");
        int ttype = int.Parse(Console.ReadLine());
        Console.WriteLine("Project Id: ");
        long pid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Assigned To: ");
        long atid = Convert.ToInt64(Console.ReadLine());
        TaskDTO task = new TaskDTO();
        task.TaskId = tid;
        task.Title = ttitle;
        task.TType = ttype;
        task.ProjectId = pid;
        task.AssignedTo = atid;
        bool add = dal.UpdateTask(task);
        if (add)
        {
            Console.WriteLine("Task Updated Successfully!");
        }
        else
        {
            Console.WriteLine("Task not updated!");
        }
    }

    public static void DeleteTask()
    {
        Console.WriteLine("Enter Task Id To Delete: ");
        long id = Convert.ToInt64(Console.ReadLine());
        bool delete = dal.DeleteTask(id);
        if (delete)
        {
            Console.WriteLine("Task Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Task not deleted!");
        }
    }

    public static void UpdateComment()
    {
        Console.WriteLine("Enter Comment Id to Update: ");
        long cid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Title: ");
        string title = Console.ReadLine();
        Console.WriteLine("Comment Text: ");
        string ctext = Console.ReadLine();
        Console.WriteLine("Task Id: ");
        long tid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Commented By: ");
        long cbid = Convert.ToInt64(Console.ReadLine());
        CommentDTO comment = new CommentDTO();
        comment.CommentId = cid;
        comment.Title = title;
        comment.CommentText = ctext;
        comment.TaskId = tid;
        comment.CommentedBy = cbid;
        bool add = dal.UpdateComment(comment);
        if (add)
        {
            Console.WriteLine("Comment Updated Successfully!");
        }
        else
        {
            Console.WriteLine("Comment not updated!");
        }
    }

    public static void DeleteComment()
    {
        Console.WriteLine("Enter Comment Id To Delete: ");
        long id = Convert.ToInt64(Console.ReadLine());
        bool delete = dal.DeleteComment(id);
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

