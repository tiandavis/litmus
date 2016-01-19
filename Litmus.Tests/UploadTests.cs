using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Litmus.Domain;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;

namespace Litmus.Tests
{
    [TestFixture]
    public class UploadTests : BaseTests
    {
        [SetUp]
        public void BeforeEach()
        {
            
        }

        [Test]
        public void HasAWSCredentials()
        {
            string AWSAccessKey = config.AppSettings.Settings["AWSAccessKey"].Value;
            string AWSSecretKey = config.AppSettings.Settings["AWSSecretKey"].Value;

            Assert.IsNotEmpty(AWSAccessKey);
            Assert.IsNotEmpty(AWSSecretKey);
        }

        [Test]
        public void HasCorrectAmazonS3RegionEndpoint()
        {
            IUpload upload = new Upload();

            Assert.AreEqual(RegionEndpoint.USEast1, upload.s3RegionEndpoint);
        }

        [Test]
        public void HasValidBucketName()
        {
            string bucketName = config.AppSettings.Settings["BucketName"].Value;

            //Bucket names must be at least 3 and no more than 63 characters long.
            Assert.AreEqual(true, (bucketName.Length > 3 && bucketName.Length < 63));

            //Bucket names can contain lowercase letters, numbers, periods and hyphens.
            Assert.AreEqual(true, Regex.IsMatch(bucketName, @"^[a-z0-9.-]+$"));

            //Bucket names must start and end with a lowercase letter or a number
            Assert.AreEqual(true, char.IsLetterOrDigit(bucketName.FirstOrDefault()));
            Assert.AreEqual(true, char.IsLetterOrDigit(bucketName.Last()));

            //Bucket names must not be formatted as an IP address
            IPAddress ip;
            Assert.AreEqual(false, IPAddress.TryParse(bucketName, out ip));

            //Bucket names with a period (.) won't pass SSL wild card certificate matches
            Assert.AreEqual(false, bucketName.Contains("."));
        }

        [Test]
        public void UploadScreenshotToAmazonS3()
        {
            IUpload upload = new Upload();
            IScreenshot screenshot;

            //Upload that screenshot to Amazon S3
            screenshot = upload.AmazonS3();

            Assert.AreEqual(true, screenshot.Url.Contains(config.AppSettings.Settings["BucketName"].Value));
            Assert.AreEqual(true, screenshot.Url.Contains(config.AppSettings.Settings["FileName"].Value));
            Assert.AreEqual(false, screenshot.Url.Contains("Expires"));


            //Check the Amazon Url is valid
            Uri uriResult;
            bool isValidUrl = Uri.TryCreate(screenshot.Url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            Assert.AreEqual(true, isValidUrl);
        }
    }
}