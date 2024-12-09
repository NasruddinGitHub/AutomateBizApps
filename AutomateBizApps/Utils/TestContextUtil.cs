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
    }
}
