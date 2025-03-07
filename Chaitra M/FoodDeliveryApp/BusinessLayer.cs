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

        public void CloseApp()
        {
            dal.Close();
            loggedInUser = null;
        }

        public bool Authenticate(string Email, string Password)
        {
            loggedInUser = dal.Login(Email, Password);
            return loggedInUser != null;
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

        public List<ResturantDTO> ListRestaurantsByLocation(string UserLocation)
        {
            return dal.ListRestaurantsByLocation(UserLocation);
        }

        public List<MenuDTO> GetRestaurantMenu(long RID)
        {
            return dal.GetRestaurantMenu(RID);
        }

        public List<MenuDTO> GetRestaurantMenu(string fltr, long RID)
        {
            List<MenuDTO> items = GetRestaurantMenu(RID);
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
                    foreach (OrderLineData mitm in menuLst)
                    {
                        bool tempStatus = dal.OrderMenuItem(NewOrderId, mitm.MenuId, mitm.Qty);
                        if (!tempStatus)
                        {
                            dal.EndTransaction(false);
                            return false;
                        }
                    }
                    dal.EndTransaction(true);
                    return true;
                }
            }
            catch (Exception)
            {
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
                return ExistingRole == reqRole.ToString();
            }
            return false;
        }
    }
}
