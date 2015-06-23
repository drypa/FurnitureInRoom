using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FurnitureInRoom.BusinessEntities
{
    public sealed class Room
    {
        public string Name { get; set; }

        private List<Furniture> _furniture;
        private List<Furniture> Furniture {
            get { return _furniture ?? (_furniture = new List<Furniture>()); }
        }

        public void CreateFurniture(string type)
        {
            Furniture newFurniture = new Furniture(type);
            Furniture.Add(newFurniture);
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
            foreach (Furniture furniture in Furniture)
            {
                Move(furniture, anotherRoom);
                return;
            }
        }

        private void Move(Furniture furniture, Room anotherRoom)
        {
            //TODO: add transaction
            Furniture.Remove(furniture);
            anotherRoom.Furniture.Add(furniture);
        }

    }
}
