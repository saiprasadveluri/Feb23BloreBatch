using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TaskManagerADO
{
    public class Program
    {
        static DataAccess dal = new DataAccess();
        static Businesslayer bl = new Businesslayer();
        static void Main(string[] args)
        {
          
            Console.WriteLine("Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            UserDTO user = bl.Authenticateuser(email, password);
            if (user != null)
            {
                Console.WriteLine($"Welcome {user.Name}");


            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Select a Category:");
                Console.WriteLine("1. User Management");
                Console.WriteLine("2. Project Management");
                Console.WriteLine("3. Task Management");
                Console.WriteLine("4. Comment Management");
                Console.WriteLine("5. Exit");

                int categoryChoice = int.Parse(Console.ReadLine());

                switch (categoryChoice)
                {
                    case 1:
                        UserMenu();
                        break;
                    case 2:
                        ProjectMenu();
                        break;
                    case 3:
                        TaskMenu();
                        break;
                    case 4:
                        CommentMenu();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            }
            else
            {   
                
                Console.WriteLine("Invalid email or password");
                bl.CloseApp();
                
            }
        }
            static void UserMenu()
            {
                bool back = false;
                while (!back)
                {
                    Console.WriteLine("\nUser Management:");
                    Console.WriteLine("1. Add User");
                    Console.WriteLine("2. Update User");
                    Console.WriteLine("3. List Users");
                    Console.WriteLine("4. Delete User");
                    Console.WriteLine("5. Back to Main Menu");

                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddUser();
                            break;
                        case 2:
                            UpdateUser();
                            break;
                        case 3:
                            ListUsers();
                            break;
                        case 4:
                            DeleteUser();
                            break;
                        case 5:
                            back = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }

            static void ProjectMenu()
            {
                bool back = false;
                while (!back)
                {
                    Console.WriteLine("\nProject Management:");
                    Console.WriteLine("1. Add Project");
                    Console.WriteLine("2. Update Project");
                    Console.WriteLine("3. List Projects");
                    Console.WriteLine("4. Delete Project");
                    Console.WriteLine("5. Back to Main Menu");

                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddProject();
                            break;
                        case 2:
                            UpdateProject();
                            break;
                        case 3:
                            ListProjects();
                            break;
                        case 4:
                            DeleteProject();
                            break;
                        case 5:
                            back = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }

            static void TaskMenu()
            {
                bool back = false;
                while (!back)
                {
                    Console.WriteLine("\nTask Management:");
                    Console.WriteLine("1. Add Task");
                    Console.WriteLine("2. Update Task");
                    Console.WriteLine("3. List Tasks");
                    Console.WriteLine("4. Delete Task");
                    Console.WriteLine("5. Back to Main Menu");

                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddTask();
                            break;
                        case 2:
                            UpdateTask();
                            break;
                        case 3:
                            ListTasks();
                            break;
                        case 4:
                            DeleteTask();
                            break;
                        case 5:
                            back = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            static void CommentMenu()
            {
                bool back = false;
                while (!back)
                {
                    Console.WriteLine("\nComment Management:");
                    Console.WriteLine("1. Add Comment");
                    Console.WriteLine("2. Update Comment");
                    Console.WriteLine("3. Display Comments");
                    Console.WriteLine("4. Delete Comment");
                    Console.WriteLine("5. Back to Main Menu");

                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddComment();
                            break;
                        case 2:
                            UpdateComment();
                            break;
                        case 3:
                            DisplayComments();
                            break;
                        case 4:
                            DeleteComment();
                            break;
                        case 5:
                            back = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        static void AddUser()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter user name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter department:");
                string dept = Console.ReadLine();
                Console.WriteLine("Enter role id:");
                long roleid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                UserDTO newUser = new UserDTO();
                newUser.Name = name;
                newUser.Department = dept;
                newUser.RoleId = roleid;
                newUser.Email = email;
                newUser.Password = password;
                bool result = dal.AddUser(newUser);
                if (result)
                    Console.WriteLine("User added successfully");
                else
                    Console.WriteLine("User not added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }
        static void ListUsers()
        {
            try
            {
                dal.OpenConnection();
                List<UserDTO> users = dal.Listusers();
                foreach (UserDTO user in users)
                {
                    Console.WriteLine($"User id: {user.UserId} - Name: {user.Name} - Department: {user.Department} - Roleid: {user.RoleId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void ListProjects()
        {
            try
            {
                dal.OpenConnection();
                List<ProjectDTO> proj = dal.Listprojects();
                foreach (ProjectDTO user in proj)
                {
                    Console.WriteLine($"Project id: {user.Projectid} - Project Title: {user.Projecttitle} - Project manager id: {user.PMid} - Project status: {user.status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void AddProject()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter PROJECT TITLE");
                string title = Console.ReadLine();
                Console.WriteLine("Enter PM ID");
                long pmid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter STATUS");
                string status = Console.ReadLine();
                ProjectDTO newProject = new ProjectDTO();
                newProject.Projecttitle = title;
                newProject.PMid = pmid;
                newProject.status = status;
                bool r = dal.AddProject(newProject);
                if (r)
                    Console.WriteLine("Project added successfully");
                else
                    Console.WriteLine("Project not added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void ListTasks()
        {
            try
            {
                dal.OpenConnection();
                List<TaskDTO> tasks = dal.ListTasks();
                foreach (TaskDTO task in tasks)
                {
                    Console.WriteLine($"Task id: {task.Taskid} - Task Title: {task.title} - Project id: {task.Projectid} - Assigned To: {task.assignedto}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void AddTask()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter Task Title");
                string tasktitle = Console.ReadLine();
                Console.WriteLine("Enter Task Type");
                long tasktype = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Project ID");
                long projectid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Assigned To");
                long assignedto = long.Parse(Console.ReadLine());
                TaskDTO newTask = new TaskDTO();
                newTask.title = tasktitle;
                newTask.Tasktype = tasktype;
                newTask.Projectid = projectid;
                newTask.assignedto = assignedto;
                bool res = dal.AddTask(newTask);
                if (res)
                    Console.WriteLine("Task added successfully");
                else
                    Console.WriteLine("Task not added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void UpdateUser()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter user id to update");
                long uid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter user name:");
                string uname = Console.ReadLine();
                Console.WriteLine("Enter department:");
                string udept = Console.ReadLine();
                Console.WriteLine("Enter role id:");
                long uroleid = long.Parse(Console.ReadLine());
                UserDTO uuser = new UserDTO();
                uuser.UserId = uid;
                uuser.Name = uname;
                uuser.Department = udept;
                uuser.RoleId = uroleid;
                bool uresult = dal.UpdateUser(uuser);
                if (uresult)
                    Console.WriteLine("User updated successfully");
                else
                    Console.WriteLine("User not updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void DeleteUser()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter user id to delete");
                long delUserId = long.Parse(Console.ReadLine());
                bool delUserResult = dal.DeleteUser(delUserId);
                if (delUserResult)
                    Console.WriteLine("User deleted successfully");
                else
                    Console.WriteLine("User not deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void UpdateProject()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter project id to update");
                long pid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter project title:");
                string ptitle = Console.ReadLine();
                Console.WriteLine("Enter PM id:");
                long ppmid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter status:");
                string pstatus = Console.ReadLine();
                ProjectDTO uproject = new ProjectDTO();
                uproject.Projectid = pid;
                uproject.Projecttitle = ptitle;
                uproject.PMid = ppmid;
                uproject.status = pstatus;
                bool presult = dal.UpdateProject(uproject);
                if (presult)
                    Console.WriteLine("Project updated successfully");
                else
                    Console.WriteLine("Project not updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void DeleteProject()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter project id to delete");
                long delProjectId = long.Parse(Console.ReadLine());
                bool delProjectResult = dal.DeleteProject(delProjectId);
                if (delProjectResult)
                    Console.WriteLine("Project deleted successfully");
                else
                    Console.WriteLine("Project not deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        static void UpdateTask()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter task id to update");
                long tid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter task title:");
                string ttitle = Console.ReadLine();
                Console.WriteLine("Enter task type:");
                long ttype = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter project id:");
                long tprojectid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter assigned to:");
                long tassignedto = long.Parse(Console.ReadLine());
                TaskDTO utask = new TaskDTO();
                utask.Taskid = tid;
                utask.title = ttitle;
                utask.Tasktype = ttype;
                utask.Projectid = tprojectid;
                utask.assignedto = tassignedto;
                bool tresult = dal.UpdateTask(utask);
                if (tresult)
                    Console.WriteLine("Task updated successfully");
                else
                    Console.WriteLine("Task not updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }
        static void DeleteTask()
        {
            try
            {
                dal.OpenConnection();
                Console.WriteLine("Enter task id to delete");
                long delTaskId = long.Parse(Console.ReadLine());
                bool delTaskResult = dal.DeleteTask(delTaskId);
                if (delTaskResult)
                    Console.WriteLine("Task deleted successfully");
                else
                    Console.WriteLine("Task not deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }
            static void AddComment()
            {
                try
                {
                    dal.OpenConnection();
                    Console.WriteLine("Enter comment title:");
                    string title = Console.ReadLine();
                    Console.WriteLine("Enter comment text:");
                    string commentText = Console.ReadLine();
                    Console.WriteLine("Enter task id:");
                    long taskId = long.Parse(Console.ReadLine());
                    Console.WriteLine("Enter commented by:");
                    long commentedBy = long.Parse(Console.ReadLine());
                    CommentsDTO newComment = new CommentsDTO();
                    newComment.Title = title;
                    newComment.Commenttext = commentText;
                    newComment.Taskid = taskId;
                    newComment.Commentedby = commentedBy;
                    bool result = dal.AddComment(newComment);
                    if (result)
                        Console.WriteLine("Comment added successfully");
                    else
                        Console.WriteLine("Comment not added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    dal.CloseConnection();
                }
            }

            static void UpdateComment()
            {
                try
                {
                    dal.OpenConnection();
                    Console.WriteLine("Enter comment id to update:");
                    long commentId = long.Parse(Console.ReadLine());
                    Console.WriteLine("Enter comment title:");
                    string title = Console.ReadLine();
                    Console.WriteLine("Enter comment text:");
                    string commentText = Console.ReadLine();
                    Console.WriteLine("Enter task id:");
                    long taskId = long.Parse(Console.ReadLine());
                    Console.WriteLine("Enter commented by:");
                    long commentedBy = long.Parse(Console.ReadLine());
                    CommentsDTO updatedComment = new CommentsDTO();
                    updatedComment.Commentid = commentId;
                    updatedComment.Title = title;
                    updatedComment.Commenttext = commentText;
                    updatedComment.Taskid = taskId;
                    updatedComment.Commentedby = commentedBy;
                    bool result = dal.UpdateComment(updatedComment);
                    if (result)
                        Console.WriteLine("Comment updated successfully");
                    else
                        Console.WriteLine("Comment not updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    dal.CloseConnection();
                }
            }

            static void DeleteComment()
            {
                try
                {
                    dal.OpenConnection();
                    Console.WriteLine("Enter comment id to delete:");
                    long commentId = long.Parse(Console.ReadLine());
                    bool result = dal.DeleteComment(commentId);
                    if (result)
                        Console.WriteLine("Comment deleted successfully");
                    else
                        Console.WriteLine("Comment not deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    dal.CloseConnection();
                }
            }
        static void DisplayComments()
        {
            try
            {
                dal.OpenConnection();
                List<CommentsDTO> comments = dal.DisplayComments();
                foreach (CommentsDTO comment in comments)
                {
                    Console.WriteLine($"Comment id: {comment.Commentid} - Title: {comment.Title} - Comment text: {comment.Commenttext} - Task id: {comment.Taskid} - Commented by: {comment.Commentedby}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dal.CloseConnection();
            }
        }

    }
    }
