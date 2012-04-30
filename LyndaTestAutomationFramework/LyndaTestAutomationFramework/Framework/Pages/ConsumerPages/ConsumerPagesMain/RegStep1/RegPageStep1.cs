using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegStep1;
using Lynda.Test.Browsers;

namespace Lynda.Test.ConsumerPages
{     
    /// <summary>
    /// Represents a Lynda Consumer Reg step 1 page https://www.lynda.com/home/Registration/ConsumerRegistrationStep1.aspx?bnr=topsubbtn_newsite
    /// </summary>
    public class RegPageStep1 : LyndaHeaderFooterPage2
    {
        private RegPageStep1Repo regPageStep1Repo = null;
        
        /// <summary>
        /// Initializes a new Lynda.Test.ConsumerPages.RegPageStep1 class. Does not select a subscription or enter any account information.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the consumer registration page step 1.</param>
        public RegPageStep1(Browser browserForPage)
        {
        	regPageStep1Repo = new RegPageStep1Repo();
        	browser = browserForPage;
        	browser.ClickTitleBar();
        	WaitForLoad();
        }
        
        /// <summary>
        /// Initializes a new Lynda.Test.ConsumerPages.RegPageStep1 class.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the consumer registration page step 1.</param>
        /// <param name="subscription">Subscription to select.</param>
        /// <param name="accountDefaultInfo">Type of account information to enter.</param>
        public RegPageStep1(Browser browserForPage, OTLSubscriptionPlan.SubscriptionPlan subscription, AccountInfo.DefaultInfo accountDefaultInfo) :
        	this (browserForPage, subscription, new AccountInfo(accountDefaultInfo))
        {	
        }
        
        /// <summary>
        /// Initializes a new Lynda.Test.ConsumerPages.RegPageStep1 class.
        /// </summary>
        /// <param name="browserForPage">Browser instance containing the consumer registration page step 1.</param>
        /// <param name="subscription">Subscription to select.</param>
        /// <param name="customAccountInfo">Custom account information to enter.</param>
        public RegPageStep1(Browser browserForPage, OTLSubscriptionPlan.SubscriptionPlan subscription, AccountInfo customAccountInfo) :
        	this (browserForPage)
        {
        	SelectOTLSubscription(subscription);
        	FillAccountInfo(customAccountInfo);
        }

        /// <summary>
        /// Enters Lynda.Test.ConsumerPages.AccountInfo.DefaultInfo.Standard account information into web page.
        /// </summary>
        public void FillAccountInfo()
        {
        	AccountInfo accountInfo = new AccountInfo(AccountInfo.DefaultInfo.Standard);
        	FillAccountInfo(accountInfo);
        }

        /// <summary>
        /// Enters custom account information into web page.
        /// </summary>
        /// <param name="accountInfo">Custom account information.</param>
        public void FillAccountInfo(AccountInfo accountInfo)
        {
        	browser.ClickTitleBar();
        	accountInfo.EnterAccountInfo();
        }

        /// <summary>
        /// Enters custom Lynda.Test.ConsumerPages.AccountInfo.Field data into web page field.
        /// </summary>
        /// <param name="formField">Field to enter data in.</param>
        /// <param name="fieldData">Data to enter.</param>
        public void FillAccountInfo(AccountInfo.Field formField, string fieldData)
        {
        	AccountInfo accountInfo = new AccountInfo();
        	browser.ClickTitleBar();
        	accountInfo.EnterAccountInfo(formField, fieldData);
        }

        /// <summary>
        /// Checks or unchecks monthly newsletters check box.
        /// </summary>
        /// <param name="signMeUpNewsletters">TRUE = Check the box, FALSE = Uncheck the box. If the checkbox is already in the
        /// requested state it will be left alone.</param>
        public void CheckOrUncheckMonthlyNewsletters(bool signMeUpNewsletters)
        {
        	AccountInfo accountInfo = new AccountInfo();
        	accountInfo.SignMeUpNewsletters = signMeUpNewsletters;      	
        	browser.ClickTitleBar();
        	accountInfo.CheckOrUncheckMonthlyNewsletters();
        }   
        
        public void CheckOrUncheckNewReleases(bool signMeUpNewReleases)
        {
        	AccountInfo accountInfo = new AccountInfo();
        	accountInfo.SignMeUpNewReleases = signMeUpNewReleases;      	
        	browser.ClickTitleBar();
        	accountInfo.CheckOrUncheckNewReleases();
        }
        
        public void CheckOrUncheckSpecialAnnouncements(bool signMeUpSpecial)
        {
        	AccountInfo accountInfo = new AccountInfo();
        	accountInfo.SignMeUpSpecial=signMeUpSpecial;    	
        	browser.ClickTitleBar();
        	accountInfo.CheckOrUncheckSpecial();
        }
        
        /// <summary>
        /// Gets account information from web page.
        /// </summary>
        /// <returns>Account information from web page.</returns>
        public AccountInfo GetAccountInfo()
        {
        	AccountInfo accountInfo = new AccountInfo();
        	browser.ClickTitleBar();
        	accountInfo.GetAccountInfo();
        	return accountInfo;
        }
        
        /// <summary>
        /// Selects an OTL subscription plan on the web page.
        /// </summary>
        /// <param name="subscriptionPlan">Lynda.Test.ConsumerPages.OTLSubscriptionPlan.SubscriptionPlan type to select.</param>
        public void SelectOTLSubscription(OTLSubscriptionPlan.SubscriptionPlan subscriptionPlan)
        {
        	OTLSubscriptionPlan otlSubscriptionPlan = new OTLSubscriptionPlan(subscriptionPlan);
        	browser.ClickTitleBar();
        	otlSubscriptionPlan.EnterOTLSubscriptionPlan();
        }
        
        /// <summary>
        /// Gets current OTL Subscription plan selected on web page.
        /// </summary>
        /// <returns>Subscription plan selected on web page currently.</returns>
        public OTLSubscriptionPlan.SubscriptionPlan GetOTLSubscriptionPlan()
        {
        	OTLSubscriptionPlan otlSubscriptionPlan = new OTLSubscriptionPlan();
        	browser.ClickTitleBar();
        	otlSubscriptionPlan.GetOTLSubscriptionPlan();
        	return otlSubscriptionPlan.Subscription;
        }
        
        /// <summary>
        /// Waits for page to load.
        /// </summary>
        public void WaitForLoad()
        { 	
        	browser.ClickTitleBar();
        	WaitForLoad(regPageStep1Repo.DOM.ContinueButtonInput);
        }
        
        /// <summary>
        /// Cicks Continue button.
        /// </summary>
        public void ClickContinue()
        {
        	browser.ClickTitleBar();
        	Validate.Exists(regPageStep1Repo.DOM.ContinueButtonInput);
        	regPageStep1Repo.DOM.ContinueButtonInput.Click();
        }
        
    }
    
}
