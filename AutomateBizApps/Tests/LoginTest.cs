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
            await ceApp.LoginModule.Login(email, password);
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("My Work", "Sales accelerator");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            // await ceApp.CommandBar.ClickCommand("New");
            await page.Locator("//span[text()='Up next']").HoverAsync();
            ILocator fakeElement = page.Locator("//div[text()='Map is disabled for this organization.']");
            await ceApp.Entity.ScrollUsingMouseUntilElementIsVisible(fakeElement, 0, 100, 20);
            //LookupItem lookupItem = new LookupItem { Name = "Primary Contact", Value = "Alex" };
            //await ceApp.Entity.SetValue(lookupItem);
            //await ceApp.Entity.SetValue("Account Name", "NasruddinAccount");
            //OptionSet customColumn = new OptionSet { Name = "CustomColumn", Value = "Yes" };
            //await ceApp.Entity.SetValue(customColumn);
            //OptionSet customColumn1 = new OptionSet { Name = "CustomColumn", Value = "No" };
            //await ceApp.Entity.SetValue(customColumn1);
            //OptionSet optionSetColumn = new OptionSet { Name = "OptionSetColumn", Value = "India" };
            //await ceApp.Entity.SetValue(optionSetColumn);
            //OptionSet optionSetColumn1 = new OptionSet { Name = "OptionSetColumn", Value = "China" };
            //await ceApp.Entity.SetValue(optionSetColumn1);
        }

    }
}
