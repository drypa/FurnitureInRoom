using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureInRoom.Events;

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

        public void CreateRoom(string name)
        {
            Room newRoom = new Room(name, (sender, added) => OnChanged(this), (sender, removed, room) => OnChanged(this));
            Rooms.Add(newRoom);
            OnRoomAdded(newRoom);
        }

        public void RemoveRoom(string roomName,Room anotherRoom)
        {
            Room roomToDelete = FindRoom(roomName);
            if (roomToDelete == null) return;
            
            //TODO: make transactional
            roomToDelete.MoveAll(anotherRoom);
            Rooms.Remove(roomToDelete);
            OnRoomRemoved(roomToDelete, anotherRoom);
        }

        private Room FindRoom(string roomName)
        {
            return Rooms.FirstOrDefault(room => room.Name == roomName);
        }

        public ReadOnlyCollection<Room> GetRooms()
        {
            return new ReadOnlyCollection<Room>(Rooms);
        }

        #region helpers


        #endregion helpers

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
