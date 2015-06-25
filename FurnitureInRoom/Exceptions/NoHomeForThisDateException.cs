using System;

namespace FurnitureInRoom.Exceptions
{
    public class NoHomeForThisDateException : Exception
    {
        public DateTime Date { get; private set; }

        public NoHomeForThisDateException(DateTime date)
        {
            Date = date;
        }
    }
}
