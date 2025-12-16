using AutomateCe.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class SubgridCommandBar : SharedPage
    {
        private IPage _page;

        public SubgridCommandBar(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task ClickCommandAsync(string subgridName, string name, int timeToCheckIfFrameExists = 1000, string subName = null, string subSecondName = null)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var commandLocator =  LocatorWithXpath(gridLocator, SubgridLocators.CommandItem.Replace("[Name]", name));
            try
            {
                await ClickAsync(commandLocator, new LocatorClickOptions { Timeout = 7000 });
                await WaitUntilAppIsIdleAsync();
            }
            catch (TimeoutException ex)
            {
                await ClickMoreCommandEllipsisAsync(subgridName, timeToCheckIfFrameExists);
                var subNameLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.CommandItem.Replace("[Name]", name), timeToCheckIfFrameExists);
                await ClickAsync(subNameLocator);
            }

            if (!string.IsNullOrEmpty(subName))
            {
                var subNameLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.CommandItem.Replace("[Name]", subName), timeToCheckIfFrameExists);
                await ClickAsync(subNameLocator);
            }

            if (!string.IsNullOrEmpty(subSecondName))
            {
                var secondSubNameLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.CommandItem.Replace("[Name]", subSecondName), timeToCheckIfFrameExists);
                await ClickAsync(secondSubNameLocator);
            }

        }

        public async Task ClickMoreCommandEllipsisAsync(string subgridName, int timeToCheckIfFrameExists = 1000)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var moreCommandsLocator = LocatorWithXpath(gridLocator, SubgridLocators.MoreCommandsEllipsis);
            await ClickAsync(moreCommandsLocator);
        }

        public async Task<List<string>> GetAllShownCommandsAsync(string subgridName, int timeToCheckIfFrameExists = 1000)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allShownCommands = LocatorWithXpath(gridLocator, SubgridLocators.AllShownCommands);
            return await GetAllElementsTextAfterWaitingAsync(allShownCommands);
        }

        public async Task<List<string>> GetAllMoreCommandsAsync(string subgridName, int timeToCheckIfFrameExists = 1000)
        {
            await ClickMoreCommandEllipsisAsync(subgridName, timeToCheckIfFrameExists);
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allMoreCommands = LocatorWithXpath(gridLocator, SubgridLocators.AllShownCommands);
            return await GetAllElementsTextAfterWaitingAsync(allMoreCommands);
        }
    }
}
