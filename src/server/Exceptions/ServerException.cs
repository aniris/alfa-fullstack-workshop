using System;
using System.Net;

namespace Server.Exceptions
{
    /// <summary>
    /// Own <see langword="abstract"/> exception class extends <see cref="System.Exception"/> to impelement user data model validation errors
    /// </summary>
    public abstract class ServerException : Exception
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public ServerException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
                => StatusCode = statusCode;
    }
}