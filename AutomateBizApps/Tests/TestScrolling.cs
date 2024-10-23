using AutomateBizApps.Constants;
using AutomateBizApps.Settings;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Tests
{
    public class TestScrolling : PageTest
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
            await page.GotoAsync("https://www.flipkart.com/");
        }

        public async Task TestScrollingFunction()
        {
            
            
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
