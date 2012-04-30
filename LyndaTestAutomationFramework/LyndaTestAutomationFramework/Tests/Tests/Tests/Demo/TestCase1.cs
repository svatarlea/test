using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using Lynda.Test.Browsers;
using Lynda.Test.ConsumerPages;

namespace Tests.Demo
{
    /// <summary>
    /// Description of TestCase1.
    /// </summary>
    [TestModule("84E4B99C-F3FE-446B-962C-7F2EB5596954", ModuleType.UserCode, 1)]
    public class TestCase1 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TestCase1()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 50;
            Keyboard.DefaultKeyPressTime = 50;
            Delay.SpeedFactor = 1.0;      

            const string url = "http://release.lynda.com/member.aspx";

            Browser browser = new Browser(BrowserProduct.IE, url,true);

            HomePageMember homePageMember = new HomePageMember(browser);
            homePageMember.ClickSubscribeLink();
            
            RegPageStep1 regPageStep1 = new RegPageStep1(browser,OTLSubscriptionPlan.SubscriptionPlan.MonthlyPremium,
                                                         AccountInfo.DefaultInfo.Standard);
            
            regPageStep1.CheckOrUncheckMonthlyNewsletters(true);
            regPageStep1.CheckOrUncheckNewReleases(true);
            regPageStep1.CheckOrUncheckSpecialAnnouncements(true);
            regPageStep1.FillAccountInfo(); //standard info default.
            //custom
            AccountInfo accountInfo = new AccountInfo(); //nothing initialized
            accountInfo.SignMeUpNewReleases=true;
            accountInfo = new AccountInfo(AccountInfo.DefaultInfo.Standard); //standard account info
            //change standard values to custom
            accountInfo.FirstName="Trebek";
            accountInfo.SignMeUpSpecial=false;
            regPageStep1.FillAccountInfo(accountInfo);
            //write to one field only
            regPageStep1.FillAccountInfo(AccountInfo.Field.LastName, "Connery");
            regPageStep1.FillAccountInfo(AccountInfo.Field.FirstName, "Sean");
            //get current account info
            accountInfo = regPageStep1.GetAccountInfo();
            string firstName = accountInfo.Username;
            bool newReleasesChecked = accountInfo.SignMeUpNewReleases;
            //change subscription plan
            regPageStep1.SelectOTLSubscription(OTLSubscriptionPlan.SubscriptionPlan.Monthly);
            //get current plan
            OTLSubscriptionPlan.SubscriptionPlan currentPlan = regPageStep1.GetOTLSubscriptionPlan();

            regPageStep1.ClickContinue();
            
            RegPageStep2 regPageStep2 = new RegPageStep2(browser, BillingInformation.DefaultInfo.Standard,
                                                         PaymentInformation.DefaultInfo.Standard);
            
            regPageStep2.FillBillingInfo(); //standard info default
            regPageStep2.FillPaymentInfo(); //standard info default
            //custom
            BillingInformation billingInfo = new BillingInformation(); //nothing initialized
            billingInfo.HowHear="Radio";
            PaymentInformation paymentInfo = new PaymentInformation(); //nothing ititialized
            paymentInfo.ExpirationYear="2013";
            billingInfo = new BillingInformation(BillingInformation.DefaultInfo.Standard); //standard billing info
            paymentInfo = new PaymentInformation(PaymentInformation.DefaultInfo.Standard); //standard payment info
            //change standard values to custom
            billingInfo.Phone="5555551234";
            paymentInfo.ExpirationYear="2014";
            regPageStep2.FillBillingInfo(billingInfo);
            regPageStep2.FillPaymentInfo(paymentInfo);
            //write to one field only
            regPageStep2.FillBillingInfo(BillingInformation.Field.Apt, "1");
            regPageStep2.FillPaymentInfo(PaymentInformation.Field.ExpireYear, "2015");
            //get current billing and payment info
            billingInfo = regPageStep2.GetBillingInfo();
            paymentInfo = regPageStep2.GetPaymentInfo();
            string billingAddress = billingInfo.Address;
            string paymentNameOnCard = paymentInfo.NameOnCard;

            regPageStep2.ClickIHaveRead();
            regPageStep2.ClickStartMembership();
   
        }
    }
}
