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
using Tests.General.Utilities.Forms;
using Lynda.Test.Browsers;

using General.Utilities.Forms;

namespace Tests.General.Tests.FreeTrial
{
    /// <summary>
    /// Description of FreeTrial.
    /// </summary>
    [TestModule("709775BA-90FF-4205-9CAD-28D09B5A813D", ModuleType.UserCode, 1)]
    
    public class FreeTrial : ITestModule
    {
        
        private static FreeTrialrepo repo = FreeTrialrepo.Instance;
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FreeTrial()
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
            //const string domain = "50.19.219.59:3001/gromble";
            //const string domain = "integration.lynda.com";
            //const string domain = "stage.lynda.com";
            const string navigateTo = "/promo/trial/default.aspx?lpk35=2547";
			const BrowserProduct browserProduct = BrowserProduct.Firefox;
			
            string url = string.Format("http://{0}{1}", domain, navigateTo.ToString());
            Browser browser = new Browser(browserProduct, url);

                      
            //Validate the Lynda Welcome Video is present
            //This validation fails bc the id changes each time the page is loaded.
            //Validate.Exists(repo.WebDocumentN7_Day_Trial___lynda_com.ObjectTagLynda_video_4638);
            
            //Validate the Most Popular Course Tabs Load
            Validate.Exists(repo.dom.coursetabs.LiTagAudio);
            Validate.Exists(repo.dom.coursetabs.LiTagBusiness);
            Validate.Exists(repo.dom.coursetabs.LiTagDesign);
            Validate.Exists(repo.dom.coursetabs.LiTagDeveloper);
            Validate.Exists(repo.dom.coursetabs.LiTagDocumentaries);
            Validate.Exists(repo.dom.coursetabs.LiTagN3D);
            Validate.Exists(repo.dom.coursetabs.LiTagPhotography);
            Validate.Exists(repo.dom.coursetabs.LiTagVideo);
            Validate.Exists(repo.dom.coursetabs.LiTagWeb);
            
            //Click each of the Most Popular Course Tabs
            repo.dom.coursetabs.LiTagAudio.Click();
            repo.dom.coursetabs.LiTagBusiness.Click();
            repo.dom.coursetabs.LiTagDesign.Click();
            repo.dom.coursetabs.LiTagDeveloper.Click();
            repo.dom.coursetabs.LiTagDocumentaries.Click();
            repo.dom.coursetabs.LiTagN3D.Click();
            repo.dom.coursetabs.LiTagPhotography.Click();
            repo.dom.coursetabs.LiTagVideo.Click();
            repo.dom.coursetabs.LiTagWeb.Click();
            
            //Validate Start Free Trial Button exists
            Validate.Exists(repo.dom.starttrial.ATagStart_free_trial);
            
            //Click the free trial button
            repo.dom.starttrial.ATagStart_free_trial.Click();
            
            //Fill out Account Information
            repo.dom.actinfo.firstname.PressKeys(FormDataAccount.GenerateFirstName());
            repo.dom.actinfo.lastname.PressKeys(FormDataAccount.GenerateLastName());
            
			string username,email;
           	FormDataAccount.GenerateUsernameEmail(out username, out email);
           	repo.dom.actinfo.email.PressKeys(email);
           	repo.dom.actinfo.username.PressKeys(username);
	
           	string password = FormDataAccount.GeneratePassword();
           	repo.dom.actinfo.password.PressKeys(password);
           	repo.dom.actinfo.passwordconfirm.PressKeys(password);
           	
           	//Click the continue button
            repo.dom.actinfo.continuebtn.Click();
            
            //Fill out Billing Information
           	string companyName, address, aptSuite, city, state, zip, country, phone;
            FormDataBilling.GenerateAddress(out companyName, out address, out aptSuite, out city,
           	                                out state, out zip, out country, out phone);
           	
           	//SelectTagUI.ChooseSelectTagOption(repo.dom.ToString(), repo.dom.billinginfo.country, country);
            repo.dom.billinginfo.firstname.PressKeys("Mark");
			repo.dom.billinginfo.lastname.PressKeys("Jordan");
			repo.dom.billinginfo.company.PressKeys(companyName);
           	repo.dom.billinginfo.address1.PressKeys(address);
           	repo.dom.billinginfo.address2.PressKeys(aptSuite);
            repo.dom.billinginfo.city.PressKeys(city);
           	SelectTagUI.ChooseSelectTagOption(repo.dom.BasePath.ToString(), repo.dom.billinginfo.state, state);
           	repo.dom.billinginfo.zip.PressKeys(zip);         	
           	repo.dom.billinginfo.phone.PressKeys(phone);
           	
           	//Fill out Payment Information
           	string paymentType, cardType, cardNumber, nameOnCard, cardSecurityCode, expireMonth, expireYear;
           	FormDataPayment.GenerateCreditCard(out paymentType, out cardType, out cardNumber, out nameOnCard,
           	                                   out cardSecurityCode, out expireMonth, out expireYear);
           	SelectTagUI.ChooseSelectTagOption(repo.dom.BasePath.ToString(), repo.dom.cctype.cctype, cardType);
           	repo.dom.ccinfo.ccnumber.PressKeys(cardNumber);
           	repo.dom.ccinfo.ccname.PressKeys(nameOnCard);
           	repo.dom.ccinfo.cccode.PressKeys(cardSecurityCode);
           	SelectTagUI.ChooseSelectTagOption(repo.dom.BasePath.ToString(), repo.dom.ccinfo.expmo, expireMonth);
           	SelectTagUI.ChooseSelectTagOption(repo.dom.BasePath.ToString(), repo.dom.ccinfo.expyr, expireYear);
           	SelectTagUI.ChooseSelectTagOption(repo.dom.BasePath.ToString(), repo.dom.terms.aboutus, FormDataBilling.GenerateHowDidYouHear()); 
           	repo.dom.terms.terms.Click();
            repo.dom.terms.submit.Click();
            
            //Wait for Confirmation page to load
            Validate.Exists(repo.confirmationpage.confirmationtxt);
            
            //Click the continue button
            repo.confirmationpage.continuebtn.Click();
            
  
        }
    }
}
