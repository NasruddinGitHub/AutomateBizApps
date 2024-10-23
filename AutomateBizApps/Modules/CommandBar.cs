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

        public async Task ClickCommand(string name, string subName = null, string subSecondName = null)
        {
            var commandLocator = Locator(CommandBarLocators.CommandItem.Replace("[Name]", name));
            try
            {
                await ClickAsync(commandLocator, new LocatorClickOptions { Timeout = 7000 });
            }
            catch (TimeoutException ex)
            {
                await ClickMoreCommandEllipsis();
                await ClickAsync(commandLocator);
            }

            if (!string.IsNullOrEmpty(subName))
            {
                var subNameLocator = Locator(CommandBarLocators.CommandItem.Replace("[Name]", subName));
                await ClickAsync(subNameLocator);
            }

            if (!string.IsNullOrEmpty(subSecondName))
            {
                var secondSubNameLocator = Locator(CommandBarLocators.CommandItem.Replace("[Name]", subSecondName));
                await ClickAsync(secondSubNameLocator);
            }

        }

        public async Task ClickMoreCommandEllipsis()
        {
            var moreCommandsLocator = Locator(CommandBarLocators.MoreCommandsEllipsis);
            await ClickAsync(moreCommandsLocator);
        }

        public async Task<List<string>> GetAllShownCommands()
        {
            var allShownCommands = Locator(CommandBarLocators.AllShownCommands);
            return await GetAllElementsTextAfterWaiting(allShownCommands);
        }

        public async Task<List<string>> GetAllMoreCommands()
        {
            await ClickMoreCommandEllipsis();
            var allMoreCommands = Locator(CommandBarLocators.AllMoreCommands);
            return await GetAllElementsTextAfterWaiting(allMoreCommands);
        }

        public async Task OpenInNewWindow()
        {
            var openInNewWindowLocator = Locator(CommandBarLocators.OpenInNewWindow);
            await ClickAsync(openInNewWindowLocator);
        }

        public async Task RecordSetNavigator()
        {
            var recordSetNavigatorLocator = Locator(CommandBarLocators.RecordSetNavigator);
            await ClickAsync(recordSetNavigatorLocator);
        }

        public async Task ClickShare()
        {
            var shareLocator = Locator(CommandBarLocators.Share);
            await ClickAsync(shareLocator);
        }

        public async Task<List<string>> GetAllSharingTypes()
        {
            await ClickShare();
            var allShareTypesLocator = Locator(CommandBarLocators.AllShareItems);
            return await GetAllElementsTextAfterWaiting(allShareTypesLocator);
        }

        public async Task ClickSharingType(string type)
        {
            await ClickShare();
            var shareTypeLocator = Locator(CommandBarLocators.ShareType.Replace("[Name]",type));
            await ClickAsync(shareTypeLocator);
        }
    }
}
