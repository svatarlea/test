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

using Tests.AppConfig;
using Tests.General.Utilities.Forms;

namespace Tests.General.Tests.Educator
{
    /// <summary>
    /// Description of CreateEducator.
    /// </summary>
    [TestModule("FE2B6F36-8341-4D18-A9CF-7325F9390251", ModuleType.UserCode, 1)]
    public class CreateEducator : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CreateEducator()
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
            
            CreateEducatorRepo repo = CreateEducatorRepo.Instance;
            
            //e.g. admin.release.lynda.com/welcome.aspx
            string url = string.Format("admin.{0}/welcome.aspx", AppSettings.Domain);
			Browser browser = new Browser(AppSettings.Browser,url,true);

			//Deal with security warning per Browser		
			switch (AppSettings.Browser)
			{
				case BrowserProduct.IE:
					{
						repo.DOM.IECertificateErrorPage.OverrideLink.Click();	
						break;
					}
				case BrowserProduct.Firefox:
					{
						Validate.Exists(repo.DOM.FirefoxUntrustedConnectionPage.IUnderstandTheRisks);						
						repo.DOM.FirefoxUntrustedConnectionPage.IUnderstandTheRisks.Click();
						Validate.Exists(repo.DOM.FirefoxUntrustedConnectionPage.AddExceptionButton);
						repo.DOM.FirefoxUntrustedConnectionPage.AddExceptionButton.Click();
						//Make sure the check box exists before seeing if it is checked,
						//otherwise .Checked can return the incorrect result if the check box is not enabled yet in the dialog.
						Validate.Exists(repo.AddSecurityExceptionDialogFirefox.PermanentlyStoreThisExceptionCheckBoxChecked);
						if (repo.AddSecurityExceptionDialogFirefox.PermanentlyStoreThisExceptionCheckBoxChecked.Checked)
						{
							//Uncheck the box if it's checked
							repo.AddSecurityExceptionDialogFirefox.PermanentlyStoreThisExceptionCheckBoxChecked.Click();
						}
						Validate.Exists(repo.AddSecurityExceptionDialogFirefox.ConfirmSecurityExceptionButton);
						repo.AddSecurityExceptionDialogFirefox.ConfirmSecurityExceptionButton.Click();
						break;
					}
				case BrowserProduct.Safari:
					{
						repo.ReviewCoursesConfirmDialogSafari.ContinueButton.Click();
						break;
					}
				case BrowserProduct.Chrome:
					{
						//Handle Chrome "This is probably not the site you are looking for!" page
						//Ranorex doesn't support this page yet.
						//Click navigate edit box then tab to "Proceed anyway" button and press Enter for now.
						Text navigateEditBox = "/form[@title='SSL Error - Google Chrome']/element/text[@accessiblename='Address']";
						Validate.Exists(navigateEditBox);
						navigateEditBox.Click();
						Keyboard.Press(System.Windows.Forms.Keys.Tab);
						Keyboard.Press("{Enter}");
						break;						
					}
				default:
					throw new Exception(String.Format("Code not implemented yet: {0}", AppSettings.Browser.ToString()));
			}
			
			//Login
			repo.DOM.AdminWelcomePageNotLoggedIn.UsernameInput.PressKeys("knvirtualuser7");
			repo.DOM.AdminWelcomePageNotLoggedIn.PasswordInput.PressKeys("lynda1");
			repo.DOM.AdminWelcomePageNotLoggedIn.LoginInput.Click();
			
