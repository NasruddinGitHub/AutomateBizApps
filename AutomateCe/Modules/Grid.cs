using AutomateCe.Enums;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class Grid : SharedPage
    {
        private IPage _page;

        public Grid(IPage page) : base(page)
        {
            this._page = page;
        }
        public async Task ClickViewOpenerAsync()
        {
            var viewOpenerLocator = Locator(GridLocators.ViewOpener);
            await ClickAsync(viewOpenerLocator);
        }

        public async Task ClickSetDefaultViewAsync()
        {
            var setDefaultViewLocator = Locator(GridLocators.SetDefaultView);
            await ClickAsync(setDefaultViewLocator);
        }

        public async Task ClickSaveAsNewViewAsync()
        {
            var saveChangesAsNewViewLocator = Locator(GridLocators.SaveChangesAsNewView);
            await ClickAsync(saveChangesAsNewViewLocator);
        }

        public async Task ClickSaveChangesToCurrentViewAsync()
        {
            var saveChangesToCurrentViewLocator = Locator(GridLocators.SaveChangesToCurrentView);
            await ClickAsync(saveChangesToCurrentViewLocator);
        }

        public async Task ClickResetDefaultViewAsync()
        {
            var resetDefaultViewLocator = Locator(GridLocators.ResetDefaultView);
            await ClickAsync(resetDefaultViewLocator);
        }

        public async Task SwitchViewAsync(string viewName)
        {
            await ClickViewOpenerAsync();
            await SelectViewAsync(viewName);
        }

        public async Task SelectViewAsync(string viewName)
        {
            var viewNameLocator = Locator(GridLocators.ViewName.Replace("[Name]", viewName));
            await ClickAsync(viewNameLocator);
            await WaitUntilAppIsIdleAsync();
        }

        public async Task<string?> GetCurrentlySelectedViewAsync()
        {
            var viewOpenerLocator = Locator(GridLocators.CurrentlySelectedViewName);
            return await TextContentAsync(viewOpenerLocator);
        }

        public async Task<string?> GetDefaultViewNameAsync()
        {
            await ClickViewOpenerAsync();
            var defaultViewNameLocator = Locator(GridLocators.DefaultViewName);
            return await TextContentAsync(defaultViewNameLocator);
        }

        public async Task<List<string?>> GetAllViewNamesAsync()
        {
            await ClickViewOpenerAsync();
            var allViewNamesLocator = Locator(GridLocators.AllViewNames);
            return await GetAllElementsTextAfterWaitingAsync(allViewNamesLocator);
        }

        public async Task<int> GetNumberOfViewsAsync()
        {
            List<string?> allViews = await GetAllViewNamesAsync();
            return allViews.Count;
        }
        public async Task SetDefaultViewAsync(string viewName)
        {
            await SwitchViewAsync(viewName);
            await ClickViewOpenerAsync();
            await ClickSetDefaultViewAsync();
        }

        public async Task SaveChangesAsNewViewAsync(string viewName)
        {
            await ClickViewOpenerAsync();
            await ClickSaveAsNewViewAsync();
            await SetValueByLabelNameAsync("Name", viewName, FormContextType.Dialog);
            await ClickButtonAsync("Save");
        }

        public async Task SaveChangesAsNewView(string viewName, string viewDescription)
        {
            await ClickViewOpenerAsync();
            await ClickSaveAsNewViewAsync();
            await SetValueByLabelNameAsync("Name", viewName, FormContextType.Dialog);
            await SetValueByLabelNameAsync("Description", viewDescription, FormContextType.Dialog);
            await ClickButtonAsync("Save");
        }

        public async Task SaveChangesToCurrentViewAsync()
        {
            await ClickViewOpenerAsync();
            await ClickSaveAsNewViewAsync();
        }

        public async Task ResetDefaultViewAsync()
        {
            await ClickViewOpenerAsync();
            await ClickResetDefaultViewAsync();
        }

        public async Task SearchViewAsync(string inputToSearch)
        {
            var searchViewLocator = Locator(GridLocators.SearchView);
            await FillAsync(searchViewLocator, inputToSearch);
        }

        public async Task<bool> IsSetDefaultViewDisplayedAsync()
        {
            var setDefaultViewLocator = Locator(GridLocators.SetDefaultView);
            return await IsVisibleAsyncWithWaiting(setDefaultViewLocator, 0);
        }

        public async Task<bool> IsResetDefaultViewDisplayedAsync()
        {
            var resetDefaultViewLocator = Locator(GridLocators.ResetDefaultView);
            return await IsVisibleAsyncWithWaiting(resetDefaultViewLocator, 0);
        }

        public async Task<bool> IsManageAndShareViewsDisplayedAsync()
        {
            var manageAndShareViewsLocator = Locator(GridLocators.ManageAndShareViews);
            return await IsVisibleAsyncWithWaiting(manageAndShareViewsLocator, 0);
        }

        public async Task SearchByKeywordAsync(string inputToFilter)
        {
            var filterByKeywordLocator = Locator(GridLocators.FilterByKeyword);
            await FillAsync(filterByKeywordLocator, inputToFilter);
            await KeyboardPressAsync("Enter");
        }

        public async Task ClearSearchAsync()
        {
            var clearFilterLocator = Locator(GridLocators.ClearFilter);
            await ClickAsync(clearFilterLocator);
        }

        public async Task FilterByAsync(string column, string filterType, string inputToFilter)
        {
            await SortColumnAsync(column, "Filter by");
            await SelectDropdownOptionAsync(GridLocators.FilterByChevronDown, GridLocators.FilterByOptions, filterType);
            await FillFilterByValueAsync(inputToFilter);
            await ApplyFilterAsync();
        }

        public async Task SelectFilterOperatorAsync(string filterType)
        {
            await SelectDropdownOptionAsync(GridLocators.FilterByChevronDown, GridLocators.FilterByOptions, filterType);
        }

        public async Task ClearColumnFilterAsync(string column)
        {
            await SortColumnAsync(column, "Clear filter");
        }

        public async Task<int> GetDisplayedRowsCountAsync()
        {
            string displayedRowsCount = await TextContentAsync(Locator(GridLocators.DisplayedRowsCount));
            displayedRowsCount = displayedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(displayedRowsCount);
        }

        public async Task<int> GetSelectedRowsCountAsync()
        {
            string selectedRowsCount = await TextContentAsync(Locator(GridLocators.SelectedRowsCount));
            selectedRowsCount = selectedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(selectedRowsCount);
        }

        public async Task<bool> IsColumnFilteredAsync(string column)
        {
            var locator = Locator(GridLocators.HeaderFiltered.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task<bool> IsColumnSortedAscendingOrderAsync(string column)
        {
            var locator = Locator(GridLocators.HeaderSortUp.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task<bool> IsColumnSortedDescendingOrderAsync(string column)
        {
            var locator = Locator(GridLocators.HeaderSortDown.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task FillFilterByValueAsync(string input)
        {
            await FillAsync(Locator(GridLocators.FilterByValue), input);
        }

        public async Task ApplyFilterAsync()
        {
            var applyFilterLocator = Locator(GridLocators.ApplyFilter);
            await ClickAsync(applyFilterLocator);
        }

        public async Task ClearFilterValueAsync()
        {
            await ClickAsync(Locator(GridLocators.ClearFilterValue));
        }

        public async Task SelectAllRecordsAsync()
        {
            var selectAllRowsLocator = Locator(GridLocators.SelectAllRows);
            await ClickAsync(selectAllRowsLocator);
        }

        public async Task SelectRecordAsync(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            int numberOfRows = await GetDisplayedRowsCountAsync();
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, numberOfRows);
            var rowSelectorLocator = Locator(GridLocators.RowSelector.Replace("[Index]", index.ToString()));

            await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, GridLocators.Grid));
            await ScrollUsingMouseUntilElementIsVisibleAsync(rowSelectorLocator, 0, 100, 300);
            await ClickAsync(rowSelectorLocator);
        }

        public async Task OpenRecordAsync(string recordName)
        {
            var rowLinkLocator = LocatorWithXpath(GridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task<int> GetRowIndex(Dictionary<string, string> record)
        {
            int rows = await GetDisplayedRowsCountAsync();
            List<string> columns = record.Keys.ToList();
            int rowIndex = 0;
            bool isMatchedRowShown = false;
            for (int i = 0; i < rows; i++)
            {
                int counter = 0;
                for (int j = 0; j < columns.Count; j++)
                {
                    string cellValue = await GetCellValue(i, columns[j]);
                    if (record[columns[j]].Equals(cellValue))
                    {
                        counter++;
                    }
                }
                if (counter == columns.Count)
                {
                    isMatchedRowShown = true;
                    break;
                }
                rowIndex++;
            }
            if (!isMatchedRowShown)
            {
                throw new ArgumentException("Not able to find row with the given data.");
            }
            return rowIndex;
        }

        public async Task OpenRecordAsync(Dictionary<string, string> record)
        {
            int index = await GetRowIndex(record);
            await OpenRecordAsync(index);
        }

        public async Task OpenRecordAsync(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            int numberOfRows = await GetDisplayedRowsCountAsync();
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, numberOfRows);
            var cellLocatorToOpenRecord = LocatorWithXpath(GridLocators.FirstCell.Replace("[Index]", index.ToString()));

            await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, GridLocators.Grid));
            await ScrollUsingMouseUntilElementIsVisibleAsync(cellLocatorToOpenRecord, 0, 100, 300);

            await DoubleClickAsync(cellLocatorToOpenRecord);
        }

        public async Task OpenRecordAsync(int index, string recordName)
        {
            var rowLocator = LocatorWithXpath(GridLocators.Row.Replace("[Index]", index.ToString()));
            var rowLinkLocator = LocatorWithXpath(rowLocator, GridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task<List<string>> GetAllDisplayedColumnNamesAsync()
        {
            var allHeaderCellsLocator = Locator(GridLocators.AllHeaderCells);
            List<string> allHeaderValues = await GetAllElementsTextAfterWaitingAsync(allHeaderCellsLocator);
            List<string> allHeaderValuesAfterTrimming = new List<string>();
            foreach (var column in allHeaderValues)
            {
                allHeaderValuesAfterTrimming.Add(column.Substring(0, column.Length / 2));
            }
            return allHeaderValuesAfterTrimming;
        }

        public async Task<int> GetNumberOfDisplayedColumnsAsync()
        {
            var allHeaderCellsLocator = Locator(GridLocators.AllHeaderCells);
            List<string> allHeaderCells = await GetAllElementsTextAfterWaitingAsync(allHeaderCellsLocator);
            return allHeaderCells.Count;
        }

        public async Task ClickColumnHeaderAsync(string columnName)
        {
            var headerColumnLocator = Locator(GridLocators.HeaderChevronDownIcon.Replace("[Name]", columnName));
            await ClickAsync(headerColumnLocator);
        }

        public async Task SortColumnAsync(string columnName, string filterOption)
        {
            await ClickColumnHeaderAsync(columnName);
            var headerContextMenuOptions = Locator(GridLocators.HeaderContextOptions.Replace("[Name]", filterOption));
            await ClickAsync(headerContextMenuOptions);
        }

        public async Task WaitUntilGridIsShownAsync()
        {
            await WaitForSelectorAsync(GridLocators.Grid, new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });
        }

        public async Task MoveColumnAsync(string columnName, string moveDirection)
        {
            await SortColumnAsync(columnName, moveDirection);
        }

        public async Task<string?> GetColumnWidthAsync()
        {
            var columnWidthInputLocator = Locator(GridLocators.ColumnWidthInput);
            return await InputValueAsync(columnWidthInputLocator);
        }

        private async Task SetPreferredColumnWidthAsync(int inputColumnWidth)
        {
            string? currentColumnWidth = await GetColumnWidthAsync();
            bool isInputLess = inputColumnWidth < Convert.ToInt32(currentColumnWidth);
            if (isInputLess)
            {
                for (int i = 0; ; i++)
                {
                    currentColumnWidth = await GetColumnWidthAsync();
                    if (Convert.ToInt32(currentColumnWidth) <= inputColumnWidth)
                    {
                        return;
                    }
                    await ClickAsync(Locator(GridLocators.ColumnWidthDownArrow));
                }
            }
            else
            {
                for (int i = 0; ; i++)
                {
                    currentColumnWidth = await GetColumnWidthAsync();
                    if (Convert.ToInt32(currentColumnWidth) >= inputColumnWidth)
                    {
                        return;
                    }
                    await ClickAsync(Locator(GridLocators.ColumnWidthUpArrow));
                }
            }
        }

        public async Task CloseFilterByDialogAsync()
        {
            await ClickAsync(Locator(GridLocators.CloseFilterByDialog));
        }

        public async Task SetColumnWidthAsync(string columnName, int columnWidth)
        {
            await SortColumnAsync(columnName, "Column width");
            await SetPreferredColumnWidthAsync(columnWidth);
            await CloseFilterByDialogAsync();
        }

        public async Task<List<string>> GetRowValuesAsListAsync(int rowIndex)
        {
            var rowLocator = LocatorWithXpath(GridLocators.Row.Replace("[Index]", rowIndex.ToString()));
            List<string> cellsValues = await GetAllElementsTextAfterWaitingAsync(LocatorWithXpath(rowLocator, GridLocators.RowCells));
            List<string> cellValuesAfterTrimming = new List<string>();
            foreach (string cell in cellsValues)
            {
                cellValuesAfterTrimming.Add(cell.Substring(0, (cell.Length / 2)));
            }
            return cellValuesAfterTrimming;
        }

        public async Task<List<List<string>>> GetRowValuesAsListAsync(int startIndex, int endIndex)
        {
            List<List<string>> rowValues = new List<List<string>>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                rowValues.Add(await GetRowValuesAsListAsync(i));
            }
            return rowValues;
        }

        public async Task<Dictionary<string, string>> GetRowValuesAsDictAsync(int rowIndex)
        {
            List<string> rowValues = await GetRowValuesAsListAsync(rowIndex);
            List<string> headerValues = await GetAllDisplayedColumnNamesAsync();
            return headerValues.Zip(rowValues).ToDictionary(x => x.First, x => x.Second);
        }

        public async Task<List<Dictionary<string, string>>> GetRowValuesAsDictAsync(int startIndex, int endIndex)
        {
            List<Dictionary<string, string>> allRowValuesWithHeaders = new List<Dictionary<string, string>>();

            List<List<string>> allRowValues = await GetRowValuesAsListAsync(startIndex, endIndex);
            List<string> headerValues = await GetAllDisplayedColumnNamesAsync();
            foreach (List<string> eachRowValues in allRowValues)
            {
                allRowValuesWithHeaders.Add(headerValues.Zip(eachRowValues).ToDictionary(x => x.First, x => x.Second));
            }
            return allRowValuesWithHeaders;
        }

        public async Task<int> GetColumnIndex(string columnName)
        {
            List<string> columns = await GetAllDisplayedColumnNamesAsync();
            int number = 0;
            bool isColumExisted = false;
            foreach (var item in columns)
            {
                if (item.Equals(columnName))
                {
                    isColumExisted = true;
                    break;
                }
                number++;
            }
            if (!isColumExisted)
            {
                throw new ArgumentException($"{columnName} is not available in the grid.");
            }
            return number;
        }

        public async Task<string> GetCellValue(int rowIndex, string columnName)
        {
            int columnIndex = await GetColumnIndex(columnName) + 2;
            ILocator cellLocator = Locator(GridLocators.Cell.Replace("[RowIndex]", rowIndex.ToString()).Replace("[ColumnIndex]", columnIndex.ToString()));
            await HoverAsync(await GetLocatorWhenInFramesNotInFramesAsync(CommonLocators.FocusedViewFrame, GridLocators.Grid));
            await ScrollUsingMouseUntilElementIsVisibleAsync(cellLocator, 0, 100, 300);
            return await InnerTextAsync(cellLocator);
        }

    }
}
