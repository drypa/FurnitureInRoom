using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FurnitureInRoom.BusinessEntities
{
    public sealed class Home
    {
        private List<Room> _rooms;
        private List<Room> Rooms
        {
            get { return _rooms ?? (_rooms = new List<Room>()); }
        }


        public void CreateRoom()
        {
            Room newRoom = new Room();
            Rooms.Add(newRoom);
        }

        public void RemoveRoom(string roomName,Room anotherRoom)
        {
            Room roomToDelete = null;
            foreach (Room room in Rooms)
            {
                if (room.Name == roomName)
                {
                    roomToDelete = room;
                    room.MoveAll(anotherRoom);
                    break;
                }
            }
            if (roomToDelete != null)
            {
                Rooms.Remove(roomToDelete);
            }
        }

        public ReadOnlyCollection<Room> GetRooms()
        {
            return new ReadOnlyCollection<Room>(Rooms);
        }
    }
}
