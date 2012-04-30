using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegStep1;

namespace Lynda.Test.ConsumerPages
{      	
	/// <summary>
	/// Represents Online Training Library Subscription Plan section on consumer registration page 1.
	/// </summary>
	public class OTLSubscriptionPlan
    {    	
		/// <summary>
		/// Monthly = Monthly Subscription.
		/// MonthlyPremium = Monthly Premium Subscription.
		/// Annual = Annual Subscription.
		/// AnnualPremium = Annual Premium Subscription.
		/// </summary>
    	public enum SubscriptionPlan { Monthly, MonthlyPremium, Annual, AnnualPremium };
    	
    	private OTLSubscriptionPlanRepo otlSubscriptionPlanRepo = null;
    	
    	private SubscriptionPlan subscription;	

    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.OTLSubscriptionPlan class without initializing the subscription choice.
    	/// </summary>
    	internal OTLSubscriptionPlan()
    	{
    		otlSubscriptionPlanRepo = new OTLSubscriptionPlanRepo();
    	}
    	
    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.OTLSubscriptionPlan class.
    	/// </summary>
    	/// <param name="subscriptionPlan">Specifies how to initialize the subscription plan.</param>
    	internal OTLSubscriptionPlan(SubscriptionPlan subscriptionPlan) :
    		this ()
	    {	    		    	
	    	Subscription = subscriptionPlan;
	    }

    	/// <summary>
    	/// Gets Ranorex.Adpater value from the web page and store in this class instance's field.
    	/// </summary>
    	/// <returns></returns>
    	internal void GetOTLSubscriptionPlan()
	    {
	    	if (Convert.ToBoolean(otlSubscriptionPlanRepo.DOM.RadioMonthlyInput.Checked))
	    	{
	    		Subscription = SubscriptionPlan.Monthly;
	    		return;
	    	}
	    	if (Convert.ToBoolean(otlSubscriptionPlanRepo.DOM.RadioMonthlyPremiumInput.Checked))
	    	{
	    		Subscription = SubscriptionPlan.MonthlyPremium;
	    		return;
	    	}
	    	if (Convert.ToBoolean(otlSubscriptionPlanRepo.DOM.RadioAnnualInput.Checked))
	    	{
	    		Subscription = SubscriptionPlan.Annual;
	    		return;
	    	}
	    	if (Convert.ToBoolean(otlSubscriptionPlanRepo.DOM.RadioAnnualPremiumInput.Checked))
	    	{
	    		Subscription = SubscriptionPlan.AnnualPremium;
	    		return;
	    	}   	
	    }
    	
	    /// <summary>
        /// Enters field value from this OTLSubscriptionPlan instance into the corresponding Ranorex.Adapter on the web page.
        /// </summary>
    	internal void EnterOTLSubscriptionPlan()
	    {
	    	switch (subscription)
	    	{
	    		case SubscriptionPlan.Monthly:
	    			{
	    				otlSubscriptionPlanRepo.DOM.RadioMonthlyInput.Click();
	    				break;
	    			}
	    		case SubscriptionPlan.MonthlyPremium:
	    			{
	    				otlSubscriptionPlanRepo.DOM.RadioMonthlyPremiumInput.Click();
	    				break;
	    			}
	    		case SubscriptionPlan.Annual:
	    			{
	    				otlSubscriptionPlanRepo.DOM.RadioAnnualInput.Click();
	    				break;
	    			}
	    		case SubscriptionPlan.AnnualPremium:
	    			{
	    				otlSubscriptionPlanRepo.DOM.RadioAnnualPremiumInput.Click();
	    				break;
	    			}
	    		default:
	    			throw new Exception(String.Format("Code not implemented yet: {0}", subscription.ToString()));
	    	}
	    }

	    /// <summary>
	    /// Gets or sets the state of the subscription plan radio button choice in this instance.
	    /// </summary>
    	internal SubscriptionPlan Subscription
	    {
	    	get
	    	{
	    		return subscription;
	    	}
	    	set
	    	{
	    		if (value < SubscriptionPlan.Monthly || value > SubscriptionPlan.AnnualPremium)
        		{
        			throw new ArgumentOutOfRangeException("value", value,
					  "Must be one of the following: Monthly, MonthlyPremium, Annual, AnnualPremium");
           		};
	    		subscription = value;
	    	}
	    }	
	    
	   
    }

}
