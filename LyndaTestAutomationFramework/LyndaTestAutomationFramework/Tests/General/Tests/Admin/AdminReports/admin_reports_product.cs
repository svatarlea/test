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
    /// Description of admin_reports_product.
    /// </summary>
    [TestModule("8C4DD5C6-6E84-4F6A-A57B-F4AAAFD5C3E4", ModuleType.UserCode, 1)]
    public class admin_reports_product : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin_reports_product()
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
            string url = "/Welcome.aspx";
            
            Browser browser = new Browser(BrowserProduct.Firefox, domain, true);            
            browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain, url));
            
            adminrepo repo = adminrepo.Instance;
            
            repo.WebDocumentWelcome.adminlogin.username.PressKeys("admintester");
            repo.WebDocumentWelcome.adminlogin.password.PressKeys("Lynda1");
            repo.WebDocumentWelcome.adminlogin.submitbtn.Click();
            
            //Reports - Product - 1. Bookmarks and Queue Reports
    	    //Reports - Product - 2. Preferences Report
            //Reports - Product - 3. Ribbon Application Actions
            //Reports - Product - 4. Most Common Personas

                  
            //Reports - Product - 4. Most Common Personas  
            url = "/Reports/ReportsViews/ViewProductUsage.aspx?lpk46=/WebReports/Most+Common+Personas";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
			Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);
                            
                            
            //Logout
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();
        }
    }
}
