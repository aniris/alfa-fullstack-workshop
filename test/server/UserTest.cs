using System.Runtime.InteropServices;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;
using Xunit;

namespace ServerTest
{
    public class UserTest
    {
        [Theory]
        [InlineData(null, "password")]
        [InlineData("username", null)]
        public void CheckUserException(string login, string password)
        {
            Assert.Throws<UserDataException>(() => new User(login, password));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("qwe")]
        [InlineData("qwertyuiopasdfgh")]
        [InlineData("%asdhldf")]
        [InlineData("asdhldf%")]
        [InlineData("%mail@ru")]
        [InlineData("_._._._")]
        public void CheckUserNameException(string login)
        {
            UserExceptionTypes type = new UserExceptionTypes();
            try
            {
                new User(login, "password");
            }
            catch (UserDataException ex)
            {
                type = ex.Type;
            }
            Assert.True(type == UserExceptionTypes.UserName);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123456")]
        [InlineData("012345678901234567890")]
        public void CheckPasswordException(string password)
        {
            UserExceptionTypes type = new UserExceptionTypes();
            try
            {
                new User("login", password);
            }
            catch (UserDataException ex)
            {
                type = ex.Type;
            }
            Assert.True(type == UserExceptionTypes.Password);
        }
    }
}