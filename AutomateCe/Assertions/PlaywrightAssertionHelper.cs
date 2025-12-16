using AutomateCe.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Playwright.Assertions;

namespace AutomateCe.Assertions
{
    public static class PlaywrightAssertionHelper
    {

        public static async Task ShouldBeVisible(ILocator locator, LocatorAssertionsToBeVisibleOptions? options = default, string message = null)
        {
            try
            {
                await Expect(locator).ToBeVisibleAsync(options);
            }
            catch (Exception ex)
            {
                throw new Exception(message ?? $"Element was expected to be visible but was not.\n{ex}");
            }
        }

        public static async Task ShouldNotBeVisible(ILocator locator, LocatorAssertionsToBeVisibleOptions? options = default, string message = null)
        {
            try
            {
                await Expect(locator).Not.ToBeVisibleAsync(options);
            }
            catch (Exception ex)
            {
                throw new Exception(message ?? $"Element was expected not to be visible but not.\n{ex}");
            }
        }

        public static async Task ShouldBeHidden(ILocator locator, string message = null)
        {
            try
            {
                await Expect(locator).ToBeHiddenAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message ?? $"Element was expected to be hidden but is visible.\n{ex}");
            }
        }

        public static async Task ShouldBeEnabled(ILocator locator, string message = null)
        {
            try
            {
                await Expect(locator).ToBeEnabledAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message ?? $"Element was expected to be enabled but is disabled.\n{ex}");
            }
        }

        public static async Task ShouldBeDisabled(ILocator locator, string message = null)
        {
            try
            {
                await Expect(locator).ToBeDisabledAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message ?? $"Element was expected to be disabled but is enabled.\n{ex}");
            }
        }

        public static async Task ShouldHaveText(ILocator locator, string expected, string message = null)
        {
            try
            {
                await Expect(locator).ToHaveTextAsync(expected);
            }
            catch (Exception ex)
            {
                throw new Exception(message ?? $"Element text mismatch. Expected: '{expected}'.\n{ex}");
            }
        }

        public static async Task ShouldContainText(ILocator locator, string expected, string message = null)
        {
            try
            {
                await Expect(locator).ToContainTextAsync(expected);
            }
            catch (Exception ex)
            {
                throw new Exception(message ?? $"Element did not contain text '{expected}'.\n{ex}");
            }
        }

        public static async Task ShouldHaveAttribute(ILocator locator, string attribute, string expectedValue)
        {
            try
            {
                await Expect(locator).ToHaveAttributeAsync(attribute, expectedValue);
            }
            catch (Exception ex)
            {
                throw new Exception($"Attribute '{attribute}' did not have expected value '{expectedValue}'.\n{ex}");
            }
        }

        public static async Task ShouldHaveValue(ILocator locator, string expected)
        {
            try
            {
                await Expect(locator).ToHaveValueAsync(expected);
            }
            catch (Exception ex)
            {
                throw new Exception($"Input value mismatch. Expected: '{expected}'.\n{ex}");
            }
        }

        public static async Task ShouldHaveCount(ILocator locator, int count)
        {
            try
            {
                await Expect(locator).ToHaveCountAsync(count);
            }
            catch (Exception ex)
            {
                throw new Exception($"Expected count: {count}. Actual does not match.\n{ex}");
            }
        }

        public static async Task UrlShouldBe(IPage page, string expectedUrl)
        {
            try
            {
                await Expect(page).ToHaveURLAsync(expectedUrl);
            }
            catch (Exception ex)
            {
                throw new Exception($"Page URL mismatch. Expected: {expectedUrl}.\n{ex}");
            }
        }

        public static async Task UrlShouldContain(IPage page, string partialUrl)
        {
            try
            {
                await Expect(page).ToHaveURLAsync(new Regex($".*{partialUrl}.*"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Page URL does not contain: {partialUrl}.\n{ex}");
            }
        }

        public static async Task TitleShouldBe(IPage page, string expectedTitle)
        {
            try
            {
                await Expect(page).ToHaveTitleAsync(expectedTitle);
            }
            catch (Exception ex)
            {
                throw new Exception($"Page title mismatch. Expected: {expectedTitle}.\n{ex}");
            }
        }
    }
}

