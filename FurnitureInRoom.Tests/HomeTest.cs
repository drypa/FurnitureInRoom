using System.Linq;
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
            int roomsAddedCount = 0;
            home.RoomAdded += (sender, added) =>
            {
                roomsAddedCount += 1;
            };
            Assert.AreEqual(0, home.GetRooms().Count);
            home.CreateRoom("living room");
            Assert.AreEqual(1, roomsAddedCount--);
            Assert.AreEqual(1, home.GetRooms().Count);

            home.CreateRoom("bedroom");
            Assert.AreEqual(1, roomsAddedCount--);
            Assert.AreEqual(2, home.GetRooms().Count);
        }

        [TestMethod]
        public void CanRemoveRoomFromHome()
        {
            Home home = new Home();
            int roomsReomovedCount = 0;
            home.RoomRemoved += (sender, added, anotherRoom) =>
            {
                roomsReomovedCount += 1;
            };
            const string bedroomName = "bedroom";
            const string livingroomName = "bedroom";
            home.CreateRoom(bedroomName);
            home.GetRooms().First().CreateFurniture("table");
            home.GetRooms().First().CreateFurniture("chair");

            home.CreateRoom(livingroomName);
            Assert.AreEqual(2, home.GetRooms().Count);
            Room livingRoom = home.GetRooms().Last();
            Assert.AreEqual(0, livingRoom.GetFurnitures().Count);
            home.RemoveRoom(bedroomName, home.GetRooms().Last());
            Assert.AreEqual(2, livingRoom.GetFurnitures().Count);
            Assert.AreEqual(1, home.GetRooms().Count);
        }
    }
}
