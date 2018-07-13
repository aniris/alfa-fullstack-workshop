using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.ViewModels
{
    /// <summary>
    /// Card model Dto to return from controller
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Card number
        /// </summary>
        /// <returns><see langword="string"/> card number representation</returns>
        public string Number { get; set; }

        /// <summary>
        /// Short name of the cards
        /// </summary>
        /// <returns><see langword="string"/> short card name representation</returns>
        public string Name { get; set; }

        /// <summary>
        /// Card <see cref="Currency"/>
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// Card <see cref="CardType"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// DTOpenCard
        /// </summary>
        public string Exp { get; set; }

        /// <summary>
        /// Get balance. Must be VIRTUAL
        /// </summary>
        public decimal? Balance { get; set; }
    }
}