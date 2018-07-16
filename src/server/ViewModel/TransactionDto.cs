using System;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// Transaction DTO model
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Public Time of transaction
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Sum in transaction
        /// </summary>
        /// <returns><see langword="decimal"/>representation of the sum transaction</returns>
        public decimal Sum { get; set; }

        /// <summary>
        /// Link to card
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Link to card
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Flag that operation is credit - else debiot
        /// </summary>
        public bool Credit { get; set; }
    }
}