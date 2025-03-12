using FoodDelApp.DTOs; // Ensure this is included
using System;
using System.Collections.Generic;

namespace FoodDelApp
{
    public class BusinessLayer
    {
        private DataAccessLayer dal;
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
                Console.WriteLine($"Authenticated: {loggedInUser.Name}, Role: {loggedInUser.RoleName}");
                return true;
            }
            else
            {
                Console.WriteLine("Authentication failed.");
                return false;
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            return dal.GetAllUsers();
        }

        public List<RestaurantDTO> GetAllRestaurants()
        {
            return dal.GetAllRestaurants();
        }

        public UserDTO GetUserById(int id)
        {
            return dal.GetUserById(id);
        }

        public bool UpdateUser(UserDTO user)
        {
            try
            {
                return dal.UpdateUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug: Exception in BusinessLayer.UpdateUser: {ex.Message}");
                return false;
            }
        }

        public RestaurantDTO GetRestaurantById(int id)
        {
            return dal.GetRestaurantById(id);
        }

        public bool UpdateRestaurant(RestaurantDTO restaurant)
        {
            try
            {
                return dal.UpdateRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug: Exception in BusinessLayer.UpdateRestaurant: {ex.Message}");
                return false;
            }
        }

        public bool AddNewUser(UserDTO user)
        {
            try
            {
                return dal.AddNewUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug: Exception in BusinessLayer.AddNewUser: {ex.Message}");
                return false;
            }
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            try
            {
                return dal.AddNewRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug: Exception in BusinessLayer.AddNewRestaurant: {ex.Message}");
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                return dal.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug: Exception in BusinessLayer.DeleteUser: {ex.Message}");
                return false;
            }
        }

        public bool DeleteRestaurant(int Id)
        {
            return dal.DeleteRestaurant(Id);
        }

        public List<UserDTO> GetRestaurantOwnersList()
        {
            return dal.GetRestaurantOwnersList();
        }

        public List<RestaurantDTO> ListMyRestaurants()
        {
            if (loggedInUser == null)
            {
                throw new InvalidOperationException("No user is logged in.");
            }

            return dal.ListRestaurantsByOwner(loggedInUser.UserId);
        }

        public List<RestaurantDTO> ListRestaurantsByOwner(int OwnerId)
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

        public List<MenuItemDTO> GetRestaurentMenu(int RID)
        {
            return dal.GetRestaurentMenu(RID);
        }

        public List<MenuItemDTO> GetRestaurentMenu(string fltr, int RID)
        {
            List<MenuItemDTO> items = GetRestaurentMenu(RID);
            return items.FindAll(i => i.FoodType == fltr);
        }

        public bool PlaceOrder(int RID, List<OrderLineData> menuLst)
        {
            try
            {
                int NewOrderId;
                dal.BeginTrans();
                bool OrderInitiated = dal.InitOrder(RID, loggedInUser.UserId, out NewOrderId);
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

        public bool AddMenuItem(MenuItemDTO itm)
        {
            return dal.AddMenuItem(itm);
        }

        private bool IsInRole(UserTypeEnum reqRole)
        {
            return loggedInUser != null && loggedInUser.RoleName == reqRole.ToString();
        }

        private bool IsInRole(int UserId, UserTypeEnum reqRole)
        {
            string ExistingRole = dal.GetUserRole(UserId);
            return ExistingRole != null && ExistingRole == reqRole.ToString();
        }
    }
}

