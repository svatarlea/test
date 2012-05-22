/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 1:18 PM
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

namespace Tests.General.Tests.BVT5
{
    /// <summary>
    /// Description of Public_lpCleanGroupsUsers.
    /// </summary>
    [TestModule("56DF0CF4-CF34-41FA-B76B-FF8EF0BB3600", ModuleType.UserCode, 1)]
    public class Public_lpCleanGroupsUsers : ITestModule
    {
        private static Public_lpBVT5Repository repo = Public_lpBVT5Repository.Instance;
        private Browser browser;
        
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// 
        
        
        string _varUsername = "";
        [TestVariable("3BA8D5D8-0134-4025-ACFB-516326AF8E0E")]
        public string varUsername
        {
        	get { return _varUsername; }
        	set { _varUsername = value; }
        }
        
        
        public Public_lpCleanGroupsUsers()
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
            Mouse.DefaultMoveTime = 200;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            BrowserProduct browserProduct = (BrowserProduct)Enum.Parse(typeof(BrowserProduct), AppSettings.Browser.ToString());
            string url = string.Format("{0}{1}","http://", AppSettings.Domain.ToString());
            browser = new Browser(browserProduct, url);
            
            
            repo.DOM.Top_Right_Menus.StrongTagLog_in.Click();
            
            Validate.Exists(repo.DOM.Login_form.txtUsernameInfo);
            
            repo.DOM.Login_form.txtUsername.PressKeys(varUsername);  
            
            
            repo.DOM.Login_form.txtPassword.PressKeys("lynda1");
            repo.DOM.Login_form.btnLogin.Click();
            
            Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_out_adminInfo);
            
            repo.DOM.Top_Right_Menus.ATagAdministration.MoveTo();
            repo.DOM.Top_Right_Menus.ATagLyndaPro_home.Click();
            
             Validate.Exists(repo.DOM.GroupsAndUsers_Grid.txtROFirstGroupNameInfo);
             
           
             if (repo.DOM.GroupsAndUsers_Grid.chkSelectAllGroups.Checked == "False")
             {	
             	Report.Log(ReportLevel.Info, "Select All unChecked false");
             	repo.DOM.GroupsAndUsers_Grid.chkSelectAllGroups.Click();
             }
             
             repo.DOM.GroupsAndUsers_Grid.imgActionMenuG.MoveTo();
             repo.DOM.GroupsAndUsers_Grid.imgActionMenuG.Click();
             repo.DOM.ManageGroupsActionMenu.lnkDelSelectedGroups.Click();
             repo.DOM.Cofirmation_Delete.btnConfirmDeleteOk.Click();
             
             Validate.Exists(repo.DOM.Body.btnAddGroupsInfo);
             
             repo.DOM.Top_Right_Menus.StrongTagLog_out_admin.Click();
             Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_inInfo);
             Host.Local.CloseApplication(repo.DOM.Self, new Duration(0));
        }
    }
}
