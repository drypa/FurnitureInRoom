using System;
using System.Collections.Generic;
using System.Linq;
using FurnitureInRoom.BusinessEntities;
using FurnitureInRoom.Events;
using FurnitureInRoom.Exceptions;

namespace FurnitureInRoom
{
    public interface IHomeState
    {
        void CreateRoom(string roomName, DateTime date);
        void RemoveRoom(string roomName, string otherRoom, DateTime date);
        IList<Room> GetRoomsList(DateTime date);
        void CreateFurniture(string furnitureType, string roomName, DateTime date);
        void MoveFurniture(string furnitureType, string fromRoomName, string toRoomName, DateTime date);
        SortedDictionary<DateTime, Home> GetHistory();
        List<DateTime> GetHomeChangeDates();
        Home GetHomeByDate(DateTime date);
    }

    public sealed class HomeState : IHomeState
    {
        private readonly ISaver<HomeState> _saver;

        public HomeState()
        {
            Changed += Save;
        }
        public HomeState(ISaver<HomeState> saver)
            : this()
        {
            _saver = saver;
        }

        private void Save(object sender, Home home)
        {
            if (_saver != null)
                _saver.Save(this);
        }
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
            var home = GetHomeByDate(date);
            home.RemoveRoom(roomName, otherRoom);
        }

        public IList<Room> GetRoomsList(DateTime date)
        {
            var home = GetClosestHomeState(date);
            return home.GetRooms();
        }

        public void CreateFurniture(string furnitureType, string roomName, DateTime date)
        {
            Home home = GetHome(date);

            var room = home.FindRoom(roomName) ?? home.CreateRoom(roomName);
            room.CreateFurniture(furnitureType);
        }

        public void MoveFurniture(string furnitureType, string fromRoomName, string toRoomName, DateTime date)
        {
            Home home = GetHomeByDate(date);
            Room room = home.FindRoom(fromRoomName);
            if (room == null)
            {
                throw new NoRoomFoundException(fromRoomName);
            }
            Room toRoom = home.FindRoom(toRoomName);
            if (toRoom == null)
            {
                throw new NoRoomFoundException(toRoomName);
            }
            room.Move(furnitureType, toRoom);
        }



        public SortedDictionary<DateTime, Home> GetHistory()
        {
            //TODO: need clone to prevent collection change
            return States;
        }
        public List<DateTime> GetHomeChangeDates()
        {
            return States.Keys.ToList();
        }

        #region helpers

        private Home GetStateByDate(DateTime date)
        {
            Home result = GetHome(date);
            return result;
        }

        private Home GetHome(DateTime date)
        {
            Home result;
            if (States.TryGetValue(date, out result))
            {
                return result;
            }
            result = CreateNewHome(date);
            return result;
        }

        private Home CreateNewHome(DateTime date)
        {
            Home result;
            Home home = GetClosestHomeState(date);
            if (home != null)
            {
                result = home.Clone();
            }
            else
            {
                result = new Home();
            }
            States.Add(date, result);
            result.Changed += (sender, h) => OnChanged(h);
            return result;
        }

        private Home GetClosestHomeState(DateTime newDate)
        {
            Home result = null;
            foreach (var pair in States)
            {
                if (pair.Key <= newDate)
                {
                    result = pair.Value;
                }
            }
            return result;
        }

        public Home GetHomeByDate(DateTime date)
        {
            Home home = GetClosestHomeState(date);
            if (home == null)
            {
                throw new NoHomeForThisDateException();
            }
            return home;
        }

        #endregion helpers
    }
}
