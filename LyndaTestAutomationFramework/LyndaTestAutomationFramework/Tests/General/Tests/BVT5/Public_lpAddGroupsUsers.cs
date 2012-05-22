/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 1:12 PM
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

namespace Tests.General.Tests.BVT5
{
    /// <summary>
    /// Description of Public_lpAddGroupsUsers.
    /// </summary>
    [TestModule("A6FB8831-FB06-43CA-A7C2-9B1ACCD64646", ModuleType.UserCode, 1)]
    public class Public_lpAddGroupsUsers : ITestModule
    {
        
    	private static Public_lpBVT5Repository repo = Public_lpBVT5Repository.Instance;
    	private Browser browser;
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Public_lpAddGroupsUsers()
        {
            // Do not delete - a parameterless constructor is required!
        }

        
        string _varUsername = "";
        [TestVariable("95874721-24BC-4CD4-AD41-6A595FA81489")]
        public string varUsername
        {
        	get { return _varUsername; }
        	set { _varUsername = value; }
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
            BrowserProduct browserProduct = (BrowserProduct)Enum.Parse(typeof(BrowserProduct), AppSettings.Browser.ToString());
            string url = string.Format("{0}{1}","http://", AppSettings.Domain.ToString());
            browser = new Browser(browserProduct, url);
            
            
            string strResultsFile = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + @"\General\Tests\BVT5\TestData\Public_lpAcctData.xlsx";
            
            
            
            
            
            repo.DOM.Top_Right_Menus.StrongTagLog_in.Click();
            
            Validate.Exists(repo.DOM.Login_form.txtUsernameInfo);
            
            
            repo.DOM.Login_form.txtUsername.PressKeys(varUsername); 
            
            repo.DOM.Login_form.txtPassword.PressKeys("lynda1");
            repo.DOM.Login_form.btnLogin.Click();
            
            Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_out_adminInfo);
            
            repo.DOM.Top_Right_Menus.ATagAdministration.MoveTo();
            repo.DOM.Top_Right_Menus.ATagLyndaPro_home.Click();
            //repo.DOM.WebDocumentSoftware_training_online.WaitForDocumentLoaded();
            
            Validate.Exists(repo.DOM.Body.btnAddGroupsInfo);
            repo.DOM.Body.btnAddGroups.Click();
            
            Validate.Exists(repo.DOM.Modal_AddGroups.lblAvailLicensesInfo);
            	repo.DOM.Modal_AddGroups.txtAGtxtGroupName.PressKeys("QA_Group1");
            	repo.DOM.Modal_AddGroups.txtAGtxtNumLicenses.PressKeys("3");
            	repo.DOM.Modal_AddGroups.selGroupStatus.Focus();
            	repo.DOM.Modal_AddGroups.selGroupStatus.Click();
            	    String itemToClickRxPath = String.Format("/container[@caption='selectbox']/listitem[@accessiblename='{0}']","Active");
		        	ListItem itemToClick = itemToClickRxPath;
		        	itemToClick.Click();          
            
		        repo.DOM.Modal_AddGroups.btnAdd_group.Click();
		        
		        Validate.Exists(repo.DOM.GroupsAndUsers_Grid.txtROFirstGroupNameInfo);
		        Report.Log(ReportLevel.Info, repo.DOM.GroupsAndUsers_Grid.txtROFirstGroupName.TagValue.ToString());
		        
		        
		        //repo.DOM.GroupsAndUsers_Grid.imgActionMenuG.Click();
		       
		        // To create n number of Groups :  noOfGroups 
		        //int noOfGroups = 2;
		        for (int i=2;i <=2 ; i++ ) {
		        	 Validate.Exists(repo.DOM.GroupsAndUsers_Grid.imgActionMenuGInfo);
		        	 repo.DOM.GroupsAndUsers_Grid.imgActionMenuG.MoveTo();
		        	 repo.DOM.GroupsAndUsers_Grid.imgActionMenuG.Click();
		        	 Validate.Exists(repo.DOM.ManageGroupsActionMenu.lnkAddGroupsInfo);
		        	 repo.DOM.ManageGroupsActionMenu.lnkAddGroups.MoveTo();
		        	 repo.DOM.ManageGroupsActionMenu.lnkAddGroups.Click();
		        	 repo.DOM.Modal_AddGroups.txtAGtxtGroupName.PressKeys("QA_Group"+i);
            		 repo.DOM.Modal_AddGroups.txtAGtxtNumLicenses.PressKeys("1");
            	     repo.DOM.Modal_AddGroups.selGroupStatus.Focus();
            	     repo.DOM.Modal_AddGroups.selGroupStatus.Click();
            	     string itemToClickRxPath1 = String.Format("/container[@caption='selectbox']/listitem[@accessiblename='{0}']","Inactive");
		        	 ListItem itemToClick1 = itemToClickRxPath1;
		        	 itemToClick1.Click();          
            		
		             repo.DOM.Modal_AddGroups.btnAdd_group.Click();
		             
		        }
		        
