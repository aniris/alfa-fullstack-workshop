using System;
using System.Collections.Generic;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// Card domain model
    /// </summary>
    public class Card
    {
        private string _cardNumber;
        private string _cardName;
        private readonly decimal amountForNewCard = 10m;
        private readonly int validityCardYear = 3;

        private readonly ICardService _cardService = new CardService();

        /// <summary>
        /// Constructor to add new card
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cardName"></param>
        /// <param name="userName"></param>
        public Card(string cardNumber, string cardName, string userName)
        {
            try
            {
                CardNumber = cardNumber;
                CardName = cardName;
            }
            catch (CardDataException ex)
            {
                throw;
            }
            Type = (CardType)_cardService.CardTypeExtract(cardNumber);
            Currency = _cardService.getRandomCurrencyTypes();
            Balance = 0;
            Validity = DateTime.Today.AddYears(validityCardYear);
            UserName = userName;
        }
        
        
        /// <summary>
        /// Constructor to get card from db
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cardName"></param>
        /// <param name="type"></param>
        /// <param name="currency"></param>
        /// <param name="validity"></param>
        /// <param name="balance"></param>
        public Card(string cardNumber, string cardName, CardType type, CurrencyTypes currency, DateTime validity, decimal balance)
        {
            CardNumber = cardNumber;
            CardName = cardName;
            Type = type;
            Currency = currency;
            Validity = validity;
            Balance = balance;
        }

        /// <summary>
        /// Card number
        /// </summary>
        /// <returns>string card number representation</returns>
        public string CardNumber
        {
            get => _cardNumber;
            private set
            {
                if (!_cardService.CheckCardNumber(value))
                {
                    throw new CardDataException("card number is incorrect", CardExceptionTypes.Number, value);
                }

                if (!_cardService.CheckCardEmmiter(value))
                {
                    throw new CardDataException("card emitter isn't Alfa Bank", CardExceptionTypes.Number, value);
                }

                if (_cardService.CardTypeExtract(value) == 0)
                {
                    throw new CardDataException("card type is undefined", CardExceptionTypes.Number, value);
                }

                _cardNumber = value;
            }
        }

        /// <summary>
        /// Short name of the cards
        /// </summary>
        /// <returns>string card name</returns>
        public string CardName
        {
            get => _cardName;
            set
            {
                if (!_cardService.CheckCardName(value))
                {
                    throw new CardDataException("card name is incorrect", CardExceptionTypes.Name, value);
                }

                _cardName = value;
            }
        }
        
        public string UserName { get; }
        
        /// <summary>
        /// Getter user card list
        /// </summary>
        public List<Transaction> Transactions { get; } = new List<Transaction>();

        /// <summary>
        /// Type of the cards
        /// </summary>
        public CardType Type { get; }
        
        /// <summary>
        /// Currency of the cards
        /// </summary>
        public CurrencyTypes Currency { get; }
        
        /// <summary>
        /// Validity of the cards
        /// </summary>
        public DateTime Validity { get; }
        
        /// <summary>
        /// Balance of the cards
        /// </summary>
        public Decimal Balance { get; private set; }

        /// <summary>
        /// Add bonus money to a new card
        /// </summary>
        public void AddMoneyForNewCard()
        {
            Transaction newTransaction;

            decimal amount = amountForNewCard;
            
            if (Currency is CurrencyTypes.Dollar)
            {
                amount = CurrencyService.CurrencyConversion(Currency, amount);
            }
            
            try
            {
                newTransaction = new Transaction(CardNumber, amount,
                    "Начисление бонусных рублей при открытии новой карты");
            }
            catch (TransactionDataException ex)
            {
                throw;
            }
            Transactions.Add(newTransaction);
            Balance = amountForNewCard;
        }
        
    }
}