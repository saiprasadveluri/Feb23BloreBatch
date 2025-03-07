using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccessLayer dal = new DataAccessLayer();
            //dal.OpenConnection();
            BusinessLayer bl = new BusinessLayer();
            
            while (true)
            {
                Console.WriteLine("1 - New User Registration\n2 - User Login ");
                if (int.TryParse(Console.ReadLine(), out int op))
                {
                    switch (op)
                    {
                        case 1:
                            Console.WriteLine("NEW USER REGISTRATION");
                            Console.WriteLine("Email Id:");
                            string newemail = Console.ReadLine();
                            if (dal.CheckEmail(newemail))
                            {

                                Console.WriteLine("Enter username:");
                                string username = Console.ReadLine();
                                Console.WriteLine("Enter Location:");
                                string location = Console.ReadLine();
                                Console.WriteLine("Enter role :");
                                string role = Console.ReadLine();
                                Console.WriteLine("Set Password");
                                string password1 = Console.ReadLine();

                                Users newuser = new Users();
                                newuser.username = username;
                                newuser.email = newemail;
                                newuser.password = password1;
                                newuser.location = location;
                                newuser.role = role;
                                if (dal.AddUser(newuser))
                                {
                                    Console.WriteLine("Registered Succesfully");
                                }
                                else
                                {
                                    Console.WriteLine("Unable to Register!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("An account with the email already exists!");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Email Id:");
                            string email = Console.ReadLine();
                            Console.WriteLine("Password :");
                            string password = Console.ReadLine();
                            if (bl.Authenticate(email, password))
                            {
                                Console.WriteLine("Successfully logged In");
                                while (true)
                                {
                                    Console.WriteLine("~~~~~MENU~~~~~");
                                    Console.WriteLine("List of Options___________________\n0. Exit\n1. Add new restaurant\n2. Remove a restaurant\n3. Assign Owner\n4. Add MenuItem \n5. Search Restaurant \n6. Preferences\n7. Place order for a given restaurant\n8. order status.");
                                    if (int.TryParse(Console.ReadLine(), out int ch))
                                    {
                                        switch (ch)
                                        {
                                            case 0:
                                                dal.CloseApp();
                                                break;

                                            case 1:
                                                if ((bl.LoggedInUser.role).ToLower()== "admin")
                                                {
                                                    Console.WriteLine("Enter Restaurant :");
                                                    string restaurantname = Console.ReadLine();
                                                    Console.WriteLine("Enter Location :");
                                                    string location = Console.ReadLine();
                                                    
                                                    if (bl.AddRestaurant(restaurantname, location, bl.LoggedInUser.userid))
                                                    {
                                                        Console.WriteLine("Restaurant Added Successfully");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Unable to add Restaurant");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("You are not authorized to perform this operation");
                                                }
                                                break;

                                            case 2:
                                                if ((bl.LoggedInUser.role).ToLower() == "admin")
                                                {
                                                    Console.WriteLine("Enter Restaurant ID :");
                                                    long restaurantid = Convert.ToInt64(Console.ReadLine());
                                                    if (bl.RemoveRestaurant(restaurantid))
                                                    {
                                                        Console.WriteLine("Restaurant Removed Successfully");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Unable to remove Restaurant");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("You are not authorized to perform this operation");
                                                }
                                                break;

                                            case 3:
                                                if ((bl.LoggedInUser.role).ToLower() == "admin")
                                                {
                                                    Console.WriteLine("Enter Restaurant ID :");
                                                    long restaurantid = Convert.ToInt64(Console.ReadLine());
                                                    Console.WriteLine("Enter Owner ID :");
                                                    long ownerid = Convert.ToInt64(Console.ReadLine());
                                                    if (dal.AssignOwner(restaurantid, ownerid))
                                                    {
                                                        Console.WriteLine("Owner Assigned Successfully");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Unable to assign Owner");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("You are not authorized to perform this operation");
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("error Logging in");
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    break;
                }
            }

            

            //dal.CloseConnection();
        }
    }
}
