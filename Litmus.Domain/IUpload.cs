using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon.Runtime;
using Amazon.S3;
using Amazon;

namespace Litmus.Domain
{
    public interface IUpload
    {
        RegionEndpoint s3RegionEndpoint { get; }

        IScreenshot AmazonS3();
    }
}
