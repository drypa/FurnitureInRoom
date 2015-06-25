using System;
using System.Collections.Generic;
using System.Linq;
using FurnitureInRoom.Exceptions;

namespace FurnitureInRoom
{
    public class CommandProcessor
    {
        private readonly HomeState _stateHolder;
        private readonly Dictionary<Command, Action<string>> _supportedCommands;


        public CommandProcessor(HomeState stateHolder)
        {
            _stateHolder = stateHolder;
            _supportedCommands = new Dictionary<Command, Action<string>>
            {
                 {new Command("create-room"),ProcessCreateRoom} 
                ,{new Command("remove-room"),ProcessRemoveRoom} 
                ,{new Command("create-furniture"),ProcessCreateFurniture} 
                ,{new Command("move-furniture"),ProcessMoveFurniture} 
                ,{new Command("query"),ProcessQuery} 
                ,{new Command("history"),ProcessHistory} 
            };
        }

        public void Process(string request)
        {
            foreach (var pair in _supportedCommands)
            {
                var cmd = pair.Key;
                if (cmd.IsSuitableFor(request))
                {
                    pair.Value(cmd.GetParametersString(request));
                    return;
                }
            }
            throw new CommandNotSupportedException(request);
        }

        public void ProcessCreateRoom(string command)
        {
            throw new NotImplementedException();
        }
        public void ProcessRemoveRoom(string command)
        {
            throw new NotImplementedException();
        }
        public void ProcessCreateFurniture(string command)
        {
            throw new NotImplementedException();
        }
        public void ProcessMoveFurniture(string command)
        {
            throw new NotImplementedException();
        }
        public void ProcessQuery(string command)
        {
            throw new NotImplementedException();
        }

        public void ProcessHistory(string command)
        {
            throw new NotImplementedException();
        }

        public List<string> GetSupportedCommandList()
        {
            return _supportedCommands.Keys.Select(x => x.CommandName).ToList();
        }

        private string ExtractParameter(string request, string paramName)
        {
            throw new NotSupportedException();
        }
    }
}
