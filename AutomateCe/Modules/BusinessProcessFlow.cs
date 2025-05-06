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

        public async Task SetValueByLabelName(string fieldName, string value, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, value, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(string fieldName, string value, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, value, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, string.Empty, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, string.Empty, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(string fieldName, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(fieldName, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(string fieldName, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(fieldName, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(lookupItem, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValue(lookupItem, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValue(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(lookupItem, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(LookupItem lookupItem, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(lookupItem, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(optionSet, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(optionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<string> GetValueByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetValue(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValues(optionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetAllAvailableValuesByLabelName(OptionSet optionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetAllAvailableValues(optionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(multiSelectOptionSet, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SetValueByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValues(multiSelectOptionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<List<string>> GetSelectedValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await GetSelectedValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValues(multiSelectOptionSet, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task ClearValuesByLabelName(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            await ClearValues(multiSelectOptionSet, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequired(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRequiredByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRequired(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommended(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldBusinessRecommendedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldBusinessRecommended(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptional(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldCompletedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldCompleted(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldOptionalByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldOptional(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
        }

        public async Task<bool> IsFieldLockedByLabelName(String field, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLocked(field, false, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task<bool> IsFieldLockedByLabelName(String field, bool dynamicallyLoaded, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            return await IsFieldLocked(field, dynamicallyLoaded, FormContextType.BusinessProcessFlow, timeToCheckIfFrameExists);
        }

        public async Task SelectStage(string stageName)
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.StageName.Replace("[Name]", stageName)));
        }

        public async Task NextStage()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.NextStage));
        }

        public async Task PreviousStage()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.PreviousStage));
        }

        public async Task SetActive()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.SetActive));
        }

        public async Task Close()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Close));
        }

        public async Task<string> Header()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Heading));
        }

        public async Task Pin()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Pin));
        }

        public async Task<List<string>> GetStageNames()
        {
            return await GetAllElementsText(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.StageNames));
        }

        public async Task<string> GetSelectedStageName()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.SelectedStageName));
        }

        public async Task<List<string>> GetUnselectedStageName()
        {
            List<string> allStages = await GetStageNames();
            string selectedStage = await GetSelectedStageName();
            allStages.Remove(selectedStage);
            return allStages;
        }

        public async Task Finish()
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.Finish));
        }
    }
}
