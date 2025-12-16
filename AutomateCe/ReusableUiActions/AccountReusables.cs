using AutomateCe.Controls;
using AutomateCe.DataModel;
using AutomateCe.Modules;
using AutomateCe.Utils;
using Azure;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.ReusableUiActions
{
    public class AccountReusables
    {
        private IPage _page;
        public AccountReusables(IPage page)
        {
            _page = page;
        }
        public async Task FillAccountDetailsInQuickCreateAndSaveAndCloseAsync(AccountQuickCreateForm accountQuickCreateForm)
        {
            CeApp ceApp = new CeApp(_page);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_firstname", accountQuickCreateForm.FirstName);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_lastname", accountQuickCreateForm.LastName);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("name", accountQuickCreateForm.AccountName);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("telephone1", accountQuickCreateForm.PhoneNumber);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("emailaddress1", accountQuickCreateForm.Email);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_line1", accountQuickCreateForm.AddressStreet1);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_line2", accountQuickCreateForm.AddressStreet2);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_city", accountQuickCreateForm.City);
            LookupItem stateLookup = new LookupItem { Name = "cx_account_stateid", Value = accountQuickCreateForm.State};
            await ceApp.QuickCreate.SetValueBySchemaNameAsync(stateLookup);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_country", accountQuickCreateForm.Country);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_postalcode", accountQuickCreateForm.ZipCode);
            await ceApp.QuickCreate.SaveAndCloseAsync();
        }

        public async Task FillAccountDetailsInQuickCreateByOverridingAndSaveAndCloseAsync(AccountQuickCreateForm accountQuickCreateForm)
        {
            CeApp ceApp = new CeApp(_page);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_firstname", accountQuickCreateForm.FirstName);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_lastname", accountQuickCreateForm.LastName);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("name", accountQuickCreateForm.AccountName);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("telephone1", accountQuickCreateForm.PhoneNumber);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("emailaddress1", accountQuickCreateForm.Email);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_line1", accountQuickCreateForm.AddressStreet1);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_line2", accountQuickCreateForm.AddressStreet2);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_city", accountQuickCreateForm.City);
            LookupItem stateLookup = new LookupItem { Name = "cx_account_stateid", Value = accountQuickCreateForm.State };
            await ceApp.QuickCreate.SetValueBySchemaNameAsync(stateLookup);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_country", accountQuickCreateForm.Country);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_postalcode", accountQuickCreateForm.ZipCode);
            await ceApp.QuickCreate.ToggleBooleanFieldOnBySchemaAsync("cx_override");
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_address1_streetnumber_text", accountQuickCreateForm.StreetNumber.ToString());
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_address1_streetname_text", accountQuickCreateForm.StreetName);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_address1_streetsuffix_text", accountQuickCreateForm.StreetSuffix);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_addess1_predirection_text", accountQuickCreateForm.PreDirection);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("cx_address1__postdirection_text", accountQuickCreateForm.PostDirection);
            await ceApp.QuickCreate.SetValueBySchemaNameAsync("address1_county", accountQuickCreateForm.County);

            await ceApp.QuickCreate.SaveAndCloseAsync();
        }
    }
}
