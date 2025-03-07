using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    internal class Businesslayer
    {
        DataAccessLayer dal = new DataAccessLayer();
        UserDTO loggedinUser = null;

        public Businesslayer()
        {
            dal.OpenConnection();
        }

        public UserDTO AuthenticateUser(string email, string password)
        {
            loggedinUser = dal.LoginUser(email, password);
            return loggedinUser;
        }

        public bool AddUser(UserDTO inp)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1) // Site Admin check
            {
                return dal.AddUser(inp);
            }
            return false;
        }

        public bool CreateProject(ProjectDTO project)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 1) // Site Admin only
            {
                return dal.AddProject(project);
            }
            return false;
        }

        public bool CreateTask(TaskDTO task)
        {
            if (loggedinUser != null && loggedinUser.RoleId == 2) // Project Manager only
            {
                return dal.AddTask(task);
            }
            return false;
        }

        public bool AddComment(CommentDTO comment)
        {
            if (loggedinUser != null)
            {
                return dal.AddComment(comment);
            }
            return false;
        }

        public void CloseApp()
        {
            dal.CloseConnection();
        }

        public int GetLoggedInUserRole() => loggedinUser?.RoleId ?? 0;

        public long GetLoggedInUserId() => loggedinUser?.UserId ?? 0;
    }
}


