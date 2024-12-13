using AutomateBizApps.Constants;
using AutomateBizApps.Modules;
using AutomateBizApps.Tests;
using AutomateCe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Tests
{
    [TestFixture]
    public class BusinessProcessFlowTest : BaseTest
    {
        string? email = TestContext.Parameters[Property.Email];
        string? password = TestContext.Parameters[Property.Password];
        string? mfaKey = TestContext.Parameters[Property.MfaKey];

        [Test]
        public async Task ValidateIfUserIsAbleToSetTheDataInTextField()
        {
            ReportUtil.CreateTest("Validate if the user is able to set the data in the text field");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Regression");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Field Service");
            ReportUtil.Info("Selected Field Service application");
            //await ceApp.SiteMapPanel.OpenSubArea("Customers", "Accounts");
        }
    }
}
