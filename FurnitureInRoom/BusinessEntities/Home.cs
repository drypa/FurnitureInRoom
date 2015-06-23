﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using FurnitureInRoom.Events;

namespace FurnitureInRoom.BusinessEntities
{
    public sealed class Home
    {
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
            Room newRoom = new Room(name);
            Rooms.Add(newRoom);
            OnRoomAdded(newRoom);
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
                OnRoomRemoved(roomToDelete, anotherRoom);
            }
        }

        public ReadOnlyCollection<Room> GetRooms()
        {
            return new ReadOnlyCollection<Room>(Rooms);
        }
    }
}
