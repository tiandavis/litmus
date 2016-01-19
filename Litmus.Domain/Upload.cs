using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace Litmus.Domain
{
    public class Upload : IUpload
    {
        public AWSCredentials credentials;
        public IAmazonS3 s3Client;
        public RegionEndpoint s3RegionEndpoint { get; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string BucketName { get; set; }
        public Configuration config;

        private IScreenshot screenshot;

        public Upload()
        {
            config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = "App.config" }, ConfigurationUserLevel.None);

            string AWSAccessKey = config.AppSettings.Settings["AWSAccessKey"].Value;
            string AWSSecretKey = config.AppSettings.Settings["AWSSecretKey"].Value;
            s3Client = new AmazonS3Client(AWSAccessKey, AWSSecretKey, RegionEndpoint.USEast1);
            s3RegionEndpoint = RegionEndpoint.USEast1;

            FilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            FileName = config.AppSettings.Settings["FileName"].Value;
            BucketName = config.AppSettings.Settings["BucketName"].Value;

            screenshot = new Screenshot();
        }

        public IScreenshot AmazonS3()
        {
            if(File.Exists(FileName))
            {
                string[] files = Directory.GetFiles(FilePath, FileName);
                
                try
                {
                    //Upload to Amazon
                    string timeStamp = GenerateTimeStamp();

                    PutObjectRequest request = new PutObjectRequest();

                    request.BucketName = BucketName;
                    request.FilePath = files.FirstOrDefault();
                    request.CannedACL = S3CannedACL.PublicRead;
                    request.Key = timeStamp + "/" + FileName;
                    request.Metadata.Add("title", FileName);
                    request.Metadata.Add("timestamp", timeStamp);

                    PutObjectResponse response = s3Client.PutObject(request);

                    //Get the Amazon S3 file Url
                    screenshot.Url = $"https://{BucketName}.s3.amazonaws.com/{timeStamp}/{FileName}";
                    screenshot.CreatedAt = DateTime.Now;
                    screenshot.UpdatedAt = DateTime.Now;

                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    Console.WriteLine(amazonS3Exception.Message);
                }
            }

            return screenshot;
        }

        private string GenerateTimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }
    }
}
