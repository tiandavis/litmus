using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;

using NUnit.Framework;
using Litmus.Domain;
using Litmus.Persistence;

using MySql;
using MySql.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Dapper;

using Faker;

namespace Litmus.Tests
{
    [TestFixture]
    public class ScreenshotRepositoryTests : BaseTests
    {
        private IScreenshotRepository repository;

        [SetUp]
        public void BeforeEach()
        {
            repository = new ScreenshotRepository();
        }

        [TearDown]
        public void AfterEach()
        {
            repository.RemoveUrlBy("robohash");
        }

        [Test]
        public void Find()
        {
            IScreenshot screenshot = new Screenshot()
            {
                Url = Faker.RoboHash.Image(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Assert.AreEqual(1, repository.Add(screenshot));

            IScreenshot screenshot1 = repository.Find(screenshot.Url);

            Assert.AreEqual(screenshot1.Url, screenshot.Url);
            Assert.AreEqual(screenshot1.CreatedAt.ToString("MM/dd/yyyy HH:mm"), screenshot.CreatedAt.ToString("MM/dd/yyyy HH:mm"));
            Assert.AreEqual(screenshot1.UpdatedAt.ToString("MM/dd/yyyy HH:mm"), screenshot.UpdatedAt.ToString("MM/dd/yyyy HH:mm"));
        }

        [Test]
        public void GetAll()
        {
            IScreenshot screenshot1 = new Screenshot()
            {
                Url = Faker.RoboHash.Image(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            IScreenshot screenshot2 = new Screenshot()
            {
                Url = Faker.RoboHash.Image(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Assert.AreEqual(1, repository.Add(screenshot1));
            Assert.AreEqual(1, repository.Add(screenshot2));

            Assert.AreEqual(true, (repository.GetAll().Count > 0));

        }

        [Test]
        public void Add()
        {
            IScreenshot screenshot = new Screenshot()
            {
                Url = Faker.RoboHash.Image(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Assert.AreEqual(1, repository.Add(screenshot));
        }

        [Test]
        public void Update()
        {
            IScreenshot screenshot = new Screenshot()
            {
                Url = Faker.RoboHash.Image(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Assert.AreEqual(1, repository.Add(screenshot));

            screenshot.UpdatedAt = DateTime.Now;

            Assert.AreEqual(1, repository.Update(screenshot));
        }

        [Test]
        public void Remove()
        {
            IScreenshot screenshot = new Screenshot()
            {
                Url = Faker.RoboHash.Image(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Assert.AreEqual(1, repository.Add(screenshot));

            Assert.AreEqual(1, repository.Remove(screenshot.Url));
        }
    }
}