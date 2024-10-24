using AutomateBizApps.Pages;
using AutomateCe.Controls;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task WaitUntilSpinnerNotDisplayed()
        {
            var spinnerLocator = Locator(GridLocators.Spinner);
            await WaitUntilElementNotVisbleIfVisible(spinnerLocator, 3000, 5000);
        }

        public ILocator GetFieldWithXpath(string fieldName)
        {
            return LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", fieldName));
        }

        public ILocator GetField(string fieldName)
        {
            return Locator(CommonLocators.FieldContainer.Replace("[Name]", fieldName));
        }

        public async Task SetValue(string fieldName, string input)
        {
            await SetValue(fieldName, input, false);
        }

        public async Task SetValue(string fieldName, string input, bool dynamicallyLoaded, string? anySelectorInScroller=null, int maxNumberOfScrolls=0)
        {
            bool isValueSet = false;
            var completeFieldLocator = GetFieldWithXpath(fieldName);
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
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
            bool isTextareaTagElementDisplayed = await IsVisibleAsyncWithWaiting(textareaTagLocator, 0);
            if (isTextareaTagElementDisplayed)
            {
                await FillAsync(textareaTagLocator, input);
                isValueSet = true;
                return;
            }

            if (!isValueSet)
            {
                throw new PlaywrightException($"{fieldName} element is not found");
            }
        }

        public async Task ClearValue(string fieldName, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SetValue(fieldName, string.Empty, dynamicallyLoaded, anySelectorInScroller, maxNumberOfScrolls);
        }

        public async Task ClearValue(string fieldName)
        {
            await SetValue(fieldName, string.Empty);
        }

        public async Task<string> GetValue(string fieldName, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            bool isValueGotten = false;
            string value = null;
            var completeFieldLocator = GetFieldWithXpath(fieldName);
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(completeFieldLocator, 0, 100, maxNumberOfScrolls);
            }
            var inputTagLocator = LocatorWithXpath(completeFieldLocator, "descendant::input");
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                isValueGotten = true;
                value =  await InputValueAsync(inputTagLocator);

            }
            var textareaTagLocator = LocatorWithXpath(completeFieldLocator, "descendant::textarea");
            bool isTextareaTagElementDisplayed = await IsVisibleAsyncWithWaiting(textareaTagLocator, 0);
            if (isTextareaTagElementDisplayed)
            {
                isValueGotten = true;
                value = await InputValueAsync(textareaTagLocator);
            }

            if (!isValueGotten)
            {
                throw new PlaywrightException(fieldName + " Element is not found");
            }
            return value;
        }

        public async Task<string> GetValue(string fieldName)
        {
            return await GetValue(fieldName, false);
        }

        public async Task SetValue(LookupItem lookupItem, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            int index = lookupItem.Index;
            string value = lookupItem.Value;
            await SetValue(field, value, dynamicallyLoaded, anySelectorInScroller, maxNumberOfScrolls);
            ILocator locator = LocatorWithXpath(LocatorWithXpath(CommonLocators.LookupResults), "li");
            await SelectOption(locator, index);
        }

        public async Task SetValue(LookupItem lookupItem)
        {
            await SetValue(lookupItem, false);
        }

        public async Task ClearValue(LookupItem lookupItem)
        {
            await ClearValue(lookupItem, false);
        }

        public async Task ClearValue(LookupItem lookupItem, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator locator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", field)), CommonLocators.CloseIconLookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(locator);
        }

        public async Task<string?> GetValue(LookupItem lookupItem, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string field = lookupItem.Name;
            ILocator locator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", field)), CommonLocators.LookupValue);
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(locator);
        }

        public async Task<string?> GetValue(LookupItem lookupItem)
        {
            return await GetValue(lookupItem, false);
        }

        public async Task SetValue(OptionSet optionSet, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator dropdownOpenerLocator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", name)), CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = Locator(CommonLocators.DropdownValues);
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(dropdownOpenerLocator, 0, 100, maxNumberOfScrolls);
            }
            await SelectDropdownOption(dropdownOpenerLocator, dropdownValuesLocator, value);
        }

        public async Task SetValue(OptionSet optionSet)
        {
            await SetValue(optionSet, false);
        }

        public async Task<string> GetCurrentlySelectedValue(OptionSet optionSet, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator dropdownLocator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", name)), CommonLocators.DropdownOpener);
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(dropdownLocator, 0, 100, maxNumberOfScrolls);
            }
            return await TextContentAsync(dropdownLocator);
        }

        public async Task<string> GetCurrentlySelectedValue(OptionSet optionSet)
        {
            return await GetCurrentlySelectedValue(optionSet, false);
        }

        public async Task<List<string>> GetAllAvailableChoices(OptionSet optionSet, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator dropdownLocator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", name)), CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = Locator(CommonLocators.DropdownValues);
            return await GetAllAvailableChoices(dropdownLocator, dropdownValuesLocator, dynamicallyLoaded, anySelectorInScroller, maxNumberOfScrolls);
        }

        public async Task<List<string>> GetAllAvailableChoices(OptionSet optionSet)
        {
            return await GetAllAvailableChoices(optionSet, false);
        }

        public async Task ClickButton(string buttonName)
        {
            await ClickAsync(Locator(CommonLocators.Button.Replace("[Name]", buttonName)));
        }
    }
}
