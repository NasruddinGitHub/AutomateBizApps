using AutomateCe.Controls;
using AutomateCe.Enums;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Modules
{

    public class Dialog : SharedPage
    {
        private IPage _page;

        public Dialog(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task SetValueByLabelName(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, value, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, value, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, string.Empty, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(fieldName, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(fieldName, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(lookupItem, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(lookupItem, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValue(lookupItem, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValue(lookupItem, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(lookupItem, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(lookupItem, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(optionSet, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(optionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(optionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(optionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValues(optionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValues(optionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(multiSelectOptionSet, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValues(multiSelectOptionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValues(multiSelectOptionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequired(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequired(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommended(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommended(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptional(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptional(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        protected async Task<bool> IsFieldLockedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLocked(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldLockedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLocked(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists);
        }
    }
}
