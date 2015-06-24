using System;
using System.Collections.Generic;
using System.Linq;
using FurnitureInRoom.BusinessEntities;
using FurnitureInRoom.Events;

namespace FurnitureInRoom
{
    public sealed class HomeState
    {
        private SortedDictionary<DateTime, Home> _states;
        private SortedDictionary<DateTime, Home> States
        {
            get
            {
                return _states ?? (_states = new SortedDictionary<DateTime, Home>());
            }
        }

        private event HomeChangedEventHandler Changed;

        private void OnChanged(Home home)
        {
            HomeChangedEventHandler handler = Changed;
            if (handler != null) handler(this, home);
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

        public SortedDictionary<DateTime, Home> GetHistory()
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
            result = CreateNewHome(date);
            

            States.Add(date, result);

            return result;
        }

        private Home CreateNewHome(DateTime date)
        {
            Home result;
            Home home = GetClosestHomeState(date);
            result = home != null ? home.Clone() : new Home();
            result.Changed += (sender, h) => OnChanged(h);
            return result;
        }

        private Home GetClosestHomeState(DateTime newDate)
        {
            Home result = null;
            foreach (var pair in States)
            {
                if (pair.Key < newDate)
                {
                    result = pair.Value;
                }
            }
            return result;
        }

        #endregion helpers
    }
}
