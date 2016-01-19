using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litmus.Domain
{
    public interface ILogin
    {
        string Username { get; set; }
        string Password { get; set; }
        string FileName { get; set; }

        bool GmailTakeScreenshot();
    }
}
