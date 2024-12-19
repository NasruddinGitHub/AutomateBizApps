using AutomateBizApps.Pages;
using AutomateCe.Enums;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateBizApps.ObjectRepository.ObjectRepository;

namespace AutomateBizApps.Modules
{
    public class Grid : SharedPage
    {
        private IPage _page;

        public Grid(IPage page) : base(page)
        {
            this._page = page;
        }
        public async Task ClickViewOpener()
        {
            var viewOpenerLocator = Locator(GridLocators.ViewOpener);
            await ClickAsync(viewOpenerLocator);
        }

        public async Task ClickSetDefaultView()
        {
            var setDefaultViewLocator = Locator(GridLocators.SetDefaultView);
            await ClickAsync(setDefaultViewLocator);
        }

        public async Task ClickSaveAsNewView()
        {
            var saveChangesAsNewViewLocator = Locator(GridLocators.SaveChangesAsNewView);
            await ClickAsync(saveChangesAsNewViewLocator);
        }

        public async Task ClickSaveChangesToCurrentView()
        {
            var saveChangesToCurrentViewLocator = Locator(GridLocators.SaveChangesToCurrentView);
            await ClickAsync(saveChangesToCurrentViewLocator);
        }

        public async Task ClickResetDefaultView()
        {
            var resetDefaultViewLocator = Locator(GridLocators.ResetDefaultView);
            await ClickAsync(resetDefaultViewLocator);
        }

        public async Task SwitchView(string viewName)
        {
            await ClickViewOpener();
            await SelectView(viewName);
        }

        public async Task SelectView(string viewName)
        {
            var viewNameLocator = Locator(GridLocators.ViewName.Replace("[Name]", viewName));
            await ClickAsync(viewNameLocator);
            await WaitUntilAppIsIdle();
        }

        public async Task<string?> GetCurrentlySelectedView()
        {
            var viewOpenerLocator = Locator(GridLocators.CurrentlySelectedViewName);
            return await TextContentAsync(viewOpenerLocator);
        }

        public async Task<string?> GetDefaultViewName()
        {
            await ClickViewOpener();
            var defaultViewNameLocator = Locator(GridLocators.DefaultViewName);
            return await TextContentAsync(defaultViewNameLocator);
        }

        public async Task<List<string?>> GetAllViewNames()
        {
            await ClickViewOpener();
            var allViewNamesLocator = Locator(GridLocators.AllViewNames);
            return await GetAllElementsTextAfterWaiting(allViewNamesLocator);
        }

        public async Task<int> GetNumberOfViews()
        {
            List<string?> allViews = await GetAllViewNames();
            return allViews.Count;
        }
        public async Task SetDefaultView(string viewName)
        {
            await SwitchView(viewName);
            await ClickViewOpener();
            await ClickSetDefaultView();
        }

        public async Task SaveChangesAsNewView(string viewName)
        {
            await ClickViewOpener();
            await ClickSaveAsNewView();
            await SetValue("Name", viewName, FormContextType.Dialog);
            await ClickButton("Save");
        }

        public async Task SaveChangesAsNewView(string viewName, string viewDescription)
        {
            await ClickViewOpener();
            await ClickSaveAsNewView();
            await SetValue("Name", viewName, FormContextType.Dialog);
            await SetValue("Description", viewDescription, FormContextType.Dialog);
            await ClickButton("Save");
        }

        public async Task SaveChangesToCurrentView()
        {
            await ClickViewOpener();
            await ClickSaveAsNewView();
        }

        public async Task ResetDefaultView()
        {
            await ClickViewOpener();
            await ClickResetDefaultView();
        }

        public async Task SearchView(string inputToSearch)
        {
            var searchViewLocator = Locator(GridLocators.SearchView);
            await FillAsync(searchViewLocator, inputToSearch);
        }

        public async Task<bool> IsSetDefaultViewDisplayed()
        {
            var setDefaultViewLocator = Locator(GridLocators.SetDefaultView);
            return await IsVisibleAsyncWithWaiting(setDefaultViewLocator, 0);
        }

        public async Task<bool> IsResetDefaultViewDisplayed()
        {
            var resetDefaultViewLocator = Locator(GridLocators.ResetDefaultView);
            return await IsVisibleAsyncWithWaiting(resetDefaultViewLocator, 0);
        }

