/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/21/2012
 * Time: 12:17 PM
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
using Lynda.Test.Advanced.Utilities.WebPages;
using Tests.General.Utilities;

namespace Tests.General.Tests.BVT5
{
    /// <summary>
    /// Description of Public_lpManageUsers.
    /// </summary>
    [TestModule("08315453-3D16-459B-8D03-6084044F00AB", ModuleType.UserCode, 1)]
    public class Public_lpManageUsers : ITestModule
    {
        private static Public_lpBVT5Repository repo = Public_lpBVT5Repository.Instance;
    	private Browser browser;
        
        
        string _varUsername = "";
        [TestVariable("1603EA22-E576-4893-92C5-E2CC5B91B028")]
        public string varUsername
        {
        	get { return _varUsername; }
        	set { _varUsername = value; }
        }
        
        
        
        
        
        
        
        
        
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Public_lpManageUsers()
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
            Mouse.DefaultMoveTime = 250;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            //TODO: Update rxrep to support all Browsers
            if(AppSettings.Browser.ToString() != "IE")
            {
            	Report.Error("Note: Currently only IE Supported; Please change the appsettings Key Browser value to IE and retry.");
            	throw new Ranorex.RanorexException();
            }
            BrowserProduct browserProduct = (BrowserProduct)Enum.Parse(typeof(BrowserProduct), AppSettings.Browser.ToString());
            string url = string.Format("{0}{1}","http://", AppSettings.Domain.ToString());
            browser = new Browser(browserProduct, url);
            
            string strDataFile = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + @"\General\Tests\BVT5\TestData\Public_lpAcctData.xlsx";
            //object[,] testdata = ExcelData.Read(strDataFile,"B2","E5");
            
            repo.DOM.Top_Right_Menus.StrongTagLog_in.Click();
            
            Validate.Exists(repo.DOM.Login_form.txtUsernameInfo);
                        
            repo.DOM.Login_form.txtUsername.PressKeys(varUsername); 
            
            repo.DOM.Login_form.txtPassword.PressKeys("lynda1");
            repo.DOM.Login_form.btnLogin.Click();
            
            Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_out_adminInfo);
            
            Validate.Exists(repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1Info);
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.MoveTo();
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.Click();
		        
		        Validate.Exists(repo.DOM.GroupsAndUsers_Grid.txtUser1_FnameInfo);
		     
		        //TODO: Validate.AreEqual(expectedStringfromExce, StringfromUI);   
		        
		        Report.Log(ReportLevel.Info,SelectTagUI.GetSelectTagCurrentText(repo.DOM.GroupsAndUsers_Grid.selFirstGroupStatus));
		        		        
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser1_Fname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser1_Lname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser1_Email.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser1_RegStatus.TagValue.ToString());
		        //Report.Log(ReportLevel.Info, testdata[0,2].ToString());
		        Report.Log(ReportLevel.Info,SelectTagUI.GetSelectTagCurrentText(repo.DOM.GroupsAndUsers_Grid.selUser1_Status));
		        
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser2_Fname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser2_Lname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser2_Email.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser2_RegStatus.TagValue.ToString());
		        //Report.Log(ReportLevel.Info, testdata[1,2].ToString());
		        Report.Log(ReportLevel.Info,SelectTagUI.GetSelectTagCurrentText(repo.DOM.GroupsAndUsers_Grid.selUser2_Status));
		        
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser3_Fname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser3_Lname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser3_Email.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser3_RegStatus.TagValue.ToString());
		        //Report.Log(ReportLevel.Info, testdata[2,2].ToString());
		        Report.Log(ReportLevel.Info,SelectTagUI.GetSelectTagCurrentText(repo.DOM.GroupsAndUsers_Grid.selUser3_Status));
		        
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser4_Fname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser4_Lname.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser4_Email.TagValue.ToString());
		        Report.Log(ReportLevel.Info,repo.DOM.GroupsAndUsers_Grid.txtUser4_RegStatus.TagValue.ToString());
		        //Report.Log(ReportLevel.Info, testdata[3,2].ToString());
		        Report.Log(ReportLevel.Info,SelectTagUI.GetSelectTagCurrentText(repo.DOM.GroupsAndUsers_Grid.selUser4_Status));
		                   
		        Report.Log(ReportLevel.Info,SelectTagUI.GetSelectTagCurrentText(repo.DOM.GroupsAndUsers_Grid.selSecondGroupStatus));
		     
		     repo.DOM.Top_Right_Menus.StrongTagLog_out_admin.Click();
		     
		     Host.Local.CloseApplication(repo.DOM.Self, new Duration(0));
            
        }
    }
}
