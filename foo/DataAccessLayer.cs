using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FoodDelApp.Data
{
    public class DataAccessLayer
    {
        private const string ConString = "Data Source=.;Initial Catalog=MFoodDelDB;Integrated Security=SSPI";
        private SqlConnection con = null;
        private SqlTransaction trans = null;

        public DataAccessLayer()
        {
            con = new SqlConnection(ConString);
            con.Open();
        }

        public void BeginTrans()
        {
            trans = con.BeginTransaction();
        }

        public void EndTransaction(bool commit)
        {
            if (commit)
                trans.Commit();
            else
                trans.Rollback();

            trans = null;
        }

        public void CloseApp()
        {
            con.Close();
        }

        public UserDTO Login(string email, string password)
        {
            // Implement login logic
            return new UserDTO(); // Placeholder
        }

        public bool AddNewRestaurant(RestaurantDTO restaurant)
        {
            // Implement add new restaurant logic
            return true; // Placeholder
        }

        public bool AddNewUser(UserDTO user)
        {
            // Implement add new user logic
            return true; // Placeholder
        }

        public List<RestaurantDTO> ListRestaurantsByLocation(string location)
        {
            // Implement list restaurants by location logic
            return new List<RestaurantDTO>(); // Placeholder
        }

        public List<RestaurantDTO> ListRestaurantsByOwner(long ownerId)
        {
            // Implement list restaurants by owner logic
            return new List<RestaurantDTO>(); // Placeholder
        }

        public List<MenuItemDTO> GetRestaurentMenu(long RID)
        {
            // Implement get restaurant menu logic
            return new List<MenuItemDTO>(); // Placeholder
        }

        public bool PlaceOrder(long RID, long userId, List<OrderLineData> menuLst)
        {
            // Implement place order logic
            return true; // Placeholder
        }

        public bool AddMenuItem(MenuItemDTO itm)
        {
            // Implement add menu item logic
            return true; // Placeholder
        }
    }
}
