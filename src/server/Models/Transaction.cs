using System;

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
        public int Id { get; set; }

        /// <summary>
        /// Public Time of transaction
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Sum in transaction
        /// </summary>
        /// <returns><see langword="decimal"/>representation of the sum transaction</returns>
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
        public string CardToNumber { get; set; }
    }
}