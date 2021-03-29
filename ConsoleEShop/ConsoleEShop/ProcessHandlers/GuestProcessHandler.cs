using System;
using BLL.Products;
using BLL.Users;
using DAL.Enums;

namespace PL.ProcessHandlers
{
    public class GuestProcessHandler : IProcessHandler
    {
        private readonly IUserService _userService;
        public GuestProcessHandler(IUserService userService)
        {
            _userService = userService;
        }

        public void ProcessRequest(int i)
        {
            switch (i)
            {
                case 3:
                    Register();
                    break;
                case 4:
                    Login();
                    break;
                default:
                    Console.WriteLine("Invalid number");
                    break;
            }
        }
        public void Register()
        {
            Console.WriteLine("Enter new username:");
            var username = Console.ReadLine();
            Console.WriteLine("Enter new password:");
            var password = Console.ReadLine();
            Console.WriteLine(!_userService.Register(username, password)
                ? "User with such username already exist"
                : "Registration completed successfully");
        }
        public void Login()
        {
            Console.WriteLine("Enter username:");
            var username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            var password = Console.ReadLine();
            Console.WriteLine(_userService.TryLogin(username, password)
                ? $"You are entered as {_userService.CurrentUser.Rights}"
                : "No user with this username or password");
        }

    }
}
