using System.Collections.Generic;
using BLL.Orders;
using BLL.Products;
using BLL.Users;
using DAL;
using DAL.UsersType;
using NUnit.Framework;

namespace BLLUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region OrderServiceTests

        [Test]
        public void OrderServiceAddOrderReturnsTrue()
        {
            //Assign
            var orderService = new OrderService();
            
            //Act + Assert
            Assert.IsTrue(orderService.AddOrder(new List<(Product, int)>(), new AuthorizedUser(), out _ ));
        }
        [Test]
        public void OrderServiceContainOrderReturnsTrue()
        {
            //Assign
            var orderService = new OrderService();
            orderService.AddOrder(new List<(Product, int)>(), new AuthorizedUser(), out var id);
            //Act + Assert
            Assert.IsTrue(orderService.ContainOrder(id));
        }
        [Test]
        public void OrderServiceGetOrderReturnsOrder()
        {
            //Assign
            var orderService = new OrderService();
            orderService.AddOrder(new List<(Product, int)>(), new AuthorizedUser(), out var id);
            //Act + Assert
            Assert.IsTrue(orderService.GetOrder(id) is Order);
        }
        #endregion

        #region ProductsServiceTests

        [Test]
        public void ProductsServiceAddProductReturnsTrue()
        {
            //Assign
            var productService = new ProductService();

            //Act + Assert
            Assert.IsTrue(productService.AddProduct("","","",0));
        }
        [Test]
        public void ProductsServiceChangeProductInfoReturnsFalse()
        {
            //Assign
            var productService = new ProductService();
            //Act + Assert
            Assert.IsFalse(productService.ChangeProductInfo(1,new Product(),10));
        }
        [Test]
        public void ProductsServiceContainsProductReturnsTrue()
        {
            //Assign
            var productService = new ProductService();
            productService.AddProduct("", "", "", 0);
            //Act + Assert
            Assert.IsTrue(productService.ContainsProduct(""));
        }
        #endregion

        #region UserServiceTests

        [Test]
        public void UserServiceRegisterReturnsTrue()
        {
            //Assign
            var userService = new UserService();

            //Act + Assert
            Assert.IsTrue(userService.Register("",""));
        }
        [Test]
        public void UserServiceTryLoginReturnsFalse()
        {
            //Assign
            var userService = new UserService();

            //Act + Assert
            Assert.IsFalse(userService.TryLogin("",""));
        }
        [Test]
        public void UserServiceEditUserInfoReturnsFalse()
        {
            //Assign
            var userService = new UserService();

            //Act + Assert
            Assert.IsFalse(userService.EditUserInfo(1,"",""));
        }
        #endregion
    }
}