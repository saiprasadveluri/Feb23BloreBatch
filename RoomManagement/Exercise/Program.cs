using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Frontdesk frontdesk = new Frontdesk();
            


            while (true)
            {
                Console.WriteLine("FrontDesk Application\n\n");
                Console.WriteLine("1. Add New Room");
                Console.WriteLine("2. Display Rooms List");
                Console.WriteLine("3. Display Room Details");
                Console.WriteLine("4. Update Room Availability");
                Console.WriteLine("5. Add Participant");
                Console.WriteLine("6. List Participants");
                Console.WriteLine("7. Find Rooms");
                Console.WriteLine("8. Delete Room");
                Console.WriteLine("9. Exit");
                Console.WriteLine("Enter your choice:");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Room ID:");
                        int roomID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Room Name:");
                        string roomName = Console.ReadLine();
                        Console.WriteLine("Enter Capacity:");
                        int capacity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Availability:");
                        bool isAvailable = Convert.ToBoolean(Console.ReadLine());
                        frontdesk.AddNewRoom(new Room { RoomID = roomID, RoomName = roomName, Capacity = capacity, IsAvailable = isAvailable });
                        break;
                    case 2:
                        frontdesk.DisplayRoomsList();
                        break;
                    case 3:
                        Console.WriteLine("Enter Room ID:");
                        int roomID1 = Convert.ToInt32(Console.ReadLine());
                        frontdesk.DisplayRoomDetails(roomID1);
                        break;
                    case 4:
                        Console.WriteLine("Enter Room ID:");
                        int roomID2 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Availability:");
                        bool isAvailable1 = Convert.ToBoolean(Console.ReadLine());
                        frontdesk.UpdateRoomAvailability(roomID2, isAvailable1);
                        break;
                    case 5:
                        Console.WriteLine("Enter Room ID:");
                        int roomID3 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Participant Name:");
                        string participantName = Console.ReadLine();
                        frontdesk.AddParticipant(roomID3, participantName);
                        break;
                    case 6:
                        Console.WriteLine("Enter Room ID:");
                        int roomID4 = Convert.ToInt32(Console.ReadLine());
                        frontdesk.ListParticipants(roomID4);
                        break;
                    case 7:
                        Console.WriteLine("Enter Capacity:");
                        int capacity1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Availability:");
                        bool isAvailable2 = Convert.ToBoolean(Console.ReadLine());
                        frontdesk.FindRooms(capacity1, isAvailable2);
                        break;
                    case 8:
                        Console.WriteLine("Enter Room ID:");
                        int roomID5 = Convert.ToInt32(Console.ReadLine());
                        frontdesk.DeleteRoom(roomID5);
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;

                }


                
            }
        }
    }
}