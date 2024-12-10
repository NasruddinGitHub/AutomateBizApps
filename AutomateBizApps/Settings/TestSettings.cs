using AutomateBizApps.Constants;
using AutomateCe.Utils;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateBizApps.Settings
{
    public static class TestSettings
    {
       public static readonly string? Browser = TestContextUtil.GetBrowser();

        public static BrowserTypeLaunchOptions browsertypeLaunchOptions = new BrowserTypeLaunchOptions
        {
            Headless = bool.Parse(TestContextUtil.GetParameter(Property.HeadlessMode)),
            Channel = Browser,
            Args = new List<string> { "--start-maximized"}
        };

        public static BrowserNewContextOptions browserNewContextOptions = new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport,
            RecordVideoDir = TestContextUtil.GetVideoRecordingDir()
        };
    }
}
