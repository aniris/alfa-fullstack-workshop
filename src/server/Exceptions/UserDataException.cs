using System;
using System.Net;

namespace Server.Exceptions
{
    /// <summary>
    /// Own exception class extends <see cref="System.Exception"/> to impelement user data model validation errors
    /// </summary>
    public class UserDataException : ServerException
    {
        public string IncorrectInputData { get; }

        public UserDataException(string message, string inputData, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base($"{message}. {inputData} is incorrect.", statusCode)
        {
            IncorrectInputData = inputData;
        }
    }
}