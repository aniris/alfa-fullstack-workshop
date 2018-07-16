using System;
using System.IO;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Server.Services;
using Xunit;

namespace ServerTest.ServicesTest
{
    public class BusinessLogicTest
    {
        private readonly IBusinessLogicService _businessLogicService =
            new BusinessLogicService(new CardService());

        [Theory]
        [InlineData(Currency.RUR, Currency.USD, 1000, "15.954052329291640076579451181")]
        [InlineData(Currency.EUR, Currency.RUR, 100, "7264")]
        [InlineData(Currency.RUR, Currency.RUR, 100, "100")]
        public void GetConvertSumPassed(Currency from, Currency to, decimal valueIn, string valueOut)
        {
            var decimalValueOut = Convert.ToDecimal(valueOut);
            Assert.Equal(decimalValueOut, _businessLogicService.GetConvertSum(valueIn, from, to));
        }

        [Fact]
        public void AddBonusOnOpen_GetBalanceOfCard_Passed()
        {
            var card = new Card
            {
                CardName = "4790878827491205",
                CardNumber = "4790878827491205",
                CardType = CardType.VISA,
                Currency = Currency.RUR
            };

            _businessLogicService.AddBonusOnOpen(card);
            Assert.Equal(10M, _businessLogicService.GetBalanceOfCard(card));
        }

        [Fact]
        public void CheckCardActivityPasses()
        {
            var card = new Card
            {
                CardName = "4790878827491205",
                CardNumber = "4790878827491205",
                CardType = CardType.VISA,
                Currency = Currency.RUR,
                DTOpenCard = DateTime.Today.AddYears(-6)
            };
            Assert.False(_businessLogicService.CheckCardActivity(card));
        }
    }
}