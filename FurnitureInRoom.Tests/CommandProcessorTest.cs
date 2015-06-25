using System;
using System.IO;
using FurnitureInRoom.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FurnitureInRoom.Tests
{
    [TestClass]
    public class CommandProcessorTest
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotSupportedException))]
        public void ProcessShouldThrowCommandNotSupportedExceptionInCaseOfWrongCommand()
        {
            CommandProcessor processor = new CommandProcessor(new HomeState(),Console.Out); 
            processor.Process("wrong-command");
        }

        [TestMethod]
        public void SupportsCreateRoomsCommand()
        {
            Mock<IHomeState> stateMock = new Mock<IHomeState>();
            CommandProcessor processor = new CommandProcessor(stateMock.Object,Console.Out);
            string command = "create-room -date 12.01.2015 -room Bedroom";
            processor.Process(command);
            stateMock.Verify(a => a.CreateRoom("Bedroom", new DateTime(2015, 01, 12)));

            command = "create-room -room Bedroom -date 12.01.2015";
            processor.Process(command);
            stateMock.Verify(a => a.CreateRoom("Bedroom", new DateTime(2015, 01, 12)));
            
            command = "create-room -room Bedroom";
            processor.Process(command);
            stateMock.Verify(a => a.CreateRoom("Bedroom", It.IsAny<DateTime>()));
        }
        [TestMethod]
        public void SupportsRemoveRoomCommand()
        {
            Mock<IHomeState> stateMock = new Mock<IHomeState>();
            CommandProcessor processor = new CommandProcessor(stateMock.Object, Console.Out);
            string command = "remove-room -date 12.01.2015 -room Bedroom -transfer dining";
            processor.Process(command);
            stateMock.Verify(a => a.RemoveRoom("Bedroom", "dining", new DateTime(2015, 01, 12)));

            command = "remove-room -room Bedroom -date 12.01.2015";
            processor.Process(command);
            stateMock.Verify(a => a.RemoveRoom("Bedroom","dining",new DateTime(2015, 01, 12)));

            command = "remove-room -room Bedroom -transfer dining";
            processor.Process(command);
            stateMock.Verify(a => a.RemoveRoom("Bedroom", "dining", It.IsAny<DateTime>()));
        }

        [TestMethod]
        public void SupprotsCreateFurnitureCommand()
        {
            Mock<IHomeState> stateMock = new Mock<IHomeState>();
            CommandProcessor processor = new CommandProcessor(stateMock.Object, Console.Out);
            string command = "create-furniture -date 12.01.2015 -room Bedroom -type sofa";
            processor.Process(command);
            stateMock.Verify(a => a.CreateFurniture("sofa", "Bedroom", new DateTime(2015, 01, 12)));

            command = "create-furniture -room Bedroom -date 12.01.2015 -type sofa";
            processor.Process(command);
            stateMock.Verify(a => a.CreateFurniture("sofa", "Bedroom", new DateTime(2015, 01, 12)));

            command = "create-furniture -room Bedroom -type sofa";
            processor.Process(command);
            stateMock.Verify(a => a.CreateFurniture("sofa", "Bedroom", It.IsAny<DateTime>()));
        }

        [TestMethod]
        public void SupprotsMoveFurniture()
        {
            Mock<IHomeState> stateMock = new Mock<IHomeState>();
            CommandProcessor processor = new CommandProcessor(stateMock.Object, Console.Out);
            string command = "move-furniture -date 12.01.2015 -room Bedroom -type sofa -to dinner";
            processor.Process(command);
            stateMock.Verify(a => a.MoveFurniture("sofa", "Bedroom", "dinner", new DateTime(2015, 01, 12)));

            command = "move-furniture -room Bedroom -date 12.01.2015 -type sofa";
            processor.Process(command);
            stateMock.Verify(a => a.MoveFurniture("sofa", "Bedroom", "dinner", new DateTime(2015, 01, 12)));

            command = "move-furniture -room Bedroom -to dinner -type sofa";
            processor.Process(command);
            stateMock.Verify(a => a.MoveFurniture("sofa", "Bedroom", "dinner", It.IsAny<DateTime>()));
        }

        [TestMethod]
        public void SupportsQueryCommand()
        {
            HomeState state = new HomeState();
            state.CreateFurniture("sofa","room",new DateTime(2015,12,01));
            Mock<TextWriter> writerMock = new Mock<TextWriter>();
            CommandProcessor processor = new CommandProcessor(state, writerMock.Object);
            string command = "query -date 01.12.2015";
            processor.Process(command);
            writerMock.Verify(w=>w.WriteLine(It.IsAny<string>()));
        }
    }
}
