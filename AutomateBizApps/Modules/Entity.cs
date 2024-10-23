using AutomateBizApps.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Modules
{
    public class Entity : SharedPage
    {
        private IPage _page;

        public Entity(IPage page) : base(page)
        {
            this._page = page;
        }
        

    }
}
