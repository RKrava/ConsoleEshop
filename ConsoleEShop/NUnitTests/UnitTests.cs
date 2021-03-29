using EShopLib;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EshopCheckingRegistrationReturnsTrue()
        {
            //Assign
            var eshop = new Eshop();
            //Act
            var result = eshop.Register("login","pass");
            //Assert
            Assert.AreEqual(true,result);
        }
        [Test]
        public void EshopCheckingRegistrationReturnsFalse()
        {
            //Assign
            var eshop = new Eshop();
            //Act
            var result = eshop.Register("user", "pass");
            //Assert
            Assert.AreEqual(false, result);
        }
        [Test]
        public void EshopCheckingLoginReturnsTrue()
        {
            //Assign
            var eshop = new Eshop();
            //Act
            var result = eshop.TryLogin("user", "upass");
            //Assert
            Assert.AreEqual(true, result);
        }
        [Test]
        public void EshopCheckingLoginReturnsFalse()
        {
            //Assign
            var eshop = new Eshop();
            //Act
            var result = eshop.TryLogin("pass", "user");
            //Assert
            Assert.AreEqual(false, result);
        }
    }
}