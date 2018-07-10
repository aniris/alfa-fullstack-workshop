using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Moq;
using Server.Data;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Server.Repository;
using Server.Services;
using Server.ViewModels;
using Xunit;

namespace ServerTest.ModelsTest
{
    public class CardTest
    {
        private readonly ICardService _cardService = new CardService();
        private readonly IBusinessLogicService _businessLogicService = new BusinessLogicService(new CardService());
        private readonly FakeDataGenerator _fakeDataGenerator;
        private readonly UserService _userService = new UserService();

        public CardTest()
        {
            _fakeDataGenerator = new FakeDataGenerator(_businessLogicService, _userService);
        }

        [Fact]
        public void AddTransactionPassed()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockUser = _fakeDataGenerator.GenerateFakeUser();
            mockUser.Cards = (IList<Card>)_fakeDataGenerator.GenerateFakeCards();
            var userList = new List<User>();
            userList.Add(mockUser);

            var fromCardNumber = mockUser.Cards.First().CardNumber;
            var toCardNumber = mockUser.Cards.Last().CardNumber;

            mock.Setup(r => r.Users.GetAll()).Returns(userList);
            mock.Setup(r => r.Cards.Save());

            var transaction = new DbBankRepository(_cardService, _businessLogicService, _userService, mock.Object)
                .TransferMoney(0.01M, fromCardNumber, toCardNumber);

            Assert.Equal(0.01M, transaction.Sum);
            Assert.Equal(fromCardNumber, transaction.CardFromNumber);
            Assert.Equal(toCardNumber, transaction.CardToNumber);
        }

        [Fact]
        public void AddTransactionException()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockUser = _fakeDataGenerator.GenerateFakeUser();
            mockUser.Cards = (IList<Card>)_fakeDataGenerator.GenerateFakeCards();
            var userList = new List<User>();
            userList.Add(mockUser);
            
            var fromCardNumber = mockUser.Cards.First().CardNumber;
            var toCardNumber = mockUser.Cards.Last().CardNumber;
            
            mock.Setup(r => r.Users.GetAll()).Returns(userList);
            mock.Setup(r => r.Cards.Save());

            var dbBankRepository = new DbBankRepository(_cardService, _businessLogicService, _userService, mock.Object);
            
            Assert.Throws<BusinessLogicException>(() => dbBankRepository.TransferMoney(100M, fromCardNumber, toCardNumber));
            Assert.Throws<UserDataException>(() => dbBankRepository.TransferMoney(0.01M, $"{fromCardNumber}1", toCardNumber));
            Assert.Throws<UserDataException>(() => dbBankRepository.TransferMoney(0.01M, fromCardNumber, $"{toCardNumber}1"));
            Assert.Throws<BusinessLogicException>(() => dbBankRepository.TransferMoney(0.01M, fromCardNumber, fromCardNumber));
        }
        
        [Fact]
        public void OpenNewCardPassed()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockUser = _fakeDataGenerator.GenerateFakeUser();
            var userList = new List<User>();
            userList.Add(mockUser);
            
            var cardDto = new CardDto
            {
                Name = "my card",
                Currency = 0,
                Type = 1
            };
            
            mock.Setup(r => r.Users.GetAll()).Returns(userList);
            mock.Setup(r => r.Cards.Save());

            var dbBankRepository = new DbBankRepository(_cardService, _businessLogicService, _userService, mock.Object);

            var result = dbBankRepository
                .OpenNewCard(cardDto.Name, (Currency) cardDto.Currency, (CardType) cardDto.Type);

            Assert.Equal(1, result.Transactions.Count);
            Assert.Equal(cardDto.Name, result.CardName);
            Assert.Equal(cardDto.Name, result.CardName);
            Assert.Equal(cardDto.Currency, (int)result.Currency);
            Assert.Equal(cardDto.Type, (int)result.CardType);
        }
        
        [Fact]
        public void OpenNewCardFailed()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockUser = _fakeDataGenerator.GenerateFakeUser();
            mockUser.Cards = (IList<Card>)_fakeDataGenerator.GenerateFakeCards();
            var userList = new List<User>();
            userList.Add(mockUser);
            
            var cardDto = new CardDto
            {
                Name = "my card2",
                Currency = 1,
                Type = 0
            };
            
            mock.Setup(r => r.Users.GetAll()).Returns(userList);
            mock.Setup(r => r.Cards.Save());

            var dbBankRepository = new DbBankRepository(_cardService, _businessLogicService, _userService, mock.Object);

            Assert.Throws<UserDataException>(() => dbBankRepository
                .OpenNewCard(cardDto.Name, (Currency) cardDto.Currency, (CardType) cardDto.Type));
            Assert.Throws<UserDataException>(() => dbBankRepository
                .OpenNewCard(mockCards.First().CardName, (Currency) cardDto.Currency, (CardType) 1));
        }

        [Fact]
        public void GetTransactionsPassed()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockUser = _fakeDataGenerator.GenerateFakeUser();
            mockUser.Cards = (IList<Card>)_fakeDataGenerator.GenerateFakeCards();
            mockUser.Cards.First().Transactions = (IList<Transaction>) _fakeDataGenerator.GenerateFakeTransactions(mockUser.Cards.First());
            var userList = new List<User>();
            userList.Add(mockUser);
            
            mock.Setup(r => r.Users.GetAll()).Returns(userList);
            mock.Setup(r => r.Cards.Save());

            var dbBankRepository = new DbBankRepository(_cardService, _businessLogicService, _userService, mock.Object);

            var result = dbBankRepository.GetTranasctions(mockUser.Cards.First().CardNumber, 0, 10);
            
            Assert.Equal(4, result.Count());
        }
        
        [Fact]
        public void GetCardPassed()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockUser = _fakeDataGenerator.GenerateFakeUser();
            mockUser.Cards = (IList<Card>)_fakeDataGenerator.GenerateFakeCards();
            var userList = new List<User>();
            userList.Add(mockUser);
            
            mock.Setup(r => r.Users.GetAll()).Returns(userList);
            mock.Setup(r => r.Cards.Save());

            var dbBankRepository = new DbBankRepository(_cardService, _businessLogicService, _userService, mock.Object);

            var result = dbBankRepository.GetCard(mockUser.Cards.First().CardNumber);
            
            Assert.Equal(mockUser.Cards.First().CardNumber, result.CardNumber);
            Assert.Equal(mockUser.Cards.First().CardName, result.CardName);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("1234 1234")]
        [InlineData("5101269382694260")]
        public void GetCardFailed(string cardNumber)
        {
            var mock = new Mock<IUnitOfWork>();
            var mockCards = _fakeDataGenerator.GenerateFakeCards();
            var mockUser = _fakeDataGenerator.GenerateFakeUser();
            mockUser.Cards = (IList<Card>)_fakeDataGenerator.GenerateFakeCards();
            var userList = new List<User>();
            userList.Add(mockUser);
            
            mock.Setup(r => r.Users.GetAll()).Returns(userList);
            mock.Setup(r => r.Cards.Save());

            var dbBankRepository = new DbBankRepository(_cardService, _businessLogicService, _userService, mock.Object);

            Assert.Throws<UserDataException>(() => dbBankRepository.GetCard(cardNumber));
        }
    }
}