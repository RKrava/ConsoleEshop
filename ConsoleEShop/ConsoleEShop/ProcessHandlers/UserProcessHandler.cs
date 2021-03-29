using System;
using System.Collections.Generic;
using BLL.Orders;
using BLL.Products;
using BLL.Users;
using DAL;
using DAL.Enums;
using DAL.UsersType;
using EShopLib;

namespace PL.ProcessHandlers
{
    public class UserProcessHandler : IProcessHandler
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public UserProcessHandler(IUserService userService, IProductService productService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
        }
        public void ProcessRequest(int i)
        {
            switch (i)
            {
                case 3:
                    CreateOrder();
                    break;
                case 4:
                    ProcessOrder();
                    break;
                case 5:
                    ShowOrders();
                    break;
                case 6:
                    SetReceivedStatus();
                    break;
                case 7:
                    ChangeInfo();
                    break;
                case 8:
                    _userService.Logout();
                    break;
                default:
                    Console.WriteLine("Invalid input, you need to type number");
                    break;
            }
        }
        private void ShowOrders()
        {
            var orders = _orderService.GetOrders();
            foreach (var order in orders)
            {
                if (order.Customer == _userService.CurrentUser)
                {
                    Console.WriteLine(order.ToString());
                }
            }
        }
        private void ChangeInfo()
        {
            Console.WriteLine("Enter your name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter your surname:");
            var surname = Console.ReadLine();
            if (_userService.CurrentUser is AuthorizedUser authorized)
            {
                authorized.SetNewInfo(name, surname);
            }
            else
            {
                throw new WrongUserException();
            }
        }
        private void SetReceivedStatus()
        {
            Console.WriteLine("Enter your order id:");
            if (!int.TryParse(Console.ReadLine(), out var orderId)) throw new NoNumberException("You did not enter a number");
            var order = _orderService.GetOrder(orderId, _userService.CurrentUser);
            if (order is null) Console.WriteLine("No such order or it is not your order");
            else
            {
                if (order.Status != Status.Sent && order.Status != Status.PaymentReceived && order.Status != Status.New && order.Status != Status.InProgress) { Console.WriteLine("Order is already closed or confirmed"); return; }
                _orderService.SetStatus(order, Status.Received);
                Console.WriteLine("Order status set");
            }
        }
        private void CreateOrder()
        {
            Console.WriteLine("How many products do you want to order?");
            if (!int.TryParse(Console.ReadLine(), out var number)) throw new NoNumberException("You did not enter a number");
            var productsList = new List<(Product, int)>();
            for (var i = 0; i < number; i++)
            {
                Console.WriteLine("Enter product name:");
                var productName = Console.ReadLine();
                if (_productService.ContainsProduct(productName))
                {
                    Console.WriteLine("Enter the quantity of this product in the order");
                    if (!int.TryParse(Console.ReadLine(), out var quantity)) throw new NoNumberException("You did not enter a number");
                    if (productsList.Exists(x => x.Item1.Name == productName))
                    {
                        var existProduct = productsList.Find(x => x.Item1.Name == productName);
                        productsList.Remove(existProduct);
                        existProduct.Item2 += quantity;
                        productsList.Add(existProduct);
                    }
                    else
                    {
                        productsList.Add((_productService.GetProduct(productName).Item1, quantity));
                    }
                }
                else Console.WriteLine("There are no product with this name");
            }

            Console.WriteLine(_orderService.AddOrder(productsList, _userService.CurrentUser, out var id)
                ? $"Order created successfully. Order id: {id}"
                : "An error occurred while creating");
        }
        private void ProcessOrder()
        {
            Console.WriteLine("Enter your order id:");
            if (!int.TryParse(Console.ReadLine(), out var orderId)) throw new NoNumberException("You did not enter a number");
            var order = _orderService.GetOrder(orderId, _userService.CurrentUser);
            if (order is null) Console.WriteLine("No such order or it is not your order");
            else
            {
                if (order.Status != Status.New) { Console.WriteLine("Order is already in progress, closed or confirmed"); return; }
                if (!_productService.CheckInStock(order.ProductsList))
                {
                    Console.WriteLine("Sorry, there are currently not enough products you order.");
                }
                else
                {
                    Console.WriteLine(
                        $"You need to pay {order.GetPrice()}. Do you want to confirm payment?(0 - no, 1 - yes)");
                    if (!int.TryParse(Console.ReadLine(), out var choice)) throw new NoNumberException("You did not enter a number");
                    switch (choice)
                    {
                        case 0:
                            _orderService.SetStatus(order, Status.CanceledByUser);
                            Console.WriteLine($"Order #{orderId} canceled");
                            break;
                        case 1:
                            _productService.ProductsReservation(order.ProductsList);
                            _orderService.SetStatus(order, Status.InProgress);
                            Console.WriteLine($"Order #{orderId} is awaiting confirmation of payment by admin. The products you ordered are reserved");
                            break;
                        default:
                            Console.WriteLine("Invalid number, only 0 or 1 allowed");
                            break;
                    }
                }
            }
        }
    }
}
