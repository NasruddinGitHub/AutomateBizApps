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
            await ceApp.ApplicationLandingPageModule.OpenApp("Field Service");
            await ceApp.SiteMapPanel.OpenSubArea("Service Delivery", "Cases");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord(0, "asds");
            await ceApp.BusinessProcessFlow.SelectStage("Identify");
            await ceApp.BusinessProcessFlow.Pin();

            LookupItem customerLookupItem = new LookupItem { Name = "Find Customer", Value = "Coho Winery (sample)", Index = 0 };
            await ceApp.BusinessProcessFlow.ClearValue(customerLookupItem);
            await ceApp.BusinessProcessFlow.SetValue(customerLookupItem);
            LookupItem contactLookupItem = new LookupItem { Name = "Find Contact", Value = "Thomas Andersen", Index = 0 };
            await ceApp.BusinessProcessFlow.SetValue(contactLookupItem);
            await ceApp.BusinessProcessFlow.NextStage();
            Console.WriteLine(await ceApp.BusinessProcessFlow.Header());
            await ceApp.BusinessProcessFlow.PreviousStage();
            await ceApp.BusinessProcessFlow.Close();
        }
    }
}
