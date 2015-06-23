using System.Collections.Generic;
using System.Linq;
using FurnitureInRoom.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FurnitureInRoom.Tests
{
    [TestClass]
    public class RoomTest
    {
        [TestMethod]
        public void CanAddNewFurnitureToRoom()
        {
            Room room = new Room();
            IList<Furniture> furnitures = room.GetFurnitures();
            Assert.AreEqual(0, furnitures.Count);

            const string furnitureType = "Sofa";
            room.CreateFurniture(furnitureType);
            Assert.AreEqual(1, furnitures.Count);
            Assert.AreEqual(furnitureType, furnitures.First().Type);


            room.CreateFurniture(furnitureType);
            Assert.AreEqual(2, furnitures.Count);
            Assert.AreEqual(furnitureType, furnitures.Last().Type);

            const string anotherFurnitureType = "Wardrobe";
            room.CreateFurniture(anotherFurnitureType);
            Assert.AreEqual(3, furnitures.Count);
            Assert.AreEqual(anotherFurnitureType, furnitures.Last().Type);
        }

        [TestMethod]
        public void CanMoveFurnitureToAnotherRoom()
        {
            Room room = new Room();
  
            const string sofaType = "Sofa";
            room.CreateFurniture(sofaType);
            room.CreateFurniture(sofaType);
            const string wardrobeType = "Wardrobe";
            room.CreateFurniture(wardrobeType);
            IList<Furniture> furnitures = room.GetFurnitures();
            Assert.AreEqual(3, furnitures.Count);

            Room anotherRoom = new Room();
            room.Move(sofaType, anotherRoom);
            IList<Furniture> anotherRoomfurnitures = anotherRoom.GetFurnitures();
            Assert.AreEqual(2, furnitures.Count);
            Assert.AreEqual(1, anotherRoomfurnitures.Count);

            room.Move(sofaType, anotherRoom);
            Assert.AreEqual(1, furnitures.Count);
            Assert.AreEqual(2, anotherRoomfurnitures.Count);

            //There are no Sofa yet
            room.Move(sofaType, anotherRoom);
            Assert.AreEqual(1, furnitures.Count);
            Assert.AreEqual(2, anotherRoomfurnitures.Count);

            room.Move(wardrobeType, anotherRoom);
            Assert.AreEqual(0, furnitures.Count);
            Assert.AreEqual(3, anotherRoomfurnitures.Count);
        }
    }
}
