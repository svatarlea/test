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

using Tests.General.Utilities.Forms;
using Tests.AppConfig;

namespace Tests.General.Tests.ConsumerReg
{
    /// <summary>
    /// Description of ConsumerReg.
    /// </summary>
    [TestModule("6FDE7386-99FE-435D-B4B3-260F15C5E33A", ModuleType.UserCode, 1)]
    public class ConsumerRegTest1 : ITestModule
    {
        private static ConsumerRegRepo repo = ConsumerRegRepo.Instance;
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public ConsumerRegTest1()
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
            Mouse.DefaultMoveTime = AppSettings.MouseDefaultMoveTime;
            Keyboard.DefaultKeyPressTime = AppSettings.KeyboardDefaultKeyPressTime;
            Delay.SpeedFactor = 1.0;

            const string navigateTo = "/";
            
            //Open browser and navigate to url           
            string url = string.Format("http://{0}{1}", AppSettings.Domain, navigateTo.ToString());
            Browser browser = new Browser(AppSettings.Browser, url);

            //Wait for page to load
            Validate.Exists(repo.MemberPage.SubscribeLink);

           	//Click Subscribe and wait for regpagestep1 to load
           	repo.MemberPage.SubscribeLink.Click();
           	Validate.Exists(repo.RegStep1Page.MonthlyPremiumRadio);
           	
           	//Select Subscription Plan
           	repo.RegStep1Page.MonthlyPremiumRadio.Click();

           	//Fill out Account Information           	
            repo.RegStep1Page.AccountInfo.FirstNameInput.PressKeys(FormDataAccount.GenerateFirstName());
            repo.RegStep1Page.AccountInfo.LastNameInput.PressKeys(FormDataAccount.GenerateLastName());          	
           	
            string username,email;
           	FormDataAccount.GenerateUsernameEmail(out username, out email);                     	
           	repo.RegStep1Page.AccountInfo.EmailInput.PressKeys(email);           	
           	repo.RegStep1Page.AccountInfo.UserNameInput.PressKeys(username);
           	
           	string password = FormDataAccount.GeneratePassword();
           	repo.RegStep1Page.AccountInfo.PasswordInput.PressKeys(password);
           	repo.RegStep1Page.AccountInfo.PasswordConfirmInput.PressKeys(password);
           	
           	//If checkboxes are checked, uncheck them
           	if (Convert.ToBoolean(repo.RegStep1Page.SignMeUp.MonthlyCheckBox.Checked))
           	{
           		repo.RegStep1Page.SignMeUp.MonthlyCheckBox.Click();
           	}
           	
           	if (Convert.ToBoolean(repo.RegStep1Page.SignMeUp.NewReleasesCheckBox.Checked))
           	{
           		repo.RegStep1Page.SignMeUp.NewReleasesCheckBox.Click();
           	}
           	
           	if (Convert.ToBoolean(repo.RegStep1Page.SignMeUp.SpecialCheckBox.Checked))
           	{
           		repo.RegStep1Page.SignMeUp.SpecialCheckBox.Click();
           	}
           	
           	//Click Continue button and wait for regpagestep2 to load
           	repo.RegStep1Page.ContinueButton.Click();
           	Validate.Exists(repo.RegStep2Page.SubmitButton);
           	
           	//Fill out Billing Information
           	string companyName, address, aptSuite, city, state, zip, country, phone;
           	FormDataBilling.GenerateAddress(out companyName, out address, out aptSuite, out city,
           	                                out state, out zip, out country, out phone);
           	repo.RegStep2Page.BillingInfo.CompanyInput.PressKeys(companyName);
           	repo.RegStep2Page.BillingInfo.AddressInput.PressKeys(address);
           	repo.RegStep2Page.BillingInfo.AptSuiteInput.PressKeys(aptSuite);
           	repo.RegStep2Page.BillingInfo.CityInput.PressKeys(city);
           	SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.RegStep2Page.BillingInfo.StateSelect, state);
           	repo.RegStep2Page.BillingInfo.ZipInput.PressKeys(zip);         	
           	SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.RegStep2Page.BillingInfo.CountrySelect, country);
           	repo.RegStep2Page.BillingInfo.PhoneInput.PressKeys(phone);
           	SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.RegStep2Page.BillingInfo.HowHearSelect, FormDataBilling.GenerateHowDidYouHear());

           	//Fill out Payment Information
           	string paymentType, cardType, cardNumber, nameOnCard, cardSecurityCode, expireMonth, expireYear;
           	FormDataPayment.GenerateCreditCard(out paymentType, out cardType, out cardNumber, out nameOnCard,
           	                                   out cardSecurityCode, out expireMonth, out expireYear);
           	SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.RegStep2Page.PaymentInfo.PaymentTypeSelect, paymentType);
           	SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.RegStep2Page.PaymentInfo.CardTypeSelect, cardType);
           	repo.RegStep2Page.PaymentInfo.CardNumberInput.PressKeys(cardNumber);
           	repo.RegStep2Page.PaymentInfo.NameOnCardInput.PressKeys(nameOnCard);
           	repo.RegStep2Page.PaymentInfo.CardSecurityCodeInput.PressKeys(cardSecurityCode);
           	SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.RegStep2Page.PaymentInfo.CardExpirationMonthSelect, expireMonth);
           	SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.RegStep2Page.PaymentInfo.CardExpirationYearSelect, expireYear);

           	//If "I have read and..." check box is checked then fail
           	Validate.IsFalse(Convert.ToBoolean(repo.RegStep2Page.IHaveReadCheckBox.Checked));
           	//Check the box
            repo.RegStep2Page.IHaveReadCheckBox.Click();
            
            //Click Submit and wait for Registration Confirm page to load
            repo.RegStep2Page.SubmitButton.Click();
            Validate.Exists(repo.RegConfirmPage.PrintReceiptButton);
            
            //Log out and wait for member page to load
            repo.RegConfirmPage.LogoutLink.Click();
            Validate.Exists(repo.MemberPage.SubscribeLink);
           	
        }
    }
}
