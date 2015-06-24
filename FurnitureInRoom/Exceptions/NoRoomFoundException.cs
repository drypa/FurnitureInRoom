using System;

namespace FurnitureInRoom.Exceptions
{
    public class NoRoomFoundException : Exception
    {
        public NoRoomFoundException(string roomName)
        {
            RoomName = roomName;
        }

        public string RoomName { get; set; }
    }
}
