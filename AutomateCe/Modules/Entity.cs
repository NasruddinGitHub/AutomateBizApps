using AutomateCe.Controls;
using AutomateCe.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class Entity : SharedPage
    {
        private IPage _page;

        public FileUpload FileUpload => this.GetElement<FileUpload>(_page);

        public Entity(IPage page) : base(page)
        {
            this._page = page;
        }

        public T GetElement<T>(IPage page)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { page });
        }

        public async Task SelectTab(string tabName, int timeToCheckIfFrameExists = 1000, string subTab = null)
        {
            var tabLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.Tab.Replace("[Name]", tabName), timeToCheckIfFrameExists);
            try
            {
                await ClickAsync(tabLocator, new LocatorClickOptions { Timeout = 5000 });
            }
            catch (TimeoutException ex)
            {
                await ClickRelatedTab(timeToCheckIfFrameExists);
                await ClickRelatedTabs(tabName, timeToCheckIfFrameExists);
            }
        }

        public async Task ClickRelatedTab(int timeToCheckIfFrameExists = 1000)
        {
            var relatedTabLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.RelatedTab, timeToCheckIfFrameExists);
            await ClickAsync(relatedTabLocator);
        }

        public async Task ClickRelatedTabs(string relatedTabName, int timeToCheckIfFrameExists = 1000)
        {
            var relatedTabsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.RelatedTabs.Replace("[Name]", relatedTabName), timeToCheckIfFrameExists);
            await ClickAsync(relatedTabsLocator);
        }

        public async Task<List<string>> GetAllShownTabs(int timeToCheckIfFrameExists = 1000)
        {
            var allShownTabsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.AllShownTabs, timeToCheckIfFrameExists);
            List<string> allShownTabs = await GetAllElementsTextAfterWaitingAsync(allShownTabsLocator);
            List<string> allShownTabsAfterTrimming = new List<string>();
            foreach (string shownTab in allShownTabs)
            {
                allShownTabsAfterTrimming.Add(shownTab.Substring(0, (shownTab.Length / 2)));
            }
            return allShownTabsAfterTrimming;
        }

        public async Task<List<string>> GetAllRelatedTabs(int timeToCheckIfFrameExists = 1000)
        {
            var relatedTabLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.RelatedTab, timeToCheckIfFrameExists);
            await ClickAsync(relatedTabLocator);
            var allRelatedTabsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.AllRelatedTabs, timeToCheckIfFrameExists);
            return await GetAllElementsTextAfterWaitingAsync(allRelatedTabsLocator);
        }

        public async Task SetValueByLabelNameAsync(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(fieldName, value, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(fieldName, string.Empty, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(fieldName, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(fieldName, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(lookupItem, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueByLabelNameAsync(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(optionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesByLabelNameAsync(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(multiSelectOptionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredByLabelNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequiredByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedByLabelNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommendedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalByLabelNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptionalByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedByLabelNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLockedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(fieldName, value, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueBySchemaNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(fieldName, string.Empty, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(fieldName, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(fieldName, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(lookupItem, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueBySchemaNameAsync(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(optionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(multiSelectOptionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredBySchemaNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequiredBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedBySchemaNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommendedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalBySchemaNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptionalBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedBySchemaNameAsync(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLockedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetFormHeaderTitleAsync()
        {
            string title = await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormHeaderTitle));
            return title.Split("-")[0].Trim();
        }

        public async Task<bool> IsFormSavedAsync()
        {
            string title = await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormHeaderTitle));
            String[] splitString = title.Split("-");
            return string.Equals(splitString[splitString.Length-1].Trim(), "Saved", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> IsWarningNotificationShownAsync()
        {
            return await IsVisibleAsyncWithWaiting(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification), 0);
        }

        public async Task<string> GetWarningNotificationTextAsync()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification));
        }

        public async Task WaitUntilWarningNotificationShownAsync()
        {
            await ToBeVisibleAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification), 0);
        }

        public async Task WaitUntilWarningNotificationNotShownAsync()
        {
            await ToBeNotVisibleAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification), 0);
        }

        public async Task<string> GetEntityNameAsync()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.EntityName));
        }

        public async Task<bool> IsFormSelectorShownAsync()
        {
            return await IsVisibleAsyncWithWaiting(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector), 0);
        }

        public async Task<string> GetSelectedFormNameAsync()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector));
        }

        public async Task SelectFormAsync(string formName)
        {
            ILocator formSelectionOpener = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector);
            await ClickAsync(formSelectionOpener);
            ILocator formSelectorItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormSelectorItems);
            await SelectOptionAsync(formSelectorItemsLocator, formName);
        }

        public async Task<List<string>> GetAllFormNamesAsync()
        {
            ILocator formSelectionOpener = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector);
            await ClickAsync(formSelectionOpener);
            ILocator allForms = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormSelectorItems);
            List<string> allFormNames = await GetAllElementsTextAsync(allForms);
            await KeyboardPressAsync("Tab");
            return allFormNames;
        }

        public async Task<string> GetHeaderControlValueAsync(string name)
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.HeaderControlValue.Replace("[Name]", name)));
        }

        public async Task ExpandHeaderFlyoutAsync()
        {
            ILocator headerFieldsExpandLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand);
            bool isHeaderFlyoutExpanded = bool.Parse(await GetAttributeAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand), "aria-expanded"));
            if (!isHeaderFlyoutExpanded)
                await ClickAsync(headerFieldsExpandLocator);
        }

        public async Task CloseHeaderFlyoutAsync()
        {
            ILocator headerFieldsExpandLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand);
            bool isHeaderFlyoutExpanded = bool.Parse(await GetAttributeAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand), "aria-expanded"));
            if (isHeaderFlyoutExpanded)
                await ClickAsync(headerFieldsExpandLocator);
        }

        public async Task SetHeaderValueByLabelNameAsync(string fieldName, string value, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(fieldName, value, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueByLabelNameAsync(string fieldName, string value, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueByLabelNameAsync(string fieldName, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(fieldName, string.Empty, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<string> GetHeaderValueByLabelNameAsync(string fieldName, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueByLabelNameAsync(fieldName, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<string> GetHeaderValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueByLabelNameAsync(fieldName, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task SetHeaderValueByLabelNameAsync(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(lookupItem, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueByLabelNameAsync(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValueByLabelNameAsync(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<string> GetHeaderValueByLabelNameAsync(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueByLabelNameAsync(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<string> GetHeaderValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task SetHeaderValueByLabelNameAsync(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(optionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<string> GetHeaderValueByLabelNameAsync(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueByLabelNameAsync(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<string> GetHeaderValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<List<string>> GetAllAvailableHeaderValuesByLabelNameAsync(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetAllAvailableValuesByLabelNameAsync(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task<List<string>> GetAllAvailableHeaderValuesByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetAllAvailableValuesByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task SetHeaderValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(multiSelectOptionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<List<string>> GetSelectedHeaderValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task<List<string>> GetSelectedHeaderValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task ClearHeaderValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<bool> IsHeaderFieldBusinessRequiredByLabelNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRequired = await IsFieldBusinessRequiredByLabelNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRequired;
        }

        public async Task<bool> IsHeaderFieldBusinessRequiredByLabelNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRequired = await IsFieldBusinessRequiredByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRequired;
        }

        public async Task<bool> IsHeaderFieldBusinessRecommendedByLabelNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRecommended = await IsFieldBusinessRecommendedByLabelNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRecommended;
        }

        public async Task<bool> IsHeaderFieldBusinessRecommendedByLabelNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRecommended = await IsFieldBusinessRecommendedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRecommended;
        }

        public async Task<bool> IsHeaderFieldOptionalByLabelNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isOptional = await IsFieldOptionalByLabelNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isOptional;
        }

        public async Task<bool> IsHeaderFieldOptionalByLabelNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isOptional = await IsFieldOptionalByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isOptional;
        }

        public async Task<bool> IsHeaderFieldLockedByLabelNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isLocked = await IsFieldLockedByLabelNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isLocked;
        }

        public async Task<bool> IsHeaderFieldLockedByLabelNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isLocked = await IsFieldLockedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isLocked;
        }

        public async Task SetHeaderValueBySchemaNameAsync(string fieldName, string value, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(fieldName, value, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueBySchemaNameAsync(string fieldName, string value, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueBySchemaNameAsync(string fieldName, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(fieldName, string.Empty, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<string> GetHeaderValueBySchemaNameAsync(string fieldName, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueBySchemaNameAsync(fieldName, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<string> GetHeaderValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueBySchemaNameAsync(fieldName, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task SetHeaderValueBySchemaNameAsync(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(lookupItem, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueBySchemaNameAsync(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValueBySchemaNameAsync(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<string> GetHeaderValueBySchemaNameAsync(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueBySchemaNameAsync(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<string> GetHeaderValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task SetHeaderValueBySchemaNameAsync(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(optionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<string> GetHeaderValueBySchemaNameAsync(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueBySchemaNameAsync(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<string> GetHeaderValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            string value = await GetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return value;
        }

        public async Task<List<string>> GetAllAvailableHeaderValuesBySchemaNameAsync(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetAllAvailableValuesBySchemaNameAsync(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task<List<string>> GetAllAvailableHeaderValuesBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetAllAvailableValuesBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task SetHeaderValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(multiSelectOptionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task SetHeaderValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await SetValueBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<List<string>> GetSelectedHeaderValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task<List<string>> GetSelectedHeaderValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            List<string> values = await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return values;
        }

        public async Task ClearHeaderValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task ClearHeaderValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
        }

        public async Task<bool> IsHeaderFieldBusinessRequiredBySchemaNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRequired = await IsFieldBusinessRequiredBySchemaNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRequired;
        }

        public async Task<bool> IsHeaderFieldBusinessRequiredBySchemaNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRequired = await IsFieldBusinessRequiredBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRequired;
        }

        public async Task<bool> IsHeaderFieldBusinessRecommendedBySchemaNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRecommended = await IsFieldBusinessRecommendedBySchemaNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRecommended;
        }

        public async Task<bool> IsHeaderFieldBusinessRecommendedBySchemaNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isRecommended = await IsFieldBusinessRecommendedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isRecommended;
        }

        public async Task<bool> IsHeaderFieldOptionalBySchemaNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isOptional = await IsFieldOptionalBySchemaNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isOptional;
        }

        public async Task<bool> IsHeaderFieldOptionalBySchemaNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isOptional = await IsFieldOptionalBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isOptional;
        }

        public async Task<bool> IsHeaderFieldLockedBySchemaNameAsync(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isLocked = await IsFieldLockedBySchemaNameAsync(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isLocked;
        }

        public async Task<bool> IsHeaderFieldLockedBySchemaNameAsync(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyoutAsync();
            bool isLocked = await IsFieldLockedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyoutAsync();
            return isLocked;
        }
    }
}
