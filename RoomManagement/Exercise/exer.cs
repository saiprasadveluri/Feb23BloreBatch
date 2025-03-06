using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Participants { get; set; } = new List<string>();
    }

    public class Frontdesk
    {
        private List<Room> rooms = new List<Room>();

        public void AddNewRoom(Room room)
        {
            rooms.Add(room);
            Console.WriteLine($"Room {room.RoomName} added.");
        }

        public void DisplayRoomsList()
        {
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room ID: {room.RoomID} Room Name: {room.RoomName}");
            }
        }

        public void DisplayRoomDetails(int roomID)
        {
            var room = rooms.Find(r => r.RoomID == roomID);
            if (room != null)
            {
                Console.WriteLine($"Room ID: {room.RoomID}");
                Console.WriteLine($"Room Name: {room.RoomName}");
                Console.WriteLine($"Capacity: {room.Capacity}");
                Console.WriteLine($"Is Available: {room.IsAvailable}");
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
        public void UpdateRoomAvailability(int roomID, bool isAvailable)
        {
            var room = rooms.Find(r => r.RoomID == roomID);
            if (room != null)
            {
                room.IsAvailable = isAvailable;
                Console.WriteLine($"Room {room.RoomName} availability updated.");
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        public void DeleteRoom(int roomID)
        {
            var room = rooms.Find(r => r.RoomID == roomID);
            if ( room != null)
            {
                
                rooms.Remove(room);
                Console.WriteLine($"Room {room.RoomName} deleted.");
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        public void FindRooms(int capacity, bool isAvailable) 
        {
            var foundRooms = rooms.Where(r=> r.Capacity >= capacity && r.IsAvailable == isAvailable);
            foreach (var room in foundRooms)
            {
                Console.WriteLine($"Room ID: {room.RoomID} Room Name: {room.RoomName}");
            }
        }

        public void AddParticipant(int roomID, string participany)
        {
            var room = rooms.Find(r => r.RoomID == roomID);
            if (room != null && room.IsAvailable && room.Participants.Count < room.Capacity)
            {
                room.Participants.Add(participany);
                Console.WriteLine($"Participant {participany} added to room {room.RoomName}.");
            }
            else
            {
                Console.WriteLine("Room not found or room is full.");
            }
        }

        public void ListParticipants(int roomID)
        {
            var room = rooms.Find(r=> r.RoomID == roomID);
            if (room != null)
            {
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
