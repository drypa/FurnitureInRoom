using FurnitureInRoom.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FurnitureInRoom.Tests
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void CanAddNewRoomToHome()
        {
            Home home = new Home();
            Assert.AreEqual(0, home.GetRooms().Count);
            home.CreateRoom();
            Assert.AreEqual(1,home.GetRooms().Count);

            home.CreateRoom();
            Assert.AreEqual(2, home.GetRooms().Count);
        }
    }
}
