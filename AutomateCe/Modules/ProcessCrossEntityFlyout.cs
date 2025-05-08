using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class ProcessCrossEntityFlyout : SharedPage
    {
        private IPage _page;

        public ProcessCrossEntityFlyout(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task CreateAsync()
        {
            await ClickButtonAsync("Create");
        }

        public async Task CloseAsync()
        {
            await ClickButtonAsync("Close");
        }

        public async Task<string> GetHeaderAsync()
        {
            return await TextContentAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, ProcessCrossEntityFlyoutLocators.Header));
        }

        public async Task<List<string>> GetItemsAsync()
        {
            return await GetAllElementsTextAfterWaitingAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, ProcessCrossEntityFlyoutLocators.ProcessCrossEntityItems), 5000);
        }

        public async Task SelectItemAsync(string item)
        {
            ILocator itemsLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, ProcessCrossEntityFlyoutLocators.ProcessCrossEntityItems);
            await SelectOptionAsync(itemsLocator, item);
        }
    }
}
