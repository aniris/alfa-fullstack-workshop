using System;
using System.ComponentModel.DataAnnotations;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// Transaction model
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Public Time of transaction
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Sum in transaction
        /// </summary>
        /// <returns><see langword="decimal"/>representation of the sum transaction</returns>
        [Required]
        public decimal Sum { get; set; }

        /// <summary>
        /// Link to valid card
        /// </summary>
        /// <returns><see cref="Card"/></returns>
        public string CardFromNumber { get; set; }

        /// <summary>
        /// Link to valid card
        /// </summary>
        /// <returns><see cref="Card"/></returns>
        [Required]
        public string CardToNumber { get; set; }

        /// <summary>
        /// Link to card
        /// </summary>
        /// <value></value>
        [Required]
        public Card Card { get; set; }
    }
}