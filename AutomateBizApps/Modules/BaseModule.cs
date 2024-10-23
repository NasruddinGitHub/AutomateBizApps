using Microsoft.Playwright;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateBizApps.Pages
{
    public class BaseModule : PageTest
    {
        private IPage _page;

        ILocator First { get; }

        ILocator Last { get; }
        public BaseModule(IPage page)
        {
            this._page = page;
        }

        public ILocator GetByAltText(string text)
        {
            return _page.GetByAltText(text);
        }

        public ILocator GetByLabel(string text)
        {
            return _page.GetByLabel(text);
        }

        public ILocator GetByPlaceholder(string text)
        {
            return _page.GetByPlaceholder(text);
        }

        public ILocator GetByRole(AriaRole ariaRole, PageGetByRoleOptions? options = default)
        {
            return _page.GetByRole(ariaRole, options);
        }

        public ILocator GetByTestId(string testId)
        {
            return _page.GetByTestId(testId);
        }
        public ILocator GetByText(string text, PageGetByTextOptions? options = default)
        {
            return _page.GetByText(text, options);
        }
        public ILocator GetByTitle(string text, PageGetByTitleOptions? options = default)
        {
            return _page.GetByTitle(text, options);
        }
        public ILocator Locator(string selector, PageLocatorOptions? options = default)
        {
            return _page.Locator(selector, options);
        }

        public ILocator LocatorWithXpath(string selector, PageLocatorOptions? options = default)
        {
            return _page.Locator($"xpath={selector}", options);
        }

        public ILocator LocatorWithXpath(ILocator locator, string selector, LocatorLocatorOptions? options = default)
        {
            return locator.Locator($"xpath={selector}", options);
        }

        public ILocator Locator(ILocator locator, string selector, LocatorLocatorOptions? options = default)
        {
            return locator.Locator(selector, options);
        }

        public async Task<string?> GetAttributeAsync(string selector, string name, PageGetAttributeOptions? options = default)
        {
            return await _page.GetAttributeAsync(selector, name, options);
        }

        public async Task<string?> GetAttributeAsync(ILocator locator, string name, LocatorGetAttributeOptions? options = default)
        {
            return await locator.GetAttributeAsync(name, options);
        }

        public ILocator SwitchToFrameAndLocate(string frameSelector, string locatorSelector, FrameLocatorLocatorOptions? options = default)
        {
            return _page.FrameLocator(frameSelector).Locator(locatorSelector, options);
        }

        public IFrame? Frame(string selector)
        {
            return _page.Frame(selector);
        }

        public IFrame? FrameByUrl(string url)
        {
            return _page.FrameByUrl(url);
        }

        public IFrame? FrameByUrl(Regex url)
        {
            return _page.FrameByUrl(url);
        }

        public async Task ClickAsync(ILocator locatorToClick, LocatorClickOptions? options = default)
        {
            await ScrollUsingMouseUntilElementIsVisible(locatorToClick, 0, 100, 50);
            await locatorToClick.ClickAsync(options);
        }
        public async Task DoubleClickAsync(ILocator locator, LocatorDblClickOptions? options = default)
        {
            await locator.DblClickAsync(options);
        }

        public async Task CheckAsync(ILocator locator, LocatorCheckOptions? options = default)
        {
            await locator.CheckAsync(options);
        }

        public async Task<int> CountAsync(ILocator locator)
        {
            return await locator.CountAsync();
        }

        //public async Task LocateAllElements(ILocator locator)
        //{
        //    _page.
        //}

        public async Task HoverAsync(ILocator locator, LocatorHoverOptions? options = default)
        {
            await locator.HoverAsync(options);
        }

        public async Task HoverAndClickAsync(ILocator locator, LocatorHoverOptions? options1 = default, LocatorClickOptions? options2 = default)
        {
            await locator.HoverAsync(options1);
            await ClickAsync(locator, options2);
        }

        public async Task FillAsync(ILocator locator, string value, LocatorFillOptions? options = default)
        {
            await locator.FillAsync(value, options);
        }
        public async Task<IPage> Page(ILocator locator)
        {
            return locator.Page;
        }

        //public async Task<ILocator> First(ILocator locator)
        //{
        //    return locator.First;
        //}

        //public async Task<ILocator> Last(ILocator locator)
        //{
        //    return locator.Last;
        //}

        public ILocator Nth(ILocator locator, int index)
        {
            return locator.Nth(index);
        }

        public async Task UncheckAsync(ILocator locator, LocatorUncheckOptions? options = default)
        {
            await locator.UncheckAsync(options);
        }

        public async Task TypeAsync(ILocator locator, string text, LocatorTypeOptions? options = default)
        {
            await locator.TypeAsync(text, options);
        }

        public async Task<bool> IsCheckedAsync(ILocator locator, LocatorIsCheckedOptions? options = default)
        {
            return await locator.IsCheckedAsync(options);
        }

        public async Task<bool> IsDisabledAsync(ILocator locator, LocatorIsDisabledOptions? options = default)
        {
            return await locator.IsDisabledAsync(options);
        }

        public async Task<bool> IsEditableAsync(ILocator locator, LocatorIsEditableOptions? options = default)
        {
            return await locator.IsEditableAsync(options);
        }

        public async Task<bool> IsEnabledAsync(ILocator locator, LocatorIsEnabledOptions? options = default)
        {
            return await locator.IsEnabledAsync(options);
        }

        public async Task<bool> IsHiddenAsync(ILocator locator, LocatorIsHiddenOptions? options = default)
        {
            return await locator.IsHiddenAsync(options);
        }

        public async Task<bool> IsVisibleAsync(ILocator locator, LocatorIsVisibleOptions? options = default)
        {
            return await locator.IsVisibleAsync(options);
        }

        public async Task ScrollIntoViewIfNeededAsync(ILocator locator, LocatorScrollIntoViewIfNeededOptions? options = default)
        {
            await locator.ScrollIntoViewIfNeededAsync(options);
        }

        public async Task SelectOptionAsync(ILocator locator, string values, LocatorSelectOptionOptions? options = default)
        {
            await locator.SelectOptionAsync(values, options);
        }

        public async Task ScreenshotAsync(ILocator locator, LocatorScreenshotOptions? options = default)
        {
            await locator.ScreenshotAsync(options);
        }

        public async void WaitForAsync(ILocator locator, LocatorWaitForOptions? options = default)
        {
            await locator.WaitForAsync(options);
        }

        public void InputValueAsync(ILocator locator, LocatorInputValueOptions? options = default)
        {
            locator.InputValueAsync(options);
        }

        public ILocator Filter(ILocator locator, LocatorFilterOptions? options = default)
        {
            return locator.Filter(options);
        }

        public async Task HighlightAsync(ILocator locator)
        {
            await locator.HighlightAsync();
        }

        public async Task<string> TextContentAsync(ILocator locator)
        {
            return locator.TextContentAsync().Result.Trim();
        }

        public async Task<string> InputValueAsync(ILocator locator)
        {
            return await locator.InputValueAsync();
        }

        // This will not wait until first element is visible
        public async Task<List<string>> GetAllElementsText(ILocator locator)
        {
            var allElementsText = new List<string?>();
            var allElementsCount = await CountAsync(locator);
            for (int i = 0; i < allElementsCount; i++)
            {
                allElementsText.Add(await TextContentAsync(locator.Nth(i)));
            }
            return allElementsText;
        }

        // This will wait until first element is visible
        public async Task<List<string>> GetAllElementsTextAfterWaiting(ILocator locator, int waitTime = 10000)
        {
            var allElementsText = new List<string?>();
            await ToBeVisibleAsync(locator, 0, waitTime);
            var allElementsCount = await CountAsync(locator);
            for (int i = 0; i < allElementsCount; i++)
            {
                allElementsText.Add(await TextContentAsync(locator.Nth(i)));
            }
            return allElementsText;
        }

        public void ThinkTime(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public async Task ToBeVisibleAsync(ILocator locator, int index, int visibleTimeout = 5000)
        {
            await Expect(locator.Nth(index)).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
        }

        public async Task ToBeNotVisibleAsync(ILocator locator, int index, int visibleTimeout = 5000)
        {
            await Expect(locator.Nth(index)).Not.ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
        }

        public async Task<bool> IsVisibleAsyncWithWaiting(ILocator locator, int index, int visibleTimeout = 5000)
        {
            try
            {
                await Expect(locator.Nth(index)).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
                return true;
            }
            catch (PlaywrightException ex)
            {
                // Console.WriteLine(ex.ToString()+" Timeout exception is thrown.");
                // Log something
            }
            return false;
        }

        public async Task WaitUntilElementNotVisbleIfVisible(ILocator locator, int waitTimeToVisble, int waitTimeNotToVisble)
        {
            try
            {
                await ToBeVisibleAsync(locator, 0, waitTimeToVisble);
                await ToBeNotVisibleAsync(locator, 0, waitTimeNotToVisble);
            }
            catch (PlaywrightException ex)
            {
                // Element is not visible
                // Log the message
            }
        }

        public async Task GoBackAsync(PageGoBackOptions pageGoBackOptions)
        {
            await _page.GoBackAsync(pageGoBackOptions);
        }

        public async Task GoForwardAsync(PageGoForwardOptions pageGoForwardOptions)
        {
            await _page.GoForwardAsync(pageGoForwardOptions);
        }

        public async Task ReloadAsync(PageReloadOptions pageReloadOptions)
        {
            await _page.ReloadAsync(pageReloadOptions);
        }

        public string GetTotp(string mfaKey)
        {
            byte[] base32Bytes = Base32Encoding.ToBytes(mfaKey);

            var totp = new Totp(base32Bytes);
            return totp.ComputeTotp();
        }

        public async Task KeyBoardDownAsync(string key)
        {
            await _page.Keyboard.DownAsync(key);
        }

        public async Task KeyBoardUpAsync(string key)
        {
            await _page.Keyboard.UpAsync(key);
        }

        public async Task KeyboardTypeAsync(string key, KeyboardTypeOptions? options = default)
        {
            await _page.Keyboard.TypeAsync(key, options);
        }

        public async Task KeyboardPressAsync(string key, KeyboardPressOptions? options = default)
        {
            await _page.Keyboard.PressAsync(key, options);
        }

        public async Task KeyboardInsertTextAsync(string key)
        {
            await _page.Keyboard.InsertTextAsync(key);
        }

        public async Task PressKeyboardKeyAsync(string key, int noOfTimes, KeyboardPressOptions? options = default)
        {
            for (int i = 0; i < noOfTimes; i++)
                await _page.Keyboard.PressAsync(key, options);
        }

        public async Task SelectOption(string options, string optionToSelect)
        {
            var optionsLocator = Locator(options);
            await ToBeVisibleAsync(optionsLocator, 0);
            var allElementsCount = await CountAsync(Locator(options));
            for (int i = 0; i < allElementsCount; i++)
            {
                string textFromUi = await TextContentAsync(optionsLocator.Nth(i));
                if (string.Equals(textFromUi, optionToSelect, StringComparison.OrdinalIgnoreCase))
                {
                    await ClickAsync(optionsLocator.Nth(i));
                    break;
                }
            }
        }

        public async Task SelectOption(ILocator optionsLocator, string optionToSelect)
        {
            await ToBeVisibleAsync(optionsLocator, 0);
            var allElementsCount = await CountAsync(optionsLocator);
            for (int i = 0; i < allElementsCount; i++)
            {
                string textFromUi = await TextContentAsync(optionsLocator.Nth(i));
                if (string.Equals(textFromUi, optionToSelect, StringComparison.OrdinalIgnoreCase))
                {
                    await ClickAsync(optionsLocator.Nth(i));
                    break;
                }
            }
        }

        public async Task SelectOption(ILocator optionsLocator, int index)
        {
            await ToBeVisibleAsync(optionsLocator, 0);
            var allElementsCount = await CountAsync(optionsLocator);
            for (int i = 0; i < allElementsCount; i++)
            {
                if (i == index)
                {
                    await ClickAsync(optionsLocator.Nth(i));
                    break;
                }
            }
        }

        public async Task SelectDropdownOption(string dropdownOptionsOpener, string dropdownOptions, string option)
        {
            await ClickAsync(Locator(dropdownOptionsOpener));
            var dropdownOptionsLocator = Locator(dropdownOptions);
            await ToBeVisibleAsync(dropdownOptionsLocator, 0);
            await SelectOption(dropdownOptions, option);
        }

        public async Task SelectDropdownOption(ILocator dropdownOptionsOpener, ILocator dropdownOptions, string option)
        {
            await ClickAsync(dropdownOptionsOpener);
            var dropdownOptionsLocator = dropdownOptions;
            await ToBeVisibleAsync(dropdownOptionsLocator, 0);
            await SelectOption(dropdownOptions, option);
        }

        public async Task ClearAsnyc(string selector)
        {
            var locator = Locator(selector);
            await locator.ClearAsync();
        }

        public async Task ClearAsnyc(ILocator locator, LocatorClearOptions? options = default)
        {
            await locator.ClearAsync(options);
        }

        public async Task WaitForLoadStateAsync(LoadState? state = default, PageWaitForLoadStateOptions? options = default)
        {
            await _page.WaitForLoadStateAsync(state, options);
        }

        public async Task WaitForSelectorAsync(string selector, PageWaitForSelectorOptions? options = default)
        {
            await _page.WaitForSelectorAsync(selector, options);
        }

        public async Task MouseDblClickAsync(float x, float y, MouseDblClickOptions? options = default)
        {
            await _page.Mouse.DblClickAsync(x, y, options);
        }

        public async Task MouseDownAsync(MouseDownOptions? options = default)
        {
            await _page.Mouse.DownAsync(options);
        }

        public async Task MouseMoveAsync(float x, float y, MouseMoveOptions? options = default)
        {
            await _page.Mouse.MoveAsync(x, y, options);
        }

        public async Task MouseUpAsync(MouseUpOptions? options = default)
        {
            await _page.Mouse.UpAsync(options);
        }

        public async Task MouseClickAsync(float x, float y, MouseClickOptions? options = default)
        {
            await _page.Mouse.ClickAsync(x, y, options);
        }

        public async Task MouseWheelAsync(float deltaX, float deltaY)
        {
            await _page.Mouse.WheelAsync(deltaX, deltaY);
        }

        public async Task ScrollUsingMouseUntilElementIsVisible(ILocator locator, float deltaX, float deltaY, int maxScrollTimes)
        {
            int i = 0;
            while (!await IsVisibleAsync(locator))
            {
                await MouseWheelAsync(deltaX, deltaY);
                i++;
                Console.WriteLine($"Scrolled {i}st time for the {locator}");
                if (i >= maxScrollTimes) break;
            }
        }

        public async Task ScrollUsingMouseUntilElementIsVisible(string selector, float deltaX, float deltaY, int maxScrollTimes)
        {
            int i = 0;
            while (!await IsVisibleAsync(Locator(selector)))
            {
                await MouseWheelAsync(deltaX, deltaY);
                i++;
                Console.WriteLine(i+" times scrolled");
                if (i >= maxScrollTimes) break;
            }
        }
    }

}

