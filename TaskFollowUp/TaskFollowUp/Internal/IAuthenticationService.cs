using System;
using System.Web.Security;

namespace TaskFollowUp.Internal
{
    public interface IAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
        bool Authenticate(string username, string password, bool createPersistentCookie);
    }

    public class FormsAuthenticationService : IAuthenticationService
    {

        public bool Authenticate(string username, string password, bool createPersistentCookie = false)
        {
            var result = username == "test" && password == "test";
            if (result)
                SignIn(username, createPersistentCookie);
            return result;
        }

        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}