			//Deal with already logged in dialog (if it appears)
			//"Hello Keynote Virtualuser!You are currently logged in to your lynda.com account at another computer.
			//Would you like to log off the other computer and login to your account on this computer?"
			switch (AppSettings.Browser)
			{
				case BrowserProduct.Chrome:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogChrome.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogChrome.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							repo.CurrentlyLoggedInDialogChrome.OKButton.Click();
						}
						else
						{
							Report.Info("Chrome Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				case BrowserProduct.IE:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogIE.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogIE.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							repo.CurrentlyLoggedInDialogIE.OKButton.Click();
						}
						else
						{
							Report.Info("IE Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				case BrowserProduct.Firefox:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogFirefox.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogFirefox.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							Validate.Exists(repo.CurrentlyLoggedInDialogFirefox.OKButton);
							repo.CurrentlyLoggedInDialogFirefox.OKButton.Click();
						}
						else
						{
							Report.Info("Firefox Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				case BrowserProduct.Safari:
					{
						if (Validate.Exists(repo.CurrentlyLoggedInDialogSafari.HelloTextInfo.AbsolutePath.ToString(), repo.CurrentlyLoggedInDialogSafari.HelloTextInfo.SearchTimeout,
				        	"{0}", new Validate.Options(false,ReportLevel.Info)))
						{
							repo.CurrentlyLoggedInDialogSafari.OKButton.Click();
							//Workaround for bug where you click on the CS menu after clicking OK to the above dialog and the CS page doesn't appear;
							//workaround is to click the CS menu here before it is clicked again after this switch code block.						
							Validate.Exists(repo.DOM.AdminWelcomePageLoggedIn.WelcomeMessage);
							Validate.Exists(repo.DOM.AdminHeaderMenusAbstractPage.CSmenu);
							repo.DOM.AdminHeaderMenusAbstractPage.CSmenu.Click();	
							Report.Info("Clicking CS menu to workaround bug where the CS page doesn't appear on the first click. Bug http://bugzilla.ldcint.com/bugzilla/show_bug.cgi?id=11318");
						}
						else
						{
							Report.Info("Safari Already Logged In dialog not found, so no need to handle.");
						}
						break;
					}
				default:	
					throw new Exception(String.Format("Code not implemented yet: {0}", AppSettings.Browser.ToString()));
			}

			//Validate on Welcome page
			Validate.Exists(repo.DOM.AdminWelcomePageLoggedIn.WelcomeMessage);
			//Click CS			
			Validate.Exists(repo.DOM.AdminHeaderMenusAbstractPage.CSmenu);
			repo.DOM.AdminHeaderMenusAbstractPage.CSmenu.Click();			
			//Click New Account
			repo.DOM.AdminHeaderMenusAbstractPage.CSmenuNewAccount.Click();			
			//Select Educator Radio
			repo.DOM.AdminCSNewAccountPage.EducatorRadio.Click();			
			//Select Regular Radio
			Validate.Exists(repo.DOM.AdminCSNewAccountPage.RegularRadio);
			repo.DOM.AdminCSNewAccountPage.RegularRadio.Click();
			//Click continue
			repo.DOM.AdminCSNewAccountPage.ContinueButton.Click();
			
			//Fill-out Educator Registration step 1 page		
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep1Page.CountrySelect, "United States");
			repo.DOM.AdminCSRegStep1Page.FirstNameInput.PressKeys("Testfirstname");
			repo.DOM.AdminCSRegStep1Page.LastNameInput.PressKeys("Testlastname");
			repo.DOM.AdminCSRegStep1Page.PositionTitleInput.PressKeys("tester");
			repo.DOM.AdminCSRegStep1Page.DepartmentInput.PressKeys("testDepartment");
			repo.DOM.AdminCSRegStep1Page.SchoolInput.PressKeys("testSchool");
			repo.DOM.AdminCSRegStep1Page.PhoneInput.PressKeys("5555555555");
			string username,email;
			const string password = "lynda1";
			FormDataAccount.GenerateUsernameEmail(out username, out email);
			email = string.Format("edu{0}",email);
			username = string.Format("edu{0}",username);
			repo.DOM.AdminCSRegStep1Page.EmailInput.PressKeys(email);
			Report.Info(string.Format("Entering username:{0}",username));
			repo.DOM.AdminCSRegStep1Page.UsernameInput.PressKeys(username);
			repo.DOM.AdminCSRegStep1Page.PasswordInput.PressKeys(password);
			repo.DOM.AdminCSRegStep1Page.PasswordConfirmInput.PressKeys(password);
			Validate.Exists(repo.DOM.AdminCSRegStep1Page.ContinueButton);
			repo.DOM.AdminCSRegStep1Page.ContinueButton.Click();

			//Fill-out step 2 page
			repo.DOM.AdminCSRegStep2Page.ClassNameInput.PressKeys("classTest");
			repo.DOM.AdminCSRegStep2Page.ClassIDInput.PressKeys("1");
			System.DateTime startDateTime = System.DateTime.Now;    				
			string shortStartDateTime = startDateTime.ToShortDateString();
			System.DateTime endDateTime = startDateTime.AddDays(7);
			string shortEndDateTime = endDateTime.ToShortDateString();			
			repo.DOM.AdminCSRegStep2Page.StartDateInput.PressKeys(shortStartDateTime);
			repo.DOM.AdminCSRegStep2Page.EndDateInput.PressKeys(shortEndDateTime);
			Validate.Exists(repo.DOM.AdminCSRegStep2Page.ContinueButton);
			repo.DOM.AdminCSRegStep2Page.ContinueButton.Click();
			
			//step3 page
			Validate.Exists(repo.DOM.AdminCSRegStep3Page.AddCourseButton);
			repo.DOM.AdminCSRegStep3Page.AddCourseButton.Click();
			//Verify table appears
			Validate.Exists(repo.DOM.AdminCSRegStep3Page.CoursesTableColumn);			
			//Click Continue
			Validate.Exists(repo.DOM.AdminCSRegStep3Page.ContinueButton);
			repo.DOM.AdminCSRegStep3Page.ContinueButton.Click();
			
			//Click OK in review courses dialog
			switch (AppSettings.Browser)
			{
				case BrowserProduct.IE:
					{						
						Validate.Exists(repo.ReviewCoursesConfirmDialogIE.OKButton);
						Validate.IsTrue(repo.ReviewCoursesConfirmDialogIE.OKButton.Visible);
						repo.ReviewCoursesConfirmDialogIE.OKButton.Click();
						break;
					}
				case BrowserProduct.Firefox:
					{
						Validate.Exists(repo.ReviewCoursesConfirmDialogFirefox.OKButton);
						Validate.IsTrue(repo.ReviewCoursesConfirmDialogFirefox.OKButton.Visible);
						repo.ReviewCoursesConfirmDialogFirefox.OKButton.Click();
						break;
					}
				case BrowserProduct.Safari:
					{
						Validate.Exists(repo.ReviewCoursesConfirmDialogSafari.OKButton);
						Validate.IsTrue(repo.ReviewCoursesConfirmDialogSafari.OKButton.Visible);
						repo.ReviewCoursesConfirmDialogSafari.OKButton.Click();
						break;
					}
				case BrowserProduct.Chrome:
					{
						Validate.Exists(repo.ReviewCoursesConfirmDialogChrome.OKButton);
						Validate.IsTrue(repo.ReviewCoursesConfirmDialogChrome.OKButton.Visible);
						repo.ReviewCoursesConfirmDialogChrome.OKButton.Click();
						break;
					}
				default:
					throw new Exception(String.Format("Code not implemented yet: {0}", AppSettings.Browser.ToString()));
			}
				
			//step 4 page
			string usernameNotUsed,studentEmail;
			FormDataAccount.GenerateUsernameEmail(out usernameNotUsed, out studentEmail);
			string studentFirstName = "testStudentFirst";
			string studentLastName = "testStudentLast";
			repo.DOM.AdminCSRegStep4Page.FirstNameInput.PressKeys(studentFirstName);
			repo.DOM.AdminCSRegStep4Page.LastNameInput.PressKeys(studentLastName);
			repo.DOM.AdminCSRegStep4Page.EmailInput.PressKeys(studentEmail);
			repo.DOM.AdminCSRegStep4Page.AddStudentButton.Click();			
			//Verify table appears
			Validate.Exists(repo.DOM.AdminCSRegStep4Page.StudentNameTableColumn);
			//Click Continue
			Validate.Exists(repo.DOM.AdminCSRegStep4Page.ContinueButton);
			repo.DOM.AdminCSRegStep4Page.ContinueButton.Click();
					
			//step 5 page
			repo.DOM.AdminCSRegStep5Page.PaidBySchoolRadio.Click();
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep5Page.PaymentTypeSelect, "Credit Card");
			//billing info...
			repo.DOM.AdminCSRegStep5Page.BillingAddressInput.PressKeys("6410 via real");
			repo.DOM.AdminCSRegStep5Page.BillingAptSuiteInput.PressKeys("test");
			repo.DOM.AdminCSRegStep5Page.BillingCityInput.PressKeys("carpintera");	
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep5Page.BillingStateSelect, "California");
			repo.DOM.AdminCSRegStep5Page.BillingZipInput.PressKeys("93013");
			//credit card info...
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep5Page.CardTypeSelect, "Visa");
			repo.DOM.AdminCSRegStep5Page.CardNumberInput.PressKeys("4111111111111111");
			repo.DOM.AdminCSRegStep5Page.CardNameInput.PressKeys("Sue Axelband");
			repo.DOM.AdminCSRegStep5Page.CardCodeInput.PressKeys("670");
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep5Page.CardMonthSelect, "08 - August");
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep5Page.CardYearSelect, "2012");
			//sales info...
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep5Page.SalesRep1Select, "Donna Gill");
			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.AdminCSRegStep5Page.SalesOperationTypeSelect, "New");
			//click continue
			Validate.Exists(repo.DOM.AdminCSRegStep5Page.ContinueButton);
			repo.DOM.AdminCSRegStep5Page.ContinueButton.Click();

			//step 6 page.
			Validate.AreEqual(string.Compare(repo.DOM.AdminCSRegStep6Page.StudentEmail.InnerText.Trim(),studentEmail,false),0,
			                  "Actual:{0} Expected:{1}"+string.Format(" Actual:{0} Expected:{1}",repo.DOM.AdminCSRegStep6Page.StudentEmail.InnerText,studentEmail),true);
			//repo.DOM.AdminCSRegStep6Page.StudentName e.g. "testStudentFirst testStudentLast"
			string patternExpectedPageStudentName =  String.Format(@"^{0} {1}$",studentFirstName,studentLastName);
			string expectedPageStudentName = string.Format("{0} {1}",studentFirstName,studentLastName);
			string actualPageStudentName = repo.DOM.AdminCSRegStep6Page.StudentName.InnerText.Trim();
			if (!Regex.IsMatch(actualPageStudentName, patternExpectedPageStudentName)) 
			{
				throw new Ranorex.ValidationException(string.Format("Student Name. Actual:\"{0}\" Expected:\"{1}\"",
				                             actualPageStudentName,expectedPageStudentName));				
			}
			Validate.Exists(repo.DOM.AdminCSRegStep6Page.ApproveNowButton);
			repo.DOM.AdminCSRegStep6Page.ApproveNowButton.Click();

			//reg confirmation page. Click continue.
			Validate.Exists(repo.DOM.AdminCSRegConfirmPage.ContinueButton);
			repo.DOM.AdminCSRegConfirmPage.ContinueButton.Click();
			//Wait for Customer Details page.
			Validate.Exists(repo.DOM.AdminDisplayCustomerPage.CustomerDetailsText);
			
			//click logout on customer details display customer page
			Validate.Exists(repo.DOM.AdminHeaderMenusAbstractPage.LogoutButton);
			repo.DOM.AdminHeaderMenusAbstractPage.LogoutButton.Click();
			//Wait for login button to appear to signify logout completion
			Validate.Exists(repo.DOM.AdminWelcomePageNotLoggedIn.LoginInput);
			
			//login and handle terms and conditions page
			browser.Navigate(string.Format("{0}", AppSettings.Domain));
			Validate.Exists(repo.DOM.MemberHomePage.LoginLink);
			repo.DOM.MemberHomePage.LoginLink.Click();
			Validate.Exists(repo.DOM.MemberHomePage.LoginUsername);
			repo.DOM.MemberHomePage.LoginUsername.PressKeys(username);
			repo.DOM.MemberHomePage.LoginPassword.PressKeys(password);
			repo.DOM.MemberHomePage.LoginButton.Click();
			Duration waitForAcceptButtonNotExistTime = new Duration(10000);
			switch (AppSettings.Browser)
			{
				case BrowserProduct.IE:
					{
						Validate.Exists(repo.TermsAndConditionsPageIE.IAcceptButton);
						repo.TermsAndConditionsPageIE.IAcceptButton.EnsureVisible();
						repo.TermsAndConditionsPageIE.IAcceptButton.Click();
						repo.TermsAndConditionsPageIE.IAcceptButtonInfo.WaitForNotExists(waitForAcceptButtonNotExistTime);
						break;
					}
				case BrowserProduct.Firefox:
					{
						Validate.Exists(repo.TermsAndConditionsPageFirefox.IAcceptButton);
						repo.TermsAndConditionsPageFirefox.IAcceptButton.EnsureVisible();
						repo.TermsAndConditionsPageFirefox.IAcceptButton.Click();
						repo.TermsAndConditionsPageFirefox.IAcceptButtonInfo.WaitForNotExists(waitForAcceptButtonNotExistTime);
						break;
					}
				case BrowserProduct.Safari:
					{
						Validate.Exists(repo.TermsAndConditionsPageSafari.IAcceptButton);
						repo.TermsAndConditionsPageSafari.IAcceptButton.EnsureVisible();
						repo.TermsAndConditionsPageSafari.IAcceptButton.Click();
						repo.TermsAndConditionsPageSafari.IAcceptButtonInfo.WaitForNotExists(waitForAcceptButtonNotExistTime);
						//Due to issue with Ranorex with Safari in identifying the DOM at this point,
						//navigate to member home page a couple of times so the DOM is visible.
						for (int i=0; i<=1; i++)
						{
							browser.Navigate(string.Format("{0}", AppSettings.Domain));
						}
						break;
					}
				case BrowserProduct.Chrome:
					{
						Validate.Exists(repo.TermsAndConditionsPageChrome.IAcceptButton);
						repo.TermsAndConditionsPageChrome.IAcceptButton.EnsureVisible();
						repo.TermsAndConditionsPageChrome.IAcceptButton.Click();
						repo.TermsAndConditionsPageChrome.IAcceptButtonInfo.WaitForNotExists(waitForAcceptButtonNotExistTime);
						break;
					}
				default:
					throw new Exception(String.Format("Code not implemented yet: {0}", AppSettings.Browser.ToString()));
			}
			//Verify now on member home page; "My courses" shows.
			Validate.Exists(repo.DOM.MemberHomePage.MyCoursesText);
			//Click log out link.
			Validate.Exists(repo.DOM.MemberHomePage.LogoutLink);
			repo.DOM.MemberHomePage.LogoutLink.Click();
			//Verify logout complete; check for login link
			Validate.Exists(repo.DOM.MemberHomePage.LoginLink);

        }
    }
}
