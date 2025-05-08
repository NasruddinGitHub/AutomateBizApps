using AutomateCe.Constants;
using AutomateCe.Modules;
using AutomateCe.Controls;
using AutomateCe.Utils;
using Microsoft.Playwright;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Tests
{
    [TestFixture]
    public class SubgridTest : BaseTest
    {
        string? email = TestContext.Parameters[Property.Email];
        string? password = TestContext.Parameters[Property.Password];
        string? mfaKey = TestContext.Parameters[Property.MfaKey];

        [Property("browser", "chrome")]
        [Test]
        public async Task ValidateSubgridTest1()
        {
            ReportUtil.CreateTest("Validate subgrid functionalities 1");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            ReportUtil.Info("Selected Sales hub application");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.Grid.OpenRecordAsync("Jimasa Glynn (sample)");
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShownAsync("opportunities"));
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShownAsync("cases"));
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShownAsync("entitlement"));
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShownAsync("asdsd"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShownAsync("opportunities"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShownAsync("cases"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShownAsync("entitlement"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShownAsync("accounts"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShownAsync("opportunities"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShownAsync("cases"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShownAsync("entitlement"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShownAsync("accounts"));
            Console.WriteLine(await ceApp.Subgrid.GetSelectedViewNameAsync("accounts"));
            await ceApp.Subgrid.SelectViewAsync("accounts", "All Accounts");

            List<string> allViews = await ceApp.Subgrid.GetAllViewsAsync("accounts");
            allViews.ForEach(x => Console.WriteLine(x));
            await ceApp.Subgrid.SearchAsync("accounts", "Adventure");
            Console.WriteLine(await ceApp.Subgrid.GetDisplayedRowsCountAsync("cases"));

            await ceApp.SubgridCommandBar.ClickCommandAsync("accounts", "New Account");

        }

        [Property("browser", "chrome")]
        [Test]
        public async Task ValidateSubgridTest2()
        {
            ReportUtil.CreateTest("Validate subgrid functionalities 2");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            ReportUtil.Info("Selected Sales hub application");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.Grid.OpenRecordAsync("Jimasa Glynn (sample)");

            await ceApp.Subgrid.IsColumnFilteredAsync("cases", "Case Title");
            await ceApp.Subgrid.IsColumnSortedAscendingOrderAsync("cases", "Priority");
            await ceApp.Subgrid.IsColumnSortedDescendingOrderAsync("cases", "Origin");
            await ceApp.Subgrid.SortColumnAsync("cases", "Priority", "Z to A");
            await ceApp.Subgrid.FilterByAsync("opportunities", "Status", "Contains", "Open");
            await ceApp.Subgrid.ClearColumnFilterAsync("opportunities", "Status");
            await ceApp.Subgrid.SelectAllRecordsAsync("cases");
            await ceApp.Subgrid.SelectRecordAsync("opportunities", 0);
        }

        [Property("browser", "chrome")]
        [Test]
        public async Task ValidateSubgridTest3()
        {
            ReportUtil.CreateTest("Validate subgrid functionalities 3");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Sales Hub");
            ReportUtil.Info("Selected Sales hub application");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTabAsync("Copilot");
            await ceApp.Grid.OpenRecordAsync("Jimasa Glynn (sample)");

            await ceApp.Subgrid.OpenRecordAsync("opportunities", 0, "Will be ordering about 110 items of all types (sample)");
            await ceApp.Subgrid.OpenRecordAsync("cases", "Product question (sample)");
            List<string> displayedColumns = await ceApp.Subgrid.GetAllDisplayedColumnNamesAsync("entitlement");
            displayedColumns.ForEach(x => Console.WriteLine(x));
            Console.WriteLine(await ceApp.Subgrid.GetNumberOfDisplayedColumnsAsync("cases"));
            await ceApp.Subgrid.MoveColumnAsync("opportunities", "Account", "Move left");
        }

    }
}
