using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using System;
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
        private static ExtentTest _extentTest;

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
            _extentTest = _extentReports.CreateTest(testName);
            return _extentTest;
        }

        public static ExtentTest CreateTest(string testName, string description)
        {
            _extentTest = _extentReports.CreateTest(testName, description);
            return _extentTest;
        }

        public static ExtentTest GetTest()
        {
            return _extentTest;
        }

        public static ExtentTest Log(Status status, Media media)
        {
           return _extentTest.Log(status, media);
        }

        public static ExtentTest Log(Status status, string details)
        {
            return _extentTest.Log(status, details);
        }

        public static ExtentTest AssignAuthor(params string[] author)
        {
            return _extentTest.AssignAuthor(author);
        }

        public static ExtentTest AssignCategory(params string[] category)
        {
            return _extentTest.AssignCategory(category);
        }

        public static ExtentTest AssignDevice(params string[] device)
        {
            return _extentTest.AssignDevice(device);
        }

        public static ExtentTest CreateNode(string name)
        {
            return _extentTest.CreateNode(name);
        }

        public static ExtentTest CreateNode(string name, string description)
        {
            return _extentTest.CreateNode(name, description);
        }

        public static ExtentTest FailTest(string details)
        {
            return _extentTest.Fail(details);
        }

        public static ExtentTest PassTest(string details)
        {
            return _extentTest.Pass(details);
        }

        public static ExtentTest Info(string details)
        {
            return _extentTest.Info(details);
        }

        public static Status GetStatus()
        {
            return _extentTest.Status;
        }

        public static ExtentTest SkipTest(string details)
        {
            return _extentTest.Skip(details);
        }

        public static ExtentTest WarningTest(string details)
        {
            return _extentTest.Warning(details);
        }

        public static ExtentTest AddScreenCaptureFromPath(string path, string title=null)
        {
            return _extentTest.AddScreenCaptureFromPath(path, title);
        }

        public static ExtentTest AddScreenCaptureFromBase64String(string path, string title = null)
        {
            return _extentTest.AddScreenCaptureFromBase64String(path, title);
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
    }
}
