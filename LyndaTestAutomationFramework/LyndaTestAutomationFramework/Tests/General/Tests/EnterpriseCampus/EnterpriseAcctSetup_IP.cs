/*
 * Created by Ranorex
 * User: jalex
 * Date: 6/1/2012
 * Time: 12:54 PM
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
using System.IO;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;
using Tests.AppConfig;
using Tests.General.Utilities;
using Tests.General.Utilities.Forms;


namespace Tests.General.Tests.EnterpriseCampus
{
    
	
	/// <summary>
    /// Description of Admin_EnterpriseAcctSetup.
    /// </summary>
    [TestModule("DBB4C28F-AD02-4A89-9683-C7146FFD459F", ModuleType.UserCode, 1)]
    public class Admin_EnterpriseAcctSetup : ITestModule
    {
       private static EnterpriseCampusAcctSetup repo = EnterpriseCampusAcctSetup.Instance;
       private Browser	browser; 
    	
    	
    	string _varUsername = "";
    	[TestVariable("18A2165A-0A8E-4413-AC47-3788C10F97C7")]
    	public string varUsername
    	{
    		get { return _varUsername; }
    		set { _varUsername = value; }
    	}
    	
    	
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Admin_EnterpriseAcctSetup()
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
             
            string strResultsFile = Directory.GetCurrentDirectory() + @"\Public_AcctAccessData.xlsx";
            string username = "knvirtualuser8";
            string password = "lynda1";
            
            BrowserProduct browserProduct = AppSettings.Browser;
           
            //*******TestCase 1 : Validate IP login.******************************************************************
            
            string url = string.Format("{0}{1}", AppSettings.Domain,"/home/iptest.aspx");
            //Open Browser and Get the local IP
            browser = new Browser(browserProduct, url);
            
            //If user is already a ip user with current ip and if the iplogin page is displayed - go to navigate out to get the ip address.
            
            if(Validate.NotExists(repo.DOM.IP_TestPage.h1_IPAddressInfo.AbsolutePath, repo.DOM.IP_TestPage.h1_IPAddressInfo.SearchTimeout,"{0}",new Validate.Options(false,ReportLevel.Info)) )
            {
            	Validate.Exists(repo.DOM.IP_LoginPage.ATag_lynda_comInfo);
            	repo.DOM.IP_LoginPage.ATag_lynda_com.Click();
            	Validate.Exists(repo.DOM.IP_LoginPage.SpanTagOkInfo);
            	repo.DOM.IP_LoginPage.SpanTagOk.Click();
            	Validate.Exists(repo.DOM.ATagLynda_comInfo);
            	url = string.Format("{0}{1}{2}","https://admin.", AppSettings.Domain,"/Welcome.aspx");
            	GotoAccountSetup(url, username, password);
            	AccountIPsCleanup();
            	url = string.Format("{0}{1}", AppSettings.Domain,"/home/iptest.aspx");
            	browser.Navigate(url);
            }
            string[] arrIPaddress = repo.DOM.IP_TestPage.h1_IPAddress.InnerText.Split(' ');
            string strIPaddress   = arrIPaddress[4].Trim();
            Report.Log(ReportLevel.Info, strIPaddress);
            
            url = string.Format("{0}{1}{2}","https://admin.", AppSettings.Domain,"/Welcome.aspx");
            
            GotoAccountSetup(url, username, password);
           	
           	Validate.Exists(repo.DOM.AccountSetup.lnklyndaEntAcctSetupInfo);
           	repo.DOM.AccountSetup.lnklyndaEntAcctSetup.MoveTo();
           	repo.DOM.AccountSetup.lnklyndaEntAcctSetup.Click();
           	
           	Validate.Exists(repo.DOM.AccountSetup.h1_Setup1of4Info);
           	Validate.Exists(repo.DOM.AccountSetup.txtFromIPAddress1Info);
           	repo.DOM.AccountSetup.txtFromIPAddress1.PressKeys("{LControlKey down}{Akey}{LControlKey up}");
           	repo.DOM.AccountSetup.txtFromIPAddress1.PressKeys("{Back}");
           	repo.DOM.AccountSetup.txtToIPAddress1.PressKeys("{LControlKey down}{Akey}{LControlKey up}");
           	repo.DOM.AccountSetup.txtToIPAddress1.PressKeys("{Back}");
           	repo.DOM.AccountSetup.txtFromIPAddress1.PressKeys(strIPaddress);
           	Validate.Exists(repo.DOM.AccountSetup.btnContinueInfo);
           	repo.DOM.AccountSetup.btnContinue.Click();
           	
           	if (Validate.Exists(repo.DOM.AccountSetup.h1_Setup2of4Info.AbsolutePath, repo.DOM.AccountSetup.h1_Setup2of4Info.SearchTimeout, "{0}", new Validate.Options(false,ReportLevel.Warn)))
           	 {
           	    Validate.Exists(repo.DOM.AccountSetup.btnApproveInfo);
           	    repo.DOM.AccountSetup.btnApprove.Click();                  	
           	 }
           	 else
           	 {
           	 	Validate.Exists(repo.DOM.AccountSetup.IP_PageDivErrorsInfo);
           	 	Validate.Exists(repo.DOM.AccountSetup.IP_PageErrorRangeInfo);
           	 	Report.Log(ReportLevel.Warn,repo.DOM.AccountSetup.IP_PageErrorRange.InnerText);
           	 	repo.DOM.AccountSetup.IP_AddressPage_Username.Click();
           	 	AccountIPsCleanup();
           	 	GotoAccountSetup(url, username, password);
           		Validate.Exists(repo.DOM.AccountSetup.lnklyndaEntAcctSetupInfo);
           		repo.DOM.AccountSetup.lnklyndaEntAcctSetup.MoveTo();
           		repo.DOM.AccountSetup.lnklyndaEntAcctSetup.Click();
           		Validate.Exists(repo.DOM.AccountSetup.h1_Setup1of4Info);
           		Validate.Exists(repo.DOM.AccountSetup.txtFromIPAddress1Info);
           		repo.DOM.AccountSetup.txtFromIPAddress1.PressKeys(strIPaddress);
           		Validate.Exists(repo.DOM.AccountSetup.btnContinueInfo);
           		repo.DOM.AccountSetup.btnContinue.Click();
           		
           	 }
           	
           	
           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg3of4Info);
           	repo.DOM.AccountSetup.btnContinuePg3of4.Click();
           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg4of4Info);
           	repo.DOM.AccountSetup.btnContinuePg4of4.Click();
           	
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLogoutInfo);
           	repo.DOM.HeaderAdminLogin.btnLogout.Click();
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLoginInfo);
           	
           	//login into Public and verify Access
           	url = string.Format("{0}{1}", AppSettings.Domain,"/ipprogram/iplogin.aspx");
           	browser.Navigate(url);
           	
           	Validate.Exists(repo.DOM.IP_LoginPage.DivTagBreadcrumbsInfo);
           	Validate.AreEqual(repo.DOM.IP_LoginPage.DivTagBreadcrumbs.InnerText, " » Enterprise login");
           	Validate.Exists(repo.DOM.IP_LoginPage.ImgTagEnterpriseLogoInfo);
           	Validate.Exists(repo.DOM.IP_LoginPage.SpanTagEnterprise_QA_Test_loginInfo);
           	
           	
           	//*******TestCase 2 : Ip login - Validate Error message when logginf from Invalid IP Address************************************************************
            // Permission denied
			// You are trying to log in from an IP address that is not associated with a lynda.com account. For additional assistance, use our contact form or call Customer Service at (888) 335-9632
			
			url = string.Format("{0}{1}{2}","https://admin.", AppSettings.Domain,"/Welcome.aspx");
			GotoAccountSetup(url, username, password);
			AccountIPsCleanup();
			
			//login into Public and verify Access
           	url = string.Format("{0}{1}", AppSettings.Domain,"/ipprogram/iplogin.aspx");
           	browser.Navigate(url);
           	
			
        }
        
        
        void GotoAccountSetup(string url, string username, string password)
        {
        	//navigate to Admin url
            browser.Navigate(url);
            if(AppSettings.Browser == BrowserProduct.IE)
              if(Validate.Exists(repo.DOM.IECertificateErrorPage.OverrideLinkInfo.AbsolutePath, repo.DOM.IECertificateErrorPage.OverrideLinkInfo.SearchTimeout,"{0}",new Validate.Options(false,ReportLevel.Warn)))
              	repo.DOM.IECertificateErrorPage.OverrideLink.Click();	//DEBUG - for IE only
            
            if(Validate.Exists(repo.DOM.HeaderAdminLogin.btnLoginInfo.AbsolutePath, repo.DOM.HeaderAdminLogin.btnLoginInfo.SearchTimeout,"{0}",new Validate.Options(true,ReportLevel.Error)))
           		Report.Log(ReportLevel.Info,"Admin Login Page loaded");
           	
            Validate.Exists(repo.DOM.HeaderAdminLogin.txtAdminUserNameInfo);
            repo.DOM.HeaderAdminLogin.txtAdminUserName.MoveTo();
            repo.DOM.HeaderAdminLogin.txtAdminUserName.PressKeys(username);
            repo.DOM.HeaderAdminLogin.txtAdminPasswd.PressKeys(password);
            repo.DOM.HeaderAdminLogin.btnLogin.MoveTo();
           	repo.DOM.HeaderAdminLogin.btnLogin.Click();
           	
           	HandleAdminCurrentlyLoggedIn();
           	
           	Validate.Exists(repo.DOM.SearchPage.menuCSInfo);
           	repo.DOM.SearchPage.menuCS.MoveTo();
           	repo.DOM.SearchPage.menuCS.Click();
            
           	Validate.Exists(repo.DOM.SearchPage.menuItemSearchInfo);
           	Validate.Exists(repo.DOM.SearchPage.txtUsernameInfo);
           	repo.DOM.SearchPage.txtUsername.PressKeys(varUsername);
           	
           	repo.DOM.SearchPage.btnSubmit.MoveTo();
           	repo.DOM.SearchPage.btnSubmit.Click();
           	
           	Validate.Exists(repo.DOM.SearchPage.lnkSearchResultsInfo);
           	Validate.AreEqual(repo.DOM.SearchPage.lnkSearchResults.InnerText, varUsername);
           	repo.DOM.SearchPage.lnkSearchResults.Click(Location.UpperLeft);
           	
           	Validate.Exists(repo.DOM.AccountSetup.txtUsernameInfo);
           	Validate.AreEqual(repo.DOM.AccountSetup.txtUsername.Value, varUsername);
        }
        
        void AccountIPsCleanup()
        {
        	
        	Validate.Exists(repo.DOM.AccountSetup.lnklyndaEntAcctSetupInfo);
           	repo.DOM.AccountSetup.lnklyndaEntAcctSetup.MoveTo();
           	repo.DOM.AccountSetup.lnklyndaEntAcctSetup.Click();
           	
           	Validate.Exists(repo.DOM.AccountSetup.txtFromIPAddress1Info);
           	repo.DOM.AccountSetup.txtFromIPAddress1.PressKeys("{LControlKey down}{Akey}{LControlKey up}");
           	repo.DOM.AccountSetup.txtFromIPAddress1.PressKeys("{Back}");
           	repo.DOM.AccountSetup.txtToIPAddress1.PressKeys("{LControlKey down}{Akey}{LControlKey up}");
           	repo.DOM.AccountSetup.txtToIPAddress1.PressKeys("{Back}");
           	Validate.Exists(repo.DOM.AccountSetup.btnContinueInfo);
           	repo.DOM.AccountSetup.btnContinue.Click();
           	
           	Validate.Exists(repo.DOM.AccountSetup.btnApproveInfo);
           	repo.DOM.AccountSetup.btnApprove.Click();
           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg3of4Info);
           	repo.DOM.AccountSetup.btnContinuePg3of4.Click();
           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg4of4Info);
           	repo.DOM.AccountSetup.btnContinuePg4of4.Click();
           	
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLogoutInfo);
           	repo.DOM.HeaderAdminLogin.btnLogout.Click();
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLoginInfo);
        	
        }
        
        void HandleAdminCurrentlyLoggedIn()
        {
        	//Deal with already logged in dialog (if it appears)
			//"Hello Keynote Virtualuser!You are currently logged in to your lynda.com account at another computer.
			//Would you like to log off the other computer and login to your account on this computer?"
			switch (AppSettings.Browser)
			{
				case BrowserProduct.Chrome:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogChrome.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogChrome.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							repo.CurrentlyLoggedInDialogChrome.OKButton.Click();
						}
						else
						{
							Report.Info("Chrome Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				case BrowserProduct.IE:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogIE.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogIE.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							repo.CurrentlyLoggedInDialogIE.OKButton.Click();
						}
						else
						{
							Report.Info("IE Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				case BrowserProduct.Firefox:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogFirefox.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogFirefox.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							repo.CurrentlyLoggedInDialogFirefox.OKButton.Click();
						}
						else
						{
							Report.Info("Firefox Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				case BrowserProduct.Safari:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogSafari.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogSafari.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							repo.CurrentlyLoggedInDialogSafari.OKButton.Click();
							//Workaround for bug where you click on the CS menu after clicking OK to the above dialog and the CS page doesn't appear;
							//workaround is to click the CS menu here before it is clicked again after this switch code block.						
							Validate.Exists(repo.DOM.AdminWelcomePageLoggedIn.WelcomeMessageInfo);
							Validate.Exists(repo.DOM.SearchPage.menuCSInfo);
							repo.DOM.SearchPage.menuCS.Click();	
							Report.Info("Clicking CS menu to workaround bug where the CS page doesn't appear on the first click. Bug http://bugzilla.ldcint.com/bugzilla/show_bug.cgi?id=11318");
						}
						else
						{
							Report.Info("Safari Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				default:	
					throw new Exception(String.Format("Code not implemented yet: {0}", AppSettings.Browser.ToString()));
			}
        }
        
        
    }
}
