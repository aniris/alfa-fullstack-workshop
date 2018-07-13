using System.Collections.Generic;
using Server.Infrastructure;
using Server.Models;
using Server.ViewModels;

namespace Server.Services
{
    /// <summary>
    /// Interface for Business Logic Service
    /// </summary>
    public interface IBusinessLogicService
    {
        /// <summary>
        /// Convert currency from to
        /// </summary>
        /// <param name="sum">Sum for convert</param>
        /// <param name="from">Convert from this currency</param>
        /// <param name="to">Convert to this currency</param>
        /// <returns>Converted sum on <see langword="decimal"/></returns>
        decimal GetConvertSum(decimal sum, Currency from, Currency to);

        /// <summary>
        /// Generate new card number from BIN list
        /// </summary>
        /// <param name="cardType">Type of the card/param>
        /// <returns>Generated new card number</returns>
        string GenerateNewCardNumber(CardType cardType);

        /// <summary>
        /// Add bonus o new card
        /// </summary>
        /// <param name="card">Card to</param>
        Transaction AddBonusOnOpen(Card card);

        /// <summary>
        /// Check Card expired or not
        /// </summary>
        /// <param name="card">Card for cheking</param>
        /// <returns><see langword="true"/> if card is active</returns>
        bool CheckCardActivity(Card card);

        /// <summary>
        /// Get balance of the card
        /// </summary>
        /// <param name="card">Card to calculaing</param>
        /// <returns><see langword="decimal"/> sum</returns>
        decimal GetBalanceOfCard(Card card);

        /// <summary>
        /// Get balance of the card round to 2
        /// </summary>
        /// <param name="card">Card to calculaing</param>
        /// <returns><see langword="decimal"/> sum</returns>
        decimal GetRoundBalanceOfCard(Card card);

        /// <summary>
        /// Method to validate transfer
        /// </summary>
        /// <param name="transaction">transaction DTO</param>
        void ValidateTransferDto(TransactionDto transaction);

        /// <summary>
        /// Mothod to validate opencard dto
        /// </summary>
        /// <param name="card">card DTO</param>
        void ValidateOpenCardDto(CardDto card);

        /// <summary>
        /// Validate cards and their balance
        /// </summary>
        /// <param name="from">card from</param>
        /// <param name="to">card to</param>
        /// <param name="sum">sum</param>
        void ValidateTransfer(Card from, Card to, decimal sum);

        /// <summary>
        /// Validate exist card by number or by name
        /// </summary>
        /// <param name="cards">cards</param>
        /// <param name="shortCardName">name</param>
        /// <param name="cardNumber">number</param>
        void ValidateCardExist(IEnumerable<Card> cards, string shortCardName, string cardNumber);
    }
}