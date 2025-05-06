using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.Constants
{
    public class Property
    {

        public static readonly string Url = "Url";
        public static readonly string Email = "Email";
        public static readonly string Password = "Password";
        public static readonly string MfaKey = "MfaSecretKey";
        public static readonly string BrowserType = "BrowserType";
        public static readonly string TakeScreenshotsForFailedTests = "TakeScreenshotsForFailedTests";
        public static readonly string TakeScreenshotsForPassedTests = "TakeScreenshotsForPassedTests";
        public static readonly string ScreenshotsForFailedTestsInExtentReport = "ScreenshotsForFailedTestsInExtentReport";
        public static readonly string ScreenshotsForPassedTestsInExtentReport = "ScreenshotsForPassedTestsInExtentReport";
        public static readonly string ExecutionReportsFolderPath = "ExecutionReportsFolderPath";
        public static readonly string ScreenshotsFolderPath = "ScreenshotsFolderPath";
        public static readonly string RecordingVideosDir = "RecordingVideosDir";
        public static readonly string HeadlessMode = "HeadlessMode";
    }
}
