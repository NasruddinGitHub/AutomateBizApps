using AutomateCe.Modules;
using AutomateCe.Pages;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutomateCe.Modules
{
    public class CeApp
    {
        internal IPage _page;
        public LoginModule LoginModule => this.GetElement<LoginModule>(_page);
        public ApplicationLandingPageModule ApplicationLandingPageModule => this.GetElement<ApplicationLandingPageModule>(_page);
        public SiteMapPanel SiteMapPanel => this.GetElement<SiteMapPanel>(_page);

        public Timeline Timeline => this.GetElement<Timeline>(_page);

        public Complementary Complementary => this.GetElement<Complementary>(_page);

        public CommandBar CommandBar => this.GetElement<CommandBar>(_page);

        public Grid Grid => this.GetElement<Grid>(_page);

        public Entity Entity => this.GetElement<Entity>(_page);

        public QuickCreate QuickCreate => this.GetElement<QuickCreate>(_page);

        public ProcessCrossEntityFlyout ProcessCrossEntityFlyout => this.GetElement<ProcessCrossEntityFlyout>(_page);

        public BusinessProcessFlow BusinessProcessFlow => this.GetElement<BusinessProcessFlow>(_page);

        public Subgrid Subgrid => this.GetElement<Subgrid>(_page);

        public SubgridCommandBar SubgridCommandBar => this.GetElement<SubgridCommandBar>(_page);

        public Dialog Dialog => this.GetElement<Dialog>(_page);

        public LookupDialogModule LookupDialogModule => this.GetElement<LookupDialogModule>(_page);

        public CeApp(IPage page)
        {
            this._page = page;
        }

        public T GetElement<T>(IPage page)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { page });
        }

    }
}
