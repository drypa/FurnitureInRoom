using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
