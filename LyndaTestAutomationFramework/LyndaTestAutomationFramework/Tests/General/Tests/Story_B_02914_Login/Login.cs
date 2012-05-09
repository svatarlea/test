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

namespace General.Tests.Story_B_02914_Login
{
    /// <summary>
    /// Description of Login.
    /// </summary>
    [TestModule("EC78CBCF-64FD-481E-A18D-90C1545FAA77", ModuleType.UserCode, 1)]
    public class Login : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Login()
        {
            // Do not delete - a parameterless constructor is required!
        }
        
        enum LoginControl { Dialog, InPage };

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

            //Specifiy domain.
            const string domain = "release.lynda.com";
            
            const string testCaseName = "Login";

            string navigateTo = TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["NavigateTo"];
            bool canDirectlyNavigateTo = Convert.ToBoolean(TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["CanDirectlyNavigateTo"]);
            bool loginGoesToAfterLoginPage = Convert.ToBoolean(TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["LoginGoesToAfterLoginPage"]);
            string afterLoginPage = TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["AfterLoginPage"];
            afterLoginPage=string.Format("http://{0}{1}",domain,afterLoginPage);
            string username = TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["Username"];
            string password = TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["Password"];

            Story_B_02914_LoginRepo repo = Story_B_02914_LoginRepo.Instance;
            
            const string browser = "IE";
            
            Host.Local.KillBrowser(browser);
            Host.Local.OpenBrowser("", browser, "", true, true); 


            repo.DOM.WebPage.WaitForDocumentLoaded();
            Validate.Exists(repo.Form.NavigateEditBox);

           	repo.Form.NavigateEditBox.PressKeys(string.Format("http://{0}{1}{2}",domain,navigateTo.ToString(), "{ENTER}"));
            
           	repo.DOM.WebPage.WaitForDocumentLoaded();
           	LoginControl loginControl; 
           	if (Validate.Exists(repo.DOM.LoginLinkInfo.AbsolutePath.ToString(),repo.DOM.LoginLinkInfo.SearchTimeout,"{0}",new Validate.Options(false,ReportLevel.Info)))
           	{       		
           		loginControl = LoginControl.Dialog;
           		Report.Info(string.Format("Logging in using {0}",LoginControl.Dialog.ToString()));
           	}
           	else
           	{
           		Validate.Exists(repo.DOM.LoginControlB.LoginButton);
           		loginControl = LoginControl.InPage;
           		Report.Info(string.Format("Logging in using {0}",LoginControl.InPage.ToString()));
           	}

            string browserURLbeforeLogin =  repo.Form.NavigateEditBox.TextValue;
            Report.Info(string.Format("browserURLbeforeLogin={0}",browserURLbeforeLogin));

            switch (loginControl)
            {
            	case LoginControl.Dialog:
            		repo.DOM.LoginLink.Click();
            		repo.DOM.WebPage.WaitForDocumentLoaded();
					Validate.Exists(repo.DOM.LoginControlA.Username);
					repo.DOM.LoginControlA.Username.PressKeys(username);
					repo.DOM.LoginControlA.Password.PressKeys(password);
					repo.DOM.LoginControlA.LoginButton.Click();	
            		break;
            	case LoginControl.InPage:
            		Validate.Exists(repo.DOM.LoginControlB.Username);
            		repo.DOM.LoginControlB.Username.PressKeys(username);
            		repo.DOM.LoginControlB.Password.PressKeys(password);
            		repo.DOM.LoginControlB.LoginButton.Click();
            		break;     
            	default:
            		break;
            }
            
			repo.DOM.WebPage.WaitForDocumentLoaded();
			Validate.Exists(repo.DOM.LogoutLink);
			
			string browserURLafterLogin = repo.Form.NavigateEditBox.TextValue;
			Report.Info(string.Format("browserURLafterLogin={0}",browserURLafterLogin));
	
			if (loginGoesToAfterLoginPage)
			{
				try
				{
					Validate.IsTrue(browserURLafterLogin==afterLoginPage,
				                string.Format("browserURLafterLogin:{0},afterLoginPage:{1}",browserURLafterLogin,afterLoginPage),true);
				}
				catch (ValidationException e)
				{
					repo.DOM.LogoutLink.Click();
					throw e;
				}
			}
			else
			{
				try
				{
					Validate.IsTrue(string.Compare(browserURLbeforeLogin,browserURLafterLogin,false)==0,
				                string.Format("browserURLbeforeLogin:{0},browserURLafterLogin:{1}",browserURLbeforeLogin,browserURLafterLogin),true);
				}
				catch (ValidationException e)
				{
					repo.DOM.LogoutLink.Click();
					throw e;
				}
			}

            repo.DOM.LogoutLink.Click();
            
            repo.DOM.WebPage.WaitForDocumentLoaded();
            switch (loginControl)
            {
            	case LoginControl.Dialog:
            	case LoginControl.InPage:
            		Validate.Exists(repo.DOM.LoginLink);
            		break;
            	default:
            		break;
            }
            
        }
    }
}
