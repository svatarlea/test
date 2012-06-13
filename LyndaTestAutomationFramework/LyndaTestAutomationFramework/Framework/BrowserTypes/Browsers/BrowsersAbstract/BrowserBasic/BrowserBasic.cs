using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Lynda.Test.Browsers
{		
    /// <summary>
    /// Represents an abstract browser.
    /// </summary>
    public abstract class BrowserBasic
    {
        //Members applicable to all browsers such as IE, Firefox, Chrome and Safari.
        
        /// <summary>
        /// Handle of the native window for the browser instance.
        /// </summary>
        protected IntPtr handle;

		protected BrowserBasic()
        {
        }
			
        /// <summary>
        /// Opens a new browser window of the specified browserProduct with the specified default uri.
        /// Initializes the handle field of this class to the native window handle of the new window.
        /// </summary>
        /// <param name="url">The default uri for the opened browser.</param>
        /// <param name="browserProduct">The Lynda.Test.Browsers.Browser.BrowserProduct such as IE, Firefox, Chrome or Safari.</param>
        /// <param name="rxPathBrowserWindow">A Ranorex.RxPath instance used to find the window.</param>
        /// <example><code>OpenBrowser("www.lynda.com", Lynda.Test.Browsers.Browser.BrowserProduct.IE.ToString(), new RxPath("/form[@class='IEFrame']"));</code></example>
        protected void OpenBrowser(string uri, string browserProduct, RxPath rxPathBrowserWindow)
        {
        	if (!String.IsNullOrEmpty(uri))
        	{
        		ValidateUri(uri);
        	}
            
            //1. Find all browser windows before opening a new one
            Hashtable hashTableWindowsBefore = new Hashtable();
            IList<Ranorex.NativeWindow> browserWindowsBefore = null;
            browserWindowsBefore = Host.Local.Find<Ranorex.NativeWindow>(rxPathBrowserWindow);
            foreach (Ranorex.NativeWindow window in browserWindowsBefore)
            {
                hashTableWindowsBefore.Add(window.Handle, true);
            }
            
            //2. Open new window
            try
            {
                Host.Local.OpenBrowser(uri, browserProduct, "", false, true);
            }
            catch (RanorexException e)
            {
                //e.g. e.InnerException.Message=="Failed to run application 'safari'."
                if (e.InnerException.Message.StartsWith("Failed to run application '"))
                {
                    throw new Exception(String.Format("Failed to open browser {0}. It may not be installed.", browserProduct.ToString()), e);
                }
                else
                {
                    throw e;
                }
            }

            //3. Wait for new window to open: Keep searching for browser windows until the total found is greater than the total before a new one has opened,
            //because only doing one search after Host.Local.OpenBrowser doesn't immediately find the new window
            IList<Ranorex.NativeWindow> browserWindowsNow = null;
            while (browserWindowsNow == null || browserWindowsNow.Count <= browserWindowsBefore.Count)
            {
                browserWindowsNow = Host.Local.Find<Ranorex.NativeWindow>(rxPathBrowserWindow);
            }
            
            //4. Store the window handle of the new browser window; new window is the one whose handle is not in hashTableWindowsBefore
            bool foundNewWindow = false;
            foreach (Ranorex.NativeWindow window in browserWindowsNow)
            {
                if (!hashTableWindowsBefore.Contains(window.Handle))
                {
                    handle = window.Handle;
                    foundNewWindow = true;
                    break;
                }
            }
            if (!foundNewWindow)
            {
                StringBuilder sB = new StringBuilder("Handles of windows before opening new window: ");
                ICollection handlesBefore = hashTableWindowsBefore.Keys;

                foreach (IntPtr windowHandleBefore in handlesBefore)
                {
                    sB.Append(windowHandleBefore.ToString());
                    sB.Append(" ");
                }

                sB.Append("Handles of windows after opening new window: ");
                foreach (Ranorex.NativeWindow window in browserWindowsNow)
                {
                    sB.Append(window.Handle);
                    sB.Append(" ");
                }

                throw new Exception(String.Format("Failed to find handle of the new browser window. {0}.", sB));
            }
        }

        /// <summary>
        /// Validates the uri is valid, throws a System.ArgumentException if it isn't.
        /// </summary>
        /// <param name="uri">uri to validate.</param>
        protected void ValidateUri(string uri)
        {
            ValidateUri(uri, false);
        }

        /// <summary>
        /// Validates the uri is valid, throws a System.ArgumentException if it isn't.
        /// </summary>
        /// <param name="uri">uri to validate.</param>
        /// <param name="processUriForNavigate">If TRUE, appends "{ENTER}" to the uri after validating it for use by Ranorex.Adapter.PressKeys().</param>
        /// <returns>null if processUriForNavigate is FALSE, otherwise the validated uri to navigate to appended with "{ENTER}" for use by Ranorex.Adapter.PressKeys().</returns>
        protected string ValidateUri(string uri, bool processUriForNavigate)
		{
			if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            if (uri == String.Empty)
            {
                throw new ArgumentException(String.Format("uri cannot be empty"), "uri");                
            }

            UriBuilder uriBuilder = null;
            try
            {
                uriBuilder = new UriBuilder(uri);
            }
            catch (UriFormatException e)
            {
                throw new ArgumentException(String.Format("Not a valid URI:{0}", uri), "uri", e);
            }

            string navigateUri = null;
            if (processUriForNavigate)
            {
                navigateUri = string.Format("{0}{1}", uriBuilder.Uri.AbsoluteUri, "{ENTER}");
            }
            
            return navigateUri;
		}

        /// <summary>
        /// Half the size of a Ranorex.Form such as a browser window.
        /// </summary>
        /// <param name="form">Ranorex.Form to half the size of.</param>
        protected void HalfSize(Form form)
        {
            int currentHeight = form.Element.Size.Height;
            int currentWidth = form.Element.Size.Width;

            form.Resize(currentWidth / 2, currentHeight / 2);
        }

        /// <summary>
        /// Move a Ranorex.Form such as a browser window.
        /// </summary>
        /// <param name="form">The Ranorex.Form to move</param>
        /// <param name="x">Adds this paramter to the Ranorex.Form current screen location x-coordinate.</param>
        /// <param name="y">Adds this paramter to the Ranorex.Form current screen location y-coordinate.</param>
        protected void Move(Form form, int x, int y)
        {
            int currentXCor = form.Element.ScreenLocation.X;
            int currentYCor = form.Element.ScreenLocation.Y;

            form.Move(currentXCor + x, currentYCor + y);
        }

        /// <summary>
        /// Included for API test stability purposes. Moves a Ranorex.Form such as a browser window down and up quickly four times.
        /// </summary>
        /// <param name="form">The Ranorex.Form to move.</param>
        protected void Fun(Form form)
        {
            int currentXCor = form.Element.ScreenLocation.X;
            int currentYCor = form.Element.ScreenLocation.Y;

            for (int l = 1; l <= 4; l++)
            {
                for (int i = 1; i <= 100; i++)
                {
                    form.Move(currentXCor + i, currentYCor + i);
                    Ranorex.Delay.Milliseconds(1);
                }

                currentXCor = form.Element.ScreenLocation.X;
                currentYCor = form.Element.ScreenLocation.Y;

                for (int i = 1; i <= 100; i++)
                {
                    form.Move(currentXCor - i, currentYCor - i);
                    Ranorex.Delay.Milliseconds(1);
                }

                currentXCor = form.Element.ScreenLocation.X;
                currentYCor = form.Element.ScreenLocation.Y;
            }

        }
        

	        
    }

}
