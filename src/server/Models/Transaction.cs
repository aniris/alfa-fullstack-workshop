using System;
using System.Collections.Generic;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// Transaction domain model
    /// </summary>
    public class Transaction
    {
        private readonly ICardService _cardService = new CardService();

        private string _description;
        private string _cardNumber;
        private decimal _amount;
        
        /// <summary>
        /// Constructor to add new transaction
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <exception cref="TransactionDataException"></exception>
        public Transaction(string cardNumber, decimal amount, string description)
        {
            try
            {
                CardNumber = cardNumber;
                Description = description;
            }
            catch (CardDataException ex)
            {
                throw;
            }
            
            if (!_cardService.CheckCardBalance(cardNumber, amount))
            {
                throw new TransactionDataException("The card balance is not enough", TransactionExceptionTypes.Amount, amount.ToString());
            }
            
            Date = DateTime.Today;


            try
            {
                Amount = amount;
            }
            catch (TransactionDataException ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Constructor to get transaction from db
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="description"></param>
        public Transaction(string cardNumber, decimal amount, DateTime date, string description)
        {
            CardNumber = cardNumber;
            Amount = amount;
            Date = date;
            Description = description;
        }

        /// <summary>
        /// Getter and setter card number of the transaction
        /// </summary>
        public string CardNumber
        {
            get => _cardNumber;
            private set
            {
                if (!_cardService.CheckExistCard(value))
                {
                    throw new TransactionDataException("The card is not found", TransactionExceptionTypes.CardNumber, value);
                }

                if (!_cardService.CheckCardActive(value))
                {
                    throw new TransactionDataException("The card is not active", TransactionExceptionTypes.CardNumber, value);
                }

                _cardNumber = value;
            }
        }
        
        /// <summary>
        /// Getter date of the transaction
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Getter and setter amount of the transaction
        /// </summary>
        public decimal Amount
        {
            get => _amount;
            set
            {
                if (value == 0)
                {
                    throw new TransactionDataException("Amount must be not zero", TransactionExceptionTypes.Amount, value);
                }

                _amount = value;
            }
        }
        
        /// <summary>
        /// Getter and setter operation description
        /// </summary>
        public string Description { 
            get => _description;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new TransactionDataException("The description is null or empty", TransactionExceptionTypes.Description, value);
                }

                _description = value;
            }}
    }
}