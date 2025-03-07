
using System.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using SQL;


class Program
{
    

    static BusinessLayer bl = new BusinessLayer();
    static void Main(string[] args)
    {

        //dal.OpenConnection();
        login:
        ListUser();
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

                if (usr.RoleId == 1)
                {
                    Console.WriteLine("1. Create User: ");
                    Console.WriteLine("2. List Users: ");
                    Console.WriteLine("15. Logout");

                }

                if (usr.RoleId == 2)
                {
                    Console.WriteLine("3. Add Project: ");
                    Console.WriteLine("4. List Projects: ");
                    Console.WriteLine("5. Assign Projects: ");
                    Console.WriteLine("6. List Assigned Projects: ");
                    Console.WriteLine("7. Add Task: ");
                    Console.WriteLine("8. List Tasks: ");
                    Console.WriteLine("9. Update Tasks");
                    Console.WriteLine("15. Logout");

                }
                if (usr.RoleId == 3)
                {
                    Console.WriteLine("6. List Assigned Projects: ");
                    Console.WriteLine("10. Update Task Status: ");
                    Console.WriteLine("11. Update Task Assigned To: ");
                    Console.WriteLine("12. List Task Assigned To: ");
                    Console.WriteLine("13. Add Comment: ");
                    Console.WriteLine("14. List Comments: ");
                    Console.WriteLine("15. Logout");
                }
                if (usr.RoleId == 4)
                {
                    Console.WriteLine("6. List Assigned Projects: ");
                    Console.WriteLine("10. Update Task Status: ");
                    Console.WriteLine("13. Add Comment: ");
                    Console.WriteLine("14. List Comments: ");
                    Console.WriteLine("15. Logout");
                }

                Console.WriteLine("Enter Your Choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        ListUser();
                        break;
                    case 3:
                        AddProject();
                        break;
                    case 4:
                        ListProject();
                        break;
                    case 5:
                        AddProjectAssignedTo();
                        break;
                    case 6:
                        if(usr.RoleId == 2)
                        {
                            ListProjectAssignedTo();
                        }
                        else
                        {
                            ListProjectsAssigned();
                        }
                        
                        break;
                    case 7:
                        AddTask();
                        break;
                    case 8:
                        ListTask();
                        break;
                    case 9:
                        UpdateTask();
                        break;
                    case 10:
                        UpdateTaskStatus();
                        break;
                    case 11:
                        UpdateTaskAssignedTo();
                        break;
                    case 12:
                        ListTaskAssignedTo();
                        break;
                    case 13:
                        AddComment();
                        break;
                    case 14:
                        ListComment();
                        break;
                    case 15:
                        goto login;
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

    public static void ListProjectAssignedTo()
    {
        List<ProjectAssignedToDTO> ProjectAssignedToList = bl.ListProjectAssignedTo();
        foreach (ProjectAssignedToDTO projectAssignedTo in ProjectAssignedToList)
        {
            Console.WriteLine($"Id: {projectAssignedTo.id} - Project Id: {projectAssignedTo.projid} - User Id: {projectAssignedTo.userid}");
        }
    }

    public static void ListProjectsAssigned()
    {
        List<ProjectAssignedToDTO> ProjectAssignedToList = bl.ListProjectsAssigned();
        foreach (ProjectAssignedToDTO projectAssignedTo in ProjectAssignedToList)
        {
            Console.WriteLine($"Id: {projectAssignedTo.id} - Project Id: {projectAssignedTo.projid} - User Id: {projectAssignedTo.userid}");
        }
    }

    public static void UpdateTaskStatus()
    {
        Console.WriteLine("Enter Task Id to Update: ");
        long tid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Task Status: ");
        string tstatus = Console.ReadLine();
        TaskDTO task = new TaskDTO();
        task.TaskId = tid;
        task.TStatus = tstatus;
        bool add = bl.UpdateTaskStatus(task);
        if (add)
        {
            Console.WriteLine("Task Status Updated Successfully!");
        }
        else
        {
            Console.WriteLine("Task Status not updated!");
        }
    }

    public static void UpdateTaskAssignedTo()
    {
        Console.WriteLine("Enter Task Id to Update: ");
        long tid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Assigned To: ");
        long atid = Convert.ToInt64(Console.ReadLine());
        TaskDTO task = new TaskDTO();
        task.TaskId = tid;
        task.AssignedTo = atid;
        bool add = bl.UpdateTaskAssignedTo(task);
        if (add)
        {
            Console.WriteLine("Task Assigned To Updated Successfully!");
        }
        else
        {
            Console.WriteLine("Task Assigned To not updated!");
        }
    }
    public static void ListTaskAssignedTo()
    {
        List<TaskDTO> TaskAssignedToList = bl.ListTasksAssignedTo();
        foreach (TaskDTO taskAssignedTo in TaskAssignedToList)
        {
            Console.WriteLine($"Task Id: {taskAssignedTo.TaskId} - Task Title: {taskAssignedTo.Title} - User Id: {taskAssignedTo.AssignedTo} - Project Id:{taskAssignedTo.ProjectId}");
        }
    }


    public static void AddProjectAssignedTo()
    {
        ProjectAssignedToDTO projectAssignedTo = new ProjectAssignedToDTO();
        Console.WriteLine("Project Id: ");
        projectAssignedTo.projid = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("User Id: ");
        projectAssignedTo.userid = Convert.ToInt64(Console.ReadLine());
        bool add = bl.AddProjectAssignedTo(projectAssignedTo);
        if (add)
        {
            Console.WriteLine("Project Assigned To Added Successfully!");
        }
        else
        {
            Console.WriteLine("Project Assigned To not added!");
        }
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

        bool add = bl.AddProject(project);
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


        bool add = bl.AddTask(task);
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
        if(bl.userhastask())
        {
            ListTaskAssignedTo();
            CommentDTO comment = new CommentDTO();
            Console.WriteLine("Title: ");
            comment.Title = Console.ReadLine();
            Console.WriteLine("Comment Text: ");
            comment.CommentText = Console.ReadLine();
            Console.WriteLine("Task Id: ");
            comment.TaskId = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Commented By: ");
            comment.CommentedBy = Convert.ToInt64(Console.ReadLine());

            bool add = bl.AddComment(comment);
            if (add)
            {
                Console.WriteLine("Comment Added Successfully!");
            }
            else
            {
                Console.WriteLine("Comment not added!");
            }
        }
        else
        {
            Console.WriteLine("You are not assigned to any task!");
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
        bool add = bl.UpdateProject(project);
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
        bool delete = bl.DeleteProject(id);
        if (delete)
        {
            Console.WriteLine("Project Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Project not deleted!/ Not Authorized");
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
        bool add = bl.UpdateTask(task);
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
        bool delete = bl.DeleteTask(id);
        if (delete)
        {
            Console.WriteLine("Task Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Task not deleted! /  UnAuthorized");
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
        bool add = bl.UpdateComment(comment);
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
        bool delete = bl.DeleteComment(id);
        if (delete)
        {
            Console.WriteLine("Comment Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Comment not deleted! /  UnAuthorized");
        }
    }



}

