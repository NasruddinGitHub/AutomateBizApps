using AutomateBizApps.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class Entity : SharedPage
    {
        private IPage _page;

        public Entity(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task SelectTab(string tabName, string subTab = null)
        {
            var tabLocator = Locator(EntityLocators.Tab.Replace("[Name]", tabName));
            try
            {
                await ClickAsync(tabLocator, new LocatorClickOptions { Timeout = 7000 });
            }
            catch (TimeoutException ex)
            {
                await ClickRelatedTab();
                await ClickRelatedTabs(tabName);
            }
        }

        public async Task ClickRelatedTab()
        {
            await ClickAsync(Locator(EntityLocators.RelatedTab));
        }

        public async Task ClickRelatedTabs(string relatedTabName)
        {
            await ClickAsync(Locator(EntityLocators.RelatedTabs.Replace("[Name]", relatedTabName)));
        }

        public async Task<List<string>> GetAllShownTabs()
        {
            var allShownTabsLocator = Locator(EntityLocators.AllShownTabs);
            List<string> allShownTabs = await GetAllElementsTextAfterWaiting(allShownTabsLocator);
            List<string> allShownTabsAfterTrimming = new List<string>();
            foreach (string shownTab in allShownTabs)
            {
                allShownTabsAfterTrimming.Add(shownTab.Substring(0, (shownTab.Length / 2)));
            }
            return allShownTabsAfterTrimming;
        }

        public async Task<List<string>> GetAllRelatedTabs()
        {
            await ClickAsync(Locator(EntityLocators.RelatedTab));
            var allRelatedTabsLocator = Locator(EntityLocators.AllRelatedTabs);
            return await GetAllElementsTextAfterWaiting(allRelatedTabsLocator);
        }
    }
}
