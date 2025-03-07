using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooddeliveryApp
{
    public class BusinessLayerfd
    {
        DataAcessLayer dal = new DataAcessLayer();
        usersDTO Loggineduser = new usersDTO();
        public BusinessLayerfd()
        {
            dal.OpenConnection();
        }
        public void Closeapp()
        {
            dal.CloseConnection();
        }


        public usersDTO AuthenticateUser(string email, string password)
        {
            Loggineduser = dal.LoginUser(email, password); ;
            return Loggineduser;
        }
        public bool AddUser(usersDTO inp)
        {
            if (Loggineduser != null && Loggineduser.roledal == usersDTO.role.admin)
            {
                return dal.Adduser(inp);
            }
            else
            {
                return false;
            }
        }
        public bool Addrestaurant(restaurantDTO res)
        {
            if (Loggineduser != null && Loggineduser.roledal == usersDTO.role.admin)
            {
                return dal.Addrestaurant(res);
            }
            else
            {
                return false;
            }
        }
        public bool Addorders(ordersDTO ord)
        {
            if (Loggineduser != null && Loggineduser.roledal == usersDTO.role.user)
            {
                return dal.Addorders(ord);
            }
            else
            {
                return false;
            }
        }
        public bool Addorderitems(orderitemDTO ordit)
        {
            if (Loggineduser != null && Loggineduser.roledal == usersDTO.role.owner)
            {
                return dal.Addorderitems(ordit);
            }
            else
            {
                return false;
            }
        }
        public bool Addmenu(menusDTO menu)
        {
            if (Loggineduser != null && Loggineduser.roledal == usersDTO.role.owner)
            {
                return dal.Addmenu(menu);
            }
            else
            {
                return false;
            }
        }
    }
}
