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
    public class Complementary : SharedPage
    {

        private IPage _page;

        public Complementary(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task OpenOrCloseTab(string appName)
        {
            var tabLocator = Locator(ComplementaryPaneLocators.Tab.Replace("[Name]", appName));
            await ClickAsync(tabLocator);
        }

        public async Task OpenTabIfNotExpanded(string appName)
        {
            var tabLocator = Locator(ComplementaryPaneLocators.Tab.Replace("[Name]", appName));
            bool isApplicationOpened = Convert.ToBoolean(await GetAttributeAsync(tabLocator, "aria-expanded"));
            if (!isApplicationOpened)
            {
                await ClickAsync(tabLocator);
            }
        }

        public async Task CloseTabIfNotExpanded(string appName)
        {
            var tabLocator = Locator(ComplementaryPaneLocators.Tab.Replace("[Name]", appName));
            bool isApplicationOpened = Convert.ToBoolean(await GetAttributeAsync(tabLocator, "aria-expanded"));
            if (isApplicationOpened)
            {
                await ClickAsync(tabLocator);
            }
        }

    }
}
