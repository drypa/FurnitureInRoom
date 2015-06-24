using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureInRoom.Events;
using FurnitureInRoom.Exceptions;

namespace FurnitureInRoom.BusinessEntities
{
    public sealed class Home
    {
        public Home(HomeChangedEventHandler changedEventHandler = null)
        {
            Changed += changedEventHandler;
        }

        public event HomeChangedEventHandler Changed;

        private void OnChanged(Home home)
        {
            HomeChangedEventHandler handler = Changed;
            if (handler != null) handler(this, home);
        }


        private List<Room> _rooms;
        private List<Room> Rooms
        {
            get { return _rooms ?? (_rooms = new List<Room>()); }
        }

        public event RoomAddedEventHandler RoomAdded;
        private void OnRoomAdded(Room roomadded)
        {
            RoomAddedEventHandler handler = RoomAdded;
            if (handler != null) handler(this, roomadded);
        }

        public event RoomRemovedEventHandler RoomRemoved;

        private void OnRoomRemoved(Room roomRemoved, Room anotherRoom)
        {
            RoomRemovedEventHandler handler = RoomRemoved;
            if (handler != null) handler(this, roomRemoved, anotherRoom);
        }

        public Room CreateRoom(string name)
        {
            Room newRoom = new Room(name, (sender, added) => OnChanged(this), (sender, removed, room) => OnChanged(this));
            Rooms.Add(newRoom);
            OnRoomAdded(newRoom);
            return newRoom;
        }

        public void RemoveRoom(string roomName, string anotherRoomName)
        {
            Room roomToDelete = FindRoom(roomName);
            if (roomToDelete == null)
            {
                throw new NoRoomFoundException(roomName);
            }
            Room anotherRoom = FindRoom(anotherRoomName);
            if (roomToDelete.GetFurnitures().Count != 0)
            {
                if (anotherRoom == null)
                {
                    throw new NoRoomFoundException(anotherRoomName);
                }
                roomToDelete.MoveAll(anotherRoom);
            }
      
            Rooms.Remove(roomToDelete);
            OnRoomRemoved(roomToDelete, anotherRoom);
        }

        public Room FindRoom(string roomName)
        {
            return Rooms.FirstOrDefault(room => room.Name == roomName);
        }

        public ReadOnlyCollection<Room> GetRooms()
        {
            return new ReadOnlyCollection<Room>(Rooms);
        }

        public Home Clone()
        {
            var clonedHome = new Home();
            foreach (Room room in Rooms)
            {
                clonedHome.Rooms.Add(room.Clone());
            }
            return clonedHome;
        }
    }
}
