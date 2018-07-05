using Server.Controllers;
using Server.Data;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Server.Models;
using Server.Services;
using Server.ViewModels;
using Server.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ServerTest.ControllersTest
{
    public class CardsControllerTest
    {
        private readonly ICardService _cardService = new CardService();
        private readonly IBusinessLogicService _businessLogicService = new BusinessLogicService(new CardService());

        [Fact]
        public void GetCardsPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCards = new FakeDataGenerator(_businessLogicService).GenerateFakeCards();

            mock.Setup(r => r.GetCards()).Returns(mockCards);

            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            // Test
            var cards = controller.Get();

            // Assert
            mock.Verify(r => r.GetCards(), Times.AtMostOnce());
            Assert.Equal(3, cards.Count());
        }

        [Fact]
        public void PostCardPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCards = new FakeDataGenerator(_businessLogicService).GenerateFakeCards();

            var cardDto = new CardDto
            {
                Name = "my card",
                Currency = 0,
                Type = 1
            };

            var mockCard = new FakeDataGenerator(_businessLogicService).GenerateFakeCard(cardDto);

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