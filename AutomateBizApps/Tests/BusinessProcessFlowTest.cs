using AutomateBizApps.Constants;
using AutomateBizApps.Modules;
using AutomateBizApps.Tests;
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
            await ceApp.LoginModule.Login(email, password, mfaKey);
            ReportUtil.Info("Logged into customer engagement application");
            await ceApp.ApplicationLandingPageModule.OpenApp("Field Service");
            ReportUtil.Info("Selected Field Service application");
            await ceApp.SiteMapPanel.OpenSubArea("Service Delivery", "Cases");
            await ceApp.CommandBar.ClickCommand("New Case");
            await ceApp.Entity.SetValue("Case Title", "Contoso Coffee Machine is not working");
            LookupItem customer = new LookupItem { Name = "Customer", Value = "Adventure Works", Index = 0};
            await ceApp.Entity.SetValue(customer);
            
            await ceApp.CommandBar.ClickCommand("Save");

            List<string> allStageNames = await ceApp.BusinessProcessFlow.GetStageNames();
            string selectedStageName = await ceApp.BusinessProcessFlow.GetSelectedStageName();
            List<string> allUnSelectedStageNames = await ceApp.BusinessProcessFlow.GetUnselectedStageName();
            await ceApp.BusinessProcessFlow.SelectStage("Identify");
            Console.WriteLine(await ceApp.BusinessProcessFlow.Header());
            LookupItem findCustomer = new LookupItem { Name = "Find Customer", Value = "Alpine Ski", Index = 0 };
            await ceApp.BusinessProcessFlow.ClearValue(findCustomer);
            await ceApp.BusinessProcessFlow.SetValue(findCustomer);
            await ceApp.BusinessProcessFlow.NextStage();
            await ceApp.BusinessProcessFlow.NextStage();
            await ceApp.BusinessProcessFlow.Finish();

        }

        // Other Methods in BPF
        public async Task OtherMethodsInBpf()
        {
            CeApp ceApp = new CeApp(page);

            // To clear, set and get the lookup value
            LookupItem serviceAccount = new LookupItem { Name = "Service Account", Value = "Alpine Ski House", Index = 0 };
            await ceApp.BusinessProcessFlow.ClearValue(serviceAccount);
            await ceApp.BusinessProcessFlow.SetValue(serviceAccount);
            Console.WriteLine(await ceApp.BusinessProcessFlow.GetValue(serviceAccount));

            // To set, get and get all available values in optionset
            OptionSet systemStatus = new OptionSet { Name = "System Status", Value = "Expired" };
            await ceApp.BusinessProcessFlow.SetValue(systemStatus);
            Console.WriteLine(await ceApp.BusinessProcessFlow.GetValue(systemStatus));
            List<string> allSystemStatusValues = await ceApp.BusinessProcessFlow.GetAllAvailableValues(systemStatus);

            // To set, get and clear all available values in optionset
            MultiSelectOptionSet days = new MultiSelectOptionSet { Name = "Days", Values = new string[] { "Saturday","Sunday"} };
            await ceApp.BusinessProcessFlow.SetValue(days);
            List<string> selectedDays =  await ceApp.BusinessProcessFlow.GetSelectedValues(days);
            await ceApp.BusinessProcessFlow.ClearValues(days);

            // To get, set and clear the value in text field
            await ceApp.BusinessProcessFlow.SetValue("Description", "2 Years Agreement");
            Console.WriteLine(await ceApp.BusinessProcessFlow.GetValue("Description"));
            await ceApp.BusinessProcessFlow.ClearValue("Description");

            // To verify if a field is optional, business required, business recommended, locked or completed in BPF
            bool isSubstatusOptionalField = await ceApp.BusinessProcessFlow.isFieldOptional("Substatus");
            bool isPriceListMandatoryField = await ceApp.BusinessProcessFlow.isFieldBusinessRequired("Price List");
            bool isSystemStatusRecommendedField = await ceApp.BusinessProcessFlow.isFieldBusinessRequired("System Status");
            bool isBillingAccountLockedField = await ceApp.BusinessProcessFlow.isFieldLocked("Billing Account");
            bool isWorkOrderTypeCompleted = await ceApp.BusinessProcessFlow.isFieldCompleted("Work Order Type");

            // This will take the user to previous stage
            await ceApp.BusinessProcessFlow.PreviousStage();

            // This is used to pin the BPF
            await ceApp.BusinessProcessFlow.Pin();

            // This is used to close the BPF
            await ceApp.BusinessProcessFlow.Close();

            // This is used to set active the BPF
            await ceApp.BusinessProcessFlow.SetActive();

            // This will take the user to previous stage
             await ceApp.BusinessProcessFlow.PreviousStage();

            // This will click on the create in the process cross entity flyout
            await ceApp.ProcessCrossEntityFlyout.Create();

            // This will be used to close the process cross entity flyout
             await ceApp.ProcessCrossEntityFlyout.Close();

            // This will be used to get the header of process cross entity flyout
            string processCrossEntityFlyoutHeader = await ceApp.ProcessCrossEntityFlyout.GetHeader();

            // This will be used to get all the items in process cross entity flyout
            List<string> allItems = await ceApp.ProcessCrossEntityFlyout.GetItems();

            // This will be used to select the process cross entity flyout
            await ceApp.ProcessCrossEntityFlyout.SelectItem("Agreement Booking Setup 1");
        }
    }
}
