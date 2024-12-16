using AutomateBizApps.Constants;
using AutomateBizApps.Modules;
using AutomateBizApps.Tests;
using AutomateBizApps.Utils;
using AutomateCe.Controls;
using AutomateCe.Utils;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Tests
{
    public class EntityTest : BaseTest
    {
        string? email = TestContext.Parameters[Property.Email];
        string? password = TestContext.Parameters[Property.Password];
        string? mfaKey = TestContext.Parameters[Property.MfaKey];

        [Property("browser", "chrome")]
        [Test]
        public async Task CreateContactTest()
        {
            ReportUtil.CreateTest("Create Contact");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");

            await ceApp.Entity.SetValue("First Name", "Nasruddin " + DateUtil.GetTimeStamp("yyyyMMddHHmmss"));
            await ceApp.Entity.SetValue("Last Name", "Shaik " + DateUtil.GetTimeStamp("yyyyMMddHHmmss"));
            await ceApp.Entity.SetValue("Job Title", "Consultant");

            LookupItem lookupItem = new LookupItem { Name = "Account Name", Value = "Alpine Ski", Index = 0 };
            await ceApp.Entity.SetValue(lookupItem);

            await ceApp.Entity.SetValue("Email", "test@test.co.in");
            await ceApp.Entity.SetValue("Business Phone", ""+RandomUtils.GetRandomNumberString(10));
            await ceApp.Entity.SetValue("Fax", "" + RandomUtils.GetRandomNumberString(10));
            await ceApp.Entity.SetValue("Mobile Phone", "" + RandomUtils.GetRandomNumberString(10));

            OptionSet optionSet = new OptionSet { Name = "Primary Time Zone", Value = "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi" };
            await ceApp.Entity.SetValue(optionSet);
            
            OptionSet preferredMethodOfContact = new OptionSet { Name = "Preferred Method of Contact", Value = "Email" };
            await ceApp.Entity.SetValue(preferredMethodOfContact);

            await ceApp.CommandBar.ClickCommand("Save");

        }

        [Test]
        public async Task SelectTabTest()
        {
            ReportUtil.CreateTest("Create Contact");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Accounts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord("A. Datum Csorporation (sample)");

            await ceApp.Entity.SelectTab("Details");
            await ceApp.Entity.SelectTab("Servicing");
            await ceApp.Entity.SelectTab("Summary");

            await ceApp.Entity.SelectTab("Audit History");

            List<string> shownTabs = await ceApp.Entity.GetAllShownTabs();
            foreach (string tab in shownTabs)
            {
                Console.WriteLine($"The shown tab is: {tab}");
            }

            List<string> relatedTabs = await ceApp.Entity.GetAllRelatedTabs();
            foreach (string tab in relatedTabs)
            {
                Console.WriteLine($"The related tab is: {tab}");
            }

        }

        [Test]
        public async Task ValidateFieldRequirementsTest()
        {
            ReportUtil.CreateTest("Field Requirement Test");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord("Jim Glynn (sample)");

            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRecommended("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRequired("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldOptional("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldLocked("First Name"));

            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRecommended("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRequired("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldOptional("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldLocked("Last Name"));
        }

        [Test]
        public async Task ValidateFormDetailsTest()
        {
            ReportUtil.CreateTest("Form Details Test");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord("Jimasa Glynn (sample)");

            Console.WriteLine(await ceApp.Entity.GetFormHeaderTitle());
            Console.WriteLine(await ceApp.Entity.GetEntityName());
            Console.WriteLine(await ceApp.Entity.IsFormSelectorShown());
            Console.WriteLine(await ceApp.Entity.GetSelectedFormName());
            Console.WriteLine(await ceApp.Entity.IsFormSaved());
            await ceApp.Entity.SelectForm("AI for Sales");
            List<string> allFormNames = await ceApp.Entity.GetAllFormNames();

            foreach (string formName in allFormNames)
            {
                Console.WriteLine($"{formName}");
            }
        }

        [Test]
        public async Task ValidateHeaderControlValues()
        {
            ReportUtil.CreateTest("Validate header control values");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Sales", "Leads");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord("Peter Houston (sample)");

            Console.WriteLine(await ceApp.Entity.GetHeaderControlValue("Lead Source"));
            Console.WriteLine(await ceApp.Entity.GetHeaderControlValue("Rating"));
            Console.WriteLine(await ceApp.Entity.GetHeaderControlValue("Status"));
            Console.WriteLine(await ceApp.Entity.GetHeaderControlValue("Owner"));
        }

        [Test]
        public async Task ValidateSetHeaderValues1()
        {
            ReportUtil.CreateTest("Validate setting header values1");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Accounts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");

            await ceApp.Entity.SetHeaderValue("Annual Revenue", "10000", expandHeaderFlyout:true);
            await ceApp.Entity.SetHeaderValue("Number of Employees", "100", closeHeaderFlyout: true);
        }

        [Test]
        public async Task ValidateSetHeaderValues2()
        {
            ReportUtil.CreateTest("Validate setting header values1");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Sales", "Leads");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");

            OptionSet leadSource = new OptionSet { Name = "Lead Source", Value = "Partner"};
            OptionSet rating = new OptionSet { Name = "Rating", Value = "Cold" };
            OptionSet status = new OptionSet { Name = "Status", Value = "Contacted" };

            await ceApp.Entity.SetHeaderValue(leadSource, expandHeaderFlyout:true);
            await ceApp.Entity.SetHeaderValue(rating);
            await ceApp.Entity.SetHeaderValue(status, closeHeaderFlyout: true);
        }

        [Test]
        public async Task ValidateHeaderFieldRequirementsTest()
        {
            ReportUtil.CreateTest("Validate header field requirement test");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Sales", "Leads");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");

            await ceApp.Entity.IsHeaderFieldBusinessRecommended("Lead Source", expandHeaderFlyout:true);
            await ceApp.Entity.IsHeaderFieldBusinessRequired("Lead Source");
            await ceApp.Entity.IsHeaderFieldOptional("Lead Source");
            await ceApp.Entity.IsHeaderFieldBusinessRecommended("Rating");
            await ceApp.Entity.IsHeaderFieldBusinessRequired("Rating");
            await ceApp.Entity.IsHeaderFieldOptional("Rating");
            await ceApp.Entity.IsHeaderFieldBusinessRecommended("Status");
            await ceApp.Entity.IsHeaderFieldBusinessRequired("Status");
            await ceApp.Entity.IsHeaderFieldOptional("Status");
            await ceApp.Entity.IsHeaderFieldLocked("Status");
            await ceApp.Entity.IsHeaderFieldOptional("Status");
        }


    }
}
