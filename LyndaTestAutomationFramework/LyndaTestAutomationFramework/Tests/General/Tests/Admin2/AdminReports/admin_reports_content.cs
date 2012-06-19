using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;

using Tests.AppConfig;

using General.Utilities.Forms;

namespace Tests.General.Tests.Admin
{
    /// <summary>
    /// Description of admin_reports_content.
    /// </summary>
    [TestModule("59D860F9-B84A-496D-A45F-0F5E212A1636", ModuleType.UserCode, 1)]
    public class admin_reports_content : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin_reports_content()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 50;
            Delay.SpeedFactor = 1.0;
            
            const string admin = "admin.";
            string domain = AppSettings.Domain;
            string url = "admin.lynda.com";
            
            Browser browser = new Browser(BrowserProduct.Firefox, domain, true);            
            browser.Navigate("https://admin.labs.lynda.com/Welcome.aspx");
            
            adminrepo repo = adminrepo.Instance;
            
            repo.WebDocumentWelcome.adminlogin.username.PressKeys("mjordan@lynda.com");
            repo.WebDocumentWelcome.adminlogin.password.PressKeys("Lynda1");
            repo.WebDocumentWelcome.adminlogin.submitbtn.Click();
            
            //Reports - Content - Viewers by Calendar Month
            //url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Viewers+by+Calendar+Month";
			//browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            //Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - Viewers Since Release Date
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Viewers+since+Release+Date";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - Usage
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Usage";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);

            //Reports - Content - Unique users logged-in in a day
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Monthly+Premium+Upgrades";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - Distinctive Views by Calendar Month
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Distinctive+Views+by+Calendar+Month";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - Distinctive Views Since Release Date
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Distinctive+Views+since+Release+Date";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - Distinct and Total View by Date Range and Persona (histogram)
            //Can add steps to make selections and view the report
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Movie+Histogram+Report";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - Movie Views by OS
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Movie+Views+by+OS";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - First Course Viewed by New Member
            //Can add steps to make selections and view the report
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/First+Course+Viewed+by+New+Member";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - OTL Courses
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/OTL+Courses";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Content - Most Viewed Courses Report
            url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Most+Viewed+Courses+Report";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Logout
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();
        }
    }
}
