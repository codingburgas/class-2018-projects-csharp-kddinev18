using BusinessLogicLayer;
using System;

namespace ServiceLayer
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

        public static int LogIn(string userName, string password, bool doRememberMe)
        {
            try
            {
                return UserLogic.LogIn(userName, password, doRememberMe);
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

        public static int? LogInWithCookies()
        {
            int? userId = UserLogic.CheckCookies();
            if (userId is null)
            {
                return null;
            }

            return userId.Value;
        }
    }
}
