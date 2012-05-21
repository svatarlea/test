/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 1:23 PM
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
    /// Description of CloseBrowser.
    /// </summary>
    [TestModule("B56BBBB1-F373-414D-A05D-BDBA455FDD2E", ModuleType.UserCode, 1)]
    public class CloseBrowser : ITestModule
    {
        
    	private static Public_lpBVT5Repository repo = Public_lpBVT5Repository.Instance;
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CloseBrowser()
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
            
             Host.Local.CloseApplication(repo.DOM.Self, new Duration(0));
        }
    }
}
