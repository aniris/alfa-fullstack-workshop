using AutoMapper;
using Server.Models;
using Server.Services;
using Server.ViewModels;

namespace Server.AutoMapper
{
    public class CardConverter : ITypeConverter<Card, CardDto>
    {
        private readonly IBusinessLogicService _businessLogicService;
        private readonly ICardService _cardService;

        public CardConverter(IBusinessLogicService businessLogicService, ICardService cardService)
        {
            _businessLogicService = businessLogicService;
            _cardService = cardService;
        }
        public CardDto Convert(Card source, CardDto destination, ResolutionContext context)
        {
            
            return new CardDto
            {
                Number = source.CardNumber,
                Type = (int)source.CardType,
                Name = source.CardName,
                Currency = (int)source.Currency,
                Exp = _cardService.GetExpDateFromDateTime(source.DTOpenCard, source.ValidityYear),
                Balance = _businessLogicService.GetRoundBalanceOfCard(source)
            };
        }
    }
}