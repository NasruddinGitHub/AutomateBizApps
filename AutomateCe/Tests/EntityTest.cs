using AutomateCe.Constants;
using AutomateCe.Controls;
using AutomateCe.Modules;
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

            await ceApp.Entity.SetValueByLabelName("First Name", "Nasruddin " + DateUtil.GetTimeStamp("yyyyMMddHHmmss"));
            await ceApp.Entity.SetValueByLabelName("Last Name", "Shaik " + DateUtil.GetTimeStamp("yyyyMMddHHmmss"));
            await ceApp.Entity.SetValueByLabelName("Job Title", "Consultant");

            LookupItem lookupItem = new LookupItem { Name = "Account Name", Value = "Alpine Ski", Index = 0 };
            await ceApp.Entity.SetValueByLabelName(lookupItem);

            await ceApp.Entity.SetValueByLabelName("Email", "test@test.co.in");
            await ceApp.Entity.SetValueByLabelName("Business Phone", ""+RandomUtils.GetRandomNumberString(10));
            await ceApp.Entity.SetValueByLabelName("Fax", "" + RandomUtils.GetRandomNumberString(10));
            await ceApp.Entity.SetValueByLabelName("Mobile Phone", "" + RandomUtils.GetRandomNumberString(10));

            OptionSet optionSet = new OptionSet { Name = "Primary Time Zone", Value = "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi" };
            await ceApp.Entity.SetValueByLabelName(optionSet);
            
            OptionSet preferredMethodOfContact = new OptionSet { Name = "Preferred Method of Contact", Value = "Email" };
            await ceApp.Entity.SetValueByLabelName(preferredMethodOfContact);

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

            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRecommendedByLabelName("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRequiredByLabelName("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldOptionalByLabelName("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldLockedByLabelName("First Name"));

            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRecommendedByLabelName("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRequiredByLabelName("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldOptionalByLabelName("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldLockedByLabelName("Last Name"));
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

            await ceApp.Entity.SetHeaderValueByLabelName("Annual Revenue", "10000", expandHeaderFlyout:true);
            await ceApp.Entity.SetHeaderValueByLabelName("Number of Employees", "100", closeHeaderFlyout: true);
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

            await ceApp.Entity.SetHeaderValueByLabelName(leadSource, expandHeaderFlyout:true);
            await ceApp.Entity.SetHeaderValueByLabelName(rating);
            await ceApp.Entity.SetHeaderValueByLabelName(status, closeHeaderFlyout: true);
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

            await ceApp.Entity.IsHeaderFieldBusinessRecommendedByLabelName("Lead Source", expandHeaderFlyout:true);
            await ceApp.Entity.IsHeaderFieldBusinessRequiredByLabelName("Lead Source");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelName("Lead Source");
            await ceApp.Entity.IsHeaderFieldBusinessRecommendedByLabelName("Rating");
            await ceApp.Entity.IsHeaderFieldBusinessRequiredByLabelName("Rating");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelName("Rating");
            await ceApp.Entity.IsHeaderFieldBusinessRecommendedByLabelName("Status");
            await ceApp.Entity.IsHeaderFieldBusinessRequiredByLabelName("Status");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelName("Status");
            await ceApp.Entity.IsHeaderFieldLockedByLabelName("Status");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelName("Status");
        }

        [Test]
        public async Task ValidateEntityInTestMode()
        {
            ReportUtil.CreateTest("Validate entity in the test mode.");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");

            await ceApp.Entity.SetValueByLabelName("First Name", "Nasruddin");
            await ceApp.Entity.SetValueByLabelName("Last Name", "Shaik");
            await ceApp.Entity.SetValueByLabelName("Address 1: Country/Region", "India");
        }

        // This test will not work if the test mode is false
        [Test]
        public async Task ValidateDynamicallyLoadedElementInNonTestMode()
        {
            ReportUtil.CreateTest("Validate entity in the test mode.");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.CommandBar.ClickCommand("New");

            await ceApp.Entity.SetValueByLabelName("First Name", "Nasruddin");
            await ceApp.Entity.SetValueByLabelName("Last Name", "Shaik");

            // Address 1: Country/Region is dynamically loaded element, so test mode should be on for this. 
            // If we call SetValue like this, it will fail . await ceApp.Entity.SetValue("Address 1: Country/Region", "India");
            // We should call like below
            await ceApp.Entity.SetValueByLabelName("Address 1: Country/Region", "India", true, anyFieldNameInScroller: "First Name", maxNumberOfScrolls: 100);
        }

    }
}