		        //To create n number of User for Group 1 : noOfUsers 
		        Validate.Exists(repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1Info);
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.MoveTo();
		        repo.DOM.GroupsAndUsers_Grid.imgClick_to_show_hide_Users_1.Click();
		        string strGrpNameAlt = repo.DOM.GroupsAndUsers_Grid.txtROFirstGroupName.Alt.ToString();
             	Report.Log(ReportLevel.Info, strGrpNameAlt);
             	string[] strGrpId = strGrpNameAlt.Split(' ');
             	Report.Log(ReportLevel.Info, strGrpId[2]);
             	
		        Validate.Exists(repo.DOM.GroupsAndUsers_Grid.spnText_NoUsersInfo);
		        Report.Info(repo.DOM.GroupsAndUsers_Grid.spnText_NoUsers.TagValue.ToString());
		        
		        repo.DOM.GroupsAndUsers_Grid.chkFirstGroup.MoveTo();
		        repo.DOM.GroupsAndUsers_Grid.chkFirstGroup.Click();
		        
		        int noOfUsers = 4; char currletter = 'z';
				for (int i=1;i <=noOfUsers ; i++ ) {
		         Validate.Exists(repo.DOM.GroupsAndUsers_Grid.imgActionMenuGInfo);
		         repo.DOM.GroupsAndUsers_Grid.imgActionMenuG.MoveTo();
		         repo.DOM.GroupsAndUsers_Grid.imgActionMenuG.Click();
				 Validate.Exists(repo.DOM.ManageGroupsActionMenu.lnkAddUsersInfo);
				 repo.DOM.ManageGroupsActionMenu.lnkAddUsers.MoveTo();
				 repo.DOM.ManageGroupsActionMenu.lnkAddUsers.Click();
				 
				 Report.Log(ReportLevel.Info,repo.DOM.Modal_AddUsers.txtAUtxtGroupName.TagValue.ToString());
				 
				 currletter = nameletterseq(currletter);
				 
				 repo.DOM.Modal_AddUsers.txtAUtxtFirstName.PressKeys("TestFname" + currletter);
				 repo.DOM.Modal_AddUsers.txtAUtxtLastName.PressKeys("TestLname");
				 repo.DOM.Modal_AddUsers.txtAUtxtEmail.PressKeys("TestFname-"+ currletter  +"@mailinator.com");
				 repo.DOM.Modal_AddUsers.selAUddlstUserStatus.Focus();
				 repo.DOM.Modal_AddUsers.selAUddlstUserStatus.Click();
				 string strUserStatus = "Active";
				 string itemToClickRxPath2 = String.Format("/container[@caption='selectbox']/listitem[@accessiblename='{0}']",strUserStatus);
				 if(i == 3)
				 {
				 	strUserStatus = "Inactive";
				 	itemToClickRxPath2 = String.Format("/container[@caption='selectbox']/listitem[@accessiblename='{0}']",strUserStatus);
				 }
			        	ListItem itemToClick2 = itemToClickRxPath2;
			        	itemToClick2.Click();
			        	
			      repo.DOM.Modal_AddUsers.btnAdd_user.Click();
			      
			      Validate.Exists(repo.DOM.RegnEmail_Yes_No.btnNoRegnEmailInfo);
			      repo.DOM.RegnEmail_Yes_No.btnNoRegnEmail.MoveTo();
			      repo.DOM.RegnEmail_Yes_No.btnNoRegnEmail.Click();
			      ExcelData.Write(strResultsFile , i+1, 2, strGrpId[2],"TestFname-"+ currletter  +"@mailinator.com", strUserStatus );
		        }
            repo.DOM.Top_Right_Menus.StrongTagLog_out_admin.Click();
            Validate.Exists(repo.DOM.Top_Right_Menus.StrongTagLog_inInfo);
            Host.Local.CloseApplication(repo.DOM.Self, new Duration(0));
            
        }
        
        char nameletterseq(char currletter)
        {
        	char nextChar;
        	
        	if (currletter == 'z')
    		nextChar = 'a'; 
			else if (currletter == 'Z') 
    		nextChar = 'A'; 
 			else 
    		nextChar = (char)(((int) currletter) + 1); 
 			
           return nextChar;
        }
        
        void CheckEmailFlow()
        {
        	
        }
        
            
            
            
        }
}
