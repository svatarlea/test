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

namespace General.Tests.MultiBrowser
{
    /// <summary>
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("A142B5B5-6514-4BED-A143-185A2EF5B538", ModuleType.UserCode, 1)]
    public class MultiBrowserExample : ITestModule
    {       
    	private static MemberHomePage repo = MemberHomePage.Instance;
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public MultiBrowserExample()
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
            
  			const string uri = "www.lynda.com";

            Browser browser1 = new Browser(BrowserProduct.IE, uri, true);
            browser1.HalfSize();
            browser1.Move(100, 100);
            
            Browser browser2 = new Browser(BrowserProduct.Safari, uri, true);
            browser2.HalfSize();
            browser2.Move(200, 200);
            
            Browser browser3 = new Browser(BrowserProduct.Firefox, uri, true);
            browser3.HalfSize();
            browser3.Move(300, 300);
            
            Browser browser4 = new Browser(BrowserProduct.Chrome, uri, true);
            browser4.HalfSize();
            browser4.Move(400,400);
            
            Ranorex.ATag loginLink;
            const string loginLinkRxPath = "dom/body/div[@id='eyebrow']/div[1]/ul/li[4]/a[@id='login-modal']";

            browser1.ClickTitleBar();
            loginLink = Host.Local.FindSingle<Ranorex.ATag>(loginLinkRxPath);
            loginLink.Click();
            Report.Log(ReportLevel.Info,browser1.CurrentUri);
            
            browser2.ClickTitleBar();
            loginLink = Host.Local.FindSingle<Ranorex.ATag>(loginLinkRxPath);
            loginLink.Click();
            Report.Log(ReportLevel.Info,browser2.CurrentUri);

            browser3.ClickTitleBar();
            loginLink = Host.Local.FindSingle<Ranorex.ATag>(loginLinkRxPath);
            loginLink.Click();
            Report.Log(ReportLevel.Info,browser3.CurrentUri);
            
            browser4.ClickTitleBar();
            loginLink = Host.Local.FindSingle<Ranorex.ATag>(loginLinkRxPath);
            loginLink.Click();
            Report.Log(ReportLevel.Info,browser4.CurrentUri);

            browser1.Navigate(uri);
            browser2.Navigate(uri);
            browser3.Navigate(uri);
            browser4.Navigate(uri);

            int total = 3;
            Browser[] browsers = new Browser[total];
            for (int i = 0; i <= total-1; i++)
            {
                browsers[i] = new Browser(BrowserProduct.IE, "lynda.com");
                browsers[i].HalfSize();
                browsers[i].HalfSize();
                browsers[i].Move((i+1) * 10, (i+1) * 10);
            }
            
            browsers[2].Fun();
            

        }
    }
}
