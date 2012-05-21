/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 1:03 PM
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

namespace Tests.General.Tests.BVT5
{
  
	/// <summary>
    /// Description of OpenBrowser.
    /// </summary>
    [TestModule("289F49D5-DC79-4EFD-9704-DE6E30488D63", ModuleType.UserCode, 1)]
    public class OpenBrowser : ITestModule
    {
      
    	private Browser browser;
    	
		string _varBrowserName = "";
		[TestVariable("6760A0D9-4014-4A5E-8C26-FCE0B3AA5D51")]
		public string varBrowserName
		{
			get { return _varBrowserName; }
			set { _varBrowserName = value; }
		}
		
		
		string _varEnvironment = "";
		[TestVariable("94732686-269F-48E5-87EA-90A80F409651")]
		public string varEnvironment
		{
			get { return _varEnvironment; }
			set { _varEnvironment = value; }
		}
    	
		
		string _varNavigateTo = "";
		[TestVariable("3A6302C2-4570-42B5-8180-301F9928F537")]
		public string varNavigateTo
		{
			get { return _varNavigateTo; }
			set { _varNavigateTo = value; }
		}
		
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public OpenBrowser()
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
                         
            BrowserProduct browserProduct = (BrowserProduct)Enum.Parse(typeof(BrowserProduct), varBrowserName);
            //BrowserProduct browserProduct = (BrowserProduct)Enum.Parse(typeof(BrowserProduct), AppSettings.Browser.ToString());
           

			string url = string.Format("{0}{1}", varEnvironment,varNavigateTo);
			browser = new Browser(browserProduct, url);

        }
    }
}
