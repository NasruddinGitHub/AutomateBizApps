using AutomateBizApps.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public async Task SelectTab(string tabName, int timeToCheckIfFrameExists = 1000, string subTab = null)
        {
            var tabLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.Tab.Replace("[Name]", tabName), timeToCheckIfFrameExists);
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

        public async Task ClickRelatedTab(int timeToCheckIfFrameExists = 1000)
        {
            var relatedTabLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.RelatedTab, timeToCheckIfFrameExists);
            await ClickAsync(relatedTabLocator);
        }

        public async Task ClickRelatedTabs(string relatedTabName, int timeToCheckIfFrameExists = 1000)
        {
            var relatedTabsLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.RelatedTabs.Replace("[Name]", relatedTabName), timeToCheckIfFrameExists);
            await ClickAsync(relatedTabsLocator);
        }

        public async Task<List<string>> GetAllShownTabs(int timeToCheckIfFrameExists = 1000)
        {
            var allShownTabsLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.AllShownTabs, timeToCheckIfFrameExists);
            List<string> allShownTabs = await GetAllElementsTextAfterWaiting(allShownTabsLocator);
            List<string> allShownTabsAfterTrimming = new List<string>();
            foreach (string shownTab in allShownTabs)
            {
                allShownTabsAfterTrimming.Add(shownTab.Substring(0, (shownTab.Length / 2)));
            }
            return allShownTabsAfterTrimming;
        }

        public async Task<List<string>> GetAllRelatedTabs(int timeToCheckIfFrameExists = 1000)
        {
            var relatedTabLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.RelatedTab, timeToCheckIfFrameExists);
            await ClickAsync(relatedTabLocator);
            var allRelatedTabsLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.AllRelatedTabs, timeToCheckIfFrameExists);
            return await GetAllElementsTextAfterWaiting(allRelatedTabsLocator);
        }
    }
}
