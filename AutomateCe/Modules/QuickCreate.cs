using AutomateCe.Controls;
using AutomateCe.Enums;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class QuickCreate : SharedPage
    {
        private IPage _page;

        public QuickCreate(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task SetValueByLabelNameAsync(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(fieldName, value, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(fieldName, string.Empty, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(fieldName, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(fieldName, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(lookupItem, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueByLabelNameAsync(lookupItem, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(lookupItem, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(optionSet, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(optionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesByLabelNameAsync(optionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(multiSelectOptionSet, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredByLabelNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequiredByLabelNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedByLabelNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommendedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalByLabelNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptionalByLabelNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedByLabelNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLockedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(fieldName, value, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueBySchemaNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(fieldName, string.Empty, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ClearValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(fieldName, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(fieldName, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(lookupItem, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueBySchemaNameAsync(lookupItem, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(lookupItem, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(optionSet, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(optionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(optionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(multiSelectOptionSet, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredBySchemaNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequiredBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedBySchemaNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommendedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalBySchemaNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptionalBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedBySchemaNameAsync(field, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLockedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task CancelAsync()
        {
            await ClickButtonAsync("Cancel");
        }

        public async Task CloseAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, QuickCreateLocators.Close));
        }

        public async Task SaveAsync()
        {
            await ClickButtonAsync("Save");
        }

        public async Task SaveAndCloseAsync()
        {
            await ClickButtonAsync("Save and Close");
            await WaitUntilAppIsIdleAsync();
        }

        public async Task SaveAndCreateNewAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, QuickCreateLocators.SaveAndNewBtnOpener));
            await ClickButtonAsync("Save & Create New");
        }

        public async Task<bool> IsBooleanFieldToggledOnBySchemaAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsBooleanFieldToggledOnBySchemaAsync(field, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsBooleanFieldToggledOnBySchemaAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsBooleanFieldToggledOnBySchemaAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ToggleBooleanFieldOnBySchemaAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            await ToggleBooleanFieldOnBySchemaAsync(field, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ToggleBooleanFieldOnBySchemaAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ToggleBooleanFieldOnBySchemaAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ToggleBooleanFieldOffBySchemaAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            await ToggleBooleanFieldOffBySchemaAsync(field, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ToggleBooleanFieldOffBySchemaAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ToggleBooleanFieldOffBySchemaAsync(field, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(lookupItem, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task AssertFieldToBeVisibleBySchemaAsync(string fieldContainer, int timeToCheckIfFrameExists = 1000)
        {
            await AssertElementToBeVisibleBySchemaAsync(fieldContainer, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task AssertFieldNotToBeVisibleBySchemaAsync(string fieldContainer, int timeToCheckIfFrameExists = 1000)
        {
            await AssertElementNotToBeVisibleBySchemaAsync(fieldContainer, false, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsLookupValueEmptyBySchemaAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await IsLookupValueEmptyBySchemaAsync(lookupItem, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsLookupValueEmptyBySchemaAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsLookupValueEmptyBySchemaAsync(lookupItem, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllNotificationsAsync(int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllNotificationsAsync(FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllNotificationsAsync(bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllNotificationsAsync(dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
        }

        public async Task<int> GetNumberOfNotificationsAsync(int timeToCheckIfFrameExists = 1000)
        {
            return await GetNumberOfNotificationsAsync(FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<int> GetNumberOfNotificationsAsync(bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetNumberOfNotificationsAsync(dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetNotificationTextAsync(int index, int timeToCheckIfFrameExists = 1000)
        {
            return await GetNotificationTextAsync(index, FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task<string> GetNotificationTextAsync(int index, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetNotificationTextAsync(index, dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
        }

        public async Task ExpandNotificationAsync(int timeToCheckIfFrameExists = 1000)
        {
            await ExpandNotificationAsync(FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ExpandNotificationAsync(bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ExpandNotificationAsync(dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
        }

        public async Task ContractNotificationAsync(int timeToCheckIfFrameExists = 1000)
        {
            await ContractNotificationAsync(FormContextType.QuickCreate, timeToCheckIfFrameExists);
        }

        public async Task ContractNotificationAsync(bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ContractNotificationAsync(dynamicallyLoaded, FormContextType.QuickCreate, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
        }
    }
}
