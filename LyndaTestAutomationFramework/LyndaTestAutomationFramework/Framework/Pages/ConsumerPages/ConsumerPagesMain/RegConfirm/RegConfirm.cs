using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegConfirm;
using Lynda.Test.Browsers;

namespace Lynda.Test.ConsumerPages
{   		
	/// <summary>
    /// Lynda Consumer registration confirmation page,
    ///  /home/registration/ConsumerRegistrationConfirm.aspx?bnr=topsubbtn_newsite
    /// </summary>
    public class RegConfirm : LyndaHeaderFooterPage3
    {  		
        private RegConfirmRepo regConfirmRepo = null;
        
        /// <summary>
        /// Initializes a new Lynda.Test.ConsumerPages.RegConfirm class.
        /// </summary>
        /// <param name="browserForPage"></param>
        public RegConfirm(Browser browserForPage)
        {     
        	regConfirmRepo = new RegConfirmRepo();
        	browser = browserForPage;
        	browser.ClickTitleBar();
        	WaitForLoad();
        }
        
        /// <summary>
        /// Gets OTL Plan information from web page.
        /// </summary>
        /// <returns>OTL Plan information from web page.</returns>
        public OTLPlanInfo GetOTLPlanInfo()
        {
        	OTLPlanInfo otlPlanInfo = new OTLPlanInfo();
        	browser.ClickTitleBar();
        	otlPlanInfo.GetOTLPlanInfo();
        	return otlPlanInfo;
        }

        /// <summary>
        /// Waits for page to load.
        /// </summary>
        public void WaitForLoad()
        {
        	browser.ClickTitleBar();
        	WaitForLoad(regConfirmRepo.DOM.PrintReceiptSpan);
        }
      
    }
}
