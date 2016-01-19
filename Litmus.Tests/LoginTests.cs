using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Litmus.Domain;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Litmus.Tests
{
    [TestFixture]
    public class LoginTests : BaseTests
    {
        private string username;
        private string password;
        private string fileName;

        [SetUp]
        public void BeforeEach()
        {
            username = config.AppSettings.Settings["Username"].Value;
            password = config.AppSettings.Settings["Password"].Value;
            fileName = config.AppSettings.Settings["FileName"].Value;
        }

        [Test]
        public void HasValidUsername()
        {
            //You can use letters, numbers and periods
            Assert.AreEqual(true, Regex.IsMatch(username, @"^\w+(\.[0-9a-zA-Z]+)?$"));
        }

        [Test]
        public void HasValidPassword()
        {
            //You can use at least 8 characters that are not your username
            Assert.AreEqual(true, (username.Length > 8));
            Assert.AreNotEqual(username, password);
        }

        [Test]
        public void HasValidFileName()
        {
            string sampleKeyPrefix = "201601181151502325/";

            Assert.LessOrEqual((fileName.Length + sampleKeyPrefix.Length), 1024);
        }

        [Test]
        public void LoginGmailTakeScreenshot()
        {
            ILogin login = new Login();

            //First get a screenshot
            login.Username = username;
            login.Password = password;

            bool loginResults = login.GmailTakeScreenshot();

            Assert.AreEqual(true, loginResults);
            Assert.AreEqual(true, File.Exists(config.AppSettings.Settings["FileName"].Value));
        }
    }
}