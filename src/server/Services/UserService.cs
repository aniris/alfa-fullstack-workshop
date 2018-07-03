using System.Text.RegularExpressions;
using Server.Exceptions;
using Server.Infrastructure;

namespace Server.Services
{
    public static class UserService
    {
        public static void CheckUserName(string userName)
        {
            const int maxLengthUserName = 15,
                minLengthUserName = 4;

            if (string.IsNullOrEmpty(userName))
                throw new UserDataException("username is null or empty", UserExceptionTypes.UserName, userName);

            if (userName.Length < minLengthUserName)
                throw new UserDataException("username length is too short", UserExceptionTypes.UserName, userName);

            if (userName.Length > maxLengthUserName)
                throw new UserDataException("username length is too long", UserExceptionTypes.UserName, userName);

            string pattern = @"^[a-z|0-9]+[a-z|0-9|\.|_|@|-]+[a-z|0-9]+$";

            Match match = Regex.Match(userName, pattern, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                throw new UserDataException("username contains incorrect symbols", UserExceptionTypes.UserName,userName);
            }
        }

        public static void CheckPassword(string password)
        {
            const int maxLengthPassword = 20,
                minLengthPassword = 7;
            
            if (string.IsNullOrEmpty(password))
                throw new UserDataException("password is null or empty", UserExceptionTypes.Password, password);
            
            if (password.Length < minLengthPassword)
                throw new UserDataException("password is too short", UserExceptionTypes.Password, password);
            
            if (password.Length > maxLengthPassword)
                throw new UserDataException("password is too long", UserExceptionTypes.Password, password);
        }
    }
}