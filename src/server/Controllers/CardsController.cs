using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Server.Services;
using Server.ViewModels;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly IBankRepository _repository;

        private readonly ICardService _cardService;

        private readonly IBusinessLogicService _businessLogicServer;

        public CardsController(IBankRepository repository, ICardService cardService, IBusinessLogicService businessLogicServer)
        {
            _repository = repository;
            _cardService = cardService;
            _businessLogicServer = businessLogicServer;
        }

        // GET api/cards
        [HttpGet]
        public IEnumerable<CardDto> Get()
        {
            var cards = _repository.GetCards();
            return cards.Select(card => new CardDto
            {
                Number = card.CardNumber,
                Type = (int)card.CardType,
                Name = card.CardName,
                Currency = (int)card.Currency,
                Exp = _cardService.GetExpDateFromDateTime(card.DTOpenCard, card.ValidityYear),
                Balance = null
            });
        }

        // GET api/cards/5334343434343...
        [HttpGet("{number}")]
        public CardDto Get(string number)
        {
            if (!_cardService.CheckCardEmmiter(number))
                throw new UserDataException("Card number is invalid", number);

            var card = _repository.GetCard(number);

            return new CardDto
            {
                Number = card.CardNumber,
                Type = (int)card.CardType,
                Name = card.CardName,
                Currency = (int)card.Currency,
                Exp = _cardService.GetExpDateFromDateTime(card.DTOpenCard, card.ValidityYear),
                Balance = _businessLogicServer.GetRoundBalanceOfCard(card)
            };
        }

        // POST api/cards
        [HttpPost]
        public IActionResult Post([FromBody] CardDto value)
        {
            if (value == null) throw new UserDataException("Card data is null", null);

            _businessLogicServer.ValidateOpenCardDto(value);

            if (string.IsNullOrWhiteSpace(value.Name))
                throw new UserDataException("Short name of the card is invalid", value.Name);

            var card = _repository.OpenNewCard(value.Name, (Currency)value.Currency, (CardType)value.Type);

            return Created($"/api/cards/{card.CardNumber}", new CardDto
            {
                Number = card.CardNumber,
                Type = (int)card.CardType,
                Name = card.CardName,
                Currency = (int)card.Currency,
                Exp = _cardService.GetExpDateFromDateTime(card.DTOpenCard, card.ValidityYear),
                Balance = _businessLogicServer.GetRoundBalanceOfCard(card)
            });
        }

        // DELETE api/cards
        [HttpDelete]
        public IActionResult Delete() => StatusCode(405);

        // PUT api/cards
        [HttpPut]
        public IActionResult Put() => StatusCode(405);
    }
}