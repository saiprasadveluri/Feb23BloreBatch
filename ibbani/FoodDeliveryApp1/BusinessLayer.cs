using FoodDeliveryApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoodDeliveryApp1
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

    public bool AddNewRestaurant(RestaurantDTO restaurant)
    {
        if (IsInRole(UserTypeEnum.ADMIN))
        {
            if (IsInRole(restaurant.OwnerId, UserTypeEnum.OWNER))
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

    public List<RestaurantDTO> ListMyRestaurants()
    {
        return ListRestaurantsByOwner(loggedInUser.UserId);
    }
    public List<RestaurantDTO> ListRestaurantsByOwner(long OwnerId)
    {
        return dal.ListRestaurantsByOwner(OwnerId);
    }

    public List<RestaurantDTO> ListRestaurantsNearMe()
    {
        return ListRestaurantsByLocation(loggedInUser.Location);
    }

    public List<RestaurantDTO> ListRestaurantsByLocation(string UserLocacation)
    {
        return dal.ListRestaurantsByLocation(UserLocacation);
    }
    public List<MenuItemDTO> GetRestaurentMenu(long RID)
    {
        return dal.GetRestaurentMenu(RID);
    }
    public List<MenuItemDTO> GetRestaurentMenu(string fltr, long RID)
    {
        List<MenuItemDTO> items = GetRestaurentMenu(RID);
        List<MenuItemDTO> fitems = items.Where(i => i.FoodType == fltr).ToList();
        return fitems;
    }

    public bool PlaceOrder(long RID, List<OrderLineData> menuLst)
    {
        try
        {
            long NewOrderId;
            dal.BeginTrans();
            bool OrderInitiated = dal.InitOrder(RID, loggedInUser.UserId, out NewOrderId);
            if (OrderInitiated)
            {
                foreach (OrderLineData mitm in menuLst)
                {
                    bool tempStatus = dal.OrderMenuItem(NewOrderId, mitm.MenuId, mitm.Qty);
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
            dal.EndTransaction(false);
        }
        dal.EndTransaction(false);
        return false;
    }

    public bool AddMenuItem(MenuItemDTO itm)
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