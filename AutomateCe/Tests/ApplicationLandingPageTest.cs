using AutomateCe.Constants;
using AutomateCe.Modules;
using AutomateCe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Tests
{
    [TestFixture]
    public class ApplicationLandingPageTest : BaseTest
    {
        string? email = TestContext.Parameters[Property.Email];
        string? password = TestContext.Parameters[Property.Password];
        string? mfaKey = TestContext.Parameters[Property.MfaKey];

        [Property("browser", "firefox")]
        [Test]
        public async Task ValidateIfUserIsAbleToSelectAnApp()
        {
            String appToSelect = "Sales Hub";
            ReportUtil.CreateTest("Validate is the user is able to select a MDA app from the landing page");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");
            ReportUtil.AssignDevice("Desktop - 1280x720");

            CeApp ceApp = new CeApp(GetPage());
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenAppAsync(appToSelect);
            ReportUtil.Info($"Selected {appToSelect} application");
        }

        [Property("browser", "msedge")]
        [Test]
        public async Task ValidateIfUserIsAbleToSignout()
        {

            ReportUtil.CreateTest("Validate whether the user is able to signout after logging in");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("E2E");
            ReportUtil.AssignDevice("Mobile - 300x450");

            CeApp ceApp = new CeApp(GetPage());
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.LogoutAsync();
            ReportUtil.Info("User is able to log-out successfully");
        }

        [Test]
        public async Task ValidateIfUserIsAbleSearchForAnApp()
        {
            string appToSearch = "Customer Service Hub";
            ReportUtil.CreateTest("Validate whether the user is able to search for an MDA application");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Regression");
            ReportUtil.AssignDevice("Tab - 600x350");

            CeApp ceApp = new CeApp(GetPage());
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.SearchAsync(appToSearch);
            ReportUtil.Info($"User is able to search for the app {appToSearch} successfully");
        }

        [Test]
        public async Task ValidateIfUserIsAbleRefreshInAppLandingPage()
        {
            ReportUtil.CreateTest("Validate whether the user is able to refresh");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Sanity");
            ReportUtil.AssignDevice("Tab - 600x350");

            CeApp ceApp = new CeApp(GetPage());
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.RefreshAsync();
            ReportUtil.Info("User has clicked on refresh successfully");
        }

        [Test]
        public async Task ValidateIfUserIsAbleToGetTheCountOfPublishedApps()
        {
            ReportUtil.CreateTest("Validate whether the user is able to get the published app's count");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Functional");
            ReportUtil.AssignDevice("Tab - 600x350");

            CeApp ceApp = new CeApp(GetPage());
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            Console.WriteLine(await ceApp.ApplicationLandingPageModule.GetNumberOfPublishedAppsAsync());
            ReportUtil.Info("User is able to get the number of published app count successfully");
        }

        [Test]
        public async Task ValidateIfUserIsAbleToGetTheNamesOfAvailableMdaApps()
        {
            ReportUtil.CreateTest("Validate whether the user is able to get the names of all published apps");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Functional");
            ReportUtil.AssignDevice("Tab - 600x350");

            CeApp ceApp = new CeApp(GetPage());
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            List<string> allAppNames = await ceApp.ApplicationLandingPageModule.GetAllAvailableAppNamesAsync();
            foreach (string appName in allAppNames)
            {
                Console.WriteLine(appName);
            }

            ReportUtil.Info("User is able to get the name of all published apps successfully");
        }

        [Test]
        public async Task ValidateIfAppIsAvailableInTheAvailableApps()
        {
            string appName = "Sales Hub";
            ReportUtil.CreateTest("Validate if app is available in the available apps");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Functional");
            ReportUtil.AssignDevice("Tab - 600x350");

            CeApp ceApp = new CeApp(GetPage());
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            bool isAppAvailable = await ceApp.ApplicationLandingPageModule.IsMdaAppAvailableAsync(appName);

            ReportUtil.Info($"User is able to verify if {appName} is available successfully");
        }
    }
}
