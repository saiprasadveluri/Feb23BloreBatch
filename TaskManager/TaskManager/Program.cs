using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static SqlConnection con;
        static void Main(string[] args)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.OpenConnection();
            Console.WriteLine("Select use of application: 1.Add User, 2.Roles");
            int op = int.Parse(Console.ReadLine());
            switch(op)
            {
                case 1:
                    Console.WriteLine("Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Dept: ");
                    string dept = Console.ReadLine();
                    Console.WriteLine("RoleId: ");
                    int roleid = int.Parse(Console.ReadLine());
                    Console.Write("Email:");
                    string eml = Console.ReadLine();
                    Console.WriteLine("Password:");
                    string pwd = Console.ReadLine();
                    UserDTO newUser = new UserDTO();
                    newUser.Name = name;
                    newUser.Department = dept;
                    newUser.RoleId = roleid;
                    newUser.Email = eml;
                    newUser.Password = pwd;
                    bool Success = dal.AddUser(newUser);
                    if (Success)
                    {
                        Console.WriteLine("User added");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add user");
                    }
                    break;

                case 2:
                    Console.WriteLine("Role: 1.Admin, 2.Project Manager, 3.Developer, 4.QA analyst");
                    int rl = int.Parse(Console.ReadLine());
                    switch (rl)
                    {
                        case 1:
                            Console.WriteLine("New Project");
                            Console.WriteLine("Title: ");
                            string title = Console.ReadLine();
                            Console.WriteLine("Project Manager: ");
                            int pm = int.Parse(Console.ReadLine());
                            Console.WriteLine("Status: ");
                            string status = Console.ReadLine();
                            ProjectDTO newProj = new ProjectDTO();
                            newProj.Title = title;
                            newProj.ProjManager = pm;
                            newProj.Status = status;
                            bool Succes = dal.AddProject(newProj);
                            if (Succes)
                            {
                                Console.WriteLine("Project added");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add project");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Assign Project");
                            Console.WriteLine("Enter Project id:");
                            int projid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter User id:");
                            int userid = int.Parse(Console.ReadLine());
                            ProjAssToDTO assProj = new ProjAssToDTO();
                            assProj.ProjId = projid;
                            assProj.Userid = userid;
                            bool assign = dal.AssignProj(assProj);
                            if (assign)
                            {
                                Console.WriteLine("Project assigned");
                            }
                            else
                            {
                                Console.WriteLine("Failed to assign project");
                            }

                            Console.WriteLine("New Task");
                            Console.WriteLine("Title: ");
                            string titl = Console.ReadLine();
                            Console.WriteLine("Task Type: 1.Bug, 2.New feature");
                            int tsktype = int.Parse(Console.ReadLine());
                            Console.WriteLine("Project Id: ");
                            int pid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Assigned To: ");
                            int assignTo = int.Parse(Console.ReadLine());
                            Console.WriteLine("Status: ");
                            string stat = Console.ReadLine();
                            TaskDTO task = new TaskDTO();
                            task.Title = titl;
                            task.TaskType = tsktype;
                            task.ProjId = pid;
                            task.AssignedTo = assignTo;
                            task.Status = stat;
                            bool tsk = dal.AddTask(task);
                            if (tsk)
                            {
                                Console.WriteLine("New task added");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add task");
                            }

                            Console.WriteLine("Tasks List");
                            List<TaskDTO> lst = dal.ListTasks();
                            foreach (TaskDTO ts in lst)
                            {
                                Console.WriteLine(ts.Title);
                            }

                            Console.WriteLine("Task Id: ");
                            int tid = int.Parse(Console.ReadLine());
                            Console.WriteLine("Status: ");
                            string stt = Console.ReadLine();
                            TaskDTO Task = new TaskDTO();
                            Task.TaskId = tid;
                            Task.Status = stt;
                            bool Tsk = dal.PMUpdateTask(Task);
                            if (Tsk)
                            {
                                Console.WriteLine("Task updated");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update task");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Tasks List");
                            Console.WriteLine("User Id: ");
                            int uid = int.Parse(Console.ReadLine());
                            List<TaskDTO> dlst = dal.DevTasks(uid);
                            foreach (TaskDTO ts in dlst)
                            {
                                Console.WriteLine(ts.Title);
                            }

                            Console.WriteLine("Add Comments");
                            Console.WriteLine("Title: ");
                            string tt = Console.ReadLine();
                            Console.WriteLine("Comment text: ");
                            string comtxt = Console.ReadLine();
                            Console.WriteLine("Task Id: ");
                            int td = int.Parse(Console.ReadLine());
                            Console.WriteLine("Commented By: ");
                            int comBy = int.Parse(Console.ReadLine());
                            CommentDTO comm = new CommentDTO();
                            comm.Title = tt;
                            comm.CommText = comtxt;
                            comm.TaskId = td;
                            comm.CommentedBy = comBy;
                            bool com = dal.AddComment(comm);
                            if (com)
                            {
                                Console.WriteLine("New comment added");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add comment");
                            }

                            Console.WriteLine("Assign to QA");
                            Console.WriteLine("Task id: ");
                            int id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Assigned To: ");
                            int at = int.Parse(Console.ReadLine());
                            TaskDTO devtsk = new TaskDTO();
                            devtsk.TaskId =id ;
                            devtsk.AssignedTo = at;
                            bool dtsk = dal.PMUpdateTask(devtsk);
                            if (dtsk)
                            {
                                Console.WriteLine("Task updated");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update task");
                            }
                             break;
                        case 4:
                            break;
                    }
                    break;

            }
        }
    }
}
