using BusinessLogicLayer;
using System;

namespace JoinLayer_API_
{
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException() { }

        public WrongCredentialsException(string message) : base(message) { }

        public WrongCredentialsException(string message, Exception inner) : base(message, inner) { }
    }
    public class NotFilledRequiredFieldsException : Exception
    {
        public NotFilledRequiredFieldsException() { }

        public NotFilledRequiredFieldsException(string message) : base(message) { }

        public NotFilledRequiredFieldsException(string message, Exception inner) : base(message, inner) { }
    }
    public static class UserAuthentication
    {
        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            try
            {
                UserProfileLogic.ConfigureUserProfile(userId, gender, about, country, city);
            }
            catch (ArgumentNullException exception)
            {
                throw new NotFilledRequiredFieldsException(exception.Message);
            }
            catch (ArgumentException exception)
            {
                throw new WrongCredentialsException(exception.Message);
            }
            catch (Exception)
            {
                throw new Exception("Fatal error!");
            }
        }


    }
}
