using AventStack.ExtentReports;
using AventStack.ExtentReports.Configuration;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using System;
using System.IO;
using System.Net;

namespace WTD_Automation
{
    public class Report
    {
        public static ExtentHtmlReporter _htmlReport;
        public static ExtentReports _extent;

        [OneTimeSetUp]
        [Obsolete]
        public static ExtentReports getInstance()
        {
            if (_extent == null)
            {
                string _currentDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
                string report = _currentDirectory + @"\APIRinxTest\ExtentReport\Rinx.html";
                var htmlReport = new ExtentV3HtmlReporter(report);
                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReport);
                htmlReport.LoadConfig(_currentDirectory + @"\APIRinxTest\extent-config.xml");
                string HostName = Dns.GetHostName();
                var os = Environment.OSVersion;
                _extent.AddSystemInfo("Host Name", HostName);
                _extent.AddSystemInfo("OS", os.ToString());
            }
            return _extent;
        }


        [OneTimeTearDown]
        public void ExtentReportClose()
        {
            _extent.Flush();


        }
    }
}
