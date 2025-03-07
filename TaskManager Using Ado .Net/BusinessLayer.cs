using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQL
{
    internal class BusinessLayer
    {
        DataAccessLayer dal = new DataAccessLayer();
        UserDTO loggedinUser = new UserDTO();

        public BusinessLayer()
        {
            dal.OpenConnection();
        }

        public UserDTO Authentication(string email,string password)
        {
            loggedinUser = dal.LoginUser(email, password);
            return loggedinUser;
        }

        public bool AddUser(UserDTO inp)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.AddUser(inp);
            }
            else
            {
                return false;
            }
        }

        public bool AddProject(ProjectDTO project)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.AddProject(project);
            }
            else
            {
                return false;
            }
        }
        public bool AddProjectAssignedTo(ProjectAssignedToDTO projectAssignedTo)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2)
            {
                return dal.AddProjAss(projectAssignedTo);
            }
            else
            {
                return false;
            }
            
        }

        public List<ProjectAssignedToDTO> ListProjectAssignedTo()
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2)
            {
                return dal.ListProjAss();
            }
            else
            {
                return null;
            }
        }

        public List<ProjectAssignedToDTO> ListProjectsAssigned()
        {
            if (loggedinUser != null && loggedinUser.RoleId == 3)
            {
                return dal.ListProjectsAssigned(loggedinUser.UserId);
            }
            else
            {
                return null;
            }
        }


        public bool AddTask(TaskDTO task)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2)
            {
                return dal.AddTask(task);
            }
            else
            {
                return false;
            }
            
        }

        public bool AddComment(CommentDTO com)
        {
            if(loggedinUser != null && (loggedinUser.RoleId == 3 || loggedinUser.RoleId == 4))
            {
                return dal.AddComment(com);
            }
            else
            {
                return false;
            }
            
        }

        public bool userhastask()
        {
            if (loggedinUser != null && loggedinUser.RoleId == 3)
            {
                return dal.userhastasks(loggedinUser.UserId);
            }
            else
            {
                return false;
            }
        }


        public bool UpdateUser(UserDTO inp)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.UpdateUser(inp);
            }
            else
            {
                return false;
            }
        }



        public List<UserDTO> ListUsers()
        {
            return dal.ListUsers();
        }

        

        public List<ProjectDTO> ListProjects()
        {
            return dal.ListProjects();
        }
        public List<TaskDTO> ListTasks()

        {
            if (loggedinUser != null && (loggedinUser.RoleId == 1 || loggedinUser.RoleId == 2))
            {
                return dal.ListTasks();
            }
            else
            {
                return null;
            }
            
        }

        public List<TaskDTO> ListTasksAssignedTo()
        {
            if (loggedinUser != null && loggedinUser.RoleId == 3)
            {
                return dal.ListTasksAssignedTo(loggedinUser.UserId);
            }
            else
            {
                return null;
            }
        }

        public List<CommentDTO> ListComments()
        {
            return dal.ListComments();
        }

        public bool DeleteUser(long id)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.DeleteUser(id);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProject(long id)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.DeleteProject(id);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTask(long id)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.DeleteTask(id);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteComment(long id)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1)
            {
                return dal.DeleteComment(id);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateComment(CommentDTO comment)
        {
            return dal.UpdateComment(comment);
        }
        
        public bool UpdateTask(TaskDTO task)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2)
            {
                return dal.UpdateTask(task);
            }
            else
            {
                return false;
            }
            
        }

        public bool UpdateTaskStatus(TaskDTO task)
        {
            if (loggedinUser != null && (loggedinUser.RoleId == 2 || loggedinUser.RoleId == 4))
            {
                return dal.UpdateTaskStatus(task);
            }
            else
            {
                return false;
            }
            
        }

     

        public bool UpdateTaskAssignedTo(TaskDTO task)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2)
            {
                return dal.UpdateTaskAssignedTo(task);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateProject(ProjectDTO project)
        {
            return dal.UpdateProject(project);
        }

        public void CloseApp()
        {
            dal.CloseConnection();
        }
    }
}
