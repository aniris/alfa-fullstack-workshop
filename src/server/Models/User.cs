using System;
using System.Collections.Generic;
using Server.Exceptions;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// User domain model
    /// </summary>
    public class User
    {
        private string _userName;
        private string _password;
        
        public User(string userName, string password)
        {
            try
            {
                UserName = userName;
                Password = password;

            }
            catch (UserDataException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Getter username of the user for login
        /// </summary>
        public string UserName
        {
            get => _userName;
            private set
            {
                try
                {
                    UserService.CheckUserName(value);
                }
                catch (UserDataException ex)
                {
                    throw;
                }

                _userName = value;
            }
        }

        /// <summary>
        /// Getter username of the user for login
        /// </summary>
        public string Password
        {
            get => _password;
            private set
            {
                try
                {
                    UserService.CheckPassword(value);
                }
                catch (UserDataException ex)
                {
                    throw;
                }

                _password = value;
            }
        }

        /// <summary>
        /// Getter user card list
        /// </summary>
        public List<Card> Cards { get; } = new List<Card>();

        /// <summary>
        /// Added new card to list
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="shortCardName"></param>
        /// <returns>Card object</returns>
        public Card AddNewCard(string cardNumber, string shortCardName)
        {
            Card newCard;
            try
            {
                newCard = new Card(cardNumber, shortCardName, UserName);
            }
            catch (CardDataException ex)
            {
                throw;
            }
            
            Cards.Add(newCard);
            newCard.AddMoneyForNewCard();

            return newCard;
        }
    }
}