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


namespace Tests.General.Tests.BusinessSolutions.lyndaEnterpriseCampus
{
    
	
	/// <summary>
    /// Description of Admin_EnterpriseAcctSetup.
    /// </summary>
    [TestModule("DBB4C28F-AD02-4A89-9683-C7146FFD459F", ModuleType.UserCode, 1)]
    public class EnterpriseAcctSetup_IP : ITestModule
    {
       public static EnterpriseCampusAcctSetup repo = EnterpriseCampusAcctSetup.Instance;
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
        public EnterpriseAcctSetup_IP()
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
            //TODO: Update rxrep to support all Browsers
            if(AppSettings.Browser != BrowserProduct.IE)
            {
            	Report.Error("Note: Currently only IE Supported; Please change the appsettings - Key Browser value to IE and retry.");
            	throw new Ranorex.RanorexException();
            } 
            
            string strResultsFile = Directory.GetCurrentDirectory() + @"\Public_AcctAccessData.xlsx";
            string username = "knvirtualuser8";
            string password = "lynda1";
            
            BrowserProduct browserProduct = AppSettings.Browser;
           
            //*******TestCase 1 : Validate IP login.******************************************************************
            
            string url = string.Format("{0}{1}", AppSettings.Domain,"/home/iptest.aspx");
            //Open Browser and Get the local IP
            browser = new Browser(browserProduct, url);
            
            //If user is already an ip user with current ip and if the iplogin page is displayed instead of the iptest page - navigate out to get the ip address.
            //Also go to Admin and clear the IPs.
            if(Validate.NotExists(repo.DOM.IP_TestPage.h1_IPAddressInfo.AbsolutePath, repo.DOM.IP_TestPage.h1_IPAddressInfo.SearchTimeout,"{0}",new Validate.Options(false,ReportLevel.Info)) )
            {
            	Validate.Exists(repo.DOM.IP_LoginPage.ATag_lynda_comInfo);
            	repo.DOM.IP_LoginPage.ATag_lynda_com.Click();
            	Validate.Exists(repo.DOM.IP_LoginPage.SpanTagOkInfo);
            	repo.DOM.IP_LoginPage.SpanTagOk.Click();
            	Validate.Exists(repo.DOM.ATagLynda_comInfo);
            	
            	url = string.Format("{0}{1}", AppSettings.Domain,"/home/iptest.aspx");
            	browser.Navigate(url);
            }
            string[] arrIPaddress = repo.DOM.IP_TestPage.h1_IPAddress.InnerText.Split(' ');
            string strIPaddress   = arrIPaddress[4].Trim();
            Report.Log(ReportLevel.Info, strIPaddress);
            
            url = string.Format("{0}{1}{2}","https://admin.", AppSettings.Domain,"/Welcome.aspx");
            
            GotoAccountSetup(browser, url, username, password, varUsername);
           	
