using AutomateBizApps.Constants;
using AutomateBizApps.Modules;
using AutomateBizApps.Pages;
using AutomateCe.Controls;
using AutomateCe.Modules;
using Microsoft.Playwright;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateBizApps.Tests
{
    [TestFixture]
    public class LoginTest : BaseTest
    {

        [Test]
        public async Task ValidateIfValidUserIsAbleToLogin()
        {
            string? email = TestContext.Parameters[Property.Email];
            string? password = TestContext.Parameters[Property.Password];
            string? mfaKey = TestContext.Parameters[Property.MfaKey];

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Accounts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord(0, "A. Datum Corporation (sample)");
            Console.WriteLine(await ceApp.Entity.GetFormHeaderTitle());
            Console.WriteLine(await ceApp.ProcessCrossEntityFlyout.GetHeader());
            List<string> items = await ceApp.ProcessCrossEntityFlyout.GetItems();
            foreach (string item in items)
            {
                Console.WriteLine(item);
            }
            await ceApp.ProcessCrossEntityFlyout.Close();
            await ceApp.ProcessCrossEntityFlyout.Create();
            await ceApp.ProcessCrossEntityFlyout.SelectItem("asdasd");
        }
    }
}
