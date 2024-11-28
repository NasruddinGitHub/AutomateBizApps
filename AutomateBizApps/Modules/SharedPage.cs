using AutomateBizApps.ObjectRepository;
using AutomateBizApps.Pages;
using AutomateCe.Controls;
using AutomateCe.Enums;
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
            await ClickAsync(Locator(CommonLocators.Button.Replace("[Name]", buttonName)));
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

        protected async Task<ILocator> ValidateFormContext(FormContextType formContextType, string field, int timeToCheckIfFrameExists)
        {
            ILocator fieldLocator = null;
            if (formContextType == FormContextType.QuickCreate)
            {

            }
            else if (formContextType == FormContextType.Entity)
            {
                ILocator formContainerLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, EntityLocators.FormContainer, timeToCheckIfFrameExists);
                fieldLocator = LocatorWithXpath(formContainerLocator, CommonLocators.FieldContainer.Replace("[Name]", field));
            }
            else if (formContextType == FormContextType.BusinessProcessFlow)
            {

            }
            else if (formContextType == FormContextType.Header)
            {

            }
            else if (formContextType == FormContextType.Dialog)
            {

            }
            return fieldLocator;
        }
    }
}
