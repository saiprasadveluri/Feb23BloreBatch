using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FooddeliveryADO
{
    public class Program
    {
        static BusinessLayer bl = new BusinessLayer();

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            usersDTO user = bl.AuthenticateUser(email, password);

            if (user != null)
            {
                Console.WriteLine("Login successful! Role: " + user.roledal);

                if (user.roledal == role.admin)
                {
                    Console.WriteLine("1. List Users");
                    Console.WriteLine("2. Add User");
                    Console.WriteLine("3. Add Restaurant");
                    Console.WriteLine("4. List Restaurants");
                    Console.WriteLine("5. delete Resturant");
                    Console.WriteLine("11.Assign owner to a restaurant");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ListUsers();
                            break;
                        case 2:
                            Addusers();
                            break;
                        case 3:
                            AddRestuarant();
                            break;
                        case 4:
                            ListRestaurants();
                            break;
                        case 11:
                            AssignOwner();
                            break;
                        default:

                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                else if (user.roledal == role.owner)
                {
                    Console.WriteLine("1.Add Restuarant");
                    Console.WriteLine("2. List Restaurants");
                    Console.WriteLine("3. Add Menu for his resturant");
                    Console.WriteLine("4. List Menus");
                    Console.WriteLine("5.List Orders");

                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddRestuarant();
                            break;
                        case 2:
                            ListRestaurants();
                            break;
                        case 3:
                            AddMenu();
                            break;
                        case 4:
                            ListMenus();
                            break;
                        case 5:
                            ListOrders();
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                else if (user.roledal == role.user)
                {
                    Console.WriteLine("1. List Restaurants by location");
                    Console.WriteLine("2. List Menus");
                    Console.WriteLine("3. Add Order");
                    Console.WriteLine("4. List Order Items");
                    Console.WriteLine("6. Get Menu by resturant");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            searchresbyloc();
                            break;
                        case 2:
                            ListMenus();
                            break;
                        case 3:
                            AddOrder();
                            break;
                        case 4:
                            ListOrders();
                            break;
                        case 5:
                            ListOrderItems();
                            break;
                        case 6:
                            GetMenuByRestaurant();
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid email or password");
            }
        }

        static void ListUsers()
        {
            try
            {
                List<usersDTO> users = bl.GetAllUsers();
                foreach (usersDTO user in users)
                {
                    Console.WriteLine(user.userid + " " + user.uname + " " + user.email + " " + user.password + " " + user.roledal + " " + user.ulocation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Addusers()
        {
            try
            {
                usersDTO user = new usersDTO();
                Console.WriteLine("Enter userid");
                user.userid = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter uname");
                user.uname = Console.ReadLine();
                Console.WriteLine("Enter email");
                user.email = Console.ReadLine();
                Console.WriteLine("Enter password");
                user.password = Console.ReadLine();
                Console.WriteLine("Enter role");
                user.roledal = (role)Enum.Parse(typeof(role), Console.ReadLine());
                Console.WriteLine("Enter location");
                user.ulocation = Console.ReadLine();
                bool result = bl.AddUser(user);
                if (result)
                    Console.WriteLine("User added successfully");
                else
                    Console.WriteLine("User not added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ListRestaurants()
        {
            List<restaurantsDTO> restaurants = bl.GetAllRestaurants();
            foreach (restaurantsDTO restaurant in restaurants)
            {
                Console.WriteLine(restaurant.rid + " " + restaurant.rname + " " + restaurant.rlocation + " " + restaurant.ownerid);
            }

        }
        public static void AddRestuarant()
        {
            restaurantsDTO restaurant = new restaurantsDTO();
            Console.WriteLine("Enter Restaurant ID");
            restaurant.rid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Restaurant Name");
            restaurant.rname = Console.ReadLine();
            Console.WriteLine("Enter Restaurant Location");
            restaurant.rlocation = Console.ReadLine();
            Console.WriteLine("Enter Owner ID");
            restaurant.ownerid = long.Parse(Console.ReadLine());
            bool result = bl.AddRestaurant(restaurant);
            if (result)
                Console.WriteLine("Restaurant added successfully");
            else
                Console.WriteLine("Restaurant not added");


        }
        public static void AddMenu()
        {
            menusDTO menu = new menusDTO();
            Console.WriteLine("Enter Menu ID");
            menu.menuid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Restaurant ID");
            menu.rid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Item Name");
            menu.itemname = Console.ReadLine();
            Console.WriteLine("Enter Price");
            menu.price = double.Parse(Console.ReadLine());
            bool result = bl.AddMenu(menu);
            if (result)
                Console.WriteLine("Menu added successfully");
            else
                Console.WriteLine("Menu not added");
        }
        public static void ListMenus()
        {
            List<menusDTO> menus = bl.GetAllMenus();
            foreach (menusDTO menu in menus)
            {
                Console.WriteLine(menu.menuid + " " + menu.rid + " " + menu.itemname + " " + menu.price);
            }
        }
        public static void AddOrder()
        {
            ordersDTO order = new ordersDTO();
            Console.WriteLine("Enter Order ID");
            order.orderid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter User ID");
            order.userid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Restaurant ID");
            order.rid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Order Status");
            order.orderstatus = Console.ReadLine();
            bool result = bl.AddOrder(order);
            if (result)
                Console.WriteLine("Order added successfully");
            else
                Console.WriteLine("Order not added");
        }
        public static void ListOrders()
        {
            List<ordersDTO> orders = bl.GetAllOrders();
            foreach (ordersDTO order in orders)
            {
                Console.WriteLine(order.orderid + " " + order.userid + " " + order.rid + " " + order.orderstatus);
            }
        }
        public static void ListOrderItems()
        {
            List<orderitemDTO> orderitems = bl.GetAllOrderItems();
            foreach (orderitemDTO orderitem in orderitems)
            {
                Console.WriteLine(orderitem.orderitemid + " " + orderitem.orderid + " " + orderitem.menuid + " " + orderitem.quantity);
            }
        }
        public static void AddOrderItem()
        {
            orderitemDTO orderitem = new orderitemDTO();
            Console.WriteLine("Enter Order Item ID");
            orderitem.orderitemid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Order ID");
            orderitem.orderid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Menu ID");
            orderitem.menuid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Quantity");
            orderitem.quantity = int.Parse(Console.ReadLine());
            bool result = bl.AddOrderItem(orderitem);
            if (result)
                Console.WriteLine("Order Item added successfully");
            else
                Console.WriteLine("Order Item not added");
        }
        public static void searchresbyloc()
        {
            Console.WriteLine("Enter location");
            string location = Console.ReadLine();
            List<restaurantsDTO> restaurants = bl.GetRestaurantsByOwner(location);
            foreach (restaurantsDTO restaurant in restaurants)
            {
                Console.WriteLine(restaurant.rid + " " + restaurant.rname + " " + restaurant.rlocation + " " + restaurant.ownerid);
            }
        }
        public static void AssignOwner()
        {
            Console.WriteLine("Enter Restaurant ID");
            long rid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Owner ID");
            long ownerid = long.Parse(Console.ReadLine());
            bool result = bl.AssignOwnerToRestaurant(rid, ownerid);
            if (result)
                Console.WriteLine("Owner assigned successfully");
            else
                Console.WriteLine("Owner not assigned");
        }
        public static void deleteresturant()
        {
            Console.WriteLine("Enter Restaurant ID");
            long rid = long.Parse(Console.ReadLine());
            bool result = bl.DeleteResturant(rid);
            if (result)
                Console.WriteLine("Restaurant deleted successfully");
            else
                Console.WriteLine("Restaurant not deleted");
        }
        public static void GetMenuByRestaurant()
        {
            Console.WriteLine("Enter Restaurant ID");
            long rid = long.Parse(Console.ReadLine());
            List<menusDTO> menus = bl.GetMenuByRestaurant(rid); 
            foreach (menusDTO menu in menus)
            {
                Console.WriteLine($"{menu.menuid} {menu.rid} {menu.itemname} {menu.price}");
            }
        }


    }
}