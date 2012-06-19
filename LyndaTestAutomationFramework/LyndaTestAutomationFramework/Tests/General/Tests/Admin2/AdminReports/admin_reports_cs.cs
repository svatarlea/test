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
    /// Description of admin_reports_cs.
    /// </summary>
    [TestModule("5B7B47A7-7126-44E3-BA80-833C79A89918", ModuleType.UserCode, 1)]
    public class admin_reports_cs : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin_reports_cs()
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
            
            //Reports - Customer Service - 1. Abusive Usage Report
    		//Reports - Customer Service - 2. Customer Support Report - Admin
    		//Reports - Customer Service - 3. Customer Support Report - Public
    		//Reports - Customer Service - 4. Detailed Contact Report
    		//Reports - Customer Service - 5. Summary Contact Report by Agent
    		//Reports - Customer Service - 6. Summary Contact Report by Inquiry Reason
    		//Reports - Customer Service - 7. Summary Contact Report by Time
    		//Reports - Customer Service - 8. Complimentary Account Creation Report
    		//Reports - Customer Service - 9. CS Report - Refunds without Transaction
    		//Reports - Customer Service - 10. CS Report - Multiple Bills
    		//Reports - Customer Service - 11. Cancel Reasons entered by Admin Users
    		//Reports - Customer Service - 12. Cancel Reasons from Public
    		//Reports - Customer Service - 13. Cancelled Accounts due to Abuse
    		//Reports - Customer Service - 14. Discount Report by Product and Month
    		//Reports - Customer Service - 15. Discount Report by Discount Reason
    		//Reports - Customer Service - 16. Discount Report - Detail
    		//Reports - Customer Service - 17. Abuse - Check IP
    		//Reports - Customer Service - 18. Abuse Flag Report
    		//Reports - Customer Service - 19. Blacklist Report
    			
    			
    	    //Reports - Customer Service - 9. CS Report - Refunds without Transaction
    	    url = "/Reports/ReportsViews/ViewCustomerService.aspx?lpk46=/WebReports/CS+Report+-+Refunds+without+Transaction";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
			Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);    			
    			
    			
    	    //Reports - Customer Service - 10. CS Report - Multiple Bills
    	    url = "/Reports/ReportsViews/ViewCustomerService.aspx?lpk46=/WebReports/CS+Report+-+Multiple+Bills";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
			Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);
        }
    }
}
