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
    [TestModule("2A66975D-B913-4FE0-8B3C-C85DB1232749", ModuleType.UserCode, 1)]
    public class admin4 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin4()
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
            
       /*     
            string[] urls = new string[] 
            {
            	"/Reports/Revenue_Enterprise.aspx",
            	"/Reports/ReconcilliationReport.aspx", 
            	"/Reports/Sales.aspx"
            };
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\mjordan\Documents\Ranorex\test\LyndaTestAutomationFramework\LyndaTestAutomationFramework\Tests\General\Tests\Admin\log.txt", true))
            	foreach (string url in urls)
            {
                //browser.Navigate("https://{0}{1}admin.",domain,urls);
                //file.WriteLine(string.Format("https://{0}{1}{2}",admin,domain,url));
                // file.WriteLine(url);
                 
                browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
                Validate.Exists(repo.WebDocumentWelcome.gobtn);
                repo.WebDocumentWelcome.gobtn.Click();
            }
            
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();
            
            */
        }
    }
}
