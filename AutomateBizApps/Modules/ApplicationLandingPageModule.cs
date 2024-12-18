using AutomateBizApps.Pages;
using AutomateCe.Utils;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateBizApps.Modules
{
    public class ApplicationLandingPageModule : SharedPage
    {
        private IPage _page;

        public ApplicationLandingPageModule(IPage page) : base(page)
        {
            this._page = page;
        }
        
        public async Task OpenApp(string applicationName)
        {
           string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
           string applicationLocator = ApplicationLandingPageLocators.Application.Replace("[Name]", applicationName);
           var appLandingPageFrame = SwitchToFrameAndLocate(appLandingFrameLocator, applicationLocator);
           await ClickAsync(appLandingPageFrame, new LocatorClickOptions { Timeout = 50000});
           bool isTestModeRequired =  bool.Parse(TestContextUtil.GetParameter("EnableTestMode"));
            if (isTestModeRequired)
            {
                await EnableTestMode();
            }
        }

        public async Task Logout()
        {
            var accountManagerElement = Locator(ApplicationLandingPageLocators.AccountManager);
            await ClickAsync(accountManagerElement);
            var accountManagerSignOutElement = Locator(ApplicationLandingPageLocators.AccountManagerSignOut);
            await ClickAsync(accountManagerSignOutElement);
        }

        public async Task Search(string appToSearch)
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string searchAppLocator = ApplicationLandingPageLocators.SearchApp;
            var searchAppLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, searchAppLocator);
            await FillAsync(searchAppLocatorInFrame, appToSearch);
        }

        public async Task Refresh()
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string refreshLocator = ApplicationLandingPageLocators.Refresh;
            var refreshLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, refreshLocator);
            await ClickAsync(refreshLocatorInFrame);
        }

        public async Task<int> GetNumberOfPublishedApps()
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string appTitlesLocator = ApplicationLandingPageLocators.AppTitles;
            var numberOfPublishedAppsLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, appTitlesLocator);
            string numberOfPublishedApps = await TextContentAsync(numberOfPublishedAppsLocatorInFrame);
            numberOfPublishedApps = numberOfPublishedApps.Replace("(", "").Replace(")", "");
            return int.Parse(numberOfPublishedApps);
        }

        public async Task<List<string>> GetAllAvailableAppNames()
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string appTitlesLocator = ApplicationLandingPageLocators.AppTitles;
            var appTitlesLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, appTitlesLocator);
            return await GetAllElementsTextAfterWaiting(appTitlesLocatorInFrame);
        }

        public async Task<bool> IsMdaAppAvailable(string appName)
        {
            List<string> allAvailableApps = await GetAllAvailableAppNames();
            return allAvailableApps.Contains(appName);
        }
    }
}
