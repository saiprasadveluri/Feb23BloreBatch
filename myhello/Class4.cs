using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public enum RoomType
    {
        ConferenceRoom,
        TrainingRoom
    }

    public class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public RoomType RoomType { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Participants { get; set; }

        public Room(int roomId, string roomName, RoomType roomType, int capacity)
        {
            RoomID = roomId;
            RoomName = roomName;
            RoomType = roomType;
            Capacity = capacity;
            IsAvailable = true;
            Participants = new List<string>();
        }
    }

    public class FrontDesk
    {
        private List<Room> rooms = new List<Room>();
        private int nextRoomId = 1;

        public void AddNewRoom(string roomName, RoomType roomType, int capacity)
        {
            Room newRoom = new Room(nextRoomId++, roomName, roomType, capacity);
            rooms.Add(newRoom);
            Console.WriteLine($"Room '{roomName}' added successfully.");
        }

        public void DisplayRoomList()
        {
            Console.WriteLine("Room List:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"{room.RoomID}. {room.RoomName} - {room.RoomType} - Capacity: {room.Capacity} - Available: {room.IsAvailable}");
            }
        }

        public void DisplayRoomDetails(int roomId)
        {
            Room room = rooms.Find(r => r.RoomID == roomId);
            if (room != null)
            {
                Console.WriteLine($"Room Details for '{room.RoomName}':");
                Console.WriteLine($"ID: {room.RoomID}");
                Console.WriteLine($"Type: {room.RoomType}");
                Console.WriteLine($"Capacity: {room.Capacity}");
                Console.WriteLine($"Available: {room.IsAvailable}");
                Console.WriteLine("Participants:");
                foreach (var participant in room.Participants)
                {
                    Console.WriteLine(participant);
                }
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        public void UpdateRoomAvailability(int roomId, bool isAvailable)
        {
            Room room = rooms.Find(r => r.RoomID == roomId);
            if (room != null)
            {
                room.IsAvailable = isAvailable;
                Console.WriteLine($"Room '{room.RoomName}' availability updated to {isAvailable}.");
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        public void DeleteRoom(int roomId)
        {
            Room room = rooms.Find(r => r.RoomID == roomId);
            if (room != null)
            {
                rooms.Remove(room);
                Console.WriteLine($"Room '{room.RoomName}' deleted from database.");
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        public void FindRooms(int capacity, bool isAvailable)
        {
            Console.WriteLine("Matching Rooms:");
            foreach (var room in rooms)
            {
                if (room.Capacity >= capacity && room.IsAvailable == isAvailable)
                {
                    Console.WriteLine($"{room.RoomID}. {room.RoomName} - {room.RoomType} - Capacity: {room.Capacity} - Available: {room.IsAvailable}");
                }
            }
        }

        public void AddParticipantsToRoom(int roomId, string participant)
        {
            Room room = rooms.Find(r => r.RoomID == roomId);
            if (room != null)
            {
                if (room.Participants.Count < room.Capacity)
                {
                    room.Participants.Add(participant);
                    Console.WriteLine($"Participant '{participant}' added to room '{room.RoomName}'.");
                }
                else
                {
                    Console.WriteLine("Room is full.");
                }
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        public void ListParticipantsInRoom(int roomId)
        {
            Room room = rooms.Find(r => r.RoomID == roomId);
            if (room != null)
            {
                Console.WriteLine($"Participants in '{room.RoomName}':");
                foreach (var participant in room.Participants)
                {
                    Console.WriteLine(participant);
                }
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }
    }
}
    
        

    
