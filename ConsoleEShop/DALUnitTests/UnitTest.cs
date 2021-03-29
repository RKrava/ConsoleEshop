using System.Buffers;
using System.Collections.Generic;
using DAL;
using DAL.DataBases;
using DAL.Interfaces;
using DAL.UsersType;
using NUnit.Framework;

namespace DALUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region OrdersDbTests

        [Test]
        public void OrdersDbReturnId()
        {
            //Assign
            var ordersDb = new OrdersDb();
            var order = new Order(new List<(Product, int)>(), new Guest());
            //Act + Assert
            Assert.IsTrue(ordersDb.AddOrder(order) == order.Id);
        }
        [Test]
        public void OrdersDbContainsOrderReturnsTrue()
        {
            //Assign
            var ordersDb = new OrdersDb();
            var order = new Order(new List<(Product, int)>(), new Guest());
            ordersDb.AddOrder(order);
            //Act + Assert
            Assert.IsTrue(ordersDb.ContainOrder(order.Id));
        }

        [Test]
        public void OrdersDbGetOrderReturnsOrder()
        {
            //Assign
            var ordersDb = new OrdersDb();
            var order = new Order(new List<(Product, int)>(), new Guest());
            ordersDb.AddOrder(order);
            //Act + Assert
            Assert.AreEqual(order,ordersDb.GetOrder(order.Id));
        }

        [Test]
        public void OrdersDbGetOrdersReturnsListOfOrder()
        {
            //Assign
            var ordersDb = new OrdersDb();
            //Act + Assert
            Assert.IsTrue(ordersDb.GetOrders() is List<Order>);
        }
        #endregion

        #region ProductsDbTests

        [Test]
        public void ProductsDbAddProductReturnsTrue()
        {
            //Assign
            var productsDb = new ProductsDB();
            var product = new Product();
            //Act + Assert
            Assert.IsTrue(productsDb.AddProduct(product));
        }

        [Test]
        public void ProductsDbAddProductReturnsFalse()
        {
            //Assign
            var productsDb = new ProductsDB();
            var product = new Product();
            productsDb.AddProduct(product);
            //Act + Assert
            Assert.IsFalse(productsDb.AddProduct(product));
        }

        [Test]
        public void ProductsDbGetProductsReturnsListProductAndInt()
        {
            //Assign
            var productsDb = new ProductsDB();
            //Act + Assert
            Assert.IsTrue(productsDb.GetProducts() is List<(Product, int)>);
        }

        [Test]
        public void ProductsDbGetProductReturnsProductAndInt()
        {
            //Assign
            var productsDb = new ProductsDB();
            var product = new Product();
            productsDb.AddProduct(product);
            //Act + Assert
            Assert.AreEqual(productsDb.GetProduct(product.Name).Item1,product);
        }

        [Test]
        public void ProductsDbChangeProductInfoReturnsTrue()
        {
            //Assign
            var productsDb = new ProductsDB();
            var product = new Product();
            productsDb.AddProduct(product);
            //Act + Assert
            Assert.IsTrue(productsDb.ChangeProductInfo(product.Id,new Product(),0 ));
        }

        [Test]
        public void ProductsDbContainsProductReturnsTrue()
        {
            //Assign
            var productsDb = new ProductsDB();
            var product = new Product();
            productsDb.AddProduct(product);
            //Act + Assert
            Assert.IsTrue(productsDb.ContainsProduct(product.Id));
        }
        #endregion

        #region UsersDbTests

        [Test]
        public void UsersDbAddUserReturnsTrue()
        {
            //Assign
            var usersDb = new UsersDb();
            //Act + Assert
            Assert.IsTrue(usersDb.AddUser("",""));
        }

        [Test]
        public void UsersDbGetUserReturnsIUser()
        {
            //Assign
            var usersDb = new UsersDb();
            usersDb.AddUser("", "");
            //Act + Assert
            Assert.IsTrue(usersDb.GetUser("", "") is IUser);
        }

        [Test]
        public void UsersDbGetAuthorizedUsersReturnsListAuthorizedUsers()
        {
            //Assign
            var usersDb = new UsersDb();
            //Act + Assert
            Assert.IsTrue(usersDb.GetAuthorizedUsers() is List<AuthorizedUser>);
        }
        #endregion
    }
}