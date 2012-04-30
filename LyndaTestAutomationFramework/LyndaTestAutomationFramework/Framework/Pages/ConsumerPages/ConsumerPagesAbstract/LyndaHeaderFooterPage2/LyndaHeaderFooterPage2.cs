using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesAbstract.LyndaHeaderFooterPage2;
using Lynda.Test.AbstractGeneral;
using Lynda.Test.Browsers;

namespace Lynda.Test.ConsumerPages
{	    
	/// <summary>
    /// Represents a lynda page with header and footer (e.g. as seen on consumer reg page 1).
    /// </summary>
    public abstract class LyndaHeaderFooterPage2 : AbstractPage1
    {
        private HeaderRepo headerRepo = null;
    	
        /// <summary>
        /// Browser instance that has a page loaded that is derived from the LyndaHeaderFooterPage2 class. 
        /// </summary>
        private Browser browser = null;
        
        /// <summary>
        /// Initializes a new instance of LyndaHeaderFooterPage2 class.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the page derived from the LyndaHeaderFooterPage2 class.</param>
        protected LyndaHeaderFooterPage2(Browser browserForPage)
        {
        	headerRepo = new HeaderRepo();
        	browser = browserForPage;
        }
        
        /// <summary>
        /// Clicks lynda.com image in footer.
        /// </summary>
        public void ClickLyndaImage()
        {
        	browser.ClickTitleBar();
        	Validate.Exists(headerRepo.DOM.LyndaImage);
        	headerRepo.DOM.LyndaImage.Click();
        }


    }

}
