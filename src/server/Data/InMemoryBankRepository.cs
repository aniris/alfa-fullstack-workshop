// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using Server.Exceptions;
// using Server.Infrastructure;
// using Server.Models;
// using Server.Services;

// namespace Server.Data
// {
//     /// <summary>
//     /// Base implementation for onMemory Storage
//     /// </summary>
//     public class InMemoryBankRepository : IBankRepository
//     {
//         private readonly User currentUser;

//         private readonly ICardService _cardService;

//         private readonly IBusinessLogicService _businessLogicService;

//         public InMemoryBankRepository(ICardService cardService, IBusinessLogicService businessLogicService)
//         {
//             _cardService = cardService;
//             _businessLogicService = businessLogicService;

//             currentUser = FakeDataGenerator.GenerateFakeUser();
//             ProducedFakeData();
//         }

//         private void ProducedFakeData()
//         {
//             // fakes generate
//             var cards = (List<Card>)GetCards();
//             cards.AddRange(new FakeDataGenerator(_businessLogicService).GenerateFakeCards());
//         }

//         /// <summary>
//         /// Get one card by number. Must be with populating balance
//         /// </summary>
//         /// <param name="cardNumber">number of the cards</param>
//         public Card GetCard(string cardNumber)
//         {
//             var card = GetCards().FirstOrDefault(c => c.CardNumber == _cardService.CreateNormalizeCardNumber(cardNumber));

//             if (card == null)
//                 throw new UserDataException("Card not found", cardNumber, HttpStatusCode.NotFound);

//             return card;
//         }

//         /// <summary>
//         /// Getter for cards. Must be with populating balance
//         /// </summary>
//         public IEnumerable<Card> GetCards() => GetCurrentUser().Cards;

//         /// <summary>
//         /// Get current logged user
//         /// </summary>
//         public User GetCurrentUser()
//             => currentUser != null ? currentUser :
//                 throw new BusinessLogicException(TypeBusinessException.USER, "User is null");

//         /// <summary>
//         /// Get range of transactions
//         /// </summary>
//         /// <param name="cardnumber"></param>
//         /// <param name="skip">how much to skip</param>
//         /// <param name="take">how much to take</param>
//         public IEnumerable<Transaction> GetTranasctions(string cardnumber, int skip, int take)
//         {
//             var card = GetCard(cardnumber);

//             var transactions = card.Transactions.Skip(skip).Take(take);

//             return transactions != null ? transactions : new List<Transaction>();
//         }

//         /// <summary>
//         /// Open new card for current user
//         /// </summary>
//         /// <param name="shortCardName"></param>
//         /// <param name="currency"></param>
//         /// <param name="cardType"></param>
//         /// <returns>new Card object</returns>
//         public Card OpenNewCard(string shortCardName, Currency currency, CardType cardType)
//         {
//             if (cardType == CardType.UNKNOWN)
//                 throw new UserDataException("Wrong type card", cardType.ToString());

//             IList<Card> allCards = (List<Card>)GetCards();

//             var cardNumber = _businessLogicService.GenerateNewCardNumber(cardType);

//             _businessLogicService.ValidateCardExist(allCards, shortCardName, cardNumber);

//             var newCard = new Card
//             {
//                 CardNumber = cardNumber,
//                 CardName = shortCardName,
//                 Currency = currency,
//                 CardType = cardType
//             };

//             allCards.Add(newCard);
//             _businessLogicService.AddBonusOnOpen(newCard);

//             return newCard;
//         }

//         /// <summary>
//         /// Transfer money
//         /// </summary>
//         /// <param name="sum">sum of operation</param>
//         /// <param name="from">card number</param>
//         /// <param name="to">card number</param>
//         public Transaction TransferMoney(decimal sum, string from, string to)
//         {
//             var cardFrom = GetCard(from);
//             var cardTo = GetCard(to);

//             _businessLogicService.ValidateTransfer(cardFrom, cardTo, sum);

//             var fromTransaction = new Transaction
//             {
//                 CardFromNumber = cardFrom.CardNumber,
//                 CardToNumber = cardTo.CardNumber,
//                 Sum = sum
//             };

//             var toTransaction = new Transaction
//             {
//                 DateTime = fromTransaction.DateTime,
//                 CardFromNumber = cardFrom.CardNumber,
//                 CardToNumber = cardTo.CardNumber,
//                 Sum = _businessLogicService.GetConvertSum(sum, cardFrom.Currency, cardTo.Currency)
//             };

//             cardFrom.Transactions.Add(fromTransaction);
//             cardTo.Transactions.Add(toTransaction);

//             return fromTransaction;
//         }
//     }
// }