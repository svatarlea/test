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
    /// Description of admin_reports_royalty.
    /// </summary>
    [TestModule("FB03F5B4-9539-4EA4-B0E8-4EB407BCEC35", ModuleType.UserCode, 1)]
    public class admin_reports_royalty : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin_reports_royalty()
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
            browser.Navigate("https://admin.release.lynda.com/Welcome.aspx");
            
            adminrepo repo = adminrepo.Instance;
            
            repo.WebDocumentWelcome.adminlogin.username.PressKeys("admintester");
            repo.WebDocumentWelcome.adminlogin.password.PressKeys("Lynda1");
            repo.WebDocumentWelcome.adminlogin.submitbtn.Click();
            
            //Reports - Royalty - 1. Royalty Report Search
            url = "/Reports/RoyaltyReportsSearch.aspx";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.searchbtn);
            repo.WebDocumentWelcome.HeaderAdmin.searchtxt.PressKeys("Rivers");
            repo.WebDocumentWelcome.HeaderAdmin.searchbtn.Click();
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.rivda1);
            
            //Reports - Royalty - 2. Royalty Calculation Report
            //url = "/Reports/ReportsViews/ViewReports.aspx?lpk46=/WebReports/Viewers+by+Calendar+Month";
			//browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            //Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.royaltysummarydata);
            
            //Reports - Royalty - 3. Distinctive Views per Royalty Job
            //url = "/Reports/ReportsViews/ViewRoyalty.aspx?lpk46=/WebReports/Distinctive+Views+per+Royalty+Job";
		   // browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
           // Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Royalty - 4. Royalty by Year Detail
            string selectYear, selectNumYrPeriod, selectMoYrPeriod, selectAuthor;
           	AdminReportViewer.ReportViewer(out selectYear, out selectNumYrPeriod, out selectMoYrPeriod, out selectAuthor );
            url = "/Reports/ReportsViews/ViewRoyalty.aspx?lpk46=/Royalty/RoyaltyByYearComplete";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.HeaderAdmin.selectyear, selectYear);
            repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn.Click();
            
            //Reports - Royalty - 5. Royalty by Year Summary
            
            //this one contains a checkbox inside a dropdown
            
            //AdminReportViewer.ReportViewer(out selectYear);
            //url = "/Reports/ReportsViews/ViewRoyalty.aspx?lpk46=/Royalty/RoyaltyByYearSummary";
			//browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            //Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            //SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.HeaderAdmin.selectyear, selectYear);
            //repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn.Click();
            
            //Reports - Royalty - 6. Royalty Report by Period / Author      
            url = "/Reports/ReportsViews/ViewRoyalty.aspx?lpk46=/Royalty/RoyaltyReportByPeriodAuthor";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.HeaderAdmin.selectyear, selectNumYrPeriod);
            SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.HeaderAdmin.selectauthor, selectAuthor);
            repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn.Click();
            
            //Reports - Royalty - 7. Royalty Payments by Period / Author  
            url = "/Reports/ReportsViews/ViewRoyalty.aspx?lpk46=/WebReports/RoyaltyPaymentsByPeriodAuthor";
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
            
            //Reports - Royalty - 8. Monthly Royalty Statements         
            url = "/Reports/MonthlyRoyaltyStatements.aspx";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
			Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.selectyear);
            SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.HeaderAdmin.selectyear, selectMoYrPeriod);
            SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.RoyaltyStatements.selectauthor, selectAuthor);
            repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn.Click();
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.totalcurrentmonthearnings);
            
                        
            //Logout
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();
            
        }
    }
}
