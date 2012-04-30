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
    /// Represents a Chrome version "18.0.1025.162 m" browser.
    /// </summary>
    internal class ChromeBrowser : BrowserBasic
    {
        /// <summary>
        /// Chrome Ranorex repository instance for this Chrome browser window.
        /// </summary>
        internal Chrome chromeRepo = null;
     
        /// <summary>
        /// Initializes a new instance of the Lynda.Test.Browsers.ChromeBrowser class, opens a new Chrome window and navigates to
        /// the specified uri.
        /// </summary>
        /// <param name="uri">uri to navigate to in the new Chrome window.</param>
        internal ChromeBrowser(string uri, bool killExisting)
        {
            chromeRepo = new Chrome();

            //Kill existing browser processes before opening a new browser
            if (killExisting)
            {
                Host.Local.KillBrowser(BrowserProduct.Chrome.ToString());
            }

            //Open browser           
            OpenBrowser(uri, BrowserProduct.Chrome.ToString(), new RxPath("/form[@class='Chrome_WidgetWin_0']"));
            //Update repository instance base path to include native window handle attribute for the form
            chromeRepo.Form.BasePath = new RxPath(String.Format("/form[@class='Chrome_WidgetWin_0' and @handle='{0}']", handle));
            Validate.Exists(chromeRepo.Form.BasePath);
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
            chromeRepo.Form.NavigateEditBox.PressKeys(navigateUri);
        }

        /// <summary>
        /// Click the title bar of this browser window.
        /// </summary>
        internal void ClickTitleBar()
        {
            Validate.Exists(chromeRepo.Form.TitleBar);
            chromeRepo.Form.TitleBar.Click();
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