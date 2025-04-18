﻿using AutomateBizApps.Constants;
using AutomateBizApps.Modules;
using AutomateBizApps.Pages;
using AutomateCe.Controls;
using AutomateCe.Modules;
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
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateBizApps.Tests
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
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            ReportUtil.Info("Selected Sales hub application");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord("Jimasa Glynn (sample)");
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShown("opportunities"));
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShown("cases"));
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShown("entitlement"));
            Console.WriteLine(await ceApp.Subgrid.IsSubgridShown("asdsd"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShown("opportunities"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShown("cases"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShown("entitlement"));
            Console.WriteLine(await ceApp.Subgrid.IsSearchboxShown("accounts"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShown("opportunities"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShown("cases"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShown("entitlement"));
            Console.WriteLine(await ceApp.Subgrid.IsViewSelectorShown("accounts"));
            Console.WriteLine(await ceApp.Subgrid.GetSelectedViewName("accounts"));
            await ceApp.Subgrid.SelectView("accounts", "All Accounts");

            List<string> allViews = await ceApp.Subgrid.GetAllViews("accounts");
            allViews.ForEach(x => Console.WriteLine(x));
            await ceApp.Subgrid.Search("accounts", "Adventure");
            Console.WriteLine(await ceApp.Subgrid.GetDisplayedRowsCount("cases"));

            await ceApp.SubgridCommandBar.ClickCommand("accounts", "New Account");

        }

        [Property("browser", "chrome")]
        [Test]
        public async Task ValidateSubgridTest2()
        {
            ReportUtil.CreateTest("Validate subgrid functionalities 2");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            ReportUtil.Info("Selected Sales hub application");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord("Jimasa Glynn (sample)");

            await ceApp.Subgrid.IsColumnFiltered("cases", "Case Title");
            await ceApp.Subgrid.IsColumnSortedAscendingOrder("cases", "Priority");
            await ceApp.Subgrid.IsColumnSortedDescendingOrder("cases", "Origin");
            await ceApp.Subgrid.SortColumn("cases", "Priority", "Z to A");
            await ceApp.Subgrid.FilterBy("opportunities", "Status", "Contains", "Open");
            await ceApp.Subgrid.ClearColumnFilter("opportunities", "Status");
            await ceApp.Subgrid.SelectAllRecords("cases");
            await ceApp.Subgrid.SelectRecord("opportunities", 0);
        }

        [Property("browser", "chrome")]
        [Test]
        public async Task ValidateSubgridTest3()
        {
            ReportUtil.CreateTest("Validate subgrid functionalities 3");
            ReportUtil.AssignAuthor("Nasruddin Shaik");
            ReportUtil.AssignCategory("Smoke");

            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Sales Hub");
            ReportUtil.Info("Selected Sales hub application");
            await ceApp.SiteMapPanel.OpenSubArea("Customers", "Contacts");
            await ceApp.Complementary.OpenOrCloseTab("Copilot");
            await ceApp.Grid.OpenRecord("Jimasa Glynn (sample)");

            await ceApp.Subgrid.OpenRecord("opportunities", 0, "Will be ordering about 110 items of all types (sample)");
            await ceApp.Subgrid.OpenRecord("cases", "Product question (sample)");
            List<string> displayedColumns = await ceApp.Subgrid.GetAllDisplayedColumnNames("entitlement");
            displayedColumns.ForEach(x => Console.WriteLine(x));
            Console.WriteLine(await ceApp.Subgrid.GetNumberOfDisplayedColumns("cases"));
            await ceApp.Subgrid.MoveColumn("opportunities", "Account", "Move left");
        }

    }
}
