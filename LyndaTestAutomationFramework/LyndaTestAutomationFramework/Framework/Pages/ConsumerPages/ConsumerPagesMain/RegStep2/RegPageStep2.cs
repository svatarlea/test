using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegStep2;
using Lynda.Test.Browsers;

namespace Lynda.Test.ConsumerPages
{   		
	/// <summary>
    /// Lynda Consumer Reg step 2 page https://stage.lynda.com/home/registration/ConsumerRegistrationStep2.aspx?bnr=topsubbtn_newsite
    /// </summary>
    public class RegPageStep2 : LyndaHeaderFooterPage2
    {
		Browser browser;      
		
        private RegPageStep2Repo regPageStep2Repo = null;
        
        /// <summary>
        /// Initializes a new Lynda.Test.ConsumerPages.RegPageStep2 class. Does not enter any billing or payment information.
        /// </summary>
        /// <param name="browserForPage"></param>
        public RegPageStep2(Browser browserForPage) : base (browserForPage)
        {     
        	regPageStep2Repo = new RegPageStep2Repo();
        	browser = browserForPage;
        	browser.ClickTitleBar();
        	WaitForLoad();
        }   
        
        /// <summary>
        /// Initializes a new Lynda.Test.ConsumerPages.RegPageStep2 class.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the consumer registration page step 2.</param>
        /// <param name="billingDefaultInfo">Type of billing information to enter.</param>
        /// <param name="paymentDefaultInfo">Type of payment information to enter.</param>
        public RegPageStep2(Browser browserForPage, BillingInformation.DefaultInfo billingDefaultInfo,
                            PaymentInformation.DefaultInfo paymentDefaultInfo) :
        	this (browserForPage, new BillingInformation(billingDefaultInfo), new PaymentInformation(paymentDefaultInfo))
        {      	
        }
              
        /// <summary>
        /// Initializes a new Lynda.Test.ConsumerPages.RegPageStep2 class.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the consumer registration page step 2.</param>
        /// <param name="customBillingInfo">Custom billing information to enter.</param>
        /// <param name="customPaymentInfo">Custom payment information to enter.</param>
        public RegPageStep2(Browser browserForPage, BillingInformation customBillingInfo, PaymentInformation customPaymentInfo) :
        	this (browserForPage)
        {
        	FillBillingInfo(customBillingInfo);
        	FillPaymentInfo(customPaymentInfo);
        }
        
        /// <summary>
        /// Enters Lynda.Test.ConsumerPages.BillingInformation.DefaultInfo.Standard billing information into web page.
        /// </summary>
        public void FillBillingInfo()
        {
        	BillingInformation billingInfo = new BillingInformation(BillingInformation.DefaultInfo.Standard);
        	FillBillingInfo(billingInfo);
        }
        
        /// <summary>
        /// Enters custom billing information into web page.
        /// </summary>
        /// <param name="billingInfo">Custom billing information.</param>
        public void FillBillingInfo(BillingInformation billingInfo)
        {
        	browser.ClickTitleBar();
        	billingInfo.EnterBillingInfo();
        }
        
        public void FillBillingInfo(BillingInformation.Field formField, string fieldData)
        {
        	BillingInformation billingInfo = new BillingInformation();
        	browser.ClickTitleBar();
        	billingInfo.EnterBillingInfo(formField, fieldData);
        }
                
        /// <summary>
        /// Gets billing information from web page.
        /// </summary>
        /// <returns>Billing information from web page.</returns>
        public BillingInformation GetBillingInfo()
        {
        	BillingInformation billingInfo = new BillingInformation();
        	browser.ClickTitleBar();
        	billingInfo.GetBillingInfo();
        	return billingInfo;
        }
        
        /// <summary>
        /// Enters Lynda.Test.ConsumerPages.PaymentInformation.DefaultInfo.Standard payment information into web page.
        /// </summary>
        public void FillPaymentInfo()
        {
        	PaymentInformation paymentInfo = new PaymentInformation(PaymentInformation.DefaultInfo.Standard);
        	FillPaymentInfo(paymentInfo);
        }
        
        /// <summary>
        /// Enters custom payment information into web page.
        /// </summary>
        /// <param name="paymentInfo">Custom payment information.</param>
        public void FillPaymentInfo(PaymentInformation paymentInfo)
        {
        	browser.ClickTitleBar();
        	paymentInfo.EnterPaymentInfo();
        }
        
        /// <summary>
        /// Enters custom Lynda.Test.ConsumerPages.PaymentInformation.Field data into web page field.
        /// </summary>
        /// <param name="formField">Field to enter data in.</param>
        /// <param name="fieldData">Data to enter.</param>
        public void FillPaymentInfo(PaymentInformation.Field formField, string fieldData)
        {
        	PaymentInformation paymentInfo = new PaymentInformation();
        	browser.ClickTitleBar();
        	paymentInfo.EnterPaymentInfo(formField,fieldData);
        }
        
        /// <summary>
        /// Gets payment information from web page.
        /// </summary>
        /// <returns>Payment information from web page.</returns>
        public PaymentInformation GetPaymentInfo()
        {
        	PaymentInformation paymentInfo = new PaymentInformation();
        	browser.ClickTitleBar();
        	paymentInfo.GetPaymentInfo();
        	return paymentInfo;
        }
     
        /// <summary>
        /// Waits for page to load.
        /// </summary>
        public void WaitForLoad()
        {
        	browser.ClickTitleBar();
        	WaitForLoad(regPageStep2Repo.DOM.StartMembershipInput);
        }
               
        /// <summary>
        /// Clicks "I have read and agree to the terms and conditions" check box.
        /// </summary>
        public void ClickIHaveRead()
        {
        	browser.ClickTitleBar();
        	Validate.Exists(regPageStep2Repo.DOM.IHaveReadInput);
        	regPageStep2Repo.DOM.IHaveReadInput.Click();
        }
        
        /// <summary>
        /// Clicks Start Membership button.
        /// </summary>
        public void ClickStartMembership()
        {
        	browser.ClickTitleBar();
        	Validate.Exists(regPageStep2Repo.DOM.StartMembershipInput);
        	regPageStep2Repo.DOM.StartMembershipInput.Click();
        }
      
    }
}
