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
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.CommandBar.ClickCommandAsync("New");

            await ceApp.Entity.SetValueByLabelNameAsync("First Name", "Nasruddin " + DateUtil.GetTimeStamp("yyyyMMddHHmmss"));
            await ceApp.Entity.SetValueByLabelNameAsync("Last Name", "Shaik " + DateUtil.GetTimeStamp("yyyyMMddHHmmss"));
            await ceApp.Entity.SetValueByLabelNameAsync("Job Title", "Consultant");

            LookupItem lookupItem = new LookupItem { Name = "Account Name", Value = "Alpine Ski", Index = 0 };
            await ceApp.Entity.SetValueByLabelNameAsync(lookupItem);

            await ceApp.Entity.SetValueByLabelNameAsync("Email", "test@test.co.in");
            await ceApp.Entity.SetValueByLabelNameAsync("Business Phone", ""+RandomUtils.GetRandomNumberString(10));
            await ceApp.Entity.SetValueByLabelNameAsync("Fax", "" + RandomUtils.GetRandomNumberString(10));
            await ceApp.Entity.SetValueByLabelNameAsync("Mobile Phone", "" + RandomUtils.GetRandomNumberString(10));

            OptionSet optionSet = new OptionSet { Name = "Primary Time Zone", Value = "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi" };
            await ceApp.Entity.SetValueByLabelNameAsync(optionSet);
            
            OptionSet preferredMethodOfContact = new OptionSet { Name = "Preferred Method of Contact", Value = "Email" };
            await ceApp.Entity.SetValueByLabelNameAsync(preferredMethodOfContact);

            await ceApp.CommandBar.ClickCommandAsync("Save");

        }

        [Test]
        public async Task SelectTabTest()
        {
            ReportUtil.CreateTest("Create Contact");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Accounts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.Grid.OpenRecordAsync("A. Datum Csorporation (sample)");

            await ceApp.Entity.SelectTabAsync("Details");
            await ceApp.Entity.SelectTabAsync("Servicing");
            await ceApp.Entity.SelectTabAsync("Summary");

            await ceApp.Entity.SelectTabAsync("Audit History");

            List<string> shownTabs = await ceApp.Entity.GetAllShownTabsAsync();
            foreach (string tab in shownTabs)
            {
                Console.WriteLine($"The shown tab is: {tab}");
            }

            List<string> relatedTabs = await ceApp.Entity.GetAllRelatedTabsAsync();
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
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.Grid.OpenRecordAsync("Jim Glynn (sample)");

            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRecommendedByLabelNameAsync("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRequiredByLabelNameAsync("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldOptionalByLabelNameAsync("First Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldLockedByLabelNameAsync("First Name"));

            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRecommendedByLabelNameAsync("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldBusinessRequiredByLabelNameAsync("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldOptionalByLabelNameAsync("Last Name"));
            Console.WriteLine(await ceApp.Entity.IsFieldLockedByLabelNameAsync("Last Name"));
        }

        [Test]
        public async Task ValidateFormDetailsTest()
        {
            ReportUtil.CreateTest("Form Details Test");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.Grid.OpenRecordAsync("Jimasa Glynn (sample)");

            Console.WriteLine(await ceApp.Entity.GetFormHeaderTitleAsync());
            Console.WriteLine(await ceApp.Entity.GetEntityNameAsync());
            Console.WriteLine(await ceApp.Entity.IsFormSelectorShownAsync());
            Console.WriteLine(await ceApp.Entity.GetSelectedFormNameAsync());
            Console.WriteLine(await ceApp.Entity.IsFormSavedAsync());
            await ceApp.Entity.SelectFormAsync("AI for Sales");
            List<string> allFormNames = await ceApp.Entity.GetAllFormNamesAsync();

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
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Sales", "Leads");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.Grid.OpenRecordAsync("Peter Houston (sample)");

            Console.WriteLine(await ceApp.Entity.GetHeaderControlValueAsync("Lead Source"));
            Console.WriteLine(await ceApp.Entity.GetHeaderControlValueAsync("Rating"));
            Console.WriteLine(await ceApp.Entity.GetHeaderControlValueAsync("Status"));
            Console.WriteLine(await ceApp.Entity.GetHeaderControlValueAsync("Owner"));
        }

        [Test]
        public async Task ValidateSetHeaderValues1()
        {
            ReportUtil.CreateTest("Validate setting header values1");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Accounts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.CommandBar.ClickCommandAsync("New");

            await ceApp.Entity.SetHeaderValueByLabelNameAsync("Annual Revenue", "10000", expandHeaderFlyout:true);
            await ceApp.Entity.SetHeaderValueByLabelNameAsync("Number of Employees", "100", closeHeaderFlyout: true);
        }

        [Test]
        public async Task ValidateSetHeaderValues2()
        {
            ReportUtil.CreateTest("Validate setting header values1");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Sales", "Leads");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.CommandBar.ClickCommandAsync("New");

            OptionSet leadSource = new OptionSet { Name = "Lead Source", Value = "Partner"};
            OptionSet rating = new OptionSet { Name = "Rating", Value = "Cold" };
            OptionSet status = new OptionSet { Name = "Status", Value = "Contacted" };

            await ceApp.Entity.SetHeaderValueByLabelNameAsync(leadSource, expandHeaderFlyout:true);
            await ceApp.Entity.SetHeaderValueByLabelNameAsync(rating);
            await ceApp.Entity.SetHeaderValueByLabelNameAsync(status, closeHeaderFlyout: true);
        }

        [Test]
        public async Task ValidateHeaderFieldRequirementsTest()
        {
            ReportUtil.CreateTest("Validate header field requirement test");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Sales", "Leads");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.CommandBar.ClickCommandAsync("New");

            await ceApp.Entity.IsHeaderFieldBusinessRecommendedByLabelNameAsync("Lead Source", expandHeaderFlyout:true);
            await ceApp.Entity.IsHeaderFieldBusinessRequiredByLabelNameAsync("Lead Source");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelNameAsync("Lead Source");
            await ceApp.Entity.IsHeaderFieldBusinessRecommendedByLabelNameAsync("Rating");
            await ceApp.Entity.IsHeaderFieldBusinessRequiredByLabelNameAsync("Rating");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelNameAsync("Rating");
            await ceApp.Entity.IsHeaderFieldBusinessRecommendedByLabelNameAsync("Status");
            await ceApp.Entity.IsHeaderFieldBusinessRequiredByLabelNameAsync("Status");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelNameAsync("Status");
            await ceApp.Entity.IsHeaderFieldLockedByLabelNameAsync("Status");
            await ceApp.Entity.IsHeaderFieldOptionalByLabelNameAsync("Status");
        }

        [Test]
        public async Task ValidateEntityInTestMode()
        {
            ReportUtil.CreateTest("Validate entity in the test mode.");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.CommandBar.ClickCommandAsync("New");

            await ceApp.Entity.SetValueByLabelNameAsync("First Name", "Nasruddin");
            await ceApp.Entity.SetValueByLabelNameAsync("Last Name", "Shaik");
            await ceApp.Entity.SetValueByLabelNameAsync("Address 1: Country/Region", "India");
        }

        // This test will not work if the test mode is false
        [Test]
        public async Task ValidateDynamicallyLoadedElementInNonTestMode()
        {
            ReportUtil.CreateTest("Validate entity in the test mode.");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);

            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.CommandBar.ClickCommandAsync("New");

            await ceApp.Entity.SetValueByLabelNameAsync("First Name", "Nasruddin");
            await ceApp.Entity.SetValueByLabelNameAsync("Last Name", "Shaik");

            // Address 1: Country/Region is dynamically loaded element, so test mode should be on for this. 
            // If we call SetValue like this, it will fail . await ceApp.Entity.SetValue("Address 1: Country/Region", "India");
            // We should call like below
            await ceApp.Entity.SetValueByLabelNameAsync("Address 1: Country/Region", "India", true, anyFieldNameInScroller: "First Name", maxNumberOfScrolls: 100);
        }

    }
}
