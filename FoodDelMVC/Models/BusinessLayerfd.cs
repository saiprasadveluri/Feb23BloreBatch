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

        public List<usersDTO> ListUsers()
        {
            List<usersDTO> ss = dal.Listusers();
            return ss;
        }

        public List<usersDTO> getowners()
        {
            List<usersDTO> ss = dal.getowners();
            return ss;
        }

        public List<restaurantDTO> ListRest()
        {
            List<restaurantDTO> ll = dal.Listrestaurant();
            return ll;
        }

        public bool DeleteRest(long Id)
        {
            return dal.deleterestaurant(Id);
        }
        public bool AddUser(usersDTO inp)
        {
            
                return dal.Adduser(inp);
            
            
        }

        public bool EditUser(usersDTO inp)
        {
            return dal.edituser(inp);
        }

        public bool EditRest(restaurantDTO res)
        {
            return dal.editrestaurant(res);
        }
        public bool DelUser(long Id)
        {
            return dal.DeleteUser(Id);
        }
        public bool Addrestaurant(restaurantDTO res)
        {
            
                return dal.Addrestaurant(res);
           
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
