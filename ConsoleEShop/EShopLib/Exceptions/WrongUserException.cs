using System;
using System.Collections.Generic;
using System.Text;

namespace EShopLib
{
    public class WrongUserException : Exception
    {
        public WrongUserException(string message) : base(message)
        {
        }

        public WrongUserException()
        {
        }
    }
}
