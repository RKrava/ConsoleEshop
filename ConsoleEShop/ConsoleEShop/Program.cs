using System;
using EShopLib;
using PL.ProcessHandlers;

namespace PL
{
    class Program
    {
        private static void Print(IProcessHandler handler)
        {
            Console.WriteLine("1 - view the list of goods;");
            Console.WriteLine("2 - search for a product by name;");

            switch (handler)
            {
                case GuestProcessHandler _:
                    Console.WriteLine("3 - user account registration;");
                    Console.WriteLine("4 - login to the online store with an account.");
                    break;
                case UserProcessHandler _:
                    Console.WriteLine("3 - creating a new order;");
                    Console.WriteLine("4 - ordering or cancellation;");
                    Console.WriteLine("5 - view order history and delivery status;");
                    Console.WriteLine("6 - setting the status of the order \"Received\";");
                    Console.WriteLine("7 - change of personal information;");
                    Console.WriteLine("8 - sign out of your account.");
                    break;
                case AdminProcessHandler _:
                    Console.WriteLine("3 - creating a new order");
                    Console.WriteLine("4 - ordering;");
                    Console.WriteLine("5 - view and change users personal information;");
                    Console.WriteLine("6 - adding a new product (name, category, description, cost);");
                    Console.WriteLine("7 - change of product information;");
                    Console.WriteLine("8 - change the status of the order;");
                    Console.WriteLine("9 - sign out of your account.");
                    break;
            }
        }
        static void Main()
        {
            var eshop = new EshopRequestHandler();

            Console.WriteLine("Welcome to the our Eshop!");
            while (true)
            {
                Print(eshop.GetCurrentHandler());
                int.TryParse(Console.ReadLine(), out var i);
                try
                {
                    eshop.DelegateRequest(i);
                }
                catch (NoNumberException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
