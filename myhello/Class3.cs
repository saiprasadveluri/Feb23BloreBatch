//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace myhello
//{




//    class Room
//    {
//        public string Name { get; set; }
//        public string Type { get; set; } // Conference or Training
//        public int Capacity { get; set; }
//        public bool IsAvailable { get; set; }
//        public string[] Participants { get; set; }
//        public int ParticipantCount { get; set; }

//        public Room(string name, string type, int capacity)
//        {
//            Name = name;
//            Type = type;
//            Capacity = capacity;
//            IsAvailable = true;
//            Participants = new string[capacity];
//            ParticipantCount = 0;
//        }
//    }

//    class FrontDesk
//    {
//        private Room[] rooms = new Room[10];
//        private int roomCount = 0;

//        public void Run()
//        {
//            while (true)
//            {
//                Console.WriteLine("\nMeeting Room Management System");
//                Console.WriteLine("1. Add New Room");
//                Console.WriteLine("2. Display Room List");
//                Console.WriteLine("3. Display Room Details");
//                Console.WriteLine("4. Update Room Availability");
//                Console.WriteLine("5. Delete Room");
//                Console.WriteLine("6. Find Room by Capacity and Availability");
//                Console.WriteLine("7. Add Participant to Room");
//                Console.WriteLine("8. List Participants in a Room");
//                Console.WriteLine("9. Exit");
//                Console.Write("Choose an option: ");

//                switch (Console.ReadLine())
//                {
//                    case "1": AddRoom(); break;
//                    case "2": DisplayRooms(); break;
//                    case "3": DisplayRoomDetails(); break;
//                    case "4": UpdateRoomAvailability(); break;
//                    case "5": DeleteRoom(); break;
//                    case "6": FindRoom(); break;
//                    case "7": AddParticipant(); break;
//                    case "8": ListParticipants(); break;
//                    case "9": return;
//                    default: Console.WriteLine("Invalid choice, try again."); break;
//                }
//            }
//        }

//        private void AddRoom()
//        {
//            if (roomCount >= rooms.Length)
//            {
//                Console.WriteLine("Cannot add more rooms.");
//                return;
//            }

//            Console.Write("Enter Room Name: ");
//            string name = Console.ReadLine();
//            Console.Write("Enter Room Type (Conference/Training): ");
//            string type = Console.ReadLine();
//            Console.Write("Enter Capacity: ");
//            int capacity = int.Parse(Console.ReadLine());

//            rooms[roomCount++] = new Room(name, type, capacity);
//            Console.WriteLine("Room added successfully.");
//        }

//        private void DisplayRooms()
//        {
//            for (int i = 0; i < roomCount; i++)
//                Console.WriteLine($"{i + 1}. {rooms[i].Name} ({rooms[i].Type}) - Capacity: {rooms[i].Capacity}, Available: {rooms[i].IsAvailable}");
//        }

//        private void DisplayRoomDetails()
//        {
//            Console.Write("Enter Room Index: ");
//            int index = int.Parse(Console.ReadLine()) - 1;
//            if (index < 0 || index >= roomCount)
//            {
//                Console.WriteLine("Invalid room index.");
//                return;
//            }
//            Room room = rooms[index];
//            Console.WriteLine($"Room: {room.Name}, Type: {room.Type}, Capacity: {room.Capacity}, Available: {room.IsAvailable}");
//        }

//        private void UpdateRoomAvailability()
//        {
//            Console.Write("Enter Room Index: ");
//            int index = int.Parse(Console.ReadLine()) - 1;
//            if (index < 0 || index >= roomCount)
//            {
//                Console.WriteLine("Invalid room index.");
//                return;
//            }
//            rooms[index].IsAvailable = !rooms[index].IsAvailable;
//            Console.WriteLine("Room availability updated.");
//        }

//        private void DeleteRoom()
//        {
//            Console.Write("Enter Room Index: ");
//            int index = int.Parse(Console.ReadLine()) - 1;
//            if (index < 0 || index >= roomCount)
//            {
//                Console.WriteLine("Invalid room index.");
//                return;
//            }
//            for (int i = index; i < roomCount - 1; i++)
//                rooms[i] = rooms[i + 1];
//            roomCount--;
//            Console.WriteLine("Room deleted.");
//        }

//        private void FindRoom()
//        {
//            Console.Write("Enter Minimum Capacity: ");
//            int minCapacity = int.Parse(Console.ReadLine());
//            Console.WriteLine("Available Rooms:");
//            for (int i = 0; i < roomCount; i++)
//            {
//                if (rooms[i].Capacity >= minCapacity && rooms[i].IsAvailable)
//                    Console.WriteLine($"{rooms[i].Name} ({rooms[i].Type}) - Capacity: {rooms[i].Capacity}");
//            }
//        }

//        private void AddParticipant()
//        {
//            Console.Write("Enter Room Index: ");
//            int index = int.Parse(Console.ReadLine()) - 1;
//            if (index < 0 || index >= roomCount)
//            {
//                Console.WriteLine("Invalid room index.");
//                return;
//            }
//            if (rooms[index].ParticipantCount >= rooms[index].Capacity)
//            {
//                Console.WriteLine("Room is full.");
//                return;
//            }
//            Console.Write("Enter Participant Name: ");
//            string participant = Console.ReadLine();
//            rooms[index].Participants[rooms[index].ParticipantCount++] = participant;
//            Console.WriteLine("Participant added.");
//        }

//        private void ListParticipants()
//        {
//            Console.Write("Enter Room Index: ");
//            int index = int.Parse(Console.ReadLine()) - 1;
//            if (index < 0 || index >= roomCount)
//            {
//                Console.WriteLine("Invalid room index.");
//                return;
//            }
//            Console.WriteLine("Participants:");
//            for (int i = 0; i < rooms[index].ParticipantCount; i++)
//                Console.WriteLine(rooms[index].Participants[i]);
//        }
//    }


//}
