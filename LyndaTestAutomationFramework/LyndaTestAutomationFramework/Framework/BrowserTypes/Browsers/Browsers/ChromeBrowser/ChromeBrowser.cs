using System;
using System.Collections.Generic;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;
using Ranorex.Controls;

using Browsers;
using Browsers.ChromeBrowser;

namespace Lynda.Test.Browsers
{

    /// <summary>
    /// Represents a Chrome browser.
    /// </summary>
    internal class ChromeBrowser : BrowserBasic
    {   	    	
    	/// <summary>
        /// Version (Major version) of chrome exe that this class supports.
        /// </summary>
    	static int supportedExeMajorVersion = 19;
    	
    	/// <summary>
        /// Expected full directory path of chrome exe including the exe.
        /// </summary>
        static string expectedExePath = null;
        
        static string installedExePath = null;
        static int installedVersion = 0;  	
    	
    	/// <summary>
        /// Chrome Ranorex repository instance for this Chrome browser window.
        /// </summary>
        internal Chrome chromeRepo = null;
     
        /// <summary>
        /// Static constructor for Lynda.Test.Browsers.ChromeBrowser class.
        /// </summary>
        static ChromeBrowser()
        {
        	expectedExePath = string.Format("{0}{1}",BrowserSystemInfo.LocalAppDataPath,"\\Google\\Chrome\\Application\\chrome.exe");
			installedExePath = BrowserSystemInfo.GetInstalledExePath(expectedExePath);
        	installedVersion = BrowserSystemInfo.GetInstalledVersion(expectedExePath);
        }    
        
        /// <summary>
        /// Initializes a new instance of the Lynda.Test.Browsers.ChromeBrowser class, opens a new Chrome window and navigates to
        /// the specified uri.
        /// </summary>
        /// <param name="uri">uri to navigate to in the new Chrome window.</param>
        internal ChromeBrowser(string uri, bool killExisting)
        {
            if (installedExePath == null)
        	{
        		throw new Exception(string.Format("Chrome is not installed. Expected location:{0}",expectedExePath));
        	}
        	if (installedVersion != supportedExeMajorVersion)
    		{
    			throw new Exception(string.Format("Chrome version {0} ({1}) is not supported by this class. Supported version: {2}.", installedVersion, installedExePath, supportedExeMajorVersion));
    		}
        	
        	chromeRepo = new Chrome();

            //Kill existing browser processes before opening a new browser
            if (killExisting)
            {
                Host.Local.KillBrowser(BrowserProduct.Chrome.ToString());
            }

            //Open browser           
            OpenBrowser(uri, BrowserProduct.Chrome.ToString(), new RxPath("/form[@class='Chrome_WidgetWin_1']"));
            //Update repository instance base path to include native window handle attribute for the form
            chromeRepo.Form.BasePath = new RxPath(String.Format("/form[@class='Chrome_WidgetWin_1' and @handle='{0}']", handle));
            Validate.Exists(chromeRepo.Form.BasePath);
        }
        
        /// <summary>
        /// Major part of the file version of browser exe.
        /// Will be 0 if browser exe is not installed.
        /// </summary>
        internal static int InstalledVersion
        {
        	get{return installedVersion;}
        }
        
		/// <summary>
		/// Full directory path of installed browser exe.
		/// Will be null if browser exe is not installed.
		/// </summary>
        internal static string InstalledExePath
        {
        	get{return installedExePath;}
        }
        
        /// <summary>
        /// Version (Major version) of chrome exe that this class supports.
        /// </summary>
        internal static int SupportedExeMajorVersion
        {
        	get {return supportedExeMajorVersion;}
        }

        /// <summary>
        /// Get the uri from this browser window's navigation edit box.
        /// </summary>
        internal string CurrentUri
        {
            get
            {
                Validate.Exists(chromeRepo.Form.NavigateEditBox);
                return chromeRepo.Form.NavigateEditBox.TextValue;
            }
        }
        
        /// <summary>
        /// Navigate to specified uri by typing it in the browser's navigate edit box and then pressing the {ENTER} key.
        /// </summary>
        /// <param name="uri">uri to navigate to</param>
        internal void Navigate(string uri)
        {
            string navigateUri = ValidateUri(uri, true);
            //Click title bar of this window first so the navigate edit box is in the active window so it can be typed into
            ClickTitleBar();
            Validate.Exists(chromeRepo.Form.NavigateEditBox);
            
            //Ctrl-A to select all text then release Ctrl. Used so all text can be selected first before being typed over.
        	const string pressKeysSelectAll = "{Control down}{Akey}{Control up}";
        	chromeRepo.Form.NavigateEditBox.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, navigateUri));
        }

        /// <summary>
        /// Click the title bar of this browser window.
        /// </summary>
        internal void ClickTitleBar()
        {
            Validate.Exists(chromeRepo.Form.TitleBarTabPageList);
            //Click using move time, otherwise a click too soon after a previous call to ClickTitleBar() acts like a double-click on the title bar
            //(which can change the window size).
            chromeRepo.Form.TitleBarTabPageList.Click(new Duration(250));
        }

        /// <summary>
        /// Half the size of this browser window.
        /// </summary>
        internal void HalfSize()
        {
            base.HalfSize(chromeRepo.Form.Self);           
        }

        /// <summary>
        /// Move this browser window
        /// </summary>
        /// <param name="x">Adds this paramter to browser window's current screen location x-coordinate.</param>
        /// <param name="y">Adds this paramter to browser window's current screen location y-coordinate.</param>
        internal void Move(int x, int y)
        {
            base.Move(chromeRepo.Form.Self, x, y);       
        }

        /// <summary>
        /// Included for API test stability purposes. Move this browser window down and up quickly four times.
        /// </summary>
        internal void Fun()
        {
            base.Fun(chromeRepo.Form.Self);
        }

    }  
    
}