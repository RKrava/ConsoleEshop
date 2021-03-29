using System;
using BLL.Orders;
using BLL.Products;
using BLL.Users;
using DAL.UsersType;
using PL.ProcessHandlers;

namespace PL
{
    public class EshopRequestHandler
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private IProcessHandler _processHandler;
        public EshopRequestHandler()
        {
            _orderService = new OrderService();
            _userService = new UserService();
            _productService = new ProductService();
            _userService.CurrentUser = new Guest();
            _processHandler = new GuestProcessHandler(_userService);
        }
        private void ShowProducts()
        {
            foreach (var (product, quantity) in _productService.GetProducts())
            {
                Console.WriteLine($"Product: {product}; Quantity: {quantity}.");
            }
        }
        private void ShowProductByName()
        {
            Console.WriteLine("Enter product name:");
            var name = Console.ReadLine();
            var (product, quantity) = _productService.GetProduct(name);
            Console.WriteLine(product == null
                ? "No such product"
                : $"Product: {product}; Quantity:{quantity}");
        }
        public void DelegateRequest(int i)
        {
            switch (i)
            {
                case 1:
                    ShowProducts(); return;
                case 2:
                    ShowProductByName(); return;
                default:
                    _processHandler.ProcessRequest(i);
                    break;
            }
            switch (_userService.CurrentUser)
            {
                case Guest _ when !(_processHandler is GuestProcessHandler):
                    _processHandler = new GuestProcessHandler(_userService);
                    break;
                case AuthorizedUser _ when !(_processHandler is UserProcessHandler):
                    _processHandler = new UserProcessHandler(_userService,_productService,_orderService);
                    break;
                case Admin _ when !(_processHandler is AdminProcessHandler):
                    _processHandler = new AdminProcessHandler(_userService, _productService, _orderService);
                    break;
            }
        }
        public IProcessHandler GetCurrentHandler()
        {
            return _processHandler;
        }
    }
}
