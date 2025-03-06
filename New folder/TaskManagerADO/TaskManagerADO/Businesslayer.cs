using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    internal class Businesslayer
    {
        private DataAccess dal;
        private UserDTO loggedinuser;

        public Businesslayer()
        {
            dal = new DataAccess();
            dal.OpenConnection();
        }
        public UserDTO Authenticateuser(string email, string password)
        {
            loggedinuser = dal.Login(email, password);
            return loggedinuser;
        }

        public bool AddUser(UserDTO user)
        {
            if (loggedinuser != null && loggedinuser.RoleId == 1)
            {
                return dal.AddUser(user);
            }
            else
            {
                Console.WriteLine("You do not have permission to add a user.");
                return false;
            }
        }

        public void CloseApp()
        {

            dal.CloseConnection();
        }
    } 
}