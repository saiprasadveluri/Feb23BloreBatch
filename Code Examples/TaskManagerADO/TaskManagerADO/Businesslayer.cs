using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
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
            loggedinUser= dal.LoginUser(email, password);
            return loggedinUser;
        }

        public bool AddUser(UserDTO inp)
        {
            if (loggedinUser!=null && loggedinUser.RoleId==1)
            {
                return dal.AddUser(inp);
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
