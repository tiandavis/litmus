using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Litmus.Services;

namespace Litmus
{
    public class Program
    {
        static void Main(string[] args)
        {
            string username = "";
            string password = "";

            Console.WriteLine("Litmus Gmail Screenshot");
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Usage: Litmus.exe <Username> <Password>");
            Console.WriteLine("<Username> and <Password> for your Gmail account. No quotes needed around <Username> or <Password>.");
            Console.WriteLine(Environment.NewLine);

            if (args.Length > 1)
            {
                username = args[0];
                password = args[1];
            }
            else
            {
                Console.WriteLine("Enter Gmail Username:");
                username = Console.ReadLine();

                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("Enter Gmail Password:");
                password = Console.ReadLine();

                Console.WriteLine(Environment.NewLine);
            }

            ILoginService loginService = new LoginService();
            loginService.LoginGmailTakeScreenshot(username, password);

            IScreenshotService screenshotService = new ScreenshotService();
            string url = screenshotService.UploadScreenshotToAmazonS3RecordTransaction();

            Console.WriteLine("Amazon S3 Url: ");
            Console.WriteLine(url);

            Console.ReadLine();
        }
    }
}
