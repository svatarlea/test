/*
 * Created by Ranorex
 * User: mjordan
 * Date: 6/1/2012
 * Time: 11:39 AM
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

using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;

using General.Utilities.Forms;

namespace Tests.General.Tests.AdminReports
{
    /// <summary>
    /// Description of adminreports.
    /// </summary>
    [TestModule("77185627-5697-4CCF-AE39-DD91B8A4F63A", ModuleType.UserCode, 1)]
    public class adminreports : ITestModule
    {
        /// <summary>
        private static Adminreportsrepo repo = Adminreportsrepo.Instance;
        /// Constructs a new instance.
        /// </summary>
        public adminreports()
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
            
            const string domain = "admin.integration.lynda.com";
            //const string domain = "admin.stage.lynda.com";
            //const string domain = "admin.release.lynda.com";
            //const string domain = "vpnadmin.lynda.com";
            const string navigateTo = "/";
			const BrowserProduct browserProduct = BrowserProduct.IE;

        }
    }
}
