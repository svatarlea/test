using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegStep2;

namespace Lynda.Test.ConsumerPages
{   	
	/// <summary>
	/// Represents OTL Subscription Plan info on consumer registration page 2.
	/// </summary>
	public class OTLSubscriptionPlanRegPage2
    {       		
		private OTLSubscriptionPlanRegPage2Repo otlSubscriptionPlanRegPage2Repo = null;
		
		private string planName;
        private string planPrice;

        /// <summary>
        /// Initializes a new instance of the Lynda.Test.ConsumerPages.OTLSubscriptionPlanRegPage2 class. 
        /// </summary>
        public OTLSubscriptionPlanRegPage2()
        {   	
        	otlSubscriptionPlanRegPage2Repo = new OTLSubscriptionPlanRegPage2Repo();
        }        
                
        /// <summary>
        /// Gets Ranorex.Adpater values from the web page and stores in this class instance's fields.
        /// </summary>
        internal void GetOTLPlanInfo()
        {       				
        	PlanName=otlSubscriptionPlanRegPage2Repo.DOM.PlanName.InnerText.Trim();
        	PlanPrice=otlSubscriptionPlanRegPage2Repo.DOM.PlanPrice.InnerText.Trim();
        }     

        /// <summary>
        /// Gets or sets the OTL Plan name in this instance.
        /// </summary>
        public string PlanName
        {            
        	get { return planName; }
            set {planName = value;}
        }
        
        /// <summary>
        /// Gets or sets the OTL Plan price in this instance.
        /// </summary>
        public string PlanPrice
        {
        	get {return planPrice;}
        	set {planPrice = value;}
        }               
	}


}
