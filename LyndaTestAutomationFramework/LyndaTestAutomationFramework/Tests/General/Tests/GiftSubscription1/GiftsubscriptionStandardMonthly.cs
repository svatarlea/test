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

using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;

using General.Utilities.Forms;

namespace Tests.General.Tests.GiftSubscription
{
    /// <summary>
    /// Description of GiftsubscriptionStandardMonthly.
    /// </summary>
    [TestModule("F4E76563-E086-4E31-BD25-3DD6B2332F15", ModuleType.UserCode, 1)]
    public class GiftsubscriptionStandardMonthly : ITestModule
    {
    	private static GiftSubscriptionRepo repo = GiftSubscriptionRepo.Instance;
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public GiftsubscriptionStandardMonthly()
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
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
           
            const string domain = "release.lynda.com";
            const string navigateTo = "/";
            
            const BrowserProduct browserProduct = BrowserProduct.Firefox;

            //Open browser and navigate to url
            string url = string.Format("http://{0}{1}", domain, navigateTo.ToString());
            Browser browser = new Browser(browserProduct, url);
            
            //Wait for page to load
            Validate.Exists(repo.GiftSub.GiftSubLink);
            
            //Click Subscribe and wait for regpagestep1 to load
           	repo.GiftSub.GiftSubLink.Click();
           	
           	//Enter "1" into the text box labeled qty for the standard monthly subscription
           	repo.qty.PressKeys("1");
           	
           	//Press the continue button to advance to the next page
           	repo.GiftSub.continuebtn.Click();
           	
           	//Verify Gift Subscription Page 2 loads
           	Validate.Exists(repo.GiftSub.continuebtn2);
           	
           	//Enter the first name
           	repo.GiftSub.GiftSub2.FirstName.PressKeys("Mark"); 
           	
           	//Enter the last name
           	repo.GiftSub.GiftSub2.LastName.PressKeys("Test"); 
           	
           	//Generate a unique email address
           	string username,email;
           	FormDataAccount.GenerateUsernameEmail(out username, out email);

           	//Enter the email
           	repo.GiftSub.GiftSub2.Email1.PressKeys(email);
           	repo.GiftSub.GiftSub2.Email2.PressKeys(email);
           	
           	//Enter a greeting message
           	repo.GiftSub.GiftSub2.Greeting.PressKeys("Please enjoy this gift of Lynda.com");
           	
           	//Leave the delivery date blank 
           	
           	//Click the continue button 
           	repo.GiftSub.continuebtn2.Click();
           	
           	//Verify Gift Subscription page 3 loads 
           	Validate.Exists(repo.GiftSub.GiftSub3CreateAct.continuebtn3);
           	
           	//***Create a New Account*** Path
           	
           	//Enter the first name
           	repo.GiftSub.GiftSub3.FirstName1.PressKeys("Mark");
           	
           	//Enter the last name
           	repo.GiftSub.GiftSub3.LastName1.PressKeys("Test");
           	
           	//Generate a unique email address
           	string username1,email1;
           	FormDataAccount.GenerateUsernameEmail(out username1, out email1);
           	
           	//Enter the email address
           	repo.GiftSub.GiftSub3.Email1.PressKeys(email1);
           	
           	//Enter the username 
           	repo.GiftSub.GiftSub3.Username1.PressKeys(username1);
           	
           	//Enter a password
           	repo.GiftSub.GiftSub3CreateAct.Password.PressKeys("lynda1");
           	
            //Confirm entered password
           	repo.GiftSub.GiftSub3CreateAct.Password2.PressKeys("lynda1");
           	
           	//Click the continue button
           	repo.GiftSub.GiftSub3CreateAct.continuebtn3.Click();
           	 
           	//Select the country from the dropdown
           	
           	//Enter First Name
           	//repo.GiftSub.BillingInfo.FirstName2.PressKeys("Test");
           	
           	//Enter Last Name
           	//repo.GiftSub.BillingInfo.LastName2.PressKeys("Test");
           	
           	//Enter Company Name
           	repo.GiftSub.BillingInfo.OrgName.PressKeys("Lynda.com");
           	
           	//Enter Address 
           	repo.GiftSub.BillingInfo.Address.PressKeys("6410 Via Real");
           	
           	//Enter Apt
           	repo.GiftSub.BillingInfo.Apt.PressKeys("1");
           	
           	//Enter city
           	repo.GiftSub.BillingInfo.City.PressKeys("Carpinteria");
           	
           	//Generate Billing Info
           	string companyName, address, aptSuite, city, state, zip, country, phone;
           	FormDataBilling.GenerateAddress(out companyName, out address, out aptSuite, out city,
           	                                out state, out zip, out country, out phone);
           	
           	//Generate Payment Info
           	string paymentType, cardType, cardNumber, nameOnCard, cardSecurityCode, expireMonth, expireYear;
           	FormDataPayment.GenerateCreditCard(out paymentType, out cardType, out cardNumber, out nameOnCard,
           	                                   out cardSecurityCode, out expireMonth, out expireYear);
           	
           	//Enter state
           	SelectTagUI.ChooseSelectTagOption(repo.GiftSub.BasePath.ToString(), repo.GiftSub.BillingInfo.State, state);
           	
           	//Enter Zip
           	repo.GiftSub.BillingInfo.Zip.PressKeys("93013");
           	
           	//Enter phone
           	repo.GiftSub.PaymentInfo.Phone.PressKeys("805-405-4535");

           	//Enter credit card type
           	SelectTagUI.ChooseSelectTagOption(repo.GiftSub.BasePath.ToString(), repo.GiftSub.PaymentInfo.cardType, cardType);

           	//Enter credit card number
           	repo.GiftSub.PaymentInfo.cardNumber.PressKeys(cardNumber);
           	
           	//Enter name on credit card
           	repo.GiftSub.CCInfo.nameOnCard.PressKeys(nameOnCard);
           	
           	//Enter credit card security code
           	repo.GiftSub.CCInfo.cardSecurityCode.PressKeys(cardSecurityCode);
           	
           	//Enter expiration Month
           	SelectTagUI.ChooseSelectTagOption(repo.GiftSub.BasePath.ToString(), repo.GiftSub.CCInfo.expireMonth, expireMonth);
           	
           	//Enter expiration Year
           	SelectTagUI.ChooseSelectTagOption(repo.GiftSub.BasePath.ToString(), repo.GiftSub.CCInfo.expireYear, expireYear);
           	
           	//Check the Agree to Terms Box 
           	repo.GiftSub.PaymentInfo.AgreeTerms.Click();
           	
           	//Click the Continue Button
           	repo.GiftSub.PaymentInfo.continuebtn4.Click();
           	
           	//Verify new page loads
           	Validate.Exists(repo.GiftSub.submitbtn);
           	
           	//Click the submit button
           	repo.GiftSub.submitbtn.Click();
           	
           	//Verify new page loads
           	
           	
           	//Verify text exists
           	/* An email containing your message and the gift subscription activation key will be sent to:
				Mark Test  at  5-16-2012-48456@mailinator.com
			*/
			
			//Click the button labeled "go to lynda.com"
			repo.GiftSub.gotolyndabtn.Click();
			
			
           		
    }
}
}