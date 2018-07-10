using AutoMapper;
using Server.Models;
using Server.Services;
using Server.ViewModels;

namespace Server.AutoMapper
{
    public class TransactionConverter : ITypeConverter<Transaction, TransactionDto>
    {
        private readonly IBusinessLogicService _businessLogicService;
        private readonly ICardService _cardService;

        public TransactionConverter(IBusinessLogicService businessLogicService, ICardService cardService)
        {
            _businessLogicService = businessLogicService;
            _cardService = cardService;
        }
        
        public TransactionDto Convert(Transaction source, TransactionDto destination, ResolutionContext context)
        {
            return new TransactionDto
            {
                DateTime = source.DateTime,
                From = source.CardFromNumber,
                To = source.CardToNumber,
                Sum = source.Sum,
                Credit = source.CardFromNumber == _cardService.CreateNormalizeCardNumber(context.Items["number"].ToString())
            };
        }
    }
}