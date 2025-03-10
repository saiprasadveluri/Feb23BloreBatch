using System;
using System.Collections.Generic;

namespace FoodDeliveryDB
{
    public class BusinessLayer
    {
        public User Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Email and password cannot be empty.");
                return null;
            }

            // Additional security measures can be added here, such as hashing the password
            return DatabaseHelper.LoginUser(email, password);
        }

        public bool RegisterNewUser(User newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                Console.WriteLine("User details are incomplete.");
                return false;
            }

            // Additional security measures can be added here, such as validating email format
            return DatabaseHelper.AddUser(newUser);
        }

        public bool AddRestaurant(Restaurant restaurant)
        {
            if (restaurant == null || string.IsNullOrEmpty(restaurant.Name) || string.IsNullOrEmpty(restaurant.Location))
            {
                Console.WriteLine("Restaurant details are incomplete.");
                return false;
            }

            return DatabaseHelper.AddRestaurant(restaurant);
        }

        public bool RemoveRestaurant(int restaurantId)
        {
            if (restaurantId <= 0)
            {
                Console.WriteLine("Invalid restaurant ID.");
                return false;
            }

            return DatabaseHelper.RemoveRestaurant(restaurantId);
        }

        public bool AssignOwnerToRestaurant(int restaurantId, int ownerId)
        {
            if (restaurantId <= 0 || ownerId <= 0)
            {
                Console.WriteLine("Invalid restaurant or owner ID.");
                return false;
            }

            return DatabaseHelper.AssignOwnerToRestaurant(restaurantId, ownerId);
        }

        public bool AddMenuItem(MenuItem menuItem)
        {
            if (menuItem == null || string.IsNullOrEmpty(menuItem.Name) || menuItem.Price <= 0)
            {
                Console.WriteLine("Menu item details are incomplete.");
                return false;
            }

            return DatabaseHelper.AddMenuItem(menuItem);
        }

        public List<Restaurant> SearchRestaurantsByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                Console.WriteLine("Location cannot be empty.");
                return new List<Restaurant>();
            }

            return DatabaseHelper.SearchRestaurantsByLocation(location);
        }

        public List<MenuItem> FilterItemsByPreferences(string preference)
        {
            if (string.IsNullOrEmpty(preference))
            {
                Console.WriteLine("Preference cannot be empty.");
                return new List<MenuItem>();
            }

            return DatabaseHelper.FilterItemsByPreferences(preference);
        }

        public bool PlaceOrder(Order order)
        {
            if (order == null || order.UserId <= 0 || order.RestaurantId <= 0)
            {
                Console.WriteLine("Order details are incomplete.");
                return false;
            }

            return DatabaseHelper.PlaceOrder(order);
        }

        public List<Order> ListOrders()
        {
            return DatabaseHelper.ListOrders();
        }

        public bool UpdateOrderStatusToDelivered(int orderId)
        {
            if (orderId <= 0)
            {
                Console.WriteLine("Invalid order ID.");
                return false;
            }

            return DatabaseHelper.UpdateOrderStatusToDelivered(orderId);
        }
    }
}
