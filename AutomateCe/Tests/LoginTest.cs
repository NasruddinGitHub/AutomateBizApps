using AutomateBizApps.Constants;
using AutomateBizApps.Modules;
using AutomateBizApps.Pages;
using AutomateCe.Controls;
using AutomateCe.Modules;
using AutomateCe.Utils;
using Microsoft.Playwright;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateBizApps.Tests
{
    [TestFixture]
    public class LoginTest : BaseTest
    {
        string? email = TestContext.Parameters[Property.Email];
        string? password = TestContext.Parameters[Property.Password];
        string? mfaKey = TestContext.Parameters[Property.MfaKey];

        [Property("browser", "chrome")]
        [Test]
        public async Task ValidateIfValidUserIsAbleToLogin1()
        {
            ReportUtil.CreateTest("Login with valid username and valid password");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");
            string? email = TestContext.Parameters[Property.Email];
            string? password = TestContext.Parameters[Property.Password];
            string? mfaKey = TestContext.Parameters[Property.MfaKey];
            
            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Field Service");
            ReportUtil.Info("Selected Field Service application");
            //await ceApp.SiteMapPanel.OpenSubArea("Customers", "Accounts");
            //await ceApp.Complementary.OpenOrCloseTab("Copilot");
            //await ceApp.Grid.OpenRecord(0, "A. Datum Corasdporation (sample)");
        }

        [Property("browser", "msedge")]
        [Test]
        public async Task ValidateIfValidUserIsAbleToLogin2()
        {
            ReportUtil.CreateTest("Login with valid username and invalid password");
            ReportUtil.AssignAuthor("Rakesh Gattupalli");
            ReportUtil.AssignCategory("Sanity");
            string? email = TestContext.Parameters[Property.Email];
            string? password = TestContext.Parameters[Property.Password];
            string? mfaKey = TestContext.Parameters[Property.MfaKey];
            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Customer Service Hub");
            ReportUtil.Info("Selected Customer Service Hub application");
            //await ceApp.SiteMapPanel.OpenSubArea("Customers", "Accounts");
            //await ceApp.Complementary.OpenOrCloseTab("Copilot");
            //await ceApp.Grid.OpenRecord(0, "A. Datum Corasdporation (sample)");
        }

        [Test]
        public async Task ValidateIfValidUserIsAbleToLogin3()
        {
            ReportUtil.CreateTest("Login with invalid username and valid password");
            ReportUtil.AssignAuthor("Anoop Royal Gunturu");
            ReportUtil.AssignCategory("End 2 End");
            string? email = TestContext.Parameters[Property.Email];
            string? password = TestContext.Parameters[Property.Password];
            string? mfaKey = TestContext.Parameters[Property.MfaKey];
            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub1");
            ReportUtil.Info("Selected Sales Hub application");
            //await ceApp.SiteMapPanel.OpenSubArea("Customers", "Accounts");
            //await ceApp.Complementary.OpenOrCloseTab("Copilot");
            //await ceApp.Grid.OpenRecord(0, "A. Datum Corasdporation (sample)");
        }
    }
}
