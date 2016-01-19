using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Litmus.Domain;

namespace Litmus.Services
{
    public class LoginService : ILoginService
    {
        private ILogin login;

        public LoginService()
        {
            login = new Login();
        }

        public void LoginGmailTakeScreenshot(string username, string password)
        {
            //Login to Gmail and Take Screenshot
            login.Username = username;
            login.Password = password;

            login.GmailTakeScreenshot();
        }
    }
}
