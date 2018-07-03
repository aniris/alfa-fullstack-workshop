using System;
using Server.Infrastructure;

namespace Server.Exceptions
{
    /// <summary>
    /// Own exception class extends <see cref="System.ArgumentException"/> to impelement user data model validation errors
    /// </summary>
    public class UserDataException : ArgumentException
    {
        public string IncorrectInputData { get; }
        public UserExceptionTypes Type { get; }

        public UserDataException(string message, UserExceptionTypes type, string inputData)
            :base($"{message}. {inputData} is incorrect")
        {
            IncorrectInputData = inputData;
            Type = type;
        }
    }
}