using AutomateCe.Assertions;
using AutomateCe.Controls;
using AutomateCe.Enums;
using AutomateCe.Modules;
using AutomateCe.Pages;
using Microsoft.CodeAnalysis;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class SharedPage : BaseModule
    {
        private IPage _page;

        public SharedPage(IPage page) : base(page)
        {
            this._page = page;
        }
        protected async Task WaitUntilSpinnerNotDisplayedAsync()
        {
            var spinnerLocator = Locator(GridLocators.Spinner);
            await WaitUntilElementNotVisbleIfVisibleAsync(spinnerLocator, 3000, 5000);
        }

        protected ILocator GetFieldWithXpathByLabel(string fieldName)
        {
            return LocatorWithXpath(CommonLocators.FieldContainerByLabel.Replace("[Name]", fieldName));
        }

        protected ILocator GetFieldByLabel(string fieldName)
        {
            return Locator(CommonLocators.FieldContainerByLabel.Replace("[Name]", fieldName));
        }

        protected async Task ClickButtonAsync(string buttonName)
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.Button.Replace("[Name]", buttonName)));
        }

        protected async Task SetValueByLabelNameAsync(string fieldName, string input, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(fieldName, input, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValueByLabelNameAsync(string fieldName, string input, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            bool isValueSet = false;
            var completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, fieldName, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            var inputTagLocator = Locator(completeFieldLocator, CommonLocators.TextBoxControl);
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                await FillAsync(inputTagLocator, input);
                isValueSet = true;
                return;
            }
            var textareaTagLocator = Locator(completeFieldLocator, CommonLocators.TextAreaControl);
            bool isTextareaTagElementDisplayed = await IsVisibleAsyncWithWaiting(textareaTagLocator, 0, 1000);
            if (isTextareaTagElementDisplayed)
            {
                await FillAsync(textareaTagLocator, input);
                isValueSet = true;
                return;
            }
            var richTextLocator = LocatorWithXpath(completeFieldLocator, "descendant::p");
            bool isRichTextLocatorDisplayed = await IsVisibleAsyncWithWaiting(richTextLocator, 0, 1000);
            if (isRichTextLocatorDisplayed)
            {
                await FillAsync(richTextLocator, input);
                isValueSet = true;
                return;
            }

            if (!isValueSet)
            {
                throw new PlaywrightException($"{fieldName} element is not found");
            }
        }

        protected async Task<string> GetValueByLabelNameAsync(string fieldName, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(fieldName, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValueByLabelNameAsync(string fieldName, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            bool isValueGotten = false;
            string value = null;
            var completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, fieldName, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            var inputTagLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.TextBoxControl);
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(inputTagLocator);
            }
            var textareaTagLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.TextAreaControl);
            bool isTextareaTagElementDisplayed = await IsVisibleAsyncWithWaiting(textareaTagLocator, 0, 1000);
            if (isTextareaTagElementDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(textareaTagLocator);
            }
            var richTextLocator = LocatorWithXpath(completeFieldLocator, "descendant::p");
            bool richTextLocatorDisplayed = await IsVisibleAsyncWithWaiting(richTextLocator, 0, 1000);
            if (richTextLocatorDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(richTextLocator);
            }
            if (!isValueGotten)
            {
                throw new PlaywrightException(fieldName + " Element is not found");
            }
            return value;
        }

        protected async Task SetValueByLabelNameAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            int index = lookupItem.Index;
            string value = lookupItem.Value;
            await SetValueByLabelNameAsync(field, value, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            await WaitUntilAppIsIdleAsync();
            ILocator noLookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.NoLookupResults);
            if (await IsVisibleAsyncWithWaiting(noLookUpItemsLocator, 0, 1000)) {
                throw new ArgumentException($"Given {value} is not available in {field} lookup.");
            }
            ILocator lookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.LookupResults);
            lookUpItemsLocator = LocatorWithXpath(lookUpItemsLocator, "li");
            await SelectOptionAsync(lookUpItemsLocator, index);
        }

        protected async Task ClearValueByLabelNameAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueByLabelNameAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClearValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.CloseIconLookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(locator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(locator);
        }

        protected async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValueByLabelNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.LookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(locator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(locator);
        }

        protected async Task SetValueByLabelNameAsync(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownOpenerLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.DropdownValues);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(dropdownOpenerLocator, 0, 100, maxNumberOfScrolls);
            }
            await SelectDropdownOptionAsync(dropdownOpenerLocator, dropdownValuesLocator, value);
        }

        protected async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueByLabelNameAsync(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValueByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(dropdownLocator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(dropdownLocator);
        }

        protected async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesByLabelNameAsync(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetAllAvailableValuesByLabelNameAsync(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.DropdownValues);
            return await GetAllAvailableChoicesAsync(dropdownLocator, dropdownValuesLocator, dynamicallyLoaded, anySelectorInScroller, maxNumberOfScrolls);
        }

        protected async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueByLabelNameAsync(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        // This method does not work if the value(s) already selected. We will have to write new function.
        protected async Task SetValueByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            string[] values = multiSelectOptionSet.Values;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            ILocator dropdownOptionsOpenerLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.MultiSelectOptionSetOpener);
            ILocator dropdownValues = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.MultiSelectOptions);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(dropdownOptionsOpenerLocator, 0, 100, maxNumberOfScrolls);
            }
            await SelectMultiSelectOptionsAsync(dropdownOptionsOpenerLocator, dropdownValues, values);
            await ClickAsync(dropdownOptionsOpenerLocator);
        }

        protected async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesByLabelNameAsync(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClearValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            string[] values = multiSelectOptionSet.Values;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }

            await HoverAsync(LocatorWithXpath(completeFieldLocator, CommonLocators.SelectedOptionsValueContainer), new LocatorHoverOptions { Force = true });
            foreach (string value in values)
            {
                ILocator dropdownOptionRemoveLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.RemoveOptionInMultiSelectOptionSet.Replace("[Name]", value));
                await ClickAsync(dropdownOptionRemoveLocator);
            }
        }

        protected async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetSelectedValuesByLabelNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            ILocator FieldContainerBylabel = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(FieldContainerBylabel, 0, 100, maxNumberOfScrolls);
            }

            string allValues = await TextContentAsync(LocatorWithXpath(FieldContainerBylabel, CommonLocators.SelectedOptionsTextInMultiSelectOptionSet));
            string[] splittedValues = allValues.Split(",");
            List<string> outputValues = new List<string>();
            foreach (string value in splittedValues)
            {
                outputValues.Add(value.Trim());
            }
            return outputValues;
        }

        private async Task<int> GetFieldRequirementByLabelNameAsync(string field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator FieldContainerBylabel = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(FieldContainerBylabel, 0, 100, maxNumberOfScrolls);
            }
            return int.Parse(await GetAttributeAsync(FieldContainerBylabel, "data-fieldrequirement"));
        }

        protected async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredByLabelNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldBusinessRequiredByLabelNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirementByLabelNameAsync(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 1 || fieldRequirement == 2;
        }

        protected async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedByLabelNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldBusinessRecommendedByLabelNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirementByLabelNameAsync(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 3;
        }

        protected async Task<bool> IsFieldOptionalByLabelNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalByLabelNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldOptionalByLabelNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirementByLabelNameAsync(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 0;
        }

        protected async Task<bool> IsFieldCompletedByLabelNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldCompletedByLabelNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldCompletedByLabelNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator FieldContainerBylabel = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(FieldContainerBylabel, 0, 100, maxNumberOfScrolls);
            }
            return await IsVisibleAsyncWithWaiting(LocatorWithXpath(FieldContainerBylabel, CommonLocators.CompletedIcon), 0);
        }

        protected async Task<bool> IsFieldLockedByLabelNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedByLabelNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldLockedByLabelNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator fieldContainer = await ValidateFormContextByLabelNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(fieldContainer, 0, 100, maxNumberOfScrolls);
            }
            return await IsVisibleAsyncWithWaiting(LocatorWithXpath(fieldContainer, CommonLocators.LockedIcon), 0);
        }

        protected async Task<ILocator> ValidateFormContextByLabelNameAsync(FormContextType formContextType, string field, int timeToCheckIfFrameExists)
        {
            ILocator fieldLocator = null;
            if (formContextType == FormContextType.QuickCreate)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, QuickCreateLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerByLabel.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Entity)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerByLabel.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.BusinessProcessFlow)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerByLabel.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Header)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, HeaderLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerByLabel.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Dialog)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, DialogLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerByLabel.Replace("[Name]", field));
            }
            return fieldLocator;
        }

        protected async Task<ILocator> ValidateFormContextBySchemaNameAsync(FormContextType formContextType, string field, int timeToCheckIfFrameExists)
        {
            ILocator fieldLocator = null;
            if (formContextType == FormContextType.QuickCreate)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, QuickCreateLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerBySchema.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Entity)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerBySchema.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.BusinessProcessFlow)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerBySchema.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Header)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, HeaderLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerBySchema.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Dialog)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, DialogLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainerBySchema.Replace("[Name]", field));
            }
            return fieldLocator;
        }

        protected async Task<ILocator> ReturnFormBySchemaNameAsync(FormContextType formContextType, int timeToCheckIfFrameExists)
        {
            ILocator formContainerLocator = null;
            if (formContextType == FormContextType.QuickCreate)
            {
                 formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, QuickCreateLocators.FormContainer, timeToCheckIfFrameExists);
            }
            else if (formContextType == FormContextType.Entity)
            {
                 formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, EntityLocators.FormContainer, timeToCheckIfFrameExists);
            }
            else if (formContextType == FormContextType.BusinessProcessFlow)
            {
                 formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.FormContainer, timeToCheckIfFrameExists);
            }
            else if (formContextType == FormContextType.Header)
            {
                 formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, HeaderLocators.FormContainer, timeToCheckIfFrameExists);
            }
            else if (formContextType == FormContextType.Dialog)
            {
                 formContainerLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, DialogLocators.FormContainer, timeToCheckIfFrameExists);
            }
            return formContainerLocator;
        }

        protected async Task SetValueBySchemaNameAsync(string field, string input, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(field, input, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValueBySchemaNameAsync(string field, string input, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            bool isValueSet = false;
            var completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            var inputTagLocator = Locator(completeFieldLocator, CommonLocators.TextBoxControl);
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                await ClickAsync(inputTagLocator);
                await ClearAsnyc(inputTagLocator);
                await PressSequentiallyAsync(inputTagLocator, input, new() { Delay = 100 });
                isValueSet = true;
                return;
            }

            var customControlInputTagLocator = Locator(completeFieldLocator, CommonLocators.CustomTextBoxControl);
            bool customControlInputTagLocatorDisplayed = await IsVisibleAsyncWithWaiting(customControlInputTagLocator, 0);
            if (customControlInputTagLocatorDisplayed)
            {
                await ClickAsync(customControlInputTagLocator);
                await ClearAsnyc(customControlInputTagLocator);
                await PressSequentiallyAsync(customControlInputTagLocator, input, new() { Delay = 100 });
                isValueSet = true;
                return;
            }
            var textareaTagLocator = Locator(completeFieldLocator, CommonLocators.TextAreaControl);
            bool isTextareaTagElementDisplayed = await IsVisibleAsyncWithWaiting(textareaTagLocator, 0, 1000);
            if (isTextareaTagElementDisplayed)
            {
                await ClickAsync(textareaTagLocator);
                await ClearAsnyc(textareaTagLocator);
                await PressSequentiallyAsync(textareaTagLocator, input, new() { Delay = 100 });
                isValueSet = true;
                return;
            }
            var richTextLocator = LocatorWithXpath(completeFieldLocator, "descendant::p");
            bool isRichTextLocatorDisplayed = await IsVisibleAsyncWithWaiting(richTextLocator, 0, 1000);
            if (isRichTextLocatorDisplayed)
            {
                await ClickAsync(richTextLocator);
                await ClearAsnyc(richTextLocator);
                await PressSequentiallyAsync(richTextLocator, input, new() { Delay = 100 });
                isValueSet = true;
                return;
            }

            if (!isValueSet)
            {
                throw new PlaywrightException($"{field} element is not found");
            }
        }

        protected async Task<string> GetValueBySchemaNameAsync(string field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValueBySchemaNameAsync(string field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            bool isValueGotten = false;
            string value = null;
            var completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            var inputTagLocator = Locator(completeFieldLocator, CommonLocators.TextBoxControl);
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(inputTagLocator);
            }
            var textareaTagLocator = Locator(completeFieldLocator, CommonLocators.TextAreaControl);
            bool isTextareaTagElementDisplayed = await IsVisibleAsyncWithWaiting(textareaTagLocator, 0, 1000);
            if (isTextareaTagElementDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(textareaTagLocator);
            }
            var richTextLocator = LocatorWithXpath(completeFieldLocator, "descendant::p");
            bool richTextLocatorDisplayed = await IsVisibleAsyncWithWaiting(richTextLocator, 0, 1000);
            if (richTextLocatorDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(richTextLocator);
            }
            if (!isValueGotten)
            {
                throw new PlaywrightException(field + " Element is not found");
            }
            return value;
        }

        protected async Task SetValueBySchemaNameAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            int index = lookupItem.Index;
            string value = lookupItem.Value;
            await SetValueBySchemaNameAsync(field, value, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
            ILocator lookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.LookupResults);
            lookUpItemsLocator = LocatorWithXpath(lookUpItemsLocator, "li");
            await SelectOptionAsync(lookUpItemsLocator, index);
        }

        protected async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValueBySchemaNameAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClearValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.CloseIconLookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(locator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(locator);
        }

        protected async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsLookupValueEmptyBySchemaAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsLookupValueEmptyBySchemaAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsLookupValueEmptyBySchemaAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.LookupInputBox);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(locator, 0, 100, maxNumberOfScrolls);
            }
            return (await TextContentAsync(locator))?.Length == 0;
        }

        protected async Task<string> GetValueBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.LookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(locator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(locator);
        }

        protected async Task SetValueBySchemaNameAsync(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownOpenerLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.DropdownValues);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(dropdownOpenerLocator, 0, 100, maxNumberOfScrolls);
            }
            await SelectDropdownOptionAsync(dropdownOpenerLocator, dropdownValuesLocator, value);
        }

        protected async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValueBySchemaNameAsync(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValueBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(dropdownLocator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(dropdownLocator);
        }

        protected async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.DropdownValues);
            return await GetAllAvailableChoicesBySchemaNameAsync(dropdownLocator, dropdownValuesLocator, dynamicallyLoaded, anyFieldInScroller, maxNumberOfScrolls);
        }

        protected async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValueBySchemaNameAsync(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        // This method does not work if the value(s) already selected. We will have to write new function.
        protected async Task SetValueBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            string[] values = multiSelectOptionSet.Values;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            ILocator dropdownOptionsOpenerLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.MultiSelectOptionSetOpener);
            ILocator dropdownValues = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.MultiSelectOptions);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(dropdownOptionsOpenerLocator, 0, 100, maxNumberOfScrolls);
            }
            await SelectMultiSelectOptionsAsync(dropdownOptionsOpenerLocator, dropdownValues, values);
            await ClickAsync(dropdownOptionsOpenerLocator);
        }

        protected async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValuesBySchemaNameAsync(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClearValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            string[] values = multiSelectOptionSet.Values;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }

            await HoverAsync(LocatorWithXpath(completeFieldLocator, CommonLocators.SelectedOptionsValueContainer), new LocatorHoverOptions { Force = true });
            foreach (string value in values)
            {
                ILocator dropdownOptionRemoveLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.RemoveOptionInMultiSelectOptionSet.Replace("[Name]", value));
                await ClickAsync(dropdownOptionRemoveLocator);
            }
        }

        protected async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValuesByLabelNameAsync(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetSelectedValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            ILocator FieldContainerBylabel = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(FieldContainerBylabel, 0, 100, maxNumberOfScrolls);
            }

            string allValues = await TextContentAsync(LocatorWithXpath(FieldContainerBylabel, CommonLocators.SelectedOptionsTextInMultiSelectOptionSet));
            string[] splittedValues = allValues.Split(",");
            List<string> outputValues = new List<string>();
            foreach (string value in splittedValues)
            {
                outputValues.Add(value.Trim());
            }
            return outputValues;
        }

        protected async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }

            ILocator dropdownOptionsOpenerLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.MultiSelectOptionSetOpener);
            await ClickAndWaitUntilLoadedAsync(dropdownOptionsOpenerLocator);
            return await GetAllElementsTextAfterWaitingAsync(Locator(CommonLocators.MultiSelectOptionsByName.Replace("[Name]", field)));
            await ClickAndWaitUntilLoadedAsync(dropdownOptionsOpenerLocator);
        }

        private async Task<int> GetFieldRequirementBySchemaNameAsync(string field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator FieldContainerBylabel = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(FieldContainerBylabel, 0, 100, maxNumberOfScrolls);
            }
            return int.Parse(await GetAttributeAsync(FieldContainerBylabel, "data-fieldrequirement"));
        }

        protected async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequiredBySchemaNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldBusinessRequiredBySchemaNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirementBySchemaNameAsync(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 1 || fieldRequirement == 2;
        }

        protected async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommendedBySchemaNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldBusinessRecommendedBySchemaNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirementBySchemaNameAsync(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 3;
        }

        protected async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptionalBySchemaNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldOptionalBySchemaNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirementBySchemaNameAsync(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 0;
        }

        protected async Task<bool> IsFieldCompletedBySchemaNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldCompletedBySchemaNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldCompletedBySchemaNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator FieldContainerBylabel = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(FieldContainerBylabel, 0, 100, maxNumberOfScrolls);
            }
            return await IsVisibleAsyncWithWaiting(LocatorWithXpath(FieldContainerBylabel, CommonLocators.CompletedIcon), 0);
        }

        protected async Task<bool> IsFieldLockedBySchemaNameAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLockedBySchemaNameAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldLockedBySchemaNameAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator fieldContainer = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(fieldContainer, 0, 100, maxNumberOfScrolls);
            }
            return await IsVisibleAsyncWithWaiting(LocatorWithXpath(fieldContainer, CommonLocators.LockedIcon), 0);
        }

        protected async Task ClickLookUpSearchIconBySchemaAsync(LookupItem lookup, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
           await ClickLookUpSearchIconBySchemaAsync(lookup, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClickLookUpSearchIconBySchemaAsync(LookupItem lookup, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = lookup.Name;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator lookupSearchIconLocator = Locator(completeFieldLocator, CommonLocators.LookupSearchIcon);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(lookupSearchIconLocator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(lookupSearchIconLocator);
        }

        protected async Task ClickLookUpSearchIconByLabelAsync(LookupItem lookup, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ClickLookUpSearchIconByLabelAsync(lookup, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClickLookUpSearchIconByLabelAsync(LookupItem lookup, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = lookup.Name;
            ILocator completeFieldLocator = await ValidateFormContextByLabelNameAsync(formContextType, name, timeToCheckIfFrameExists);
            ILocator lookupSearchIconLocator = Locator(completeFieldLocator, CommonLocators.LookupSearchIcon);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerByLabel.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(lookupSearchIconLocator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(lookupSearchIconLocator);
        }

        public async Task ClickLookUpAddNewBtnAsync()
        {
            ILocator lookupAddNewButtonLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.LookupAddNewButton);
            await ClickAsync(lookupAddNewButtonLocator);
        }

        public async Task ClickLookupRecordTypeAsync(string recordType)
        {
            ILocator lookupRecordTypeLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.LookupRecordType.Replace("[Name]", recordType));
            await ClickAsync(lookupRecordTypeLocator);
        }

        public async Task ClickSelectedRecordInLookupBySchemaAysnc(LookupItem lookupItem)
        {
            ILocator fieldContainer = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", lookupItem.Name));
            ILocator selectedLookupLocator = Locator(fieldContainer, CommonLocators.SelectedRecordType);
            await ClickAsync(selectedLookupLocator);
        }

        protected async Task<bool> IsBooleanFieldToggledOnBySchemaAsync(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsBooleanFieldToggledOnBySchemaAsync(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsBooleanFieldToggledOnBySchemaAsync(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator FieldContainerBylabel = await ValidateFormContextBySchemaNameAsync(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(FieldContainerBylabel, 0, 100, maxNumberOfScrolls);
            }
            return await IsVisibleAsyncWithWaiting(LocatorWithXpath(FieldContainerBylabel, CommonLocators.ToggledCheckbox), 0);
        }

        protected async Task ToggleBooleanFieldOnBySchemaAsync(string schemaName, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ToggleBooleanFieldOnBySchemaAsync(schemaName, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ToggleBooleanFieldOnBySchemaAsync(string schemaName, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, schemaName, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.UntoggledCheckbox);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(locator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(locator);
        }

        protected async Task ToggleBooleanFieldOffBySchemaAsync(string schemaName, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ToggleBooleanFieldOffBySchemaAsync(schemaName, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ToggleBooleanFieldOffBySchemaAsync(string schemaName, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, schemaName, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.ToggledCheckbox);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(locator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(locator);
        }

        protected async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            string schemaName = lookupItem.Name;
            string optionName = lookupItem.OptionName;
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, schemaName, timeToCheckIfFrameExists);
            ILocator searchIcon = LocatorWithXpath(completeFieldLocator, CommonLocators.LookupSearchIcon);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(searchIcon, 0, 100, maxNumberOfScrolls);
            }
            await ClickAndWaitUntilLoadedAsync(searchIcon);
            ILocator lookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.LookupResults);
            lookUpItemsLocator = LocatorWithXpath(lookUpItemsLocator, CommonLocators.AllLookupValuesByTable.Replace("[Name]", optionName));
            return await GetAllElementsTextAfterWaitingAsync(lookUpItemsLocator);
        }

        protected async Task<List<string>> GetAllAvailableValuesBySchemaNameAsync(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValuesBySchemaNameAsync(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task AssertElementToBeVisibleBySchemaAsync(string fieldContainer, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, fieldContainer, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            await AssertElementVisible(completeFieldLocator);
        }

        protected async Task AssertElementToBeVisibleBySchemaAsync(string fieldContainer, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await AssertElementToBeVisibleBySchemaAsync(fieldContainer, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task AssertElementNotToBeVisibleBySchemaAsync(string fieldContainer, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator completeFieldLocator = await ValidateFormContextBySchemaNameAsync(formContextType, fieldContainer, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            await AssertElementNotVisible(completeFieldLocator);
        }

        protected async Task AssertElementNotToBeVisibleBySchemaAsync(string fieldContainer, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await AssertElementNotToBeVisibleBySchemaAsync(fieldContainer, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetAllNotificationsAsync(FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllNotificationsAsync(false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetAllNotificationsAsync(bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator formLocator = await ReturnFormBySchemaNameAsync(formContextType, timeToCheckIfFrameExists);
            ILocator allNotificationsTextLocator = LocatorWithXpath(formLocator, NotificationLocators.AllNotificationText);
            ILocator notificationExpandIconLocator = LocatorWithXpath(formLocator, NotificationLocators.ExpandIcon);
            ILocator notificationContractIconLocator = LocatorWithXpath(formLocator, NotificationLocators.ContractIcon);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(allNotificationsTextLocator, 0, 100, maxNumberOfScrolls);
            }

            if(await IsVisibleAsyncWithWaiting(notificationExpandIconLocator, 0, 2000) || await IsVisibleAsyncWithWaiting(notificationContractIconLocator, 0, 2000))
            {
                allNotificationsTextLocator = LocatorWithXpath(formLocator, NotificationLocators.AllNotificationWithFormAfterExpanding);
            }
            bool isNotificationPanelExpanded = false;
            if (await IsVisibleAsyncWithWaiting(notificationExpandIconLocator, 0))
            {
                isNotificationPanelExpanded = true;
                await ExpandNotificationAsync(formContextType, timeToCheckIfFrameExists);
            }
            List<string> allNotificationsText = await GetAllElementsTextAfterWaitingAsync(allNotificationsTextLocator);
            if (isNotificationPanelExpanded)
            {
                await ContractNotificationAsync(formContextType, timeToCheckIfFrameExists);
            }
            return allNotificationsText;
        }

        protected async Task<int> GetNumberOfNotificationsAsync(FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetNumberOfNotificationsAsync(false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<int> GetNumberOfNotificationsAsync(bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            List<string> notifications = await GetAllNotificationsAsync(dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
            return notifications.Count;
        }

        protected async Task<string> GetNotificationTextAsync(int index, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetNotificationTextAsync(index, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetNotificationTextAsync(int index, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            List<string> notifications = await GetAllNotificationsAsync(dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldInScroller, maxNumberOfScrolls);
            return notifications[index];
        }

        protected async Task ExpandNotificationAsync(FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ExpandNotificationAsync(false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ExpandNotificationAsync(bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator formLocator = await ReturnFormBySchemaNameAsync(formContextType, timeToCheckIfFrameExists);
            ILocator expandNotificationIcon = LocatorWithXpath(formLocator, NotificationLocators.ExpandIcon);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(expandNotificationIcon, 0, 100, maxNumberOfScrolls);
            }

            await ClickAsync(expandNotificationIcon);
        }

        protected async Task ContractNotificationAsync(FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ContractNotificationAsync(false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ContractNotificationAsync(bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator formLocator = await ReturnFormBySchemaNameAsync(formContextType, timeToCheckIfFrameExists);
            ILocator contractNotificationIcon = LocatorWithXpath(formLocator, NotificationLocators.ContractIcon);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainerBySchema.Replace("[Name]", anyFieldInScroller)));
                await ScrollUsingMouseUntilElementIsVisibleAsync(contractNotificationIcon, 0, 100, maxNumberOfScrolls);
            }

            await ClickAsync(contractNotificationIcon);
        }

    }
}
