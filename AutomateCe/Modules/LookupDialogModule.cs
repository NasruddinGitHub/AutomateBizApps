using AutomateCe.Controls;
using AutomateCe.Enums;
using AutomateCe.Pages;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class LookupDialogModule : BaseModule
    {
        private IPage _page;

        public LookupDialogModule(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task TypeValue(string value)
        {
            await FillAsync(Locator(Locator(LookupDialogLocators.Root), LookupDialogLocators.SearchBox), value);
        }

        public async Task SetValueByIndexAsync(LookupItem lookupItem)
        {
            string[] values = lookupItem.Values;
            int index = lookupItem.Index;
            foreach (string value in values)
            {
                await TypeValue(value);
                await WaitUntilAppIsIdleAsync();
                ILocator noLookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.NoLookupResults);
                if (await IsVisibleAsyncWithWaiting(noLookUpItemsLocator, 0, 1000))
                {
                    throw new ArgumentException($"Given {value} is not available in the lookup.");
                }
                ILocator lookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.LookupResults);
                lookUpItemsLocator = LocatorWithXpath(lookUpItemsLocator, "li");
                await SelectOptionAsync(lookUpItemsLocator, index);
            }
        }

        public async Task SetValueByOptionAsync(LookupItem lookupItem)
        {
            
            string[] values = lookupItem.Values;
            string optionName = lookupItem.OptionName;
            foreach (string value in values)
            {
                await TypeValue(value);
                await WaitUntilAppIsIdleAsync();
                ILocator noLookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.NoLookupResults);
                if (await IsVisibleAsyncWithWaiting(noLookUpItemsLocator, 0, 1000))
                {
                    throw new ArgumentException($"Given {value} is not available in the lookup.");
                }
                ILocator lookUpItemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommonLocators.LookupResults);
                lookUpItemsLocator = LocatorWithXpath(lookUpItemsLocator, LookupDialogLocators.AllDialogLookupValues.Replace("[Name]", optionName));
                await SelectOptionAsync(lookUpItemsLocator, value);
            }
        }

        public async Task ClickAddAsync()
        {
            await ClickAndWaitUntilLoadedAsync(Locator(Locator(LookupDialogLocators.Root), CommonLocators.Button.Replace("[Name]", "Add")));
        }

        public async Task ClickCancelAsync()
        {
            await ClickAndWaitUntilLoadedAsync(Locator(Locator(LookupDialogLocators.Root), CommonLocators.Button.Replace("[Name]", "Cancel")));
        }

        public async Task ClickNewAsync()
        {
            await ClickAndWaitUntilLoadedAsync(Locator(Locator(LookupDialogLocators.Root), LookupDialogLocators.NewButton));
        }

        public async Task<List<string>> GetAllSelectedValues()
        {
            return await GetAllElementsTextAfterWaitingAsync(Locator(Locator(LookupDialogLocators.Root), LookupDialogLocators.SelectedLookupValues));
        }

        public async Task ClearAllSelectedValues()
        {
            await GetAllElementsAfterWaitingAsync(Locator(Locator(LookupDialogLocators.Root), LookupDialogLocators.SelectedLookupValuesCrossIcon));
        }

    }
}
