using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Xunit;

namespace ServerTest
{
    public class CardTest
    {
        [Theory]
        [InlineData(null, "cardname")]
        [InlineData("4083969259636239", null)]
        public void CheckCardException(string cardNumber, string cardName)
        {
            Assert.Throws<CardDataException>(() => new Card(cardNumber, cardName, "username"));
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345")]
        [InlineData("as")]
        [InlineData("01234567890123456789")]
        [InlineData("4083969259636238")]
        public void CheckCardNumberException(string cardNumber)
        {
            CardExceptionTypes type = new CardExceptionTypes();
            try
            {
                new Card(cardNumber, "cardName", "username");
            }
            catch (CardDataException ex)
            {
                type = ex.Type;
            }
            
            Assert.True(type == CardExceptionTypes.Number);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1234")]
        [InlineData("01234567891")]
        public void CheckCardNameException(string cardName)
        {
            CardExceptionTypes type = new CardExceptionTypes();
            try
            {
                new Card("4083969259636239", cardName, "username");
            }
            catch (CardDataException ex)
            {
                type = ex.Type;
            }
            
            Assert.True(type == CardExceptionTypes.Name);
        }
    }
}