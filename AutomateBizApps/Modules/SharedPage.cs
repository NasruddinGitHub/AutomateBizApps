using AutomateBizApps.ObjectRepository;
using AutomateBizApps.Pages;
using AutomateCe.Controls;
using AutomateCe.Enums;
using AutomateCe.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateBizApps.Modules
{
    public class SharedPage : BaseModule
    {
        private IPage _page;

        public SharedPage(IPage page) : base(page)
        {
            this._page = page;
        }
        protected async Task WaitUntilSpinnerNotDisplayed()
        {
            var spinnerLocator = Locator(GridLocators.Spinner);
            await WaitUntilElementNotVisbleIfVisible(spinnerLocator, 3000, 5000);
        }

        protected ILocator GetFieldWithXpath(string fieldName)
        {
            return LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", fieldName));
        }

        protected ILocator GetField(string fieldName)
        {
            return Locator(CommonLocators.FieldContainer.Replace("[Name]", fieldName));
        }
        protected async Task ClickButton(string buttonName)
        {
            await ClickAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.Button.Replace("[Name]", buttonName)));
        }

        protected async Task SetValue(string fieldName, string input, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(fieldName, input, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValue(string fieldName, string input, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            bool isValueSet = false;
            var completeFieldLocator = await ValidateFormContext(formContextType, fieldName, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            var inputTagLocator = LocatorWithXpath(completeFieldLocator, "descendant::input");
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                await FillAsync(inputTagLocator, input);
                isValueSet = true;
                return;
            }
            var textareaTagLocator = LocatorWithXpath(completeFieldLocator, "descendant::textarea");
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

        protected async Task<string> GetValue(string fieldName, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(fieldName, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValue(string fieldName, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            bool isValueGotten = false;
            string value = null;
            var completeFieldLocator = await ValidateFormContext(formContextType, fieldName, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            var inputTagLocator = LocatorWithXpath(completeFieldLocator, "descendant::input");
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(inputTagLocator);
            }
            var textareaTagLocator = LocatorWithXpath(completeFieldLocator, "descendant::textarea");
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

        protected async Task SetValue(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValue(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            int index = lookupItem.Index;
            string value = lookupItem.Value;
            await SetValue(field, value, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            ILocator lookUpItemsLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.LookupResults);
            lookUpItemsLocator = LocatorWithXpath(lookUpItemsLocator, "li");
            await SelectOption(lookUpItemsLocator, index);
        }

        protected async Task ClearValue(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValue(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClearValue(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator completeFieldLocator = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.CloseIconLookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(locator);
        }

        protected async Task<string> GetValue(LookupItem lookupItem, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(lookupItem, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValue(LookupItem lookupItem, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator completeFieldLocator = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            ILocator locator = LocatorWithXpath(completeFieldLocator, CommonLocators.LookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(locator);
        }

        protected async Task SetValue(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task SetValue(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContext(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownOpenerLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.DropdownValues);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(dropdownOpenerLocator, 0, 100, maxNumberOfScrolls);
            }
            await SelectDropdownOption(dropdownOpenerLocator, dropdownValuesLocator, value);
        }

        protected async Task<string> GetValue(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetValue(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<string> GetValue(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContext(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(dropdownLocator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(dropdownLocator);
        }

        protected async Task<List<string>> GetAllAvailableValues(OptionSet optionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetAllAvailableValues(optionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetAllAvailableValues(OptionSet optionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator completeFieldLocator = await ValidateFormContext(formContextType, name, timeToCheckIfFrameExists);
            ILocator dropdownLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.DropdownValues);
            return await GetAllAvailableChoices(dropdownLocator, dropdownValuesLocator, dynamicallyLoaded, anySelectorInScroller, maxNumberOfScrolls);
        }

        protected async Task SetValue(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await SetValue(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        // This method does not work if the value(s) already selected. We will have to write new function.
        protected async Task SetValue(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            string[] values = multiSelectOptionSet.Values;
            ILocator completeFieldLocator = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            ILocator dropdownOptionsOpenerLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.MultiSelectOptionSetOpener);
            ILocator dropdownValues = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.MultiSelectOptions);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(dropdownOptionsOpenerLocator, 0, 100, maxNumberOfScrolls);
            }
            await SelectMultiSelectOptions(dropdownOptionsOpenerLocator, dropdownValues, values);
            await ClickAsync(dropdownOptionsOpenerLocator);
        }

        protected async Task ClearValues(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            await ClearValues(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task ClearValues(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            string[] values = multiSelectOptionSet.Values;
            ILocator completeFieldLocator = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }

            await HoverAsync(LocatorWithXpath(completeFieldLocator, CommonLocators.SelectedOptionsValueContainer), new LocatorHoverOptions { Force = true });
            foreach (string value in values)
            {
                ILocator dropdownOptionRemoveLocator = LocatorWithXpath(completeFieldLocator, CommonLocators.RemoveOptionInMultiSelectOptionSet.Replace("[Name]", value));
                await ClickAsync(dropdownOptionRemoveLocator);
            }
        }

        protected async Task<List<string>> GetSelectedValues(MultiSelectOptionSet multiSelectOptionSet, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await GetSelectedValues(multiSelectOptionSet, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<List<string>> GetSelectedValues(MultiSelectOptionSet multiSelectOptionSet, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = multiSelectOptionSet.Name;
            ILocator fieldContainer = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(fieldContainer, 0, 100, maxNumberOfScrolls);
            }

            string allValues = await TextContentAsync(LocatorWithXpath(fieldContainer, CommonLocators.SelectedOptionsTextInMultiSelectOptionSet));
            string[] splittedValues = allValues.Split(",");
            List<string> outputValues = new List<string>();
            foreach (string value in splittedValues)
            {
                outputValues.Add(value.Trim());
            }
            return outputValues;
        }

        private async Task<int> GetFieldRequirement(string field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator fieldContainer = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(fieldContainer, 0, 100, maxNumberOfScrolls);
            }
            return int.Parse(await GetAttributeAsync(fieldContainer, "data-fieldrequirement"));
        }

        protected async Task<bool> IsFieldBusinessRequired(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRequired(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldBusinessRequired(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            
            int fieldRequirement = await GetFieldRequirement(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 2;
        }

        protected async Task<bool> IsFieldBusinessRecommended(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldBusinessRecommended(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldBusinessRecommended(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirement(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 3;
        }

        protected async Task<bool> IsFieldOptional(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldOptional(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldOptional(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            int fieldRequirement = await GetFieldRequirement(field, dynamicallyLoaded, formContextType, timeToCheckIfFrameExists, anyFieldNameInScroller, maxNumberOfScrolls);
            return fieldRequirement == 0;
        }

        protected async Task<bool> IsFieldCompleted(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldCompleted(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldCompleted(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator fieldContainer = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(fieldContainer, 0, 100, maxNumberOfScrolls);
            }
            return await IsVisibleAsyncWithWaiting(LocatorWithXpath(fieldContainer, CommonLocators.CompletedIcon), 0);
        }

        protected async Task<bool> IsFieldLocked(String field, FormContextType formContextType, int timeToCheckIfFrameExists = 1000)
        {
            return await IsFieldLocked(field, false, formContextType, timeToCheckIfFrameExists);
        }

        protected async Task<bool> IsFieldLocked(String field, bool dynamicallyLoaded, FormContextType formContextType, int timeToCheckIfFrameExists = 1000, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            ILocator fieldContainer = await ValidateFormContext(formContextType, field, timeToCheckIfFrameExists);
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(fieldContainer, 0, 100, maxNumberOfScrolls);
            }
            return await IsVisibleAsyncWithWaiting(LocatorWithXpath(fieldContainer, CommonLocators.LockedIcon), 0);
        }

        protected async Task<ILocator> ValidateFormContext(FormContextType formContextType, string field, int timeToCheckIfFrameExists)
        {
            ILocator fieldLocator = null;
            if (formContextType == FormContextType.QuickCreate)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, QuickCreateLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainer.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Entity)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainer.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.BusinessProcessFlow)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, BusinessProcessFlowLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainer.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Header)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, HeaderLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainer.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.Dialog)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, DialogLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainer.Replace("[Name]", field));
            }
            return fieldLocator;
        }
    }
}
