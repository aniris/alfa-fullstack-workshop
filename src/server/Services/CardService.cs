using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Infrastructure;

namespace Server.Services
{
    /// <summary>
    /// Our implementing of the <see cref="ICardService"/> interface
    /// </summary>
    public class CardService : ICardService
    {
        #region ICardService
        /// <summary>
        /// Check card number by Lun algoritm
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card is valid</returns>
        public bool CheckCardNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;

            var cardNumber = CreateNormalizeCardNumber(number);

            if (cardNumber.Length < 12 || cardNumber.Length > 19) return false;

            var intNumbers = CreateIntCollectionFromString(cardNumber);

            var checkList = new List<int>();

            for (int i = 0; i < intNumbers.Count; i += 2)
            {
                int digit = intNumbers[i] * 2;
                digit = digit > 9 ? digit - 9 : digit;
                checkList.Add(digit);
            }

            for (int i = 1; i < intNumbers.Count; i += 2)
                checkList.Add(intNumbers[i]);

            int controllSum = 0;
            foreach (var item in checkList)
                controllSum += item;

            return controllSum % 10 == 0;
        }

        /// <summary>
        /// Extract card number
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return enum CardType</returns>
        public CardType CardTypeExtract(string number)
        {
            if (!CheckCardNumber(number)) return 0;

            var firstDigit = number[0];
            var secondDigit = number[1];

            switch (firstDigit)
            {
                case '2':
                    return CardType.MIR;
                case '4':
                    return CardType.VISA;
                case '5' when secondDigit == '0' || secondDigit > '5':
                    return CardType.MAESTRO;
                case '5' when secondDigit >= '1' && secondDigit <= '5':
                    return CardType.MASTERCARD;
                case '6':
                    return CardType.MAESTRO;
                default:
                    return CardType.UNKNOWN;
            }
        }

        /// <summary>
        /// Check card number by Alfabank emmiter property
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card was emmited in Alfabank </returns>
        public bool CheckCardEmmiter(string number)
        {
            if (!CheckCardNumber(number)) return false;

            return Constants.AlfaBINs.Any(x => number.StartsWith(x));
        }

        /// <summary>
        /// Utils method
        /// </summary>
        /// <param name="cardNumber">card number in any format</param>
        /// <returns>Digits of a card number </returns>
        public string CreateNormalizeCardNumber(string cardNumber)
        {
            var resultNumbers = new StringBuilder();

            foreach (var item in cardNumber)
                if (char.IsDigit(item)) resultNumbers.Append(item);

            return resultNumbers.ToString();
        }

        /// <summary>
        /// Convert DateTime and years to Exp date "01/18"
        /// </summary>
        /// <param name="dt">dateTime opencard</param>
        /// <param name="years">count of years</param>
        public string GetExpDateFromDateTime(DateTime dt, int years) => dt.AddYears(years).ToString("MM/yy");

        #endregion

        #region Utils
        /// <summary>
        /// Utils method. Create collection of ints from valid card string
        /// </summary>
        /// <param name="numbers">Valid card number</param>
        /// <returns>Collection of Int</returns>
        private IList<int> CreateIntCollectionFromString(string numbers)
        {
            var result = new List<int>(numbers.Length);

            foreach (var item in numbers)
            {
                int digit;
                if (int.TryParse(item.ToString(), out digit))
                    result.Add(digit);
            }

            return result;
        }

        #endregion
    }
}