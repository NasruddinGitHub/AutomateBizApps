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

        public async Task ExpandSiteMapAsync()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            await ClickAsync(siteMapLauncherElement);
        }

        public async Task ShrinkSiteMapAsync()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            await ClickAsync(siteMapLauncherElement);
        }

        public async Task ExpandSiteMapIfShrinkedAsync()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            bool isSiteMapExpanded = Convert.ToBoolean(await GetAttributeAsync(siteMapLauncherElement, "aria-expanded"));
            if (!isSiteMapExpanded)
            {
                await ClickAsync(siteMapLauncherElement);
            }
        }

        public async Task ShrinkSiteMapIfExpandedAsync()
        {
            var siteMapLauncherElement = Locator(SiteMapPanelLocators.SiteMapLauncherOrCloser);
            bool isSiteMapExpanded = Convert.ToBoolean(await GetAttributeAsync(siteMapLauncherElement, "aria-expanded"));
            if (isSiteMapExpanded)
            {
                await ClickAsync(siteMapLauncherElement);
            }
        }

        public async Task OpenSubAreaAsync(string area, string subarea)
        {
            string subAreaLocator = SiteMapPanelLocators.SubArea.Replace("[Area]", area).Replace("[Subarea]", subarea);
            var subAreaElement = Locator(subAreaLocator);
            await ClickAsync(subAreaElement);
        }

        public async Task<List<string>> GetSubAreasAsync(string area)
        {
            var subAreaTreeItemsSelector = SiteMapPanelLocators.SiteMapEntity.Replace("[Area]", area);
            var subAreaTreeItems = Locator(subAreaTreeItemsSelector);
            var subAreaTreeItemsCount = await CountAsync(subAreaTreeItems);
            return await GetAllElementsTextAsync(subAreaTreeItems);
        }

        public async Task ChangeAreaAsync(string areaToSelect)
        {
            var areaChangerElement = Locator(SiteMapPanelLocators.AreaChanger);
            await ClickAsync(areaChangerElement);
            var areaChangerItemElement = Locator(SiteMapPanelLocators.AreaChangerItem.Replace("[Name]", areaToSelect));
            await ClickAsync(areaChangerItemElement);
        }

        public async Task<List<string>> GetChangeAreaItemsAsync()
        {
            var areaChangerElement = Locator(SiteMapPanelLocators.AreaChanger);
            await ClickAsync(areaChangerElement);
            var areaChangerItemsElement = Locator(SiteMapPanelLocators.AreaChangerItems);
            return await GetAllElementsTextAsync(areaChangerItemsElement);
        }

        public async Task<string?> GetCurrentlySelectedAreaAsync()
        {
            var areaChangerElement = Locator(SiteMapPanelLocators.AreaChanger);
            return await TextContentAsync(areaChangerElement);
        }

        public async Task ClickSiteMapTabAsync(string tabName)
        {
            var siteMapTabElement = Locator(SiteMapPanelLocators.SiteMapTab.Replace("[Name]", tabName));
            await ClickAsync(siteMapTabElement);
        }

        public async Task ExpandOrCollapseRecentTabAsync()
        {
            await ClickSiteMapTabAsync("Recent");
        }

        public async Task ExpandOrCollapsePinnedTabAsync()
        {
            await ClickSiteMapTabAsync("Pinned");
        }

        public async Task OpenItemFromRecentGroupAsync(string itemName)
        {
            await ExpandOrCollapseRecentTabAsync();
            var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", itemName));
            await ExpandOrCollapseRecentTabAsync();
        }

        public async Task PinRecentItemAsync(string itemName)
        {
            await ExpandOrCollapseRecentTabAsync();
            var recentItemPinLocator = Locator(SiteMapPanelLocators.PinRecentItem.Replace("[Name]", itemName));
            var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", itemName));
            await HoverAsync(recentItemLocator);
            await ClickAsync(recentItemPinLocator);
        }

        public async Task PinRecentItems(List<string> items)
        {
            await ExpandOrCollapseRecentTabAsync();
            foreach (string item in items)
            {
                var recentItemPinLocator = Locator(SiteMapPanelLocators.PinRecentItem.Replace("[Name]", item));
                var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", item));
                await HoverAsync(recentItemLocator);
                await ClickAsync(recentItemPinLocator);
            }
        }

        public async Task UnPinRecentItemAsync(string itemName)
        {
            await ExpandOrCollapseRecentTabAsync();
            var recentItemUnpinLocator = Locator(SiteMapPanelLocators.UnPinRecentItem.Replace("[Name]", itemName));
            var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", itemName));
            await HoverAsync(recentItemLocator);
            await ClickAsync(recentItemUnpinLocator);
        }

        public async Task UnPinRecentItemsAsync(List<string> items)
        {
            await ExpandOrCollapseRecentTabAsync();
            foreach (string item in items)
            {
                var recentItemUnpinLocator = Locator(SiteMapPanelLocators.UnPinRecentItem.Replace("[Name]", item));
                var recentItemLocator = Locator(SiteMapPanelLocators.RecentItem.Replace("[Name]", item));
                await HoverAsync(recentItemLocator);
                await ClickAsync(recentItemUnpinLocator);
            }
        }

        public async Task<List<string?>> GetAllPinnedItemsFromRecentGroupAsync()
        {
            await ExpandOrCollapseRecentTabAsync();
            var allPinnedRecentItemsLocator = Locator(SiteMapPanelLocators.AllPinnedRecentItem);
            await ToBeVisibleAsync(allPinnedRecentItemsLocator, 0, 30000);
            return await GetAllElementsTextAsync(allPinnedRecentItemsLocator);
        }

        public async Task<List<string?>> GetAllPinnedItemsFromPinnedGroupAsync()
        {
            await ExpandOrCollapsePinnedTabAsync();
            var allPinnedItemsLocator = Locator(SiteMapPanelLocators.AllPinnedItems);
            await ToBeVisibleAsync(allPinnedItemsLocator, 0, 30000);
            return await GetAllElementsTextAsync(allPinnedItemsLocator);
        }

        public async Task UnPinItemFromPinnedGroupAsync(string itemName)
        {
            await ExpandOrCollapsePinnedTabAsync();
            var unPinItemFromPinnedGroupLocator = Locator(SiteMapPanelLocators.UnPinPinnedItemFromPinnedGroup.Replace("[Name]", itemName));
            var pinnedItemLocator = Locator(SiteMapPanelLocators.PinnedItem.Replace("[Name]", itemName));
            await HoverAsync(pinnedItemLocator);
            await ClickAsync(unPinItemFromPinnedGroupLocator);
        }

        public async Task UnPinItemsFromPinnedGroupAsync(List<string> items)
        {
            await ExpandOrCollapsePinnedTabAsync();
            foreach (string item in items)
            {
                var unPinItemFromPinnedGroupLocator = Locator(SiteMapPanelLocators.UnPinPinnedItemFromPinnedGroup.Replace("[Name]", item));
                var pinnedItemLocator = Locator(SiteMapPanelLocators.PinnedItem.Replace("[Name]", item));
                await HoverAsync(pinnedItemLocator);
                await ClickAsync(unPinItemFromPinnedGroupLocator);
            }
        }

        public async Task<List<string?>> GetAllUnPinnedItemsFromRecentGroupAsync()
        {
            await ExpandOrCollapseRecentTabAsync();
            var allUnPinnedRecentItemsLocator = Locator(SiteMapPanelLocators.AllUnPinnedRecentItem);
            await ToBeVisibleAsync(allUnPinnedRecentItemsLocator, 0, 30000);
            return await GetAllElementsTextAsync(allUnPinnedRecentItemsLocator);
        }

        public async Task<List<string?>> GetAllItemsFromRecentGroupAsync()
        {
            await ClickSiteMapTabAsync("Recent");
            var allRecentItemsLocator = Locator(SiteMapPanelLocators.AllRecentItems);
            await ToBeVisibleAsync(allRecentItemsLocator, 0, 30000);
            return await GetAllElementsTextAsync(allRecentItemsLocator);
        }
    }
}
