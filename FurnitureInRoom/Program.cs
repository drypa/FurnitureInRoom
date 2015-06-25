using System;

namespace FurnitureInRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            IHomeState state = new HomeState();
            CommandProcessor processor = new CommandProcessor(state,Console.Out);
            while (!processor.NeedQuit)
            {
                processor.Process(Console.ReadLine());
            }
        }
    }
}
