using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Exceptions
{
    public class UnauthorizedHttpRequestException : Exception
    {
        public UnauthorizedHttpRequestException(string message)
            : base(message)
        {

        }
    }
}
