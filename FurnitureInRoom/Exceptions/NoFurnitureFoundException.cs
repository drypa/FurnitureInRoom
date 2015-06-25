using System;

namespace FurnitureInRoom.Exceptions
{
    public class NoFurnitureFoundException : Exception
    {
        public string FurnitureType { get; private set; }

        public NoFurnitureFoundException(string furnitureType)
        {
            FurnitureType = furnitureType;
        }
    }
}
