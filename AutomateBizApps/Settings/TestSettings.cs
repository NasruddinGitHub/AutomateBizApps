using AutomateBizApps.Constants;
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
       public static readonly string? Browser = TestContext.Parameters[Property.BrowserType];

        public static BrowserTypeLaunchOptions browsertypeLaunchOptions = new BrowserTypeLaunchOptions 
        { 
            Headless = false,
            Channel = Browser,
            Args = new List<string> { "--start-maximized" }
        };

        public static BrowserNewContextOptions browserNewContextOptions = new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        };
    }
}
