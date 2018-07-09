using Server.Controllers;
using Server.Data;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Server.Models;
using Server.Services;
using Server.ViewModels;
using Server.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Server;
using Server.AutoMapper;
using Server.Exceptions;

namespace ServerTest.Controllers
{
    public class TransactionsControllerTest
    {
        private readonly ICardService _cardService = new CardService();
        private readonly IBusinessLogicService _businessLogicService = new BusinessLogicService(new CardService());
        private readonly UserService _userService = new UserService();

        private readonly FakeDataGenerator _fakeDataGenerator;
        public TransactionsControllerTest()
        {
            _fakeDataGenerator = new FakeDataGenerator(_businessLogicService, _userService);
        }

        [Theory]
        [InlineData("1234 1234 1233 1234")]
        [InlineData("12341233123")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("5395029009021990")]
        [InlineData("4978588211036789")]
        public void GetTransactionsException(string value)
        {
            var mock = new Mock<IBankRepository>();
            var controller = new TransactionsController(mock.Object, _cardService, _businessLogicService);

            Assert.Throws<UserDataException>(() => controller.Get(value, 0));
        }

        [Theory]
        [InlineData("4083969259636239")]
        [InlineData("5101265622568232")]
        public void GetTransactionsPassed(string value)
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCard = _fakeDataGenerator.GenerateFakeCard(value);
            var mockTran = _fakeDataGenerator.GenerateFakeTransactions(mockCard);

            mock.Setup(r => r.GetTranasctions(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(mockTran);

            var controller = new TransactionsController(mock.Object, _cardService, _businessLogicService);

            // Test
            var transactions = controller.Get(value, 0);

            // Assert
            mock.Verify(r => r.GetTranasctions(value, 0, 10), Times.AtMostOnce());
            Assert.Equal(4, transactions.Count());
        }
        
        [Fact]
        public void PostTransactionsPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockTransaction = _fakeDataGenerator.GenerateFakeTransactions(mockCards.First()).First();

            var transactionDto = new TransactionDto
            {
                From = mockTransaction.CardFromNumber,
                To = mockTransaction.CardToNumber,
                Sum = mockTransaction.Sum
            };

            mock.Setup(r => r.TransferMoney(mockTransaction.Sum, mockTransaction.CardFromNumber, mockTransaction.CardToNumber)).Returns(mockTransaction);

            var controller = new TransactionsController(mock.Object, _cardService, _businessLogicService);

            // Test
            var result = (CreatedResult)controller.Post(transactionDto);
            var resultTransaction = (TransactionDto)result.Value;

            // Assert
            mock.Verify(r => r.TransferMoney(mockTransaction.Sum, mockTransaction.CardFromNumber, mockTransaction.CardToNumber));

            Assert.Equal(201, result.StatusCode);
            Assert.Equal(transactionDto.Sum, resultTransaction.Sum);
            Assert.Equal(transactionDto.From, resultTransaction.From);
            Assert.Equal(transactionDto.To, resultTransaction.To);
            Assert.NotNull(resultTransaction.DateTime.ToString());
            Assert.True(resultTransaction.Credit);
        }

        [Fact]
        public void PostTransactionsFailed()
        {
            var mock = new Mock<IBankRepository>();

            var controller = new TransactionsController(mock.Object, _cardService, _businessLogicService);

            // Assert
            Assert.Throws<UserDataException>(() => controller.Post(null));
        }
    }
}
