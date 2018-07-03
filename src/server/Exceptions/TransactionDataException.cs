using System;
using Server.Infrastructure;

namespace Server.Exceptions
{
    public class TransactionDataException : ArgumentException
    {
        public string IncorrectInputData { get; }
        public TransactionExceptionTypes Type { get; }
        
        public TransactionDataException(string message, TransactionExceptionTypes type, string inputData)
            :base($"{message}. {inputData} is incorrect")
        {
            IncorrectInputData = inputData;
            Type = type;
        }
        
        public TransactionDataException(string message, TransactionExceptionTypes type, decimal inputData)
            :base($"{message}. {inputData} is incorrect")
        {
            IncorrectInputData = inputData.ToString();
            Type = type;
        }
    }
}