using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// Card model
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Card number
        /// </summary>
        /// <returns><see langword="string"/> card number representation</returns>
        [MinLength(12)]
        [MaxLength(19)]
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// Short name of the cards
        /// </summary>
        /// <returns><see langword="string"/> short card name representation</returns>
        [MinLength(3)]
        public string CardName { get; set; }

        /// <summary>
        /// Card <see cref="Currency"/>
        /// </summary>
        [Required]
        public Currency Currency { get; set; }

        /// <summary>
        /// Card <see cref="CardType"/>
        /// </summary>
        [Required]
        public CardType CardType { get; set; }

        /// <summary>
        /// DTOpenCard
        /// </summary>
        public DateTime DTOpenCard { get; set; } = DateTime.Now;

        /// <summary>
        /// Count year's
        /// </summary>
        public int ValidityYear { get; set; } = 3;

        /// <summary>
        /// Return all transaction of the card
        /// </summary>
        /// <typeparam name="Transaction"></typeparam>
        /// <returns><see cref="List"/> of transactions</returns>
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();

        /// <summary>
        /// Link to User
        /// </summary>
        /// <returns><see cref="User"/></returns>
        public User User { get; set; }
    }
}