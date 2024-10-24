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
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales trial");
            await ceApp.SiteMapPanel.OpenSubArea("Learn", "All Fields");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");
            await ceApp.Entity.SetValue("New column", "Setting the value");
            await ceApp.Entity.SetValue("Plain Text", "Setting the value");
            await ceApp.Entity.SetValue("Text Area", "Setting the value");
            // await ceApp.Entity.SetValue("Rich Text", "Setting the value"); // This needs to be handled
            await ceApp.Entity.SetValue("Phone Number", "23432432432434");
            await ceApp.Entity.SetValue("Ticker Symbol", "13212");
            await ceApp.Entity.SetValue("URL", "https://www.google.com/");
            await ceApp.Entity.SetValue("Multi Line Plain Text", "Multi Line Plain Text");
            // await ceApp.Entity.SetValue("Multi Line Rich Text", "Multi Line Rich Text");
            await ceApp.Entity.SetValue("Decimal", "132.232");
            await ceApp.Entity.SetValue("Float", "132.232213213");
            await ceApp.Entity.SetValue("Whole Number", "2132321");
            LookupItem lookupItem = new LookupItem { Name = "Lookup", Value = "Account" };
            await ceApp.Entity.SetValue(lookupItem);
            LookupItem lookupItem1 = new LookupItem { Name = "Customer", Value = "Account", Index = 1 };
            await ceApp.Entity.SetValue(lookupItem1);

            OptionSet optionSet = new OptionSet { Name = "SingleSelectChoice", Value = "India" };
            await ceApp.Entity.SetValue(optionSet);


        }

    }
}