        public async Task<bool> IsManageAndShareViewsDisplayed()
        {
            var manageAndShareViewsLocator = Locator(GridLocators.ManageAndShareViews);
            return await IsVisibleAsyncWithWaiting(manageAndShareViewsLocator, 0);
        }

        public async Task SearchByKeyword(string inputToFilter)
        {
            var filterByKeywordLocator = Locator(GridLocators.FilterByKeyword);
            await FillAsync(filterByKeywordLocator, inputToFilter);
            await KeyboardPressAsync("Enter");
        }

        public async Task ClearSearch()
        {
            var clearFilterLocator = Locator(GridLocators.ClearFilter);
            await ClickAsync(clearFilterLocator);
        }

        public async Task FilterBy(string column, string filterType, string inputToFilter)
        {
            await SortColumn(column, "Filter by");
            await SelectDropdownOption(GridLocators.FilterByChevronDown, GridLocators.FilterByOptions, filterType);
            await FillFilterByValue(inputToFilter);
            await ApplyFilter();
        }

        public async Task SelectFilterOperator(string filterType)
        {
            await SelectDropdownOption(GridLocators.FilterByChevronDown, GridLocators.FilterByOptions, filterType);
        }

        public async Task ClearColumnFilter(string column)
        {
            await SortColumn(column, "Clear filter");
        }

        public async Task<int> GetDisplayedRowsCount()
        {
            string displayedRowsCount = await TextContentAsync(Locator(GridLocators.DisplayedRowsCount));
            displayedRowsCount = displayedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(displayedRowsCount);
        }

        public async Task<int> GetSelectedRowsCount()
        {
            string selectedRowsCount = await TextContentAsync(Locator(GridLocators.SelectedRowsCount));
            selectedRowsCount = selectedRowsCount.Split(':')[1].Trim();
            return Convert.ToInt32(selectedRowsCount);
        }

