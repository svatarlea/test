/*
 * Created by Ranorex
 * User: mperry
 * Date: 5/8/2012
 * Time: 10:54 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
using Lynda.Test.Advanced.Utilities.WebPages;

namespace Tests.Tests.Demo
{
    /// <summary>
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("12652A8A-67D3-40D0-848D-FB92775B7863", ModuleType.UserCode, 1)]
    public class AllRegistrations : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AllRegistrations()
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
            const string testCaseName = "AllRegistrations";
            string subscriptionPlanRow = TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["SubscriptionPlan"];
            string regPage2PlanNameExpected = TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["regPage2PlanNameExpected"];
            string confirmPagePlanNameExpected = TestSuite.Current.GetTestCase(testCaseName).DataContext.CurrentRow["confirmPagePlanNameExpected"];

            OTLSubscriptionPlanRegPage1.SubscriptionPlan subscriptionPlanToSelect = (OTLSubscriptionPlanRegPage1.SubscriptionPlan)
            	Enum.Parse(typeof(OTLSubscriptionPlanRegPage1.SubscriptionPlan),subscriptionPlanRow,false);

        	Mouse.DefaultMoveTime = 100;
            Keyboard.DefaultKeyPressTime = 40;
            Delay.SpeedFactor = 1.0;

            const string url = "http://stage.lynda.com/member.aspx";

            Browser browser = new Browser(BrowserProduct.IE, url,true);

            HomePageMember homePageMember = new HomePageMember(browser);
            homePageMember.ClickSubscribeLink();
            
            RegPageStep1 regPageStep1 = new RegPageStep1(browser,subscriptionPlanToSelect,
                                                         AccountInfo.DefaultInfo.Standard);
            
            OTLSubscriptionPlanRegPage1 otlPlan = regPageStep1.GetOTLSubscriptionPlan();
            //e.g. Monthly, MonthlyPremium, Annual, AnnualPremium
            OTLSubscriptionPlanRegPage1.SubscriptionPlan regPage1PlanSelected = otlPlan.Subscription;
            //e.g. "$37.50/month" "$250.00/year" "$375.00/year" "$25.00/month"
            string regPage1PlanPrice = otlPlan.SubscriptionPrice;
                        
            regPageStep1.ClickContinue();
            
            RegPageStep2 regPageStep2 = new RegPageStep2(browser, BillingInformation.DefaultInfo.Standard,
                                                         PaymentInformation.DefaultInfo.Standard);          
            
            OTLSubscriptionPlanRegPage2 otlPlanPage2 = regPageStep2.GetOTLSubscriptionPlan();
            //e.g. "Monthly Premium Subscription" "Monthly Subscription" "Annual Subscription" "Annual Premium Subscription"
            string regPage2PlanName = otlPlanPage2.PlanName;
            //e.g. "$37.50/month" "$25.00/month" "$250.00/year" "$375.00/year"
            string regPage2PlanPrice = otlPlanPage2.PlanPrice; 

            regPageStep2.ClickIHaveRead();
            regPageStep2.ClickStartMembership();
            
            RegConfirm regPageConfirm = new RegConfirm(browser);
            OTLPlanInfo otlPlanInfo = regPageConfirm.GetOTLPlanInfo();
            string confirmPagePlanName = otlPlanInfo.Name; //e.g. "Monthly Premium" "Monthly" "Annual" "Annual Premium"
            string confirmPagePlanPrice = otlPlanInfo.Price; //e.g. "$37.50" "$25.00" "$250.00" "$375.00"
                                 
            regPageConfirm.ClickLogoutLink();
            
            homePageMember.WaitForLoad();

            //Verify displayed subscription info on reg page 2 and confirm page matches what was selected on reg page 1.
            Validate.AreEqual(string.Compare(regPage2PlanName,regPage2PlanNameExpected,false),0,
                              "Actual:{0} Expected:{1}",true);
            Validate.AreEqual(string.Compare(confirmPagePlanName,confirmPagePlanNameExpected,false),0,
            			      "Actual:{0} Expected:{1}",true);
            Validate.AreEqual(string.Compare(regPage1PlanPrice,regPage2PlanPrice,false),0,
            			      "Actual:{0} Expected:{1}",true);
            Validate.AreEqual(string.Compare(confirmPagePlanPrice,regPage2PlanPrice.Substring(0,confirmPagePlanPrice.Length),false),0,
            			      "Actual:{0} Expected:{1}",true);
        }
    }
}
