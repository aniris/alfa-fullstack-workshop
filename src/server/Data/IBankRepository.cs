using System.Collections.Generic;
using Server.Infrastructure;
using Server.Models;

namespace Server.Data
{
    /// <summary>
    /// Generic repository for current user
    /// </summary>
    public interface IBankRepository
    {
        /// <summary>
        /// Getter for cards
        /// </summary>
        IEnumerable<Card> GetCards();

        /// <summary>
        /// Get one card by number
        /// </summary>
        /// <param name="cardNumber">number of the cards</param>
        Card GetCard(string cardNumber);

        /// <summary>
        /// Open new card for current user
        /// </summary>
        /// <param name="shortCardName"></param>
        /// <param name="currency"></param>
        /// <param name="cardType"></param>
        /// <returns>new <see cref="Card"/> object</returns>
        Card OpenNewCard(string shortCardName, Currency currency, CardType cardType);

        /// <summary>
        /// Transfer money
        /// </summary>
        /// <param name="sum">sum of operation</param>
        /// <param name="from">card number</param>
        /// <param name="to">card number</param>
        /// <returns>new <see cref="Transaction"/> object</returns>
        Transaction TransferMoney(decimal sum, string from, string to);

        /// <summary>
        /// Get range of transactions
        /// </summary>
        /// <param name="cardnumber"></param>
        /// <param name="skip">how much to skip</param>
        /// <param name="take">how much to take</param>
        IEnumerable<Transaction> GetTranasctions(string cardnumber, int skip, int take);

        /// <summary>
        /// Get current logged user
        /// </summary>
        User GetCurrentUser();
    }
}