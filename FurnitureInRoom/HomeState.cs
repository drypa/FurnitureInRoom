using System;
using System.Collections.Generic;
using FurnitureInRoom.BusinessEntities;

namespace FurnitureInRoom
{
    public class HomeState
    {
        private Dictionary<DateTime, Home> _states;
        private Dictionary<DateTime, Home> States
        {
            get
            {
                return _states ?? (_states = new Dictionary<DateTime, Home>());
            }
        }

        public void CreateRoom(string roomName, DateTime date)
        {
            throw new NotImplementedException();
        }
        public void RemoveRoom(string roomName, string otherRoom, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IList<Room> GetRoomsList(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void CreateFurniture(string furnitureType,string roomName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void MoveFurniture(string furnitureType, string fromRoomName, string toRoomName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, Home> GetHistory()
        {
            throw new NotImplementedException();
        }
        public List<DateTime> GetHomeChangeDates()
        {
            throw new NotImplementedException();
        }
    }
}
