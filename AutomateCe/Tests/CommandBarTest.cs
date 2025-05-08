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
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.CommandBar.ClickCommandAsync("Refresh");
            List<string> allShownCommands = await ceApp.CommandBar.GetAllShownCommandsAsync();
            foreach (string command in allShownCommands)
            {
                Console.WriteLine("The shown command is: "+command);
            }

            List<string> allMoreCommands = await ceApp.CommandBar.GetAllMoreCommandsAsync();
            foreach (string command in allMoreCommands)
            {
                Console.WriteLine("The more command is: " + command);
            }

        }

    }
}
