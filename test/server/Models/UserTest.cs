using System;
using System.IO;
using System.Linq;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Server.Services;
using Xunit;

namespace ServerTest.ModelsTest
{
    public class UserTest
    {
        private IBusinessLogicService blservice = new BusinessLogicService(new CardService());

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("oleg")]
        public void CreateUserException(string value)
            => Assert.Throws<UserDataException>(() => new User(value));

        [Fact]
        public void CreateUserPassed()
        {
            var oleg = new User("oleg@oleg.ru");
            Assert.Equal("oleg@oleg.ru", oleg.UserName);
            Assert.Equal(0, oleg.Cards.Count);
        }

        // [Fact]
        // public void OpenNewCardException()
        // {
        //     var user = new User("oleg@oleg.ru");

        //     Assert.Throws<UserDataException>(() => user.OpenNewCard("firstcard", Currency.RUR, CardType.UNKNOWN));
        // }

        // [Fact]
        // public void OpenNewCardDublicatedException()
        // {
        //     var user = new User("oleg@oleg.ru");
        //     user.OpenNewCard("firstcard", Currency.RUR, CardType.VISA);

        //     Assert.Throws<UserDataException>(() => user.OpenNewCard("firstcard", Currency.RUR, CardType.VISA));
        // }

        // [Fact]
        // public void OpenNewCardPassed()
        // {
        //     var user = new User("oleg@oleg.ru");
        //     var card = user.OpenNewCard("firstcard", Currency.RUR, CardType.VISA);
        //     var actualCard = user.Cards.First();

        //     Assert.Equal(1, user.Cards.Count);
        //     Assert.Equal(CardType.VISA, card.CardType);
        //     Assert.Equal(1, card.Transactions.Count);
        //     Assert.Equal(1, actualCard.Transactions.Count);
        //     Assert.StrictEqual(card, actualCard);

        //     Assert.Equal(10M, blservice.GetBalanceOfCard(card));
        // }

        // [Fact]
        // public void TryDeleteCardException()
        // {
        //     var user = new User("oleg@oleg.ru");
        //     var card = user.OpenNewCard("firstcard", Currency.RUR, CardType.VISA);

        //     Assert.Throws<NotSupportedException>(() => user.Cards.Remove(card));
        // }
    }
}