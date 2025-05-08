using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class CommandBar : SharedPage
    {
        private IPage _page;

        public CommandBar(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task ClickCommandAsync(string name, int timeToCheckIfFrameExists = 1000, string subName = null, string subSecondName = null)
        {

            var commandLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.CommandItem.Replace("[Name]", name), timeToCheckIfFrameExists);
            try
            {
                await ClickAsync(commandLocator, new LocatorClickOptions { Timeout = 7000 });
                await WaitUntilAppIsIdleAsync();
            }
            catch (TimeoutException ex)
            {
                await ClickMoreCommandEllipsisAsync(timeToCheckIfFrameExists);
                await ClickAsync(commandLocator);
            }

            if (!string.IsNullOrEmpty(subName))
            {
                var subNameLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.CommandItem.Replace("[Name]", subName), timeToCheckIfFrameExists);
                await ClickAsync(subNameLocator);
            }

            if (!string.IsNullOrEmpty(subSecondName))
            {
                var secondSubNameLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.CommandItem.Replace("[Name]", subSecondName), timeToCheckIfFrameExists);
                await ClickAsync(secondSubNameLocator);
            }

        }

        public async Task ClickMoreCommandEllipsisAsync(int timeToCheckIfFrameExists = 1000)
        {
            ILocator moreCommandsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.MoreCommandsEllipsis, timeToCheckIfFrameExists);
            await ClickAsync(moreCommandsLocator);
        }

        public async Task<List<string>> GetAllShownCommandsAsync(int timeToCheckIfFrameExists = 1000)
        {
            var allShownCommands = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.AllShownCommands, timeToCheckIfFrameExists);
            return await GetAllElementsTextAfterWaitingAsync(allShownCommands);
        }

        public async Task<List<string>> GetAllMoreCommandsAsync(int timeToCheckIfFrameExists = 1000)
        {
            ILocator moreCommandsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.MoreCommandsEllipsis, timeToCheckIfFrameExists);
            if (await IsVisibleAsyncWithWaiting(moreCommandsLocator, 0))
            {
                await ClickMoreCommandEllipsisAsync(timeToCheckIfFrameExists);
                var allMoreCommands = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.AllMoreCommands, timeToCheckIfFrameExists);
                return await GetAllElementsTextAfterWaitingAsync(allMoreCommands);
            } return [];
        }

        public async Task OpenInNewWindowAsync(int timeToCheckIfFrameExists = 1000)
        {
            var openInNewWindowLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.OpenInNewWindow, timeToCheckIfFrameExists);
            await ClickAsync(openInNewWindowLocator);
        }

        public async Task RecordSetNavigatorAsync(int timeToCheckIfFrameExists = 1000)
        {
            var recordSetNavigatorLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.RecordSetNavigator, timeToCheckIfFrameExists);
            await ClickAsync(recordSetNavigatorLocator);
        }

        public async Task ClickShareAsync(int timeToCheckIfFrameExists = 1000)
        {
            var shareLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.Share, timeToCheckIfFrameExists);
            await ClickAsync(shareLocator);
        }

        public async Task<List<string>> GetAllSharingTypesAsync(int timeToCheckIfFrameExists = 1000)
        {
            await ClickShareAsync(timeToCheckIfFrameExists);
            var allShareTypesLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.AllShareItems, timeToCheckIfFrameExists);
            return await GetAllElementsTextAfterWaitingAsync(allShareTypesLocator);
        }

        public async Task ClickSharingTypeAsync(string type, int timeToCheckIfFrameExists = 1000)
        {
            await ClickShareAsync(timeToCheckIfFrameExists);
            var shareTypeLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, CommandBarLocators.ShareType.Replace("[Name]", type), timeToCheckIfFrameExists);
            await ClickAsync(shareTypeLocator);
        }
    }
}
