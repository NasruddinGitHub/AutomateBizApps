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
            await SetValueByLabelName(fieldName, value, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelName(fieldName, value, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelName(fieldName, string.Empty, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelName(fieldName, string.Empty, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelName(fieldName, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelName(fieldName, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelName(lookupItem, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelName(lookupItem, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueByLabelName(lookupItem, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValueByLabelName(lookupItem, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelName(lookupItem, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelName(lookupItem, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelName(optionSet, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelName(optionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelName(optionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelName(optionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesByLabelName(optionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesByLabelName(optionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelName(multiSelectOptionSet, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelName(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesByLabelName(multiSelectOptionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValuesByLabelName(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesByLabelName(multiSelectOptionSet, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValuesByLabelName(multiSelectOptionSet, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredByLabelName(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequiredByLabelName(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedByLabelName(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommendedByLabelName(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalByLabelName(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptionalByLabelName(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        protected async Task<bool> IsFieldLockedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedByLabelName(field, false, FormContextType.Dialog, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldLockedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLockedByLabelName(field, dynamicallyLoaded, FormContextType.Dialog, timeToCheckIfFrameExists);
        }
    }
}
