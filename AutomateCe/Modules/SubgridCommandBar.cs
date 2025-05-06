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

        public async Task ClickCommand(string subgridName, string name, int timeToCheckIfFrameExists = 1000, string subName = null, string subSecondName = null)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var commandLocator =  LocatorWithXpath(gridLocator, SubgridLocators.CommandItem.Replace("[Name]", name));
            try
            {
                await ClickAsync(commandLocator, new LocatorClickOptions { Timeout = 7000 });
                await WaitUntilAppIsIdle();
            }
            catch (TimeoutException ex)
            {
                await ClickMoreCommandEllipsis(subgridName, timeToCheckIfFrameExists);
                var subNameLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, CommandBarLocators.CommandItem.Replace("[Name]", name), timeToCheckIfFrameExists);
                await ClickAsync(subNameLocator);
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

        public async Task ClickMoreCommandEllipsis(string subgridName, int timeToCheckIfFrameExists = 1000)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var moreCommandsLocator = LocatorWithXpath(gridLocator, SubgridLocators.MoreCommandsEllipsis);
            await ClickAsync(moreCommandsLocator);
        }

        public async Task<List<string>> GetAllShownCommands(string subgridName, int timeToCheckIfFrameExists = 1000)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allShownCommands = LocatorWithXpath(gridLocator, SubgridLocators.AllShownCommands);
            return await GetAllElementsTextAfterWaiting(allShownCommands);
        }

        public async Task<List<string>> GetAllMoreCommands(string subgridName, int timeToCheckIfFrameExists = 1000)
        {
            await ClickMoreCommandEllipsis(subgridName, timeToCheckIfFrameExists);
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allMoreCommands = LocatorWithXpath(gridLocator, SubgridLocators.AllShownCommands);
            return await GetAllElementsTextAfterWaiting(allMoreCommands);
        }
    }
}
