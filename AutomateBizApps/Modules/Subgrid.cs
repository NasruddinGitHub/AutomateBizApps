using AutomateBizApps.Modules;
using AutomateBizApps.Pages;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class Subgrid : BaseModule
    {
        private IPage _page;

        public Subgrid(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task<bool> IsSubgridShown(string subgridName)
        {
            ILocator locator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]",subgridName));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task<bool> IsSearchboxShown(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator searchboxLocator = LocatorWithXpath(gridLocator, SubgridLocators.SearchBox);
            return await IsVisibleAsyncWithWaiting(searchboxLocator, 0);
        }

        public async Task<bool> IsViewSelectorShown(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            return await IsVisibleAsyncWithWaiting(selectedViewLocator, 0);
        }

        public async Task<string> GetSelectedViewName(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            return await TextContentAsync(selectedViewLocator);
        }

        public async Task SelectView(string subgridName, string viewName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            ILocator viewLocatorToBeSelected = LocatorWithXpath(SubgridLocators.View.Replace("[Name]", viewName));
            await ClickAsync(selectedViewLocator);
            await ClickAsync(viewLocatorToBeSelected);
        }

        public async Task<List<string>> GetAllViews(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedViewLocator = LocatorWithXpath(gridLocator, SubgridLocators.CurrentlySelectedView);
            ILocator viewLocatorToBeSelected = LocatorWithXpath(SubgridLocators.AllViews);
            await ClickAsync(selectedViewLocator);
            List<string> allElementsText = await GetAllElementsTextAfterWaiting(viewLocatorToBeSelected);
            await ClickAsync(selectedViewLocator);
            return allElementsText;
        }

        public async Task Search(string subgridName, string textToSearch)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator searchLocator = LocatorWithXpath(gridLocator, SubgridLocators.SearchBox);
            await FillAsync(searchLocator, textToSearch);
            await KeyboardPressAsync("Enter");
        }

        public async Task<int> GetDisplayedRowsCount(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator rowCountLocator = LocatorWithXpath(gridLocator, SubgridLocators.DisplayedRowsCount);
            string displayedRowsCount = await TextContentAsync(rowCountLocator);
            displayedRowsCount = displayedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(displayedRowsCount);
        }

        public async Task<int> GetSelectedRowsCount(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator selectedCountLocator = LocatorWithXpath(gridLocator, SubgridLocators.SelectedRowsCount);
            string selectedRowsCount = await TextContentAsync(selectedCountLocator);
            selectedRowsCount = selectedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(selectedRowsCount);
        }

        public async Task<bool> IsColumnFiltered(string subgridName, string column)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator headerFilteredLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderFiltered.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(headerFilteredLocator, 0);
        }

        public async Task<bool> IsColumnSortedAscendingOrder(string subgridName, string column)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator headerSortUpLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderSortUp.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(headerSortUpLocator, 0);
        }

        public async Task<bool> IsColumnSortedDescendingOrder(string subgridName, string column)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            ILocator headerSortDownLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderSortDown.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(headerSortDownLocator, 0);
        }

        public async Task FillFilterByValue(string input)
        {
            ILocator filterByValueLocator = LocatorWithXpath(SubgridLocators.FilterByValue);
            await FillAsync(filterByValueLocator, input);
        }

        public async Task ClickColumnHeader(string subgridName, string columnName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var headerColumnLocator = LocatorWithXpath(gridLocator, SubgridLocators.HeaderChevronDownIcon.Replace("[Name]", columnName));
            await ClickAsync(headerColumnLocator);
        }

        public async Task SortColumn(string subgridName, string columnName, string filterOption)
        {
            await ClickColumnHeader(subgridName, columnName);
            var headerContextMenuOptions = LocatorWithXpath(SubgridLocators.HeaderContextOptions.Replace("[Name]", filterOption));
            await ClickAsync(headerContextMenuOptions);
        }

        public async Task FilterBy(string subgridName, string column, string filterType, string inputToFilter)
        {
            await SortColumn(subgridName, column, "Filter by");
            await SelectFilterOperator(filterType);
            await FillFilterByValue(inputToFilter);
            await ApplyFilter();
        }

        public async Task SelectFilterOperator(string filterType)
        {
            await SelectDropdownOption(SubgridLocators.FilterByChevronDown, SubgridLocators.FilterByOptions, filterType);
        }

        public async Task ClearColumnFilter(string subgridName, string column)
        {
            await SortColumn(subgridName, column, "Clear filter");
        }

        public async Task ApplyFilter()
        {
            var applyFilterLocator = Locator(SubgridLocators.ApplyFilter);
            await ClickAsync(applyFilterLocator);
        }

        public async Task ClearFilterValue()
        {
            await ClickAsync(Locator(SubgridLocators.ClearFilterValue));
        }

        public async Task SelectAllRecords(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var selectAllRowsLocator = LocatorWithXpath(gridLocator, SubgridLocators.SelectAllRows);
            await ClickAsync(selectAllRowsLocator);
        }

        public async Task SelectRecord(string subgridName, int index)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowSelectorLocator = LocatorWithXpath(gridLocator, SubgridLocators.RowSelector);
            await ClickAsync(rowSelectorLocator);
        }

        public async Task OpenRecord(string subgridName, int index, string recordName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowLocator = LocatorWithXpath(gridLocator, SubgridLocators.Row.Replace("[Index]", index.ToString()));
            var rowLinkLocator = LocatorWithXpath(rowLocator, SubgridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task OpenRecord(string subgridName, string recordName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowLinkLocator = LocatorWithXpath(gridLocator, SubgridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task<List<string>> GetAllDisplayedColumnNames(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allHeaderCellsLocator = LocatorWithXpath(gridLocator, SubgridLocators.AllHeaderCells);
            List<string> allHeaderValues = await GetAllElementsTextAfterWaiting(allHeaderCellsLocator);
            List<string> allHeaderValuesAfterTrimming = new List<string>();
            foreach (var column in allHeaderValues)
            {
                allHeaderValuesAfterTrimming.Add(column.Substring(0, column.Length / 2));
            }
            return allHeaderValuesAfterTrimming;
        }

        public async Task<int> GetNumberOfDisplayedColumns(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var allHeaderCellsLocator = LocatorWithXpath(gridLocator, SubgridLocators.AllHeaderCells);
            List<string> allHeaderCells = await GetAllElementsTextAfterWaiting(allHeaderCellsLocator);
            return allHeaderCells.Count;
        }

        public async Task MoveColumn(string subgridName, string columnName, string moveDirection)
        {
            await SortColumn(subgridName, columnName, moveDirection);
        }

        public async Task<string?> GetColumnWidth(string subgridName)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var columnWidthInputLocator = LocatorWithXpath(gridLocator, SubgridLocators.ColumnWidthInput);
            return await InputValueAsync(columnWidthInputLocator);
        }

        public async Task CloseFilterByDialog()
        {
            await ClickAsync(Locator(SubgridLocators.CloseFilterByDialog));
        }

        public async Task<List<string>> GetRowValuesAsList(string subgridName, int rowIndex)
        {
            ILocator gridLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, SubgridLocators.SubgridRootContainer.Replace("[Name]", subgridName));
            var rowLocator = LocatorWithXpath(gridLocator, SubgridLocators.Row.Replace("[Index]", rowIndex.ToString()));
            List<string> cellsValues = await GetAllElementsTextAfterWaiting(LocatorWithXpath(rowLocator, GridLocators.RowCells));
            List<string> cellValuesAfterTrimming = new List<string>();
            foreach (string cell in cellsValues)
            {
                cellValuesAfterTrimming.Add(cell.Substring(0, (cell.Length / 2)));
            }
            return cellValuesAfterTrimming;
        }


        public async Task<List<List<string>>> GetRowValuesAsList(string subgridName, int startIndex, int endIndex)
        {
            List<List<string>> rowValues = new List<List<string>>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                rowValues.Add(await GetRowValuesAsList(subgridName, i));
            }
            return rowValues;
        }

        public async Task<Dictionary<string, string>> GetRowValuesAsDict(string subgridName, int rowIndex)
        {
            List<string> rowValues = await GetRowValuesAsList(subgridName, rowIndex);
            List<string> headerValues = await GetAllDisplayedColumnNames(subgridName);
            return headerValues.Zip(rowValues).ToDictionary(x => x.First, x => x.Second);
        }


        public async Task<List<Dictionary<string, string>>> GetRowValuesAsDict(string subgridName, int startIndex, int endIndex)
        {
            List<Dictionary<string, string>> allRowValuesWithHeaders = new List<Dictionary<string, string>>();

            List<List<string>> allRowValues = await GetRowValuesAsList(subgridName, startIndex, endIndex);
            List<string> headerValues = await GetAllDisplayedColumnNames(subgridName);
            foreach (List<string> eachRowValues in allRowValues)
            {
                allRowValuesWithHeaders.Add(headerValues.Zip(eachRowValues).ToDictionary(x => x.First, x => x.Second));
            }
            return allRowValuesWithHeaders;
        }

        public async Task SetColumnWidth(string subgridName, string columnName, int columnWidth)
        {
            await SortColumn(subgridName, columnName, "Column width");
            await SetPreferredColumnWidth(subgridName, columnWidth);
            await CloseFilterByDialog();
        }

        private async Task SetPreferredColumnWidth(string subgridName, int inputColumnWidth)
        {
            string? currentColumnWidth = await GetColumnWidth(subgridName);
            bool isInputLess = inputColumnWidth < Convert.ToInt32(currentColumnWidth);
            if (isInputLess)
            {
                for (int i = 0; ; i++)
                {
                    currentColumnWidth = await GetColumnWidth(subgridName);
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
                    currentColumnWidth = await GetColumnWidth(subgridName);
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
