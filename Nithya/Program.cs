using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk
{
    class Program
    {
        static void Main(string[] args)
        {
            FrontDesk manager = new FrontDesk();
            while (true)
            {
                Console.WriteLine("1. Add Room");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Display Room Details");
                Console.WriteLine("4. Remove Room");
                Console.WriteLine("5.add participants");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter Room Name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Room Capacity");
                        int capacity = int.Parse(Console.ReadLine());
                        manager.AddRoom(name, capacity);
                        break;
                    case "2":
                        manager.DisplayRooms();
                        break;
                    case "3":
                        Console.WriteLine("Enter Room Name");
                        string roomName = Console.ReadLine();
                        manager.DisplayRoomDetails(roomName);
                        break;
                    case "4":
                        Console.WriteLine("Enter Room Name");
                        string roomName1 = Console.ReadLine();
                        manager.RemoveRoom(roomName1);
                        break;
                    case "5":
                        Console.WriteLine("Enter Room Name");
                        string roomName2 = Console.ReadLine();
                        Console.WriteLine("Enter Participant Name");
                        string participantName = Console.ReadLine();
                        manager.AddParticipant(roomName2, participantName);
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

            }
        }
    }
}

