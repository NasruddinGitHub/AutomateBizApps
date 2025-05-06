using AutomateCe.Constants;
using AutomateCe.Modules;
using AutomateCe.Tests;
using AutomateCe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Tests
{
    public class CommandBarTest : BaseTest
    {

        string? email = TestContext.Parameters[Property.Email];
        string? password = TestContext.Parameters[Property.Password];
        string? mfaKey = TestContext.Parameters[Property.MfaKey];

        [Property("browser", "chrome")]
        [Test]
        public async Task ValidateIfUserIsAbleToSelectAnApp()
        {
            ReportUtil.CreateTest("Validate is the user is able to select a MDA app from the landing page");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.CommandBar.ClickCommand("Refresh");
            List<string> allShownCommands = await ceApp.CommandBar.GetAllShownCommands();
            foreach (string command in allShownCommands)
            {
                Console.WriteLine("The shown command is: "+command);
            }

            List<string> allMoreCommands = await ceApp.CommandBar.GetAllMoreCommands();
            foreach (string command in allMoreCommands)
            {
                Console.WriteLine("The more command is: " + command);
            }

        }

    }
}
