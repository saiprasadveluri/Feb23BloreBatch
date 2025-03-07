using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using FoodDeliveryApp;

namespace TaskManagerADO
{
    class Program
    {
        static SqlConnection con;
        static BusinessLayer ba;
        static void Main(string[] args)
        {
            
            try
            {
                BusinessLayer business = new BusinessLayer();
                Console.Write("Enter Email  ");
                string Email = Console.ReadLine();
                Console.Write("enter Password  ");
                string Password = Console.ReadLine();
                UserDTO usr = business.AuthenticationUser(Email, Password.Trim());
                if (usr != null)
                {
                    Console.WriteLine($"\n Welcome! {usr.UserName}");
                }
                else
                {
                    Console.WriteLine("\n Not Authorized to Access the application");
                }

                Console.WriteLine("1.Add Restaurant  2.Add User  3. Delete Restaurant");
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        Console.WriteLine("enter name");
                        string RName = Console.ReadLine();
                        Console.WriteLine("enter loc");
                        string Rloc = Console.ReadLine();
                        Console.WriteLine("enter OwnerId");
                        long OwnId = long.Parse(Console.ReadLine());

                        RestaurentsDTO res = new RestaurentsDTO();
                        UserDTO us = new UserDTO();
                        res.ResName = RName;
                        res.ResLoc = Rloc;
                        res.OwnerID = OwnId;
                        us.UserID = OwnId;

                        bool check = business.CheckOwnerId(us);
                        bool stat = false;
                        if (check)
                        {
                            stat = business.AddRestaurant(res);
                        }
                        else
                        {
                            Console.WriteLine("User does not exists. Create user first");
                        }
                        if (stat)
                        {
                            Console.WriteLine("Restaurant added");
                        }
                        else
                        {
                            Console.WriteLine("Not Authorized to add Restaurant");
                        }
                        break;

                    case 2:
                        Console.WriteLine("enter name");
                        string UserName = Console.ReadLine();
                        Console.WriteLine("enter mail");
                        string UserEmail = Console.ReadLine();
                        Console.WriteLine("enter roleId");
                        long RoleId = long.Parse(Console.ReadLine());
                        Console.WriteLine("enter pass");
                        string UserPassword = Console.ReadLine();
                        Console.WriteLine("enter loc");
                        string Userloc = Console.ReadLine();


                        UserDTO user = new UserDTO();
                        user.UserName = UserName;
                        user.Email= UserEmail;
                        user.Password = UserPassword;
                        user.RoleId = RoleId;
                        user.UserLoc = Userloc;
                        bool status = business.AddUser(user);
                        if (status)
                        {
                            Console.WriteLine("Created");
                        }
                        else
                        {
                            Console.WriteLine("Not Enough Credential");
                        }

                        break;

                    case 3:
                        Console.WriteLine("enter Restaurant ID");
                        long RestID = long.Parse(Console.ReadLine());
                        RestaurentsDTO res1 = new RestaurentsDTO();

                        res1.ResId = RestID;
                        bool s = business.DeleteRestaurant(res1);
                        if (s)
                        {
                            Console.WriteLine("Restaurent deleted");
                        }
                        else
                        {
                            Console.WriteLine("Could not delete restaurant");
                        }

                        break;

                    default:
                        Console.WriteLine("wrong authenication");
                        break;
                }


                business.CloseApp();

            }
            finally
            {

            }
            

        }
    }
}
