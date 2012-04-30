using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using Lynda.Test.AbstractGeneral;
using ConsumerPagesAbstract.LyndaHeaderFooterPage1;
using Lynda.Test.Browsers;

namespace Lynda.Test.ConsumerPages
{	
	/// <summary>
    /// Header support menu items.
    /// Faqs = Frequently Asked Questions.
    /// ContactUs = Contact us.
    /// SysReqs = System requirements.
    /// RegKey = Register activation key.
    /// </summary>
    public enum SupportMenuItem { Faqs, ContactUs, SysReqs, RegKey };
    
    /// <summary>
    /// Represents a lynda page with header and footer (e.g. as seen on home consumer page)
    /// </summary>
    public abstract class LyndaHeaderFooterPage1 : AbstractPage1
    {
        private HeaderRepo headerRepo = null;
    	
        /// <summary>
        /// Browser instance that has a page loaded that is derived from the LyndaHeaderFooterPage1 class. 
        /// </summary>
        private Browser browser = null;
        
        /// <summary>
        /// Initializes new instance of LyndaHeaderFooterPage1 class.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the page derived from the LyndaHeaderFooterPage1 class.</param>
        protected LyndaHeaderFooterPage1(Browser browserForPage)
        {
        	headerRepo = new HeaderRepo();
        	browser=browserForPage;
        }

        /// <summary>
        /// Clicks Subscribe link in header.
        /// </summary>
        public void ClickSubscribeLink()
        {
        	browser.ClickTitleBar();
        	Validate.Exists(headerRepo.DOMbase.SubscribeLink);
        	headerRepo.DOMbase.SubscribeLink.Click();
        }

        /// <summary>
        /// Clicks a support menu item in the header.
        /// </summary>
        /// <param name="item">Menu item.</param>
        public void ClickSupportMenu(SupportMenuItem item)
        {            
            if (item < SupportMenuItem.Faqs || item > SupportMenuItem.RegKey)
            {
                throw new ArgumentOutOfRangeException();
            };
            browser.ClickTitleBar();
            Validate.Exists(headerRepo.DOMbase.SupportMenuInfo);
            headerRepo.DOMbase.SupportMenu.MoveTo();
            switch (item)
            {
            	case SupportMenuItem.Faqs:
        		{
       				Validate.Exists(headerRepo.DOMbase.SupportMenu_FAQsInfo);
        			headerRepo.DOMbase.SupportMenu_FAQs.Click();
        			break;
        		}
            	default:
            		throw new ArgumentException("Code not implemented yet", item.ToString());
            }          
        }
    }

}
