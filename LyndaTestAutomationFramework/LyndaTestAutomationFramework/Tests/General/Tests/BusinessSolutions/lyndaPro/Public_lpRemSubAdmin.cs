/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/22/2012
 * Time: 3:00 PM
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

using Tests.AppConfig;
using Lynda.Test.Browsers;
using Lynda.Test.Advanced.Utilities.WebPages;
using Tests.General.Utilities;

namespace Tests.General.Tests.BusinessSolutions.lyndaPro
{
    /// <summary>
    /// Description of Public_lpRemSubAdmin.
    /// </summary>
    [TestModule("4EF5F661-3628-46F2-A92F-7242C002BD46", ModuleType.UserCode, 1)]
    public class Public_lpRemSubAdmin : ITestModule
    {
        
    	private static Public_lpBVT5Repository repo = Public_lpBVT5Repository.Instance;
    	private Browser browser;
    	
    	
    	string _varUsername = "";
    	[TestVariable("576A43C1-2DEB-484F-BEB5-E8DE322202C4")]
    	public string varUsername
    	{
    		get { return _varUsername; }
    		set { _varUsername = value; }
    	}
    	
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Public_lpRemSubAdmin()
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
            
            repo.DOM.Top_Right_Menus.StrongTagLog_in.Click();
            Validate.Exists(repo.DOM.Login_form.txtUsernameInfo);
            repo.DOM.Login_form.txtUsername.PressKeys(varUsername); 
            repo.DOM.Login_form.txtPassword.PressKeys("lynda1");
            repo.DOM.Login_form.btnLogin.Click();
            Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_out_adminInfo);
            repo.DOM.Top_Right_Menus.ATagAdministration.MoveTo();
            repo.DOM.Top_Right_Menus.ATagManage_CreateSubadmin.Click();
            
            Validate.Exists(repo.DOM.Body.tdGroupsManaged_ManageSubAdminPageInfo);
            repo.DOM.Body.ATagRemove_ManageSubadminPage.MoveTo();
            repo.DOM.Body.ATagRemove_ManageSubadminPage.Click();
            
            
            Validate.Exists(repo.FormMessage_from_webpage.ButtonOKInfo);
            repo.FormMessage_from_webpage.ButtonOK.Click();
            
            try
            {
               if (Validate.NotExists(repo.DOM.Body.tdGroupsManaged_ManageSubAdminPageInfo.AbsolutePath, repo.DOM.Body.tdGroupsManaged_ManageSubAdminPageInfo.SearchTimeout,"{0}",new Validate.Options(true,ReportLevel.Error)))
               {
            		repo.DOM.Body.btnContinue_MangeSubadminPage.Click();
               }
            }
            catch(ValidationException ve)
            {
            	Report.Log(ReportLevel.Warn,"ValidationException - " + ve.ToString());
            }
            
            Validate.Exists(repo.DOM.GroupsAndUsers_Grid.txtROFirstGroupNameInfo);
		    Validate.Exists(repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1Info);
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.MoveTo();
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.Click();
		   //Reg User
		   Validate.AreEqual(repo.DOM.GroupsAndUsers_Grid.txtUser1_RegStatus.TagValue.ToString(), "Reg User");
           repo.DOM.Top_Right_Menus.StrongTagLog_out_admin.MoveTo();
		   repo.DOM.Top_Right_Menus.StrongTagLog_out_admin.Click();
		   Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_inInfo);
		   repo.DOM.Top_Right_Menus.StrongTagLog_in.MoveTo();
		   Host.Local.CloseApplication(repo.DOM.Self, new Duration(100));
            
            
        }
    }
}
