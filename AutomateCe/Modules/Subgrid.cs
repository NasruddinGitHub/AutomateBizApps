using AutomateCe.Pages;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class Subgrid : BaseModule
    {
        private IPage _page;

        public SubgridCommandBar SubgridCommandBar => this.GetElement<SubgridCommandBar>(_page);

        public Subgrid(IPage page) : base(page)
        {
            this._page = page;
        }
        public T GetElement<T>(IPage page)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { page });
        }

        public async Task<bool> IsSubgridShownAsync(string subgridName)
        {
            ILocator locator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]",subgridName));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task<bool> IsSearchboxShownAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator searchboxLocator = LocatorWithXpath(gridLocator, SubgridLocators.SearchBox);
            return await IsVisibleAsyncWithWaiting(searchboxLocator, 0);
        }

        public async Task<bool> IsViewSelectorShownAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            return await IsVisibleAsyncWithWaiting(selectedViewLocator, 0);
        }

        public async Task<string> GetSelectedViewNameAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            return await TextContentAsync(selectedViewLocator);
        }

        public async Task SelectViewAsync(string subgridName, string viewName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            ILocator viewLocatorToBeSelected = LocatorWithXpath(SubgridLocators.View.Replace("[Name]", viewName));
            await ClickAsync(selectedViewLocator);
            await ClickAsync(viewLocatorToBeSelected);
        }

        public async Task<List<string>> GetAllViewsAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            ILocator viewLocatorToBeSelected = LocatorWithXpath(SubgridLocators.AllViews);
            await ClickAsync(selectedViewLocator);
            List<string> allElementsText = await GetAllElementsTextAfterWaitingAsync(viewLocatorToBeSelected);
            await ClickAsync(selectedViewLocator);
            return allElementsText;
        }

        public async Task SearchAsync(string subgridName, string textToSearch)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator searchLocator = LocatorWithXpath(gridLocator, SubgridLocators.SearchBox);
            await FillAsync(searchLocator, textToSearch);
            await KeyboardPressAsync("Enter");
        }

        public async Task<int> GetDisplayedRowsCountAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator rowCountLocator = LocatorWithXpath(gridLocator, SubgridLocators.DisplayedRowsCount);
            string displayedRowsCount = await TextContentAsync(rowCountLocator);
            displayedRowsCount = displayedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(displayedRowsCount);
        }

        public async Task<int> GetSelectedRowsCountAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedCountLocator = LocatorWithXpath(gridLocator, SubgridLocators.SelectedRowsCount);
            string selectedRowsCount = await TextContentAsync(selectedCountLocator);
            selectedRowsCount = selectedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(selectedRowsCount);
        }

        public async Task<bool> IsColumnFilteredAsync(string subgridName, string column)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator headerFilteredLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderFiltered.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(headerFilteredLocator, 0);
        }

        public async Task<bool> IsColumnSortedAscendingOrderAsync(string subgridName, string column)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator headerSortUpLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderSortUp.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(headerSortUpLocator, 0);
        }

        public async Task<bool> IsColumnSortedDescendingOrderAsync(string subgridName, string column)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator headerSortDownLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderSortDown.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(headerSortDownLocator, 0);
        }

        public async Task FillFilterByValueAsync(string input)
        {
            ILocator filterByValueLocator = LocatorWithXpath(SubgridLocators.FilterByValue);
            await FillAsync(filterByValueLocator, input);
        }

        public async Task ClickColumnHeaderAsync(string subgridName, string columnName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var headerColumnLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderChevronDownIcon.Replace("[Name]", columnName));
            await ClickAsync(headerColumnLocator);
        }

        public async Task SortColumnAsync(string subgridName, string columnName, string filterOption)
        {
            await ClickColumnHeaderAsync(subgridName, columnName);
            var headerContextMenuOptions = LocatorWithXpath(SubgridLocators.HeaderContextOptions.Replace("[Name]", filterOption));
            await ClickAsync(headerContextMenuOptions);
        }

        public async Task FilterByAsync(string subgridName, string column, string filterType, string inputToFilter)
        {
            await SortColumnAsync(subgridName, column, "Filter by");
            await SelectFilterOperatorAsync(filterType);
            await FillFilterByValueAsync(inputToFilter);
            await ApplyFilterAsync();
        }

        public async Task SelectFilterOperatorAsync(string filterType)
        {
            await SelectDropdownOptionAsync(SubgridLocators.FilterByChevronDown, SubgridLocators.FilterByOptions, filterType);
        }

        public async Task ClearColumnFilterAsync(string subgridName, string column)
        {
            await SortColumnAsync(subgridName, column, "Clear filter");
        }

        public async Task ApplyFilterAsync()
        {
            var applyFilterLocator = Locator(SubgridLocators.ApplyFilter);
            await ClickAsync(applyFilterLocator);
        }

        public async Task ClearFilterValueAsync()
        {
            await ClickAsync(Locator(SubgridLocators.ClearFilterValue));
        }

        public async Task SelectAllRecordsAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var selectAllRowsLocator = LocatorWithXpath(gridLocator, SubgridLocators.SelectAllRows);
            await ClickAsync(selectAllRowsLocator);
        }

        public async Task SelectRecordAsync(string subgridName, int index)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowSelectorLocator = LocatorWithXpath(gridLocator, SubgridLocators.RowSelector);
            await ClickAsync(rowSelectorLocator);
        }

        public async Task OpenRecordAsync(string subgridName, int index, string recordName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowLocator = LocatorWithXpath(gridLocator, SubgridLocators.Row.Replace("[Index]", index.ToString()));
            var rowLinkLocator = LocatorWithXpath(rowLocator, SubgridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task OpenRecordAsync(string subgridName, string recordName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowLinkLocator = LocatorWithXpath(gridLocator, SubgridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task<List<string>> GetAllDisplayedColumnNamesAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allHeaderCellsLocator = LocatorWithXpath(gridLocator, SubgridLocators.AllHeaderCells);
            List<string> allHeaderValues = await GetAllElementsTextAfterWaitingAsync(allHeaderCellsLocator);
            List<string> allHeaderValuesAfterTrimming = new List<string>();
            foreach (var column in allHeaderValues)
            {
                allHeaderValuesAfterTrimming.Add(column.Substring(0, column.Length / 2));
            }
            return allHeaderValuesAfterTrimming;
        }

        public async Task<int> GetNumberOfDisplayedColumnsAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allHeaderCellsLocator = LocatorWithXpath(gridLocator, SubgridLocators.AllHeaderCells);
            List<string> allHeaderCells = await GetAllElementsTextAfterWaitingAsync(allHeaderCellsLocator);
            return allHeaderCells.Count;
        }

        public async Task MoveColumnAsync(string subgridName, string columnName, string moveDirection)
        {
            await SortColumnAsync(subgridName, columnName, moveDirection);
        }

        public async Task<string?> GetColumnWidthAsync(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var columnWidthInputLocator = LocatorWithXpath(gridLocator, SubgridLocators.ColumnWidthInput);
            return await InputValueAsync(columnWidthInputLocator);
        }

        public async Task CloseFilterByDialogAsync()
        {
            await ClickAsync(Locator(SubgridLocators.CloseFilterByDialog));
        }

        public async Task<List<string>> GetRowValuesAsListAsync(string subgridName, int rowIndex)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowLocator = LocatorWithXpath(gridLocator, SubgridLocators.Row.Replace("[Index]", rowIndex.ToString()));
            List<string> cellsValues = await GetAllElementsTextAfterWaitingAsync(LocatorWithXpath(rowLocator, GridLocators.RowCells));
            List<string> cellValuesAfterTrimming = new List<string>();
            foreach (string cell in cellsValues)
            {
                cellValuesAfterTrimming.Add(cell.Substring(0, (cell.Length / 2)));
            }
            return cellValuesAfterTrimming;
        }


        public async Task<List<List<string>>> GetRowValuesAsListAsync(string subgridName, int startIndex, int endIndex)
        {
            List<List<string>> rowValues = new List<List<string>>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                rowValues.Add(await GetRowValuesAsListAsync(subgridName, i));
            }
            return rowValues;
        }

        public async Task<Dictionary<string, string>> GetRowValuesAsDictAsync(string subgridName, int rowIndex)
        {
            List<string> rowValues = await GetRowValuesAsListAsync(subgridName, rowIndex);
            List<string> headerValues = await GetAllDisplayedColumnNamesAsync(subgridName);
            return headerValues.Zip(rowValues).ToDictionary(x => x.First, x => x.Second);
        }


        public async Task<List<Dictionary<string, string>>> GetRowValuesAsDictAsync(string subgridName, int startIndex, int endIndex)
        {
            List<Dictionary<string, string>> allRowValuesWithHeaders = new List<Dictionary<string, string>>();

            List<List<string>> allRowValues = await GetRowValuesAsListAsync(subgridName, startIndex, endIndex);
            List<string> headerValues = await GetAllDisplayedColumnNamesAsync(subgridName);
            foreach (List<string> eachRowValues in allRowValues)
            {
                allRowValuesWithHeaders.Add(headerValues.Zip(eachRowValues).ToDictionary(x => x.First, x => x.Second));
            }
            return allRowValuesWithHeaders;
        }

        public async Task SetColumnWidthAsync(string subgridName, string columnName, int columnWidth)
        {
            await SortColumnAsync(subgridName, columnName, "Column width");
            await SetPreferredColumnWidthAsync(subgridName, columnWidth);
            await CloseFilterByDialogAsync();
        }

        private async Task SetPreferredColumnWidthAsync(string subgridName, int inputColumnWidth)
        {
            string? currentColumnWidth = await GetColumnWidthAsync(subgridName);
            bool isInputLess = inputColumnWidth < Convert.ToInt32(currentColumnWidth);
            if (isInputLess)
            {
                for (int i = 0; ; i++)
                {
                    currentColumnWidth = await GetColumnWidthAsync(subgridName);
                    if (Convert.ToInt32(currentColumnWidth) <= inputColumnWidth)
                    {
                        return;
                    }
                    await ClickAsync(Locator(SubgridLocators.ColumnWidthDownArrow));
                }
            }
            else
            {
                for (int i = 0; ; i++)
                {
                    currentColumnWidth = await GetColumnWidthAsync(subgridName);
                    if (Convert.ToInt32(currentColumnWidth) >= inputColumnWidth)
                    {
                        return;
                    }
                    await ClickAsync(Locator(SubgridLocators.ColumnWidthUpArrow));
                }
            }
        }
    }
}
