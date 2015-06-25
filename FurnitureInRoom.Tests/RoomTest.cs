using System.Collections.Generic;
using System.Linq;
using FurnitureInRoom.BusinessEntities;
using FurnitureInRoom.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FurnitureInRoom.Tests
{
    [TestClass]
    public class RoomTest
    {
        [TestMethod]
        public void CanAddNewFurnitureToRoom()
        {
            int addedEventsInvokedCount = 0;
            Room room = new Room("bedroom", (sender, added) =>
            {
                addedEventsInvokedCount += 1;
            }, null);

            IList<Furniture> furnitures = room.GetFurnitures();
            Assert.AreEqual(0, furnitures.Count);

            const string furnitureType = "Sofa";
            room.CreateFurniture(furnitureType);
            Assert.AreEqual(1, addedEventsInvokedCount--, "There are no event invoked when furniture was added");
            Assert.AreEqual(1, furnitures.Count);
            Assert.AreEqual(furnitureType, furnitures.First().Type);


            room.CreateFurniture(furnitureType);
            Assert.AreEqual(1, addedEventsInvokedCount--, "There are no event invoked when furniture was added");
            Assert.AreEqual(2, furnitures.Count);
            Assert.AreEqual(furnitureType, furnitures.Last().Type);

            const string anotherFurnitureType = "Wardrobe";
            room.CreateFurniture(anotherFurnitureType);
            Assert.AreEqual(1, addedEventsInvokedCount--, "There are no event invoked when furniture was added");
            Assert.AreEqual(3, furnitures.Count);
            Assert.AreEqual(anotherFurnitureType, furnitures.Last().Type);
        }

        [TestMethod]
        public void CanMoveFurnitureToAnotherRoom()
        {
            int removeEventsInvokedCount = 0;
            Room room = new Room("bedroom", null, (sender, remoed, newRoom) =>
            {
                removeEventsInvokedCount += 1;
            });

            const string sofaType = "Sofa";
            room.CreateFurniture(sofaType);
            room.CreateFurniture(sofaType);
            const string wardrobeType = "Wardrobe";
            room.CreateFurniture(wardrobeType);
            IList<Furniture> furnitures = room.GetFurnitures();
            Assert.AreEqual(3, furnitures.Count);

            Room anotherRoom = new Room("living room", null, null);
            room.Move(sofaType, anotherRoom);
            Assert.AreEqual(1, removeEventsInvokedCount--, "There are no event invoked when furniture was moved");
            IList<Furniture> anotherRoomfurnitures = anotherRoom.GetFurnitures();
            Assert.AreEqual(2, furnitures.Count);
            Assert.AreEqual(1, anotherRoomfurnitures.Count);

            room.Move(sofaType, anotherRoom);
            Assert.AreEqual(1, removeEventsInvokedCount--, "There are no event invoked when furniture was moved");
            Assert.AreEqual(1, furnitures.Count);
            Assert.AreEqual(2, anotherRoomfurnitures.Count);

            room.Move(wardrobeType, anotherRoom);
            Assert.AreEqual(1, removeEventsInvokedCount--, "There are no event invoked when furniture was moved");
            Assert.AreEqual(0, furnitures.Count);
            Assert.AreEqual(3, anotherRoomfurnitures.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NoFurnitureFoundException))]
        public void CantMoveNotExistentFurniture()
        {
            Room room = new Room("bedroom", null, null);
            Room anotherRoom = new Room("room", null, null);
            room.Move("nonexistent furniture", anotherRoom);
        }


        [TestMethod]
        public void NothingHappendWhenMovingFurnitureToSameRoom()
        {
            int addEventsInvokedCount = 0;
            int removeEventsInvokedCount = 0;
            Room room = new Room("bedroom", (sender, added) =>
            {
                addEventsInvokedCount += 1;
            }, (sender, remoed, newRoom) =>
            {
                removeEventsInvokedCount += 1;
            });
            const string sofaType = "Sofa";
            room.CreateFurniture(sofaType);
            room.Move(sofaType, room);
            Assert.AreEqual(1, addEventsInvokedCount);
            Assert.AreEqual(0, removeEventsInvokedCount);
        }


        [TestMethod]
        public void CanListingRoom()
        {
            Room room = new Room("bedroom", null,null);
            room.CreateFurniture("sofa");
            room.CreateFurniture("chair");
            var listing = room.Listing();
            Assert.IsNotNull(listing);
            Assert.IsTrue(listing.Contains("bedroom"));
            Assert.IsTrue(listing.Contains("sofa"));
            Assert.IsTrue(listing.Contains("chair"));
        }
    }
}