        public async Task<bool> IsColumnFiltered(string column)
        {
            var locator = Locator(GridLocators.HeaderFiltered.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task<bool> IsColumnSortedAscendingOrder(string column)
        {
            var locator = Locator(GridLocators.HeaderSortUp.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task<bool> IsColumnSortedDescendingOrder(string column)
        {
            var locator = Locator(GridLocators.HeaderSortDown.Replace("[Name]", column));
            return await IsVisibleAsyncWithWaiting(locator, 0);
        }

        public async Task FillFilterByValue(string input)
        {
            await FillAsync(Locator(GridLocators.FilterByValue), input);
        }

        public async Task ApplyFilter()
        {
            var applyFilterLocator = Locator(GridLocators.ApplyFilter);
            await ClickAsync(applyFilterLocator);
        }

        public async Task ClearFilterValue()
        {
            await ClickAsync(Locator(GridLocators.ClearFilterValue));
        }

        public async Task SelectAllRecords()
        {
            var selectAllRowsLocator = Locator(GridLocators.SelectAllRows);
            await ClickAsync(selectAllRowsLocator);
        }

        public async Task SelectRecord(int index)
        {
            var rowSelectorLocator = Locator(GridLocators.RowSelector).Nth(index);
            await ClickAsync(rowSelectorLocator);
        }

        public async Task OpenRecord(string recordName)
        {
            var rowLinkLocator = LocatorWithXpath(GridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task OpenRecord(int index, string recordName)
        {
            var rowLocator = LocatorWithXpath(GridLocators.Row.Replace("[Index]", index.ToString()));
            var rowLinkLocator = LocatorWithXpath(rowLocator, GridLocators.RowLink.Replace("[Name]", recordName));
            await ClickAsync(rowLinkLocator);
        }

        public async Task<List<string>> GetAllDisplayedColumnNames()
        {
            var allHeaderCellsLocator = Locator(GridLocators.AllHeaderCells);
            List<string> allHeaderValues = await GetAllElementsTextAfterWaiting(allHeaderCellsLocator);
            List<string> allHeaderValuesAfterTrimming = new List<string>();
            foreach (var column in allHeaderValues)
            {
                allHeaderValuesAfterTrimming.Add(column.Substring(0, column.Length / 2));
            }
            return allHeaderValuesAfterTrimming;
        }

        public async Task<int> GetNumberOfDisplayedColumns()
        {
            var allHeaderCellsLocator = Locator(GridLocators.AllHeaderCells);
            List<string> allHeaderCells = await GetAllElementsTextAfterWaiting(allHeaderCellsLocator);
            return allHeaderCells.Count;
        }

        public async Task ClickColumnHeader(string columnName)
        {
            var headerColumnLocator = Locator(GridLocators.HeaderChevronDownIcon.Replace("[Name]", columnName));
            await ClickAsync(headerColumnLocator);
        }

        public async Task SortColumn(string columnName, string filterOption)
        {
            await ClickColumnHeader(columnName);
            var headerContextMenuOptions = Locator(GridLocators.HeaderContextOptions.Replace("[Name]", filterOption));
            await ClickAsync(headerContextMenuOptions);
        }

        public async Task WaitUntilGridIsShown()
        {
            await WaitForSelectorAsync(GridLocators.Grid, new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });
        }

        public async Task MoveColumn(string columnName, string moveDirection)
        {
            await SortColumn(columnName, moveDirection);
        }

        public async Task<string?> GetColumnWidth()
        {
            var columnWidthInputLocator = Locator(GridLocators.ColumnWidthInput);
            return await InputValueAsync(columnWidthInputLocator);
        }

        private async Task SetPreferredColumnWidth(int inputColumnWidth)
        {
            string? currentColumnWidth = await GetColumnWidth();
            bool isInputLess = inputColumnWidth < Convert.ToInt32(currentColumnWidth);
            if (isInputLess)
            {
                for (int i = 0; ; i++)
                {
                    currentColumnWidth = await GetColumnWidth();
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
                    currentColumnWidth = await GetColumnWidth();
                    if (Convert.ToInt32(currentColumnWidth) >= inputColumnWidth)
                    {
                        return;
                    }
                    await ClickAsync(Locator(GridLocators.ColumnWidthUpArrow));
                }
            }
        }

        public async Task CloseFilterByDialog()
        {
            await ClickAsync(Locator(GridLocators.CloseFilterByDialog));
        }

        public async Task SetColumnWidth(string columnName, int columnWidth)
        {
            await SortColumn(columnName, "Column width");
            await SetPreferredColumnWidth(columnWidth);
            await CloseFilterByDialog();
        }

        public async Task<List<string>> GetRowValuesAsList(int rowIndex)
        {
            var rowLocator = LocatorWithXpath(GridLocators.Row.Replace("[Index]", rowIndex.ToString()));
            List<string> cellsValues = await GetAllElementsTextAfterWaiting(LocatorWithXpath(rowLocator, GridLocators.RowCells));
            List<string> cellValuesAfterTrimming = new List<string>();
            foreach (string cell in cellsValues)
            {
                cellValuesAfterTrimming.Add(cell.Substring(0, (cell.Length/2)));
            }
            return cellValuesAfterTrimming;
        }

        public async Task<List<List<string>>> GetRowValuesAsList(int startIndex, int endIndex)
        {
            List<List<string> > rowValues = new List<List<string>> ();
            for (int i = startIndex; i <= endIndex; i++)
            {
                rowValues.Add(await GetRowValuesAsList(i));
            }
            return rowValues;
        }

        public async Task<Dictionary<string, string>> GetRowValuesAsDict(int rowIndex)
        {
            List<string> rowValues = await GetRowValuesAsList(rowIndex);
            List<string> headerValues = await GetAllDisplayedColumnNames();
            return headerValues.Zip(rowValues).ToDictionary(x => x.First, x => x.Second);
        }

        public async Task<List<Dictionary<string, string>>> GetRowValuesAsDict(int startIndex, int endIndex)
        {
            List<Dictionary<string, string>> allRowValuesWithHeaders = new List<Dictionary<string, string>>();

            List <List<string>> allRowValues = await GetRowValuesAsList(startIndex, endIndex);
            List<string> headerValues = await GetAllDisplayedColumnNames();
            foreach (List<string> eachRowValues in allRowValues)
            {
                allRowValuesWithHeaders.Add(headerValues.Zip(eachRowValues).ToDictionary(x => x.First, x => x.Second));
            }
            return allRowValuesWithHeaders;
        }
        
    }
}
