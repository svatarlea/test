using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using System.Collections;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using Lynda.Test.Browsers;
using Lynda.Test.ConsumerPages;
using Lynda.Test.Advanced.Utilities.WebPages;

namespace Tests.Demo
{
    /// <summary>
    /// Description of TestCase2.
    /// </summary>
    [TestModule("5487DE9F-33EE-41EF-9ED9-EC9705B4A5C7", ModuleType.UserCode, 1)]
    public class RegMultiBrowser : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public RegMultiBrowser()
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
            Mouse.DefaultMoveTime = 100;
            Keyboard.DefaultKeyPressTime = 40;
            Delay.SpeedFactor = 1.0;
            
            const string homePageURI = "integration.lynda.com/member.aspx";      

            Browser browserIE = new Browser(BrowserProduct.IE,homePageURI,true);
            browserIE.HalfSize();
            Browser browserFF = new Browser(BrowserProduct.Firefox,homePageURI,true);            
            browserFF.HalfSize();
            browserFF.Move(200,200);
            
            //Create new member home page instance which also waits for the page to load
            HomePageMember homePageMemberIE = new HomePageMember(browserIE);
            HomePageMember homePageMemberFF = new HomePageMember(browserFF);

            homePageMemberIE.ClickSubscribeLink();
            homePageMemberFF.ClickSubscribeLink();
            
            RegPageStep1 regPageStep1IE = new RegPageStep1(browserIE);
            RegPageStep1 regPageStep1FF = new RegPageStep1(browserFF);
                      
            regPageStep1IE.SelectOTLSubscription(OTLSubscriptionPlanRegPage1.SubscriptionPlan.MonthlyPremium);
            regPageStep1FF.SelectOTLSubscription(OTLSubscriptionPlanRegPage1.SubscriptionPlan.Annual);
            
            regPageStep1IE.FillAccountInfo(new AccountInfo(AccountInfo.DefaultInfo.Standard));
            AccountInfo accountInfo = new AccountInfo(AccountInfo.DefaultInfo.Standard);
            accountInfo.FirstName="John";
            regPageStep1FF.FillAccountInfo(accountInfo);
            
            regPageStep1IE.ClickContinue();
            regPageStep1FF.ClickContinue();

            RegPageStep2 regPageStep2IE = new RegPageStep2(browserIE,BillingInformation.DefaultInfo.Standard,
                                                           PaymentInformation.DefaultInfo.Standard);
            RegPageStep2 regPageStep2FF = new RegPageStep2(browserFF,BillingInformation.DefaultInfo.Standard,
                                                           PaymentInformation.DefaultInfo.Standard);
      
            regPageStep2IE.ClickIHaveRead();
            regPageStep2IE.ClickStartMembership();
            regPageStep2FF.ClickIHaveRead();
            regPageStep2FF.ClickStartMembership();

        }
    }
}
