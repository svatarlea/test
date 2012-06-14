/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 11:27 AM
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

using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;
using Tests.AppConfig;
using Tests.General.Utilities;

namespace Tests.General.Tests.BVT5
{
   
	
    /// <summary>
    /// Description of Public_NewAcctAccess.
    /// </summary>
    [TestModule("A69389BD-527D-4B6D-B0FF-557E1E283C9E", ModuleType.UserCode, 1)]
    public class Public_NewAcctAccess : ITestModule
    {
        private static Public_NewAcctAccessRepository repo = Public_NewAcctAccessRepository.Instance;
    	private Browser	browser;
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Public_NewAcctAccess()
        {
            // Do not delete - a parameterless constructor is required!
        }

        
        string _varUserName = "";
        [TestVariable("613EC119-8E51-4F27-9CD2-BE4F82CD939B")]
        public string varUserName
        {
        	get { return _varUserName; }
        	set { _varUserName = value; }
        }
        
        
        string _varPersona = "";
        [TestVariable("A4730979-E4FE-4FB3-A8F1-1BCE4DFD6869")]
        public string varPersona
        {
        	get { return _varPersona; }
        	set { _varPersona = value; }
        }
        
        
        
        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        
        void ITestModule.Run()
        {
           Mouse.DefaultMoveTime = 100;
            Keyboard.DefaultKeyPressTime = 50;
            Delay.SpeedFactor = 1.0;
            BrowserProduct browserProduct = AppSettings.Browser;
            string url = string.Format("{0}{1}","http://", AppSettings.Domain);             
            
            varUserName = TestCase.Current.DataContext.CurrentRow["Username"];
            varPersona  = TestCase.Current.DataContext.CurrentRow["persona"];
            string password = "lynda1";
            
                browser = new Browser(browserProduct, url);    
                //Wait for page to load
            	Validate.Exists(repo.DOM.SomeBodyTag.btnMainLog_in);
            	repo.DOM.SomeBodyTag.btnMainLog_in.Click();
            	
            	Validate.Exists(repo.DOM.SomeBodyTag.btnModalLog_inInfo);
            	repo.DOM.SomeBodyTag.txtUsername.PressKeys(varUserName);
            	repo.DOM.SomeBodyTag.txtUserPassword.PressKeys(password);
            	repo.DOM.SomeBodyTag.btnModalLog_in.Click();
            	
            	try
            	{
            		if (Validate.Exists(repo.DOM.SomeBodyTag.btnI_acceptInfo.AbsolutePath, repo.DOM.SomeBodyTag.btnI_acceptInfo.SearchTimeout,"{0}",new Validate.Options(false,ReportLevel.Warn)))
            		    {
            		    	Report.Log(ReportLevel.Info, varPersona + " : " + varUserName + " : First Login.");
            		    	repo.DOM.SomeBodyTag.btnI_accept.Click();
            		    	
            		    	Delay.Milliseconds(1000);
            		    	if (Validate.Exists(repo.DOM.SomeBodyTag.btnMainLog_outInfo.AbsolutePath, repo.DOM.SomeBodyTag.btnMainLog_outInfo.SearchTimeout,"{0}",new Validate.Options(true,ReportLevel.Error)))
            		    	 repo.DOM.SomeBodyTag.btnMainLog_out.Click();
            		    	
            		    }
            		    else
            		    {
            		    	if ( varPersona == "lyndaCampus admin" || varPersona == "lyndaEnterprise admin" ||  varPersona == "lyndaEnterprise admin" )
            		    		Report.Log(ReportLevel.Info, varPersona + " : " + varUserName + " : User logged in successfully.");
            		    	else
            		    	    Report.Log(ReportLevel.Warn, varPersona + " : " + varUserName + " : User has logged in previously.");
            		    		
            		    	if (Validate.Exists(repo.DOM.SomeBodyTag.btnMainLog_outInfo.AbsolutePath, repo.DOM.SomeBodyTag.btnMainLog_outInfo.SearchTimeout,"{0}",new Validate.Options(true,ReportLevel.Error)))
            		    	 repo.DOM.SomeBodyTag.btnMainLog_out.Click();
            		    }
            	}
            	catch(ValidationException e)
            	{
            		Report.Log(ReportLevel.Error, e.ToString());
            	}
            	catch(Exception e)
            	{
            		Report.Log(ReportLevel.Error, e.ToString());
            	}
            	Validate.Exists(repo.DOM.SomeBodyTag.btnMainLog_inInfo);
            	Host.Local.CloseApplication(repo.DOM.Self, new Duration(0));
        }
    }
}
