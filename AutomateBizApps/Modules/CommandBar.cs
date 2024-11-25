using AutomateBizApps.Pages;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateBizApps.Modules
{
    public class CommandBar : SharedPage
    {
        private IPage _page;

        public CommandBar(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task ClickCommand(string name, int timeToCheckIfFrameExists = 1000, string subName = null, string subSecondName = null)
        {

            var commandLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.CommandItem.Replace("[Name]", name), timeToCheckIfFrameExists);
            try
            {
                await ClickAsync(commandLocator, new LocatorClickOptions { Timeout = 7000 });
                await WaitUntilAppIsIdle();
            }
            catch (TimeoutException ex)
            {
                await ClickMoreCommandEllipsis(timeToCheckIfFrameExists);
                await ClickAsync(commandLocator);
            }

            if (!string.IsNullOrEmpty(subName))
            {
                var subNameLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.CommandItem.Replace("[Name]", subName), timeToCheckIfFrameExists);
                await ClickAsync(subNameLocator);
            }

            if (!string.IsNullOrEmpty(subSecondName))
            {
                var secondSubNameLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.CommandItem.Replace("[Name]", subSecondName), timeToCheckIfFrameExists);
                await ClickAsync(secondSubNameLocator);
            }

        }

        public async Task ClickMoreCommandEllipsis(int timeToCheckIfFrameExists = 1000)
        {
            ILocator moreCommandsLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.MoreCommandsEllipsis, timeToCheckIfFrameExists);
            await ClickAsync(moreCommandsLocator);
        }

        public async Task<List<string>> GetAllShownCommands(int timeToCheckIfFrameExists = 1000)
        {
            var allShownCommands = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.AllShownCommands, timeToCheckIfFrameExists);
            return await GetAllElementsTextAfterWaiting(allShownCommands);
        }

        public async Task<List<string>> GetAllMoreCommands(int timeToCheckIfFrameExists = 1000)
        {
            await ClickMoreCommandEllipsis(timeToCheckIfFrameExists);
            var allMoreCommands = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.AllMoreCommands, timeToCheckIfFrameExists);
            return await GetAllElementsTextAfterWaiting(allMoreCommands);
        }

        public async Task OpenInNewWindow(int timeToCheckIfFrameExists = 1000)
        {
            var openInNewWindowLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.OpenInNewWindow, timeToCheckIfFrameExists);
            await ClickAsync(openInNewWindowLocator);
        }

        public async Task RecordSetNavigator(int timeToCheckIfFrameExists = 1000)
        {
            var recordSetNavigatorLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.RecordSetNavigator, timeToCheckIfFrameExists);
            await ClickAsync(recordSetNavigatorLocator);
        }

        public async Task ClickShare(int timeToCheckIfFrameExists = 1000)
        {
            var shareLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.Share, timeToCheckIfFrameExists);
            await ClickAsync(shareLocator);
        }

        public async Task<List<string>> GetAllSharingTypes(int timeToCheckIfFrameExists = 1000)
        {
            await ClickShare(timeToCheckIfFrameExists);
            var allShareTypesLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.AllShareItems, timeToCheckIfFrameExists);
            return await GetAllElementsTextAfterWaiting(allShareTypesLocator);
        }

        public async Task ClickSharingType(string type, int timeToCheckIfFrameExists = 1000)
        {
            await ClickShare(timeToCheckIfFrameExists);
            var shareTypeLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.ShareType.Replace("[Name]", type), timeToCheckIfFrameExists);
            await ClickAsync(shareTypeLocator);
        }
    }
}
