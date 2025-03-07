
using System.Collections.Generic;

namespace TaskManagerADO
{
    class Businesslayer
    {
        DataAccessLayer da = new DataAccessLayer();
        UserDTO loggedinUser = null;

        public void OpenConnection()
        {
            da.OpenConnection();
        }

        public void CloseConnection()
        {
            da.CloseConnection();
        }

        public List<UserDTO> ListUsers()
        {
            return da.ListUsers();
        }

        public bool AddUser(UserDTO inp)
        {
            return da.AddUser(inp);
        }

        public UserDTO LoginUser(string Email, string Password)
        {
            return da.LoginUser(Email, Password);
        }
    }
}
