using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FooddeliveryApp
{
    public class Program
    {   static BusinessLayerfd blf=new BusinessLayerfd();
        static DataAcessLayer dal = new DataAcessLayer();
        public static void Main(string[] args)
        {
            Loginuser();
            
            //Adduser();
            blf.Closeapp();
            //dal.CloseConnection();
            //Listusers();

        }
        public static void Loginuser()
        {
            Console.WriteLine("Enter the email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter the password");
            string password = Console.ReadLine();
            usersDTO user = blf.AuthenticateUser(email, password);
            if(user.roledal == usersDTO.role.admin)
            {
                Console.WriteLine("Admin authenticated successfully");
                Console.WriteLine("1.Add user\n 2.ListUser \n3.AddRestaurent\n 4.ListRestaurant");
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Adduser();
                        break;
                    case 2:
                        
                    case 3: Addrestaurant();
                        break;
                    case 4:dal.OpenConnection();
                        Listrestaurant();
                        dal.CloseConnection();
                        break;
                    case 5:dal.OpenConnection();
                        deleterestaurant();
                        dal.CloseConnection();
                        break;
                    default:Console.WriteLine("Invalid option");
                        break;
                }
            }
            else if (user.roledal == usersDTO.role.user)
            {
                Console.WriteLine("User authenticated successfully");
                Console.WriteLine("1.ListRestaurant\n 2.AddOrder\n 3.ListOrderiems\n 4.restaurent Serach by location");
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Listrestaurant();
                        break;
                    case 2:
                        Addorders();
                        break;
                    case 3:
                        dal.OpenConnection();
                        Listorderitems();
                        dal.CloseConnection();
                        break;
                    case 4:
                        dal.OpenConnection();
                        Console.WriteLine("Enter the location");
                        string location = Console.ReadLine();
                        List<restaurantDTO> res = dal.Searchrestaurant(location);
                        foreach (var r in res)
                        {
                            Console.WriteLine(r.resid + " " + r.rname + " " + r.rlocation + " " + r.ownerid);
                        }
                        dal.CloseConnection();

                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            else if(user.roledal == usersDTO.role.owner)
            {
                Console.WriteLine("Restaurent Owner authenticated successfully");
                Console.WriteLine("1.AddOrderItems\n 2.Addmenu\n 3.Listorderitmes\n 4.Listorders \n");
                int option = int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                        addorderitems();
                        break;
                    case 2:addmenu(user.userid);
                        break;
                    case 3:Listorderitems();
                        break;
                        case 4:Listorders();
                        break;
                    
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            else
            {
                Console.WriteLine("User not authenticated");
            }
        }
        public static void deleterestaurant()
        {Console.WriteLine("Enter the restaurant id to delete");
            long resid = long.Parse(Console.ReadLine());
            bool res = dal.deleterestaurant(resid);
            if (res)
            {
                Console.WriteLine("Restaurant deleted successfully");
            }
            else
            {
                Console.WriteLine("Restaurant not deleted");
            }

        }
        public static void Listrestaurant()
        {
            List<restaurantDTO> res = dal.Listrestaurant();
            foreach (var r in res)
            {
                Console.WriteLine(r.resid + " " + r.rname + " " + r.rlocation + " " + r.ownerid);
            }
        }
        public static void Listusers()
        {
            List<usersDTO> users = dal.Listusers();
            foreach (var user in users)
            {
                Console.WriteLine(user.userid + " " + user.uname + " " + user.email + " " + user.password + " " + user.roledal + " " + user.ulocation);
            }

        }
        public static void Listorders()
        {
            List<ordersDTO> orders = dal.Listorders();
            foreach (var order in orders)
            {
                Console.WriteLine(order.orderid + " " + order.userid + " " + order.rid + " " + order.orderstatus);
            }
        }


        public static void Listorderitems()
        {
            List<orderitemDTO> orderitems = dal.Listorderitems();
            foreach (var orderitem in orderitems)
            {
                Console.WriteLine(orderitem.orderitemid + " " + orderitem.orderid + " " + orderitem.menuid + " " + orderitem.quantity);
            }
        }
        public  static void addmenu(long ans)
        {menusDTO menus = new menusDTO();
            Console.WriteLine("Enter the restaurant id");
            menus.rid = ans;
            Console.WriteLine("Enter the item name");
            menus.itemname = Console.ReadLine();
            Console.WriteLine("Enter the price");
            menus.price = long.Parse(Console.ReadLine());
            bool ores = blf.Addmenu(menus);
            if (ores)
            {
                Console.WriteLine("Menu added successfully");
            }
            else
            {
                Console.WriteLine("Menu not added");
            }

        }
        public static void addorderitems()
        {
            orderitemDTO orderitems = new orderitemDTO();
            
            Console.WriteLine("Enter the order id");
            orderitems.orderid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter the menuid id");
            orderitems.menuid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter the quantity");
            orderitems.quantity = long.Parse(Console.ReadLine());
            bool ores = blf.Addorderitems(orderitems);
            if (ores)
            {
                Console.WriteLine("Order added successfully");
            }
            else
            {
                Console.WriteLine("Order not added");
            }
        }
        public static void Addorders()
        {
            ordersDTO order = new ordersDTO();
            
            Console.WriteLine("Enter the user id");
            order.userid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter the restaurant id");
            order.rid = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter the order status");
            order.orderstatus =Console.ReadLine();
            bool ores=blf.Addorders(order);
            if (ores)
            {
                Console.WriteLine("Order added successfully");
            }
            else
            {
                Console.WriteLine("Order not added");
            }
        }
        public static void Addrestaurant()
        {
            restaurantDTO res = new restaurantDTO();
            
            Console.WriteLine("Enter the restaurant name");
            res.rname = Console.ReadLine();
            Console.WriteLine("Enter the location");
            res.rlocation = Console.ReadLine();
            Console.WriteLine("Enter the owner id");
            res.ownerid = long.Parse(Console.ReadLine());
            bool rres = blf.Addrestaurant(res);
            if (rres)
            {
                Console.WriteLine("Restaurant added successfully");
            }
            else
            {
                Console.WriteLine("Restaurant not added");
            }   
        }

        public static void Adduser()
        {
            usersDTO user = new usersDTO();
            
            Console.WriteLine("Enter the user name");
            user.uname = Console.ReadLine();
            Console.WriteLine("Enter the email");
            user.email = Console.ReadLine();
            Console.WriteLine("Enter the password");
            user.password = Console.ReadLine();
            Console.WriteLine("Enter the role");
            user.roledal = (usersDTO.role)Enum.Parse(typeof(usersDTO.role), Console.ReadLine());
            Console.WriteLine("Enter the location");
            user.ulocation = Console.ReadLine();
            bool res=blf.AddUser(user);
            if (res)
            {
                Console.WriteLine("User added successfully");
            }
            else
            {
                Console.WriteLine("User not added");
            }
        }

    }
}

