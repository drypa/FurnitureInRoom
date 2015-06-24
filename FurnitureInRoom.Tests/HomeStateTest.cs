using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FurnitureInRoom.Tests
{
    [TestClass]
    public class HomeStateTest
    {
        [TestMethod]
        public void CreateRoomShouldCreateNewStateIfDateNotExists()
        {
            HomeState stateHolder = new HomeState();
            const string badroomName = "bedroom";
            DateTime date = new DateTime(2015,01,05);
            stateHolder.CreateRoom(badroomName, date);
            var history = stateHolder.GetHistory();

            Assert.AreEqual(1, history.Count);

            Assert.AreEqual(date, history.First().Key);
            Assert.IsNotNull(history.First().Value);
            Assert.AreEqual(1,history.First().Value.GetRooms().Count);
            Assert.AreEqual(badroomName, history.First().Value.GetRooms().First().Name);

            DateTime newDate = new DateTime(2015, 01, 06);
            stateHolder.CreateRoom(badroomName, newDate);
            Assert.AreEqual(2, history.Count);

            newDate = new DateTime(2014, 01, 01);
            stateHolder.CreateRoom(badroomName, newDate);
            Assert.AreEqual(3, history.Count);
        }

        [TestMethod]
        public void CreateRoomShouldCreateRoomInExistingState()
        {
            HomeState stateHolder = new HomeState();
            const string roomName = "bedroom";
            DateTime date = new DateTime(2015, 01, 05);
            stateHolder.CreateRoom(roomName, date);
            var history = stateHolder.GetHistory();

            Assert.AreEqual(1, history.Count);
            stateHolder.CreateRoom(roomName, date);
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(2, history.First().Value.GetRooms().Count);
        }


        [TestMethod]
        public void CreateRoomShouldContainAllRoomsFromPreviousState()
        {
            HomeState stateHolder = new HomeState();
            const string roomName = "bedroom";
            DateTime date = new DateTime(2015, 01, 05);
            stateHolder.CreateRoom(roomName, date);
            var history = stateHolder.GetHistory();

            Assert.AreEqual(1, history.Count);
            DateTime newDate = new DateTime(2015, 01, 06);
            stateHolder.CreateRoom(roomName, newDate);

            Assert.AreEqual(2, history.Count);
            Assert.AreEqual(2, history.Last().Value.GetRooms().Count);
        }
    }
}
