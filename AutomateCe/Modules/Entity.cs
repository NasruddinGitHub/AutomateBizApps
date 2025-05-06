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
            var tabLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.Tab.Replace("[Name]", tabName), timeToCheckIfFrameExists);
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

        public async Task SetValueByLabelName(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, value, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, value, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, string.Empty, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(fieldName, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(fieldName, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(lookupItem, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValue(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValue(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(optionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValues(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValues(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(multiSelectOptionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValues(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValues(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequired(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequired(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommended(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommended(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptional(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptional(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLocked(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLocked(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetFormHeaderTitle()
        {
            string title = await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormHeaderTitle));
            return title.Split("-")[0].Trim();
        }

        public async Task<bool> IsFormSaved()
        {
            string title = await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormHeaderTitle));
            String[] splitString = title.Split("-");
            return string.Equals(splitString[splitString.Length-1].Trim(), "Saved", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> IsWarningNotificationShown()
        {
            return await IsVisibleAsyncWithWaiting(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification), 0);
        }

        public async Task<string> GetWarningNotificationText()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification));
        }

        public async Task WaitUntilWarningNotificationShown()
        {
            await ToBeVisibleAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification), 0);
        }

        public async Task WaitUntilWarningNotificationNotShown()
        {
            await ToBeNotVisibleAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.WarningNotification), 0);
        }

        public async Task<string> GetEntityName()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.EntityName));
        }

        public async Task<bool> IsFormSelectorShown()
        {
            return await IsVisibleAsyncWithWaiting(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector), 0);
        }

        public async Task<string> GetSelectedFormName()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector));
        }

        public async Task SelectForm(string formName)
        {
            ILocator formSelectionOpener = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector);
            await ClickAsync(formSelectionOpener);
            ILocator formSelectorItemsLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelectorItems);
            await SelectOption(formSelectorItemsLocator, formName);
        }

        public async Task<List<string>> GetAllFormNames()
        {
            ILocator formSelectionOpener = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector);
            await ClickAsync(formSelectionOpener);
            ILocator allForms = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelectorItems);
            List<string> allFormNames = await GetAllElementsText(allForms);
            await KeyboardPressAsync("Tab");
            return allFormNames;
        }

        public async Task<string> GetHeaderControlValue(string name)
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.HeaderControlValue.Replace("[Name]", name)));
        }

        public async Task ExpandHeaderFlyout()
        {
            ILocator headerFieldsExpandLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand);
            bool isHeaderFlyoutExpanded = bool.Parse(await GetAttributeAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand), "aria-expanded"));
            if (!isHeaderFlyoutExpanded)
                await ClickAsync(headerFieldsExpandLocator);
        }

        public async Task CloseHeaderFlyout()
        {
            ILocator headerFieldsExpandLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand);
            bool isHeaderFlyoutExpanded = bool.Parse(await GetAttributeAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.HeaderFieldsExpand), "aria-expanded"));
            if (isHeaderFlyoutExpanded)
                await ClickAsync(headerFieldsExpandLocator);
        }

        public async Task SetHeaderValueByLabelName(string fieldName, string value, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(fieldName, value, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task SetHeaderValueByLabelName(string fieldName, string value, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(fieldName, value, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task ClearHeaderValueByLabelName(string fieldName, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(fieldName, string.Empty, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task ClearHeaderValueByLabelName(string fieldName, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task<string> GetHeaderValueByLabelName(string fieldName, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            string value = await GetValue(fieldName, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return value;
        }

        public async Task<string> GetHeaderValueByLabelName(string fieldName, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            string value = await GetValue(fieldName, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return value;
        }

        public async Task SetHeaderValueByLabelName(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(lookupItem, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task SetHeaderValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task ClearHeaderValueByLabelName(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await ClearValue(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task ClearHeaderValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await ClearValue(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task<string> GetHeaderValueByLabelName(LookupItem lookupItem, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            string value = await GetValue(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return value;
        }

        public async Task<string> GetHeaderValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            string value = await GetValue(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return value;
        }

        public async Task SetHeaderValueByLabelName(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(optionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task SetHeaderValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task<string> GetHeaderValueByLabelName(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            string value = await GetValue(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return value;
        }

        public async Task<string> GetHeaderValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            string value = await GetValue(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return value;
        }

        public async Task<List<string>> GetAllAvailableHeaderValuesByLabelName(OptionSet optionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            List<string> values = await GetAllAvailableValues(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return values;
        }

        public async Task<List<string>> GetAllAvailableHeaderValuesByLabelName(OptionSet optionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            List<string> values = await GetAllAvailableValues(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return values;
        }

        public async Task SetHeaderValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(multiSelectOptionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task SetHeaderValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await SetValue(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task<List<string>> GetSelectedHeaderValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            List<string> values = await GetSelectedValues(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return values;
        }

        public async Task<List<string>> GetSelectedHeaderValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            List<string> values = await GetSelectedValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return values;
        }

        public async Task ClearHeaderValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await ClearValues(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task ClearHeaderValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            await ClearValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
        }

        public async Task<bool> IsHeaderFieldBusinessRequiredByLabelName(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isRequired = await IsFieldBusinessRequired(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isRequired;
        }

        public async Task<bool> IsHeaderFieldBusinessRequiredByLabelName(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isRequired = await IsFieldBusinessRequired(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isRequired;
        }

        public async Task<bool> IsHeaderFieldBusinessRecommendedByLabelName(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isRecommended = await IsFieldBusinessRecommended(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isRecommended;
        }

        public async Task<bool> IsHeaderFieldBusinessRecommendedByLabelName(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isRecommended = await IsFieldBusinessRecommended(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isRecommended;
        }

        public async Task<bool> IsHeaderFieldOptionalByLabelName(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isOptional = await IsFieldOptional(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isOptional;
        }

        public async Task<bool> IsHeaderFieldOptionalByLabelName(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isOptional = await IsFieldOptional(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isOptional;
        }

        public async Task<bool> IsHeaderFieldLockedByLabelName(String field, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isLocked = await IsFieldLocked(field, false, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isLocked;
        }

        public async Task<bool> IsHeaderFieldLockedByLabelName(String field, bool dynamicallyLoaded, bool expandHeaderFlyout = false, bool closeHeaderFlyout = false, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (expandHeaderFlyout)
                await ExpandHeaderFlyout();
            bool isLocked = await IsFieldLocked(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists);
            if (closeHeaderFlyout)
                await CloseHeaderFlyout();
            return isLocked;
        }

    }
}
