using System;

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
