﻿
using System;
using System.Collections.Generic;

namespace FoodDeliveryAggregateApp
{
    class BusinessAccessLayer
    {
        DataAccessLayer dal; // Creating object of Data Access Layer
        public UserDTO loggedInUser;

        public BusinessAccessLayer()
        {
            dal = new DataAccessLayer();
        }

        public void CloseApp()
        {
            dal.CloseApp();
            loggedInUser = null;
        }

        public bool Authentication(string Email, string Password)
        {
            loggedInUser = dal.Login(Email, Password);
            return loggedInUser != null;
        }

        public bool AddUser(UserDTO user)
        {
            return dal.AddUser(user);
        }

        public bool AddRestaurantByAdmin(RestuarantDTO restuarant)
        {
            if (loggedInUser != null && loggedInUser.Rolename.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return dal.AddRestuarant(restuarant);
            }
            else
            {
                throw new UnauthorizedAccessException("Only admins can add restaurants.");
            }
        }

        public bool AssignOwnerByAdmin(long restuarantId, long ownerId)
        {
            if (loggedInUser != null && loggedInUser.Rolename.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return dal.AssignOwner(restuarantId, ownerId);
            }
            else
            {
                throw new UnauthorizedAccessException("Only admins can assign owners.");
            }
        }

        public bool AddMenuByOwner(MenuDTO menu)
        {
            if (loggedInUser != null && loggedInUser.Rolename.Equals("Owner", StringComparison.OrdinalIgnoreCase))
            {
                return dal.AddMenu(menu);
            }
            else
            {
                throw new UnauthorizedAccessException("Only owners can add menus.");
            }
        }

        public List<RestuarantDTO> GetRestuarants()
        {
            return dal.GetRestuarants();
        }

        public List<RestuarantDTO> GetRestuarantsByLocation(string location)
        {
            return dal.GetRestuarantsByLocation(location);
        }

        public List<MenuDTO> GetMenu(long RID)
        {
            return dal.GetMenu(RID);
        }

        public List<MenuDTO> GetMenuByFoodType(string foodType)
        {
            return dal.GetMenuByFoodType(foodType);
        }

        public bool PlaceOrderByUser(long restaurantId, long menuId)
        {
            if (loggedInUser != null && loggedInUser.Rolename.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                OrderDTO order = new OrderDTO
                {
                    RID = restaurantId,
                    OrderBy = loggedInUser.UserId,
                    Status = "Pending",
                    OrderDate = DateTime.Now
                };
                return dal.PlaceOrder(order);
            }
            else
            {
                throw new UnauthorizedAccessException("Only users can place orders.");
            }
        }

        public bool UpdateOrderStatus(long orderId, string status)
        {
            return dal.UpdateOrderStatus(orderId, status);
        }

        public List<OrderDTO> GetOrders()
        {
            return dal.GetOrders();
        }

        public OrderDTO ViewOrderByUser(long orderId)
        {
            return dal.GetOrderById(orderId);
        }
    }
}
