using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Server.Controllers;
using Server.Data;
using Server.Infrastructure;
using Server.Services;
using Xunit;

namespace ServerTest.ControllersTest
{
    public class TransactionControllerTest
    {
        private readonly ICardService _cardService = new CardService();
        
        [Fact]
        public void GetTransactionsPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockUser = FakeDataGenerator.GenerateFakeUser();
            var mockCards = FakeDataGenerator.GenerateFakeCardsToUser(mockUser);
            var mockTransactions = FakeDataGenerator.GenerateFakeTransactionstoCard(mockCards.First());

            mock.Setup(r => r.GetTranasctions(mockCards.First().CardNumber, 1)).Returns(mockTransactions.Skip(1).Take(10));

            var controller = new TransactionsController(mock.Object, _cardService);

            // Test
            var transactions = controller.Get(1, mockCards.First().CardNumber);

            // Assert
            mock.Verify(r => r.GetTranasctions(mockCards.First().CardNumber, 1), Times.AtMostOnce());
            Assert.Equal(10, transactions.Count());
        }
        
        [Fact]
        public void GetTransactionsFailed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockUser = FakeDataGenerator.GenerateFakeUser();
            var mockCards = FakeDataGenerator.GenerateFakeCardsToUser(mockUser);
            var mockTransactions = FakeDataGenerator.GenerateFakeTransactionstoCard(mockCards.First());

            mock.Setup(r => r.GetTranasctions(mockCards.First().CardNumber, 1)).Returns(mockTransactions.Skip(1).Take(10));

            var controller = new TransactionsController(mock.Object, _cardService);

            // Test
            var transactions = controller.Get(1, mockCards.First().CardNumber);

            // Assert
            mock.Verify(r => r.GetTranasctions(mockCards.First().CardNumber, 1), Times.AtMostOnce());
            Assert.NotEqual(0, transactions.Count());
        }
        
        [Fact]
        public void DeleteTransactionFailed()
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