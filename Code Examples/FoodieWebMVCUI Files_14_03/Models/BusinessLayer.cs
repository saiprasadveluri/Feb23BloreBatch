using FoodDelApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FoodDelApp
{    
    public class BusinessLayer:IDisposable
    {
        DataAccessLayer dal;  
        
        public BusinessLayer() 
        {
            dal=new DataAccessLayer();           
        }

        public void CloseSession()
        {
            if (dal != null)
            {
                dal.CloseSession();
            }          
            
        }
        public bool Authenticate(string Email, string Password,out UserDTO loggedInUser)
        {
            loggedInUser= dal.Login(Email, Password);
            if(loggedInUser!=null)            
            {
                 
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<UserDTO> GetAllusers()
        {
            return dal.GetAllUsers();
        }

        public UserDTO GetUserById(long UID)
        {
            return dal.GetUserById(UID);
        }

        public bool EditUser(UserDTO user)
        {
            return dal.EditUser(user);
        }

        public bool AddNewRestaurant(/*UserDTO loggedInuser,*/RestaurantDTO restaurant)
        {
            /*if (IsInRole(loggedInuser,UserTypeEnum.ADMIN))
            {
                if (IsInRole(restaurant.OwnerId, UserTypeEnum.OWNER))
                    return dal.AddNewRestaurant(restaurant);
                else
                    return false;
            }
            else
                return false;*/
            return dal.AddNewRestaurant(restaurant);
        }

        public bool DeleteRestaurant(long RID)
        {
            return dal.DeleteRestaurant(RID);
        }
        public bool AddNewUser(/*UserDTO loggedInuser,*/ UserDTO user)
        {
            /*if (IsInRole(loggedInuser,UserTypeEnum.ADMIN))
                return dal.AddNewUser(user);
            else
                return false;*/
            return dal.AddNewUser(user);
        }

        public bool DeleteUser(long UserId)
        {
            return dal.DeleteUser(UserId);
        }

        public List<UserDTO> GetRestaurantOwnersList()
        {
            return dal.GetRestaurantOwnersList();
        }

        public List<RestaurantDTO> ListMyRestaurants(UserDTO loggedInUser)
        {
            
            if (loggedInUser != null)
                return ListRestaurantsByOwner(loggedInUser.UserId);
            else
                return null;
        }
        public List<RestaurantDTO> ListRestaurantsByOwner(long OwnerId)
        {
            return dal.ListRestaurantsByOwner(OwnerId);
        }

        public List<RestaurantDTO> ListRestaurantsNearMe(UserDTO loggedInUser)
        {            
            if(loggedInUser != null)
            return ListRestaurantsByLocation(loggedInUser.Location);
            else
                return null;
        }

        public List<RestaurantDTO> ListRestaurantsByLocation(string UserLocacation)
        {
            return dal.ListRestaurantsByLocation(UserLocacation);
        }

        public List<RestaurantDTO> GetAllRestaurants()
        {
            return dal.ListRestaurants();
        }
        public List<MenuItemDTO> GetRestaurentMenu(long RID)
        {
            return dal.GetRestaurentMenu(RID);
        }
        public List<MenuItemDTO> GetRestaurentMenu(string fltr,long RID)
        {
            List<MenuItemDTO> items = GetRestaurentMenu(RID);
            List<MenuItemDTO> fitems=items.Where(i=>i.FoodType== fltr).ToList();
            return fitems;
        }

        public bool PlaceOrder(UserDTO loggedInUser, long RID, List<OrderLineData> menuLst)
        {
            try
            {
                long NewOrderId;
                dal.BeginTrans();
                if (loggedInUser == null)
                    return false;
                bool OrderInitiated = dal.InitOrder(RID, loggedInUser.UserId,out NewOrderId);
                if (OrderInitiated)
                {
                    foreach(OrderLineData mitm in menuLst)
                    {
                       bool tempStatus= dal.OrderMenuItem(NewOrderId, mitm.MenuId, mitm.Qty);
                        if(tempStatus==false)
                        {
                            dal.EndTransaction(false);
                            return false;
                        }
                    }
                    dal.EndTransaction(true);
                    return true;
                }
            }
            catch(Exception ex)
            {
                dal.EndTransaction(false);
            }
            dal.EndTransaction(false);
            return false;
        }

        public bool AddMenuItem(MenuItemDTO itm)
        {
            return dal.AddMenuItem(itm);
        }
        private bool IsInRole(UserDTO loggedInUser,UserTypeEnum reqRole)
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
            if(ExistingRole != null)
            {
                if(ExistingRole== reqRole.ToString())
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            CloseSession();
        }
    }
}
