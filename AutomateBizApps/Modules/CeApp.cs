using AutomateBizApps.Pages;
using AutomateCe.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutomateBizApps.Modules
{
    public class CeApp
    {
        internal IPage _page;
        public LoginModule LoginModule => this.GetElement<LoginModule>(_page);
        public ApplicationLandingPageModule ApplicationLandingPageModule => this.GetElement<ApplicationLandingPageModule>(_page);
        public SiteMapPanel SiteMapPanel => this.GetElement<SiteMapPanel>(_page);

        public Complementary Complementary => this.GetElement<Complementary>(_page);

        public CommandBar CommandBar => this.GetElement<CommandBar>(_page);

        public Grid Grid => this.GetElement<Grid>(_page);

        public Entity Entity => this.GetElement<Entity>(_page);

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
