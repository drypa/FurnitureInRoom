using System;

namespace FurnitureInRoom.Exceptions
{
    public class CommandParameterException : Exception
    {
        public string ParameterName
        {
            get; private set;
        }

        public CommandParameterException(string paramName)
        {
            ParameterName = paramName;
        }
    }
}
