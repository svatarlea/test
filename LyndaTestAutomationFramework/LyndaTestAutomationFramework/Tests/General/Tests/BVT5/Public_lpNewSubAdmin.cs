/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/22/2012
 * Time: 11:55 AM
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
using Tests.AppConfig;
using Lynda.Test.Browsers;
using Tests.General.Utilities;
using Lynda.Test.Advanced.Utilities.WebPages;

namespace Tests.General.Tests.BVT5.TestData
{
    /// <summary>
    /// Description of Public_lpNewSubAdmin.
    /// </summary>
    [TestModule("3F31D071-EF12-455D-85DA-C40EB003EFC0", ModuleType.UserCode, 1)]
    public class Public_lpNewSubAdmin : ITestModule
    {
        private static Public_lpBVT5Repository repo = Public_lpBVT5Repository.Instance;
    	private Browser browser;
    	
    	
    	string _varUsername = "";
    	[TestVariable("B4B312A7-E6F9-479F-87B6-4E788F3B014E")]
    	public string varUsername
    	{
    		get { return _varUsername; }
    		set { _varUsername = value; }
    	}
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Public_lpNewSubAdmin()
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
            //TODO: Update rxrep to support all Browsers
            if(AppSettings.Browser != BrowserProduct.IE)
            {
            	Report.Error("Note: Currently only IE Supported; Please change the appsettings Key - Browser value to IE and retry.");
            	throw new Ranorex.RanorexException();
            }
            BrowserProduct browserProduct = AppSettings.Browser;
            string url = string.Format("{0}{1}","http://", AppSettings.Domain);
            browser = new Browser(browserProduct, url);
            string strResultsFile = Directory.GetCurrentDirectory() + @"\Public_lpAcctData.xlsx";
            
            repo.DOM.Top_Right_Menus.StrongTagLog_in.Click();
            Validate.Exists(repo.DOM.Login_form.txtUsernameInfo);
            repo.DOM.Login_form.txtUsername.PressKeys(varUsername); 
            
            repo.DOM.Login_form.txtPassword.PressKeys("lynda1");
            repo.DOM.Login_form.btnLogin.Click();
            Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_out_adminInfo);
            repo.DOM.Top_Right_Menus.ATagAdministration.MoveTo();
            repo.DOM.Top_Right_Menus.ATagManage_CreateSubadmin.Click();
            
            Validate.Exists(repo.DOM.Body.btnNewSubAdminInfo);
            repo.DOM.Body.btnNewSubAdmin.Click();
            Validate.Exists(repo.DOM.Body.selGroupName_CreateSubadminPageInfo);
            SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.Body.selGroupName_CreateSubadminPage, "QA_Group1");
		    
		    SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.Body.selUserName_CreateSubadminPage, "TestLname, TestFnamea");

			foreach (OptionTag option in repo.DOM.Body.selGroupsAvail_CreateSubAdminPage.Find(".//option", 30000))
			{
				if(option.InnerText == "QA_Group1")
				{
					option.Focus();
					option.Click();
				}	
			}
	
		    //TODO: validate repo.DOM.Body.selGroupsManaged_CreateSubadminPage is empty 
		        	
		    repo.DOM.Body.btnAddGrouptoManage_CreateSubadminPage.Click();
		    		    
		    //TODO: validate repo.DOM.Body.selGroupsManaged_CreateSubadminPage has the added group id
		    
		   	repo.DOM.Body.ATagGSel_GrpMgmtPermsns_CreateSubadminPage.Click();
		    
		    repo.DOM.Body.ATagUSel_UsrMgmtPermsns_CreateSubadminPage.Click();
		    
		    repo.DOM.Body.btnSubmit_CreateSubadminPage.MoveTo();
		    repo.DOM.Body.btnSubmit_CreateSubadminPage.Click();
		    
		    Validate.Exists(repo.DOM.Body.tdGroupsManaged_CreateSubAdminPageInfo);
		    Validate.AreEqual(repo.DOM.Body.tdGroupsManaged_CreateSubAdminPage.InnerText,"QA_Group1");
		    
		    repo.DOM.Body.btnContinue_CreateSubadminPage.MoveTo();
		    repo.DOM.Body.btnContinue_CreateSubadminPage.Click();
		    
		    Validate.Exists(repo.DOM.DivTagCtl00_main_divSendEmail.selEmailTemplateTypeInfo);
		    repo.DOM.DivTagCtl00_main_divSendEmail.btnSendEmail.MoveTo();
		    repo.DOM.DivTagCtl00_main_divSendEmail.btnSendEmail.Click();
		    
		    Validate.Exists(repo.DOM.Body.lbSendEmailConfirmInfo);
		    repo.DOM.Body.btnContinue_CreateSubadmincomplete.MoveTo();
		    repo.DOM.Body.btnContinue_CreateSubadmincomplete.Click();
		    
		    Validate.Exists(repo.DOM.GroupsAndUsers_Grid.txtROFirstGroupNameInfo);
		    Validate.Exists(repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1Info);
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.MoveTo();
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.Click();
		   //Reg Sub-Administrator
		   Validate.AreEqual(repo.DOM.GroupsAndUsers_Grid.txtUser1_RegStatus.TagValue.ToString(), "Reg Sub-Administrator");
		   
		   repo.DOM.Top_Right_Menus.StrongTagLog_out_admin.MoveTo();
		   repo.DOM.Top_Right_Menus.StrongTagLog_out_admin.Click();
		   Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_inInfo);
		   Host.Local.CloseApplication(repo.DOM.Self, new Duration(100));
		    
		    
        }
    }
}
