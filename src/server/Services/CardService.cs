using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Server.Services
{
    /// <summary>
    /// Our implementing of the <see cref="ICardService"/> interface
    /// </summary>
    public class CardService : ICardService
    {
        private enum TypeCard
        {
            Mastercard = 1,
            Visa,
            Maestro,
            VisaElectron
        }
        
        /// <summary>
        /// Check card number by Lun algoritm
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card is valid</returns>
        public bool CheckCardNumber(string number)
        {
            const int minLength = 12,
                maxLenght = 19;
            
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            number = number.Replace(" ", "");

            int length = number.Length,
                numeral,
                sum = 0;
            
            if (length <= minLength || length >= maxLenght)
            {
                return false;
            }

            if (number.Any(o => !char.IsDigit(o)))
            {
                return false;
            }
            

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
        /// <returns>Return 0 is card is invalid, 1 if card is mastercard, 2 is visa, 3 is maestro, 4 is visa electron</returns>
        public int CardTypeExtract(string number)
        {
            Match m;
            Dictionary<TypeCard, string> typesCard = new Dictionary<TypeCard, string>()
            {
                { TypeCard.Mastercard, @"^(5[1-5]|222[1-9]|2[3-6]|27[0-1]|2720)\d*" },
                { TypeCard.VisaElectron, @"^(4026|417500|4508|4844|491(3|7))\d*" },
                { TypeCard.Visa, @"^4\d*" },
                { TypeCard.Maestro, @"^(5[0678]|6304|6390|6054|6271|67)\d*" }
            };

            foreach (KeyValuePair<TypeCard, string> type in typesCard)
            {
                m = Regex.Match(number, type.Value);
                if (m.Success) {
                    return (int)type.Key;
                }
            }

            return 0;
        }
    }
}