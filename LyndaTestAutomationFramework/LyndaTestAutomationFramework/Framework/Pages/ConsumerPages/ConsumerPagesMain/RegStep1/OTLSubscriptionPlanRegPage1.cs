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
	public class OTLSubscriptionPlanRegPage1
    {    	
		/// <summary>
		/// Monthly = Monthly Subscription.
		/// MonthlyPremium = Monthly Premium Subscription.
		/// Annual = Annual Subscription.
		/// AnnualPremium = Annual Premium Subscription.
		/// </summary>
    	public enum SubscriptionPlan { Monthly, MonthlyPremium, Annual, AnnualPremium };
    	
    	private OTLSubscriptionPlanRegPage1Repo otlSubscriptionPlanRepo = null;
    	
    	private SubscriptionPlan subscription;	
    	private string subscriptionPrice;

    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.OTLSubscriptionPlan class without initializing the subscription choice.
    	/// </summary>
    	internal OTLSubscriptionPlanRegPage1()
    	{
    		otlSubscriptionPlanRepo = new OTLSubscriptionPlanRegPage1Repo();
    	}
    	
    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.OTLSubscriptionPlan class.
    	/// </summary>
    	/// <param name="subscriptionPlan">Specifies how to initialize the subscription plan.</param>
    	internal OTLSubscriptionPlanRegPage1(SubscriptionPlan subscriptionPlan) :
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
	    		SubscriptionPrice=otlSubscriptionPlanRepo.DOM.MonthlyPrice.InnerText.Trim();
	    		return;
	    	}
	    	if (Convert.ToBoolean(otlSubscriptionPlanRepo.DOM.RadioMonthlyPremiumInput.Checked))
	    	{
	    		Subscription = SubscriptionPlan.MonthlyPremium;
	    		SubscriptionPrice=otlSubscriptionPlanRepo.DOM.MonthlyPremiumPrice.InnerText.Trim();
	    		return;
	    	}
	    	if (Convert.ToBoolean(otlSubscriptionPlanRepo.DOM.RadioAnnualInput.Checked))
	    	{
	    		Subscription = SubscriptionPlan.Annual;
	    		SubscriptionPrice=otlSubscriptionPlanRepo.DOM.AnnualPrice.InnerText.Trim();
	    		return;
	    	}
	    	if (Convert.ToBoolean(otlSubscriptionPlanRepo.DOM.RadioAnnualPremiumInput.Checked))
	    	{
	    		Subscription = SubscriptionPlan.AnnualPremium;
	    		SubscriptionPrice=otlSubscriptionPlanRepo.DOM.AnuualPremiumPrice.InnerText.Trim();
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
	    /// Gets or sets the subscription plan radio button choice in this instance.
	    /// </summary>
    	public SubscriptionPlan Subscription
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
    	
    	/// <summary>
    	/// Gets or sets the subscription price for the chosen subscription in this instance.
    	/// </summary>
    	public string SubscriptionPrice
    	{
    		get{return subscriptionPrice;}
    		set{subscriptionPrice=value;}
    	}
	    
	   
    }

}
