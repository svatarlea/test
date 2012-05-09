using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegConfirm;

namespace Lynda.Test.ConsumerPages
{   	
	/// <summary>
	/// Represents OTL Plan information section on confirmation page.
	/// </summary>
	public class OTLPlanInfo
    {       				
		private OTLPlanInfoRepo otlPlanInfoRepo = null;
		
		private string name;
        private string price;

        /// <summary>
        /// Initializes a new instance of the Lynda.Test.ConsumerPages.OTLPlanInfo class.
        /// </summary>
        public OTLPlanInfo()
        {   	
        	otlPlanInfoRepo = new OTLPlanInfoRepo();
        }        
   
        /// <summary>
        /// Gets Ranorex.Adpater values from the web page and stores in this class instance's fields.
        /// </summary>
        internal void GetOTLPlanInfo()
        {       				
        	Name=otlPlanInfoRepo.DOM.OTLPlanName.InnerText.Trim();
        	Price=otlPlanInfoRepo.DOM.OTLPlanPrice.InnerText.Trim();
        }      
             
        /// <summary>
        /// Gets or sets the OTL Plan name in this instance.
        /// </summary>
        public string Name
        {            
        	get { return name; }
        	set { name = value; }
        }
        
        /// <summary>
        /// Gets or sets the OTL Plan price in this instance.
        /// </summary>
        public string Price
        {
        	get {return price;}
        	set {price = value;}
        }
                   
	}

}
