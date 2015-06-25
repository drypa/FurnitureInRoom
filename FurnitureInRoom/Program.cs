using System;
using FurnitureInRoom.Exceptions;

namespace FurnitureInRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            IHomeState state = new HomeState();
            CommandProcessor processor = new CommandProcessor(state,Console.Out);
            processor.Help();
            while (!processor.NeedQuit)
            {
                try
                {
                    Console.WriteLine("Insert Your command:");
                    processor.Process(Console.ReadLine());
                }
                catch (CommandNotSupportedException commandNotSupportedException)
                {
                    Console.Error.WriteLine("Command '{0}' is not supported", commandNotSupportedException.FailedRequest);
                }
                catch (CommandParameterException commandParameterException)
                {
                    Console.Error.WriteLine("Command parameter '{0}' error", commandParameterException.ParameterName);
                }

            }
        }
    }
}
