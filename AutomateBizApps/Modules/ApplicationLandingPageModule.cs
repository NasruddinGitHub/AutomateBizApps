using AutomateBizApps.Pages;
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
        }

        public async Task Logout()
        {
            var accountManagerElement = Locator(ApplicationLandingPageLocators.AccountManager);
            await ClickAsync(accountManagerElement);
            var accountManagerSignOutElement = Locator(ApplicationLandingPageLocators.AccountManagerSignOut);
            await ClickAsync(accountManagerSignOutElement);
        }
    }
}
