/*
 * Created by Ranorex
 * User: jalex
 * Date: 6/6/2012
 * Time: 7:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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

using Lynda.Test.Browsers;
using Tests.AppConfig;

namespace Tests.General.Tests.EnterpriseCampus
{
    /// <summary>
    /// Description of TestModule.
    /// </summary>
    [TestModule("9AFAFA71-D4C8-46A2-BBEC-56086E4A20A0", ModuleType.UserCode, 1)]
    public class TestModule : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TestModule()
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
            
            EnterpriseAcctSetup_IP cmod1 = new EnterpriseAcctSetup_IP();
            string username = "knvirtualuser8";
            string password = "lynda1";
            BrowserProduct browserProduct = AppSettings.Browser;
            string testusername = "6-5-2012-35589";
            Browser browser = new Browser(browserProduct, "google.com");

            
            cmod1.GotoAccountSetup(browser, "admin.release.lynda.com/welcome.aspx",username, password, testusername);
            

        }
    }
}
