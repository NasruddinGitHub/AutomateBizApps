using AutomateBizApps.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateBizApps.ObjectRepository
{
    public static class ObjectRepository
    {
        // LoginModule
        public static class LoginModuleLocators
        {
            public static string Username = "//input[@name='loginfmt']";
            public static string Password = "//input[@name='passwd']";
            public static string Next = "//input[@type='submit']";
            public static string SignIn = "//input[@value='Sign in']";
            public static string StaySignedInYes = "//input[@value='Yes']";
            public static string StaySignedInNo = "//input[@value='No']";
            public static string Code = "//input[@aria-label='Code']";
            public static string VerifyCode = "#idSubmit_SAOTCC_Continue";
        }

        // ApplicationLandingPage
        public static class ApplicationLandingPageLocators
        {
            public static string ApplandingPageFrame = "#AppLandingPage";
            public static string Application = "//div[@data-type='app-title' and @title='[Name]']";
            public static string AccountManager = "//*[@id='mectrl_main_trigger']";
            public static string AccountManagerSignOut = "//*[@id='mectrl_body_signOut']";
        }

        public static class ComplementaryPaneLocators
        {
            public static string Tab = "//div[@role='complementary']/descendant::button[contains(@aria-label,'[Name]')]";
        }

        // SiteMapPanel 
        public static class SiteMapPanelLocators
        {
            public static string SiteMapLauncherOrCloser = "//button[@title='Site Map']";
            public static string SubArea = "//ul[@aria-label='[Area]']/child::li[@aria-label='[Subarea]']";
            public static string SiteMapEntity = "//ul[@aria-label='[Area]']/child::li/descendant::span[2]";
            public static string AreaChanger = "(//button[@id='areaSwitcherId']/child::span)[1]";
            public static string AreaChangerItem = "//ul[@aria-label='Change area']/child::li/child::span[text()='[Name]']";
            public static string AreaChangerItems = "//ul[@aria-label='Change area']/child::li/child::span[2]";
            public static string SiteMapTab = "//span[text()='[Name]']";
            public static string RecentItem = "//ul[@aria-label='Recent']/child::li[@title='[Name]']";
            public static string PinnedItem = "//ul[@aria-label='Pinned']/child::li[@title='[Name]']";
            public static string AllRecentItems = "//ul[@aria-label='Recent']/child::li";
            public static string PinRecentItem = "//ul[@aria-label='Recent']/child::li[@title='[Name]']/descendant::*[contains(@class,'Pin-symbol')]";
            public static string AllPinnedRecentItem = "//button[@title='Remove from Pinned']/ancestor::li[contains(@aria-label,'pinned')]";
            public static string AllPinnedItems = "//ul[@aria-label='Pinned']/child::li";
            public static string AllUnPinnedRecentItem = "//button[@title='Add to Pinned']/ancestor::li[contains(@aria-label,'not pinned')]";
            public static string UnPinRecentItem = "//ul[@aria-label='Recent']/child::li[@title='[Name]']/descendant::*[contains(@class,'Unpin-symbol')]";
            public static string UnPinPinnedItemFromPinnedGroup = "//ul[@aria-label='Pinned']/child::li[@title='[Name]']/descendant::*[contains(@class,'Unpin-symbol')]";
        }

        public static class CommandBarLocators
        { 
            public static string CommandItem = "//button//span[text()='[Name]']";
            public static string AllShownCommands = "//ul[@data-id='CommandBar' and contains(@aria-label,'Commands')]/child::li[not(contains(@id,'Overflow'))]/descendant::button[not(contains(@aria-describedby,'flyoutButton'))]";
            public static string MoreCommandsEllipsis = "//button[@aria-label='More commands for Account']";
            public static string AllMoreCommands = "//ul[contains(@id,'MenuSectionItemsOverflowButton')]/li/descendant::button[1]";
            public static string OpenInNewWindow = "//*[@aria-label='Open in new window']";
            public static string RecordSetNavigator = "//*[@aria-label='Record set navigator']";
            public static string Share = "//button[@aria-label='Share']";
            public static string AllShareItems = "//div[contains(@class,'fui-MenuList')]/child::div[@role='menuitem']";
            public static string ShareType = "//div[contains(@aria-labelledby,'ApplicationShell')]/child::div[@role='menuitem']/child::span[text()='[Name]']";
        }

        public static class GridLocators
        {
            public static string AllViewNames = "//ul[@aria-label='Views']/descendant::label[contains(@class,'viewName')]";
            public static string DefaultViewName = "//label[text()='Default']/preceding-sibling::label";
            public static string CurrentlySelectedViewName = "//button[contains(@id,'ViewSelector')]/descendant::span[contains(@class,'label')]";
            public static string ViewOpener = "//button[contains(@id,'ViewSelector')]/descendant::*[@data-icon-name='ChevronDown']";
            public static string ViewName = "//label[contains(@class,'viewName') and text()='[Name]']";
            public static string Spinner = "//span[contains(@class,'spinnerTail')]";
            public static string SetDefaultView = "//label[text()='Set as default view']";
            public static string SaveChangesToCurrentView = "//label[text()='Save changes to current view']";
            public static string SaveChangesAsNewView = "//label[text()='Save as new view']";
            public static string ResetDefaultView = "//label[text()='Reset default view']";
            public static string ManageAndShareViews = "//label[text()='Manage and share views']";
            public static string SearchView = "//input[@placeholder='Search views']";
            public static string FilterByKeyword = "//input[@placeholder='Filter by keyword']";
            public static string ClearFilter = "//i[@data-icon-name='Clear']";
            public static string CloseColumnDialog = "//button[@title='Close']";
            public static string FilterByChevronDown = "//div[@data-automation-id='filterBy']/descendant::*[@aria-label='Filter by operator']";
            public static string FilterByOptions = "//div[@aria-label='Filter by operator' and @role='listbox']/descendant::span[contains(@class,'optionText')]";
            public static string FilterByValue = "//input[@aria-label='Filter by value']";
            public static string ApplyFilter = "//span[text()='Apply']";
            public static string ClearFilterValue = "//span[text()='Clear']";
            public static string SelectAllRows = "//div[text()='Toggle selection of all rows']/preceding-sibling::div[contains(@class,'ms-Checkbox')]";
            public static string AllHeaderCells = "//div[@class='ag-header-container']/div[contains(@class,'ag-header-row')]/div[contains(@class,'ag-header-cell')]/descendant::div[contains(@class,'headerTextContainer')]/descendant::div[contains(@class,'TooltipHost')]";
            public static string HeaderColumn = "//div[@class='ag-header-container']/div[contains(@class,'ag-header-row')]/div[contains(@class,'ag-header-cell')]/descendant::div[contains(@class,'headerTextContainer')]/descendant::div[contains(@class,'TooltipHost')][text()='[Name]']";
            public static string HeaderChevronDownIcon = "//div[@class='ag-header-container']/descendant::div[contains(@class,'TooltipHost')][text()='[Name]']/ancestor::div[contains(@class,'headerText')]/following-sibling::div[contains(@class,'icons')]/i[@data-icon-name='ChevronDownSmall']";
            public static string HeaderContextOptions = "//div[@data-testid='columnContextMenu']/descendant::span[text()='[Name]']";
            public static string ColumnWidthInput = "//div[@aria-labelledby='column-width-dialog-title']/descendant::input[contains(@id,'input')]";
            public static string ColumnWidthUpArrow = "//span[contains(@class,'arrowButtonsContainer')]/button[contains(@class,'UpButton')]";
            public static string ColumnWidthDownArrow = "//span[contains(@class,'arrowButtonsContainer')]/button[contains(@class,'DownButton')]";
            public static string RowSelector = "//input[@aria-label='select or deselect the row']/parent::div[contains(@class,'ms-Checkbox')]";
            public static string Row = "//div[@class='ag-center-cols-container']/div[@row-index='[Index]']";
            public static string RowLink = "descendant::a[@role='link' and @aria-label='[Name]']";
            public static string RowCells = "div[contains(@class,'editable-cell')]";
            public static string HeaderSortUp = "//div[contains(@class,'TooltipHost') and text()='[Name]']/ancestor::div[@data-testid='columnHeader']/descendant::*[@data-icon-name='SortUp']";
            public static string HeaderSortDown = "//div[contains(@class,'TooltipHost') and text()='[Name]']/ancestor::div[@data-testid='columnHeader']/descendant::*[@data-icon-name='SortDown']";
            public static string HeaderFiltered = "//div[contains(@class,'TooltipHost') and text()='[Name]']/ancestor::div[@data-testid='columnHeader']/descendant::*[@data-icon-name='FilterSolid']";
            public static string DisplayedRowsCount = "//span[contains(text(),'Rows:')]";
            public static string SelectedRowsCount = "//span[contains(text(),'Selected:')]";
            public static string Grid = "//div[@data-id='grid-container']/descendant::div[@class='ag-center-cols-container' and @role='rowgroup']";
        }

        public static class CommonLocators
        {
            public static string FieldContainer = "//label[text()='[Name]']/ancestor::div[contains(@data-id,'FieldSectionItemContainer')]";
            public static string Button = "//button[text()='[Name]']";
            public static string LookupResults = "//ul[@aria-label='Lookup results']";
            public static string CloseIconLookupValue = "descendant::button[contains(@aria-label,'Delete')]";
            public static string LookupValue = "descendant::div[contains(@id,'selected_tag_text')]";
            public static string DropdownOpener = "descendant::button[@role='combobox']";
            public static string DropdownValues = "//div[@role='listbox']/descendant::div[@role='option']";
            public static string MultiSelectOptionSetOpener = "descendant::div[contains(@class,'msos-caret-container')]";
            public static string MultiSelectOptions = "//div[@class='msos-selection-container']/descendant::label/descendant::div[contains(@class,'msos-optionitem-text')]";
            public static string RemoveOptionInMultiSelectOptionSet = "descendant::span[contains(@class,'selected') and text()='[Name]']/following-sibling::button[contains(@class,'msos-quick-delete')]";
            public static string SelectedOptionsTextInMultiSelectOptionSet = "descendant::div[@class='msos-viewmode-text']";
            public static string SelectedOptionsValueContainer = "descendant::div[contains(@class,'msos-container')]";
            public static string FocusedViewFrame = "//iframe[@id='FormControlIframe_ID']";
        }

        public static class EntityLocators
        {
            public static string Tab = "//ul[contains(@id,'tablist')]/li[text()='[Name]']";
            public static string AllShownTabs = "//ul[contains(@id,'tablist')]/li[contains(@id,'tab') and not(contains(@id,'related'))]";
            public static string RelatedTab = "//div[contains(@id,'button_more_tab') or (contains(@id,'container_related_tab') and text()='Related')]";
            public static string AllRelatedTabs = "//div[@id='relatedEntityContainer']/descendant::div[@role='menuitem']/descendant::span[contains(@class,'fui-MenuItem') and contains(@class,'content')]";
            public static string RelatedTabs = "//div[@id='relatedEntityContainer']/descendant::div[@role='menuitem']/descendant::span[contains(@class,'fui-MenuItem') and text()='[Name]']";
        }
    }


}
