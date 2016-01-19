using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Awesomium.Core;

using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Litmus.Domain
{
    public class Login : ILogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }

        private Configuration config;

        private WebView webView;

        public Login()
        {
            WebCore.Initialize(new WebConfig()
            {
                CustomCSS = "::-webkit-scrollbar { visibility: hidden; }"
            });

            WebSession session = WebCore.CreateWebSession(new WebPreferences { WebSecurity = false });

            webView = WebCore.CreateWebView(1024, 600, session, WebViewType.Offscreen);

            config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = "App.config" }, ConfigurationUserLevel.None);

            FileName = config.AppSettings.Settings["FileName"].Value;
        }

        public bool GmailTakeScreenshot()
        {
            bool results = false;

            webView.Source = new Uri("https://accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/");

            webView.LoadingFrameComplete += (s, e) =>
            {
                if (!e.IsMainFrame)
                    return;

                try
                {
                    dynamic document = (JSObject)webView.ExecuteJavascriptWithResult("document");

                    var email = document.getElementById("Email");
                    email.value = Username;

                    var next = document.getElementById("next");
                    next.click();

                    Thread.Sleep(500);

                    var password = document.getElementById("Passwd");
                    password.value = Password;

                    var signin = document.getElementById("signIn");
                    signin.click();
                }
                catch(Exception ex)
                {

                }

                webView.LoadingFrameComplete += (sender, argEvents) =>
                {
                    if (!argEvents.IsMainFrame)
                        return;

                    Thread.Sleep(500);

                    BitmapSurface surface = (BitmapSurface)webView.Surface;
                    surface.SaveToPNG(FileName, true);

                    results = true;

                    WebCore.Shutdown();
                };
            };

            WebCore.Run();

            return results;
        }
    }
}
