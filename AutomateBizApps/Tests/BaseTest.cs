using AutomateBizApps.Constants;
using AutomateBizApps.Enums;
using AutomateBizApps.Settings;
using Microsoft.Playwright;
using PlayWrightMVP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateBizApps.Tests
{
    [TestFixture]
    public class BaseTest : PageTest
    {
        public IBrowser browser;

        protected IPage page;

        [SetUp]
        public async Task OpenBrowser()
        {
            string? browserParam = TestContext.Parameters[Property.BrowserType];
            browser = await OpenBrowser(browserParam);

            IBrowserContext browserContext = await browser.NewContextAsync(TestSettings.browserNewContextOptions).ConfigureAwait(false);

            page = await browserContext.NewPageAsync().ConfigureAwait(false);
        }

        [SetUp]
        public async Task GoToUrl()
        {
            string? url = TestContext.Parameters["Url"];
            await page.GotoAsync(url);
        }

        [TearDown]
        public async Task CloseBrowserInstance()
        {
           await page.CloseAsync();
           await browser.CloseAsync();
        }

        public async Task<IBrowser> OpenBrowser(string browser)
        {
            switch (browser.ToLower())
            {
                case "msedge":
                case "chrome":
                    return await Playwright.Chromium.LaunchAsync(TestSettings.browsertypeLaunchOptions);
                case "firefox":
                    return await Playwright.Firefox.LaunchAsync(TestSettings.browsertypeLaunchOptions);
                default:
                    throw new ArgumentException("Invalid browser name is given. Given browser is " + browser);
            }
        }

    }
}
