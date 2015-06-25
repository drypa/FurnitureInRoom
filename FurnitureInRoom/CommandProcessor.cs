using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FurnitureInRoom.BusinessEntities;
using FurnitureInRoom.Exceptions;

namespace FurnitureInRoom
{
    public class CommandProcessor
    {
        public bool NeedQuit { get; set; }
        private readonly IHomeState _stateHolder;
        private readonly TextWriter _writer;
        private readonly Dictionary<Command, Action<string>> _supportedCommands;


        public CommandProcessor(IHomeState stateHolder, TextWriter writer)
        {
            NeedQuit = false;
            _stateHolder = stateHolder;
            _writer = writer;
            _supportedCommands = new Dictionary<Command, Action<string>>
            {
                 {new Command("create-room","create-room -room room1 [-date 01.01.2015]"),ProcessCreateRoom} 
                ,{new Command("remove-room","remove-room -room room1 -transfer room2 [-date 01.01.2015]"),ProcessRemoveRoom} 
                ,{new Command("create-furniture","create-furniture -type smtg -room room1 [-date 01.01.2015]"),ProcessCreateFurniture} 
                ,{new Command("move-furniture","move-furniture -type sofa -room room1 -to room2 [-date 01.01.2015]"),ProcessMoveFurniture} 
                ,{new Command("query","query [-date 01.01.2015]"),ProcessQuery} 
                ,{new Command("history","history [-short]"),ProcessHistory} 
                ,{new Command("quit"),ProcessQuit} 
                ,{new Command("help"),ProcessHelp} 
            };
        }

        public void Help()
        {
            _writer.WriteLine("Suppotted commands:");
            foreach (var command in _supportedCommands.Keys)
            {
                _writer.WriteLine(command.Help());
            }
        }

        private void ProcessHelp(string obj)
        {
            Help();
        }

        private void ProcessQuit(string obj)
        {
            NeedQuit = true;
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

        private void ProcessCreateRoom(string parameters)
        {
            var date = ExtractDateParameter(parameters, "-date") ?? DateTime.UtcNow;
            var room = ExtractStringParameter(parameters, "-room");
            _stateHolder.CreateRoom(room, date);
        }
        private void ProcessRemoveRoom(string parameters)
        {
            var date = ExtractDateParameter(parameters, "-date") ?? DateTime.UtcNow;
            var room = ExtractStringParameter(parameters, "-room");
            var anotherRoom = ExtractStringParameter(parameters, "-transfer");
            _stateHolder.RemoveRoom(room, anotherRoom, date);
        }
        private void ProcessCreateFurniture(string parameters)
        {
            var date = ExtractDateParameter(parameters, "-date") ?? DateTime.UtcNow;
            var room = ExtractStringParameter(parameters, "-room");
            var furnitureType = ExtractStringParameter(parameters, "-type");
            _stateHolder.CreateFurniture(furnitureType, room, date);
        }
        private void ProcessMoveFurniture(string parameters)
        {
            var date = ExtractDateParameter(parameters, "-date") ?? DateTime.UtcNow;
            var room = ExtractStringParameter(parameters, "-room");
            var furnitureType = ExtractStringParameter(parameters, "-type");
            var anotherRoom = ExtractStringParameter(parameters, "-to");
            _stateHolder.MoveFurniture(furnitureType, room, anotherRoom, date);
        }
        private void ProcessQuery(string parameters)
        {
            var date = ExtractDateParameter(parameters, "-date") ?? DateTime.UtcNow;
            _writer.WriteLine(_stateHolder.GetHomeByDate(date).Listing());
        }

        private void ProcessHistory(string parameters)
        {
            var shortHistory = ExtractBoolParameter(parameters, "-short");
            string result = null;
            if (shortHistory)
            {
                result = string.Join("\r\n", _stateHolder.GetHomeChangeDates());
            }
            else
            {
                result = string.Join("\r\n", _stateHolder.GetHistory().Select(GetString));
            }
            _writer.WriteLine(result);
        }

        private string GetString(KeyValuePair<DateTime, Home> pair)
        {
            return string.Format("{0}:\r\n{1}", pair.Key, pair.Value.Listing());
        }

        public List<string> GetSupportedCommandList()
        {
            return _supportedCommands.Keys.Select(x => x.CommandName).ToList();
        }

        private bool ExtractBoolParameter(string request, string paramName)
        {
            var arr = request.Split(' ');
            return arr.Any(str => str.Equals(paramName, StringComparison.InvariantCultureIgnoreCase));
        }


        private string ExtractStringParameter(string request, string paramName)
        {
            var arr = request.Split(' ');
            for (int i = 0; i < arr.Length; ++i)
            {
                string str = arr[i];
                if (str.Equals(paramName, StringComparison.InvariantCultureIgnoreCase))
                {
                    var paramPosition = i + 1;
                    if (paramPosition < arr.Length)
                    {
                        return arr[paramPosition].Trim();
                    }
                    throw new CommandParameterException(paramName);
                }
            }
            return null;
        }

        private DateTime? ExtractDateParameter(string request, string paramName)
        {
            var valueAsString = ExtractStringParameter(request, paramName);
            if (string.IsNullOrEmpty(valueAsString))
            {
                return null;
            }
            DateTime date;
            if (!DateTime.TryParse(valueAsString, out date))
            {
                throw new CommandParameterException(paramName);
            }
            return date;
        }
    }
}
