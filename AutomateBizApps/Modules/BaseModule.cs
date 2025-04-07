using AutomateCe.Controls;
using AutomateCe.Enums;
using Microsoft.Playwright;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

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

        protected ILocator GetByAltText(string text)
        {
            return _page.GetByAltText(text);
        }

        protected ILocator GetByLabel(string text)
        {
            return _page.GetByLabel(text);
        }

        protected ILocator GetByPlaceholder(string text)
        {
            return _page.GetByPlaceholder(text);
        }

        protected ILocator GetByRole(AriaRole ariaRole, PageGetByRoleOptions? options = default)
        {
            return _page.GetByRole(ariaRole, options);
        }

        protected ILocator GetByTestId(string testId)
        {
            return _page.GetByTestId(testId);
        }
        protected ILocator GetByText(string text, PageGetByTextOptions? options = default)
        {
            return _page.GetByText(text, options);
        }
        protected ILocator GetByTitle(string text, PageGetByTitleOptions? options = default)
        {
            return _page.GetByTitle(text, options);
        }
        protected ILocator Locator(string selector, PageLocatorOptions? options = default)
        {
            return _page.Locator(selector, options);
        }

        protected ILocator LocatorWithXpath(string selector, PageLocatorOptions? options = default)
        {
            return _page.Locator($"xpath={selector}", options);
        }

        protected ILocator LocatorWithXpath(ILocator locator, string selector, LocatorLocatorOptions? options = default)
        {
            return locator.Locator($"xpath={selector}", options);
        }

        protected ILocator Locator(ILocator locator, string selector, LocatorLocatorOptions? options = default)
        {
            return locator.Locator(selector, options);
        }

        protected async Task<string> GetAttributeAsync(string selector, string name, PageGetAttributeOptions? options = default)
        {
            return await _page.GetAttributeAsync(selector, name, options);
        }

        protected async Task<string> GetAttributeAsync(ILocator locator, string name, LocatorGetAttributeOptions? options = default)
        {
            return await locator.GetAttributeAsync(name, options);
        }

        protected IFrameLocator FrameLocator(string frameSelector)
        {
            return _page.FrameLocator(frameSelector);
        }

        protected ILocator SwitchToFrameAndLocate(string frameSelector, string locatorSelector, FrameLocatorLocatorOptions? options = default)
        {
            return FrameLocator(frameSelector).Locator(locatorSelector, options);
        }

        protected ILocator SwitchToFrameAndLocateWithXpath(string frameSelector, string locatorSelector, FrameLocatorLocatorOptions? options = default)
        {
            return FrameLocator(frameSelector).Locator($"xpath={locatorSelector}", options);
        }

        protected ILocator SwitchToFrameAndLocate(IFrameLocator frameLocator, string locatorSelector, FrameLocatorLocatorOptions? options = default)
        {
            return frameLocator.Locator(locatorSelector, options);
        }

        protected ILocator SwitchToFrameAndLocate(IFrame frame, string locatorSelector, FrameLocatorOptions? options = default)
        {
            return frame.Locator(locatorSelector, options);
        }

        protected ILocator SwitchToFrameAndLocateWithXpath(ILocator frameSelector, string locatorSelector, LocatorLocatorOptions? options = default)
        {
            return frameSelector.Locator($"xpath={locatorSelector}", options);
        }

        protected IFrame? Frame(string selector)
        {
            return _page.Frame(selector);
        }

        protected IFrame? FrameByUrl(string url)
        {
            return _page.FrameByUrl(url);
        }

        protected IFrame? FrameByUrl(Regex url)
        {
            return _page.FrameByUrl(url);
        }

        protected async Task EvaluateAsync(ILocator locatorToClick, string expression, object? arg = default, LocatorEvaluateOptions? options = default)
        {
            await locatorToClick.EvaluateAsync(expression, default, options);
        }

        protected async Task HightlightElementAsync(ILocator locatorToClick, LocatorEvaluateOptions? options = default)
        {
            await EvaluateAsync(locatorToClick, "el => el.style.border = '3px solid red'", options);
            await EvaluateAsync(locatorToClick, "el => el.style.backgroundColor = 'yellow'", options);
        }

        protected async Task ClickAsync(ILocator locatorToClick, LocatorClickOptions? options1 = default, LocatorEvaluateOptions? options2 = default)
        {

            // await HightlightElementAsync(locatorToClick, options2);
            await locatorToClick.ClickAsync(options1);
            await WaitUntilAppIsIdle();
        }

        protected async Task ClickAsync(ILocator locatorToClick, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorClickOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locatorToClick, 0, 100, maxNumberOfScrolls);
            }
            await HightlightElementAsync(locatorToClick);
            await locatorToClick.ClickAsync(options);
            await WaitUntilAppIsIdle();
        }

        // Need to add scrollables overloads
        protected async Task DoubleClickAsync(ILocator locator, LocatorDblClickOptions? options = default)
        {
            await HightlightElementAsync(locator);
            await locator.DblClickAsync(options);
            await WaitUntilAppIsIdle();
        }

        protected async Task DoubleClickAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorDblClickOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await HightlightElementAsync(locator);
            await locator.DblClickAsync(options);
            await WaitUntilAppIsIdle();
        }

        protected async Task CheckAsync(ILocator locator, LocatorCheckOptions? options = default)
        {
            await HightlightElementAsync(locator);
            await locator.CheckAsync(options);
            await WaitUntilAppIsIdle();
        }

        protected async Task CheckAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorCheckOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await HightlightElementAsync(locator);
            await locator.CheckAsync(options);
            await WaitUntilAppIsIdle();
        }

        protected async Task<int> CountAsync(ILocator locator)
        {
            return await locator.CountAsync();
        }

        protected async Task HoverAsync(ILocator locator, LocatorHoverOptions? options = default)
        {
            await HightlightElementAsync(locator);
            await locator.Nth(0).HoverAsync(options);
        }

        protected async Task HoverAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorHoverOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await HightlightElementAsync(locator);
            await locator.HoverAsync(options);
        }

        protected async Task HoverAndClickAsync(ILocator locator, LocatorHoverOptions? options1 = default, LocatorClickOptions? options2 = default)
        {
            await HightlightElementAsync(locator);
            await locator.HoverAsync(options1);
            await ClickAsync(locator, options2);
        }

        protected async Task HoverAndClickAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorHoverOptions? options1 = default, LocatorClickOptions? options2 = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await HightlightElementAsync(locator);
            await locator.HoverAsync(options1);
            await ClickAsync(locator, options2);
        }

        protected async Task FillAsync(ILocator locator, string value, LocatorFillOptions? options = default)
        {
            await locator.FillAsync(value, options);
        }

        protected async Task UploadFile(ILocator locator, string filePath)
        {
            await locator.SetInputFilesAsync(filePath);
        }

        protected async Task FillAsync(ILocator locator, string value, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorFillOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await locator.FillAsync(value, options);
        }

        protected IPage Page(ILocator locator)
        {
            return locator.Page;
        }

        //protected async Task<ILocator> First(ILocator locator)
        //{
        //    return locator.First;
        //}

        //protected async Task<ILocator> Last(ILocator locator)
        //{
        //    return locator.Last;
        //}

        protected ILocator Nth(ILocator locator, int index)
        {
            return locator.Nth(index);
        }

        protected async Task UncheckAsync(ILocator locator, LocatorUncheckOptions? options = default)
        {
            await HightlightElementAsync(locator);
            await locator.UncheckAsync(options);
            await WaitUntilAppIsIdle();
        }

        protected async Task UncheckAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorUncheckOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await HightlightElementAsync(locator);
            await locator.UncheckAsync(options);
            await WaitUntilAppIsIdle();
        }

        protected async Task TypeAsync(ILocator locator, string text, LocatorTypeOptions? options = default)
        {
            await locator.TypeAsync(text, options);
        }

        protected async Task TypeAsync(ILocator locator, string text, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorTypeOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await locator.TypeAsync(text, options);
        }

        protected async Task<bool> IsCheckedAsync(ILocator locator, LocatorIsCheckedOptions? options = default)
        {
            return await locator.IsCheckedAsync(options);
        }

        protected async Task<bool> IsCheckedAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorIsCheckedOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.IsCheckedAsync(options);
        }

        protected async Task<bool> IsDisabledAsync(ILocator locator, LocatorIsDisabledOptions? options = default)
        {
            return await locator.IsDisabledAsync(options);
        }

        protected async Task<bool> IsDisabledAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorIsDisabledOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.IsDisabledAsync(options);
        }

        protected async Task<bool> IsEditableAsync(ILocator locator, LocatorIsEditableOptions? options = default)
        {
            return await locator.IsEditableAsync(options);
        }

        protected async Task<bool> IsEditableAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorIsEditableOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.IsEditableAsync(options);
        }

        protected async Task<bool> IsEnabledAsync(ILocator locator, LocatorIsEnabledOptions? options = default)
        {
            return await locator.IsEnabledAsync(options);
        }

        protected async Task<bool> IsEnabledAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorIsEnabledOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.IsEnabledAsync(options);
        }

        protected async Task<bool> IsHiddenAsync(ILocator locator, LocatorIsHiddenOptions? options = default)
        {
            return await locator.IsHiddenAsync(options);
        }

        protected async Task<bool> IsHiddenAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorIsHiddenOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.IsHiddenAsync(options);
        }

        protected async Task<bool> IsVisibleAsync(ILocator locator, LocatorIsVisibleOptions? options = default)
        {
            return await locator.IsVisibleAsync(options);
        }

        protected async Task<bool> IsVisibleAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorIsVisibleOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.IsVisibleAsync(options);
        }

        protected async Task ScrollIntoViewIfNeededAsync(ILocator locator, LocatorScrollIntoViewIfNeededOptions? options = default)
        {
            await locator.ScrollIntoViewIfNeededAsync(options);
        }

        protected async Task SelectOptionAsync(ILocator locator, string values, LocatorSelectOptionOptions? options = default)
        {
            await locator.SelectOptionAsync(values, options);
        }

        protected async Task<List<string>> GetAllAvailbleSelecOptions(ILocator locator, LocatorSelectOptionOptions? options = default)
        {
            return await locator.EvaluateAsync<List<string>>("select => Array.from(select.options).map(option => option.text)");
        }

        protected async Task SelectOptionAsync(ILocator locator, string values, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorSelectOptionOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await locator.SelectOptionAsync(values, options);
        }

        public async Task<byte[]> ElementScreenshotAsync(ILocator locator, LocatorScreenshotOptions? options = default)
        {
            return await locator.ScreenshotAsync(options);
        }

        public async Task<byte[]> PageScreenshotAsync(PageScreenshotOptions? options = default)
        {
            return await _page.ScreenshotAsync(options);
        }

        public async Task<byte[]> PageScreenshotAsync(string path)
        {
            return await _page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
        }

        protected async Task<byte[]> ScreenshotAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorScreenshotOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.ScreenshotAsync(options);
        }

        protected async Task WaitForAsync(ILocator locator, LocatorWaitForOptions? options = default)
        {
            await locator.WaitForAsync(options);
        }

        protected async Task<string> InputValueAsync(ILocator locator, LocatorInputValueOptions? options = default)
        {
            return await InputValueAsync(locator, false, null, 0, options);
        }

        protected async Task<string> InputValueAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorInputValueOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.InputValueAsync(options);
        }

        protected ILocator Filter(ILocator locator, LocatorFilterOptions? options = default)
        {
            return locator.Filter(options);
        }

        protected async Task HighlightAsync(ILocator locator)
        {
            await locator.HighlightAsync();
        }

        protected async Task HighlightAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await locator.HighlightAsync();
        }

        protected async Task<string> TextContentAsync(ILocator locator, LocatorTextContentOptions? options = default)
        {
            string textContext = await locator.TextContentAsync(options);
            return textContext.Trim();
        }

        protected async Task<string> InnerTextAsync(ILocator locator, LocatorInnerTextOptions? options = default)
        {
            string textContext = await locator.InnerTextAsync(options);
            return textContext.Trim().Replace('\u00A0', ' ');
        }

        protected async Task<string?> TextContentAsync(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorTextContentOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            return await locator.TextContentAsync(options);
        }

        // This will not wait until first element is visible
        protected async Task<List<string>> GetAllElementsText(ILocator locator)
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
        protected async Task<List<string>> GetAllElementsTextAfterWaiting(ILocator locator, int waitTime = 10000)
        {
            var allElementsText = new List<string?>();
            await ToBeVisibleAsync(locator, 0, waitTime);
            var allElementsCount = await CountAsync(locator);
            for (int i = 0; i < allElementsCount; i++)
            {
                string elementText = await TextContentAsync(locator.Nth(i));
                allElementsText.Add(elementText.Trim());
            }
            await KeyboardPressAsync("Tab");
            return allElementsText;
        }

        // This will wait until first element is visible
        protected async Task<List<string>> GetAllElementsInnerTextAfterWaiting(ILocator locator, int waitTime = 10000)
        {
            var allElementsText = new List<string?>();
            await ToBeVisibleAsync(locator, 0, waitTime);
            var allElementsCount = await CountAsync(locator);
            for (int i = 0; i < allElementsCount; i++)
            {
                string elementText = await InnerTextAsync(locator.Nth(i));
                allElementsText.Add(elementText.Trim());
            }
            await KeyboardPressAsync("Tab");
            return allElementsText;
        }

        protected async Task<List<ILocator>> GetAllElementsAfterWaiting(ILocator locator, int waitTime = 10000)
        {
            var allElementsLocator = new List<ILocator?>();
            await ToBeVisibleAsync(locator, 0, waitTime);
            var allElementsCount = await CountAsync(locator);
            for (int i = 0; i < allElementsCount; i++)
            {
                allElementsLocator.Add(locator.Nth(i));
            }
            return allElementsLocator;
        }

        protected async Task<string> GetOuterTagText(ILocator locator, int waitTime = 10000)
        {
            string outerText = await locator.EvaluateAsync<string>("el => Array.from(el.childNodes).filter(n => n.nodeType === Node.TEXT_NODE).map(n => n.textContent).join('').trim()");
            return outerText;
        }

        protected async Task<List<string>> GetAllElementsTextAfterWaiting(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, int waitTime = 10000)
        {
            var allElementsText = new List<string?>();
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await ToBeVisibleAsync(locator, 0, waitTime);
            var allElementsCount = await CountAsync(locator);
            for (int i = 0; i < allElementsCount; i++)
            {
                allElementsText.Add(await TextContentAsync(locator.Nth(i)));
            }
            return allElementsText;
        }

        protected void ThinkTime(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        protected async Task ToBeVisibleAsync(ILocator locator, int index = 0, int visibleTimeout = 5000)
        {
            await Expect(locator.Nth(index)).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
        }

        protected async Task ToBeVisibleAsync(ILocator locator, int index, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, int visibleTimeout = 5000)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await Expect(locator.Nth(index)).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
        }

        protected async Task ToBeNotVisibleAsync(ILocator locator, int index = 0, int visibleTimeout = 5000)
        {
            await Expect(locator.Nth(index)).Not.ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
        }

        protected async Task ToBeNotVisibleAsync(ILocator locator, int index, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, int visibleTimeout = 5000)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await Expect(locator.Nth(index)).Not.ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
        }

        protected async Task<bool> IsVisibleAsyncWithWaiting(ILocator locator, int index = 0, int visibleTimeout = 5000)
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

        protected async Task<bool> IsDisplayedAsyncWithWaiting(ILocator locator, int index = 0, int visibleTimeout = 5000)
        {

            await WaitForAsync(locator);
            int numberOfElements = await CountAsync(locator);
            if (numberOfElements > 0)
            {
                return true;
            }

            return false;
        }

        protected async Task<bool> IsNotVisibleAsyncWithWaiting(ILocator locator, int index, int visibleTimeout = 5000)
        {
            try
            {
                await Expect(locator.Nth(index)).Not.ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = visibleTimeout });
                return true;
            }
            catch (PlaywrightException ex)
            {
                // Console.WriteLine(ex.ToString()+" Timeout exception is thrown.");
                // Log something
            }
            return false;
        }

        protected async Task<bool> IsVisibleAsyncWithWaiting(ILocator locator, int index, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, int visibleTimeout = 5000)
        {
            try
            {
                if (dynamicallyLoaded)
                {
                    await HoverAsync(Locator(anySelectorInScroller));
                    await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
                }
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

        protected async Task WaitUntilElementNotVisbleIfVisible(ILocator locator, int waitTimeToVisble, int waitTimeNotToVisble)
        {
            await WaitUntilElementNotVisbleIfVisible(locator, waitTimeToVisble, waitTimeNotToVisble, false);
        }

        protected async Task WaitUntilElementNotVisbleIfVisible(ILocator locator, int waitTimeToVisble, int waitTimeNotToVisble, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            try
            {
                if (dynamicallyLoaded)
                {
                    await HoverAsync(Locator(anySelectorInScroller));
                    await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
                }
                await ToBeVisibleAsync(locator, 0, waitTimeToVisble);
                await ToBeNotVisibleAsync(locator, 0, waitTimeNotToVisble);
            }
            catch (PlaywrightException ex)
            {
                // Element is not visible
                // Log the message
            }
        }

        protected async Task GoBackAsync(PageGoBackOptions pageGoBackOptions)
        {
            await _page.GoBackAsync(pageGoBackOptions);
        }

        protected async Task GoForwardAsync(PageGoForwardOptions pageGoForwardOptions)
        {
            await _page.GoForwardAsync(pageGoForwardOptions);
        }

        protected async Task ReloadAsync(PageReloadOptions pageReloadOptions)
        {
            await _page.ReloadAsync(pageReloadOptions);
        }

        protected string GetTotp(string mfaKey)
        {
            byte[] base32Bytes = Base32Encoding.ToBytes(mfaKey);

            var totp = new Totp(base32Bytes);
            return totp.ComputeTotp();
        }

        protected async Task KeyBoardDownAsync(string key)
        {
            await _page.Keyboard.DownAsync(key);
        }

        protected async Task KeyBoardUpAsync(string key)
        {
            await _page.Keyboard.UpAsync(key);
        }

        protected async Task KeyboardTypeAsync(string key, KeyboardTypeOptions? options = default)
        {
            await _page.Keyboard.TypeAsync(key, options);
        }

        protected async Task KeyboardPressAsync(string key, KeyboardPressOptions? options = default)
        {
            await _page.Keyboard.PressAsync(key, options);
        }

        protected async Task KeyboardInsertTextAsync(string key)
        {
            await _page.Keyboard.InsertTextAsync(key);
        }

        protected async Task PressKeyboardKeyAsync(string key, int noOfTimes, KeyboardPressOptions? options = default)
        {
            for (int i = 0; i < noOfTimes; i++)
                await _page.Keyboard.PressAsync(key, options);
        }

        protected async Task SelectOption(string options, string optionToSelect)
        {
            await SelectOption(options, optionToSelect, false);
        }

        protected async Task SelectOption(string options, string optionToSelect, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            var optionsLocator = Locator(options);
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(optionsLocator, 0, 100, maxNumberOfScrolls);
            }
            await ToBeVisibleAsync(optionsLocator, 0);
            var allElementsCount = await CountAsync(Locator(options));
            for (int i = 0; i < allElementsCount; i++)
            {
                string? textFromUi = await TextContentAsync(optionsLocator.Nth(i));
                if (string.Equals(textFromUi, optionToSelect, StringComparison.OrdinalIgnoreCase))
                {
                    await ClickAsync(optionsLocator.Nth(i));
                    break;
                }
            }
        }

        protected async Task SelectOption(ILocator optionsLocator, string optionToSelect)
        {
            await SelectOption(optionsLocator, optionToSelect, false);
        }

        protected async Task SelectOption(ILocator optionsLocator, string optionToSelect, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(optionsLocator, 0, 100, maxNumberOfScrolls);
            }
            await ToBeVisibleAsync(optionsLocator, 0);
            var allElementsCount = await CountAsync(optionsLocator);
            for (int i = 0; i < allElementsCount; i++)
            {
                string? textFromUi = await TextContentAsync(optionsLocator.Nth(i));
                if (string.Equals(textFromUi, optionToSelect, StringComparison.OrdinalIgnoreCase))
                {
                    await ClickAsync(optionsLocator.Nth(i));
                    break;
                }
            }
        }

        protected async Task SelectOptions(ILocator optionsLocator, string[] optionsToSelect)
        {
            await SelectOptions(optionsLocator, optionsToSelect, false);
        }

        protected async Task SelectOptions(ILocator optionsLocator, string[] optionsToSelect, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(optionsLocator, 0, 100, maxNumberOfScrolls);
            }
            await ToBeVisibleAsync(optionsLocator, 0);
            var allElementsCount = await CountAsync(optionsLocator);

            for (int i = 0; i < optionsToSelect.Length; i++)
            {
                for (int j = 0; j < allElementsCount; j++)
                {
                    string? textFromUi = await TextContentAsync(optionsLocator.Nth(j));
                    if (string.Equals(optionsToSelect[i], textFromUi, StringComparison.OrdinalIgnoreCase))
                    {
                        await ClickAsync(optionsLocator.Nth(j));
                        break;
                    }
                }
            }
        }

        protected async Task SelectOption(ILocator optionsLocator, int index)
        {
            await SelectOption(optionsLocator, index, false);
        }

        protected async Task SelectOption(ILocator optionsLocator, int index, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(optionsLocator, 0, 100, maxNumberOfScrolls);
            }
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

        protected async Task SelectDropdownOption(string dropdownOptionsOpener, string dropdownOptions, string option)
        {
            await SelectDropdownOption(dropdownOptionsOpener, dropdownOptions, option, false);
        }

        protected async Task SelectDropdownOption(string dropdownOptionsOpener, string dropdownOptions, string option, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            await SelectDropdownOption(LocatorWithXpath(dropdownOptionsOpener), LocatorWithXpath(dropdownOptions), option, dynamicallyLoaded);
        }

        protected async Task SelectDropdownOption(ILocator dropdownOptionsOpener, ILocator dropdownOptions, string option)
        {
            await SelectDropdownOption(dropdownOptionsOpener, dropdownOptions, option, false);
        }

        protected async Task SelectDropdownOption(ILocator dropdownOptionsOpener, ILocator dropdownOptions, string option, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(dropdownOptionsOpener, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(dropdownOptionsOpener);
            var dropdownOptionsLocator = dropdownOptions;
            await ToBeVisibleAsync(dropdownOptionsLocator, 0);
            await SelectOption(dropdownOptions, option);
        }

        protected async Task SelectMultiSelectOptions(ILocator dropdownOptionsOpener, ILocator dropdownOptions, string[] options, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(dropdownOptionsOpener, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(dropdownOptionsOpener);
            await ToBeVisibleAsync(dropdownOptions, 0);
            await SelectOptions(dropdownOptions, options);
        }

        protected async Task SelectMultiSelectOptions(ILocator dropdownOptionsOpener, ILocator dropdownOptions, string[] options)
        {
            await SelectMultiSelectOptions(dropdownOptionsOpener, dropdownOptions, options, false);
        }

        protected async Task<List<string>> GetAllAvailableChoices(ILocator dropdownOptionsOpener, ILocator dropdownOptions, bool dynamicallyLoaded, string? anyFieldNameInScroller = null, int maxNumberOfScrolls = 0)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommonLocators.FieldContainer.Replace("[Name]", anyFieldNameInScroller)));
                await ScrollUsingMouseUntilElementIsVisible(dropdownOptionsOpener, 0, 100, maxNumberOfScrolls);
            }
            await ClickAsync(dropdownOptionsOpener);
            List<string> availableChoices = await GetAllElementsTextAfterWaiting(dropdownOptions);
            await KeyboardPressAsync("Tab");
            return availableChoices;
        }

        protected async Task<List<string>> GetAllAvailableChoices(ILocator dropdownOptionsOpener, ILocator dropdownOptions, bool dynamicallyLoaded)
        {
            return await GetAllAvailableChoices(dropdownOptionsOpener, dropdownOptions, false);
        }

        protected async Task ClearAsnyc(ILocator locator, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorClearOptions? options = default)
        {
            if (dynamicallyLoaded)
            {
                await HoverAsync(Locator(anySelectorInScroller));
                await ScrollUsingMouseUntilElementIsVisible(locator, 0, 100, maxNumberOfScrolls);
            }
            await locator.ClearAsync(options);
        }

        protected async Task ClearAsnyc(ILocator locator, LocatorClearOptions? options = default)
        {
            await ClearAsnyc(locator, false, null, 0, options);
        }

        protected async Task ClearAsnyc(string selector, bool dynamicallyLoaded, string? anySelectorInScroller = null, int maxNumberOfScrolls = 0, LocatorClearOptions? options = default)
        {
            await ClearAsnyc(Locator(selector), dynamicallyLoaded, anySelectorInScroller, maxNumberOfScrolls, options);
        }

        protected async Task ClearAsnyc(string selector, LocatorClearOptions? options = default)
        {
            await ClearAsnyc(Locator(selector), false, null, 0, options);
        }

        protected async Task WaitForLoadStateAsync(LoadState? state = default, PageWaitForLoadStateOptions? options = default)
        {
            await _page.WaitForLoadStateAsync(state, options);
        }

        protected async Task WaitUntilDomContentLoaded()
        {
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        protected async Task WaitUntilLoaded()
        {
            await _page.WaitForLoadStateAsync(LoadState.Load);
        }

        protected async Task WaitForSelectorAsync(string selector, PageWaitForSelectorOptions? options = default)
        {
            await _page.WaitForSelectorAsync(selector, options);
        }

        protected async Task MouseDblClickAsync(float x, float y, MouseDblClickOptions? options = default)
        {
            await _page.Mouse.DblClickAsync(x, y, options);
        }

        protected async Task MouseDownAsync(MouseDownOptions? options = default)
        {
            await _page.Mouse.DownAsync(options);
        }

        protected async Task MouseMoveAsync(float x, float y, MouseMoveOptions? options = default)
        {
            await _page.Mouse.MoveAsync(x, y, options);
        }

        protected async Task MouseUpAsync(MouseUpOptions? options = default)
        {
            await _page.Mouse.UpAsync(options);
        }

        protected async Task MouseClickAsync(float x, float y, MouseClickOptions? options = default)
        {
            await _page.Mouse.ClickAsync(x, y, options);
        }

        protected async Task MouseWheelAsync(float deltaX, float deltaY)
        {
            await _page.Mouse.WheelAsync(deltaX, deltaY);
        }

        protected async Task ScrollUsingMouseUntilElementIsVisible(ILocator locator, float deltaX, float deltaY, int maxScrollTimes)
        {
            int i = 0;
            while (!await IsVisibleAsync(locator))
            {
                await MouseWheelAsync(deltaX, deltaY);
                i++;
                Console.WriteLine($"Scrolled {i}st time for the {locator}");
                if (i >= maxScrollTimes)
                    throw new PlaywrightException($"{locator} is not shown even after scroll for {maxScrollTimes} times");
            }
        }

        protected async Task ScrollUsingMouseUntilElementIsVisible(string selector, float deltaX, float deltaY, int maxScrollTimes)
        {
            int i = 0;
            while (!await IsVisibleAsync(Locator(selector)))
            {
                await MouseWheelAsync(deltaX, deltaY);
                i++;
                Console.WriteLine($"Scrolled {i}st time for the {selector}");
                if (i >= maxScrollTimes)
                    throw new PlaywrightException($"{selector} is not shown even after scroll for {maxScrollTimes} times");
            }
        }

        protected async Task<ILocator> GetLocatorWhenInFramesNotInFrames(string frameSelector, string elementSelector)
        {
            bool isFrameNotShown = await IsNotVisibleAsyncWithWaiting(Locator(frameSelector), 0);
            if (isFrameNotShown)
            {
                return LocatorWithXpath(elementSelector);
            }
            return SwitchToFrameAndLocateWithXpath(frameSelector, elementSelector);
        }

        protected async Task<ILocator> GetLocatorWhenInFramesNotInFrames(string frameSelector, string elementSelector, int timeToCheckIfFrameExists)
        {
            bool isFrameNotShown = await IsNotVisibleAsyncWithWaiting(Locator(frameSelector), 0, timeToCheckIfFrameExists);
            if (isFrameNotShown)
            {
                return LocatorWithXpath(elementSelector);
            }
            return SwitchToFrameAndLocateWithXpath(frameSelector, elementSelector);
        }

        protected async Task WaitUntilAppIsIdle()
        {
            try
            {
                // To statically wait after click, if not below command may return true
                await Task.Delay(1000);
                if (!await _page.EvaluateAsync<bool>("window.UCWorkBlockTracker.isAppIdle()"))
                {
                    // Let's log something
                    await Task.Delay(500);
                    await WaitUntilAppIsIdle();

                }
            }
            catch (Exception e)
            {
                // Ignore this exception
            }
        }



        protected async Task WaitUntilAppReadyStateIsComplete()
        {
            string readyState = await _page.EvaluateAsync<string>("document.readyState");
            try
            {
                // To statically wait after click, if not below command may return complete immediately
                await Task.Delay(1000);
                bool isAppReadyStateComplete = String.Equals(readyState, "complete", StringComparison.OrdinalIgnoreCase);
                if (isAppReadyStateComplete)
                {
                    return;
                }
                await Task.Delay(500);
                await WaitUntilAppReadyStateIsComplete();
            }
            catch (Exception e)
            {
                // Ignore this exception
            }
        }

        protected async Task GotoAsync(string url, PageGotoOptions? options = default)
        {
            await _page.GotoAsync(url, options);
        }

        protected async Task WaitForNoActiveRequests()
        {
            try
            {
                // To statically wait after click, if not below command may return complete 0
                await Task.Delay(1000);
                int noOfRequest = await _page.EvaluateAsync<int>("window.performance.getEntriesByType('resource')" +
            ".filter(r => r.initiatorType === 'xmlhttprequest' || r.initiatorType === 'fetch').length");
                Console.WriteLine($"No active requests: {noOfRequest}");
                if (noOfRequest == 0)
                {
                    return;
                }
                await Task.Delay(500);
                await WaitForNoActiveRequests();
            }
            catch (Exception e)
            {
                // Ignore this exception
            }
        }

        private NameValueCollection GetUrlQueryParams(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            Uri uri = new Uri(url);
            var query = uri.Query.ToLower();
            NameValueCollection result = HttpUtility.ParseQueryString(query);
            return result;
        }

        protected async Task EnableTestMode()
        {
            String previousUrl = _page.Url;

            var prevQuery = GetUrlQueryParams(previousUrl);
            string queryParams = "";
            if (prevQuery.Get("flags") == null)
            {
                queryParams += "&flags=easyreproautomation=true,testmode=true";
            }

            var testModeUri = previousUrl + queryParams;
            await GotoAsync(testModeUri);
        }
    }

}

