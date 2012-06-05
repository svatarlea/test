/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 1:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Tests.AppConfig;
using Tests.General.Utilities;
using Tests.General.Utilities.Forms;
using WinForms = System.Windows.Forms;

namespace Tests.General.Tests.BVT5
{
    /// <summary>
    /// Description of Public_lpNewUser_RegisterLogin.
    /// </summary>
    [TestModule("13F3DD1C-4C7A-4F4A-A4D9-F42F3DF4104D", ModuleType.UserCode, 1)]
    public class Public_lpNewUser_RegisterLogin : ITestModule
    {
        
    	private static Public_lpBVT5Repository repo = Public_lpBVT5Repository.Instance;
    	private Browser browser;
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Public_lpNewUser_RegisterLogin()
        {
            // Do not delete - a parameterless constructor is required!
        }

        
        string _varGroupCode = "";
        [TestVariable("33709D07-6267-4323-A1D4-4AD77257FB4D")]
        public string varGroupCode
        {
        	get { return _varGroupCode; }
        	set { _varGroupCode = value; }
        }
        
        
        
        string _varUserEmail = "";
        [TestVariable("84503917-B646-410D-B189-B3F2A22F3EF1")]
        public string varUserEmail
        {
        	get { return _varUserEmail; }
        	set { _varUserEmail = value; }
        }
        
        
        string _varUserstatus = "";
        [TestVariable("B61E2514-DE87-4953-A94C-AFC36CD9D3F3")]
        public string varUserstatus
        {
        	get { return _varUserstatus; }
        	set { _varUserstatus = value; }
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
            	Report.Error("Note: Currently only IE Supported; Please change the appsettings Key - Browser value to IE and retry.");
            	throw new Ranorex.RanorexException();
            }
            BrowserProduct browserProduct = AppSettings.Browser;
            string url = string.Format("{0}{1}{2}","http://", AppSettings.Domain,"/lyndaPro/UserRegistration/UserRegistrationStep1.aspx");
            browser = new Browser(browserProduct, url);
            
            string strResultsFile = Directory.GetCurrentDirectory() + @"\Public_lpAcctData.xlsx";
            
            Validate.Exists(repo.DOM.Body.txtGroupCode_RegnPage1Info);
            repo.DOM.Body.txtGroupCode_RegnPage1.PressKeys(varGroupCode);
            repo.DOM.Body.txtTagEmail_RegnPage1.PressKeys(varUserEmail);
            
            repo.DOM.Body.btnTagContinue_RegnPage1.Click();
            
            Validate.Exists(repo.DOM.UserRegnPage2.selAlreadyMember_RegnPage2Info);
            SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.UserRegnPage2.selAlreadyMember_RegnPage2, "No");

		    Validate.Exists(repo.DOM.UserRegnPage2.txtUsername_RegnPage2Info);
		    

             	string strUsername, strEmail;
            	FormDataAccount.GenerateUsernameEmail(out strUsername, out strEmail);
            	
            	repo.DOM.UserRegnPage2.txtUsername_RegnPage2.PressKeys("lpuser-"+strUsername);
             	
             	repo.DOM.UserRegnPage2.txtPassword_RegnPage2.PressKeys("lynda1");
             	repo.DOM.UserRegnPage2.txtPasswordConfirm_RegnPage2.PressKeys("lynda1");
             	
             	repo.DOM.UserRegnPage2.btnContinue_RegnPage2.Click();
             	
             	ExcelData.Write(strResultsFile, TestCase.Current.DataContext.CurrentRowIndex+1,  5, "lpuser-"+strUsername);
             	
             	if(varUserstatus=="Active")
             	{
	             	//Confirmation Message for Active User.
	               	//Your registration is complete! You can now log in to the lynda.com Online Training Library® using the log in link.
	               	//TODO: Validate.AreEqual(expectedString, StringfromUI); 
	               	Report.Log(ReportLevel.Info, repo.DOM.UserRegnPage2.ConfirmationMessage_forActiveUsers.GetInnerHtml());
             	}
             	else
             	{
             		//Confirmation Message for Inactive User.
             		//Your registration is complete! However, at this point, you can not log in as your account is inactive. Please contact your account administrator to activate your account.
               		//TODO: Validate.AreEqual(expectedString, StringfromUI); 
             		Report.Log(ReportLevel.Info, repo.DOM.UserRegnPage2.ConfirmationMessage_forInactiveUsers.GetInnerHtml());
               	}    
             	
               	Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_inInfo);
               	repo.DOM.Top_Right_Menus.StrongTagLog_in.Click();
               
               	Validate.Exists(repo.DOM.Login_form.txtUsernameInfo);            
            	repo.DOM.Login_form.txtUsername.PressKeys("lpuser-"+strUsername);  
                repo.DOM.Login_form.txtPassword.PressKeys("lynda1");
            	repo.DOM.Login_form.btnLogin.Click();
            	
            	//Message:Inactive Account
                //Your account administrator has deactivated your account. If you have questions please contact your lyndaPro account administrator.
				//TODO: Validate.AreEqual(expectedString, StringfromUI);
				
				if(varUserstatus=="Active")
				{
            	
                Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_outInfo);
                //TODO : Browse Course and Watch Videos.
                
                repo.DOM.Top_Right_Menus.StrongTagLog_out.Click();
                Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_inInfo);
                repo.DOM.Top_Right_Menus.StrongTagLog_in.MoveTo();
				}
				else
				{
					//TODO: Validate.AreEqual(expectedString, StringfromUI); 
					Report.Log(ReportLevel.Info, repo.DOM.UserRegnPage2.Message_forInactiveUser.GetInnerHtml());
				}
                
				Host.Local.CloseApplication(repo.DOM.Self, new Duration(100));
        }
    }
}
