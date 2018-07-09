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

namespace ServerTest.ControllersTest
{
    public class CardsControllerTest
    {
        private readonly ICardService _cardService = new CardService();
        private readonly IBusinessLogicService _businessLogicService = new BusinessLogicService(new CardService());
        private readonly UserService _userService = new UserService();

        private readonly FakeDataGenerator _fakeDataGenerator;
        public CardsControllerTest()
        {
            _fakeDataGenerator = new FakeDataGenerator(_businessLogicService, _userService);
        }
        
        [Fact]
        public void GetCardsPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();

            mock.Setup(r => r.GetCards()).Returns(mockCards);

            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            // Test
            var cards = controller.Get();

            // Assert
            mock.Verify(r => r.GetCards(), Times.AtMostOnce());
            Assert.Equal(3, cards.Count());
        }
        
        [Fact]
        public void GetCardPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockFirstCard = mockCards.First();

            mock.Setup(r => r.GetCard(mockFirstCard.CardNumber)).Returns(mockFirstCard);

            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            // Test
            var result = controller.Get(mockFirstCard.CardNumber);
            
            // Assert
            mock.Verify(r => r.GetCard(mockFirstCard.CardNumber), Times.AtMostOnce());
            Assert.Equal(mockFirstCard.CardName, result.Name);
            Assert.Equal(mockFirstCard.CardNumber, result.Number);
            Assert.Equal((int)mockFirstCard.CardType, result.Type);
            Assert.Equal((int)mockFirstCard.Currency, result.Currency);
        }
        
        [Theory]
        [InlineData("1234")]
        [InlineData("5101269382694260")]
        [InlineData(null)]
        public void GetCardFailed(string number)
        {
            // Arrange
            var mock = new Mock<IBankRepository>();

            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            // Assert
            Assert.Throws<HttpStatusCodeException>(() => controller.Get(number));
        }

        [Fact]
        public void PostCardPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();

            var cardDto = new CardDto
            {
                Name = "my card",
                Currency = 0,
                Type = 1
            };

            var mockCard = _fakeDataGenerator.GenerateFakeCard(cardDto);

            mock.Setup(r => r.GetCards()).Returns(mockCards);
            mock.Setup(r => r.OpenNewCard(It.IsAny<string>(), It.IsAny<Currency>(), It.IsAny<CardType>())).Returns(mockCard);

            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            // Test
            var result = (CreatedResult)controller.Post(cardDto);
            var resultCard = (CardDto)result.Value;

            // Assert
            mock.Verify(r => r.GetCards(), Times.AtMostOnce());
            mock.Verify(r => r.OpenNewCard(It.IsAny<string>(), It.IsAny<Currency>(), It.IsAny<CardType>()), Times.AtMostOnce());

            Assert.Equal(201, result.StatusCode);
            Assert.Equal(0, resultCard.Balance);
            Assert.Equal(cardDto.Name, resultCard.Name);
            Assert.NotNull(resultCard.Number);
            Assert.Equal(cardDto.Currency, resultCard.Currency);
            Assert.Equal(cardDto.Type, resultCard.Type);
        }
        
        [Theory]
        [InlineData(null, 1, 1)]
        [InlineData("my card", 1, 0)]
        [InlineData("my card", 1, 5)]
        [InlineData("my card", -1, 1)]
        [InlineData("my card", 3, 1)]
        public void PostCardFailed(string name, int currency, int type)
        {
            // Arrange
            var mock = new Mock<IBankRepository>();

            var cardDto = new CardDto
            {
                Name = name,
                Currency = currency,
                Type = type
            };

            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            // Assert
            Assert.Throws<UserDataException>(() => controller.Post(cardDto));
        }

        [Fact]
        public void PutCard405()
        {
            var mock = new Mock<IBankRepository>();
            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            var result = (StatusCodeResult)controller.Put();

            Assert.Equal(405, result.StatusCode);
        }

        [Fact]
        public void DeleteCard405()
        {
            var mock = new Mock<IBankRepository>();
            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            var result = (StatusCodeResult)controller.Delete();

            Assert.Equal(405, result.StatusCode);
        }
    }
}