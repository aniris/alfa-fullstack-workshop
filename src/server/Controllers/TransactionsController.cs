using System;
using System.Collections;
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
    public class TransactionsController : Controller
    {
        private readonly IBankRepository _repository;

        private readonly ICardService _cardService;

        private readonly IBusinessLogicService _businessLogicService;
        
        private readonly IMapper _mapper;

        public TransactionsController(IBankRepository repository, ICardService cardService, IBusinessLogicService businessLogicService)
        {
            _repository = repository;
            _cardService = cardService;
            _businessLogicService = businessLogicService;
            
            var transactionConverter = new TransactionConverter(_businessLogicService, _cardService);
            var confMap = new MapperConfiguration(cfg => cfg.CreateMap<Transaction, TransactionDto>().ConvertUsing(transactionConverter));
            _mapper = new Mapper(confMap);
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

            return _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionDto>>(
                transactions,
                opts => opts.Items["number"] = number
            );
        }

        // POST api/transactions
        [HttpPost]
        public IActionResult Post([FromBody] TransactionDto value)
        {
            if (value == null) throw new UserDataException("transaction data is null", null);

            _businessLogicService.ValidateTransferDto(value);

            var transaction = _repository.TransferMoney(value.Sum, value.From, value.To);

            return Created(
                $"/transactions/{_cardService.CreateNormalizeCardNumber(value.From)}",
                _mapper.Map<Transaction, TransactionDto>(
                    transaction,
                    opts => opts.Items["number"] = value.From
                )
            );
        }

        // DELETE api/transactions
        [HttpDelete]
        public IActionResult Delete() => StatusCode(405);

        // PUT api/transactions
        [HttpPut]
        public IActionResult Put() => StatusCode(405);
    }
}
