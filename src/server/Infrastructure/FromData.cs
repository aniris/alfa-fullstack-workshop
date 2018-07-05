using System.ComponentModel.DataAnnotations;

namespace Server.Infrastructure
{
    public class TransactionFromData
    {
        [Required]
        public decimal sum;
        [Required]
        public string from;
        [Required]
        public string to;
    }

    public class CardFromData
    {
        [Required]
        public string name;
        [Required]
        public Currency currency;
        [Required]
        public CardType type;
    }
}