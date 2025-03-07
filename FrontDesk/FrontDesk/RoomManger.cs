using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk
{
    public class RoomManger
    {
        private Room[] rooms = new Room[10];
        public int roomIdCount = 1;
        public void AddRoom(string name, int Capacity)
        {
            if (roomIdCount < rooms.Length)
            {
                rooms[roomIdCount] = new Room(roomIdCount + 1, name, Capacity, true, new string[0]);
                roomIdCount++;
                Console.WriteLine("Room added successfully");
            }
            else
            {
                Console.WriteLine("No more rooms can be added");


            }
        }
        public void RemoveRoom(string name)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].Roomname == name)
                {
                    rooms[i] = null;
                    Console.WriteLine("Room removed successfully");
                    break;
                }
            }
        }
        public void DisplayRooms()
        {
            if (roomIdCount == 1)
            {
                Console.WriteLine("No rooms available");
                return;
            }
            Console.WriteLine("/RoomList/");
            for (int i = 0; i < rooms.Length; i++)
            {
                Console.WriteLine($"[{rooms[i].RoomId}] {rooms[i].Roomname}- Capacity:{rooms[i].Capacity}-Availabilty:{rooms[i].Availability}");
            }
        }

    }
}
