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
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("9D027FDD-0799-46BC-910E-CEDF7EC0CA4E", ModuleType.UserCode, 1)]
    public class admin_reports_finance : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin_reports_finance()
        {
            // Do not delete - a parameterless constructor is required!
        }
        private static adminrepo repo = adminrepo.Instance;
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
            browser.Navigate("https://admin.release.lynda.com/Welcome.aspx");
            
            adminrepo repo = adminrepo.Instance;
            
            repo.WebDocumentWelcome.adminlogin.username.PressKeys("admintester");
            repo.WebDocumentWelcome.adminlogin.password.PressKeys("Lynda1");
            repo.WebDocumentWelcome.adminlogin.submitbtn.Click();
            
            //Reports - Financial - Summary Report - takes forever to return results
            //browser.Navigate(domain"/Reports/Summary.aspx");
            
            //url = "/Reports/Revenue_Enterprise.aspx";
            //browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            //Validate.Exists(repo.WebDocumentWelcome.gobtn);
            
            //Reports - Financial - Reconcilliation Report
            url = "/Reports/ReconcilliationReport.aspx";
            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.gobtn);
            repo.WebDocumentWelcome.gobtn.Click();
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.numberoftrans);
            
            //Reports - Financial - Store Sales Report
            url = "/Reports/Sales.aspx";
            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.gobtn);
            repo.WebDocumentWelcome.gobtn.Click();
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.storesalestotals);
            
            //Reports - Financial - Store Returns Report
            url = "/Reports/Returns.aspx";
            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.gobtn);
            repo.WebDocumentWelcome.gobtn.Click();
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.storesalestotals);
            
            //Reports - Financial - Aging Report
            url = "/Reports/ReportsViews/ViewFinancial.aspx?lpk46=/WebReports/Aging+Report+(NEW)";
            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Financial - Membership Graph
			url = "/Reports/ReportsViews/ViewFinancial.aspx?lpk46=/WebReports/Membership+Graph";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);

			//Reports - Financial - Revenue Graph
			url = "/Reports/ReportsViews/ViewFinancial.aspx?lpk46=/WebReports/Revenue+Graph";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);

			//Reports - Financial - Users by Payment type
			url = "/Reports/ReportsViews/ViewFinancial.aspx?lpk46=/WebReports/Users+by+Payment+Type+and+Credit+Card+Type";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);

			//Reports - Financial - Sign ups by hour
			url = "/Reports/ReportsViews/ViewFinancial.aspx?lpk46=/WebReports/Signups+by+Hour";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);

			//Reports - Financial - Amoritized Royalty Revenue Report
			url = "/Reports/ReportsViews/ViewFinancial.aspx?lpk46=/WebReports/RoyaltyRevenue";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Logout
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();
            
        }
    }
}


