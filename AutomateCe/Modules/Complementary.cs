using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class Complementary : SharedPage
    {

        private IPage _page;

        public Complementary(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task OpenOrCloseTabAsync(string appName)
        {
            var tabLocator = Locator(ComplementaryPaneLocators.Tab.Replace("[Name]", appName));
            await ClickAsync(tabLocator);
        }

        public async Task OpenTabIfNotExpandedAsync(string appName)
        {
            var tabLocator = Locator(ComplementaryPaneLocators.Tab.Replace("[Name]", appName));
            bool isApplicationOpened = Convert.ToBoolean(await GetAttributeAsync(tabLocator, "aria-expanded"));
            if (!isApplicationOpened)
            {
                await ClickAsync(tabLocator);
            }
        }

        public async Task CloseTabIfNotExpandedAsync(string appName)
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
