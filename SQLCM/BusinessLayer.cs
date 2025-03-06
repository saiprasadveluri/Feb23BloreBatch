using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            return dal.ListTasks();
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

        public void CloseApp()
        {
            dal.CloseConnection();
        }
    }
}
