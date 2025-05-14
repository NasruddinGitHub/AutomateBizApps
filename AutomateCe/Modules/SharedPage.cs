using AutomateCe.Controls;
using AutomateCe.Enums;
using AutomateCe.Modules;
using AutomateCe.Pages;
using Microsoft.Playwright;
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

    }
}
