using System.Linq;
using FurnitureInRoom.BusinessEntities;
using FurnitureInRoom.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FurnitureInRoom.Tests
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void CanAddNewRoomToHome()
        {
            int roomsAddedCount = 0;
            Home home = new Home();
            home.RoomAdded += (sender, h) =>
            {
                roomsAddedCount += 1;
            };

            Assert.AreEqual(0, home.GetRooms().Count);
            var room = home.CreateRoom("living room");
            Assert.IsNotNull(room);
            Assert.AreEqual(1, roomsAddedCount--);
            Assert.AreEqual(1, home.GetRooms().Count);

            room =home.CreateRoom("bedroom");
            Assert.IsNotNull(room);
            Assert.AreEqual(1, roomsAddedCount--);
            Assert.AreEqual(2, home.GetRooms().Count);
        }

        [TestMethod]
        public void CanRemoveRoomFromHome()
        {
            int roomsReomovedCount = 0;
            Home home = new Home();
            home.RoomRemoved += (sender, added, anotherRoom) =>
            {
                roomsReomovedCount += 1;
            };

            const string bedroomName = "bedroom";
            const string livingroomName = "living room";
            var room =home.CreateRoom(bedroomName);
            Assert.IsNotNull(room);
            home.GetRooms().First().CreateFurniture("table");
            home.GetRooms().First().CreateFurniture("chair");

            room = home.CreateRoom(livingroomName);
            Assert.IsNotNull(room);
            Assert.AreEqual(2, home.GetRooms().Count);
            Room livingRoom = home.GetRooms().Last();
            Assert.AreEqual(0, livingRoom.GetFurnitures().Count);
            home.RemoveRoom(bedroomName, livingroomName);
            Assert.AreEqual(2, livingRoom.GetFurnitures().Count);
            Assert.AreEqual(1, home.GetRooms().Count);
        }
        [TestMethod]
        [ExpectedException(typeof(NoRoomFoundException))]
        public void RemoveRoomShouldThrowNoRoomFoundExceptionIfWrongRoomName()
        {
            Home home = new Home();
            home.RemoveRoom("room1","room2");
        }

        [TestMethod]
        [ExpectedException(typeof(NoRoomFoundException))]
        public void RemoveRoomShouldThrowNoRoomFoundExceptionIfWrongAnotherRoomNameIfThereAreSomeFurnitureInFirstRoom()
        {
            Home home = new Home();
            var room = home.CreateRoom("room1");
            Assert.IsNotNull(room);
            home.GetRooms().First().CreateFurniture("sofa");
            home.RemoveRoom("room1", "room2");
        }
    }
}
