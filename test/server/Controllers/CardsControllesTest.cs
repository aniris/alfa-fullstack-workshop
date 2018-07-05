using System;
using Server.Controllers;
using Server.Data;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Server.Infrastructure;
using Server.Models;
using Server.Services;

namespace ServerTest.ControllersTest
{
    public class CardsControllerTest
    {
        private readonly ICardService _cardService = new CardService();

        [Fact]
        public void GetCardsPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockUser = FakeDataGenerator.GenerateFakeUser();
            var mockCards = FakeDataGenerator.GenerateFakeCardsToUser(mockUser);

            mock.Setup(r => r.GetCards()).Returns(mockCards);

            var controller = new CardsController(mock.Object, _cardService);

            // Test
            var cards = controller.Get();

            // Assert
            mock.Verify(r => r.GetCards(), Times.AtMostOnce());
            Assert.Equal(3, cards.Count());
        }
        
        [Fact]
        public void GetCardByNumberPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockUser = FakeDataGenerator.GenerateFakeUser();
            var mockCards = FakeDataGenerator.GenerateFakeCardsToUser(mockUser);

            var numberFirstCard = mockCards.First().CardNumber;
            
            mock.Setup(r => r.GetCard(numberFirstCard)).Returns(mockCards.First());

            var controller = new CardsController(mock.Object, _cardService);

            // Test
            var card = controller.Get(numberFirstCard);

            // Assert
            mock.Verify(r => r.GetCard(numberFirstCard), Times.AtMostOnce());
            Assert.Equal(numberFirstCard, card.CardNumber);
        }
        
        [Fact]
        public void GetCardByNumberFailed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockUser = FakeDataGenerator.GenerateFakeUser();
            var mockCards = FakeDataGenerator.GenerateFakeCardsToUser(mockUser);
            var mockTransactions = FakeDataGenerator.GenerateFakeTransactionstoCard(mockCards.First());
            var numberFirstCard = mockCards.First().CardNumber;
            
            mock.Setup(r => r.GetCard(numberFirstCard)).Returns(mockCards.First());

            var controller = new CardsController(mock.Object, _cardService);

            // Test
            var card = controller.Get(numberFirstCard);

            // Assert
            mock.Verify(r => r.GetCard(numberFirstCard), Times.AtMostOnce());
            Assert.NotEqual("1234567812345678", card.CardNumber);
        }
        
        [Fact]
        public void DeleteCardFailed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockUser = FakeDataGenerator.GenerateFakeUser();
            var mockCards = FakeDataGenerator.GenerateFakeCardsToUser(mockUser);

            var controller = new TransactionsController(mock.Object, _cardService);
            bool exceptionCatch = false;

            // Test
            try
            {
                var transactions = controller.Delete(mockCards.First().CardNumber);
            }
            catch (Exception)
            {
                exceptionCatch = true;
            }
            
            Assert.True(exceptionCatch);
        }
    }
}