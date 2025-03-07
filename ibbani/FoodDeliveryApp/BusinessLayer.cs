using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class BusinessLayer
    {
        DataAccessLayer dal;
        UserDTO loggedInUser;

        public BusinessLayer()
        {
            dal = new DataAccessLayer();
        }

        public bool Authenticate(string email,string pswd)
        {
            loggedInUser = dal.Login(email, pswd);
            if(loggedInUser!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        
        public bool AddNewRestaurent(RestaurentDTO restaurent)
        {
            if (IsInRole(UserTypeEnum.ADMIN))
            {
                if (IsInRole(restaurent.Ownerid, UserTypeEnum.OWNER))
                    return dal.AddNewRestaurent(restaurent);
                else
                    return false;
            }
            else
                return false;
        }
        public bool AddNewUser(UserDTO user)
        {
            if (IsInRole(UserTypeEnum.ADMIN))
                return dal.AddNewUser(user);
            else
                return false;
        }

        private bool IsInRole(UserTypeEnum reqRole)
        {
            if (loggedInUser != null)
            {
                return loggedInUser.roleid == reqRole.ToString();
            }
            else
            {
                return false;
            }
        }

        private bool IsInRole(long UserId, UserTypeEnum reqRole)
        {
            string ExistingRole = dal.GetUserRole(UserId);
            if (ExistingRole != null)
            {
                if (ExistingRole == reqRole.ToString())
                    return true;
            }
            return false;
        }
    }
}

    }
}
