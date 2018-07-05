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

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly IBankRepository _repository;

        private readonly ICardService _cardService;

        public CardsController(IBankRepository repository, ICardService cardService)
        {
            _repository = repository;
            _cardService = cardService;
        }

        // GET api/cards
        [HttpGet]
        public IEnumerable<Card> Get() => _repository.GetCards();

        // GET api/cards/5334343434343...
        [HttpGet("{number}")]
        public Card Get(string number)
        {
            if (!_cardService.CheckCardEmmiter(number))
                throw new HttpStatusCodeException(400, "card number is incorrect");
            
            return _repository.GetCard(number);
        }

        // POST api/cards
        [HttpPost]
        public IActionResult Post([FromBody] CardFromData data)
        {
            if (!ModelState.IsValid || data == null)
                throw new HttpStatusCodeException(400, "all fields must be filled");
            
            return  Json(_repository.OpenNewCard(data.name, data.currency, data.type));
        }

        // DELETE api/cards/5
        [HttpDelete("{number}")]
        public IActionResult Delete(string number) => throw new HttpStatusCodeException(405, "Method Not Allowed");

        //PUT api/cards/
        [HttpPut]
        public IActionResult Put(object data) => throw new HttpStatusCodeException(405, "Method Not Allowed");
    }
}
