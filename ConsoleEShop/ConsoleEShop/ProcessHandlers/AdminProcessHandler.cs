using System;
using System.Collections.Generic;
using BLL.Orders;
using BLL.Products;
using BLL.Users;
using DAL;
using DAL.Enums;
using EShopLib;

namespace PL.ProcessHandlers
{
    public class AdminProcessHandler : IProcessHandler
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public AdminProcessHandler(IUserService userService, IProductService productService, IOrderService orderService)
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
                    ShowUsersInfo();
                    EditUsersInfo();
                    break;
                case 6:
                    AddProduct();
                    break;
                case 7:
                    EditProduct();
                    break;
                case 8:
                    EditOrderStatus();
                    break;
                case 9:
                    _userService.Logout();
                    break;
                default:
                    Console.WriteLine("Invalid input, you need to type number");
                    break;
            }
        }
        private void ShowUsersInfo()
        {
            var authorizedUsers = _userService.GetAuthorizedUsers();
            foreach (var authorizedUser in authorizedUsers)
            {
                Console.WriteLine(authorizedUser.ToString());
            }

        }
        private void EditUsersInfo()
        {
            Console.WriteLine("if you want to change user data, enter his ID, if not, enter something else");
            if (!int.TryParse(Console.ReadLine(), out var userId)) return;
            Console.WriteLine("Enter new name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter new surname");
            var surname = Console.ReadLine();
            Console.WriteLine(_userService.EditUserInfo(userId, name, surname) ? "Successfully edited" : "No user with such id");
        }
        private void AddProduct()
        {
            Console.WriteLine("Enter product name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter product category:");
            var category = Console.ReadLine();
            Console.WriteLine("Enter product description:");
            var description = Console.ReadLine();
            Console.WriteLine("Enter product price:");
            if (!int.TryParse(Console.ReadLine(), out var price)) throw new NoNumberException("You did not enter a number");
            Console.WriteLine(_productService.AddProduct(name, category, description, price)
                ? "New product added"
                : "Such product already exist");
        }
        private void EditProduct()
        {
            Console.WriteLine("Enter the id of the product you want to change:");
            if (!int.TryParse(Console.ReadLine(), out var id)) throw new NoNumberException("You did not enter a number");
            if (!_productService.ContainsProduct(id)) { Console.WriteLine("No such product id"); return; }

            Console.WriteLine("Enter new product name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter new product category:");
            var category = Console.ReadLine();
            Console.WriteLine("Enter new product description:");
            var description = Console.ReadLine();
            Console.WriteLine("Enter new product price:");
            if (!int.TryParse(Console.ReadLine(), out var price)) throw new NoNumberException("You did not enter a number");
            Console.WriteLine("Enter new product quantity:");
            if (!int.TryParse(Console.ReadLine(), out var quantity)) throw new NoNumberException("You did not enter a number");
            Console.WriteLine(
                _productService.ChangeProductInfo(id, new Product(name, category, description, price), quantity)
                    ? "Edit successfully"
                    : "An error occurred during the edit");
        }
        private void EditOrderStatus()
        {
            Console.WriteLine("Enter order id the status of which you want to change:");
            if (!int.TryParse(Console.ReadLine(), out var id)) throw new NoNumberException("You did not enter a number");
            if (!_orderService.ContainOrder(id)) { Console.WriteLine("No such order id"); return; }
            var order = _orderService.GetOrder(id);
            Console.WriteLine("Enter the status you want to set:");
            var status = Console.ReadLine()?.ToLower();
            switch (status)
            {
                case "canceledbyadmin":
                    _orderService.SetStatus(order, Status.CanceledByAdmin);
                    break;
                case "paymentreceived":
                    _orderService.SetStatus(order, Status.PaymentReceived);
                    break;
                case "sent":
                    _orderService.SetStatus(order, Status.Sent);
                    break;
                case "received":
                    _orderService.SetStatus(order, Status.Received);
                    break;
                case "completed":
                    _orderService.SetStatus(order, Status.Completed);
                    break;
                default:
                    Console.WriteLine("Only such values ​​are allowed:");
                    Console.WriteLine("CanceledByAdmin, PaymentReceived, Sent, Received, Complete");
                    break;
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
