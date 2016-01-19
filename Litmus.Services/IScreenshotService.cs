using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Litmus.Domain;

namespace Litmus.Services
{
    public interface IScreenshotService
    {
        string UploadScreenshotToAmazonS3RecordTransaction();
    }
}
