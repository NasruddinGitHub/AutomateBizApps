﻿using AutomateBizApps.Modules;
using AutomateCe.Controls;
using AutomateCe.Enums;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
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

        public async Task SetValue(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, value, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValue(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, value, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValue(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, string.Empty, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValue(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValue(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(fieldName, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValue(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(fieldName, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValue(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(lookupItem, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValue(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValue(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValue(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValue(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValue(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValue(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(lookupItem, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValue(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(lookupItem, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValue(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(optionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValue(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValue(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValue(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValues(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValues(optionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValues(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValues(optionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValue(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(multiSelectOptionSet, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task SetValue(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValues(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValues(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValues(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValues(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValues(multiSelectOptionSet, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task ClearValues(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> isFieldBusinessRequired(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldBusinessRequired(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> isFieldBusinessRequired(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldBusinessRequired(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> isFieldBusinessRecommended(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldBusinessRecommended(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> isFieldBusinessRecommended(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldBusinessRecommended(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> isFieldOptional(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldOptional(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<bool> isFieldOptional(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldOptional(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        protected async Task<bool> isFieldLocked(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldLocked(field, false, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        protected async Task<bool> isFieldLocked(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldLocked(field, dynamicallyLoaded, FormContextType.Entity, timeToCheckIfFrameExists);
        }

        public async Task<string> GetFormHeaderTitle()
        {
            string title = await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormHeaderTitle));
            return title.Split("-")[0].Trim();
        }

        public async Task<string> GetEntityName()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.EntityName));
        }

        public async Task<string> GetSelectedFormName()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector));
        }

        public async Task SelectForm(string formName)
        {
            ILocator formSelectionOpener = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector);
            await ClickAsync(formSelectionOpener);
            await SelectOption(formSelectionOpener, formName);
        }

        public async Task<List<string>> GetAllFormNames()
        {
            ILocator formSelectionOpener = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelector);
            await ClickAsync(formSelectionOpener);
            ILocator allForms = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormSelectorItems);
            return await GetAllElementsText(allForms);
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

        public async Task SetHeaderValue(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await ExpandHeaderFlyout();
            await SetValue(fieldName, value, FormContextType.Header, timeToCheckIfFrameExists);
            await CloseHeaderFlyout();
        }

        public async Task SetHeaderValue(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ExpandHeaderFlyout();
            await SetValue(fieldName, value, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            await CloseHeaderFlyout();
        }

        public async Task ClearHeaderValue(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, string.Empty, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task ClearHeaderValue(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetHeaderValue(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(fieldName, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<string> GetHeaderValue(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(fieldName, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetHeaderValue(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(lookupItem, false, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task SetHeaderValue(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearHeaderValue(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValue(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task ClearHeaderValue(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValue(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetHeaderValue(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(lookupItem, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<string> GetHeaderValue(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(lookupItem, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetHeaderValue(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(optionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task SetHeaderValue(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetHeaderValue(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<string> GetHeaderValue(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableHeaderValues(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValues(optionSet, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableHeaderValues(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValues(optionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetHeaderValue(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(multiSelectOptionSet, false, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task SetHeaderValue(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedHeaderValues(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValues(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedHeaderValues(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearHeaderValues(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValues(multiSelectOptionSet, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task ClearHeaderValues(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> isHeaderFieldBusinessRequired(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldBusinessRequired(field, false, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<bool> isHeaderFieldBusinessRequired(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldBusinessRequired(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> isHeaderFieldBusinessRecommended(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldBusinessRecommended(field, false, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<bool> isHeaderFieldBusinessRecommended(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldBusinessRecommended(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> isHeaderFieldOptional(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldOptional(field, false, FormContextType.Header, timeToCheckIfFrameExists);
        }

        public async Task<bool> isHeaderFieldOptional(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldOptional(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        protected async Task<bool> isHeaderFieldLocked(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await isFieldLocked(field, false, FormContextType.Header, timeToCheckIfFrameExists);
        }

        protected async Task<bool> isHeaderFieldLocked(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await isFieldLocked(field, dynamicallyLoaded, FormContextType.Header, timeToCheckIfFrameExists);
        }

    }
}
