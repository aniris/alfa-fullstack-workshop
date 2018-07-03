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
        /// <returns>Return 0 is card is invalid, 1 if card is mastercard, 2 is visa, 3 is maestro, 4 is visa electon</returns>
        int CardTypeExtract(string number);
        
        /// <summary>
        /// Check card name
        /// </summary>
        /// <param name="cardName"></param>
        /// <returns>Return <see langword="true"/> if card name is valid</returns>
        bool CheckCardName(string cardName);
        
        /// <summary>
        /// Check if the card exists
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns>Return <see langword="true"/> if card exists</returns>
        bool CheckExistCard(string cardNumber);
        
        /// <summary>
        /// Check if the card is active
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns>Return <see langword="true"/> if card is active</returns>
        bool CheckCardActive(string cardNumber);
        
        /// <summary>
        /// Check if the card balance is enough
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="operationAmount"></param>
        /// <returns>Return <see langword="true"/> if card balance is enough</returns>
        bool CheckCardBalance(string cardNumber, decimal operationAmount);

        /// <summary>
        /// Get a random currency type
        /// </summary>
        /// <returns></returns>
        CurrencyTypes getRandomCurrencyTypes();
    }
}