using System;
using System.Collections.Generic;
using System.Linq;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Server.Services;

namespace Server.Data
{
    /// <summary>
    /// Base implementation for onMemory Storage
    /// </summary>
    public class InMemoryBankRepository : IBankRepository
    {
        private readonly User currentUser;
        private static CardService _cardService;

        public InMemoryBankRepository()
        {
            currentUser = FakeDataGenerator.GenerateFakeUser();
            FakeDataGenerator.GenerateFakeCardsToUser(currentUser);
            foreach (var card in currentUser.Cards)
            {
                FakeDataGenerator.GenerateFakeTransactionstoCard(card);
            }
        }

        /// <summary>
        /// Get one card by number
        /// </summary>
        /// <param name="cardNumber">number of the cards</param>
        public Card GetCard(string cardNumber)
        {
            try
            {
                return currentUser.Cards.First(c => c.CardNumber.Equals(cardNumber));
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(TypeBusinessException.CARD, cardNumber, "card number not found");
            }
        }

        /// <summary>
        /// Getter for cards
        /// </summary>
        public IEnumerable<Card> GetCards() => GetCurrentUser().Cards;

        /// <summary>
        /// Get current logged user
        /// </summary>
        public User GetCurrentUser()
            => currentUser ?? throw new BusinessLogicException(TypeBusinessException.USER, "User is null");

        /// <summary>
        /// Get range of transactions
        /// </summary>
        /// <param name="cardnumber"></param>
        /// <param name="from">from range</param>
        public IEnumerable<Transaction> GetTranasctions(string cardnumber, int from)
        {
            return GetCard(cardnumber).Transactions.Skip(from).Take(10);
        }

        /// <summary>
        /// OpenNewCard
        /// </summary>
        /// <param name="cardType">type of the cards</param>
        public Card OpenNewCard(string cardName, Currency currency, CardType cardType)
        {
            return currentUser.OpenNewCard(cardName, currency, cardType);
        }

        /// <summary>
        /// Transfer money
        /// </summary>
        /// <param name="sum">sum of operation</param>
        /// <param name="from">card number</param>
        /// <param name="to">card number</param>
        public Transaction TransferMoney(decimal sum, string from, string to)
        {
            var fromCard = GetCard(from);
            var toCard = GetCard(to);
            var transaction = new Transaction(sum, fromCard, toCard);
            fromCard.AddTransaction(transaction);
            toCard.AddTransaction(transaction);

            return transaction;
        }
    }
}