using FrontDesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk
{
    public abstract class room
    {
        public string RoomId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool Availability { get; set; }
        public string[] Participants { get; set; }
        public int ParticipantCount { get; set; }
        public abstract room(string roomId, string name, int capacity)
        {
            RoomId = roomId;
            Name = name;
            Capacity = capacity;
            Availability = true;
            Participants = new string[capacity];
            ParticipantCount = 0;
        }
        public bool AddParticipant(string participantName)
        {
            if (ParticipantCount < Capacity)
            {
                Participants[ParticipantCount] = participantName;
                ParticipantCount++;
                return true;
            }
            return false;
        }
        public void ListParticipants()
        {
            Console.WriteLine($"Participants:{Name}");
            for (int i = 0; i < ParticipantCount; i++)
            {
                Console.WriteLine(Participants[i]);
            }
        }
    

    public abstract void DisplayDetails();
    }
}


public class meetingRoom : room
{
    public meetingRoom(string roomId, string name, int capacity) : base(roomId, name, capacity)
    {
    }
    public override void DisplayDetails()
    {
        Console.WriteLine($"Room Id:{RoomId}-Capacity: {Capacity},Availablity:{Availabitlity}");
        
    }
}
