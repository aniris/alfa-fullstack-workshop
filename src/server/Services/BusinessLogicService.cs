using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Server.ViewModels;

namespace Server.Services
{
    /// <summary>
    /// Business Logic Service
    /// </summary>
    public class BusinessLogicService : IBusinessLogicService
    {
        private readonly ICardService _cardService;

        public BusinessLogicService(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Convert currency from to
        /// </summary>
        /// <param name="sum">Sum for convert</param>
        /// <param name="from">Convert from this currency</param>
        /// <param name="to">Convert to this currency</param>
        /// <returns>Converted sum on <see langword="decimal"/></returns>
        public decimal GetConvertSum(decimal sum, Currency from, Currency to)
        {
            if (from == to) return sum;

            return sum * Constants.Currencies[from] / Constants.Currencies[to];
        }

        /// <summary>
        /// Add bonus o new card
        /// </summary>
        /// <param name="card">Card to</param>
        public void AddBonusOnOpen(Card card)
            => card.Transactions.Add(new Transaction
            {
                CardToNumber = card.CardNumber,
                Sum = GetConvertSum(10M, Currency.RUR, card.Currency)
            });

        /// <summary>
        /// Check Card expired or not
        /// </summary>
        /// <param name="card">Card for cheking</param>
        /// <returns><see langword="true"/> if card is active</returns>
        public bool CheckCardActivity(Card card) => !(card.DTOpenCard.AddYears(card.ValidityYear) <= DateTime.Today);

        /// <summary>
        /// Get balance of the card
        /// </summary>
        /// <param name="card">Card to calculaing</param>
        /// <returns><see langword="decimal"/> sum</returns>
        public decimal GetBalanceOfCard(Card card)
        {
            var credit = card.Transactions.Where(x => x.CardToNumber == card.CardNumber).Sum(x => x.Sum);
            var debit = card.Transactions.Where(x => x.CardFromNumber == card.CardNumber).Sum(x => x.Sum);
            return credit - debit;
        }

        /// <summary>
        /// Get balance of the card round to 2
        /// </summary>
        /// <param name="card">Card to calculaing</param>
        /// <returns><see langword="decimal"/> sum</returns>
        public decimal GetRoundBalanceOfCard(Card card)
        {
            var balance = GetBalanceOfCard(card);
            return Math.Round(balance, 2, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Generate new card number from BIN list
        /// </summary>
        /// <param name="cardType">Type of the card/param>
        /// <returns>Generated new card number</returns>
        public string GenerateNewCardNumber(CardType cardType)
        {
            string startBin;

            switch (cardType)
            {
                case CardType.MIR:
                    startBin = "2";
                    break;
                case CardType.VISA:
                    startBin = "4";
                    break;
                case CardType.MAESTRO:
                    startBin = "6";
                    break;
                case CardType.MASTERCARD:
                    startBin = "51";
                    break;
                default:
                    throw new BusinessLogicException(TypeBusinessException.CARD, "Cannot create new card number");
            }

            startBin = Constants.AlfaBINs.First(x => x.StartsWith(startBin));
            if (string.IsNullOrWhiteSpace(startBin))
                throw new BusinessLogicException(TypeBusinessException.CARD, "Cannot create new card number");

            // generate new CardNumber for user
            return GenerateNewCardNumber(startBin);
        }

        private string GenerateNewCardNumber(string prefix, int length = 16)
        {
            string ccnumber = prefix;

            while (ccnumber.Length < (length - 1))
            {
                double rnd = (new Random().NextDouble() * 1.0f - 0f);
                ccnumber += Math.Floor(rnd * 10);
            }

            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();
            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            // calculate sum
            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < length - 1)
            {
                int odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != (length - 2))
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            // calculate check digit
            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            return ccnumber;
        }

        /// <summary>
        /// Method to validate transfer
        /// </summary>
        /// <param name="transaction">transaction DTO</param>
        public void ValidateTransferDto(TransactionDto transaction)
        {
            if (!_cardService.CheckCardEmmiter(transaction.From))
                throw new UserDataException("Card number is invalid", transaction.From);

            if (!_cardService.CheckCardEmmiter(transaction.To))
                throw new UserDataException("Card number is invalid", transaction.To);

            if (transaction.Sum <= 0)
                throw new UserDataException("Sum must be greated then 0", transaction.Sum.ToString());
        }

        /// <summary>
        /// Mothod to validate opencard dto
        /// </summary>
        /// <param name="card">card DTO</param>
        public void ValidateOpenCardDto(CardDto card)
        {
            if (card.Type <= 0 || card.Type > 4)
                throw new UserDataException("Card type is invalid", card.Type.ToString());
            if (card.Currency < 0 || card.Currency > 2)
                throw new UserDataException("Currency is invalid", card.Currency.ToString());
        }

        /// <summary>
        /// Validate cards and their balance
        /// </summary>
        /// <param name="from">card from</param>
        /// <param name="to">card to</param>
        /// <param name="sum">sum</param>
        public void ValidateTransfer(Card from, Card to, decimal sum)
        {
            if (from.CardNumber == to.CardNumber)
                throw new BusinessLogicException(TypeBusinessException.TRANSACTION,
               "From card and to card is Equal", "Нельзя перевести на туже карту");

            if (!CheckCardActivity(from) && !CheckCardActivity(to))
                throw new BusinessLogicException(TypeBusinessException.CARD, "Card is expired", "Карта просрочена");

            if (GetBalanceOfCard(from) < sum)
                throw new BusinessLogicException(TypeBusinessException.CARD, "Balance of the card is low", "Нет денег на карте");
        }

        /// <summary>
        /// Validate exist card by number or by name
        /// </summary>
        /// <param name="cards">cards</param>
        /// <param name="shortCardName">name</param>
        /// <param name="cardNumber">number</param>
        public void ValidateCardExist(IEnumerable<Card> cards, string shortCardName, string cardNumber)
        {
            if (cards.Any(c => c.CardName == shortCardName || c.CardNumber == cardNumber))
                throw new UserDataException("Card is already exist", shortCardName);
        }
    }
}