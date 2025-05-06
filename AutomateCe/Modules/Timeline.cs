using AutomateCe.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class Timeline : SharedPage
    {
        private IPage _page;

        public Timeline(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task<bool> IsActivityShown(string subject)
        {
            ILocator recordContainer = Locator(TimelineLocators.TimelineRecordContainer.Replace("[SubjectContains]", subject));
            return await IsVisibleAsyncWithWaiting(recordContainer);
        }

        public async Task ExpandActivity(string subject)
        {
            ILocator expandActivity = Locator(Locator(TimelineLocators.TimelineRecordContainer.Replace("[SubjectContains]", subject)), TimelineLocators.ViewMore);
            await ClickAsync(expandActivity);
        }

        public async Task CollapseActivity(string subject)
        {
            ILocator collapseActivity = Locator(Locator(TimelineLocators.TimelineRecordContainer.Replace("[SubjectContains]", subject)), TimelineLocators.ViewLess);
            await ClickAsync(collapseActivity);
        }

        public async Task OpenActivity(string subject)
        {
            ILocator openActivity = Locator(Locator(TimelineLocators.TimelineRecordContainer.Replace("[SubjectContains]", subject)), TimelineLocators.OpenRecord);
            await ClickAsync(openActivity);
        }

        public async Task RefreshTimeline()
        {
            ILocator refresh = Locator(TimelineLocators.RefreshTimelineRecordsIcon);
            await ClickAsync(refresh);
        }

        public async Task ExpandAllRecord()
        {
            ILocator expandTimelineRecords = Locator(TimelineLocators.ExpandCollapseTimelineRecordsIcon);
            await ClickAsync(expandTimelineRecords);
        }

        public async Task CollapseAllRecord()
        {
            ILocator collapseTimelineRecords = Locator(TimelineLocators.ExpandCollapseTimelineRecordsIcon);
            await ClickAsync(collapseTimelineRecords);
        }

        public async Task SearchTimeline(string text)
        {
            ILocator searchTimelineBox = Locator(TimelineLocators.SearchTimelineBox);
            await FillAsync(searchTimelineBox, text);
            await KeyboardPressAsync("Tab");
        }

        public async Task ClearTimelineText()
        {
            ILocator timelineSearchClearIcon = Locator(TimelineLocators.TimelineSearchClearIcon);
            await ClickAsync(timelineSearchClearIcon);
        }
        public async Task<int> GetNumberOfTimelineRecords()
        {
            ILocator timelineRecords = Locator(TimelineLocators.TimelineRecords);
            List<ILocator> allTimelineRecords = await GetAllElementsAfterWaiting(timelineRecords);
            return allTimelineRecords.Count;
        }
    }
}
