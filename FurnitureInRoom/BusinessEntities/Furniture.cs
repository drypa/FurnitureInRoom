namespace FurnitureInRoom.BusinessEntities
{
    public class Furniture 
    {
        public Furniture(string type)
        {
            Type = type;
        }
        public string Type { get; private set; }

        public Furniture Clone()
        {
            return new Furniture(Type);
        }

    }
}
