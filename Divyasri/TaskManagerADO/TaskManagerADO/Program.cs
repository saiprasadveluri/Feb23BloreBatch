

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    class Program
    {
        static Businesslayer dal = new Businesslayer();
        static void Main(string[] args)
        {
            try
            {
                dal.OpenConnection();
                List<UserDTO> lst = dal.ListUsers();

                foreach (UserDTO usr in lst)
                {
                    Console.WriteLine($"userid:{usr.UserId}-name:{usr.Name}- Department:{usr.Dept}");
                }

                Console.WriteLine("Enter 1.List User Details, 2. Add User");
                int opts = int.Parse(Console.ReadLine());
                switch (opts)
                {
                    case 1:
                        List<UserDTO> list = dal.ListUsers();
                        foreach (UserDTO user in list)
                        {
                            Console.WriteLine($"userid:{user.UserId}-name:{user.Name}- Department:{user.Dept}");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter User Name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Dept:");
                        string dept = Console.ReadLine();
                        Console.WriteLine("Enter Roleid:");
                        int roleid = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Email:");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter Password:");
                        string password = Console.ReadLine();

                        UserDTO newUser = new UserDTO
                        {
                            Name = name,
                            Dept = dept,
                            Roleid = roleid,
                            Email = email,
                            Password = password
                        };

                        bool res = dal.AddUser(newUser);
                        if (res)
                        {
                            Console.WriteLine("User Added");
                        }
                        else
                        {
                            Console.WriteLine("User not Added");
                        }
                        break;
                }

                dal.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}


