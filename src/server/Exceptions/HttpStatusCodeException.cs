using System;

namespace Server.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }

        public HttpStatusCodeException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}