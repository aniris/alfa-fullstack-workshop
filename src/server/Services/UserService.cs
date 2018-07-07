using System;
using System.Net.Mail;

namespace Server.Services
{
    public class UserService
    {
        private MailAddress _mail;
        public bool CheckUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            try
            {
                _mail = new MailAddress(userName);
            }
            catch (FormatException)
            {
                return false;
            }
            
            return true;
        }
    }
}