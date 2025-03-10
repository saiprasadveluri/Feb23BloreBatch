using FoodDeliveryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelApp
{
    public class BusinessLayer
    {
        DataAccessLayer dal;
        UserDTO loggedInUser;
        public BusinessLayer()
        {
            dal = new DataAccessLayer();
        }

        public void CloseApp()
        {
            dal.CloseApp();
            loggedInUser = null;
        }
        public bool Authenticate(string Email, string Password)
        {
            loggedInUser = dal.Login(Email, Password);
            if (loggedInUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddNewRestaurant(ResturantDTO restaurant)
        {
            if (IsInRole(UserTypeEnum.ADMIN))
            {
                if (IsInRole(restaurant.Ownerid, UserTypeEnum.OWNER))
                    return dal.AddNewRestaurant(restaurant);
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

        public List<UserDTO> GetRestaurantOwnersList()
        {
            return dal.GetRestaurantOwnersList();
        }

        public List<ResturantDTO> ListMyRestaurants()
        {
            return ListRestaurantsByOwner(loggedInUser.Userid);
        }
        public List<ResturantDTO> ListRestaurantsByOwner(long OwnerId)
        {
            return dal.ListRestaurantsByOwner(OwnerId);
        }

        public List<ResturantDTO> ListRestaurantsNearMe()
        {
            return ListRestaurantsByLocation(loggedInUser.Location);
        }

        public List<ResturantDTO> ListRestaurantsByLocation(string UserLocacation)
        {
            return dal.ListRestaurantsByLocation(UserLocacation);
        }
        public List<MenuDTO> GetRestaurentMenu(long RID)
        {
            return dal.GetRestaurentMenu(RID);
        }
        public List<MenuDTO> GetRestaurentMenu(string fltr, long RID)
        {
            List<MenuDTO> items = GetRestaurentMenu(RID);
            List<MenuDTO> fitems = items.Where(i => i.FoodType == fltr).ToList();
            return fitems;
        }

        public bool PlaceOrder(long RID, List<OrderlistDTO> menuLst)
        {
            try
            {
                long NewOrderId;
                dal.BeginTrans();
                bool OrderInitiated = dal.InitOrder(RID, loggedInUser.Userid, out NewOrderId);
                if (OrderInitiated)
                {
                    foreach (OrderlistDTO mitm in menuLst)
                    {
                        bool tempStatus = dal.OrderMenuItem(NewOrderId, mitm.Menuid, mitm.Qty);
                        if (tempStatus == false)
                        {
                            dal.EndTransaction(false);
                            return false;
                        }
                    }
                    dal.EndTransaction(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex.Message);
                dal.EndTransaction(false);
            }
            dal.EndTransaction(false);
            return false;
        }

        public bool AddMenuItem(MenuDTO itm)
        {
            return dal.AddMenuItem(itm);
        }
        private bool IsInRole(UserTypeEnum reqRole)
        {
            if (loggedInUser != null)
            {
                return loggedInUser.RoleName == reqRole.ToString();
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
