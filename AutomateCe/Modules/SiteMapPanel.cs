using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class SiteMapPanel : SharedPage
    {
        private IPage _page;

        public SiteMapPanel(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task ExpandSiteMap()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            await ClickAsync(siteMapLauncherElement);
        }

        public async Task ShrinkSiteMap()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            await ClickAsync(siteMapLauncherElement);
        }

        public async Task ExpandSiteMapIfShrinked()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            bool isSiteMapExpanded = Convert.ToBoolean(await GetAttributeAsync(siteMapLauncherElement, "aria-expanded"));
            if (!isSiteMapExpanded)
            {
                await ClickAsync(siteMapLauncherElement);
            }
        }

        public async Task ShrinkSiteMapIfExpanded()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            bool isSiteMapExpanded = Convert.ToBoolean(await GetAttributeAsync(siteMapLauncherElement, "aria-expanded"));
            if (isSiteMapExpanded)
            {
                await ClickAsync(siteMapLauncherElement);
            }
        }

        public async Task OpenSubArea(string area, string subarea)
        {
            string subAreaLocator = SiteMapPanelLocators.SubArea.Replace("[Area]", area).Replace("[Subarea]", subarea);
            var subAreaElement = Locator(subAreaLocator);
            await ClickAsync(subAreaElement);
        }

        public async Task<List<string>> GetSubAreas(string area)
        {
            var subAreaTreeItemsSelector = SiteMapPanelLocators.SiteMapEntity.Replace("[Area]", area);
            var subAreaTreeItems = Locator(subAreaTreeItemsSelector);
            var subAreaTreeItemsCount = await CountAsync(subAreaTreeItems);
            return await GetAllElementsText(subAreaTreeItems);
        }

        public async Task ChangeArea(string areaToSelect)
        {
            var areaChangerElement = Locator(SiteMapPanelLocators.AreaChanger);
            await ClickAsync(areaChangerElement);
            var areaChangerItemElement = Locator(SiteMapPanelLocators.AreaChangerItem.Replace("[Name]", areaToSelect));
            await ClickAsync(areaChangerItemElement);
        }

        public async Task<List<string>> GetChangeAreaItems()
        {
            var areaChangerElement = Locator(SiteMapPanelLocators.AreaChanger);
            await ClickAsync(areaChangerElement);
            var areaChangerItemsElement = Locator(SiteMapPanelLocators.AreaChangerItems);
            return await GetAllElementsText(areaChangerItemsElement);
        }

        public async Task<string?> GetCurrentlySelectedArea()
        {
            var areaChangerElement = Locator(SiteMapPanelLocators.AreaChanger);
            return await TextContentAsync(areaChangerElement);
        }

        public async Task ClickSiteMapTab(string tabName)
        {
            var siteMapTabElement = Locator(SiteMapPanelLocators.SiteMapTab.Replace("[Name]", tabName));
            await ClickAsync(siteMapTabElement);
        }

        public async Task ExpandOrCollapseRecentTab()
        {
            await ClickSiteMapTab("Recent");
        }

        public async Task ExpandOrCollapsePinnedTab()
        {
            await ClickSiteMapTab("Pinned");
        }

        public async Task OpenItemFromRecentGroup(string itemName)
        {
            await ExpandOrCollapseRecentTab();
            var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", itemName));
            await ExpandOrCollapseRecentTab();
        }

        public async Task PinRecentItem(string itemName)
        {
            await ExpandOrCollapseRecentTab();
            var recentItemPinLocator = Locator(SiteMapPanelLocators.PinRecentItem.Replace("[Name]", itemName));
            var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", itemName));
            await HoverAsync(recentItemLocator);
            await ClickAsync(recentItemPinLocator);
        }

        public async Task PinRecentItems(List<string> items)
        {
            await ExpandOrCollapseRecentTab();
            foreach (string item in items)
            {
                var recentItemPinLocator = Locator(SiteMapPanelLocators.PinRecentItem.Replace("[Name]", item));
                var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", item));
                await HoverAsync(recentItemLocator);
                await ClickAsync(recentItemPinLocator);
            }
        }

        public async Task UnPinRecentItem(string itemName)
        {
            await ExpandOrCollapseRecentTab();
            var recentItemUnpinLocator = Locator(SiteMapPanelLocators.UnPinRecentItem.Replace("[Name]", itemName));
            var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", itemName));
            await HoverAsync(recentItemLocator);
            await ClickAsync(recentItemUnpinLocator);
        }

        public async Task UnPinRecentItems(List<string> items)
        {
            await ExpandOrCollapseRecentTab();
            foreach (string item in items)
            {
                var recentItemUnpinLocator = Locator(SiteMapPanelLocators.UnPinRecentItem.Replace("[Name]", item));
                var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", item));
                await HoverAsync(recentItemLocator);
                await ClickAsync(recentItemUnpinLocator);
            }
        }

        public async Task<List<string?>> GetAllPinnedItemsFromRecentGroup()
        {
            await ExpandOrCollapseRecentTab();
            var allPinnedRecentItemsLocator = Locator(SiteMapPanelLocators.AllPinnedRecentItem);
            await ToBeVisibleAsync(allPinnedRecentItemsLocator, 0, 30000);
            return await GetAllElementsText(allPinnedRecentItemsLocator);
        }

        public async Task<List<string?>> GetAllPinnedItemsFromPinnedGroup()
        {
            await ExpandOrCollapsePinnedTab();
            var allPinnedItemsLocator = Locator(SiteMapPanelLocators.AllPinnedItems);
            await ToBeVisibleAsync(allPinnedItemsLocator, 0, 30000);
            return await GetAllElementsText(allPinnedItemsLocator);
        }

        public async Task UnPinItemFromPinnedGroup(string itemName)
        {
            await ExpandOrCollapsePinnedTab();
            var unPinItemFromPinnedGroupLocator = Locator(SiteMapPanelLocators.UnPinPinnedItemFromPinnedGroup.Replace("[Name]", itemName));
            var pinnedItemLocator = Locator(SiteMapPanelLocators.PinnedItem.Replace("[Name]", itemName));
            await HoverAsync(pinnedItemLocator);
            await ClickAsync(unPinItemFromPinnedGroupLocator);
        }

        public async Task UnPinItemsFromPinnedGroup(List<string> items)
        {
            await ExpandOrCollapsePinnedTab();
            foreach (string item in items)
            {
                var unPinItemFromPinnedGroupLocator = Locator(SiteMapPanelLocators.UnPinPinnedItemFromPinnedGroup.Replace("[Name]", item));
                var pinnedItemLocator = Locator(SiteMapPanelLocators.PinnedItem.Replace("[Name]", item));
                await HoverAsync(pinnedItemLocator);
                await ClickAsync(unPinItemFromPinnedGroupLocator);
            }
        }

        public async Task<List<string?>> GetAllUnPinnedItemsFromRecentGroup()
        {
            await ExpandOrCollapseRecentTab();
            var allUnPinnedRecentItemsLocator = Locator(SiteMapPanelLocators.AllUnPinnedRecentItem);
            await ToBeVisibleAsync(allUnPinnedRecentItemsLocator, 0, 30000);
            return await GetAllElementsText(allUnPinnedRecentItemsLocator);
        }

        public async Task<List<string?>> GetAllItemsFromRecentGroup()
        {
            await ClickSiteMapTab("Recent");
            var allRecentItemsLocator = Locator(SiteMapPanelLocators.AllRecentItems);
            await ToBeVisibleAsync(allRecentItemsLocator, 0, 30000);
            return await GetAllElementsText(allRecentItemsLocator);
        }
    }
}
