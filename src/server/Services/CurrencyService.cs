using System;
using Server.Infrastructure;

namespace Server.Services
{
    public static class CurrencyService
    {
        public static decimal CurrencyConversion(CurrencyTypes from, decimal amount)
        {
            Random rnd = new Random();
            decimal rateRubleInDollar = (decimal)rnd.Next(5000, 6000)/100;

            switch (from)
            {
                case CurrencyTypes.Dollar:
                    return amount * rateRubleInDollar;
                case CurrencyTypes.Ruble:
                    return amount / rateRubleInDollar;
                default:
                    throw new Exception("Currency type is incorrect");
            }
        }
    }
}