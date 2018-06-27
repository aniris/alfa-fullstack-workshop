using System;
using System.Linq;

namespace Server.Services
{
    /// <summary>
    /// Our implementing of the <see cref="ICardService"/> interface
    /// </summary>
    public class CardService : ICardService
    {
        /// <summary>
        /// Check card number by Lun algoritm
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card is valid</returns>
        public bool CheckCardNumber(string number)
        {
            const int length = 16;
            
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            number = number.Replace(" ", "");
            
            if (number.Length != length)
            {
                return false;
            }

            if (number.Any(o => !char.IsDigit(o)))
            {
                return false;
            }
            
            int numeral,
                sum = 0;

            for (var i = 0; i < length; i++)
            {
                numeral = number[i] - '0';
                if (i % 2 == 0)
                {
                    numeral *= 2;
                    numeral = (numeral > 9) ? numeral - 9 : numeral;
                }
                sum += numeral;
            }
            
            return sum % 10 == 0;
        }

        /// <summary>
        /// Check card number by Alfabank emmiter property
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card was emmited in Alfabank </returns>
        public bool CheckCardEmmiter(string number) => throw new System.NotImplementedException();

        /// <summary>
        /// Extract card number
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return 0 is card is invalid, 1 if card is mastercard, 2 is visa, 3 is maestro, 4 is visa electon</returns>
        public int CardTypeExtract(string number) => throw new System.NotImplementedException();
    }
}