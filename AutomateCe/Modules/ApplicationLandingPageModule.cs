using AutomateCe.Modules;
using AutomateCe.Utils;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class ApplicationLandingPageModule : SharedPage
    {
        private IPage _page;

        public ApplicationLandingPageModule(IPage page) : base(page)
        {
            this._page = page;
        }
        
        public async Task OpenAppAsync(string applicationName)
        {
           string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
           string applicationLocator = ApplicationLandingPageLocators.Application.Replace("[Name]", applicationName);
           var appLandingPageFrame = SwitchToFrameAndLocate(appLandingFrameLocator, applicationLocator);
           await ClickAsync(appLandingPageFrame, new LocatorClickOptions { Timeout = 50000}, new LocatorEvaluateOptions { Timeout = 50000 });
           bool isTestModeRequired =  bool.Parse(TestContextUtil.GetParameter("EnableTestMode"));
            if (isTestModeRequired)
            {
                await EnableTestModeAsync();
            }
        }

        public async Task LogoutAsync()
        {
            var accountManagerElement = Locator(ApplicationLandingPageLocators.AccountManager);
            await ClickAsync(accountManagerElement);
            var accountManagerSignOutElement = Locator(ApplicationLandingPageLocators.AccountManagerSignOut);
            await ClickAsync(accountManagerSignOutElement);
        }

        public async Task SearchAsync(string appToSearch)
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string searchAppLocator = ApplicationLandingPageLocators.SearchApp;
            var searchAppLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, searchAppLocator);
            await FillAsync(searchAppLocatorInFrame, appToSearch);
        }

        public async Task RefreshAsync()
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string refreshLocator = ApplicationLandingPageLocators.Refresh;
            var refreshLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, refreshLocator);
            await ClickAsync(refreshLocatorInFrame);
        }

        public async Task<int> GetNumberOfPublishedAppsAsync()
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string appTitlesLocator = ApplicationLandingPageLocators.AppTitles;
            var numberOfPublishedAppsLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, appTitlesLocator);
            string numberOfPublishedApps = await TextContentAsync(numberOfPublishedAppsLocatorInFrame);
            numberOfPublishedApps = numberOfPublishedApps.Replace("(", "").Replace(")", "");
            return int.Parse(numberOfPublishedApps);
        }

        public async Task<List<string>> GetAllAvailableAppNamesAsync()
        {
            string appLandingFrameLocator = ApplicationLandingPageLocators.ApplandingPageFrame;
            string appTitlesLocator = ApplicationLandingPageLocators.AppTitles;
            var appTitlesLocatorInFrame = SwitchToFrameAndLocate(appLandingFrameLocator, appTitlesLocator);
            return await GetAllElementsTextAfterWaitingAsync(appTitlesLocatorInFrame);
        }

        public async Task<bool> IsMdaAppAvailableAsync(string appName)
        {
            List<string> allAvailableApps = await GetAllAvailableAppNamesAsync();
            return allAvailableApps.Contains(appName);
        }
    }
}
