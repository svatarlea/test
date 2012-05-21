/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 4:07 PM
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

namespace Tests.General.Tests.BVT5
{
    /// <summary>
    /// Description of Admin_CloseBrowser.
    /// </summary>
    [TestModule("F5FBCE6D-4891-482A-B5C1-9AE7C094B4CE", ModuleType.UserCode, 1)]
    public class Admin_CloseBrowser : ITestModule
    {
        
    	private Admin_CS_NewAcctRepository repo = Admin_CS_NewAcctRepository.Instance;
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Admin_CloseBrowser()
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
            Mouse.DefaultMoveTime = 50;
            Keyboard.DefaultKeyPressTime = 50;
            Delay.SpeedFactor = 1.0;
            
            repo.DOM.DivTagCtl00_UcHeaderAdminLogin.btnLogout.Click();
            Host.Local.CloseApplication(repo.FormCustomer_Details.Self, new Duration(0));
        }
    }
}
