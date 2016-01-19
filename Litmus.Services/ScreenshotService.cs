using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Litmus.Domain;
using Litmus.Persistence;

namespace Litmus.Services
{
    public class ScreenshotService : IScreenshotService
    {
        private IScreenshot screenshot;
        private IUpload upload;
        private IScreenshotRepository repository;

        public ScreenshotService()
        {
            upload = new Upload();
            repository = new ScreenshotRepository();
        }

        public string UploadScreenshotToAmazonS3RecordTransaction()
        {
            //Upload screenshot to Amazon S3
            screenshot = upload.AmazonS3();

            //Insert screenshot into the Screenshots table
            repository.Add(screenshot);

            return screenshot.Url;
        }
    }
}
