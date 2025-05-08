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

    public class BusinessProcessFlow : SharedPage
    {
        private IPage _page;

        public BusinessProcessFlow(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task SetValueByLabelNameAsync(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(fieldName, value, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(fieldName, string.Empty, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(fieldName, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(fieldName, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(lookupItem, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueByLabelNameAsync(lookupItem, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(lookupItem, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(optionSet, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(optionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesByLabelNameAsync(optionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesByLabelNameAsync(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(multiSelectOptionSet, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredByLabelNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequiredByLabelNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedByLabelNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommendedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalByLabelNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldCompletedByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldCompletedByLabelNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptionalByLabelNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedByLabelNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedByLabelNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedByLabelNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLockedByLabelNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(fieldName, value, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(fieldName, value, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueBySchemaNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(fieldName, string.Empty, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(fieldName, string.Empty, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(fieldName, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(fieldName, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(lookupItem, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueBySchemaNameAsync(lookupItem, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(lookupItem, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(optionSet, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(optionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValueBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(optionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(multiSelectOptionSet, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValueBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredBySchemaNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequiredBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedBySchemaNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommendedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalBySchemaNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldCompletedBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldCompletedBySchemaNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptionalBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedBySchemaNameAsync(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedBySchemaNameAsync(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedBySchemaNameAsync(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLockedBySchemaNameAsync(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SelectStageAsync(string stageName)
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.StageName.Replace("[Name]", stageName)));
        }

        public async Task NextStageAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.NextStage));
        }

        public async Task PreviousStageAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.PreviousStage));
        }

        public async Task SetActiveAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.SetActive));
        }

        public async Task CloseAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Close));
        }

        public async Task<string> HeaderAsync()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Heading));
        }

        public async Task PinAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Pin));
        }

        public async Task<List<string>> GetStageNamesAsync()
        {
            return await GetAllElementsTextAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.StageNames));
        }

        public async Task<string> GetSelectedStageNameAsync()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.SelectedStageName));
        }

        public async Task<List<string>> GetUnselectedStageNameAsync()
        {
            List<string> allStages = await GetStageNamesAsync();
            string selectedStage = await GetSelectedStageNameAsync();
            allStages.Remove(selectedStage);
            return allStages;
        }

        public async Task FinishAsync()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Finish));
        }
    }
}
