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
            bool isValueSet = false;
            var locator = GetFieldWithXpath(fieldName);
            var inputTagLocator = LocatorWithXpath(locator, "descendant::input");
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                await FillAsync(inputTagLocator, input);
                isValueSet = true;
                return;
            }
            var textareaTagLocator = LocatorWithXpath(locator, "descendant::textarea");
            bool isTextareaTagElementDisplayed = await IsVisibleAsyncWithWaiting(textareaTagLocator, 0);
            if (isTextareaTagElementDisplayed)
            {
                await FillAsync(textareaTagLocator, input);
                isValueSet = true;
                return;
            }

            if (!isValueSet)
            {
                throw new PlaywrightException(fieldName+" Element is not found");
            }
        }

        public async Task ClearValue(string fieldName)
        {
            await SetValue(fieldName, string.Empty);
        }

        public async Task<string> GetValue(string fieldName)
        {
            bool isValueGotten = false;
            string value = null;
            var locator = GetFieldWithXpath(fieldName);
            var inputTagLocator = LocatorWithXpath(locator, "descendant::input");
            bool isInputTagElementDisplayed = await IsVisibleAsyncWithWaiting(inputTagLocator, 0);
            if (isInputTagElementDisplayed)
            {
                isValueGotten = true;
                value =  await InputValueAsync(inputTagLocator);

            }
            var textareaTagLocator = LocatorWithXpath(locator, "descendant::textarea");
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

        public async Task SetValue(LookupItem lookupItem)
        {
            string field = lookupItem.Name;
            int index = lookupItem.Index;
            string value = lookupItem.Value;
            //await HoverAsync(Locator("//h2[text()='ACCOUNT INFORMATION']"));
            //await ScrollUsingMouseUntilElementIsVisible(field, 0, 100, 10);
            await SetValue(field, value);
            ILocator locator = LocatorWithXpath(LocatorWithXpath(CommonLocators.LookupResults), "li");
            await SelectOption(locator, index);
        }

        public async Task SetValue(OptionSet optionSet)
        {
            string name = optionSet.Name;
            string value = optionSet.Value;
            ILocator dropdownOpenerLocator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", name)), CommonLocators.DropdownOpener);
            ILocator dropdownValuesLocator = Locator(CommonLocators.DropdownValues);
            await SelectDropdownOption(dropdownOpenerLocator, dropdownValuesLocator, value);
        }

        public async Task ClearValue(LookupItem lookupItem)
        {
            string field = lookupItem.Name;
            ILocator locator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", field)), CommonLocators.CloseIconLookupValue);
            await ClickAsync(locator);
        }

        public async Task<string> GetValue(LookupItem lookupItem)
        {
            string field = lookupItem.Name;
            ILocator locator = LocatorWithXpath(LocatorWithXpath(CommonLocators.FieldContainer.Replace("[Name]", field)), CommonLocators.LookupValue);
            return await TextContentAsync(locator);
        }

        public async Task ClickButton(string buttonName)
        {
            await ClickAsync(Locator(CommonLocators.Button.Replace("[Name]", buttonName)));
        }
    }
}
