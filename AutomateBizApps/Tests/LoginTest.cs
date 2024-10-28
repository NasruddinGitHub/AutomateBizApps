using AutomateBizApps.Constants;
using AutomateBizApps.Modules;
using AutomateBizApps.Pages;
using AutomateCe.Controls;
using Microsoft.Playwright;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales trial");
            await ceApp.SiteMapPanel.OpenSubArea("Learn", "All Fields");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");
            string[] multiSelectOptions = {"Selenium", "Playwright", "Cypress", "Katalon Studio"};
            MultiSelectOptionSet multiSelectOptionSet = new MultiSelectOptionSet { Name = "MultiSelectChoice", Values =  multiSelectOptions  };
            await ceApp.Entity.SetValue(multiSelectOptionSet, true, "//label[text()='New column']", 50);
            List<string> allValues = await ceApp.Entity.GetValues(multiSelectOptionSet, true, "//label[text()='New column']", 50);
            foreach (string value in allValues) {
                Console.WriteLine(value);
            }
        }

    }
}
