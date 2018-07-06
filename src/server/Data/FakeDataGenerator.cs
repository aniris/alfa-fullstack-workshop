using System;
using System.Collections.Generic;
using Server.Infrastructure;
using Server.Models;
using Server.Services;
using Server.ViewModels;

namespace Server.Data
{
    public class FakeDataGenerator
    {
        private readonly IBusinessLogicService _businessLogicService;

        public FakeDataGenerator(IBusinessLogicService businessLogicService)
        {
            _businessLogicService = businessLogicService;
        }

        public static User GenerateFakeUser() => new User("admin@admin.net");

        public Card GenerateFakeCard(CardDto card) => new Card
        {
            CardNumber = _businessLogicService.GenerateNewCardNumber(CardType.MAESTRO),
            CardName = card.Name,
            Currency = (Currency)card.Currency,
            CardType = (CardType)card.Type
        };

        public Card GenerateFakeCard(string number) => new Card
        {
            CardNumber = number,
            CardName = "mu card",
            Currency = Currency.RUR,
            CardType = CardType.MAESTRO
        };

        public IEnumerable<Transaction> GenerateFakeTransactions(Card card)
        {
            var transactions = new List<Transaction>
            {
                new Transaction{
                    CardFromNumber = card.CardNumber,
                    CardToNumber = "4083969259636239",
                    Sum = 10
                },
                new Transaction{
                    CardFromNumber = card.CardNumber,
                    CardToNumber = "4083969259636239",
                    Sum = 10
                },
                new Transaction{
                    CardFromNumber = card.CardNumber,
                    CardToNumber = "4083969259636239",
                    Sum = 10
                },
                new Transaction{
                    CardFromNumber = card.CardNumber,
                    CardToNumber = "4083969259636239",
                    Sum = 10
                }
            };

            return transactions;
        }

        public IEnumerable<Card> GenerateFakeCards()
        {    // create fake cards
            var cards = new List<Card>
            {
                new Card
                {
                    CardNumber = _businessLogicService.GenerateNewCardNumber(CardType.MAESTRO),
                    CardName = "my salary",
                    Currency = Currency.RUR,
                    CardType = CardType.MAESTRO
                },
                new Card
                {
                    CardNumber = _businessLogicService.GenerateNewCardNumber(CardType.VISA),
                    CardName = "my debt",
                    Currency = Currency.EUR,
                    CardType = CardType.VISA
                },
                new Card
                {
                    CardNumber = _businessLogicService.GenerateNewCardNumber(CardType.MASTERCARD),
                    CardName = "my my lovely wife",
                    Currency = Currency.USD,
                    CardType = CardType.MASTERCARD
                }
            };
            cards.ForEach(card => _businessLogicService.AddBonusOnOpen(card));

            return cards;
        }
    }
}