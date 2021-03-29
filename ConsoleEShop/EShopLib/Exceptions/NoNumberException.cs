using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace EShopLib
{
    public class NoNumberException : Exception
    {
        public NoNumberException()
        {
        }

        public NoNumberException(string message) : base(message)
        {
        }
    }
}
