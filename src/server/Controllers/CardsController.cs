using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.AutoMapper;
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

        private readonly IBusinessLogicService _businessLogicService;
        
        private readonly IMapper _mapper;

        public CardsController(IBankRepository repository, ICardService cardService, IBusinessLogicService businessLogicService)
        {
            _repository = repository;
            _cardService = cardService;
            _businessLogicService = businessLogicService;
            
            var cardConverter = new CardConverter(_businessLogicService, _cardService);
            var confMap = new MapperConfiguration(cfg => cfg.CreateMap<Card, CardDto>().ConvertUsing(cardConverter));
            _mapper = new Mapper(confMap);
        }

        // GET api/cards
        [HttpGet]
        public IEnumerable<CardDto> Get()
        {
            var cards = _repository.GetCards();

            return _mapper.Map<IEnumerable<Card>, IEnumerable<CardDto>>(cards);
        }

        // GET api/cards/5334343434343...
        [HttpGet("{number}")]
        public CardDto Get(string number)
        {
            if (!_cardService.CheckCardEmmiter(number))
                throw new HttpStatusCodeException(400, "card number is incorrect");
            
            var card = _repository.GetCard(number);

            return _mapper.Map<Card, CardDto>(card);
        }

        // POST api/cards
        [HttpPost]
        public IActionResult Post([FromBody] CardDto value)
        {
            if (value == null) throw new UserDataException("Card data is null", null);

            _businessLogicService.ValidateOpenCardDto(value);

            if (string.IsNullOrWhiteSpace(value.Name))
                throw new UserDataException("Short name of the card is invalid", value.Name);

            var card = _repository.OpenNewCard(value.Name, (Currency)value.Currency, (CardType)value.Type);

            return Created($"/api/cards/{card.CardNumber}", _mapper.Map<Card, CardDto>(card));
        }

        // DELETE api/cards
        [HttpDelete]
        public IActionResult Delete() => StatusCode(405);

        // PUT api/cards
        [HttpPut]
        public IActionResult Put() => StatusCode(405);
    }
}