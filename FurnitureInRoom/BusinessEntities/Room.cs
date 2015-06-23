using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureInRoom.Events;

namespace FurnitureInRoom.BusinessEntities
{
    public sealed class Room
    {
        public Room(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        private List<Furniture> _furniture;
        private List<Furniture> Furniture {
            get { return _furniture ?? (_furniture = new List<Furniture>()); }
        }

        public event FurnitureAddedEventHandler FurnitureAdded;
        private void OnFurnitureAdded(Furniture furnitureAdded)
        {
            FurnitureAddedEventHandler handler = FurnitureAdded;
            if (handler != null) handler(this, furnitureAdded);
        }

        public event FurnitureRemovedEventHandler FurnitureRemoved;

        private void OnFurnitureRemoved(Furniture furnitureRemoed, Room newRoom)
        {
            FurnitureRemovedEventHandler handler = FurnitureRemoved;
            if (handler != null) handler(this, furnitureRemoed, newRoom);
        }

        public void CreateFurniture(string type)
        {
            Furniture newFurniture = new Furniture(type);
            Furniture.Add(newFurniture);
            OnFurnitureAdded(newFurniture);
        }

        public ReadOnlyCollection<Furniture> GetFurnitures()
        {
            return new ReadOnlyCollection<Furniture>(Furniture);
        }

        public void Move(string furnitureType,Room anotherRoom)
        {
            foreach (Furniture furniture in Furniture)
            {
                if (furniture.Type == furnitureType)
                {
                    Move(furniture, anotherRoom);
                    return;
                }
            }
            
        }

        public void MoveAll(Room anotherRoom)
        {
            while (Furniture.Count>0)
            {
                Furniture furniture = Furniture.First();
                Move(furniture, anotherRoom);
            }
        }

        private void Move(Furniture furniture, Room anotherRoom)
        {
            //TODO: add transaction
            Furniture.Remove(furniture);
            anotherRoom.Furniture.Add(furniture);
            OnFurnitureRemoved(furniture, anotherRoom);
        }

    }
}
