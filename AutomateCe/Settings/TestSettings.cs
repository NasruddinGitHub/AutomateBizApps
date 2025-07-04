﻿using AutomateCe.Constants;
using AutomateCe.Utils;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Settings
{
    public static class TestSettings
    {

        public static BrowserTypeLaunchOptions BrowserTypeLaunchOptions()
        {
            return new BrowserTypeLaunchOptions
            {
                Headless = bool.Parse(TestContextUtil.GetParameter(Property.HeadlessMode)),
                Channel = TestContextUtil.GetBrowser(),
                Args = new List<string> { "--start-maximized", "--incognito", "--auth-server-whitelist=\"_\"" }
            };
        }

        public static BrowserNewContextOptions BrowserNewContextOptions()
        {
            return new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
                RecordVideoDir = TestContextUtil.GetVideoRecordingDir()
            };
        }
    }
}
