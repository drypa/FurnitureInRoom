using System;
using FurnitureInRoom.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FurnitureInRoom.Tests
{
    [TestClass]
    public class CommandProcessorTest
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotSupportedException))]
        public void ProcessShouldThrowCommandNotSupportedExceptionInCaseOfWrongCommand()
        {
            throw new NotImplementedException();
        }
    }
}
