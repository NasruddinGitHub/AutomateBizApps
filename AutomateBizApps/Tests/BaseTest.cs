using AutomateBizApps.Constants;
using AutomateBizApps.Enums;
using AutomateBizApps.Pages;
using AutomateBizApps.Settings;
using AutomateBizApps.Utils;
using AutomateCe.Utils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using PlayWrightMVP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomateBizApps.Tests
{
    
    public class BaseTest
    {
        public IBrowser browser;

        protected IPage page;

        private ExtentReports _extentReports;

        private string _timestamp;

        private IBrowserContext _browserContext;

        [OneTimeSetUp]
        public void InitializeExtentReport()
        {
            String timestamp = DateUtil.GetTimeStamp("yyyyMMddHHmmss");
            string projectRoot = TestContextUtil.GetProjectRootDir();
            string executionReportFolderPath = Path.Combine(projectRoot, TestContext.Parameters[Property.ExecutionReportsFolderPath]);
            string extentFilePath = Path.Combine(executionReportFolderPath, "Test_Execution_Report" + timestamp + ".html");
            _extentReports = ReportUtil.GetInstance(extentFilePath);
        }

        [SetUp]
        public async Task OpenBrowser()
        {
            _timestamp = DateUtil.GetTimeStamp("yyyyMMddHHmmss");
            browser = await OpenBrowser(TestContextUtil.GetBrowser());

            _browserContext = await browser.NewContextAsync(TestSettings.BrowserNewContextOptions()).ConfigureAwait(false);
            page = await _browserContext.NewPageAsync().ConfigureAwait(false);
        }

        [SetUp]
        public async Task GoToUrl()
        {
            string? url = TestContext.Parameters[Property.Url];
            await page.GotoAsync(url);
        }

        [TearDown]
        public async Task CloseBrowserInstance()
        {
            await page.CloseAsync();
            await _browserContext.CloseAsync();
            await browser.CloseAsync();
        }

        [TearDown]
        public async Task AfterTest()
        {
            string projectRoot = TestContextUtil.GetProjectRootDir();
            string screenshotsFolderPath = Path.Combine(projectRoot, TestContext.Parameters[Property.ScreenshotsFolderPath]);
            string testCaseName = TestContext.CurrentContext.Test.Name;
            string screenshotPath = Path.Combine(screenshotsFolderPath, testCaseName + _timestamp + ".png");
            await new BaseModule(page).PageScreenshotAsync(screenshotPath);
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                ReportUtil.FailTest("Test failed");
                bool needScreenshotForFailedTests = bool.Parse(TestContext.Parameters[Property.ScreenshotsForFailedTests]);
                if (needScreenshotForFailedTests)
                    ReportUtil.AddScreenCaptureFromPath(screenshotPath);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Skipped)
            {
                ReportUtil.SkipTest("Test skipped");
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                ReportUtil.PassTest("Test passed");
                bool needScreenshotForPassedTests = bool.Parse(TestContext.Parameters[Property.ScreenshotsForPassedTests]);
                if (needScreenshotForPassedTests)
                    ReportUtil.AddScreenCaptureFromPath(screenshotPath);
            }
        }

        [OneTimeTearDown]
        public void SaveReport()
        {
            if (_extentReports != null)
            {
                _extentReports.Flush();
            }
        }

        public async Task<IBrowser> OpenBrowser(string browser)
        {
            var playwright = await Playwright.CreateAsync();
            switch (browser.ToLower())
            {
                case "msedge":
                case "chrome":
                    return await playwright.Chromium.LaunchAsync(TestSettings.BrowserTypeLaunchOptions());
                case "firefox":
                    return await playwright.Firefox.LaunchAsync(TestSettings.BrowserTypeLaunchOptions());
                default:
                    throw new ArgumentException("Invalid browser name is given. Given browser is " + browser);
            }
        }

    }
}
