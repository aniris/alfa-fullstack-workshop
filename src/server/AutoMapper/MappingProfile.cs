using System.Collections.Generic;
using AutoMapper;
using Server.AutoMapper;
using Server.Models;
using Server.Services;
using Server.ViewModels;

namespace Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDto>().ConvertUsing<CardConverter>();
            CreateMap<Transaction, TransactionDto>().ConvertUsing<TransactionConverter>();
        }
    }
}