using System;
using Server.Infrastructure;

namespace Server.Services
{
    /// <summary>
    /// Interface for checking numbers and extracting card types
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Check card number by Lun algoritm
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card is valid</returns>
        bool CheckCardNumber(string number);

        /// <summary>
        /// Check card number by Alfabank emmiter property
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card was emmited in Alfabank </returns>
        bool CheckCardEmmiter(string number);

        /// <summary>
        /// Extract card number
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return enum CardType</returns>
        CardType CardTypeExtract(string number);

        /// <summary>
        /// Utils method
        /// </summary>
        /// <param name="cardNumber">card number in any format</param>
        /// <returns>Digits of a card number </returns>
        string CreateNormalizeCardNumber(string cardNumber);

        /// <summary>
        /// Convert DateTime and years to Exp date "01/18"
        /// </summary>
        /// <param name="dt">dateTime opencard</param>
        /// <param name="years">count of years</param>
        string GetExpDateFromDateTime(DateTime dt, int years);
    }
}