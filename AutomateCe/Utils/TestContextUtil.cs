using AutomateBizApps.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Utils
{
    public static class TestContextUtil
    {
        public static string GetProjectRootDir()
        {
            string executingPath = Assembly.GetExecutingAssembly().Location;
            return Directory.GetParent(executingPath).Parent.Parent.Parent.FullName;
        }

        public static string GetVideoRecordingDir()
        {
            string givenPath = TestContext.Parameters[Property.RecordingVideosDir];
            if (givenPath == "")
            {
                return "";
            }
            return Path.Combine(GetProjectRootDir(), givenPath);
        }

        public static string GetParameter(String parameter)
        {
            return TestContext.Parameters[parameter];
        }

        public static string GetBrowser()
        {
            string? browserName = (string?)TestContext.CurrentContext.Test.Properties.Get("browser");
            if (browserName == null)
            {
                browserName = TestContext.Parameters[Property.BrowserType];
            }
            return browserName;
        }


    }
}
