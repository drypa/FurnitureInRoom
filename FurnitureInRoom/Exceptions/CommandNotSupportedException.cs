using System;

namespace FurnitureInRoom.Exceptions
{
    public class CommandNotSupportedException : Exception
    {
        private readonly string _request;
        public string FailedRequest {
            get { return _request; }
        }

        public CommandNotSupportedException(string request)
        {
            _request = request;
        }
    }
}
