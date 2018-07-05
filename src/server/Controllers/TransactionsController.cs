<<<<<<< HEAD
﻿using System.Collections.Generic;
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
=======
﻿using System;
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
    public class TransactionsController : Controller
    {
        private readonly IBankRepository _repository;

        private readonly ICardService _cardService;

        private readonly IBusinessLogicService _businessLogicServer;

        public TransactionsController(IBankRepository repository, ICardService cardService, IBusinessLogicService businessLogicServer)
        {
            _repository = repository;
            _cardService = cardService;
            _businessLogicServer = businessLogicServer;
        }

        // GET api/transactions/5334343434343?skip=...
        [HttpGet("{number}")]
        public IEnumerable<TransactionDto> Get(string number, [FromQuery] int skip)
        {
            if (!_cardService.CheckCardEmmiter(number))
                throw new UserDataException("Card number is invalid", number);

            if (skip < 0)
                throw new UserDataException("Skip must be greater than -1", skip.ToString());

            var transactions = _repository.GetTranasctions(number, skip, 10);

            return transactions.Select(transaction => new TransactionDto
            {
                DateTime = transaction.DateTime,
                From = transaction.CardFromNumber,
                To = transaction.CardToNumber,
                Sum = transaction.Sum,
                Credit = transaction.CardToNumber == _cardService.CreateNormalizeCardNumber(number)
            });
        }

        // POST api/transactions
        [HttpPost]
        public IActionResult Post([FromBody] TransactionDto value)
        {
            if (value == null) throw new UserDataException("transaction data is null", null);

            _businessLogicServer.ValidateTransferDto(value);

            var transaction = _repository.TransferMoney(value.Sum, value.From, value.To);

            return Created($"/transactions/{_cardService.CreateNormalizeCardNumber(value.From)}", new TransactionDto
            {
                DateTime = transaction.DateTime,
                From = transaction.CardFromNumber,
                To = transaction.CardToNumber,
                Sum = transaction.Sum,
                Credit = transaction.CardToNumber == _cardService.CreateNormalizeCardNumber(value.From)
            });
        }

        // DELETE api/transactions
        [HttpDelete]
        public IActionResult Delete() => StatusCode(405);

        // PUT api/transactions
        [HttpPut]
        public IActionResult Put() => StatusCode(405);
    }
}
>>>>>>> 016c4fa60f2f201a69d747c766956d5b1d7404fb
