using NUnit.Framework;
using PL;

namespace PLUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RequestHandlerGetCurrentHandlerReturnIProcessHandler()
        {
            //Assign
            var requestHandler = new EshopRequestHandler();
            //Act + Assert
            Assert.IsTrue(requestHandler.GetCurrentHandler() is IProcessHandler);
        }
    }
}