using System;
using Xunit;
using Server.Services;

namespace ServerTest
{
    /// <summary>
    /// Tests for <see cref="CardService"/>>
    /// </summary>
    public class CardServiceTest
    {
        private readonly ICardService cardService = new CardService();

        /// <summary>
        /// Check if this cards numbers is valid
        /// </summary>
        [Theory]
        [InlineData("5610 5910 8101 8250")]
        [InlineData("5610591081018250")]
        [InlineData("5511    5910 8101 8250")]
        public void CheckCardNumberOK(string value)
        {
            var result = cardService.CheckCardNumber(value);

            Assert.True(result, $"{value} is a valid card number");
        }

        /// <summary>
        /// Check if this cards numbers is not valid
        /// </summary>
        [Theory]
        [InlineData("1234 1234 1233 1234")]
        [InlineData("1234 1234 1233 12341")]
        [InlineData("-234 1234 1233 1234")]
        [InlineData("1a34 1234 1233 1234")]
        [InlineData("")]
        [InlineData("1234 1233 1233")]
        [InlineData("1234 1233 1233  ")]
        public void CheckCardNumberFailed(string value)
        {
            var result = cardService.CheckCardNumber(value);

            Assert.False(result, $"{value} is not a valid card number");
        }
    }
}
