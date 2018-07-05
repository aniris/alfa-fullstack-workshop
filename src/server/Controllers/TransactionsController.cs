using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IBankRepository _repository;

        private readonly ICardService _cardService;

        public TransactionsController(IBankRepository repository, ICardService cardService)
        {
            _repository = repository;
            _cardService = cardService;
        }
        
        // GET api/transactions/(card number)/?from=
        [HttpGet("{cardNumber}")]
        public IEnumerable<Transaction> Get([FromQuery(Name = "from")] int from, string cardNumber)
        {
            if (!_cardService.CheckCardEmmiter(cardNumber))
                throw new UserDataException("Card number is invalid", cardNumber);
            
            return _repository.GetTranasctions(cardNumber, from);
        }
        
        // POST api/transactions
        [HttpPost]
        public IActionResult Post([FromBody] TransactionFromData data)
        {
            if (!ModelState.IsValid || data == null)
                throw new HttpStatusCodeException(400, "all fields must be filled");
            
            
            return Ok(Json(_repository.TransferMoney(data.sum, data.from, data.to)));
        }
        
        // DELETE api/transaction/5
        [HttpDelete("{number}")]
        public IActionResult Delete(string number) => throw new HttpStatusCodeException(405, "Method Not Allowed");

        //PUT api/transaction/
        [HttpPut]
        public IActionResult Put(object data) => throw new HttpStatusCodeException(405, "Method Not Allowed");
    }
}