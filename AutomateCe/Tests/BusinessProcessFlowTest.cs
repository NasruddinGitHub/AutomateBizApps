using AutomateCe.Constants;
using AutomateCe.Modules;
using AutomateCe.Controls;
using AutomateCe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Tests
{
    [TestFixture]
    public class BusinessProcessFlowTest : BaseTest
    {
        string? email = TestContext.Parameters[Property.Email];
        string? password = TestContext.Parameters[Property.Password];
        string? mfaKey = TestContext.Parameters[Property.MfaKey];

        [Test]
        public async Task ValidateIfUserIsAbleToHandleLookupField()
        {
            ReportUtil.CreateTest("Validate if the user is able to handle the lookup field in BPF");
            CeApp ceApp = new CeApp(page);
            await ceApp.LoginModule.LoginAsync(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenAppAsync("Field Service");
            ReportUtil.Info("Selected Field Service application");
            await ceApp.SiteMapPanel.OpenSubAreaAsync("Service Delivery", "Cases");
            await ceApp.CommandBar.ClickCommandAsync("New Case");
            await ceApp.Entity.SetValueByLabelNameAsync("Case Title", "Contoso Coffee Machine is not working");
            LookupItem customer = new LookupItem { Name = "Customer", Value = "Adventure Works", Index = 0};
            await ceApp.Entity.SetValueByLabelNameAsync(customer);
            
            await ceApp.CommandBar.ClickCommandAsync("Save");

            List<string> allStageNames = await ceApp.BusinessProcessFlow.GetStageNamesAsync();
            string selectedStageName = await ceApp.BusinessProcessFlow.GetSelectedStageNameAsync();
            List<string> allUnSelectedStageNames = await ceApp.BusinessProcessFlow.GetUnselectedStageNameAsync();
            await ceApp.BusinessProcessFlow.SelectStageAsync("Identify");
            Console.WriteLine(await ceApp.BusinessProcessFlow.HeaderAsync());
            LookupItem findCustomer = new LookupItem { Name = "Find Customer", Value = "Alpine Ski", Index = 0 };
            await ceApp.BusinessProcessFlow.ClearValueByLabelNameAsync(findCustomer);
            await ceApp.BusinessProcessFlow.SetValueByLabelNameAsync(findCustomer);
            await ceApp.BusinessProcessFlow.NextStageAsync();
            await ceApp.BusinessProcessFlow.NextStageAsync();
            await ceApp.BusinessProcessFlow.FinishAsync();

        }

        // Other Methods in BPF
        public async Task OtherMethodsInBpf()
        {
            CeApp ceApp = new CeApp(page);

            // To clear, set and get the lookup value
            LookupItem serviceAccount = new LookupItem { Name = "Service Account", Value = "Alpine Ski House", Index = 0 };
            await ceApp.BusinessProcessFlow.ClearValueByLabelNameAsync(serviceAccount);
            await ceApp.BusinessProcessFlow.SetValueByLabelNameAsync(serviceAccount);
            Console.WriteLine(await ceApp.BusinessProcessFlow.GetValueByLabelNameAsync(serviceAccount));

            // To set, get and get all available values in optionset
            OptionSet systemStatus = new OptionSet { Name = "System Status", Value = "Expired" };
            await ceApp.BusinessProcessFlow.SetValueByLabelNameAsync(systemStatus);
            Console.WriteLine(await ceApp.BusinessProcessFlow.GetValueByLabelNameAsync(systemStatus));
            List<string> allSystemStatusValues = await ceApp.BusinessProcessFlow.GetAllAvailableValuesByLabelNameAsync(systemStatus);

            // To set, get and clear all available values in optionset
            MultiSelectOptionSet days = new MultiSelectOptionSet { Name = "Days", Values = new string[] { "Saturday","Sunday"} };
            await ceApp.BusinessProcessFlow.SetValueByLabelNameAsync(days);
            List<string> selectedDays =  await ceApp.BusinessProcessFlow.GetSelectedValuesByLabelNameAsync(days);
            await ceApp.BusinessProcessFlow.ClearValuesByLabelNameAsync(days);

            // To get, set and clear the value in text field
            await ceApp.BusinessProcessFlow.SetValueByLabelNameAsync("Description", "2 Years Agreement");
            Console.WriteLine(await ceApp.BusinessProcessFlow.GetValueByLabelNameAsync("Description"));
            await ceApp.BusinessProcessFlow.ClearValueByLabelNameAsync("Description");

            // To verify if a field is optional, business required, business recommended, locked or completed in BPF
            bool isSubstatusOptionalField = await ceApp.BusinessProcessFlow.IsFieldOptionalByLabelNameAsync("Substatus");
            bool isPriceListMandatoryField = await ceApp.BusinessProcessFlow.IsFieldBusinessRequiredByLabelNameAsync("Price List");
            bool isSystemStatusRecommendedField = await ceApp.BusinessProcessFlow.IsFieldBusinessRequiredByLabelNameAsync("System Status");
            bool isBillingAccountLockedField = await ceApp.BusinessProcessFlow.IsFieldLockedByLabelNameAsync("Billing Account");
            bool isWorkOrderTypeCompleted = await ceApp.BusinessProcessFlow.IsFieldCompletedByLabelNameAsync("Work Order Type");

            // This will take the user to previous stage
            await ceApp.BusinessProcessFlow.PreviousStageAsync();

            // This is used to pin the BPF
            await ceApp.BusinessProcessFlow.PinAsync();

            // This is used to close the BPF
            await ceApp.BusinessProcessFlow.CloseAsync();

            // This is used to set active the BPF
            await ceApp.BusinessProcessFlow.SetActiveAsync();

            // This will take the user to previous stage
             await ceApp.BusinessProcessFlow.PreviousStageAsync();

            // This will click on the create in the process cross entity flyout
            await ceApp.ProcessCrossEntityFlyout.CreateAsync();

            // This will be used to close the process cross entity flyout
             await ceApp.ProcessCrossEntityFlyout.CloseAsync();

            // This will be used to get the header of process cross entity flyout
            string processCrossEntityFlyoutHeader = await ceApp.ProcessCrossEntityFlyout.GetHeaderAsync();

            // This will be used to get all the items in process cross entity flyout
            List<string> allItems = await ceApp.ProcessCrossEntityFlyout.GetItemsAsync();

            // This will be used to select the process cross entity flyout
            await ceApp.ProcessCrossEntityFlyout.SelectItemAsync("Agreement Booking Setup 1");
        }
    }
}
