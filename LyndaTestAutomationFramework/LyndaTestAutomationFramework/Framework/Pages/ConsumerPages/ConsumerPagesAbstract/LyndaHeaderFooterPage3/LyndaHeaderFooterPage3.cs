using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesAbstract.LyndaHeaderFooterPage3;
using Lynda.Test.AbstractGeneral;
using Lynda.Test.Browsers;

namespace Lynda.Test.ConsumerPages
{	    
	/// <summary>
    /// Represents a lynda page with header and footer (e.g. as seen on consumer registration confirmation page).
    /// </summary>
    public abstract class LyndaHeaderFooterPage3 : AbstractPage1
    {
        private HeaderRepo headerRepo = null;
    	
        /// <summary>
        /// Browser instance that has a page loaded that is derived from the LyndaHeaderFooterPage3 class. 
        /// </summary>
        protected Browser browser = null;
        
        /// <summary>
        /// Initializes a new instance of LyndaHeaderFooterPage3 class.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the page derived from the LyndaHeaderFooterPage3 class.</param>
        protected LyndaHeaderFooterPage3()
        {
        	headerRepo = new HeaderRepo();
        }
        
        /// <summary>
        /// Clicks Logout link in header.
        /// </summary>
        public void ClickLogoutLink()
        {
        	browser.ClickTitleBar();
        	Validate.Exists(headerRepo.DOM.LogOut);
        	headerRepo.DOM.LogOut.Click();
        }


    }

}
