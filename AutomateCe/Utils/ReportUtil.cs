using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutomateCe.Utils
{
    public static class ReportUtil
    {
        private static ExtentSparkReporter _sparkReport;
        private static ExtentReports _extentReports;
        // Thread-safe per-test ExtentTest
        private static readonly ConcurrentDictionary<string, ExtentTest> _testMap
        = new ConcurrentDictionary<string, ExtentTest>();

        public static ExtentReports GetInstance(string reportPath)
        {
            if (_extentReports == null)
            {
                _sparkReport = InitExtentSparkReport(reportPath);
                SetReportTitle("Automation Test Report");
                SetReportName("Sample Execution Results");

                _extentReports = InitExtentReport();
                AttachReport();
            }
            return _extentReports;
        }

        public static ExtentSparkReporter InitExtentSparkReport(string reportTargetLocation)
        {
            _sparkReport = new ExtentSparkReporter(reportTargetLocation);
            return _sparkReport;
        }

        public static ExtentSparkReporter GetExtentSparkReport()
        {
            return _sparkReport;
        }

        public static ExtentReports InitExtentReport()
        {
            _extentReports = new ExtentReports();
            return _extentReports;
        }

        public static void AttachReport()
        {
            _extentReports.AttachReporter(_sparkReport);
        }

        public static void AttachReport(ExtentSparkReporter extentSparkReport)
        {
            _extentReports.AttachReporter(extentSparkReport);
        }

        public static ExtentReports GetExtentReport()
        {
            return _extentReports;
        }

        public static void setSystemInformation(string key, string value)
        {
            _extentReports.AddSystemInfo(key, value);
        }

        public static ExtentTest CreateTest(string testName)
        {
            var test = _extentReports.CreateTest(testName);  // create ExtentTest
            string id = TestId();
            _testMap[id] = test;   // store test safely for parallel runs
            return test;
        }

        public static ExtentTest CreateTest(string testName, string description)
        {
            var test = _extentReports.CreateTest(testName, description);  // create ExtentTest
            string id = TestId();
            _testMap[id] = test;   // store test safely for parallel runs
            return test;
        }

        public static ExtentTest GetTest()
        {
            string id = TestId();
            return _testMap.TryGetValue(id, out var test) ? test : null;
        }

        public static ExtentTest Log(Status status, Media media)
        {
           return GetTest().Log(status, media);
        }

        public static ExtentTest Log(Status status, string details)
        {
            return GetTest().Log(status, details);
        }

        public static ExtentTest AssignAuthor(params string[] author)
        {
            return GetTest().AssignAuthor(author);
        }

        public static ExtentTest AssignCategory(params string[] category)
        {
            return GetTest().AssignCategory(category);
        }

        public static ExtentTest AssignDevice(params string[] device)
        {
            return GetTest().AssignDevice(device);
        }

        public static ExtentTest CreateNode(string name)
        {
            return GetTest().CreateNode(name);
        }

        public static ExtentTest CreateNode(string name, string description)
        {
            return GetTest().CreateNode(name, description);
        }

        public static ExtentTest FailTest(string details)
        {
            return GetTest().Fail(details);
        }

        public static ExtentTest PassTest(string details)
        {
            return GetTest().Pass(details);
        }

        public static ExtentTest Info(string details)
        {
            return GetTest().Info(details);
        }

        public static Status GetStatus()
        {
            return GetTest().Status;
        }

        public static ExtentTest SkipTest(string details)
        {
            return GetTest().Skip(details);
        }

        public static ExtentTest WarningTest(string details)
        {
            return GetTest().Warning(details);
        }

        public static ExtentTest AddScreenCaptureFromPath(string path, string title=null)
        {
            return GetTest().AddScreenCaptureFromPath(path, title);
        }

        public static ExtentTest AddScreenCaptureFromBase64String(string path, string title = null)
        {
            return GetTest().AddScreenCaptureFromBase64String(path, title);
        }

        public static void Flush()
        {
            _extentReports.Flush();
        }

        public static void RemoveTest(string testName)
        {
            _extentReports.RemoveTest(testName);
        }

        public static ReportStats GetReportStats(string testName)
        {
            return _extentReports.Report.Stats;
        }

        public static ExtentSparkReporterConfig GetExtentSparkReportConfig()
        {
            return _sparkReport.Config;
        }

        public static void SetTheme(Theme theme)
        {
            GetExtentSparkReportConfig().Theme = theme;
        }

        public static void LoadJSONConfigFile(string filePath)
        {
            _sparkReport.LoadJSONConfig(filePath);
        }

        public static void LoadXMLConfigFile(string filePath) 
        {
            _sparkReport.LoadXMLConfig(filePath);
        }

        public static void SetReportName(string reportName)
        {
            GetExtentSparkReportConfig().ReportName = reportName;
        }

        public static void SetTimeStampFormat(string timeStampFormat)
        {
            GetExtentSparkReportConfig().TimeStampFormat = timeStampFormat;
        }

        public static void SetReportTitle(string reportTitle)
        {
            GetExtentSparkReportConfig().DocumentTitle = reportTitle;
        }

        public static void SetTimelineEnabled(bool timelineEnabled)
        {
            GetExtentSparkReportConfig().TimelineEnabled = timelineEnabled;
        }

        public static Report GetReport()
        {
            return _sparkReport.Report;
        }

        /// <summary>
        /// Key for identifying thread-safe test storage
        /// </summary>
        private static string TestId()
        {
            return NUnit.Framework.TestContext.CurrentContext.Test.ID;
        }
    }
}
