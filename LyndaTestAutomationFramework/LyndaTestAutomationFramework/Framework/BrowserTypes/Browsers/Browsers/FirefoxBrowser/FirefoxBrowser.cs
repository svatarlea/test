using System;
using System.Collections.Generic;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;
using Ranorex.Controls;

using Browsers;
using Browsers.FirefoxBrowser;

namespace Lynda.Test.Browsers
{

    /// <summary>
    /// Represents a Firefox browser.
    /// </summary>
    internal class FirefoxBrowser : BrowserBasic
    {
        /// <summary>
        /// Version (Major version) of firefox exe that this class supports.
        /// </summary>
    	static int supportedExeMajorVersion = 13;
    	
    	/// <summary>
        /// Expected full directory path of firefox exe including the exe.
        /// </summary>
        static string expectedExePath = null;
        
        static string installedExePath = null;
        static int installedVersion = 0;  	
    	
    	/// <summary>
        /// Firefox Ranorex repository instance for this Firefox browser window
        /// </summary>
        internal Firefox firefoxRepo = null;
        
        /// <summary>
        /// Static constructor for Lynda.Test.Browsers.FirefoxBrowser class.
        /// </summary>
        static FirefoxBrowser()
        {
        	expectedExePath = string.Format("{0}{1}",BrowserSystemInfo.ProgramFilesX86Path,"\\Mozilla Firefox\\firefox.exe");
			installedExePath = BrowserSystemInfo.GetInstalledExePath(expectedExePath);
        	installedVersion = BrowserSystemInfo.GetInstalledVersion(expectedExePath);
        }   
     
        /// <summary>
        /// Initializes a new instance of the Lynda.Test.Browsers.FirefoxBrowser class, opens a new Firefox window and navigates to
        /// the specified uri.
        /// </summary>
        /// <param name="uri">uri to navigate to in the new Firefox window.</param>
        internal FirefoxBrowser(string uri, bool killExisting)
        {
            if (installedExePath == null)
        	{
        		throw new Exception(string.Format("Firefox is not installed. Expected location:{0}",expectedExePath));
        	}
        	if (installedVersion != supportedExeMajorVersion)
    		{
    			throw new Exception(string.Format("Firefox version {0} ({1}) is not supported by this class. Supported version: {2}.", installedVersion, installedExePath, supportedExeMajorVersion));
    		}        	
        	
        	firefoxRepo = new Firefox();

            //Kill existing browser processes before opening a new browser
            if (killExisting)
            {
                Host.Local.KillBrowser(BrowserProduct.Firefox.ToString());
            }

            //Open browser           
            OpenBrowser(uri, BrowserProduct.Firefox.ToString(), new RxPath("/form[@class='MozillaWindowClass']"));
            //Update repository instance base path to include native window handle attribute for the form
            firefoxRepo.Form.BasePath = new RxPath(String.Format("/form[@class='MozillaWindowClass' and @handle='{0}']", handle));
            Validate.Exists(firefoxRepo.Form.BasePath);
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
        /// Version (Major version) of firefox exe that this class supports.
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
                Validate.Exists(firefoxRepo.Form.NavigateEditBox);                
                return firefoxRepo.Form.NavigateEditBox.Text;
                
            }
        }
        
        /// <summary>
        /// Navigate to specified uri by typing it in the browser's navigate edit box and then pressing the {ENTER} key.
        /// </summary>
        /// <param name="uri">uri to navigate to.</param>
        internal void Navigate(string uri)
        {
            string navigateUri = ValidateUri(uri, true);
            //Click title bar of this window first so the navigate edit box is in the active window so it can be typed into
            ClickTitleBar();
            Validate.Exists(firefoxRepo.Form.NavigateEditBox);
            firefoxRepo.Form.NavigateEditBox.PressKeys(navigateUri);            
        }

        /// <summary>
        /// Click the title bar of this browser window.
        /// </summary>
        internal void ClickTitleBar()
        {
            Validate.Exists(firefoxRepo.Form.TitleBar);
            //Click using move time, otherwise a click too soon after a previous call to ClickTitleBar() acts like a double-click on the title bar
            //(which can change the window size).
            firefoxRepo.Form.TitleBar.Click(new Duration(250));
        }

        /// <summary>
        /// Half the size of this browser window.
        /// </summary>
        internal void HalfSize()
        {
            base.HalfSize(firefoxRepo.Form.Window);
        }

        /// <summary>
        /// Move this browser window.
        /// </summary>
        /// <param name="x">Adds this paramter to browser window's current screen location x-coordinate.</param>
        /// <param name="y">Adds this paramter to browser window's current screen location y-coordinate.</param>
        internal void Move(int x, int y)
        {
            base.Move(firefoxRepo.Form.Window, x, y);       
        }

        /// <summary>
        /// Included for API test stability purposes. Move this browser window down and up quickly four times.
        /// </summary>
        internal void Fun()
        {
            base.Fun(firefoxRepo.Form.Window);
        }

    }  
    
}