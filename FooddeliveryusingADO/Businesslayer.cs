using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooddeliveryusingADO
{
    internal class Businesslayer
    {
        
            DataAccessLayer dal = new DataAccessLayer();
            Admin loggedinAdmin = null;

            public Businesslayer()
            {
                dal.OpenConnection();
            }

            public Admin AuthenticateAdmin(string email, string password)
            {
                loggedinAdmin = dal.LoginAdmin(email, password);
                return loggedinAdmin;
            }

            public void AddRestaurant(Restaurant restaurant)
            {
                if (loggedinAdmin != null)
                {
                    dal.AddRestaurant(restaurant);
                }
                else
                {
                    Console.WriteLine("Authentication required to add a restaurant.");
                }
            }

            public void AddMenuItem(Menu menu)
            {
                if (loggedinAdmin != null)
                {
                    dal.AddMenuItem(menu);
                }
                else
                {
                    Console.WriteLine("Authentication required to add a menu item.");
                }
            }

            public List<Restaurant> SearchRestaurantsByLocation(string location)
            {
                return dal.SearchRestaurantsByLocation(location);
            }

            public List<Menu> FilterItemsByPreference(string preference)
            {
                return dal.FilterItemsByPreference(preference);
            }

            public void PlaceOrder(Order order, List<OrderItem> orderItems)
            {
                
                
                    dal.PlaceOrder(order, orderItems);
               
            }

            public void UpdateOrderStatus(int orderId, string status)
            {
                if (loggedinAdmin != null)
                {
                    dal.UpdateOrderStatus(orderId, status);
                }
                else
                {
                    Console.WriteLine("Authentication required to update order status.");
                }
            }

            public void CloseApp()
            {
                dal.CloseConnection();
            }
        }
    }

