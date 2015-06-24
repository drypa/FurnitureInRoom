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
            DateTime date = new DateTime(2015,01,05);
            stateHolder.CreateRoom("bedroom", date);
            var history = stateHolder.GetHistory();

            Assert.AreEqual(1, history.Count);

            Assert.AreEqual(date, history.First().Key);
            Assert.IsNotNull(history.First().Value);
            Assert.AreEqual(1,history.First().Value.GetRooms().Count);
            Assert.AreEqual("bedroom", history.First().Value.GetRooms().First().Name);

        }
    }
}
