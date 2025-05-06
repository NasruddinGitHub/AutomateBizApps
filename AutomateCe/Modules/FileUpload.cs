using AutomateCe.Controls;
using AutomateCe.Enums;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{

    public class FileUpload : SharedPage
    {
        private IPage _page;

        public FileUpload(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task UploadFile(string field, string path)
        {
            var chooseFileLocator = await GetLocatorWhenInFramesNotInFrames(CommonLocators.FocusedViewFrame, FileUploadLocators.ChooseFile.Replace("[Name]", field));
            await UploadFile(chooseFileLocator, path);
        }
    }
}
