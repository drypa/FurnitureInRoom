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
            Home home = GetStateByDate(date);
            home.CreateRoom(roomName);
        }
        public void RemoveRoom(string roomName, string otherRoom, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IList<Room> GetRoomsList(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void CreateFurniture(string furnitureType, string roomName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void MoveFurniture(string furnitureType, string fromRoomName, string toRoomName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, Home> GetHistory()
        {
            //TODO: need clone to prevent collection change
            return States;
        }
        public List<DateTime> GetHomeChangeDates()
        {
            throw new NotImplementedException();
        }

        #region helpers

        private Home GetStateByDate(DateTime date)
        {
            Home result;
            if (States.TryGetValue(date, out result))
            {
                return result;
            }
            //TODO: need copy prev. home state
            result = new Home((sender, added) => { },(sender, removed, room) => { });

            States.Add(date, result);

            return result;
        }

        #endregion helpers
    }
}
