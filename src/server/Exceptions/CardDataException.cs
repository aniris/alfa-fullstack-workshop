using System;
using Server.Infrastructure;

namespace Server.Exceptions
{
    public class CardDataException : ArgumentException
    {
        public string IncorrectInputData { get; }
        public CardExceptionTypes Type { get; }
        
        public CardDataException(string message, CardExceptionTypes type, string inputData)
            :base($"{message}. {inputData} is incorrect")
        {
            IncorrectInputData = inputData;
            Type = type;
        }
    }
}