           	Validate.Exists(repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetupInfo);
           	repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.MoveTo();
           	repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.Click();
           	
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
           	 	Report.Log(ReportLevel.Warn,repo.DOM.AccountSetup.IP_PageErrorRange.InnerText + " :  " + repo.DOM.AccountSetup.IP_AddressPage_Username.InnerText);
           	 	repo.DOM.AccountSetup.IP_AddressPage_Username.Click();
           	 	AccountIPsCleanup(strIPaddress);
           	 	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLogoutInfo);
           		repo.DOM.HeaderAdminLogin.btnLogout.MoveTo();
           		repo.DOM.HeaderAdminLogin.btnLogout.Click(Location.Center);
           		Validate.Exists(repo.DOM.HeaderAdminLogin.btnLoginInfo);
           	 	GotoAccountSetup(browser, url, username, password, varUsername);
           		Validate.Exists(repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetupInfo);
           		repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.MoveTo();
           		repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.Click();
           		Validate.Exists(repo.DOM.AccountSetup.h1_Setup1of4Info);
           		Validate.Exists(repo.DOM.AccountSetup.txtFromIPAddress1Info);
           		repo.DOM.AccountSetup.txtFromIPAddress1.PressKeys(strIPaddress);
           		Validate.Exists(repo.DOM.AccountSetup.btnContinueInfo);
           		repo.DOM.AccountSetup.btnContinue.Click();
           		Validate.Exists(repo.DOM.AccountSetup.btnApproveInfo);
           	    repo.DOM.AccountSetup.btnApprove.Click();  
           		
           	 }
           	
           	 //Setup: 3 of 4 – Customize Landing Page
           	 Validate.Attribute(repo.DOM.AccountSetup.chkRequireUsernametobeEmailAddressInfo, "Checked", "True");
           	 Validate.Exists(repo.DOM.AccountSetup.txtEmailDomainInfo);
           	 if(repo.DOM.AccountSetup.txtEmailDomain.InnerText != "")
           	 {
           	 	repo.DOM.AccountSetup.txtEmailDomain.PressKeys("{LControlKey down}{Akey}{LControlKey up}");
           		repo.DOM.AccountSetup.txtEmailDomain.PressKeys("{Back}");
           	 	repo.DOM.AccountSetup.txtEmailDomain.PressKeys("testdomain.com");
           	 }
           	 Validate.Exists(repo.DOM.AccountSetup.btnContinuePg3of4Info);
           	repo.DOM.AccountSetup.btnContinuePg3of4.MoveTo();
           	repo.DOM.AccountSetup.btnContinuePg3of4.Click();
           	
           	//
           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg4of4Info);
           	repo.DOM.AccountSetup.btnContinuePg4of4.MoveTo();
           	repo.DOM.AccountSetup.btnContinuePg4of4.Click();
           	
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLogoutInfo);
           	repo.DOM.HeaderAdminLogin.btnLogout.MoveTo();
           	repo.DOM.HeaderAdminLogin.btnLogout.Click(Location.Center);
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLoginInfo);
           	
           	//login into Public and verify Access
           	ClearBrowserCache();
			Host.Local.CloseApplication(repo.DOM.Self,new Duration(1000));
           	url = string.Format("{0}{1}", AppSettings.Domain,"/ipprogram/iplogin.aspx");
           	browser = new Browser(browserProduct, url);
           	
           	Validate.Exists(repo.DOM.IP_LoginPage.DivTagBreadcrumbsInfo);
           	Validate.AreEqual(repo.DOM.IP_LoginPage.DivTagBreadcrumbs.InnerText, " » Enterprise login");
           	Validate.Exists(repo.DOM.IP_LoginPage.ImgTagEnterpriseLogoInfo);
           	Validate.Exists(repo.DOM.IP_LoginPage.SpanTagEnterprise_QA_Test_loginInfo);
           	
           	
           	//*******TestCase 2 : Ip login - Validate Error message when logginf from Invalid IP Address************************************************************
            // Permission denied
			// You are trying to log in from an IP address that is not associated with a lynda.com account. For additional assistance, use our contact form or call Customer Service at (888) 335-9632
			
			url = string.Format("{0}{1}{2}","https://admin.", AppSettings.Domain,"/Welcome.aspx");
			GotoAccountSetup(browser, url, username, password, varUsername);
			AccountIPsCleanup();
			
			//login into Public and verify Access
			
			ClearBrowserCache();
			Host.Local.CloseApplication(repo.DOM.Self,new Duration(1000));
			
           	url = string.Format("{0}{1}", AppSettings.Domain,"/ipprogram/iplogin.aspx");
           	browser = new Browser(browserProduct, url);
           	
           	Validate.Exists(repo.DOM.InvalidIP_LoginPage.h1_Permission_deniedInfo);
           	Validate.Exists(repo.DOM.InvalidIP_LoginPage.p_IP_Permission_denied_mesgInfo);
           	string strPermDend = "You are trying to log in from an IP address that is not associated with a lynda.com account. For additional assistance, use our or call Customer Service at (888) 335-9632.";
           	Report.Log(ReportLevel.Info, repo.DOM.InvalidIP_LoginPage.p_IP_Permission_denied_mesg.InnerText);
           	Report.Log(ReportLevel.Info, strPermDend);
           	
           	Host.Local.CloseApplication(repo.DOM.Self, new Duration(0));
			
        }
        
        
    	public void GotoAccountSetup(Browser browser, string url, string username, string password, string testMAusername)
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
           	repo.DOM.SearchPage.txtUsername.PressKeys(testMAusername);
           	
           	repo.DOM.SearchPage.btnSubmit.MoveTo();
           	repo.DOM.SearchPage.btnSubmit.Click();
           	
           	Validate.Exists(repo.DOM.SearchPage.lnkSearchResultsInfo);
           	Validate.AreEqual(repo.DOM.SearchPage.lnkSearchResults.InnerText, testMAusername);
           	repo.DOM.SearchPage.lnkSearchResults.Click(Location.UpperLeft);
           	
           	Validate.Exists(repo.DOM.CustomerDetailsPage.txtUsernameInfo);
           	Validate.AreEqual(repo.DOM.CustomerDetailsPage.txtUsername.Value, testMAusername);
        }
        
    	public void AccountIPsCleanup(string strIPaddress)
        {
        	//The account could be Enterprise Master Admin, Campus Master Admin or Kiosk Master Admin
        	//Get the Master Admin's Product
        	//TODO: handle based on lyndaCampus, Kiosk or Enterprise

				Validate.Exists(repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetupInfo);
           		repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.MoveTo();
           		repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.Click();

           	    Validate.Exists(repo.DOM.AccountSetup.txtFromIPAddress1Info);
           	
           	
           	    int intrownum = 0; 
           	    int intbtnrow = 0;
           	   
           	    //Loop through the table and get the IPAddress Range row that has conflicting IPAddress
           	    foreach (TrTag row in repo.DOM.AccountSetup.tblIPAddresses.Find("./tbody/tr"))
				{  
				     
				    intrownum++;
				    TdTag rowNameCell = row.FindSingle("./td[1]");
				      
				    if(rowNameCell.InnerText.Trim()==(intrownum-1).ToString())
				    {
					    int intcellnum = 0;
					    string[] ipaddrs= {"",""};
					    bool blnIPinRange=false;
				    	foreach (TdTag cell in row.Find("./td"))
					    {  
					          
					        intcellnum++;
					        if(intcellnum == 2 || intcellnum == 3)
					        {
					         cell.SetStyle("background-color","#33ff00");
					         InputTag txtAddress = cell.FindSingle("./input");
					         ipaddrs[intcellnum-2]= txtAddress.Value.Trim();
					         Report.Log(ReportLevel.Info, "IP Address [" + (intcellnum-2).ToString() + "] : " + ipaddrs[intcellnum-2].ToString());
					        }
					        
					        if(ipaddrs[0] != "" && ipaddrs[1]!= "")
				    		{
				    			IPAddressRange iprange = new IPAddressRange(ipaddrs[0], ipaddrs[1]);
				    			blnIPinRange = iprange.IsInRange(strIPaddress);
				    			Report.Log(ReportLevel.Info, "Is " + strIPaddress + " in the range?  " + blnIPinRange.ToString());
				    		}
					        
					        if(intcellnum == 5 && blnIPinRange)
					        {
					        		cell.SetStyle("background-color","#FF0000");
					        		intbtnrow = intrownum-1;
					        }
					    }
				    	
				    }
				      
				}  
           	    
           	   
           	    //Click the delete button on the Row with the conflicting IP Address range
           	    foreach (TrTag row in repo.DOM.AccountSetup.tblIPAddresses.Find("./tbody/tr"))
				{
           	    	    TdTag rowNameCell = row.FindSingle("./td[1]");
           	    	    if(rowNameCell.InnerText.Trim()==intbtnrow.ToString())
					    {
	           	    		TdTag btncell = row.FindSingle("./td[5]");
	           	    		InputTag btnDelete = btncell.FindSingle("./input");
							btnDelete.MoveTo();
							btnDelete.Click();
	           	    	}
           	    }
       
	           	Validate.Exists(repo.DOM.AccountSetup.btnContinueInfo);
	           	repo.DOM.AccountSetup.btnContinue.Click();
	           	Validate.Exists(repo.DOM.AccountSetup.btnApproveInfo);
	           	repo.DOM.AccountSetup.btnApprove.Click();
	           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg3of4Info);
	           	repo.DOM.AccountSetup.btnContinuePg3of4.Click();
	           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg4of4Info);
	           	repo.DOM.AccountSetup.btnContinuePg4of4.Click();
        }
        
    	public void HandleAdminCurrentlyLoggedIn()
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
        
    	public void AccountIPsCleanup()
        {
        	//The account could be Enterprise Master Admin, Campus Master Admin or Kiosk Master Admin
        	//Get the Master Admin's Product
        	//TODO: handle based on lyndaCampus, Kiosk or Enterprise

				Validate.Exists(repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetupInfo);
           		repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.MoveTo();
           		repo.DOM.CustomerDetailsPage.lnklyndaEntAcctSetup.Click();

        	
           	
           	Validate.Exists(repo.DOM.AccountSetup.txtFromIPAddress1Info);
           	//Clean hit delete on all rows
           	int intrownum = 0;
           	int intDelBtnsCnt = 0;
           	foreach (TrTag row in repo.DOM.AccountSetup.tblIPAddresses.Find("./tbody/tr"))
			{  
           		intrownum++;    
           		TdTag rowNameCell = row.FindSingle("./td[1]");
				    if(rowNameCell.InnerText.Trim()==(intrownum-1).ToString())
				    {
					    
				    	foreach (TdTag cell in row.Find("./td[5]"))
					    {  
					        intDelBtnsCnt++;
					        
					    }
				    	
				    }
				      
			}  
           	
           	Report.Log(ReportLevel.Info, "Debug : " + intDelBtnsCnt);
           	    
           	for (int i= intDelBtnsCnt; i >0; --i)
           	{
           		if(Validate.Exists(repo.DOM.AccountSetup.txtFromIPAddress1Info.AbsolutePath, repo.DOM.AccountSetup.txtFromIPAddress1Info.SearchTimeout, "{0}", new Validate.Options(false,ReportLevel.Info)))
           		  ClickbtnDelete(i);
           	}
           	    
           	    
           	
           	Validate.Exists(repo.DOM.AccountSetup.btnContinueInfo);
           	repo.DOM.AccountSetup.btnContinue.Click();
           	
           	Validate.Exists(repo.DOM.AccountSetup.btnApproveInfo);
           	repo.DOM.AccountSetup.btnApprove.Click();
           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg3of4Info);
           	repo.DOM.AccountSetup.btnContinuePg3of4.Click();
           	Validate.Exists(repo.DOM.AccountSetup.btnContinuePg4of4Info);
           	repo.DOM.AccountSetup.btnContinuePg4of4.Click();
           	
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLogoutInfo);
           	repo.DOM.HeaderAdminLogin.btnLogout.MoveTo();
           	repo.DOM.HeaderAdminLogin.btnLogout.Click(Location.Center);
           	Validate.Exists(repo.DOM.HeaderAdminLogin.btnLoginInfo);
        	
        }
 
    	void ClickbtnDelete(int intbutrow)
    	{
    		foreach (TrTag row in repo.DOM.AccountSetup.tblIPAddresses.Find("./tbody/tr"))
				{
           	    	    TdTag rowNameCell = row.FindSingle("./td[1]");
           	    	    if(rowNameCell.InnerText.Trim()==intbutrow.ToString())
					    {
	           	    		TdTag btncell = row.FindSingle("./td[5]");
	           	    		InputTag btnDelete = btncell.FindSingle("./input");
							btnDelete.MoveTo();
							btnDelete.Click();
	           	    	}
           	    }
    	}
    	
    	
        //method to clear cache
     	public  void ClearBrowserCache()
        {
        	switch(AppSettings.Browser)
        	{
        		case BrowserProduct.IE:
				{
        				repo.ClearCacheIE.Windows_Interne.ButtonTools.Click();
        				repo.ClearCacheIE.ContextMenuIexplore.MenuItemInternet_options.MoveTo();
        				repo.ClearCacheIE.ContextMenuIexplore.MenuItemInternet_options.Click();
        				Validate.Exists(repo.ClearCacheIE.FormInternet_Options.TabPageGeneralInfo);
        				repo.ClearCacheIE.FormInternet_Options.TabPageGeneral.Click();
        				repo.ClearCacheIE.FormInternet_Options.ButtonDelete.MoveTo();
        				repo.ClearCacheIE.FormInternet_Options.ButtonDelete.Click();
        				Validate.Exists(repo.ClearCacheIE.FormDelete_Browsing_History.CheckBoxPreserve_Favorites_websiInfo);
        				Validate.Attribute(repo.ClearCacheIE.FormDelete_Browsing_History.CheckBoxPreserve_Favorites_websiInfo,"Checked",false);
        				
        				repo.ClearCacheIE.FormDelete_Browsing_History.ButtonDelete.MoveTo();
        				repo.ClearCacheIE.FormDelete_Browsing_History.ButtonDelete.Click();
        				Delay.Milliseconds(3000);
        				repo.ClearCacheIE.FormInternet_Options.ButtonOK.MoveTo();
        				repo.ClearCacheIE.FormInternet_Options.ButtonOK.Click();
        				
        		break;
        		}
        		case BrowserProduct.Firefox:
        		{
        				repo.ClearCacheFirefox.Firefox.ButtonFirefox.Click();
        				repo.ClearCacheFirefox.Firefox.MenuItemOptions.MoveTo();
        				repo.ClearCacheFirefox.Firefox.MenuItemOptions.Click();
        				Validate.Exists(repo.ClearCacheFirefox.FormOptions.ListItemGeneralInfo);
        				repo.ClearCacheFirefox.FormOptions.ListItemGeneral.MoveTo();
        				repo.ClearCacheFirefox.FormOptions.ListItemGeneral.Click();
        				repo.ClearCacheFirefox.FormOptions.ListItemAdvanced.MoveTo();
        				repo.ClearCacheFirefox.FormOptions.ListItemAdvanced.Click();
        				repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.TabPageGeneral.MoveTo();
        				repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.TabPageGeneral.Click();
        				repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.TabPageNetwork.MoveTo();
        				repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.TabPageNetwork.Click();
        				repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.ButtonClear_Now.MoveTo();
        				repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.ButtonClear_Now.Click();
        				Delay.Milliseconds(3000);
        				Validate.Exists(repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.ButtonClear_Now1Info);
        				repo.ClearCacheFirefox.FormOptions.ContainerAdvanced.ButtonClear_Now1.Click();
        				Delay.Milliseconds(3000);
        				repo.ClearCacheFirefox.FormOptions.ButtonOK.MoveTo();
        				repo.ClearCacheFirefox.FormOptions.ButtonOK.Click();
        				
        		break;		
        		}
        		case BrowserProduct.Chrome:
        		{
        				Report.Log(ReportLevel.Error, String.Format("Code not implemented yet: {0}", AppSettings.Browser.ToString()));	
        		break;		
        		}
        		case BrowserProduct.Safari:
        		{
        				if(Validate.NotExists(repo.ClearCacheSafari.Safari.MenuItemEditInfo.AbsolutePath, repo.ClearCacheSafari.Safari.MenuItemEditInfo.SearchTimeout, "{0}", new Validate.Options(false,ReportLevel.Warn)))
        				 {
        				   	repo.ClearCacheSafari.Safari.DisplaySettings.Click(Location.CenterRight);
        				   	Validate.Exists(repo.ClearCacheSafari.ContextMenuSafari.MenuItemShow_Menu_BarInfo);
        				   	repo.ClearCacheSafari.ContextMenuSafari.MenuItemShow_Menu_Bar.MoveTo();
        				   	repo.ClearCacheSafari.ContextMenuSafari.MenuItemShow_Menu_Bar.Click();
        				  }
        				   
        				   repo.ClearCacheSafari.Safari.MenuItemEdit.MoveTo();
        				   repo.ClearCacheSafari.Safari.MenuItemEdit.Click();
        				   repo.ClearCacheSafari.ContextMenuSafari.MenuItemReset_Safari.MoveTo();
        				   repo.ClearCacheSafari.ContextMenuSafari.MenuItemReset_Safari.Click();
        				  
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxClear_historyInfo, "Checked", "False" ,"{2}",  false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxClear_history.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxReset_Top_SitesInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxReset_Top_Sites.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_all_webpage_previInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_all_webpage_previ.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_the_DownloadsInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_the_Downloads.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_all_website_iconsInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_all_website_icons.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_saved_names_and_pInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_saved_names_and_p.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_other_AutoFill_foInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_other_AutoFill_fo.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxClose_all_Safari_windowsInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxClose_all_Safari_windows.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxReset_all_location_warniInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxReset_all_location_warni.Checked = true;
        				   if(Validate.Attribute(repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_all_website_dataInfo, "Checked", "False" ,"{2}", false))
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.CheckBoxRemove_all_website_data.Checked = true;
        				   
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.ButtonReset.MoveTo();
        				    repo.ClearCacheSafari.FormSafari.ContainerClient.ButtonReset.Click();
        				    Delay.Milliseconds(2000);
        		break;		
        		}
        		default:
        		throw new Exception(String.Format("Code not implemented yet: {0}", AppSettings.Browser.ToString()));
        	}
        }
        
        
    }
}
