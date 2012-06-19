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
    /// Description of admin_reports_marketing.
    /// </summary>
    [TestModule("C4038A8D-E933-4672-B249-DD4984B258EA", ModuleType.UserCode, 1)]
    public class admin_reports_marketing : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin_reports_marketing()
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
            Keyboard.DefaultKeyPressTime = 100;
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
            
                //Reports - Marketing - 1. Affiliate Report
    			//Reports - Marketing - 2. Trial Promo Admin Summary
    			//Reports - Marketing - 3. Affiliates Activity
    			//Reports - Marketing - 4. Membership History by Month
    			//Reports - Marketing - 5. MTD Membership Report
    			//Reports - Marketing - 6. Membership History by Month (NEW)
    			//Reports - Marketing - 7. MTD Membership Report (NEW)

    			
    			//Reports - Marketing - 1. Affiliate Report
    			//UI element is same as SelectYear, but this one is select partner id
    			
    			
    			//Reports - Marketing - 2. Trial Promo Admin Summary
    			url = "/Reports/ReportsViews/ViewMarketingReports.aspx?lpk46=/WebReports/Trial+Promo+Admin+Summary";
			    browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
                Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
                
                
                //Reports - Marketing - 3. Affiliates Activity
    			url = "/Reports/ReportsViews/ViewMarketingReports.aspx?lpk46=/WebReports/Affiliates+Activity";
			    browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
                Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);
    			
    			//Reports - Marketing - 4. Membership History by Month
    			url = "/Reports/ReportsViews/ViewMarketingReports.aspx?lpk46=/WebReports/Membership+History+by+Month_Old";
			    browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
			    
			    //Reports - Marketing - 5. MTD Membership Report
			    url = "/Reports/ReportsViews/ViewMarketingReports.aspx?lpk46=/WebReports/MTD+Membership+Report_Old";
	            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
	            
	            //Reports - Marketing - 6. Membership History by Month (NEW)
	            url = "/Reports/ReportsViews/ViewMarketingReports.aspx?lpk46=/WebReports/Membership+History+by+Month";
	            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
	            
    			//Reports - Marketing - 7. MTD Membership Report (NEW)
    			url = "/Reports/ReportsViews/ViewMarketingReports.aspx?lpk46=/WebReports/MTD+Membership+Report_Old";
	            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
	            
	                        
            //Logout
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();
        }
    }
}
