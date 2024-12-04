using AutomateBizApps.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class ProcessCrossEntityFlyout : SharedPage
    {
        private IPage _page;

        public ProcessCrossEntityFlyout(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task Create()
        {
            await ClickButton("Create");
        }

        public async Task Close()
        {
            await ClickButton("Close");
        }

        public async Task<string> GetHeader()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, ProcessCrossEntityFlyoutLocators.Header));
        }

        public async Task<List<string>> GetItems()
        {
            return await GetAllElementsTextAfterWaiting(await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, ProcessCrossEntityFlyoutLocators.ProcessCrossEntityItems), 5000);
        }

        public async Task SelectItem(string item)
        {
            ILocator itemsLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, ProcessCrossEntityFlyoutLocators.ProcessCrossEntityItems);
            await SelectOption(itemsLocator, item);
        }
    }
}
