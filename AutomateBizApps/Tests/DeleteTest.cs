using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateBizApps.Tests
{
    public class DeleteTest : BaseTest
    {

        [Test]
        public async Task Test()
        {
            await page.Locator("//input[@name='loginfmt']").HighlightAsync();
            await page.Locator("//input[@name='loginfmt']").FillAsync("test@test.com");
            Console.WriteLine("Test is completed.");
        }
    }
}
